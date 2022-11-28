
using Godot;

/// <summary>
/// AI 正常状态
/// </summary>
public class AiNormalState : StateBase<Enemy, AIStateEnum>
{
    public AiNormalState() : base(AIStateEnum.AINormal)
    {
    }
    
    public override void PhysicsProcess(float delta)
    {
        //检测玩家
        var player = GameApplication.Instance.Room.Player;
        //玩家中心点坐标
        var playerPos = player.MountPoint.GlobalPosition;

        if (Master.IsInViewRange(playerPos) && Master.TestViewRayCast(playerPos) == null)
        {
            //发现玩家
            ChangeStateLate(AIStateEnum.AITailAfter);
        }
    }
}
