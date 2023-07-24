using System;

/// <summary>
/// 状态基类
/// </summary>
public abstract class StateBase<T, S> where T : ActivityObject where S : Enum
{
    /// <summary>
    /// 当前活跃的状态对象实例
    /// </summary>
    public StateBase<T, S> CurrStateBase => StateController.CurrStateBase;
    
    /// <summary>
    /// 当前对象对应的状态枚举
    /// </summary>
    public S State { get; }

    /// <summary>
    /// 当前状态对象挂载的角色对象
    /// </summary>
    public T Master { get; set; }

    /// <summary>
    /// 当前状态对象所处的状态机对象
    /// </summary>
    public StateController<T, S> StateController { get; set; }

    public StateBase(S state)
    {
        State = state;
    }
    
    /// <summary>
    /// 当从其他状态进入到当前状态时调用
    /// </summary>
    /// <param name="prev">上一个状态类型</param>
    /// <param name="args">切换当前状态时附带的参数</param>
    public virtual void Enter(S prev, params object[] args)
    {
        
    }

    /// <summary>
    /// 如果当前状态已被激活, 帧每帧更新
    /// </summary>
    public virtual void Process(float delta)
    {
        
    }

    /// <summary>
    /// 是否允许切换至下一个状态, 该函数由状态机控制器调用, 不需要手动调用
    /// </summary>
    /// <param name="next">下一个状态类型</param>
    public virtual bool CanChangeState(S next)
    {
        return true;
    }

    /// <summary>
    /// 从当前状态退出时调用
    /// </summary>
    /// <param name="next">下一个状态类型</param>
    public virtual void Exit(S next)
    {
        
    }
    
    /// <summary>
    /// 当启用 debug 后调用该函数, 调试绘制, 需要调用 Master 身上的绘制函数
    /// </summary>
    public virtual void DebugDraw()
    {
        
    }

    /// <summary>
    /// 立即切换到下一个指定状态, 并且这一帧会被调用 Process
    /// </summary>
    public void ChangeStateInstant(S next, params object[] args)
    {
        StateController.ChangeStateInstant(next, args);
    }
    
    /// <summary>
    /// 切换到下一个指定状态, 下一帧才会调用 Process
    /// </summary>
    public void ChangeState(S next, params object[] args)
    {
        StateController.ChangeState(next, args);
    }
}