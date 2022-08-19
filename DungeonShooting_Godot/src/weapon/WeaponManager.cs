using System.Reflection;
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 武器管理类, 负责武器注册和创建
/// </summary>
public static class WeaponManager
{

    private static Dictionary<string, Func<Weapon>> registerData = new Dictionary<string, Func<Weapon>>();

    /// <summary>
    /// 从一个指定的程序集中扫描并注册武器对象
    /// </summary>
    /// <param name="assembly">查询集</param>
    public static void RegisterWeaponFromAssembly(Assembly assembly)
    {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            //注册类
            Attribute[] attribute = Attribute.GetCustomAttributes(type, typeof(RegisterWeapon), false);
            if (attribute != null && attribute.Length > 0)
            {
                if (!typeof(Weapon).IsAssignableFrom(type))
                {
                    throw new Exception($"注册武器类'{type.FullName}'没有继承类'Weapon'!");
                }
                var atts = (RegisterWeapon[])attribute;
                foreach (var att in atts)
                {
                    //注册类
                    if (att.AttributeType == null) //没有指定属性类型
                    {
                        RegisterWeapon(att.Id, () =>
                        {
                            return Activator.CreateInstance(type, att.Id, new WeaponAttribute()) as Weapon;
                        });
                    }
                    else
                    {
                        if (!typeof(WeaponAttribute).IsAssignableFrom(att.AttributeType))
                        {
                            throw new Exception($"注册武器类'{type.FullName}'标注的特性中参数'AttributeType'类型没有继承'WeaponAttribute'!");
                        }
                        RegisterWeapon(att.Id, () =>
                        {
                            return Activator.CreateInstance(type, att.Id, Activator.CreateInstance(att.AttributeType)) as Weapon;
                        });
                    }
                }
            }

            //注册函数
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var method in methods)
            {
                Attribute mAttribute;
                //
                if ((mAttribute = Attribute.GetCustomAttribute(method, typeof(RegisterWeaponFunction), false)) != null)
                {
                    if (!typeof(Weapon).IsAssignableFrom(method.ReturnType)) //返回值类型不是 Weapon
                    {
                        throw new Exception($"注册武器函数'{method.DeclaringType.FullName}.{method.Name}'返回值类型不为'Weapon'!");
                    }
                    var args = method.GetParameters();
                    if (args == null || args.Length != 1 || args[0].ParameterType != typeof(string)) //参数类型不正确
                    {
                        throw new Exception($"注册武器函数'{method.DeclaringType.FullName}.{method.Name}'参数不满足(string id)类型");
                    }
                    var att = (RegisterWeaponFunction)mAttribute;
                    //注册函数
                    RegisterWeapon(att.Id, () =>
                    {
                        return method.Invoke(null, new object[] { att.Id }) as Weapon;
                    });
                }
            }
        }
    }

    /// <summary>
    /// 注册当个武器对象
    /// </summary>
    /// <param name="id">武器唯一id, 该id不能重复</param>
    /// <param name="callBack">获取武器时的回调函数, 函数返回武器对象</param>
    public static void RegisterWeapon(string id, Func<Weapon> callBack)
    {
        if (registerData.ContainsKey(id))
        {
            throw new Exception($"武器id: '{id} 已经被注册!'");
        }
        registerData.Add(id, callBack);
    }

    /// <summary>
    /// 根据武器唯一id获取
    /// </summary>
    /// <param name="id">武器id</param>
    public static Weapon GetGun(string id)
    {
        if (registerData.TryGetValue(id, out var callback))
        {
            return callback();
        }
        throw new Exception($"当前武器'{id}未被注册'");
    }

    /// <summary>
    /// 根据武器唯一id获取
    /// </summary>
    /// <param name="id">武器id</param>
    public static T GetGun<T>(string id) where T : Weapon
    {
        return (T)GetGun(id);
    }

    //----------------------------- 以下均为临时处理 -------------------------------
    /*
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
    */
}
