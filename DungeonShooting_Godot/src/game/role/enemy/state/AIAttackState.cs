
using Godot;

/// <summary>
/// 目标在视野范围内, 发起攻击
/// </summary>
public class AIAttackState : StateBase<Enemy, AIStateEnum>
{
        
    /// <summary>
    /// 是否在视野内
    /// </summary>
    public bool IsInView;
    
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;
    
    public AIAttackState() : base(AIStateEnum.AIAttack)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        _navigationUpdateTimer = 0;
        IsInView = true;
    }

    public override void PhysicsProcess(float delta)
    {
        var masterPos = Master.GlobalPosition;
        var playerPos = GameApplication.Instance.Room.Player.GlobalPosition;
        
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
        
        //计算移动
        var nextPos = Master.NavigationAgent2D.GetNextLocation();
        Master.LookTargetPosition(playerPos);
        Master.AnimatedSprite.Animation = AnimatorNames.Run;
        Master.Velocity = (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() * Master.MoveSpeed;
        Master.CalcMove(delta);
        
        //检测玩家是否在视野内
        if (masterPos.DistanceSquaredTo(playerPos) <= Master.TailAfterViewRange * Master.TailAfterViewRange)
        {
            IsInView = !Master.TestViewRayCast(playerPos);
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
            ChangeStateLate(AIStateEnum.AITailAfter);
        }
    }

    public override void DebugDraw()
    {
        var playerPos = GameApplication.Instance.Room.Player.GlobalPosition;
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Red);
    }
}