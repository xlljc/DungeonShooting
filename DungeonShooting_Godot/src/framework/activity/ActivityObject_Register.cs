
using System;
using System.Collections.Generic;
using Config;
using Godot;

public partial class ActivityObject
{
    private class RegisterActivityData
    {
        public RegisterActivityData(string path, ExcelConfig.ActivityObject config)
        {
            Path = path;
            Config = config;
        }

        public string Path;
        public ExcelConfig.ActivityObject Config;
    }
    
    //负责存放所有注册对象数据
    private static Dictionary<string, RegisterActivityData> _activityRegisterMap = new Dictionary<string, RegisterActivityData>();
    private static bool _initState = false;

    /// <summary>
    /// 初始化调用, 开始扫描当前程序集, 并自动注册 ActivityObject 物体
    /// </summary>
    public static void InitActivity()
    {
        if (_initState)
        {
            return;
        }

        _initState = true;
        _InitRegister();
    }

    /// <summary>
    /// 通过 ItemId 实例化 ActivityObject 对象
    /// </summary>
    public static ActivityObject Create(string itemId)
    {
        var world = GameApplication.Instance.World;
        if (world == null)
        {
            throw new Exception("实例化 ActivityObject 前请先调用 'GameApplication.Instance.CreateNewWorld()' 初始化 World 对象");
        }

        if (_activityRegisterMap.TryGetValue(itemId, out var config))
        {
            var instance = ResourceManager.LoadAndInstantiate<ActivityObject>(config.Path);
            instance._InitNode(config, world);
            return instance;
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