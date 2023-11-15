
using Godot;

namespace NnormalState;

/// <summary>
/// 距离目标足够近, 在目标附近随机移动, 并开火
/// </summary>
public class AiSurroundState : StateBase<Enemy, AINormalStateEnum>
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

    public AiSurroundState() : base(AINormalStateEnum.AiSurround)
    {
    }

    public override void Enter(AINormalStateEnum prev, params object[] args)
    {
        Master.TargetInView = true;
        _isMoveOver = true;
        _pauseTimer = 0;
        _moveFlag = false;
    }

    public override void Process(float delta)
    {
        var playerPos = Player.Current.GetCenterPosition();

        //枪口指向玩家
        Master.LookTargetPosition(playerPos);

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
                    Master.DoIdle();
                }
                else if (!_moveFlag)
                {
                    _moveFlag = true;
                    //计算移动
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
                        Master.DoIdle();
                    }
                    else
                    {
                        //计算移动
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
                
                if (masterPosition.DistanceSquaredTo(playerPos) > Mathf.Pow(Master.GetAttackRange() * 0.7f, 2)) //玩家离开正常射击范围
                {
                    ChangeState(AINormalStateEnum.AiFollowUp);
                }
                else if (Master.GetAttackTimer() <= 0) //发起攻击
                {
                    ChangeState(AINormalStateEnum.AiAttack);
                }
            }
        }
        else //目标离开视野
        {
            ChangeState(AINormalStateEnum.AiTailAfter);
        }
    }

    private void RunOver(Vector2 targetPos)
    {
        var distance = (int)(Master.GetAttackRange() * 0.7f);
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