
/// <summary>
/// AI 发现玩家
/// </summary>
public class AITailAfterState : IState<Enemy, AIStateEnum>
{
    public AIStateEnum StateType { get; } = AIStateEnum.AITailAfter;
    public Enemy Master { get; set; }
    public StateController<Enemy, AIStateEnum> StateController { get; set; }
    public void Enter(AIStateEnum prev, params object[] args)
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

    public bool CanChangeState(AIStateEnum next)
    {
        return true;
    }

    public void Exit(AIStateEnum next)
    {
        
    }
}