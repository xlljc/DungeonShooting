using Godot;

namespace AdvancedState;

/// <summary>
/// ai 攻击状态
/// </summary>
public class AiAttackState : StateBase<AdvancedEnemy, AIAdvancedStateEnum>
{
    /// <summary>
    /// 上一个状态
    /// </summary>
    public AIAdvancedStateEnum PrevState;

    /// <summary>
    /// 攻击状态
    /// </summary>
    public AiAttackEnum AttackState;
    
    public AiAttackState() : base(AIAdvancedStateEnum.AiAttack)
    {
    }

    public override void Enter(AIAdvancedStateEnum prev, params object[] args)
    {
        var weapon = Master.WeaponPack.ActiveItem;
        if (weapon == null)
        {
            Debug.LogError("进入 AIAdvancedStateEnum.AiAttack 状态时角色没有武器!");
            ChangeState(prev);
            return;
        }

        if (!weapon.TriggerIsReady())
        {
            Debug.LogError("进入 AIAdvancedStateEnum.AiAttack 状态时角色武器还玩法触动扳机!");
            ChangeState(prev);
            return;
        }
        
        Master.BasisVelocity = Vector2.Zero;
        AttackState = AiAttackEnum.None;
        Master.LockTargetTime = 0;
        PrevState = prev;
    }

    public override void Exit(AIAdvancedStateEnum next)
    {
        Master.MountLookTarget = true;
        Master.LockTargetTime = 0;
        Master.LookTarget = null;
    }

    public override void Process(float delta)
    {
        var weapon = Master.WeaponPack.ActiveItem;
        if (weapon == null || (weapon.GetAttackTimer() > 0 && weapon.GetContinuousCount() <= 0)) //攻击完成, 可以切换状态了
        {
            //这里要做换弹判断, 还有上膛判断
            
            ChangeState(PrevState);
        }
        else //攻击状态
        {
            //这里还要做是否在视野内的判断
            
            Master.LookTarget = Player.Current;
            //触发扳机
            AttackState = Master.WeaponPack.ActiveItem.AiTriggerAttackState();
            
            if (AttackState == AiAttackEnum.LockingTime) //锁定玩家状态
            {
                Master.LockTargetTime += delta;
                
                var aiLockRemainderTime = Master.GetLockRemainderTime();
                Master.MountLookTarget = aiLockRemainderTime >= weapon.Attribute.AiAttackAttr.LockAngleTime;
                //更新瞄准辅助线
                if (weapon.Attribute.AiAttackAttr.ShowSubline)
                {
                    if (Master.SubLine == null)
                    {
                        Master.InitSubLine();
                    }
                    else
                    {
                        Master.SubLine.Enable = true;
                    }

                    //播放警告删掉动画
                    if (!Master.SubLine.IsPlayWarnAnimation && aiLockRemainderTime <= 0.5f)
                    {
                        Master.SubLine.PlayWarnAnimation(0.5f);
                    }
                }
                
                if (weapon.Attribute.AiAttackAttr.LockingStand) //锁定目标时站立不动
                {
                    Master.DoIdle();
                }
                else //正常移动
                {
                    Master.DoMove();
                }
            }
            else
            {
                Master.LockTargetTime = 0;
                //关闭辅助线
                if (Master.SubLine != null)
                {
                    Master.SubLine.Enable = false;
                }

                if (AttackState == AiAttackEnum.Attack || AttackState == AiAttackEnum.AttackInterval)
                {
                    if (weapon.Attribute.AiAttackAttr.AttackLockAngle) //开火时锁定枪口角度
                    {
                        //连发状态锁定角度
                        Master.MountLookTarget = !(weapon.GetContinuousCount() > 0 || weapon.GetAttackTimer() > 0);
                    }
                    else
                    {
                        Master.MountLookTarget = true;
                    }
                }
                else
                {
                    Master.MountLookTarget = true;
                }
                
                if (AttackState == AiAttackEnum.Attack && weapon.Attribute.AiAttackAttr.FiringStand) //开火时站立不动
                {
                    Master.DoIdle();
                }
                else //正常移动
                {
                    Master.DoMove();
                }
            }
        }
    }
}