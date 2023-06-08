using Godot;

[Tool, GlobalClass]
public partial class Shotgun : Weapon
{
    protected override void OnReload()
    {
        SoundManager.PlaySoundEffectPosition(ResourcePath.resource_sound_sfx_Reloading0003_ogg, GameApplication.Instance.ViewToGlobalPosition(GlobalPosition));
    }

    protected override void OnFire()
    {
        //创建一个弹壳
        ThrowShell(ActivityObject.Ids.Id_shell0001);
        
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
        SoundManager.PlaySoundEffectPosition(ResourcePath.resource_sound_sfx_Shooting0003_mp3, GameApplication.Instance.ViewToGlobalPosition(GlobalPosition), -15);
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
