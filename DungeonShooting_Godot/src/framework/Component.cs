
using Godot;

/// <summary>
/// 组件基类
/// </summary>
public abstract class Component<TG> : Component where TG : KinematicBody2D
{
    public ComponentControl<TG> ComponentControl { get; private set; }

    public Vector2 Position
    {
        get => ComponentControl.Position;
        set => ComponentControl.Position = value;
    }

    public Vector2 GlobalPosition
    {
        get => ComponentControl.GlobalPosition;
        set => ComponentControl.GlobalPosition = value;
    }
    
    public bool Visible
    {
        get => ComponentControl.Visible;
        set => ComponentControl.Visible = value;
    }

    public bool Enable { get; set; } = true;

    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 当组件销毁
    /// </summary>
    public override void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        if (ComponentControl != null)
        {
            ComponentControl.RemoveComponent(this);
        }

        OnDestroy();
    }
    
    internal void SetGameObject(ComponentControl<TG> componentControl)
    {
        ComponentControl = componentControl;
    }
}

/// <summary>
/// 组件基类
/// </summary>
public abstract class Component : IProcess, IDestroy
{
    /// <summary>
    /// 该组件所绑定的GameObject的坐标
    /// </summary>
    Vector2 Position { get; set; }

    /// <summary>
    /// 该组件所绑定的GameObject的全局坐标
    /// </summary>
    Vector2 GlobalPosition { get; set; }

    /// <summary>
    /// 该组件所绑定的GameObject的显示状态
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// 是否启用该组件, 如果停用, 则不会调用 Process 和 PhysicsProcess
    /// </summary>
    bool Enable { get; set; }

    public bool IsDestroyed { get; }
    
    private bool _isReady = false;
    
    /// <summary>
    /// 第一次调用 Process 或 PhysicsProcess 之前调用
    /// </summary>
    public virtual void Ready()
    {
    }

    public virtual void Process(float delta)
    {
    }

    public virtual void PhysicsProcess(float delta)
    {
    }

    public virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 当该组件挂载到GameObject上时调用
    /// </summary>
    public virtual void OnMount()
    {
    }
    
    /// <summary>
    /// 当该组件被取消挂载时调用
    /// </summary>
    public virtual void OnUnMount()
    {
    }
    
    public abstract void Destroy();

    internal void _TriggerProcess(float delta)
    {
        if (!_isReady)
        {
            _isReady = true;
            Ready();
        }

        Process(delta);
    }

    internal void _TriggerPhysicsProcess(float delta)
    {
        if (!_isReady)
        {
            _isReady = true;
            Ready();
        }

        PhysicsProcess(delta);
    }
}