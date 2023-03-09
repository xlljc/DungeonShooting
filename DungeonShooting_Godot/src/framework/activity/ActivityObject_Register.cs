
using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;

public partial class ActivityObject
{
    private static bool _initState = false;

    //物体注册数据
    private class RegisterActivityData
    {
        public RegisterActivityData(RegisterActivity registerActivity, Func<ActivityObject> callBack)
        {
            RegisterActivity = registerActivity;
            CallBack = callBack;
        }

        public RegisterActivity RegisterActivity;
        public Func<ActivityObject> CallBack;
    }
    
    //所有注册物体集合
    private static readonly Dictionary<string, RegisterActivityData> _activityRegisterMap = new();
    
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
        //扫描当前程序集
        ScannerFromAssembly(typeof(ActivityObject).Assembly);
    }
    
    /// <summary>
    /// 扫描指定程序集, 自动注册带有 RegisterActivity 特性的类
    /// </summary>
    public static void ScannerFromAssembly(Assembly assembly)
    {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            //注册类
            var attribute = Attribute.GetCustomAttributes(type, typeof(RegisterActivity), false);
            if (attribute.Length > 0)
            {
                if (!typeof(ActivityObject).IsAssignableFrom(type))
                {
                    //不是继承自 ActivityObject
                    throw new Exception($"The registered object '{type.FullName}' does not inherit the class ActivityObject.");
                }
                else if (type.IsAbstract)
                {
                    //不能加到抽象类上
                    throw new Exception($"'RegisterActivity' cannot be used on abstract class '{type.FullName}'.");
                }
                var attrs = (RegisterActivity[])attribute;
                foreach (var att in attrs)
                {
                    //注册操作
                    if (_activityRegisterMap.ContainsKey(att.Id))
                    {
                        throw new Exception($"Object ID: '{att.Id}' is already registered");
                    }
                    _activityRegisterMap.Add(att.Id, new RegisterActivityData(att, () =>
                    {
                        return (ActivityObject)Activator.CreateInstance(type);
                    }));
                }
            }
        }
    }

    /// <summary>
    /// 通过 ItemId 实例化 ActivityObject 对象
    /// </summary>
    public static ActivityObject Create(string itemId)
    {
        if (_activityRegisterMap.TryGetValue(itemId, out var item))
        {
            var instance = item.CallBack();
            instance._InitNode(item.RegisterActivity.Id, item.RegisterActivity.PrefabPath);
            item.RegisterActivity.CustomHandler(instance);
            return instance;
        }
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
        GD.PrintErr("创建实例失败, 未找到id为'" + itemId + "'的物体!");
        return null;
    }
}