
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

    //绑定的kinematicBody2D节点
    private KinematicBody2D _kinematicBody2D;

    //碰撞器形状
    private CollisionShape2D _collisionShape2D;

    private RectangleShape2D _rectangleShape2D;

    public ThrowComponent()
    {
        _kinematicBody2D = new KinematicBody2D();
        _kinematicBody2D.Name = nameof(ThrowComponent);
        //只与墙壁碰撞
        _kinematicBody2D.CollisionMask = 1;
        _kinematicBody2D.CollisionLayer = 0;
        //创建碰撞器
        _collisionShape2D = new CollisionShape2D();
        _collisionShape2D.Name = "Collision";
        _rectangleShape2D = new RectangleShape2D();
        _collisionShape2D.Shape = _rectangleShape2D;
        _kinematicBody2D.AddChild(_collisionShape2D);
        
        _kinematicBody2D.ZIndex = 2;
    }

    public override void Update(float delta)
    {
        if (!IsOver)
        {
            _kinematicBody2D.MoveAndSlide(new Vector2(XSpeed, 0).Rotated(Direction * Mathf.Pi / 180));
            Position = new Vector2(0, Position.y - YSpeed * delta);
            var rotate = ActivityObject.GlobalRotationDegrees + RotateSpeed * delta;
            ActivityObject.GlobalRotationDegrees = rotate;

            ShadowSprite.GlobalRotationDegrees = rotate;
            // ShadowSprite.GlobalRotationDegrees = rotate + (inversionX ? 180 : 0);
            ShadowSprite.GlobalPosition = AnimatedSprite.GlobalPosition + new Vector2(0, 1 - Position.y);
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
        _collisionShape2D.Disabled = false;

        IsOver = false;
        Size = size;
        _kinematicBody2D.GlobalPosition = StartPosition = start;
        Direction = direction;
        XSpeed = xSpeed;
        YSpeed = ySpeed;
        StartXSpeed = xSpeed;
        StartYSpeed = ySpeed;
        RotateSpeed = rotate;

        _rectangleShape2D.Extents = Size * 0.5f;
        
        var mountParent = ActivityObject.GetParent();
        if (mountParent == null)
        {
            _kinematicBody2D.AddChild(ActivityObject);
        }
        else if (mountParent != ActivityObject)
        {
            mountParent.RemoveChild(ActivityObject);
            _kinematicBody2D.AddChild(ActivityObject);
        }

        Position = new Vector2(0, -startHeight);

        var parent = _kinematicBody2D.GetParent();
        if (parent == null)
        {
            RoomManager.Current.SortRoot.AddChild(_kinematicBody2D);
        }
        else if (parent == RoomManager.Current.ObjectRoot)
        {
            parent.RemoveChild(ActivityObject);
            RoomManager.Current.SortRoot.AddChild(_kinematicBody2D);
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
            _kinematicBody2D.RemoveChild(ActivityObject);
            var parent = _kinematicBody2D.GetParent();
            parent.AddChild(ActivityObject);
            ActivityObject.GlobalPosition = gp;
            ActivityObject.GlobalRotation = gr;
        }

        _collisionShape2D.Disabled = true;
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
        _kinematicBody2D.GetParent().RemoveChild(_kinematicBody2D);
        RoomManager.Current.ObjectRoot.AddChild(_kinematicBody2D);
        _collisionShape2D.Disabled = true;
    }

}
