
using Godot;

/// <summary>
/// 目标在视野内, 跟进目标, 如果距离在子弹有效射程内, 则开火
/// </summary>
public class AiFollowUpState : StateBase<Enemy, AiStateEnum>
{

    /// <summary>
    /// 目标是否在视野内
    /// </summary>
    public bool IsInView;

    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;

    public AiFollowUpState() : base(AiStateEnum.AiFollowUp)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        _navigationUpdateTimer = 0;
        IsInView = true;
    }

    public override void Process(float delta)
    {
        //先检查弹药是否打光
        if (Master.IsAllWeaponTotalAmmoEmpty())
        {
            //再寻找是否有可用的武器
            var targetWeapon = Master.FindTargetWeapon();
            if (targetWeapon != null)
            {
                ChangeState(AiStateEnum.AiFindAmmo, targetWeapon);
                return;
            }
            else
            {
                //切换到随机移动状态
                ChangeState(AiStateEnum.AiSurround);
            }
        }

        var playerPos = Player.Current.GetCenterPosition();

        //更新玩家位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            Master.NavigationAgent2D.TargetPosition = playerPos;
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }

        var masterPosition = Master.GlobalPosition;

        //是否在攻击范围内
        var inAttackRange = false;

        var weapon = Master.Holster.ActiveItem;
        if (weapon != null)
        {
            inAttackRange = masterPosition.DistanceSquaredTo(playerPos) <= Mathf.Pow(Master.GetWeaponRange(0.7f), 2);
        }

        //枪口指向玩家
        Master.LookTargetPosition(playerPos);
        
        if (!Master.NavigationAgent2D.IsNavigationFinished())
        {
            //计算移动
            var nextPos = Master.NavigationAgent2D.GetNextPathPosition();
            Master.AnimatedSprite.Play(AnimatorNames.Run);
            Master.BasisVelocity = (nextPos - masterPosition - Master.NavigationPoint.Position).Normalized() *
                              Master.RoleState.MoveSpeed;
        }
        else
        {
            Master.BasisVelocity = Vector2.Zero;
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
            if (inAttackRange) //在攻击范围内
            {
                //发起攻击
                Master.EnemyAttack(delta);
                
                //距离够近, 可以切换到环绕模式
                if (Master.GlobalPosition.DistanceSquaredTo(playerPos) <= Mathf.Pow(weapon.Attribute.BulletMinDistance, 2) * 0.7f)
                {
                    ChangeState(AiStateEnum.AiSurround);
                }
            }
        }
        else
        {
            ChangeState(AiStateEnum.AiTailAfter);
        }
    }

    public override void DebugDraw()
    {
        var playerPos = Player.Current.GetCenterPosition();
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Red);
    }
}