using Godot;

/// <summary>
/// 普通的子弹
/// </summary>
public class OrdinaryBullets : KinematicBody2D, IBullet
{
    public CampEnum TargetCamp { get; private set; }

    public Gun Gun { get; private set; }

    public Node2D Master { get; private set; }

    /// <summary>
    /// 碰撞物体后产生的火花
    /// </summary>
    [Export] public PackedScene Hit;

    // 起始点坐标
    private Vector2 StartPosition;
    // 最大飞行距离
    private float MaxDistance;
    // 子弹飞行速度
    private float FlySpeed = 1500;
    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;
    //碰撞数据
    private KinematicCollision2D CollisionData = null;

    //子弹的精灵
    private Sprite BulletSprite;

    public void Init(CampEnum target, Gun gun, Node2D master)
    {
        TargetCamp = target;
        Gun = gun;
        Master = master;

        MaxDistance = MathUtils.RandRange(gun.Attribute.MinDistance, gun.Attribute.MaxDistance);
        StartPosition = GlobalPosition;
        //Scale = new Vector2(0, 1);
        BulletSprite = GetNode<Sprite>("Bullet");
        // BulletSprite.Visible = false;

        Scale = new Vector2(FlySpeed / 60 * 1.5f, 1);

        GD.Print("---------------1" + GlobalPosition);
    }

    public override void _PhysicsProcess(float delta)
    {
        GD.Print("---------------2" + GlobalPosition);

        if (CollisionData != null) //碰到物体
        {
            // 销毁
            QueueFree();
            Node2D hit = Hit.Instance<Node2D>();
            hit.RotationDegrees = MathUtils.RandRangeInt(0, 360);
            hit.GlobalPosition = CollisionData.Position;
            GetTree().CurrentScene.AddChild(hit);
            return;
        }

        // 移动
        CollisionData = MoveAndCollide(Vector2.Right.Rotated(GlobalRotation) * FlySpeed * delta);

        if (CurrFlyDistance == 0)
        {
            BulletSprite.Visible = true;
        }

        CurrFlyDistance += FlySpeed * delta;
        if (CurrFlyDistance >= MaxDistance)
        {
            QueueFree();
        }
    }

}