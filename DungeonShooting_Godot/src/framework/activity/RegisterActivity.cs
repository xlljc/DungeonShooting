
using System;

/// <summary>
/// 用在 ActivityObject 子类上, 用于注册游戏中的物体, 一个类可以添加多个 [RegisterActivity] 特性, ActivityObject 会自动扫描并注册物体
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterActivity : Attribute
{
    /// <summary>
    /// 注册物体唯一ID， 该ID不能有重复
    /// </summary>
    public string ItemId { get; protected set; }

    /// <summary>
    /// 模板 Prefab 的路径
    /// </summary>
    public string PrefabPath { get; protected set; }

    public RegisterActivity(string itemId, string prefabPath)
    {
        ItemId = itemId;
        PrefabPath = prefabPath;
    }

    /// <summary>
    /// 该函数在物体实例化后调用, 可用于一些自定义操作, 参数为实例对象
    /// </summary>
    public virtual void CustomHandler(ActivityObject instance)
    {
    }
}