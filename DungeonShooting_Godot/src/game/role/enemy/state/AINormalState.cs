
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
        Master.PathSign.Enable = false;
    }

    public void PhysicsProcess(float delta)
    {
        //检测玩家
        var player = GameApplication.Instance.Room.Player;
        //玩家中心点坐标
        var playerPos = player.MountPoint.GlobalPosition;
        
        //玩家是否在前方
        var isForward = Master.IsPositionInForward(playerPos);
        
        if (isForward) //脸朝向玩家
        {
            if (Master.GlobalPosition.DistanceSquaredTo(playerPos) <= Master.ViewRange * Master.ViewRange) //没有超出视野半径
            {
                //射线检测墙体
                Master.ViewRay.Enabled = true;
                var localPos = Master.ViewRay.ToLocal(playerPos);
                Master.ViewRay.CastTo = localPos;
                Master.ViewRay.ForceRaycastUpdate();
            
                if (!Master.ViewRay.IsColliding()) //视野无阻
                {
                    //发现玩家, 切换状态
                    StateController.ChangeStateLate(AIStateEnum.AITailAfter);
                }

                Master.ViewRay.Enabled = false;
            }
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
