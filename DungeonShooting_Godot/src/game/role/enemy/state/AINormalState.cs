
using Godot;

/// <summary>
/// AI 正常状态
/// </summary>
public class AINormalState : IState<Enemy, AIStateEnum>
{
    public AIStateEnum StateType { get; } = AIStateEnum.AINormal;
    public Enemy Master { get; set; }
    public StateController<Enemy, AIStateEnum> StateController { get; set; }
    public void Enter(AIStateEnum prev, params object[] args)
    {
        
    }

    public void PhysicsProcess(float delta)
    {
        //检测玩家
        var player = GameApplication.Instance.Room.Player;
        //玩家中心点坐标
        var playerPos = player.MountPoint.GlobalPosition;

        if (Master.IsInViewRange(playerPos) && Master.TestViewRayCast(playerPos) == null)
        {
            //发现玩家
            StateController.ChangeStateLate(AIStateEnum.AITailAfter);
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
