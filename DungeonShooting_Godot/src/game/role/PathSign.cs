
using Godot;

/// <summary>
/// 寻路标记, 记录下Role移动过的位置, 用于Ai寻路
/// </summary>
public class PathSign : Node2D, IDestroy
{
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 当前标记在整个链上的索引
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// 监视的对象
    /// </summary>
    public Role Target { get; }
    
    /// <summary>
    /// 视野半径
    /// </summary>
    public float ViewRadius { get; }

    /// <summary>
    /// 射线对象
    /// </summary>
    public RayCast2D RayCast { get; }
    
    /// <summary>
    /// 连接的下一个 PathSign
    /// </summary>
    public PathSign Next { get; set; }

    /// <summary>
    /// 是否启用标记路径, 如果禁用, 将会清空所有标记
    /// </summary>
    public bool Enable
    {
        get => _enable;
        set
        {
            if (_enable && !value && Next != null)
            {
                Next.Destroy();
                Next = null;
            }

            if (!value)
            {
                _isInRange = false;
                _isCollision = false;
                _targetPos = Vector2.Zero;
                _isDiscoverTarget = false;
            }
            _enable = value;
        }
    }

    /// <summary>
    /// 目标出现过的位置
    /// </summary>
    public Vector2 TargetPosition
    {
        get => _targetPos;
        set => _targetPos = value;
    }

    //是否发现过目标
    private bool _isDiscoverTarget = false;
    //目标在视野范围内出现过的位置
    private Vector2 _targetPos;
    //射线是否碰撞到目标
    private bool _isCollision;
    //目标是否在范围内
    private bool _isInRange;
    //是否启用
    private bool _enable = false;

    /// <summary>
    /// 创建标记
    /// </summary>
    /// <param name="root">挂载节点</param>
    /// <param name="viewRadius">视野半径</param>
    /// <param name="target">监视对象</param>
    public PathSign(Node2D root, float viewRadius, Role target) : this(root, Vector2.Zero, viewRadius, target, 0)
    {
    }

    private PathSign(Node2D root, Vector2 pos, float viewRadius, Role target, int index)
    {
        Index = index;
        Target = target;
        ViewRadius = viewRadius;
        root.AddChild(this);
        Position = pos;

        //目前只检测墙壁碰撞
        RayCast = new RayCast2D();
        RayCast.CollisionMask = PhysicsLayer.Wall;
        AddChild(RayCast);

        //绘制箭头
        if (GameApplication.Instance.Debug)
        {
            var sprite = new Sprite();
            sprite.Texture = ResourceManager.Load<Texture>(ResourcePath.resource_effects_debug_arrows_png);
            sprite.Position = new Vector2(0, -sprite.Texture.GetHeight() * 0.5f);
            AddChild(sprite);
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (GameApplication.Instance.Debug)
        {
            Update();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!_enable)
        {
            return;
        }
        //监视目标
        var nowTargetPos = Target.GlobalPosition;
        var distanceSquared = GlobalPosition.DistanceSquaredTo(nowTargetPos);
        var nowIsInRange = distanceSquared <= ViewRadius * ViewRadius;

        if (nowIsInRange) //在视野范围内
        {
            var isCollision = Detect(nowTargetPos);

            if (isCollision) //碰到墙
            {
                if (_isInRange && !_isCollision && Next == null) //如果上一帧就在视野内, 才能创建新的折点
                {
                    var distance = Mathf.Sqrt(distanceSquared);
                    Next = new PathSign(GameApplication.Instance.Room.GetRoot(false), _targetPos, ViewRadius - distance, Target, Index + 1);
                    Next._targetPos = nowTargetPos;
                    Next.Enable = true;
                }
            }
            else //没有碰到墙
            {
                if (Next != null)
                {
                    Next.Destroy();
                    Next = null;
                }
                _targetPos = nowTargetPos;
                _isDiscoverTarget = true;
            }
            
            _isCollision = isCollision;
        }
        else
        {
            _isCollision = false;
        }

        _isInRange = nowIsInRange;
    }

    /// <summary>
    /// 检测射线是否碰到墙壁
    /// </summary>
    /// <returns></returns>
    private bool Detect(Vector2 pos)
    {
        RayCast.Enabled = true;
        RayCast.CastTo = RayCast.ToLocal(pos);
        RayCast.ForceRaycastUpdate();

        var flag = RayCast.IsColliding();
        RayCast.Enabled = false;
        return flag;
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;

        if (Next != null)
        {
            Next.Destroy();
        }

        QueueFree();
    }

    public override void _Draw()
    {
        if (GameApplication.Instance.Debug && _isDiscoverTarget)
        {
            if (Next != null)
            {
                DrawLine(Vector2.Zero, ToLocal(Next.GlobalPosition), Colors.Blue);
            }
            else if (_isInRange && !_isCollision)
            {
                var pos = ToLocal(_targetPos);
                DrawString(ResourceManager.Load<Font>(ResourcePath.resource_font_cn_font_12_tres), new Vector2(-6, 12), (ViewRadius - GlobalPosition.DistanceTo(_targetPos)).ToString());
                DrawLine(Vector2.Zero, pos, Colors.Red);
            }
            else
            {
                var pos = ToLocal(_targetPos);
                DrawString(ResourceManager.Load<Font>(ResourcePath.resource_font_cn_font_12_tres), new Vector2(-6, 12), "0");
                DrawLine(Vector2.Zero, pos, Colors.Yellow);
            }
        }
    }
}

#region 备份代码
/*
第一个绑定在Role身上的点, 需要每一帧更新
if (Master.PathSign.Enable)
{
    var targetSign = master.PathSign;
    var enemyPos = master.GlobalPosition;
    if (targetSign.Next == null)
    {
        var targetPosition = targetSign.TargetPosition;

        if (enemyPos.DistanceSquaredTo(targetPosition) <=
            master.Velocity.LengthSquared() * delta) //移动到下一个节点了, 还是没有找到目标, 变为第二状态
        {
            StateController.ChangeStateLate(AIStateEnum.AINormal);
        }
        else //继续移动
        {
            master.LookTargetPosition(targetPosition);
            master.AnimatedSprite.Animation = AnimatorNames.Run;
            master.Velocity = (targetPosition - enemyPos).Normalized() * master.MoveSpeed;
            master.CalcMove(delta);
        }
    }
    else
    {
        var nextPos = targetSign.Next.GlobalPosition;

        if (enemyPos.DistanceSquaredTo(nextPos) <=
            master.Velocity.LengthSquared() * delta) //已经移动到下一个节点了, 删除下一个节点, 后面的接上
        {
            var nextNext = targetSign.Next.Next;
            var tempPos = targetSign.Next.TargetPosition;
            targetSign.Next.Next = null;
            targetSign.Next.Destroy();
            targetSign.Next = nextNext;

            if (nextNext != null) //下一个点继续移动
            {
                nextPos = nextNext.GlobalPosition;
                master.LookTargetPosition(nextPos);
                master.AnimatedSprite.Animation = AnimatorNames.Run;
                master.Velocity = (nextPos - enemyPos).Normalized() * master.MoveSpeed;
                master.CalcMove(delta);
            }
            else
            {
                targetSign.TargetPosition = tempPos;
            }
        }
        else //继续移动
        {
            master.LookTargetPosition(nextPos);
            master.AnimatedSprite.Animation = AnimatorNames.Run;
            master.Velocity = (nextPos - enemyPos).Normalized() * master.MoveSpeed;
            master.CalcMove(delta);
        }
    }
}

*/
#endregion
