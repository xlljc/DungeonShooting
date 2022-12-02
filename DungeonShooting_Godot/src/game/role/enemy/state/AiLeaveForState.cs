
using Godot;

/// <summary>
/// 前往目标位置
/// </summary>
public class AiLeaveForState : StateBase<Enemy, AiStateEnum>
{
    private Vector2 _targetPosition;

    public AiLeaveForState() : base(AiStateEnum.AiLeaveFor)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        if (args.Length > 0 && args[0] is Vector2 pos)
        {
            UpdateTargetPosition(pos);
        }
        else
        {
            ChangeState(AiStateEnum.AiNormal);
        }
    }

    public override void PhysicsProcess(float delta)
    {
        //计算移动
        var nextPos = Master.NavigationAgent2D.GetNextLocation();
        Master.LookTargetPosition(_targetPosition);
        Master.AnimatedSprite.Animation = AnimatorNames.Run;
        Master.Velocity = (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() *
                          Master.MoveSpeed;
        Master.CalcMove(delta);

        var playerPos = GameApplication.Instance.Room.Player.GetCenterPosition();
        //检测玩家是否在视野内, 如果在, 则切换到 AiTargetInView 状态
        if (Master.IsInTailAfterViewRange(playerPos))
        {
            if (!Master.TestViewRayCast(playerPos)) //看到玩家
            {
                //关闭射线检测
                Master.TestViewRayCastOver();
                //切换成发现目标状态
                ChangeStateLate(AiStateEnum.AiTargetInView);
                //派发发现玩家事件
                EventManager.EmitEvent(EventEnum.OnEnemyFindPlayer);
                return;
            }
            else
            {
                //关闭射线检测
                Master.TestViewRayCastOver();
            }
        }

        //移动到目标掉了, 还没发现目标
        if (Master.NavigationAgent2D.IsNavigationFinished())
        {
            ChangeStateLate(AiStateEnum.AiNormal);
        }
    }

    public void UpdateTargetPosition(Vector2 pos)
    {
        _targetPosition = pos;
        Master.NavigationAgent2D.SetTargetLocation(pos);
    }
}
