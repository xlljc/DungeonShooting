
using Godot;

/// <summary>
/// Ui节点接口
/// </summary>
public interface IUiNode
{
    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    UiBase OpenNestedUi(string uiName, UiBase prevUi = null);

    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    T OpenNestedUi<T>(string uiName, UiBase prevUi = null) where T : UiBase;
    
    /// <summary>
    /// 获取所属Ui面板
    /// </summary>
    UiBase GetUiPanel();
    
    /// <summary>
    /// 获取Ui实例
    /// </summary>
    Node GetUiInstance();

    /// <summary>
    /// 获取克隆的Ui实例
    /// </summary>
    IUiCellNode CloneUiCell();

    /// <summary>
    /// 添加子级Ui节点
    /// </summary>
    void AddChild(IUiNode uiNode);

    /// <summary>
    /// 移除子级Ui节点
    /// </summary>
    void RemoveChild(IUiNode uiNode);
}
