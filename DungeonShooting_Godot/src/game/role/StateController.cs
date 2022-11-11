using Godot;
using System.Collections.Generic;

/// <summary>
/// 对象状态机控制器
/// </summary>
public class StateController<T> : Component where T : ActivityObject
{
    /// <summary>
    /// 当前活跃的状态
    /// </summary>
    public IState<T> CurrState => _currState;
    private IState<T> _currState;
    
    /// <summary>
    /// 负责存放状态实例对象
    /// </summary>
    private readonly Dictionary<StateEnum, IState<T>> _states = new Dictionary<StateEnum, IState<T>>();

    /// <summary>
    /// 记录下当前帧是否有改变的状态
    /// </summary>
    private bool _isChangeState;
    
    public override void PhysicsProcess(float delta)
    {
        _isChangeState = false;
        if (CurrState != null)
        {
            CurrState.PhysicsProcess(delta);
            //判断当前帧是否有改变的状态, 如果有, 则重新调用 PhysicsProcess() 方法
            if (_isChangeState)
            {
                PhysicsProcess(delta);
            }
        }
    }

    /// <summary>
    /// 往状态机力注册一个新的状态
    /// </summary>
    public void Register(IState<T> state)
    {
        if (GetStateInstance(state.StateType) != null)
        {
            GD.PrintErr("当前状态已经在状态机中注册:", state);
            return;
        }
        state.Master = ActivityObject as T;
        state.StateController = this;
        _states.Add(state.StateType, state);
    }

    /// <summary>
    /// 立即切换到下一个指定状态, 并且这一帧会被调用 PhysicsProcess
    /// </summary>
    public void ChangeState(StateEnum next, params object[] arg)
    {
        _changeState(false, next, arg);
    }

    /// <summary>
    /// 切换到下一个指定状态, 下一帧才会调用 PhysicsProcess
    /// </summary>
    public void ChangeStateLate(StateEnum next, params object[] arg)
    {
        _changeState(true, next, arg);
    }

    /// <summary>
    /// 根据状态类型获取相应的状态对象
    /// </summary>
    private IState<T> GetStateInstance(StateEnum stateType)
    {
        _states.TryGetValue(stateType, out var v);
        return v;
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    private void _changeState(bool late, StateEnum next, params object[] arg)
    {
        if (_currState != null && _currState.StateType == next)
        {
            return;
        }
        var newState = GetStateInstance(next);
        if (newState == null)
        {
            GD.PrintErr("当前状态机未找到相应状态:" + next);
            return;
        }
        if (_currState == null)
        {
            _currState = newState;
            newState.Enter(StateEnum.None, arg);
        }
        else if (_currState.CanChangeState(next))
        {
            _isChangeState = !late;
            var prev = _currState.StateType;
            _currState.Exit(next);
            GD.Print("nextState => " + next);
            _currState = newState;
            _currState.Enter(prev, arg);
        }
    }
}