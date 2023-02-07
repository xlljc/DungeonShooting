
/// <summary>
/// 帧循环接口
/// </summary>
public interface IProcess
{
    /// <summary>
    /// 普通帧每帧调用
    /// </summary>
    void Process(float delta);

    /// <summary>
    /// 物理帧每帧调用
    /// </summary>
    void PhysicsProcess(float delta);
}