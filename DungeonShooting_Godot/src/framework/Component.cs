
using Godot;

/// <summary>
/// 组件基类
/// </summary>
public abstract class Component : IProcess, IDestroy
{
    public ActivityObject ActivityObject { get; private set; }

    public Vector2 Position
    {
        get => ActivityObject.Position;
        set => ActivityObject.Position = value;
    }

    public Vector2 GlobalPosition
    {
        get => ActivityObject.GlobalPosition;
        set => ActivityObject.GlobalPosition = value;
    }

    public bool Visible
    {
        get => ActivityObject.Visible;
        set => ActivityObject.Visible = value;
    }

    public AnimatedSprite AnimatedSprite => ActivityObject.AnimatedSprite;
    public Sprite ShadowSprite => ActivityObject.ShadowSprite;
    public CollisionShape2D Collision => ActivityObject.Collision;

    public bool Enable { get; set; } = true;

    public bool IsDestroyed { get; private set; }

    private bool _isReady = false;

    public Component()
    {
    }

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

    /// <summary>
    /// 当组件销毁
    /// </summary>
    public new void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        if (ActivityObject != null)
        {
            ActivityObject.RemoveComponent(this);
        }

        OnDestroy();
    }

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

    internal void _SetActivityObject(ActivityObject activityObject)
    {
        ActivityObject = activityObject;
    }
}