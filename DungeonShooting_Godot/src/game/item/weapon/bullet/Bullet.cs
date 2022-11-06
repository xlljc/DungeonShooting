using Godot;

/// <summary>
/// 子弹类
/// </summary>
public class Bullet : ActivityObject
{
    public Bullet(string scenePath, float maxDistance, Vector2 position, float rotation, uint targetLayer) :
        base(scenePath)
    {
        MaxDistance = maxDistance;
        CollisionMask = targetLayer;
        Position = position;
        Rotation = rotation;
        ShadowOffset = new Vector2(0, 5);
    }

    // 最大飞行距离
    private float MaxDistance;

    // 子弹飞行速度
    private float FlySpeed = 1500;

    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    public override void _Ready()
    {
        base._Ready();
        //绘制阴影
        ShowShadowSprite();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        //移动
        var kinematicCollision = MoveAndCollide(new Vector2(FlySpeed * delta, 0).Rotated(Rotation));
        //有碰撞
        if (kinematicCollision == null)
        {
            //距离太大, 自动销毁
            CurrFlyDistance += FlySpeed * delta;
            if (CurrFlyDistance >= MaxDistance)
            {
                Destroy();
            }
        }
        else
        {
            var collider = kinematicCollision.Collider;
            if (collider is Role role)
            {
                role.Hit(1);
            }

            //播放受击动画
            Node2D hit = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_Hit_tscn).Instance<Node2D>();
            hit.RotationDegrees = MathUtils.RandRangeInt(0, 360);
            hit.GlobalPosition = kinematicCollision.Position;
            GameApplication.Instance.Room.GetRoot(true).AddChild(hit);

            Destroy();
        }

    }
}