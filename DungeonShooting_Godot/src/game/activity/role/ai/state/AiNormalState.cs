
using System.Linq;
using Godot;

namespace AiState;

/// <summary>
/// AI 正常状态
/// </summary>
public class AiNormalState : StateBase<AiRole, AIStateEnum>
{
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

    public AiNormalState() : base(AIStateEnum.AiNormal)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        _isMoveOver = true;
        _againstWall = false;
        _againstWallNormalAngle = 0;
        _pauseTimer = 0;
        _moveFlag = false;
        Master.LookTarget = null;
    }

    public override void Process(float delta)
    {
        if (Master.HasAttackDesire) //有攻击欲望
        {
            //获取攻击目标
            var attackTarget = Master.GetAttackTarget();
            if (attackTarget != null)
            {
                //玩家中心点坐标
                var targetPos = attackTarget.GetCenterPosition();

                if (Master.IsInViewRange(targetPos) && !Master.TestViewRayCast(targetPos)) //发现目标
                {
                    //关闭射线检测
                    Master.TestViewRayCastOver();
                    //发现玩家
                    Master.LookTarget = attackTarget;
                    //判断是否进入通知状态
                    if (Master.World.Enemy_InstanceList.FindIndex(enemy =>
                            enemy != Master && !enemy.IsDie && enemy.AffiliationArea == Master.AffiliationArea &&
                            enemy.StateController.CurrState == AIStateEnum.AiNormal) != -1)
                    {
                        //进入惊讶状态, 然后再进入通知状态
                        ChangeState(AIStateEnum.AiAstonished, AIStateEnum.AiNotify);
                    }
                    else
                    {
                        //进入惊讶状态, 然后再进入跟随状态
                        ChangeState(AIStateEnum.AiAstonished, AIStateEnum.AiTailAfter);
                    }
                    return;
                }
            }
        }

        if (_pauseTimer >= 0)
        {
            Master.AnimatedSprite.Play(AnimatorNames.Idle);
            _pauseTimer -= delta;
        }
        else if (_isMoveOver) //没发现玩家, 且已经移动完成
        {
            RunOver();
            _isMoveOver = false;
        }
        else if (Master.HasMoveDesire) //移动中
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
                //站立
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
                if (lastSlideCollision != null && lastSlideCollision.GetCollider() is Role) //碰到其他角色
                {
                    _pauseTimer = Utils.Random.RandomRangeFloat(0.1f, 0.5f);
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

    public override void DebugDraw()
    {
        Master.DrawLine(new Vector2(0, -8), Master.ToLocal(_nextPos), Colors.Green);
    }
}
