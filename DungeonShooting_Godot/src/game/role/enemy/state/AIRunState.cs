
/// <summary>
/// AI 奔跑行为
/// </summary>
public class AIRunState : IState<Enemy>
{
    public StateEnum StateType { get; } = StateEnum.Run;
    public Enemy Master { get; set; }
    public StateController<Enemy> StateController { get; set; }
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