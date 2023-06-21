
using Godot;

/// <summary>
/// 距离目标足够近, 在目标附近随机移动, 并开火
/// </summary>
public class AiSurroundState : StateBase<Enemy, AiStateEnum>
{
    /// <summary>
    /// 目标是否在视野内
    /// </summary>
    public bool IsInView = true;

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

    public AiSurroundState() : base(AiStateEnum.AiSurround)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        IsInView = true;
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
                ChangeState(AiStateEnum.AiFindAmmo, targetWeapon);
                return;
            }
        }

        var playerPos = Player.Current.GetCenterPosition();
        var weapon = Master.Holster.ActiveWeapon;

        //枪口指向玩家
        Master.LookTargetPosition(playerPos);

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
                if (_lockTimer >= 1) //卡在一个点超过一秒
                {
                    RunOver(playerPos);
                    _isMoveOver = false;
                    _lockTimer = 0;
                }
                else if (Master.NavigationAgent2D.IsNavigationFinished()) //到达终点
                {
                    _pauseTimer = Utils.RandomRangeFloat(0f, 0.5f);
                    _isMoveOver = true;
                    _moveFlag = false;
                    Master.BasisVelocity = Vector2.Zero;
                }
                else if (!_moveFlag)
                {
                    _moveFlag = true;
                    //计算移动
                    var pos = Master.GlobalPosition;
                    var nextPos = Master.NavigationAgent2D.GetNextPathPosition();
                    Master.AnimatedSprite.Play(AnimatorNames.Run);
                    Master.BasisVelocity = (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() *
                                           Master.MoveSpeed;
                }
                else
                {
                    var pos = Master.GlobalPosition;
                    var lastSlideCollision = Master.GetLastSlideCollision();
                    if (lastSlideCollision != null && lastSlideCollision.GetCollider() is Role) //碰到其他角色
                    {
                        _pauseTimer = Utils.RandomRangeFloat(0f, 0.3f);
                        _isMoveOver = true;
                        _moveFlag = false;
                        Master.BasisVelocity = Vector2.Zero;
                    }
                    else
                    {
                        //计算移动
                        var nextPos = Master.NavigationAgent2D.GetNextPathPosition();
                        Master.AnimatedSprite.Play(AnimatorNames.Run);
                        Master.BasisVelocity = (nextPos - pos - Master.NavigationPoint.Position).Normalized() *
                                               Master.MoveSpeed;
                    }
                    
                    if (_prevPos.DistanceSquaredTo(pos) <= 0.01f)
                    {
                        _lockTimer += delta;
                    }
                    else
                    {
                        _prevPos = pos;
                    }
                }

                if (weapon != null)
                {
                    var position = Master.GlobalPosition;
                    if (position.DistanceSquaredTo(playerPos) > Mathf.Pow(Master.GetWeaponRange(0.7f), 2)) //玩家离开正常射击范围
                    {
                        ChangeState(AiStateEnum.AiFollowUp);
                    }
                    else
                    {
                        //发起攻击
                        Master.EnemyAttack(delta);
                    }
                }
            }
        }
        else //目标离开视野
        {
            ChangeState(AiStateEnum.AiTailAfter);
        }
    }

    private void RunOver(Vector2 targetPos)
    {
        var weapon = Master.Holster.ActiveWeapon;
        var distance = (int)(weapon == null ? 150 : (weapon.Attribute.BulletMinDistance * 0.7f));
        _nextPosition = new Vector2(
            targetPos.X + Utils.RandomRangeInt(-distance, distance),
            targetPos.Y + Utils.RandomRangeInt(-distance, distance)
        );
        Master.NavigationAgent2D.TargetPosition = _nextPosition;
    }

    public override void DebugDraw()
    {
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(_nextPosition), Colors.White);
    }
}