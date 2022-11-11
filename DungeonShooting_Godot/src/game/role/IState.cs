/// <summary>
/// 状态接口
/// </summary>
public interface IState<T> where T : ActivityObject
{
    /// <summary>
    /// 当前状态对象对应的状态枚举类型
    /// </summary>
    StateEnum StateType { get; }

    /// <summary>
    /// 当前状态对象挂载的角色对象
    /// </summary>
    T Master { get; set; }

    /// <summary>
    /// 当前状态对象所处的状态机对象
    /// </summary>
    StateController<T> StateController { get; set; }

    /// <summary>
    /// 当从其他状态进入到当前状态时调用
    /// </summary>
    /// <param name="prev">上一个状态类型</param>
    /// <param name="args">切换当前状态时附带的参数</param>
    void Enter(StateEnum prev, params object[] args);

    /// <summary>
    /// 物理帧每帧更新
    /// </summary>
    void PhysicsProcess(float delta);

    /// <summary>
    /// 是否允许切换至下一个状态
    /// </summary>
    /// <param name="next">下一个状态类型</param>
    bool CanChangeState(StateEnum next);

    /// <summary>
    /// 从当前状态退出时调用
    /// </summary>
    /// <param name="next">下一个状态类型</param>
    void Exit(StateEnum next);
}