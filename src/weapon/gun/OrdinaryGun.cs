using Godot;


/// <summary>
/// 普通的枪
/// </summary>
public class OrdinaryGun : Gun
{

    [Export] public PackedScene bulletPacked;

    [Export] public PackedScene shell;

    protected override void Init()
    {

    }

    protected override void Fire()
    {
        //创建一个弹壳
        var temp = new ThrowNode();
        temp.InitThrow(GlobalPosition, 30, new Vector2(30, -100), shell.Instance<Node2D>());
        RoomManager.Current.ItemRoot.AddChild(temp);
    }

    protected override void ShootBullet()
    {
        //创建子弹
        CreateBullet(bulletPacked, FirePoint.GlobalPosition, (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle());
    }
}