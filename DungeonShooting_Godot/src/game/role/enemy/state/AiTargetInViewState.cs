
using Godot;

/// <summary>
/// 目标在视野范围内, 发起攻击
/// </summary>
public class AiTargetInViewState : StateBase<Enemy, AiStateEnum>
{
        
    /// <summary>
    /// 是否在视野内
    /// </summary>
    public bool IsInView;
    
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;
    
    public AiTargetInViewState() : base(AiStateEnum.AiTargetInView)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        _navigationUpdateTimer = 0;
        IsInView = true;
    }

    public override void PhysicsProcess(float delta)
    {
        var playerPos = GameApplication.Instance.Room.Player.GetCenterPosition();
        
        //更新玩家位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            if (Master.NavigationAgent2D.GetTargetLocation() != playerPos)
            {
                Master.NavigationAgent2D.SetTargetLocation(playerPos);
            }
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }

        var masterPosition = Master.GlobalPosition;
        var canMove = false;

        var weapon = Master.Holster.ActiveWeapon;
        if (weapon != null)
        {
            canMove = masterPosition.DistanceSquaredTo(playerPos) >= 50 * 50;
        }
        
        if (canMove)
        {
            if (!Master.NavigationAgent2D.IsNavigationFinished())
            {
                //计算移动
                var nextPos = Master.NavigationAgent2D.GetNextLocation();
                Master.LookTargetPosition(playerPos);
                Master.AnimatedSprite.Animation = AnimatorNames.Run;
                Master.Velocity = (nextPos - masterPosition - Master.NavigationPoint.Position).Normalized() * Master.MoveSpeed;
                Master.CalcMove(delta);
            }
        }

        //检测玩家是否在视野内
        if (Master.IsInTailAfterViewRange(playerPos))
        {
            IsInView = !Master.TestViewRayCast(playerPos);
            //关闭射线检测
            Master.TestViewRayCastOver();
        }
        else
        {
            IsInView = false;
        }

        if (IsInView)
        {
            Master.EnemyAttack();
        }
        else
        {
            ChangeStateLate(AiStateEnum.AiTailAfter);
        }
    }

    public override void DebugDraw()
    {
        var playerPos = GameApplication.Instance.Room.Player.GetCenterPosition();
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Red);
    }
}