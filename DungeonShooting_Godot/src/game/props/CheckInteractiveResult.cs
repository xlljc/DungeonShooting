
/// <summary>
/// 检测互动返回的数据集
/// </summary>
public class CheckInteractiveResult
{
    /// <summary>
    /// 互动物体
    /// </summary>
    public ActivityObject Target;
    /// <summary>
    /// 是否可以互动
    /// </summary>
    public bool CanInteractive;
    /// <summary>
    /// 互动提示信息
    /// </summary>
    public string Message;
    /// <summary>
    /// 互动提示显示的图标
    /// </summary>
    public string ShowIcon;

    public CheckInteractiveResult(ActivityObject target)
    {
        Target = target;
    }
}