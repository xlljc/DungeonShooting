
using Godot;

/// <summary>
/// 用于网格中的cell对象
/// </summary>
public interface IUiCellNode
{
    /// <summary>
    /// 获取节点实例
    /// </summary>
    Node GetUiInstance();

    /// <summary>
    /// 克隆并返回新的节点实例
    /// </summary>
    IUiCellNode CloneUiCell();

    /// <summary>
    /// 获取所属 Ui 面板
    /// </summary>
    UiBase GetUiPanel();
}