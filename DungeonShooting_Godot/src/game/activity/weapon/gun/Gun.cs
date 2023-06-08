using Godot;

/// <summary>
/// 普通的枪
/// </summary>
[Tool, GlobalClass]
public partial class Gun : Weapon
{
    protected override void OnReload()
    {
        //播放换弹音效
        if (!string.IsNullOrEmpty(Attribute.ReloadSound))
        {
            var position = GameApplication.Instance.ViewToGlobalPosition(GlobalPosition);
            if (Attribute.ReloadSoundDelayTime <= 0)
            {
                SoundManager.PlaySoundEffectPosition(Attribute.ReloadSound, position);
            }
            else
            {
                SoundManager.PlaySoundEffectPositionDelay(Attribute.ReloadSound, position, Attribute.ReloadSoundDelayTime);
            }
        }
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

        var position = GameApplication.Instance.ViewToGlobalPosition(GlobalPosition);
        //播放射击音效
        if (!string.IsNullOrEmpty(Attribute.ShootSound))
        {
            SoundManager.PlaySoundEffectPosition(Attribute.ShootSound, position, 0);
        }
        
        //播放上膛音效
        if (!string.IsNullOrEmpty(Attribute.EquipSound))
        {
            if (Attribute.EquipSoundDelayTime <= 0)
            {
                SoundManager.PlaySoundEffectPosition(Attribute.EquipSound, position);
            }
            else
            {
                SoundManager.PlaySoundEffectPositionDelay(Attribute.EquipSound, position, Attribute.EquipSoundDelayTime);
            }
        }
    }

    protected override void OnShoot(float fireRotation)
    {
        //创建子弹
        var bullet = ActivityObject.Create<Bullet>(Attribute.BulletId);
        bullet.Init(
            this,
            350,
            Utils.RandomRangeFloat(Attribute.MinDistance, Attribute.MaxDistance),
            FirePoint.GlobalPosition,
            fireRotation,
            GetAttackLayer()
        );
        bullet.PutDown(RoomLayerEnum.YSortLayer);
    }
}