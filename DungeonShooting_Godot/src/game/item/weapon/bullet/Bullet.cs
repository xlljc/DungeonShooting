using Godot;

/// <summary>
/// 子弹类
/// </summary>
public class Bullet : ActivityObject
{
    /// <summary>
    /// 碰撞区域
    /// </summary>
    public Area2D CollisionArea { get; }

    // 最大飞行距离
    private float MaxDistance;

    // 子弹飞行速度
    private float FlySpeed = 350;

    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    public Bullet(string scenePath, float maxDistance, Vector2 position, float rotation, uint targetLayer) :
        base(scenePath)
    {
        CollisionArea = GetNode<Area2D>("CollisionArea");
        CollisionArea.CollisionMask = targetLayer;
        CollisionArea.Connect("area_entered", this, nameof(OnArea2dEntered));
        CollisionArea.Connect("body_entered", this, nameof(OnBodyEntered));

        Collision.Disabled = true;

        MaxDistance = maxDistance;
        Position = position;
        Rotation = rotation;
        ShadowOffset = new Vector2(0, 5);
    }

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

    private void OnArea2dEntered(Area2D other)
    {
        var role = other.AsActivityObject<Role>();
        if (role != null)
        {
            role.Hurt(1);
         
            //播放受击动画
            // Node2D hit = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_Hit_tscn).Instance<Node2D>();
            // hit.RotationDegrees = Utils.RandRangeInt(0, 360);
            // hit.GlobalPosition = GlobalPosition;
            // GameApplication.Instance.Room.GetRoot(true).AddChild(hit);
            
            DoDestroy();
        }
    }

    private void OnBodyEntered(Node2D other)
    {
        if (!(other is Role))
        {
            DoDestroy();
        }
    }

    private void DoDestroy()
    {
        SpecialEffectManager.Play(ResourcePath.resource_effects_Hit_tres, "default", GlobalPosition,
            Mathf.Deg2Rad(Utils.RandRangeInt(0, 360)), Vector2.One, new Vector2(1, 11), 0);

        Destroy();
    }
}