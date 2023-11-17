
using System;
using Godot;

namespace AdvancedState;

/// <summary>
/// 距离目标足够近, 在目标附近随机移动, 并开火
/// </summary>
public class AiSurroundState : StateBase<AdvancedEnemy, AIAdvancedStateEnum>
{
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

    public AiSurroundState() : base(AIAdvancedStateEnum.AiSurround)
    {
    }

    public override void Enter(AIAdvancedStateEnum prev, params object[] args)
    {
        if (Master.LookTarget == null)
        {
            throw new Exception("进入 AIAdvancedStateEnum.AiSurround 状态时角色没有攻击目标!");
        }
        
        Master.TargetInView = true;
        _isMoveOver = true;
        _pauseTimer = 0;
        _moveFlag = false;
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
        }

        var playerPos = Master.LookTarget.GetCenterPosition();
        var weapon = Master.WeaponPack.ActiveItem;

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
            //更新标记位置
            Master.UpdateMarkTargetPosition();
            
            if (_pauseTimer >= 0)
            {
                Master.AnimatedSprite.Play(AnimatorNames.Idle);
                _pauseTimer -= delta;
            }
            else if (_isMoveOver) //移动已经完成
            {
                RunOver(playerPos);
                _isMoveOver = false;
            }
            else
            {
                var masterPosition = Master.Position;
                if (_lockTimer >= 1) //卡在一个点超过一秒
                {
                    RunOver(playerPos);
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
                    if (lastSlideCollision != null && lastSlideCollision.GetCollider() is AdvancedRole) //碰到其他角色
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

                if (weapon != null)
                {
                    if (masterPosition.DistanceSquaredTo(playerPos) > Mathf.Pow(Master.GetWeaponRange(0.7f), 2)) //玩家离开正常射击范围
                    {
                        ChangeState(AIAdvancedStateEnum.AiFollowUp);
                    }
                    else if (weapon.TriggerIsReady()) //可以攻击
                    {
                        //发起攻击
                        ChangeState(AIAdvancedStateEnum.AiAttack);
                    }
                }
            }
        }
        else //目标离开视野
        {
            ChangeState(AIAdvancedStateEnum.AiTailAfter);
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

    public override void DebugDraw()
    {
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(_nextPosition), Colors.White);
    }
}