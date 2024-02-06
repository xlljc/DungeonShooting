
/// <summary>
/// 可被 ActivityObject 挂载的物体
/// </summary>
public interface IMountItem
{
    /// <summary>
    /// 挂载到 ActivityObject 时调用
    /// </summary>
    void OnMount(ActivityObject target);
    
    /// <summary>
    /// 从 ActivityObject 卸载时调用
    /// </summary>
    void OnUnmount(ActivityObject target);
}