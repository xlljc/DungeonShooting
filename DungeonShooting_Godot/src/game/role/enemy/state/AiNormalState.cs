
using Godot;

/// <summary>
/// AI 正常状态
/// </summary>
public class AiNormalState : StateBase<Enemy, AiStateEnum>
{
    //是否发现玩家
    private bool _isFindPlayer;

    //下一个运动的角度
    private Vector2 _nextPos;

    //是否移动结束
    private bool _isMoveOver;

    //上一次移动是否撞墙
    private bool _againstWall;
    
    //撞墙法线角度
    private float _againstWallNormalAngle;

    //移动停顿计时器
    private float _pauseTimer;

    public AiNormalState() : base(AiStateEnum.AiNormal)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        _isFindPlayer = false;
        _isMoveOver = true;
        _againstWall = false;
        _againstWallNormalAngle = 0;
        _pauseTimer = 0;
    }
    
    public override void PhysicsProcess(float delta)
    {
        //其他敌人发现玩家
        if (Enemy.IsFindTarget)
        {
            ChangeStateLate(AiStateEnum.AiLeaveFor);
            return;
        }
        
        if (_isFindPlayer) //已经找到玩家了
        {
            //现临时处理, 直接切换状态
            ChangeStateLate(AiStateEnum.AiTailAfter);
        }
        else //没有找到玩家
        {
            //检测玩家
            var player = GameApplication.Instance.Room.Player;
            //玩家中心点坐标
            var playerPos = player.GetCenterPosition();

            if (Master.IsInViewRange(playerPos) && !Master.TestViewRayCast(playerPos)) //发现玩家
            {
                //发现玩家
                _isFindPlayer = true;
            }
            else if (_pauseTimer >= 0)
            {
                _pauseTimer -= delta;
            }
            else if (_isMoveOver) //没发现玩家, 且已经移动完成
            {
                RunOver();
                _isMoveOver = false;
            }
            else //移动中
            {
                //计算移动
                var nextPos = Master.NavigationAgent2D.GetNextLocation();
                Master.AnimatedSprite.Animation = AnimatorNames.Run;
                Master.Velocity = (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() *
                                  Master.MoveSpeed;
                Master.CalcMove(delta);

                if (Master.NavigationAgent2D.IsNavigationFinished()) //到达终点
                {
                    _pauseTimer = Utils.RandRange(0.3f, 2f);
                    _isMoveOver = true;
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
            angle = Utils.RandRange(_againstWallNormalAngle - Mathf.Pi * 0.5f,
                _againstWallNormalAngle + Mathf.Pi * 0.5f);
        }
        else
        {
            angle = Utils.RandRange(0, Mathf.Pi * 2f);
        }

        var len = Utils.RandRangeInt(30, 200);
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

        Master.NavigationAgent2D.SetTargetLocation(_nextPos);
        Master.LookTargetPosition(_nextPos);
    }

    public override void DebugDraw()
    {
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(_nextPos), Colors.Green);
    }
}
