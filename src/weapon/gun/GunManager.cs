using System.Collections.Generic;
using Godot;

public static class GunManager
{

    private static readonly Dictionary<string, PackedScene> CacheGunPack = new Dictionary<string, PackedScene>();

    /// <summary>
    /// 加载武器预制体,
    /// 路径位于res://prefab/weapon/下
    /// </summary>
    /// <param name="path">资源路径</param>
    public static Gun LoadGun(string path)
    {
        path = "res://prefab/weapon/" + path;
        if (CacheGunPack.TryGetValue(path, out var pack))
        {
            return pack.Instance<Gun>();
        }
        else
        {
            pack = ResourceLoader.Load<PackedScene>(path);
            if (pack != null)
            {
                CacheGunPack.Add(path, pack);
                return pack.Instance<Gun>();
            }
        }
        return null;
    }

    public static Gun GetGun1()
    {
        //加载枪的 prefab
        var gun = LoadGun("Gun.tscn");
        //设置基础属性
        var attr = new GunAttribute();
        attr.StartFiringSpeed = 480;
        attr.StartScatteringRange = 5;
        attr.FinalScatteringRange = 60;
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
        attr.Sprite = "res://resource/sprite/gun/gun4.png";
        gun.Init(attr);
        return gun;
    }
}
