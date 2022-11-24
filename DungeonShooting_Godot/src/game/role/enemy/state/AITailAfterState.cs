
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
        //临时处理
        //Master.PathSign.Enable = true;
    }

    public void PhysicsProcess(float delta)
    {
        var master = Master;
        
    }

    public bool CanChangeState(AIStateEnum next)
    {
        return true;
    }

    public void Exit(AIStateEnum next)
    {
        
    }
}