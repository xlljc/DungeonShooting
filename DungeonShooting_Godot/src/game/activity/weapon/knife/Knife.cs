
using Godot;

/// <summary>
/// 近战武器,刀
/// </summary>
[Tool]
public partial class Knife : Weapon
{
    /// <summary>
    /// 近战攻击范围
    /// </summary>
    [Export]
    public int AttackRange { get; set; } = 41;

    /// <summary>
    /// 开始蓄力时武器抬起角度
    /// </summary>
    [Export]
    public int BeginChargeAngle { get; set; } = 120;
    
    private Area2D _hitArea;
    private int _attackIndex = 0;
    private CollisionPolygon2D _collisionPolygon;
    
    public override void OnInit()
    {
        base.OnInit();

        //没有Master时不能触发开火
        NoMasterCanTrigger = false;
        _hitArea = GetNode<Area2D>("HitArea");
        _collisionPolygon = new CollisionPolygon2D();
        var a = Mathf.Abs(-BeginChargeAngle + Attribute.UpliftAngle);
        var ca = Utils.ConvertAngle(-a / 2f);
        _collisionPolygon.Polygon = Utils.CreateSectorPolygon(ca, AttackRange, a, 6);
        _hitArea.AddChild(_collisionPolygon);
        
        _hitArea.Monitoring = false;
        _hitArea.Monitorable = false;
        _hitArea.BodyEntered += OnBodyEntered;
        _hitArea.AreaEntered += OnArea2dEntered;

        //禁用自动播放动画
        IsAutoPlaySpriteFrames = false;
    }

    protected override void Process(float delta)
    {
        base.Process(delta);
        if (IsActive)
        {
            //让碰撞节点与武器挂载节点位置保持一致, 而不跟着武器走
            _hitArea.GlobalPosition = Master.MountPoint.GlobalPosition;
        }
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);
        //过去两个物理帧后就能关闭碰撞了
        if (++_attackIndex >= 2)
        {
            _hitArea.Monitoring = false;
        }
    }

    protected override void OnBeginCharge()
    {
        //开始蓄力时武器角度
        RotationDegrees = -BeginChargeAngle;
    }

    protected override void OnFire()
    {
        Debug.Log("近战武器攻击! 蓄力时长: " + GetTriggerChargeTime() + ", 扳机按下时长: " + GetTriggerDownTime());
        //更新碰撞层级
        _hitArea.CollisionMask = AttackLayer | PhysicsLayer.Bullet;
        //启用碰撞
        _hitArea.Monitoring = true;
        _attackIndex = 0;

        if (IsActive) //被使用
        {
            //播放挥刀特效
            SpecialEffectManager.PlaySpriteFrames(
                Master,
                ResourcePath.resource_spriteFrames_weapon_Weapon0004_hit_tres, "default",
                Master.MountPoint.Position,
                Master.MountPoint.Rotation + Mathf.DegToRad(Attribute.UpliftAngle + 60),
                AnimatedSprite.Scale,
                new Vector2(17, 4), 1
            );
        }


        if (Master == World.Player)
        {
            var r = Master.MountPoint.RotationDegrees;
            //创建屏幕抖动
            if (Master.Face == FaceDirection.Right)
            {
                //GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(r - 90)) * 5);
                GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(r - 180)) * Attribute.CameraShake);
            }
            else
            {
                //GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(270 - r)) * 5);
                GameCamera.Main.DirectionalShake(Vector2.FromAngle(Mathf.DegToRad(-r)) * Attribute.CameraShake);
            }
        }
    }

    protected override void OnShoot(float fireRotation)
    {
        
    }

    protected override int UseAmmoCount()
    {
        //这里要做判断, 如果没有碰到敌人, 则不消耗弹药 (耐久)
        return 0;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is IHurt hurt)
        {
            HandlerCollision(hurt);
        }
        else if (body is Bullet bullet) //攻击子弹
        {
            var attackLayer = bullet.AttackLayer;
            if (TriggerRole != null && TriggerRole.CollisionWithMask(attackLayer)) //是攻击玩家的子弹
            {
                //反弹子弹
                bullet.OnPlayDisappearEffect();
                var scale = -1f;
                var weapon = bullet.BulletData.Weapon;
                if (weapon != null)
                {
                    scale /= weapon.AiUseAttribute.AiAttackAttr.BulletSpeedScale;
                }
                bullet.MoveController.ScaleAllVelocity(scale);
                bullet.Rotation += Mathf.Pi;
                bullet.RefreshBulletColor(false);
            }
        }
    }
    
    private void OnArea2dEntered(Area2D area)
    {
        if (area is IHurt hurt)
        {
            HandlerCollision(hurt);
        }
    }

    private void HandlerCollision(IHurt hurt)
    {
        var damage = Utils.Random.RandomConfigRange(Attribute.Bullet.HarmRange);
        //计算子弹造成的伤害
        if (TriggerRole != null)
        {
            damage = TriggerRole.RoleState.CalcDamage(damage);
        }
        //击退
        var attr = GetUseAttribute(TriggerRole);
        var repel = Utils.Random.RandomConfigRange(attr.Bullet.RepelRange);
        //计算击退
        if (TriggerRole != null)
        {
            repel = TriggerRole.RoleState.CalcBulletRepel(repel);
        }
        
        var globalPosition = GlobalPosition;
        if (repel != 0)
        {
            var o = hurt.GetActivityObject();
            if (o != null && o is not Player) //不是玩家才能被击退
            {
                Vector2 position;
                if (TriggerRole != null)
                {
                    position = o.GlobalPosition - TriggerRole.MountPoint.GlobalPosition;
                }
                else
                {
                    position = o.GlobalPosition - globalPosition;
                }
                var v2 = position.Normalized() * repel;
                o.AddRepelForce(v2);
            }
        }
        
        //造成伤害
        if (hurt.CanHurt(TriggerRole))
        {
            hurt.Hurt(TriggerRole, damage, (hurt.GetPosition() - globalPosition).Angle());
        }
    }
}
