using Godot;

[RegisterWeapon(ActivityIdPrefix.Weapon + "0002", typeof(ShotgunAttribute))]
public partial class Shotgun : Weapon
{

    private class ShotgunAttribute : WeaponAttribute
    {
        public ShotgunAttribute()
        {
            Name = "霰弹枪";
            Sprite2D = ResourcePath.resource_sprite_gun_gun2_png;
            Weight = 40;
            CenterPosition = new Vector2(0.4f, -2.6f);
            StartFiringSpeed = 400;
            StartScatteringRange = 30;
            FinalScatteringRange = 90;
            ScatteringRangeAddValue = 50f;
            ScatteringRangeBackSpeed = 50;
            //连发
            ContinuousShoot = false;
            AmmoCapacity = 7;
            StandbyAmmoCapacity = 42;
            MaxAmmoCapacity = 42;
            AloneReload = true;
            AloneReloadCanShoot = true;
            ReloadTime = 0.6f;
            //连发数量
            MinContinuousCount = 1;
            MaxContinuousCount = 1;
            //开火前延时
            DelayedTime = 0f;
            //攻击距离
            MinDistance = 200;
            MaxDistance = 250;
            //发射子弹数量
            MinFireBulletCount = 5;
            MaxFireBulletCount = 5;
            //抬起角度
            UpliftAngle = 15;
            MaxBacklash = 6;
            MinBacklash = 5;
            //开火位置
            FirePosition = new Vector2(18, 4);

            AiUseAttribute = Clone();
            AiUseAttribute.AiTargetLockingTime = 0.2f;
            AiUseAttribute.TriggerInterval = 3.5f;
        }
    }
    
    /// <summary>
    /// 弹壳预制体
    /// </summary>
    public PackedScene ShellPack;

    public override void _Ready()
    {
        base._Ready();
        ShellPack = ResourceManager.Load<PackedScene>(ResourcePath.prefab_weapon_shell_ShellCase_tscn);
    }

    protected override void OnFire()
    {
        //创建一个弹壳
        var startPos = GlobalPosition + new Vector2(0, 5);
        var startHeight = 6;
        var direction = GlobalRotationDegrees + Utils.RandomRangeInt(-30, 30) + 180;
        var xf = Utils.RandomRangeInt(20, 60);
        var yf = Utils.RandomRangeInt(60, 120);
        var rotate = Utils.RandomRangeInt(-720, 720);
        var shell = ActivityObject.Create<ShellCase>(ActivityIdPrefix.Shell + "0001");;
        shell.Throw(new Vector2(5, 10), startPos, startHeight, direction, xf, yf, rotate, true);
        
        if (Master == GameApplication.Instance.RoomManager.Player)
        {
            //创建抖动
            GameCamera.Main.ProcessDirectionalShake(Vector2.Right.Rotated(GlobalRotation) * 2f);
        }
        
        //创建开火特效
        var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_ShotFire_tscn);
        var sprite = packedScene.Instantiate<Sprite2D>();
        sprite.GlobalPosition = FirePoint.GlobalPosition;
        sprite.GlobalRotation = FirePoint.GlobalRotation;
        sprite.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        
        //播放射击音效
        SoundManager.PlaySoundEffectPosition(ResourcePath.resource_sound_sfx_ordinaryBullet3_mp3, GameApplication.Instance.ViewToGlobalPosition(GlobalPosition), -15);
    }

    protected override void OnShoot(float fireRotation)
    {
        //创建子弹
        const string bulletId = ActivityIdPrefix.Bullet + "0001";
        var bullet = ActivityObject.Create<Bullet>(bulletId);
        bullet.Init(
            Utils.RandomRangeInt(280, 380),
            Utils.RandomRangeFloat(Attribute.MinDistance, Attribute.MaxDistance),
            FirePoint.GlobalPosition,
            fireRotation + Utils.RandomRangeFloat(-20 / 180f * Mathf.Pi, 20 / 180f * Mathf.Pi),
            GetAttackLayer()
        );
        bullet.PutDown(RoomLayerEnum.YSortLayer);
    }
}
