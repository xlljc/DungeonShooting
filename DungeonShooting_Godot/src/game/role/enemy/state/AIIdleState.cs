
/// <summary>
/// AI 静止行为
/// </summary>
public class AIIdleState : IState<Role>
{
    public StateEnum StateType { get; } = StateEnum.Idle;
    public Role Master { get; set; }
    public StateController<Role> StateController { get; set; }
    public void Enter(StateEnum prev, params object[] args)
    {
        
    }

    public void PhysicsProcess(float delta)
    {
        
    }

    public bool CanChangeState(StateEnum next)
    {
        return true;
    }

    public void Exit(StateEnum next)
    {
        
    }
}
