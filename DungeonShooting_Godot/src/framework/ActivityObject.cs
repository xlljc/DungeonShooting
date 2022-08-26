
using System;
using Godot;

/// <summary>
/// 房间内活动物体基类
/// </summary>
public abstract class ActivityObject<T> : ActivityObject where T : ActivityObject
{
    public ActivityObject()
    {
        ComponentControl = (ComponentControl<T>)Activator.CreateInstance(typeof(ComponentControl<T>), this);
        Sprite = GetNodeOrNull<Sprite>("Sprite");
        if (Sprite == null)
        {
            GD.PrintErr("ActivityObject节点下必须要有一个'Sprite'节点!");
        }
        Collision = GetNodeOrNull<CollisionShape2D>("Collision");
        if (Collision == null)
        {
            GD.PrintErr("ActivityObject节点下必须要有一个'Collision'节点!");
        }
    }

    /// <summary>
    /// 组件管理器
    /// </summary>
    public new ComponentControl<T> ComponentControl { get; }
}

/// <summary>
/// 房间内活动物体基类
/// </summary>
public abstract class ActivityObject : KinematicBody2D
{
    
    /// <summary>
    /// 组件管理器
    /// </summary>
    public ComponentControl<ActivityObject> ComponentControl { get; }

    /// <summary>
    /// 当前物体显示的精灵图像, 节点名称必须叫 "Sprite"
    /// </summary>
    public Sprite Sprite { get; protected set; }
    
    /// <summary>
    /// 当前物体碰撞器节点, 节点名称必须叫 "Collision"
    /// </summary>
    public CollisionShape2D Collision { get; protected set; }
    
    /// <summary>
    /// 返回是否能与其他ActivityObject互动
    /// </summary>
    /// <param name="master">触发者</param>
    public abstract CheckInteractiveResult CheckInteractive<TU>(ActivityObject<TU> master) where TU : ActivityObject;

    /// <summary>
    /// 与其它ActivityObject互动时调用
    /// </summary>
    /// <param name="master">触发者</param>
    public abstract void Interactive<TU>(ActivityObject<TU> master) where TU : ActivityObject;
}