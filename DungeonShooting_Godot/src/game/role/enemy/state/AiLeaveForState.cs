
using Godot;

/// <summary>
/// 前往目标位置
/// </summary>
public class AiLeaveForState : StateBase<Enemy, AiStateEnum>
{
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;
    
    public AiLeaveForState() : base(AiStateEnum.AiLeaveFor)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        if (Enemy.IsFindTarget)
        {
            Master.NavigationAgent2D.SetTargetLocation(Enemy.FindTargetPosition);
        }
        else
        {
            ChangeStateLate(prev);
        }
    }

    public override void PhysicsProcess(float delta)
    {
        //更新玩家位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            if (Master.NavigationAgent2D.GetTargetLocation() != Enemy.FindTargetPosition)
            {
                Master.NavigationAgent2D.SetTargetLocation(Enemy.FindTargetPosition);
            }
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }

        if (!Master.NavigationAgent2D.IsNavigationFinished())
        {
            //计算移动
            var nextPos = Master.NavigationAgent2D.GetNextLocation();
            Master.LookTargetPosition(Enemy.FindTargetPosition);
            Master.AnimatedSprite.Animation = AnimatorNames.Run;
            Master.Velocity = (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() *
                              Master.MoveSpeed;
            Master.CalcMove(delta);
        }

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

    public override void DebugDraw()
    {
        Master.DrawLine(Vector2.Zero, Master.ToLocal(Master.NavigationAgent2D.GetTargetLocation()), Colors.Yellow);
    }
}
