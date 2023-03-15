
using Godot;

/// <summary>
/// Ui节点代码接口
/// </summary>
/// <typeparam name="TNodeType">Godot中的节点类型</typeparam>
/// <typeparam name="TCloneType">克隆该对象返回的类型</typeparam>
public abstract class IUiNode<TNodeType, TCloneType> where TNodeType : Node
{
    /// <summary>
    /// Godot节点实例
    /// </summary>
    public TNodeType Instance { get; }
    /// <summary>
    /// 克隆当前对象, 并返回新的对象,
    /// 注意: 如果子节点改名或者移动层级, 那么有可能对导致属性中的子节点无法访问
    /// </summary>
    public abstract TCloneType Clone();

    public IUiNode(TNodeType node)
    {
        Instance = node;
    }
    
    public UiBase OpenNestedUi(string resourcePath, params object[] args)
    {
        var packedScene = ResourceManager.Load<PackedScene>(resourcePath);
        var uiBase = packedScene.Instantiate<UiBase>();
        Instance.AddChild(uiBase);
        uiBase.OnCreateUi();
        uiBase.ShowUi(args);
        return uiBase;
    }
    
    public T OpenNestedUi<T>(string resourcePath) where T : UiBase
    {
        return (T)OpenNestedUi(resourcePath);
    }
}