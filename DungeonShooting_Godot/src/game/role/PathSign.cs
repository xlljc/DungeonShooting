
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
    /// 连接的上一个 PathSign
    /// </summary>
    public PathSign Prev { get; set; }

    /// <summary>
    /// 连接的下一个 PathSign
    /// </summary>
    public PathSign Next { get; set; }

    private Vector2 _prevTargetPos;
    
    /// <summary>
    /// 创建标记
    /// </summary>
    /// <param name="pos">坐标</param>
    /// <param name="viewRadius">视野半径</param>
    /// <param name="target">监视对象</param>
    public PathSign(Vector2 pos, float viewRadius, Role target) : this(pos, viewRadius, target, 0, null)
    {
    }

    private PathSign(Vector2 pos, float viewRadius, Role target, int index, PathSign prev)
    {
        Index = index;
        Prev = prev;
        Target = target;
        ViewRadius = viewRadius;
        GameApplication.Instance.Room.GetRoot(false).AddChild(this);
        GlobalPosition = pos;

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
        if (Next == null)
        {
            //监视目标
            var targetPos = Target.GlobalPosition;
            if (GlobalPosition.DistanceSquaredTo(targetPos) <= ViewRadius * ViewRadius) //在视野范围内
            {
                RayCast.Enabled = true;
                RayCast.CastTo = RayCast.ToLocal(targetPos);
                RayCast.ForceRaycastUpdate();

                if (RayCast.IsColliding())
                {
                    Next = new PathSign(_prevTargetPos, ViewRadius, Target, Index + 1, this);
                }
                RayCast.Enabled = false;
                _prevTargetPos = targetPos;
            }
        }
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
        if (GameApplication.Instance.Debug)
        {
            if (Next != null)
            {
                DrawLine(Vector2.Zero,ToLocal(Next.GlobalPosition), Colors.Red);
            }
        }
    }
}
