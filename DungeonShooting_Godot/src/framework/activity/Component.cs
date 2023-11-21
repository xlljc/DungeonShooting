
using System;
using System.Collections;
using Godot;

/// <summary>
/// 组件基类, 用于挂载到游戏物体上, 相比于原生 Node 更加轻量化, 实例化 Component 不会创建额外的 Node, 可以大量添加组件
/// </summary>
public abstract class Component<T> : Component where T : ActivityObject
{
    /// <summary>
    /// 当前组件所挂载的游戏对象
    /// </summary>
    public new T Master => (T)base.Master;
}

/// <summary>
/// 组件基类, 用于挂载到游戏物体上, 相比于原生 Node 更加轻量化, 实例化 Component 不会创建额外的 Node, 可以大量添加组件
/// </summary>
public abstract class Component : IProcess, IDestroy, ICoroutine
{
    /// <summary>
    /// 当前组件所挂载的游戏对象
    /// </summary>
    public ActivityObject Master { get; internal set; }

    /// <summary>
    /// 当前组件所挂载的物体的坐标
    /// </summary>
    public Vector2 Position
    {
        get => Master.Position;
        set => Master.Position = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的全局坐标
    /// </summary>
    public Vector2 GlobalPosition
    {
        get => Master.GlobalPosition;
        set => Master.GlobalPosition = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的缩放
    /// </summary>
    public Vector2 Scale
    {
        get => Master.Scale;
        set => Master.Scale = value;
    }
    
    /// <summary>
    /// 当前组件所挂载物体的全局缩放
    /// </summary>
    public Vector2 GlobalScale
    {
        get => Master.GlobalScale;
        set => Master.GlobalScale = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的旋转角度
    /// </summary>
    public float Rotation
    {
        get => Master.Rotation;
        set => Master.Rotation = value;
    }
    
    /// <summary>
    /// 当前组件所挂载物体的全局旋转角度
    /// </summary>
    public float GlobalRotation
    {
        get => Master.GlobalRotation;
        set => Master.GlobalRotation = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的角度制旋转角度
    /// </summary>
    public float RotationDegrees
    {
        get => Master.RotationDegrees;
        set => Master.RotationDegrees = value;
    }
    
    /// <summary>
    /// 当前组件所挂载物体的全局角度制旋转角度
    /// </summary>
    public float GlobalRotationDegrees
    {
        get => Master.GlobalRotationDegrees;
        set => Master.GlobalRotationDegrees = value;
    }
    
    /// <summary>
    /// 当前组件所挂载物体的ZIndex
    /// </summary>
    public int ZIndex
    {
        get => Master.ZIndex;
        set => Master.ZIndex = value;
    }
    
    /// <summary>
    /// 当前组件是否显示
    /// </summary>
    public bool Visible
    {
        get => Master.Visible;
        set => Master.Visible = value;
    }

    /// <summary>
    /// 挂载物体的动画节点
    /// </summary>
    public AnimatedSprite2D AnimatedSprite => Master.AnimatedSprite;
    /// <summary>
    /// 挂载物体的阴影节点
    /// </summary>
    public Sprite2D ShadowSprite => Master.ShadowSprite;
    /// <summary>
    /// 挂载物体的碰撞器节点
    /// </summary>
    public CollisionShape2D Collision => Master.Collision;
    /// <summary>
    /// 移动控制器
    /// </summary>
    public MoveController MoveController => Master.MoveController;

    /// <summary>
    /// 是否启用当前组件, 如果禁用, 则不会调用 Process 和 PhysicsProcess
    /// </summary>
    public bool Enable
    {
        get => _enable;
        set
        {
            if (!_enable && value)
            {
                _enable = true;
                OnEnable();
            }
            else if (_enable && !value)
            {
                _enable = false;
                OnDisable();
            }
        }
    }

    private bool _enable = true;

    /// <summary>
    /// 是否被销毁
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 初始化时调用
    /// </summary>
    public virtual void Ready()
    {
    }

    /// <summary>
    /// 如果启用了组件, 则每帧会调用一次 Process
    /// </summary>
    public virtual void Process(float delta)
    {
    }

    /// <summary>
    /// 如果启用了组件, 则每物理帧会调用一次 PhysicsProcess
    /// </summary>
    public virtual void PhysicsProcess(float delta)
    {
    }

    /// <summary>
    /// 当组件被销毁时调用
    /// </summary>
    public virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 当组件启用时调用
    /// </summary>
    public virtual void OnEnable()
    {
    }

    /// <summary>
    /// 当组件禁用时调用
    /// </summary>
    public virtual void OnDisable()
    {
    }

    /// <summary>
    /// 如果开启 debug, 则每帧调用该函数, 可用于绘制文字线段等, 需要调用 ActivityInstance 身上的绘制函数
    /// </summary>
    public virtual void DebugDraw()
    {
    }

    /// <summary>
    /// 当组件销毁
    /// </summary>
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        Master.RemoveComponent(this);
        OnDestroy();
    }

    public T AddComponent<T>() where T : Component, new()
    {
        return Master.AddComponent<T>();
    }
    
    public Component AddComponent(Type type)
    {
        return Master.AddComponent(type);
    }
    
    public T GetComponent<T>() where T : Component, new()
    {
        return Master.GetComponent<T>();
    }
    
    public Component GetComponent(Type type)
    {
        return Master.GetComponent(type);
    }

    public T[] GetComponents<T>() where T : Component, new()
    {
        return Master.GetComponents<T>();
    }
    
    public Component[] GetComponents(Type type)
    {
        return Master.GetComponents(type);
    }
    
    public void RemoveComponent(Component component)
    {
        Master.RemoveComponent(component);
    }
    
    public void AddChild(Node node)
    {
        Master.AddChild(node);
    }
    
    public void RemoveChild(Node node)
    {
        Master.RemoveChild(node);
    }
    
    public int GetChildCount()
    {
        return Master.GetChildCount();
    }
    
    public Node GetNode(NodePath path)
    {
        return Master.GetNode(path);
    }
    
    public T GetNode<T>(NodePath path) where T : class
    {
        return Master.GetNode<T>(path);
    }
    
    public Node GetNodeOrNull(NodePath path)
    {
        return Master.GetNodeOrNull(path);
    }
    
    public T GetNodeOrNull<T>(NodePath path) where T : class
    {
        return Master.GetNodeOrNull<T>(path);
    }
    
    public Node GetParent()
    {
        return Master.GetParent();
    }

    public T GetParent<T>() where T : class
    {
        return Master.GetParent<T>();
    }

    public void Reparent(Node node)
    {
        Master.Reparent(node);
    }

    public float GetProcessDeltaTime()
    {
        return (float)Master.GetProcessDeltaTime();
    }
    
    public float GetPhysicsProcessDeltaTime()
    {
        return (float)Master.GetPhysicsProcessDeltaTime();
    }
    
    public Vector2 GetGlobalMousePosition()
    {
        return Master.GetGlobalMousePosition();
    }
    
    public Vector2 GetLocalMousePosition()
    {
        return Master.GetLocalMousePosition();
    }
    
    public long StartCoroutine(IEnumerator able)
    {
        return Master.StartCoroutine(able);
    }

    public void StopCoroutine(long coroutineId)
    {
        Master.StopCoroutine(coroutineId);
    }

    public bool IsCoroutineOver(long coroutineId)
    {
        return Master.IsCoroutineOver(coroutineId);
    }

    public void StopAllCoroutine()
    {
        Master.StopAllCoroutine();
    }
}