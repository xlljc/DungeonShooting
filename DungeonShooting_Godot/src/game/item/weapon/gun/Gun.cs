using Godot;

/// <summary>
/// 普通的枪
/// </summary>
[RegisterWeapon("1001", typeof(Gun.RifleAttribute))]
[RegisterWeapon("1003", typeof(Gun.PistolAttribute))]
public class Gun : Weapon
{
    //步枪属性数据
    private class RifleAttribute : WeaponAttribute
    {
        public RifleAttribute()
        {
            Name = "步枪";
            Sprite = ResourcePath.resource_sprite_gun_gun4_png;
            Weight = 40;
            CenterPosition = new Vector2(0.4f, -2.6f);
            StartFiringSpeed = 480;
            StartScatteringRange = 30;
            FinalScatteringRange = 90;
            ScatteringRangeAddValue = 2f;
            ScatteringRangeBackSpeed = 40;
            //连发
            ContinuousShoot = true;
            AmmoCapacity = 30;
            StandbyAmmoCapacity = 30 * 70;
            MaxAmmoCapacity = 30 * 70;
            //扳机检测间隔
            TriggerInterval = 0f;
            //连发数量
            MinContinuousCount = 3;
            MaxContinuousCount = 3;
            //开火前延时
            DelayedTime = 0f;
            //攻击距离
            MinDistance = 300;
            MaxDistance = 400;
            //发射子弹数量
            MinFireBulletCount = 1;
            MaxFireBulletCount = 1;
            //抬起角度
            UpliftAngle = 10;
            //开火位置
            FirePosition = new Vector2(16, 2);
        }
    }

    //手枪属性数据
    private class PistolAttribute : WeaponAttribute
    {
        public PistolAttribute()
        {
            Name = "手枪";
            Sprite = ResourcePath.resource_sprite_gun_gun3_png;
            Weight = 20;
            CenterPosition = new Vector2(0.4f, -2.6f);
            WeightType = WeaponWeightType.DeputyWeapon;
            StartFiringSpeed = 300;
            StartScatteringRange = 5;
            FinalScatteringRange = 60;
            ScatteringRangeAddValue = 8f;
            ScatteringRangeBackSpeed = 40;
            //连发
            ContinuousShoot = false;
            AmmoCapacity = 12;
            StandbyAmmoCapacity = 72;
            MaxAmmoCapacity = 72;
            //扳机检测间隔
            TriggerInterval = 0.1f;
            //连发数量
            MinContinuousCount = 1;
            MaxContinuousCount = 1;
            //开火前延时
            DelayedTime = 0f;
            //攻击距离
            MinDistance = 250;
            MaxDistance = 300;
            //发射子弹数量
            MinFireBulletCount = 1;
            MaxFireBulletCount = 1;
            //抬起角度
            UpliftAngle = 30;
            //开火位置
            FirePosition = new Vector2(10, 2);
        }
    }

    public Gun(string id, WeaponAttribute attribute): base(id, attribute)
    {
    }

    protected override void OnFire()
    {
        //创建一个弹壳
        var startPos = GlobalPosition + new Vector2(0, 5);
        var startHeight = 6;
        var direction = GlobalRotationDegrees + Utils.RandRangeInt(-30, 30) + 180;
        var xf = Utils.RandRangeInt(20, 60);
        var yf = Utils.RandRangeInt(60, 120);
        var rotate = Utils.RandRangeInt(-720, 720);
        var shell = new ShellCase();
        shell.Throw(new Vector2(10, 5), startPos, startHeight, direction, xf, yf, rotate, true);
        
        if (Master == GameApplication.Instance.Room.Player)
        {
            //创建抖动
            GameCamera.Main.ProcessDirectionalShake(Vector2.Right.Rotated(GlobalRotation) * 2f);
        }

        //创建开火特效
        var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_ShotFire_tscn);
        var sprite = packedScene.Instance<Sprite>();
        sprite.GlobalPosition = FirePoint.GlobalPosition;
        sprite.GlobalRotation = FirePoint.GlobalRotation;
        GameApplication.Instance.Room.GetRoot(true).AddChild(sprite);

        //播放射击音效
        SoundManager.PlaySoundEffectPosition(ResourcePath.resource_sound_sfx_ordinaryBullet2_mp3, GameApplication.Instance.ViewToGlobalPosition(GlobalPosition), -8);
    }

    protected override void OnShoot(float fireRotation)
    {
        //创建子弹
        //CreateBullet(BulletPack, FirePoint.GlobalPosition, fireRotation);
        var bullet = new Bullet(
            ResourcePath.prefab_weapon_bullet_Bullet_tscn,
            350,
            Utils.RandRange(Attribute.MinDistance, Attribute.MaxDistance),
            FirePoint.GlobalPosition,
            fireRotation,
            GetAttackLayer()
        );
        bullet.PutDown();
    }
}