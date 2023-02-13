
using Godot;

/// <summary>
/// 组件基类, 用于挂载到游戏物体上, 相比于原生 Node 更加轻量化, 实例化 Component 不会创建额外的 Node, 可以大量添加组件
/// </summary>
public abstract class Component : IProcess, IDestroy
{
    /// <summary>
    /// 当前组件所挂载的游戏对象
    /// </summary>
    public ActivityObject ActivityInstance { get; internal set; }

    /// <summary>
    /// 当前组件所挂载的物体的坐标
    /// </summary>
    public Vector2 Position
    {
        get => ActivityInstance.Position;
        set => ActivityInstance.Position = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的全局坐标
    /// </summary>
    public Vector2 GlobalPosition
    {
        get => ActivityInstance.GlobalPosition;
        set => ActivityInstance.GlobalPosition = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的缩放
    /// </summary>
    public Vector2 Scale
    {
        get => ActivityInstance.Scale;
        set => ActivityInstance.Scale = value;
    }
    
    /// <summary>
    /// 当前组件所挂载物体的全局缩放
    /// </summary>
    public Vector2 GlobalScale
    {
        get => ActivityInstance.GlobalScale;
        set => ActivityInstance.GlobalScale = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的旋转角度
    /// </summary>
    public float Rotation
    {
        get => ActivityInstance.Rotation;
        set => ActivityInstance.Rotation = value;
    }
    
    /// <summary>
    /// 当前组件所挂载物体的全局旋转角度
    /// </summary>
    public float GlobalRotation
    {
        get => ActivityInstance.GlobalRotation;
        set => ActivityInstance.GlobalRotation = value;
    }

    /// <summary>
    /// 当前组件所挂载物体的角度制旋转角度
    /// </summary>
    public float RotationDegrees
    {
        get => ActivityInstance.RotationDegrees;
        set => ActivityInstance.RotationDegrees = value;
    }
    
    /// <summary>
    /// 当前组件所挂载物体的全局角度制旋转角度
    /// </summary>
    public float GlobalRotationDegrees
    {
        get => ActivityInstance.GlobalRotationDegrees;
        set => ActivityInstance.GlobalRotationDegrees = value;
    }
    
    /// <summary>
    /// 当前组件所挂载物体的ZIndex
    /// </summary>
    public int ZIndex
    {
        get => ActivityInstance.ZIndex;
        set => ActivityInstance.ZIndex = value;
    }
    
    /// <summary>
    /// 当前组件是否显示
    /// </summary>
    public bool Visible
    {
        get => ActivityInstance.Visible;
        set => ActivityInstance.Visible = value;
    }

    /// <summary>
    /// 挂载物体的动画节点
    /// </summary>
    public AnimatedSprite2D AnimatedSprite2D => ActivityInstance.AnimatedSprite;
    /// <summary>
    /// 挂载物体的阴影节点
    /// </summary>
    public Sprite2D ShadowSprite => ActivityInstance.ShadowSprite;
    /// <summary>
    /// 挂载物体的碰撞器节点
    /// </summary>
    public CollisionShape2D Collision => ActivityInstance.Collision;

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
                OnEnable();
            }
            else if (_enable && !value)
            {
                OnDisable();
            }

            _enable = value;
        }
    }

    private bool _enable = true;

    /// <summary>
    /// 是否被销毁
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 是否调用过 Ready 函数
    /// </summary>
    public bool IsReady { get; set; }

    /// <summary>
    /// 第一次调用 Process 或 PhysicsProcess 之前调用
    /// </summary>
    public virtual void Ready()
    {
    }

    /// <summary>
    /// 如果启用了组件, 则每帧会调用一次 Process
    /// </summary>
    /// <param name="delta"></param>
    public virtual void Process(float delta)
    {
    }

    /// <summary>
    /// 如果启用了组件, 则每物理帧会调用一次 PhysicsProcess
    /// </summary>
    /// <param name="delta"></param>
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
    /// 如果开启 debug, 则每帧调用该函数, 可用于绘制文字线段等, 需要调用 ActivityObject 身上的绘制函数
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
        ActivityInstance.RemoveComponent(this);
        OnDestroy();
    }
}