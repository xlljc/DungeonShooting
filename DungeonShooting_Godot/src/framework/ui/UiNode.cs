
using Godot;

/// <summary>
/// Ui节点父类, 无泛型无属性
/// </summary>
public abstract class UiNode
{
    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public abstract UiBase OpenNestedUi(string uiName, UiBase prevUi = null);

    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public abstract T OpenNestedUi<T>(string uiName, UiBase prevUi = null) where T : UiBase;
    
    /// <summary>
    /// 获取所属Ui面板
    /// </summary>
    public abstract UiBase GetUiPanel();
    
    /// <summary>
    /// 获取Ui实例
    /// </summary>
    public abstract Node GetUiInstance();

    /// <summary>
    /// 获取克隆的Ui实例
    /// </summary>
    public abstract IUiCellNode CloneUiCell();
}

/// <summary>
/// Ui节点父类
/// </summary>
/// <typeparam name="TUi">所属Ui面板类型</typeparam>
/// <typeparam name="TNodeType">Godot中的节点类型</typeparam>
/// <typeparam name="TCloneType">克隆该对象返回的类型</typeparam>
public abstract class UiNode<TUi, TNodeType, TCloneType>
    : UiNode, IUiCellNode, IClone<TCloneType>
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
    }
    
    public override UiBase OpenNestedUi(string uiName, UiBase prevUi = null)
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
    
    public override T OpenNestedUi<T>(string uiName, UiBase prevUi = null)
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

    public override UiBase GetUiPanel()
    {
        return UiPanel;
    }

    public override Node GetUiInstance()
    {
        return Instance;
    }

    public override IUiCellNode CloneUiCell()
    {
        return Clone();
    }
}