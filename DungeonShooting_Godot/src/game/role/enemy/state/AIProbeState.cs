
/// <summary>
/// Ai 不确定玩家位置
/// </summary>
public class AIProbeState : IState<Enemy, AIStateEnum>
{
    public AIStateEnum StateType { get; } = AIStateEnum.AIProbe;
    public Enemy Master { get; set; }
    public StateController<Enemy, AIStateEnum> StateController { get; set; }
    public void Enter(AIStateEnum prev, params object[] args)
    {
        
    }

    public void PhysicsProcess(float delta)
    {

    }

    public bool CanChangeState(AIStateEnum next)
    {
        return true;
    }

    public void Exit(AIStateEnum next)
    {
        
    }
}