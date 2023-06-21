using Godot;

/// <summary>
/// 普通的枪
/// </summary>
[Tool, GlobalClass]
public partial class Gun : Weapon
{
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
    }

    protected override void OnShoot(float fireRotation)
    {
        ShootBullet(fireRotation, Attribute.BulletId);
    }
}