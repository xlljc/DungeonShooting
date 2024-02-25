
using System;
using Config;

public partial class ActivityObject
{
    /// <summary>
    /// 通过 ActivityBase 实例化 ActivityObject 对象
    /// </summary>
    public static ActivityObject Create(ExcelConfig.ActivityBase config)
    {
        var world = World.Current;
        if (world == null)
        {
            throw new Exception("实例化 ActivityObject 前请先调用 'GameApplication.Instance.DungeonManager.CreateNewWorld()' 初始化 World 对象");
        }
        var instance = ResourceManager.LoadAndInstantiate<ActivityObject>(config.Prefab);
        instance._InitNode(config, world);
        return instance;
    }

    /// <summary>
    /// 通过 ActivityBase 实例化 ActivityObject 对象
    /// </summary>
    public static T Create<T>(ExcelConfig.ActivityBase config) where T : ActivityObject
    {
        return (T)Create(config);
    }
    
    /// <summary>
    /// 通过 ItemId 实例化 ActivityObject 对象
    /// </summary>
    public static ActivityObject Create(string itemId)
    {
        if (ExcelConfig.ActivityBase_Map.TryGetValue(itemId, out var config))
        {
            return Create(config);
        }
        Debug.LogError("创建实例失败, 未找到id为'" + itemId + "'的物体!");
        return null;
    }

    /// <summary>
    /// 通过 ItemId 实例化 ActivityObject 对象
    /// </summary>
    public static T Create<T>(string itemId) where T : ActivityObject
    {
        var instance = Create(itemId);
        if (instance != null)
        {
            return (T)instance;
        }
        return null;
    }
}