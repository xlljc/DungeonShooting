
using Godot;

namespace AdvancedState;

/// <summary>
/// 目标在视野内, 跟进目标, 如果距离在子弹有效射程内, 则开火
/// </summary>
public class AiFollowUpState : StateBase<AdvancedEnemy, AIAdvancedStateEnum>
{
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;

    public AiFollowUpState() : base(AIAdvancedStateEnum.AiFollowUp)
    {
    }

    public override void Enter(AIAdvancedStateEnum prev, params object[] args)
    {
        _navigationUpdateTimer = 0;
        Master.TargetInView = true;
    }

    public override void Exit(AIAdvancedStateEnum next)
    {
        Master.LookTarget = null;
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
                ChangeState(AIAdvancedStateEnum.AiFindAmmo, targetWeapon);
                return;
            }
            else
            {
                //切换到随机移动状态
                ChangeState(AIAdvancedStateEnum.AiSurround);
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

        //是否在攻击范围内
        var inAttackRange = false;

        var weapon = Master.WeaponPack.ActiveItem;
        var distanceSquared = Master.Position.DistanceSquaredTo(playerPos);
        if (weapon != null)
        {
            inAttackRange = distanceSquared <= Mathf.Pow(Master.GetWeaponRange(0.7f), 2);
        }

        //枪口指向玩家
        Master.LookTarget = Player.Current;
        
        if (!Master.NavigationAgent2D.IsNavigationFinished())
        {
            //移动
            Master.DoMove();
        }
        else
        {
            //站立
            Master.DoIdle();
        }

        //检测玩家是否在视野内
        if (Master.IsInTailAfterViewRange(playerPos))
        {
            Master.TargetInView = !Master.TestViewRayCast(playerPos);
            //关闭射线检测
            Master.TestViewRayCastOver();
        }
        else
        {
            Master.TargetInView = false;
        }

        //在视野中
        if (Master.TargetInView)
        {
            if (inAttackRange) //在攻击范围内
            {
                //距离够近, 可以切换到环绕模式
                if (distanceSquared <= Mathf.Pow(Utils.GetConfigRangeStart(weapon.Attribute.Bullet.DistanceRange), 2) * 0.7f)
                {
                    ChangeState(AIAdvancedStateEnum.AiSurround);
                }
                else if (weapon.TriggerIsReady()) //可以攻击
                {
                    //攻击状态
                    ChangeState(AIAdvancedStateEnum.AiAttack);
                }
            }
        }
        else //不在视野中
        {
            ChangeState(AIAdvancedStateEnum.AiTailAfter);
        }
    }

    public override void DebugDraw()
    {
        var playerPos = Player.Current.GetCenterPosition();
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Red);
    }
}