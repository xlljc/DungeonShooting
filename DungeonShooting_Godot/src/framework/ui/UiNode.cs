
using Godot;

/// <summary>
/// Ui节点父类, 无泛型无属性
/// </summary>
public abstract class UiNode
{
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
    
    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public UiBase OpenNestedUi(string uiName)
    {
        var packedScene = ResourceManager.Load<PackedScene>("res://" + GameConfig.UiPrefabDir + uiName + ".tscn");
        var uiBase = packedScene.Instantiate<UiBase>();
        Instance.AddChild(uiBase);
        UiPanel.RecordNestedUi(uiBase, UiManager.RecordType.Open);
        
        uiBase.OnCreateUi();
        if (UiPanel.IsOpen)
        {
            uiBase.ShowUi();
        }
        
        return uiBase;
    }
    
    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public T OpenNestedUi<T>(string uiName) where T : UiBase
    {
        return (T)OpenNestedUi(uiName);
    }

    public Node GetUiInstance()
    {
        return Instance;
    }

    public IUiCellNode CloneUiCell()
    {
        return Clone();
    }
}