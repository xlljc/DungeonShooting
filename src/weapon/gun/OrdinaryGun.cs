using System;
using Godot;


/// <summary>
/// 普通的枪
/// </summary>
public class OrdinaryGun : Gun
{
    protected override void Init()
    {

    }

    protected override void OnFire()
    {
        //创建一个弹壳
        var temp = new ThrowShell();
        var startPos = GlobalPosition + new Vector2(0, 5);
        var startHeight = 6;
        var direction = GlobalRotationDegrees + MathUtils.RandRangeInt(-30, 30) + 180;
        var xf = MathUtils.RandRangeInt(20, 60);
        var yf = MathUtils.RandRangeInt(60, 120);
        var rotate = MathUtils.RandRangeInt(-720, 720);
        var sprite = Attribute.ShellPack.Instance<Sprite>();
        temp.InitThrow(new Vector2(5, 10), startPos, startHeight, direction, xf, yf, rotate, sprite, sprite);
        RoomManager.Current.SortRoot.AddChild(temp);
    }

    protected override void OnShootBullet()
    {
        //创建子弹
        CreateBullet(Attribute.BulletPack, FirePoint.GlobalPosition, (FirePoint.GlobalPosition - OriginPoint.GlobalPosition).Angle());
    }

    protected override void OnReload()
    {
        
    }

    protected override void OnPickUp(Role master)
    {
        
    }

    protected override void OnThrowOut()
    {

    }

    protected override void OnActive()
    {
        
    }

    protected override void OnConceal()
    {
        
    }

}