using Godot;

namespace NnormalState;

/// <summary>
/// AI 正常状态
/// </summary>
public class AiNormalState : StateBase<Enemy, AINormalStateEnum>
{
    //是否发现玩家
    private bool _isFindPlayer;

    //下一个运动的坐标
    private Vector2 _nextPos;

    //是否移动结束
    private bool _isMoveOver;

    //上一次移动是否撞墙
    private bool _againstWall;
    
    //撞墙法线角度
    private float _againstWallNormalAngle;

    //移动停顿计时器
    private float _pauseTimer;
    private bool _moveFlag;

    //上一帧位置
    private Vector2 _prevPos;
    //卡在一个位置的时间
    private float _lockTimer;
    
    public AiNormalState() : base(AINormalStateEnum.AiNormal)
    {
    }

    public override void Enter(AINormalStateEnum prev, params object[] args)
    {
        _isFindPlayer = false;
        _isMoveOver = true;
        _againstWall = false;
        _againstWallNormalAngle = 0;
        _pauseTimer = 0;
        _moveFlag = false;
    }

    public override void Process(float delta)
    {
        if (_isFindPlayer) //已经找到玩家了
        {
            //现临时处理, 直接切换状态
            ChangeState(AINormalStateEnum.AiTailAfter);
        }
        else //没有找到玩家
        {
            //检测玩家
            var player = Player.Current;
            //玩家中心点坐标
            var playerPos = player.GetCenterPosition();

            if (Master.IsInViewRange(playerPos) && !Master.TestViewRayCast(playerPos)) //发现玩家
            {
                //发现玩家
                _isFindPlayer = true;
            }
            else if (_pauseTimer >= 0)
            {
                Master.AnimatedSprite.Play(AnimatorNames.Idle);
                _pauseTimer -= delta;
            }
            else if (_isMoveOver) //没发现玩家, 且已经移动完成
            {
                RunOver();
                _isMoveOver = false;
            }
            else //移动中
            {
                if (_lockTimer >= 1) //卡在一个点超过一秒
                {
                    RunOver();
                    _isMoveOver = false;
                    _lockTimer = 0;
                }
                else if (Master.NavigationAgent2D.IsNavigationFinished()) //到达终点
                {
                    _pauseTimer = Utils.Random.RandomRangeFloat(0.3f, 2f);
                    _isMoveOver = true;
                    _moveFlag = false;
                    Master.DoIdle();
                }
                else if (!_moveFlag)
                {
                    _moveFlag = true;
                    var pos = Master.Position;
                    //移动
                    Master.DoMove();
                    _prevPos = pos;
                }
                else
                {
                    var pos = Master.Position;
                    var lastSlideCollision = Master.GetLastSlideCollision();
                    if (lastSlideCollision != null && lastSlideCollision.GetCollider() is AdvancedRole) //碰到其他角色
                    {
                        _pauseTimer = Utils.Random.RandomRangeFloat(0.1f, 0.5f);
                        _isMoveOver = true;
                        _moveFlag = false;
                        Master.DoIdle();
                    }
                    else
                    {
                        //计算移动
                        Master.DoMove();
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
            }

            //关闭射线检测
            Master.TestViewRayCastOver();
        }
    }
    
    //移动结束
    private void RunOver()
    {
        float angle;
        if (_againstWall)
        {
            angle = Utils.Random.RandomRangeFloat(_againstWallNormalAngle - Mathf.Pi * 0.5f,
                _againstWallNormalAngle + Mathf.Pi * 0.5f);
        }
        else
        {
            angle = Utils.Random.RandomRangeFloat(0, Mathf.Pi * 2f);
        }

        var len = Utils.Random.RandomRangeInt(30, 200);
        _nextPos = new Vector2(len, 0).Rotated(angle) + Master.GlobalPosition;
        //获取射线碰到的坐标
        if (Master.TestViewRayCast(_nextPos)) //碰到墙壁
        {
            _nextPos = Master.ViewRay.GetCollisionPoint();
            _againstWall = true;
            _againstWallNormalAngle = Master.ViewRay.GetCollisionNormal().Angle();
        }
        else
        {
            _againstWall = false;
        }

        Master.NavigationAgent2D.TargetPosition = _nextPos;
        Master.LookTargetPosition(_nextPos);
    }
}