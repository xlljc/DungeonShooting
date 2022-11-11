
/// <summary>
/// AI 奔跑行为
/// </summary>
public class AIRunState : IState<Role>
{
    public StateEnum StateType { get; } = StateEnum.Run;
    public Role Master { get; set; }
    public StateController<Role> StateController { get; set; }
    public void Enter(StateEnum prev, params object[] args)
    {
        
    }

    public void PhysicsProcess(float delta)
    {
        var master = Master;
        if (master.LookTarget != null)
        {
            master.AnimatedSprite.Animation = AnimatorNames.ReverseRun;
            master.Velocity = (master.LookTarget.GlobalPosition - master.GlobalPosition).Normalized() * master.MoveSpeed;
            master.CalcMove(delta);
        }
    }

    public bool CanChangeState(StateEnum next)
    {
        return true;
    }

    public void Exit(StateEnum next)
    {
        
    }
}