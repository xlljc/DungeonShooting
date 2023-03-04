
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class ActivityObject
{
    private static bool _initState = false;

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
    
    private static readonly Dictionary<string, RegisterActivityData> _activityRegisterMap = new();
    
    public static void Init()
    {
        if (_initState)
        {
            return;
        }

        _initState = true;
        
        ScannerFromAssembly(typeof(ActivityObject).Assembly);
    }
    
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
                    throw new Exception($"The registered object '{type.FullName}' does not inherit the class ActivityObject.");
                }
                else if (type.IsAbstract)
                {
                    throw new Exception($"'RegisterActivity' cannot be used on abstract class '{type.FullName}'.");
                }
                var attrs = (RegisterActivity[])attribute;
                foreach (var att in attrs)
                {
                    if (_activityRegisterMap.ContainsKey(att.Id))
                    {
                        throw new Exception($"Object ID: '{att.Id}' is already registered");
                    }
                    var registerCallback = att.RegisterInstantiationCallback(type);
                    _activityRegisterMap.Add(att.Id, new RegisterActivityData(att, registerCallback));
                }
            }
        }
    }

    /// <summary>
    /// 通过 ItemId 实例化 ActivityObject 对象
    /// </summary>
    public static T Create<T>(string itemId) where T : ActivityObject
    {
        if (_activityRegisterMap.TryGetValue(itemId, out var item))
        {
            var instance = item.CallBack();
            instance._InitNode(item.RegisterActivity.PrefabPath);
        }
        return null;
    }
}