using System;
using Godot;

/// <summary>
/// 普通的子弹
/// </summary>
public class OrdinaryBullets : Node2D, IBullet
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
    //射线碰撞节点
    private RayCast2D RayCast;
    //子弹的精灵
    private Sprite BulletSprite;
    //绘制阴影的精灵
    private Sprite ShadowSprite;

    private int frame = 0;

    public void Init(CampEnum target, Gun gun, Node2D master)
    {
        TargetCamp = target;
        Gun = gun;
        Master = master;

        MaxDistance = MathUtils.RandRange(gun.Attribute.MinDistance, gun.Attribute.MaxDistance);
        StartPosition = GlobalPosition;
        BulletSprite = GetNode<Sprite>("Bullet");
        BulletSprite.Visible = false;
        RayCast = GetNode<RayCast2D>("RayCast2D");

        //创建阴影
        ShadowSprite = new Sprite();
        ShadowSprite.Visible = false;
        ShadowSprite.ZIndex = -1;
        ShadowSprite.Texture = BulletSprite.Texture;
        ShadowSprite.Offset = BulletSprite.Offset;
        ShadowSprite.Material = ResourceManager.ShadowMaterial;
        AddChild(ShadowSprite);
    }

    public override void _Ready()
    {
        //生成时播放音效
        SoundManager.PlaySoundEffect("ordinaryBullet.ogg", this, 6f);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (frame++ == 0)
        {
            BulletSprite.Visible = true;
            ShadowSprite.Visible = true;
        }
        //碰到墙壁
        if (RayCast.IsColliding())
        {
            //var target = RayCast.GetCollider();
            var pos = RayCast.GetCollisionPoint();
            //播放受击动画
            Node2D hit = Hit.Instance<Node2D>();
            hit.RotationDegrees = MathUtils.RandRangeInt(0, 360);
            hit.GlobalPosition = pos;
            GetTree().CurrentScene.AddChild(hit);
            QueueFree();
        }
        else //没有碰到, 继续移动
        {
            ShadowSprite.GlobalPosition = GlobalPosition + new Vector2(0, 5);
            Position += new Vector2(FlySpeed * delta, 0).Rotated(Rotation);

            CurrFlyDistance += FlySpeed * delta;
            if (CurrFlyDistance >= MaxDistance)
            {
                QueueFree();
            }
        }
    }

}