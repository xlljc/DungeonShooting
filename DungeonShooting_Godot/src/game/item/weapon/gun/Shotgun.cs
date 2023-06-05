using Godot;

[Tool, GlobalClass]
public partial class Shotgun : Weapon
{

    private class ShotgunAttribute : WeaponAttribute
    {
        public ShotgunAttribute()
        {
            Name = "霰弹枪";
            Icon = ResourcePath.resource_sprite_gun_gun2_png;
            SpriteFrames = ResourcePath.resource_spriteFrames_Weapon0002_tres;
            Weight = 40;
            ThrowSpritePosition = new Vector2(0.4f, -2.6f);
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
            FirePosition = new Vector2(22, -4);
            //精灵位置
            SpritePosition = new Vector2(9, -2);
            ShellPosition = new Vector2(3, -4);
            
            BulletId = ActivityIdPrefix.Bullet + "0002";

            AiUseAttribute = Clone();
            AiUseAttribute.AiTargetLockingTime = 0.2f;
            AiUseAttribute.TriggerInterval = 3.5f;
        }
    }
    
    /// <summary>
    /// 弹壳预制体
    /// </summary>
    public PackedScene ShellPack;

    public override void OnInit()
    {
        base.OnInit();
        ShellPack = ResourceManager.Load<PackedScene>(ResourcePath.prefab_weapon_shell_ShellCase_tscn);
    }

    protected override void OnFire()
    {
        //创建一个弹壳
        ThrowShell("0001");
        
        if (Master == Player.Current)
        {
            //创建抖动
            GameCamera.Main.DirectionalShake(Vector2.Right.Rotated(GlobalRotation) * 2f);
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
        var bullet = ActivityObject.Create<Bullet>(Attribute.BulletId);
        bullet.Init(
            this,
            Utils.RandomRangeInt(280, 380),
            Utils.RandomRangeFloat(Attribute.MinDistance, Attribute.MaxDistance),
            FirePoint.GlobalPosition,
            fireRotation + Utils.RandomRangeFloat(-20 / 180f * Mathf.Pi, 20 / 180f * Mathf.Pi),
            GetAttackLayer()
        );
        bullet.PutDown(RoomLayerEnum.YSortLayer);
    }
}
