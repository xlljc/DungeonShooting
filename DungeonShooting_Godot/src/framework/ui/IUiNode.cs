
using Godot;

/// <summary>
/// Ui节点接口
/// </summary>
public interface IUiNode
{
    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public UiBase OpenNestedUi(string uiName, UiBase prevUi = null);

    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public T OpenNestedUi<T>(string uiName, UiBase prevUi = null) where T : UiBase;
    
    /// <summary>
    /// 获取所属Ui面板
    /// </summary>
    public UiBase GetUiPanel();
    
    /// <summary>
    /// 获取Ui实例
    /// </summary>
    public Node GetUiInstance();

    /// <summary>
    /// 获取克隆的Ui实例
    /// </summary>
    public IUiCellNode CloneUiCell();
}
