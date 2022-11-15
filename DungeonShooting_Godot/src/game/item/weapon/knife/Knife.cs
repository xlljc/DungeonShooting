
using Godot;

[RegisterWeapon("1004", typeof(KnifeAttribute))]
public class Knife : Weapon
{
    private class KnifeAttribute : WeaponAttribute
    {
        public KnifeAttribute()
        {
            Sprite = ResourcePath.resource_sprite_gun_knife1_png;
            WeaponPrefab = ResourcePath.prefab_weapon_Knife_tscn;
            //攻速设置
            StartFiringSpeed = 180;
            FinalFiringSpeed = StartFiringSpeed;
            //关闭连发
            ContinuousShoot = false;
            //设置成松发开火
            LooseShoot = true;
            //弹药量, 可以理解为耐久度
            AmmoCapacity = 180;
            MaxAmmoCapacity = AmmoCapacity;
            //握把位置
            HoldPosition = new Vector2(10, 0);
            //后坐力改为向前, 模拟手伸长的效果
            MaxBacklash = -8;
            MinBacklash = -8;
            BacklashRegressionSpeed = 24;
            UpliftAngle = -95;
        }
    }

    private Area2D _hitArea;

    private int _attackIndex = 0;
    
    public Knife(string id, WeaponAttribute attribute) : base(id, attribute)
    {
        _hitArea = GetNode<Area2D>("HitArea");
        _hitArea.Monitoring = false;
        _hitArea.Monitorable = false;

        _hitArea.Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        
        if (IsActive)
        {
            //让碰撞节点与武器挂载节点位置保持一致, 而不跟着武器走
            _hitArea.GlobalPosition = Master.MountPoint.GlobalPosition;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        //过去两个物理帧后就能关闭碰撞了
        if (++_attackIndex >= 2)
        {
            _hitArea.Monitoring = false;
        }
    }

    protected override void OnStartCharge()
    {
        //开始蓄力时武器角度上抬120度
        RotationDegrees = -120;
    }

    protected override void OnFire()
    {
        GD.Print("近战武器攻击! 蓄力时长: " + GetTriggerChargeTime() + ", 扳机按下时长: " + GetTriggerDownTime());
        //更新碰撞层级
        _hitArea.CollisionMask = GetAttackLayer();
        //启用碰撞
        _hitArea.Monitoring = true;
        _attackIndex = 0;

        if (IsActive) //被使用
        {
            //播放挥刀特效
            SpecialEffectManager.Play(
                ResourcePath.resource_effects_KnifeHit1_tres, "default",
                Master.MountPoint.GlobalPosition, GlobalRotation + Mathf.Pi * 0.5f, new Vector2((int)Master.Face, 1),
                new Vector2(17, 4), 1
            );
        }


        if (Master == GameApplication.Instance.Room.Player)
        {
            //创建抖动
            //GameCamera.Main.ProcessDirectionalShake(Vector2.Right.Rotated(GlobalRotation - Mathf.Pi * 0.5f) * 1.5f);
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
        GD.Print("碰到物体: " + body.Name);
        var activityObject = body.AsActivityObject();
        if (activityObject != null)
        {
            if (activityObject is Role role)
            {
                role.Hurt(1);
            }
        }
    }
}
