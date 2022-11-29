
using Godot;

/// <summary>
/// AI 正常状态
/// </summary>
public class AiNormalState : StateBase<Enemy, AIStateEnum>
{
    //是否发现玩家
    private bool _isFindPlayer;
    //下一个运动的角度
    private Vector2 _nextPos;
    //是否移动结束
    private bool _isMoveOver;
    
    public AiNormalState() : base(AIStateEnum.AINormal)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        _isFindPlayer = false;
        _isMoveOver = true;
    }

    public override void PhysicsProcess(float delta)
    {

        if (_isFindPlayer) //已经找到玩家了
        {
            //现临时处理, 直接切换状态
            ChangeStateLate(AIStateEnum.AITailAfter);
        }
        else //没有找到玩家
        {
            //检测玩家
            var player = GameApplication.Instance.Room.Player;
            //玩家中心点坐标
            var playerPos = player.MountPoint.GlobalPosition;

            if (Master.IsInViewRange(playerPos) && !Master.TestViewRayCast(playerPos)) //发现玩家
            {
                //发现玩家
                _isFindPlayer = true;
            }
            else if (_isMoveOver) //没发现玩家, 且已经移动完成
            {
                var angle = Utils.RandRange(0, Mathf.Pi * 2f);
                var len = Utils.RandRangeInt(50, 500);
                _nextPos = new Vector2(len, 0).Rotated(angle);
                //获取射线碰到的坐标
                if (Master.TestViewRayCast(_nextPos)) //碰到墙壁
                {
                    _nextPos = Master.ViewRay.GetCollisionPoint();
                }
                Master.NavigationAgent2D.SetTargetLocation(_nextPos);
                _isMoveOver = false;
            }
            else //移动中
            {
                //计算移动
                var nextPos = Master.NavigationAgent2D.GetNextLocation();
                Master.LookTargetPosition(_nextPos);
                Master.AnimatedSprite.Animation = AnimatorNames.Run;
                Master.Velocity = (nextPos - Master.GlobalPosition - Master.NavigationPoint.Position).Normalized() * Master.MoveSpeed;
                Master.CalcMove(delta);

                if (Master.NavigationAgent2D.IsNavigationFinished())
                {
                    _isMoveOver = true;
                }
            }
            Master.TestViewRayCastOver();
        }
    }

    public override void DebugDraw()
    {
        Master.DrawLine(Vector2.Zero, Master.ToLocal(_nextPos), Colors.Green);
    }
}
