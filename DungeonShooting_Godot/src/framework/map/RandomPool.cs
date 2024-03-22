
using System.Collections.Generic;
using Config;
using Godot;

public class RandomPool
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public SeedRandom Random { get; }
    
    /// <summary>
    /// 所属世界
    /// </summary>
    public World World { get; }
    
    public RandomPool(World world)
    {
        World = world;
        Random = world.Random;
    }

    /// <summary>
    /// 获取随机武器
    /// </summary>
    public ExcelConfig.ActivityBase GetRandomWeapon()
    {
        return Random.RandomChoose(PreinstallMarkManager.GetMarkConfigsByType(ActivityType.Weapon));
    }

    /// <summary>
    /// 获取随机敌人
    /// </summary>
    public ExcelConfig.ActivityBase GetRandomEnemy()
    {
        return Random.RandomChoose(PreinstallMarkManager.GetMarkConfigsByType(ActivityType.Enemy));
    }

    /// <summary>
    /// 获取随机道具
    /// </summary>
    public ExcelConfig.ActivityBase GetRandomProp()
    {
        return Random.RandomChoose(PreinstallMarkManager.GetMarkConfigsByType(ActivityType.Prop));
    }

    /// <summary>
    /// 填充自动波次数据
    /// </summary>
    public void FillAutoWave(RoomPreinstall preinstall)
    {
        if (preinstall.RoomInfo.RoomType == DungeonRoomType.Battle)
        {
            FillBattleRoom(preinstall);
        }
    }

    //填充战斗房间
    private void FillBattleRoom(RoomPreinstall preinstall)
    {
        var count = World.Random.RandomRangeInt(3, 10);
        var tileInfo = preinstall.RoomInfo.RoomSplit.TileInfo;
        var serializeVector2s = tileInfo.NavigationVertices;
        var vertices = new List<Vector2>();
        foreach (var sv2 in serializeVector2s)
        {
            vertices.Add(sv2.AsVector2());
        }
        var positionArray = World.Random.GetRandomPositionInPolygon(vertices, tileInfo.NavigationPolygon, count);
        var arr = new ActivityType[] { ActivityType.Enemy, ActivityType.Weapon, ActivityType.Prop };
        var weight = new int[] { 15, 2, 1 };
        for (var i = 0; i < count; i++)
        {
            var tempWave = preinstall.GetOrCreateWave(World.Random.RandomRangeInt(0, 2));
            var index = World.Random.RandomWeight(weight);
            var activityType = arr[index];
    
            //创建标记
            var mark = ActivityMark.CreateMark(activityType, i * 0.3f, preinstall.RoomInfo.ToGlobalPosition(positionArray[i]));
            
            if (activityType == ActivityType.Enemy) //敌人
            {
                mark.Id = GetRandomEnemy().Id;
                mark.Attr.Add("Face", "0");
                mark.DerivedAttr = new Dictionary<string, string>();
                mark.DerivedAttr.Add("Face", World.Random.RandomChoose((int)FaceDirection.Left, (int)FaceDirection.Right).ToString()); //链朝向
                if (World.Random.RandomBoolean(0.8f)) //手持武器
                {
                    var weapon = GetRandomWeapon();
                    var weaponAttribute = Weapon.GetWeaponAttribute(weapon.Id);
                    mark.Attr.Add("Weapon", weapon.Id); //武器id
                    mark.Attr.Add("CurrAmmon", weaponAttribute.AmmoCapacity.ToString()); //弹夹弹药量
                    mark.Attr.Add("ResidueAmmo", weaponAttribute.AmmoCapacity.ToString()); //剩余弹药量
                }
            }
            else if (activityType == ActivityType.Weapon) //武器
            {
                mark.Id = GetRandomWeapon().Id;
            }
            else if (activityType == ActivityType.Prop) //道具
            {
                mark.Id = GetRandomProp().Id;
            }
            tempWave.Add(mark);
        }
    }
}