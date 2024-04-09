
using System;
using Godot;

namespace AiState;

/// <summary>
/// 目标在视野内, 跟进目标, 如果距离在子弹有效射程内, 则开火
/// </summary>
public class AiFollowUpState : StateBase<AiRole, AIStateEnum>
{
    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 0.3f;

    public AiFollowUpState() : base(AIStateEnum.AiFollowUp)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        if (Master.LookTarget == null)
        {
            ChangeState(AIStateEnum.AiNormal);
            return;
            //throw new Exception("进入 AIAdvancedStateEnum.AiFollowUp 状态时角色没有攻击目标!");
        }
        
        _navigationUpdateTimer = 0;
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
                ChangeState(AIStateEnum.AiFindAmmo, targetWeapon);
                return;
            }
            else
            {
                //切换到随机移动状态
                ChangeState(AIStateEnum.AiSurround);
            }
        }

        var playerPos = Master.LookTarget.GetCenterPosition();

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
        else
        {
            inAttackRange = distanceSquared <= Mathf.Pow(Master.ViewRange * 0.7f, 2);
        }
        
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

        //在视野中
        if (Master.TargetInView)
        {
            //更新标记位置
            Master.UpdateMarkTargetPosition();
            if (inAttackRange) //在攻击范围内
            {
                if (weapon != null)
                {
                    //距离够近, 可以切换到环绕模式
                    if (distanceSquared <= Mathf.Pow(Utils.GetConfigRangeStart(weapon.Attribute.Bullet.DistanceRange) * 0.7f, 2))
                    {
                        ChangeState(AIStateEnum.AiSurround);
                    }
                    else if (!Master.IsAttack && weapon.TriggerIsReady()) //可以攻击
                    {
                        //攻击状态
                        ChangeState(AIStateEnum.AiAttack);
                    }
                }
                else
                {
                    //距离够近, 可以切换到环绕模式
                    if (distanceSquared <= Mathf.Pow(Master.ViewRange * 0.7f, 2))
                    {
                        ChangeState(AIStateEnum.AiSurround);
                    }
                    else if (!Master.IsAttack && Master.NoWeaponAttack) //可以在没有武器时发起攻击
                    {
                        //攻击状态
                        ChangeState(AIStateEnum.AiAttack);
                    }
                }
            }
        }
        else //不在视野中
        {
            ChangeState(AIStateEnum.AiTailAfter);
        }
    }

    public override void DebugDraw()
    {
        var playerPos = Master.LookTarget.GetCenterPosition();
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(playerPos), Colors.Red);
    }
}