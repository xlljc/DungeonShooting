
/// <summary>
/// Ui节点脚本接口, 用于脚本便捷获取所属Ui节点对象<br/>
/// 注意: 由于 Godot 编辑器不会自动编译代码, 所以新创建的脚本如果实现该接口请先编译代码
/// </summary>
public interface IUiNodeScript
{
    /// <summary>
    /// 设置所属Ui节点对象, 该函数会在 _Ready() 之后调用
    /// </summary>
    void SetUiNode(IUiNode uiNode);

    /// <summary>
    /// 当前Ui被销毁时调用
    /// </summary>
    void OnDestroy();
}