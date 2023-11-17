using System;
using Godot;

namespace EnemyState;

/// <summary>
/// ai 攻击状态
/// </summary>
public class AiAttackState : StateBase<Enemy, AIStateEnum>
{
    /// <summary>
    /// 上一个状态
    /// </summary>
    public AIStateEnum PrevState;

    /// <summary>
    /// 攻击状态
    /// </summary>
    public AiAttackEnum AttackState;

    //是否移动结束
    private bool _isMoveOver;

    //移动停顿计时器
    private float _pauseTimer;
    private bool _moveFlag;
    
    //下一个移动点
    private Vector2 _nextPosition;
    
    //上一帧位置
    private Vector2 _prevPos;
    //卡在一个位置的时间
    private float _lockTimer;
    
    public AiAttackState() : base(AIStateEnum.AiAttack)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        if (Master.LookTarget == null)
        {
            throw new Exception("进入 AIAdvancedStateEnum.AiAttack 状态时角色没有攻击目标!");
        }
        
        var weapon = Master.WeaponPack.ActiveItem;
        if (weapon == null)
        {
            throw new Exception("进入 AIAdvancedStateEnum.AiAttack 状态时角色没有武器!");

        }

        if (!weapon.TriggerIsReady())
        {
            throw new Exception("进入 AIAdvancedStateEnum.AiAttack 状态时角色武器还玩法触动扳机!");
        }
        
        Master.BasisVelocity = Vector2.Zero;
        AttackState = AiAttackEnum.None;
        Master.LockTargetTime = 0;
        PrevState = prev;
        
        _isMoveOver = true;
        _pauseTimer = 0;
        _moveFlag = false;
    }

    public override void Exit(AIStateEnum next)
    {
        Master.MountLookTarget = true;
        Master.LockTargetTime = 0;
    }

    public override void Process(float delta)
    {
        //更新标记位置
        Master.UpdateMarkTargetPosition();
        
        var weapon = Master.WeaponPack.ActiveItem;
        if (weapon == null)
        {
            //攻击结束
            ChangeState(PrevState);
        }
        else if (AttackState == AiAttackEnum.AttackInterval) //攻击完成
        {
            if (weapon.GetAttackTimer() <= 0) //攻击冷却完成
            {
                Master.MountLookTarget = true;
                //这里要做换弹判断, 还有上膛判断
                if (weapon.CurrAmmo <= 0) //换弹判断
                {
                    if (!weapon.Reloading)
                    {
                        weapon.Reload();
                    }
                }
                else if (weapon.GetBeLoadedStateState() != 2) //上膛
                {
                    if (weapon.GetBeLoadedStateState() == 0)
                    {
                        weapon.BeLoaded();
                    }
                }
                else
                {
                    //攻击结束
                    ChangeState(PrevState);
                }
            }
            MoveHandler(delta);
        }
        else //攻击状态
        {
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
                    MoveHandler(delta);
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
                    MoveHandler(delta);
                }
            }
        }
    }

    private void MoveHandler(float delta)
    {

        if (_pauseTimer >= 0)
        {
            Master.AnimatedSprite.Play(AnimatorNames.Idle);
            _pauseTimer -= delta;
        }
        else if (_isMoveOver) //移动已经完成
        {
            RunOver(Master.LookTarget.Position);
            _isMoveOver = false;
        }
        else
        {
            var masterPosition = Master.Position;
            if (_lockTimer >= 1) //卡在一个点超过一秒
            {
                RunOver(Master.LookTarget.Position);
                _isMoveOver = false;
                _lockTimer = 0;
            }
            else if (Master.NavigationAgent2D.IsNavigationFinished()) //到达终点
            {
                _pauseTimer = Utils.Random.RandomRangeFloat(0f, 0.5f);
                _isMoveOver = true;
                _moveFlag = false;
                //站立
                Master.DoIdle();
            }
            else if (!_moveFlag)
            {
                _moveFlag = true;
                //移动
                Master.DoMove();
            }
            else
            {
                var lastSlideCollision = Master.GetLastSlideCollision();
                if (lastSlideCollision != null && lastSlideCollision.GetCollider() is Role) //碰到其他角色
                {
                    _pauseTimer = Utils.Random.RandomRangeFloat(0f, 0.3f);
                    _isMoveOver = true;
                    _moveFlag = false;
                    //站立
                    Master.DoIdle();
                }
                else
                {
                    //移动
                    Master.DoMove();
                }

                if (_prevPos.DistanceSquaredTo(masterPosition) <= 1 * delta)
                {
                    _lockTimer += delta;
                }
                else
                {
                    _lockTimer = 0;
                    _prevPos = masterPosition;
                }
            }
        }
    }

    private void RunOver(Vector2 targetPos)
    {
        var weapon = Master.WeaponPack.ActiveItem;
        var distance = (int)(weapon == null ? 150 : (Utils.GetConfigRangeStart(weapon.Attribute.Bullet.DistanceRange) * 0.7f));
        _nextPosition = new Vector2(
            targetPos.X + Utils.Random.RandomRangeInt(-distance, distance),
            targetPos.Y + Utils.Random.RandomRangeInt(-distance, distance)
        );
        Master.NavigationAgent2D.TargetPosition = _nextPosition;
    }    
    
}