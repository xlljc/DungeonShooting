using Godot;

/// <summary>
/// 子弹类
/// </summary>
public class Bullet : ActivityObject
{
    public Bullet(string scenePath, float maxDistance, Vector2 position, float rotation, uint targetLayer) :
        base(scenePath)
    {
        CollisionArea = GetNode<Area2D>("CollisionArea");
        CollisionArea.CollisionMask = targetLayer;
        CollisionArea.Connect("body_entered", this, nameof(_BodyEntered));

        Collision.Disabled = true;
        
        MaxDistance = maxDistance;
        Position = position;
        Rotation = rotation;
        ShadowOffset = new Vector2(0, 5);
    }

    public Area2D CollisionArea { get; }
    
    // 最大飞行距离
    private float MaxDistance;

    // 子弹飞行速度
    private float FlySpeed = 450;

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
        Position += new Vector2(FlySpeed * delta, 0).Rotated(Rotation);
        //距离太大, 自动销毁
        CurrFlyDistance += FlySpeed * delta;
        if (CurrFlyDistance >= MaxDistance)
        {
            Destroy();
        }
    }

    private void _BodyEntered(Node2D other)
    {
        if (other is Role role)
        {
            role.Hit(1);
        }

        //播放受击动画
        Node2D hit = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_Hit_tscn).Instance<Node2D>();
        hit.RotationDegrees = Utils.RandRangeInt(0, 360);
        hit.GlobalPosition = GlobalPosition;
        GameApplication.Instance.Room.GetRoot(true).AddChild(hit);

        Destroy();
    }
}