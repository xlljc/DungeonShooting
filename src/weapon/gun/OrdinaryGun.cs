using Godot;

/// <summary>
/// 普通的枪
/// </summary>
public class OrdinaryGun : Gun
{
    //子弹
    private PackedScene bulletPacked;


    protected override void Init()
    {
        //子弹
        bulletPacked = ResourceLoader.Load<PackedScene>("res://prefab/Bullet.tscn");
    }

    protected override void Fire()
    {
        
    }

    protected override void ShootBullet()
    {
        //创建子弹
        var bullet = CreateBullet<HighSpeedBullet>(bulletPacked);
        //位置
        bullet.GlobalPosition = FirePoint.GlobalPosition;
        //角度
        bullet.Rotation = (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle();
        GetTree().CurrentScene.AddChild(bullet);
        //飞行距离
        var distance = MathUtils.RandRange(Attribute.MinDistance, Attribute.MaxDistance);
        //初始化子弹数据
        bullet.InitData(distance, Colors.White);
    }

    protected T CreateBullet<T>(PackedScene bulletPack) where T : Bullet
    {
        T bullet = bulletPack.Instance<T>();
        bullet.Init(TargetCamp, this, null);
        return bullet;
    }
}