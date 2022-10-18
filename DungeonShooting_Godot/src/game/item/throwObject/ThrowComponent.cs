
using Godot;

public class ThrowComponent : Component
{

    /// <summary>
    /// 是否已经结束
    /// </summary>
    public bool IsOver { get; protected set; } = true;

    /// <summary>
    /// 物体大小
    /// </summary>
    public Vector2 Size { get; protected set; } = Vector2.One;

    /// <summary>
    /// 起始坐标
    /// </summary>
    public Vector2 StartPosition { get; protected set; }

    /// <summary>
    /// 移动方向, 0 - 360
    /// </summary>
    public float Direction { get; protected set; }

    /// <summary>
    /// x速度, 也就是水平速度
    /// </summary>
    public float XSpeed { get; protected set; }

    /// <summary>
    /// y轴速度, 也就是竖直速度
    /// </summary>
    public float YSpeed { get; protected set; }

    /// <summary>
    /// 初始x轴组队
    /// </summary>
    public float StartXSpeed { get; protected set; }

    /// <summary>
    /// 初始y轴速度
    /// </summary>
    public float StartYSpeed { get; protected set; }

    /// <summary>
    /// 旋转速度
    /// </summary>
    public float RotateSpeed { get; protected set; }

    /// <summary>
    /// 绑定的kinematicBody2D节点
    /// </summary>
    protected KinematicBody2D KinematicBody;
    /// <summary>
    /// 碰撞器节点
    /// </summary>
    protected CollisionShape2D CollisionShape;
    /// <summary>
    /// 碰撞器形状
    /// </summary>
    protected RectangleShape2D RectangleShape;

    public ThrowComponent()
    {
        KinematicBody = new KinematicBody2D();
        KinematicBody.Name = nameof(ThrowComponent);
        //只与墙壁碰撞
        KinematicBody.CollisionMask = 1;
        KinematicBody.CollisionLayer = 0;
        //创建碰撞器
        CollisionShape = new CollisionShape2D();
        CollisionShape.Name = "Collision";
        RectangleShape = new RectangleShape2D();
        CollisionShape.Shape = RectangleShape;
        KinematicBody.AddChild(CollisionShape);
    }

    public override void Update(float delta)
    {
        if (!IsOver)
        {
            KinematicBody.MoveAndSlide(new Vector2(XSpeed, 0).Rotated(Direction * Mathf.Pi / 180));
            Position = new Vector2(0, Position.y - YSpeed * delta);
            var rotate = ActivityObject.GlobalRotationDegrees + RotateSpeed * delta;
            ActivityObject.GlobalRotationDegrees = rotate;

            //计算阴影位置
            ShadowSprite.GlobalRotationDegrees = rotate;
            // ShadowSprite.GlobalRotationDegrees = rotate + (inversionX ? 180 : 0);
            ShadowSprite.GlobalPosition = AnimatedSprite.GlobalPosition + new Vector2(0, 2 - Position.y);
            var ysp = YSpeed;
            YSpeed -= GameConfig.G * delta;
            //达到最高点
            if (ysp * YSpeed < 0)
            {
                OnMaxHeight(-Position.y);
            }

            //落地判断
            if (Position.y >= 0)
            {
                Position = new Vector2(0, 0);
                IsOver = true;
                OnOver();
            }
        }
    }

    public virtual void StartThrow(Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed,
        float ySpeed, float rotate)
    {
        CollisionShape.Disabled = false;

        IsOver = false;
        Size = size;
        KinematicBody.GlobalPosition = StartPosition = start;
        Direction = direction;
        XSpeed = xSpeed;
        YSpeed = ySpeed;
        StartXSpeed = xSpeed;
        StartYSpeed = ySpeed;
        RotateSpeed = rotate;

        RectangleShape.Extents = Size * 0.5f;

        var mountParent = ActivityObject.GetParent();
        if (mountParent == null)
        {
            KinematicBody.AddChild(ActivityObject);
        }
        else if (mountParent != ActivityObject)
        {
            mountParent.RemoveChild(ActivityObject);
            KinematicBody.AddChild(ActivityObject);
        }

        Position = new Vector2(0, -startHeight);

        var parent = KinematicBody.GetParent();
        if (parent == null)
        {
            RoomManager.Current.SortRoot.AddChild(KinematicBody);
        }
        else if (parent == RoomManager.Current.ObjectRoot)
        {
            GD.Print("1111");
            parent.RemoveChild(KinematicBody);
            RoomManager.Current.SortRoot.AddChild(KinematicBody);
        }

        //显示阴影
        ActivityObject.ShowShadowSprite();
        ShadowSprite.Scale = AnimatedSprite.Scale;
    }

    /// <summary>
    /// 停止投抛运动
    /// </summary>
    public void StopThrow()
    {
        if (!IsOver)
        {
            var gp = ActivityObject.GlobalPosition;
            var gr = ActivityObject.GlobalRotation;
            IsOver = true;
            KinematicBody.RemoveChild(ActivityObject);
            var parent = KinematicBody.GetParent();
            parent.AddChild(ActivityObject);
            ActivityObject.GlobalPosition = gp;
            ActivityObject.GlobalRotation = gr;
        }

        CollisionShape.Disabled = true;
    }

    /// <summary>
    /// 达到最高点时调用
    /// </summary>
    protected virtual void OnMaxHeight(float height)
    {

    }

    /// <summary>
    /// 结束的调用
    /// </summary>
    protected virtual void OnOver()
    {
        KinematicBody.GetParent().RemoveChild(KinematicBody);
        RoomManager.Current.ObjectRoot.AddChild(KinematicBody);
        CollisionShape.Disabled = true;
    }

}
