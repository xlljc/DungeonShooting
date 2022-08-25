
using Godot;

/// <summary>
/// 房间内活动物体基类
/// </summary>
public abstract class ActivityObject<T> : ActivityObject where T : KinematicBody2D
{
    public ActivityObject()
    {
        ComponentControl = CreateComponentControl();
    }
    
    /// <summary>
    /// 组件管理器
    /// </summary>
    public ComponentControl<T> ComponentControl { get; }

    public abstract ComponentControl<T> CreateComponentControl();
}

/// <summary>
/// 房间内活动物体基类
/// </summary>
public abstract class ActivityObject : KinematicBody2D
{
    /// <summary>
    /// 返回是否能与其他ActivityObject互动
    /// </summary>
    /// <param name="master">触发者</param>
    public abstract CheckInteractiveResult CheckInteractive<TU>(ActivityObject<TU> master) where TU : KinematicBody2D;

    /// <summary>
    /// 与其它ActivityObject互动时调用
    /// </summary>
    /// <param name="master">触发者</param>
    public abstract void Interactive<TU>(ActivityObject<TU> master) where TU : KinematicBody2D;
}