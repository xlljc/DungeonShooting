
using Godot;

/// <summary>
/// Ui节点父类
/// </summary>
/// <typeparam name="TUi">所属Ui面板类型</typeparam>
/// <typeparam name="TNodeType">Godot中的节点类型</typeparam>
/// <typeparam name="TCloneType">克隆该对象返回的类型</typeparam>
public abstract class UiNode<TUi, TNodeType, TCloneType>
    : IUiNode, IUiCellNode, IClone<TCloneType>
    where TUi : UiBase
    where TNodeType : Node
    where TCloneType : IUiCellNode
{
    /// <summary>
    /// 当前Ui节点所属的Ui面板对象
    /// </summary>
    public TUi UiPanel { get; }
    /// <summary>
    /// Godot节点实例
    /// </summary>
    public TNodeType Instance { get; }
    /// <summary>
    /// 克隆当前对象, 并返回新的对象,
    /// 注意: 如果子节点改名或者移动层级, 那么有可能对导致属性中的子节点无法访问
    /// </summary>
    public abstract TCloneType Clone();

    public UiNode(TUi uiPanel, TNodeType node)
    {
        UiPanel = uiPanel;
        Instance = node;
        if (node is IUiNodeScript uiNodeScript)
        {
            uiNodeScript.SetUiNode(this);
        }
    }
    
    //已知问题: 通过 OpenNestedUi() 打开子Ui, 然后再克隆当前节点, 被克隆出来的节点的子Ui不会被调用生命周期函数, 也就是没有被记录
    public UiBase OpenNestedUi(string uiName, UiBase prevUi = null)
    {
        var packedScene = ResourceManager.Load<PackedScene>("res://" + GameConfig.UiPrefabDir + uiName + ".tscn");
        var uiBase = packedScene.Instantiate<UiBase>();
        uiBase.PrevUi = prevUi;
        Instance.AddChild(uiBase);
        UiPanel.RecordNestedUi(uiBase, this, UiManager.RecordType.Open);
        
        uiBase.OnCreateUi();
        uiBase.OnInitNestedUi();
        if (UiPanel.IsOpen)
        {
            uiBase.ShowUi();
        }
        
        return uiBase;
    }

    public T OpenNestedUi<T>(string uiName, UiBase prevUi = null) where T : UiBase
    {
        return (T)OpenNestedUi(uiName, prevUi);
    }

    /// <summary>
    /// 克隆当前节点, 并放到同父节点下
    /// </summary>
    public TCloneType CloneAndPut()
    {
        var inst = Clone();
        Instance.GetParent().AddChild(inst.GetUiInstance());
        return inst;
    }

    public UiBase GetUiPanel()
    {
        return UiPanel;
    }

    public Node GetUiInstance()
    {
        return Instance;
    }

    public IUiCellNode CloneUiCell()
    {
        return Clone();
    }

    public void AddChild(IUiNode uiNode)
    {
        Instance.AddChild(uiNode.GetUiInstance());
    }

    public void AddChild(Node node)
    {
        Instance.AddChild(node);
    }
    
    public void RemoveChild(IUiNode uiNode)
    {
        Instance.RemoveChild(uiNode.GetUiInstance());
    }
    
    public void RemoveChild(Node node)
    {
        Instance.RemoveChild(node);
    }

    public void QueueFree()
    {
        Instance.QueueFree();
    }

    public void Reparent(IUiNode uiNode)
    {
        Instance.Reparent(uiNode.GetUiInstance());
    }
    
    public void Reparent(Node node)
    {
        Instance.Reparent(node);
    }

    public Node GetParent()
    {
        return Instance.GetParent();
    }
}