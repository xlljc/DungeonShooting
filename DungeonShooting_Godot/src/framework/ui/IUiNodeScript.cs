
/// <summary>
/// Ui节点脚本接口, 用于脚本便捷获取所属Ui节点对象
/// </summary>
public interface IUiNodeScript
{
    /// <summary>
    /// 设置所属Ui节点对象
    /// </summary>
    void SetUiNode(IUiNode uiNode);
}