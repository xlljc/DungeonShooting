
using Godot;

/// <summary>
/// 组件基类, 用于挂载到游戏物体上, 相比于原生 Node 更加轻量化, 可以大量添加组件
/// </summary>
public abstract class Component : IProcess, IDestroy
{
    /// <summary>
    /// 当前组件所挂载的游戏对象
    /// </summary>
    public ActivityObject ActivityObject { get; private set; }

    /// <summary>
    /// 当前组件所挂载的物体的坐标
    /// </summary>
    public Vector2 Position
    {
        get => ActivityObject.Position;
        set => ActivityObject.Position = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的全局坐标
    /// </summary>
    public Vector2 GlobalPosition
    {
        get => ActivityObject.GlobalPosition;
        set => ActivityObject.GlobalPosition = value;
    }

    /// <summary>
    /// 当前组件是否显示
    /// </summary>
    public bool Visible
    {
        get => ActivityObject.Visible;
        set => ActivityObject.Visible = value;
    }

    /// <summary>
    /// 挂载物体的动画节点
    /// </summary>
    public AnimatedSprite AnimatedSprite => ActivityObject.AnimatedSprite;
    /// <summary>
    /// 挂载物体的阴影节点
    /// </summary>
    public Sprite ShadowSprite => ActivityObject.ShadowSprite;
    /// <summary>
    /// 挂载物体的碰撞器节点
    /// </summary>
    public CollisionShape2D Collision => ActivityObject.Collision;

    /// <summary>
    /// 是否启用当前组件
    /// </summary>
    public bool Enable { get; set; } = true;

    /// <summary>
    /// 是否被销毁
    /// </summary>
    public bool IsDestroyed { get; private set; }

    //是否调用过 start 函数
    internal bool IsStart = false;

    /// <summary>
    /// 第一次调用 Update 或 PhysicsUpdate 之前调用
    /// </summary>
    public virtual void Start()
    {
    }

    /// <summary>
    /// 如果启用了组件, 则每帧会调用一次 Update
    /// </summary>
    /// <param name="delta"></param>
    public virtual void Update(float delta)
    {
    }

    /// <summary>
    /// 如果启用了组件, 则每物理帧会调用一次 PhysicsUpdate
    /// </summary>
    /// <param name="delta"></param>
    public virtual void PhysicsUpdate(float delta)
    {
    }

    /// <summary>
    /// 当组件被销毁时调用
    /// </summary>
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
    public void Destroy()
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
    
    internal void _SetActivityObject(ActivityObject activityObject)
    {
        ActivityObject = activityObject;
    }
}