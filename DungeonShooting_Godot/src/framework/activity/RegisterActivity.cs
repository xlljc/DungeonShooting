
using System;
using System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterActivity : Attribute
{
    /// <summary>
    /// 注册物体唯一ID， 该ID不能有重复
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// 模板Prefab的路径
    /// </summary>
    public string PrefabPath { get; protected set; }

    public RegisterActivity(string id, string prefabPath)
    {
        Id = id;
        PrefabPath = prefabPath;
    }


    public virtual Func<ActivityObject> RegisterInstantiationCallback(Type type)
    {
        return () => { return (ActivityObject)Activator.CreateInstance(type); };
    }
}