using System;
using Godot;
using System.Collections.Generic;

/// <summary>
/// 对象状态机控制器
/// </summary>
public class StateController<T, S> : Component<T> where T : ActivityObject where S : Enum
{
    /// <summary>
    /// 获取当前状态
    /// </summary>
    public S CurrState => CurrStateBase != null ? CurrStateBase.State : default;
    
    /// <summary>
    /// 当前活跃的状态对象实例
    /// </summary>
    public StateBase<T, S> CurrStateBase { get; private set; }

    /// <summary>
    /// 负责存放状态实例对象
    /// </summary>
    private readonly Dictionary<S, StateBase<T, S>> _states = new Dictionary<S, StateBase<T, S>>();

    /// <summary>
    /// 记录下当前帧是否有改变的状态
    /// </summary>
    private bool _isChangeState;

    public override void Process(float delta)
    {
        _isChangeState = false;
        if (CurrStateBase != null)
        {
            CurrStateBase.Process(delta);
            //判断当前帧是否有改变的状态, 如果有, 则重新调用 Process() 方法
            if (_isChangeState)
            {
                Process(delta);
            }
        }
    }

    public override void DebugDraw()
    {
        if (CurrStateBase != null)
        {
            CurrStateBase.DebugDraw();
        }
    }

    /// <summary>
    /// 往状态机里注册一个新的状态实例
    /// </summary>
    public void Register(StateBase<T, S> stateBase)
    {
        if (GetStateInstance(stateBase.State) != null)
        {
            Debug.LogError("当前状态已经在状态机中注册:", stateBase);
            return;
        }

        stateBase.Master = Master;
        stateBase.StateController = this;
        _states.Add(stateBase.State, stateBase);
    }

    /// <summary>
    /// 返回该状态机控制器中是否存在指定的状态实例
    /// </summary>
    public bool HasState(S state)
    {
        return _states.ContainsKey(state);
    }
    
    /// <summary>
    /// 获取指定状态对应的实例对象
    /// </summary>
    public StateBase<T, S> GetState(S state)
    {
        if (_states.TryGetValue(state, out var temp))
        {
            return temp;
        }

        return null;
    }

    /// <summary>
    /// 立即切换到下一个指定状态, 并且这一帧会被调用 Process
    /// </summary>
    public void ChangeStateInstant(S next, params object[] arg)
    {
        _changeState(false, next, arg);
    }

    /// <summary>
    /// 切换到下一个指定状态, 下一帧才会调用 Process
    /// </summary>
    public void ChangeState(S next, params object[] arg)
    {
        _changeState(true, next, arg);
    }

    /// <summary>
    /// 根据状态类型获取相应的状态对象
    /// </summary>
    private StateBase<T, S> GetStateInstance(S stateType)
    {
        _states.TryGetValue(stateType, out var v);
        return v;
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    private void _changeState(bool late, S next, params object[] arg)
    {
        if (CurrStateBase != null && CurrStateBase.State.Equals(next))
        {
            return;
        }

        var newState = GetStateInstance(next);
        if (newState == null)
        {
            Debug.LogError("当前状态机未找到相应状态:" + next);
            return;
        }

        if (CurrStateBase == null)
        {
            CurrStateBase = newState;
            newState.Enter(default, arg);
        }
        else if (CurrStateBase.CanChangeState(next))
        {
            _isChangeState = !late;
            var prev = CurrStateBase.State;
            CurrStateBase.Exit(next);
            CurrStateBase = newState;
            CurrStateBase.Enter(prev, arg);
        }
    }
}