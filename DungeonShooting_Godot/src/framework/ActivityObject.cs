
using System;
using System.Collections.Generic;
using Godot;
using Plugin;

/// <summary>
/// 房间内活动物体基类
/// </summary>
public abstract class ActivityObject : KinematicBody2D
{
    /// <summary>
    /// 当前物体显示的精灵图像, 节点名称必须叫 "AnimatedSprite", 类型为 AnimatedSprite
    /// </summary>
    public AnimatedSprite AnimatedSprite { get; }

    /// <summary>
    /// 当前物体显示的阴影图像, 节点名称必须叫 "ShadowSprite", 类型为 Sprite
    /// </summary>
    public Sprite ShadowSprite { get; }

    /// <summary>
    /// 当前物体碰撞器节点, 节点名称必须叫 "Collision", 类型为 CollisionShape2D
    /// </summary>
    public CollisionShape2D Collision { get; }


    /// <summary>
    /// 是否调用过 Destroy() 函数
    /// </summary>
    public bool IsDestroyed { get; private set; }

    private List<KeyValuePair<Type, Component>> _components = new List<KeyValuePair<Type, Component>>();

    public ActivityObject(string scenePath)
    {
        //加载预制体
        var tempPrefab = ResourceManager.Load<PackedScene>(scenePath);
        if (tempPrefab == null)
        {
            throw new Exception("创建 ActivityObject 的参数 scenePath 为 null !");
        }

        var tempNode = tempPrefab.Instance<ActivityObjectTemplate>();
        ZIndex = tempNode.ZIndex;
        CollisionLayer = tempNode.CollisionLayer;
        CollisionMask = tempNode.CollisionMask;

        //移动子节点
        var count = tempNode.GetChildCount();
        for (int i = 0; i < count; i++)
        {
            var body = tempNode.GetChild(0);
            tempNode.RemoveChild(body);
            AddChild(body);

            switch (body.Name)
            {
                case "AnimatedSprite":
                    AnimatedSprite = body as AnimatedSprite;
                    break;
                case "ShadowSprite":
                    ShadowSprite = body as Sprite;
                    break;
                case "Collision":
                    Collision = body as CollisionShape2D;
                    break;
            }
        }
    }

    /// <summary>
    /// 返回是否能与其他ActivityObject互动
    /// </summary>
    /// <param name="master">触发者</param>
    public abstract CheckInteractiveResult CheckInteractive(ActivityObject master);

    /// <summary>
    /// 与其它ActivityObject互动时调用
    /// </summary>
    /// <param name="master">触发者</param>
    public abstract void Interactive(ActivityObject master);

    public void AddComponent(Component component)
    {
        if (!ContainsComponent(component))
        {
            _components.Add(new KeyValuePair<Type, Component>(component.GetType(), component));
            component.OnMount();
        }
    }

    public void RemoveComponent(Component component)
    {
        if (ContainsComponent(component))
        {
            component.OnUnMount();
        }
    }

    public Component GetComponent(Type type)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var temp = _components[i];
            if (temp.Key == type)
            {
                return temp.Value;
            }
        }

        return null;
    }

    public TC GetComponent<TC>() where TC : Component
    {
        var component = GetComponent(typeof(TC));
        if (component == null) return null;
        return (TC)component;
    }

    public override void _Process(float delta)
    {
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            if (IsDestroyed) return;
            var temp = arr[i].Value;
            if (temp != null && temp.ActivityObject == this && temp.Enable)
            {
                temp._TriggerProcess(delta);
            }
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            if (IsDestroyed) return;
            var temp = arr[i].Value;
            if (temp != null && temp.ActivityObject == this && temp.Enable)
            {
                temp._TriggerPhysicsProcess(delta);
            }
        }
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Value?.Destroy();
        }
    }

    private bool ContainsComponent(Component component)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i].Value == component)
            {
                return true;
            }
        }

        return false;
    }
}