using Godot;


/// <summary>
/// 普通的枪
/// </summary>
public class OrdinaryGun : Gun
{

    [Export] public PackedScene bulletPacked;

    protected override void Init()
    {

    }

    protected override void Fire()
    {
        
    }

    protected override void ShootBullet()
    {
        //创建子弹
        CreateBullet(bulletPacked, FirePoint.GlobalPosition, (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle());
    }
}