using Godot;

/// <summary>
/// 普通的枪
/// </summary>
[Tool]
public partial class Gun : Weapon
{
    protected override void OnFire()
    {
        if (Master == World.Player)
        {
            //创建抖动
            GameCamera.Main.DirectionalShake(Vector2.Right.Rotated(GlobalRotation) * Attribute.CameraShake);
        }

        //创建开火特效
        if (!string.IsNullOrEmpty(Attribute.FireEffect))
        {
            var effect = ObjectManager.GetPoolItem<IEffect>(Attribute.FireEffect);
            var sprite = (Node2D)effect;
            sprite.Position = GetLocalFirePosition();
            AddChild(sprite);
            effect.PlayEffect();
        }
    }

    protected override void OnShoot(float fireRotation)
    {
        FireManager.ShootBullet(this, fireRotation, Attribute.Bullet);
    }
}