using System.Collections.Generic;
using Godot;

public static class WeaponManager
{

    public static void RegisterWeapon()
    {

    }

    public static Weapon GetGun(string id)
    {
        return null;
    }

    public static T GetGun<T>(string id) where T : Weapon
    {
        return (T)GetGun(id);
    }


    //----------------------------- 以下均为临时处理 -------------------------------

    public static Weapon GetGun1()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadWeaponAndInstance("res://prefab/weapon/Weapon.tscn");
        //设置基础属性
        var attr = new WeaponAttribute();
        attr.Id = "1";
        attr.Name = "Gun1";
        attr.Weight = 40;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.StartFiringSpeed = 480;
        attr.StartScatteringRange = 30;
        attr.FinalScatteringRange = 90;
        attr.ScatteringRangeAddValue = 2f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = true;
        //扳机检测间隔
        attr.TriggerInterval = 0f;
        //连发数量
        attr.MinContinuousCount = 3;
        attr.MaxContinuousCount = 3;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //抬起角度
        attr.UpliftAngle = 10;
        //枪身长度
        attr.FirePosition = new Vector2(16, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun4.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/OrdinaryBullets.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }

    public static Weapon GetGun2()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadWeaponAndInstance("res://prefab/weapon/Weapon.tscn");
        //设置基础属性
        var attr = new WeaponAttribute();
        attr.Id = "2";
        attr.Name = "Gun2";
        attr.Weight = 20;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.WeightType = WeaponWeightType.DeputyWeapon;
        attr.StartFiringSpeed = 600;
        attr.StartScatteringRange = 5;
        attr.FinalScatteringRange = 60;
        attr.ScatteringRangeAddValue = 8f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = false;
        //扳机检测间隔
        attr.TriggerInterval = 0.4f;
        //连发数量
        attr.MinContinuousCount = 3;
        attr.MaxContinuousCount = 3;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //抬起角度
        attr.UpliftAngle = 30;
        //枪身长度
        attr.FirePosition = new Vector2(10, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun3.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/HighSpeedBullet.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }

    public static Weapon GetGun3()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadWeaponAndInstance("res://prefab/weapon/gun/Shotgun.tscn");
        //设置基础属性
        var attr = new WeaponAttribute();
        attr.Id = "3";
        attr.Name = "Gun3";
        attr.Weight = 30;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.StartFiringSpeed = 180;
        attr.StartScatteringRange = 30;
        attr.FinalScatteringRange = 90;
        attr.ScatteringRangeAddValue = 2f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = false;
        //装弹时间
        attr.ReloadTime = 0.4f;
        //单独装弹
        attr.AloneReload = true;
        attr.AloneReloadCount = 1;
        attr.AloneReloadCanShoot = true;
        //扳机检测间隔
        attr.TriggerInterval = 0f;
        //连发数量
        attr.MinContinuousCount = 1;
        attr.MaxContinuousCount = 1;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 150;
        attr.MaxDistance = 200;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //抬起角度
        attr.UpliftAngle = 10;
        //枪身长度
        attr.FirePosition = new Vector2(16, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun2.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/OrdinaryBullets.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }

    public static Weapon GetGun4()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadWeaponAndInstance("res://prefab/weapon/Weapon.tscn");
        //设置基础属性
        var attr = new WeaponAttribute();
        attr.Id = "4";
        attr.Name = "Gun4";
        attr.Weight = 10;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.WeightType = WeaponWeightType.DeputyWeapon;
        attr.StartFiringSpeed = 600;
        attr.StartScatteringRange = 5;
        attr.FinalScatteringRange = 60;
        attr.ScatteringRangeAddValue = 8f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = false;
        //扳机检测间隔
        attr.TriggerInterval = 0.4f;
        //连发数量
        attr.MinContinuousCount = 3;
        attr.MaxContinuousCount = 3;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //抬起角度
        attr.UpliftAngle = 30;
        //枪身长度
        attr.FirePosition = new Vector2(10, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun7.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/HighSpeedBullet.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }

    public static Weapon GetGun5()
    {
        //加载枪的 prefab
        var gun = ResourceManager.LoadWeaponAndInstance("res://prefab/weapon/Weapon.tscn");
        //设置基础属性
        var attr = new WeaponAttribute();
        attr.Id = "5";
        attr.Name = "Gun5";
        attr.Weight = 10;
        attr.CenterPosition = new Vector2(0.4f, -2.6f);
        attr.WeightType = WeaponWeightType.DeputyWeapon;
        attr.StartFiringSpeed = 480;
        attr.StartScatteringRange = 5;
        attr.FinalScatteringRange = 30;
        attr.ScatteringRangeAddValue = 8f;
        attr.ScatteringRangeBackSpeed = 40;
        //连发
        attr.ContinuousShoot = true;
        //扳机检测间隔
        attr.TriggerInterval = 0.4f;
        //连发数量
        attr.MinContinuousCount = 1;
        attr.MaxContinuousCount = 1;
        //开火前延时
        attr.DelayedTime = 0f;
        //攻击距离
        attr.MinDistance = 500;
        attr.MaxDistance = 600;
        //发射子弹数量
        attr.MinFireBulletCount = 1;
        attr.MaxFireBulletCount = 1;
        //弹夹容量
        attr.AmmoCapacity = 120;
        attr.MaxAmmoCapacity = 360;
        //抬起角度
        attr.UpliftAngle = 30;
        //枪身长度
        attr.FirePosition = new Vector2(10, 1.5f);
        attr.Sprite = ResourceManager.Load<Texture>("res://resource/sprite/gun/gun5.png");
        attr.BulletPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/bullet/HighSpeedBullet.tscn");
        attr.ShellPack = ResourceManager.Load<PackedScene>("res://prefab/weapon/shell/ShellCase.tscn");
        gun.Init(attr);
        return gun;
    }
}
