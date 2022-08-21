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
    /// <param name="assembly">数据集</param>
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
}
