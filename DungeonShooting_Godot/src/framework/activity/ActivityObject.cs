
using System;
using System.Collections;
using System.Collections.Generic;
using Config;
using Godot;

/// <summary>
/// 房间内活动物体基类, 所有物体都必须继承该类,<br/>
/// 该类提供基础物体运动模拟, 互动接口, 自定义组件, 协程等功能<br/>
/// ActivityObject 子类实例化请不要直接使用 new, 而用该在类上标上 [Tool], 并在 ActivityObject.xlsx 配置文件中注册物体, 导出配置表后使用 ActivityObject.Create(id) 来创建实例.<br/>
/// </summary>
[Tool]
public partial class ActivityObject : CharacterBody2D, IDestroy, ICoroutine
{
    /// <summary>
    /// 是否是调试模式
    /// </summary>
    public static bool IsDebug { get; set; }

    /// <summary>
    /// 实例唯一 Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 当前物体对应的配置数据, 如果不是通过 ActivityObject.Create() 函数创建出来的对象那么 ItemConfig 为 null
    /// </summary>
    public ExcelConfig.ActivityBase ActivityBase { get; private set; }

    /// <summary>
    /// 是否是静态物体, 如果为true, 则会禁用移动处理
    /// </summary>
    public bool IsStatic
    {
        get => MoveController != null ? !MoveController.Enable : true;
        set
        {
            if (MoveController != null)
            {
                MoveController.Enable = !value;
            }
        }
    }

    /// <summary>
    /// 是否显示阴影
    /// </summary>
    public bool IsShowShadow { get; private set; }
    
    /// <summary>
    /// 当前物体显示的阴影图像, 节点名称必须叫 "ShadowSprite", 类型为 Sprite2D
    /// </summary>
    [Export, ExportFillNode]
    public Sprite2D ShadowSprite { get; set; }
    
    /// <summary>
    /// 当前物体显示的精灵图像, 节点名称必须叫 "AnimatedSprite2D", 类型为 AnimatedSprite2D
    /// </summary>
    [Export, ExportFillNode]
    public AnimatedSprite2D AnimatedSprite { get; set; }

    /// <summary>
    /// 当前物体碰撞器节点, 节点名称必须叫 "Collision", 类型为 CollisionShape2D
    /// </summary>
    [Export, ExportFillNode]
    public CollisionShape2D Collision { get; set; }

    /// <summary>
    /// 是否调用过 Destroy() 函数
    /// </summary>
    public bool IsDestroyed { get; private set; }
    
    /// <summary>
    /// 阴影偏移
    /// </summary>
    [Export]
    public Vector2 ShadowOffset { get; protected set; } = new Vector2(0, 2);
    
    /// <summary>
    /// 移动控制器
    /// </summary>
    public MoveController MoveController { get; private set; }

    /// <summary>
    /// 物体移动基础速率
    /// </summary>
    public Vector2 BasisVelocity
    {
        get
        {
            if (MoveController != null)
            {
                return MoveController.BasisVelocity;
            }

            return Vector2.Zero;
        }
        set
        {
            if (MoveController != null)
            {
                MoveController.BasisVelocity = value;
            }
        }
    }

    /// <summary>
    /// 当前物体归属的区域, 如果为 null 代表不属于任何一个区域
    /// </summary>
    public AffiliationArea AffiliationArea
    {
        get => _affiliationArea;
        set
        {
            if (value != _affiliationArea)
            {
                var prev = _affiliationArea;
                _affiliationArea = value;
                if (!IsDestroyed)
                {
                    OnAffiliationChange(prev);
                }
            }
        }
    }

    /// <summary>
    /// 是否正在投抛过程中
    /// </summary>
    public bool IsThrowing => _throwForce != null && !_isFallOver;

    /// <summary>
    /// 当前物体的海拔高度, 如果大于0, 则会做自由落体运动, 也就是执行投抛逻辑
    /// </summary>
    public float Altitude
    {
        get => _altitude;
        set
        {
            _altitude = value;
            _hasResilienceVerticalSpeed = false;
        }
    }

    private float _altitude = 0;

    /// <summary>
    /// 物体纵轴移动速度, 如果设置大于0, 就可以营造向上投抛物体的效果, 该值会随着重力加速度衰减
    /// </summary>
    public float VerticalSpeed
    {
        get => _verticalSpeed;
        set
        {
            _verticalSpeed = value;
            _hasResilienceVerticalSpeed = false;
        }
    }

    private float _verticalSpeed;

    /// <summary>
    /// 投抛状态下物体碰撞器大小, 如果 (x, y) 都小于 0, 则默认使用 AnimatedSprite 的默认动画第一帧的大小
    /// </summary>
    [Export]
    public Vector2 ThrowCollisionSize { get; set; } = new Vector2(-1, -1);

    /// <summary>
    /// 是否启用垂直方向上的运动模拟, 默认开启, 如果禁用, 那么下落和投抛效果, 同样 Throw() 函数也将失效
    /// </summary>
    public bool EnableVerticalMotion { get; set; } = true;
    
    /// <summary>
    /// 是否启用物体更新行为, 默认 true, 如果禁用, 则会停止当前物体的 Process(), PhysicsProcess() 调用, 并且禁用 Collision 节点, 禁用后所有组件也同样被禁用行为
    /// </summary>
    public bool EnableBehavior
    {
        get => _enableBehavior;
        set
        {
            if (value != _enableBehavior)
            {
                _enableBehavior = value;
                SetProcess(value);
                SetPhysicsProcess(value);
                if (value)
                {
                    Collision.Disabled = _enableBehaviorCollisionDisabledFlag;
                }
                else
                {
                    _enableBehaviorCollisionDisabledFlag = Collision.Disabled;
                    Collision.Disabled = true;
                }
            }
        }
    }

    /// <summary>
    /// 是否启用自定义行为, 默认 true, 如果禁用, 则会停止调用子类重写的 Process(), PhysicsProcess() 函数, 并且当前物体除 MoveController 以外的组件 Process(), PhysicsProcess() 也会停止调用
    /// </summary>
    public bool EnableCustomBehavior { get; set; } = true;
    
    /// <summary>
    /// 物体材质数据
    /// </summary>
    public ExcelConfig.ActivityMaterial ActivityMaterial { get; private set; }
    
    /// <summary>
    /// 所在的 World 对象
    /// </summary>
    public World World { get; private set; }

    /// <summary>
    /// 是否开启描边
    /// </summary>
    public bool ShowOutline
    {
        get => _blendShaderMaterial == null ? false : _blendShaderMaterial.GetShaderParameter(_shader_show_outline).AsBool();
        set
        {
            _blendShaderMaterial?.SetShaderParameter(_shader_show_outline, value);
            _shadowBlendShaderMaterial?.SetShaderParameter(_shader_show_outline, value);
        }
    }

    /// <summary>
    /// 描边颜色
    /// </summary>
    public Color OutlineColor
    {
        get => _blendShaderMaterial == null ? Colors.Black : _blendShaderMaterial.GetShaderParameter(_shader_outline_color).AsColor();
        set => _blendShaderMaterial?.SetShaderParameter(_shader_outline_color, value);
    }

    /// <summary>
    /// 灰度
    /// </summary>
    public float Grey
    {
        get => _blendShaderMaterial == null ? 0 : _blendShaderMaterial.GetShaderParameter(_shader_grey).AsSingle();
        set => _blendShaderMaterial?.SetShaderParameter(_shader_grey, value);
    }
    
    /// <summary>
    /// 是否是自定义阴影纹理
    /// </summary>
    public bool IsCustomShadowSprite { get; private set; }

    // --------------------------------------------------------------------------------

    private static readonly StringName _shader_grey = "grey";
    private static readonly StringName _shader_outline_color = "outline_color";
    private static readonly StringName _shader_show_outline = "show_outline";

    //是否正在调用组件 Update 函数
    private bool _updatingComp = false;
    //组件集合
    private readonly List<KeyValuePair<Type, Component>> _components = new List<KeyValuePair<Type, Component>>();
    //修改的组件集合, value 为 true 表示添加组件, false 表示移除组件
    private readonly List<KeyValuePair<Component, bool>> _changeComponents = new List<KeyValuePair<Component, bool>>();
    //上一帧动画名称
    private StringName _prevAnimation;
    //上一帧动画
    private int _prevAnimationFrame;

    //播放 Hit 动画
    private bool _playHit;
    private float _playHitSchedule;

    //混色shader材质
    private ShaderMaterial _blendShaderMaterial;
    private ShaderMaterial _shadowBlendShaderMaterial;
    
    //存储投抛该物体时所产生的数据
    private readonly ActivityFallData _fallData = new ActivityFallData();
    
    //所在层级
    private RoomLayerEnum _currLayer;
    
    //标记字典
    private Dictionary<string, object> _signMap;
    
    //开启的协程
    private List<CoroutineData> _coroutineList;

    //物体所在区域
    private AffiliationArea _affiliationArea;

    //是否是第一次下坠
    private bool _firstFall = true;
    
    //下坠是否已经结束
    private bool _isFallOver = true;

    //下坠状态碰撞器形状
    private RectangleShape2D _throwRectangleShape;

    //投抛移动速率
    private ExternalForce _throwForce;
    
    //落到地上回弹的速度
    private float _resilienceVerticalSpeed = 0;
    private bool _hasResilienceVerticalSpeed = false;

    //是否启用物体行为
    private bool _enableBehavior = true;
    private bool _enableBehaviorCollisionDisabledFlag;

    private bool _processingBecomesStaticImage = false;

    //击退外力
    private ExternalForce _repelForce;

    // --------------------------------------------------------------------------------
    
    //实例索引
    private static long _instanceIndex = 0;
    
    //冻结显示的Sprite
    private FreezeSprite _freezeSprite;

    //初始化节点
    private void _InitNode(ExcelConfig.ActivityBase config, World world)
    {
#if TOOLS
        if (!Engine.IsEditorHint())
        {
            if (GetType().GetCustomAttributes(typeof(ToolAttribute), false).Length == 0)
            {
                throw new Exception($"ActivityObject子类'{GetType().FullName}'没有加[Tool]标记!");
            }
        }
#endif
        if (config.Material == null)
        {
            ActivityMaterial = ExcelConfig.ActivityMaterial_List[0];
        }
        else
        {
            ActivityMaterial = config.Material;
        }

        //GravityScale 为 0 时关闭重力
        if (ActivityMaterial.GravityScale == 0)
        {
            EnableVerticalMotion = false;
        }
        
        World = world;
        ActivityBase = config;
        Name = GetType().Name + (_instanceIndex++);
        Id = _instanceIndex;
        
        _blendShaderMaterial = AnimatedSprite.Material as ShaderMaterial;
        IsCustomShadowSprite = ShadowSprite.Texture != null;
        if (!IsCustomShadowSprite) //没有自定义阴影纹理
        {
            _shadowBlendShaderMaterial = ShadowSprite.Material as ShaderMaterial;
            if (_shadowBlendShaderMaterial != null && _blendShaderMaterial != null)
            {
                var value = _blendShaderMaterial.GetShaderParameter(_shader_show_outline);
                _shadowBlendShaderMaterial.SetShaderParameter(_shader_show_outline, value);
            }
            
            ShadowSprite.Visible = false;
        }

        MotionMode = MotionModeEnum.Floating;
        MoveController = AddComponent<MoveController>();
        IsStatic = config.IsStatic;
        OnInit();
    }

    /// <summary>
    /// 子类重写的 _Ready() 可能会比 _InitNode() 函数调用晚, 所以禁止子类重写, 如需要 _Ready() 类似的功能需重写 OnInit()
    /// </summary>
    public sealed override void _Ready()
    {

    }
    
    /// <summary>
    /// 子类需要重写 _EnterTree() 函数, 请重写 EnterTree()
    /// </summary>
    public sealed override void _EnterTree()
    {
#if TOOLS
        // 在工具模式下创建的 template 节点自动创建对应的必要子节点
        if (Engine.IsEditorHint())
        {
            _InitNodeInEditor();
            return;
        }
#endif
        EnterTree();
    }
    
    /// <summary>
    /// 子类需要重写 _ExitTree() 函数, 请重写 ExitTree()
    /// </summary>
    public sealed override void _ExitTree()
    {
#if TOOLS
        // 在工具模式下创建的 template 节点自动创建对应的必要子节点
        if (Engine.IsEditorHint())
        {
            return;
        }
#endif
        ExitTree();
    }

    /// <summary>
    /// 显示并更新阴影
    /// </summary>
    public void ShowShadowSprite()
    {
        if (!IsCustomShadowSprite)
        {
            var anim = AnimatedSprite.Animation;
        
            var frame = AnimatedSprite.Frame;
            if (_prevAnimation != anim || _prevAnimationFrame != frame)
            {
                var frames = AnimatedSprite.SpriteFrames;
                if (frames != null && frames.HasAnimation(anim))
                {
                    //切换阴影动画
                    ShadowSprite.Texture = frames.GetFrameTexture(anim, frame);
                }
            }

            _prevAnimation = anim;
            _prevAnimationFrame = frame;
        }

        IsShowShadow = true;
        CalcShadowTransform();
        ShadowSprite.Visible = true;
    }

    /// <summary>
    /// 隐藏阴影
    /// </summary>
    public void HideShadowSprite()
    {
        ShadowSprite.Visible = false;
        IsShowShadow = false;
    }

    /// <summary>
    /// 设置默认序列帧动画的第一帧
    /// </summary>
    public void SetDefaultTexture(Texture2D texture)
    {
        if (AnimatedSprite.SpriteFrames == null)
        {
            SpriteFrames spriteFrames = new SpriteFrames();
            AnimatedSprite.SpriteFrames = spriteFrames;
            spriteFrames.AddFrame("default", texture);
        }
        else
        {
            SpriteFrames spriteFrames = AnimatedSprite.SpriteFrames;
            spriteFrames.SetFrame("default", 0, texture);
        }
    
        AnimatedSprite.Play("default");
    }

    /// <summary>
    /// 获取默认序列帧动画的第一帧
    /// </summary>
    public Texture2D GetDefaultTexture()
    {
        return AnimatedSprite.SpriteFrames.GetFrameTexture("default", 0);
    }
    
    /// <summary>
    /// 获取当前序列帧动画的 Texture2D
    /// </summary>
    public Texture2D GetCurrentTexture()
    {
        var spriteFrames = AnimatedSprite.SpriteFrames;
        if (spriteFrames == null)
        {
            return null;
        }
        return spriteFrames.GetFrameTexture(AnimatedSprite.Animation, AnimatedSprite.Frame);
    }

    /// <summary>
    /// 物体初始化时调用
    /// </summary>
    public virtual void OnInit()
    {
    }
    
    /// <summary>
    /// 进入场景树时调用
    /// </summary>
    public virtual void EnterTree()
    {
    }

    /// <summary>
    /// 离开场景树时调用
    /// </summary>
    public virtual void ExitTree()
    {
    }
    
    /// <summary>
    /// 返回是否能与其他ActivityObject互动
    /// </summary>
    /// <param name="master">触发者</param>
    public virtual CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        return new CheckInteractiveResult(this);
    }

    /// <summary>
    /// 与其它ActivityObject互动时调用, 如果要检测是否能互动请 CheckInteractive() 函数, 如果直接调用该函数那么属于强制互动行为, 例如子弹碰到物体
    /// </summary>
    /// <param name="master">触发者</param>
    public virtual void Interactive(ActivityObject master)
    {
    }

    /// <summary>
    /// 开始投抛该物体时调用
    /// </summary>
    protected virtual void OnThrowStart()
    {
    }
    
    /// <summary>
    /// 投抛该物体达到最高点时调用
    /// </summary>
    protected virtual void OnThrowMaxHeight(float height)
    {
    }

    /// <summary>
    /// 投抛状态下第一次接触地面时调用, 之后的回弹落地将不会调用该函数
    /// </summary>
    protected virtual void OnFirstFallToGround()
    {
    }

    /// <summary>
    /// 投抛状态下每次接触地面时调用
    /// </summary>
    protected virtual void OnFallToGround()
    {
    }

    /// <summary>
    /// 投抛结束时调用
    /// </summary>
    protected virtual void OnThrowOver()
    {
    }

    /// <summary>
    /// 当前物体销毁时调用, 销毁物体请调用 Destroy() 函数
    /// </summary>
    protected virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 每帧调用一次, 物体的 Process() 会在组件的 Process() 之前调用
    /// </summary>
    protected virtual void Process(float delta)
    {
    }
    
    /// <summary>
    /// 每物理帧调用一次, 物体的 PhysicsProcess() 会在组件的 PhysicsProcess() 之前调用
    /// </summary>
    protected virtual void PhysicsProcess(float delta)
    {
    }
    
    /// <summary>
    /// 如果开启 debug, 则每帧调用该函数, 可用于绘制文字线段等
    /// </summary>
    protected virtual void DebugDraw()
    {
    }

    /// <summary>
    /// 归属区域发生改变
    /// </summary>
    /// <param name="prevArea">上一个区域, 注意可能为空</param>
    protected virtual void OnAffiliationChange(AffiliationArea prevArea)
    {
    }

    /// <summary>
    /// 移动并碰撞到物体时调用该函数, 参数为碰撞数据, 该函数由 MoveController 调用
    /// </summary>
    public virtual void OnMoveCollision(KinematicCollision2D collision)
    {
    }

    /// <summary>
    /// 撞到墙壁反弹时调用该函数, 参数为反弹的角度, 弧度制, 该函数由 MoveController 调用
    /// </summary>
    public virtual void OnBounce(float rotation)
    {
    }

    /// <summary>
    /// 返回当物体 CollisionLayer 是否能与 mask 层碰撞
    /// </summary>
    public bool CollisionWithMask(uint mask)
    {
        return (CollisionLayer & mask) != 0;
    }
    
    /// <summary>
    /// 拾起一个 node 节点, 也就是将其从场景树中移除
    /// </summary>
    public void Pickup()
    {
        var parent = GetParent();
        if (parent != null)
        {
            if (IsThrowing)
            {
                StopThrow();
            }

            parent.RemoveChild(this);
        }
    }

    /// <summary>
    /// 将一个节点扔到地上
    /// <param name="layer">放入的层</param>
    /// <param name="showShadow">是否显示阴影</param>
    /// </summary>
    public virtual void PutDown(RoomLayerEnum layer, bool showShadow = true)
    {
        _currLayer = layer;
        var parent = GetParent();
        var root = GameApplication.Instance.World.GetRoomLayer(layer);
        if (parent != root)
        {
            if (parent != null)
            {
                Reparent(root);
            }
            else
            {
                root.AddChild(this);
            }
        }

        if (showShadow)
        {
            if (IsInsideTree())
            {
                ShowShadowSprite();
            }
            else
            {
                //注意需要延时调用
                CallDeferred(nameof(ShowShadowSprite));
                CalcShadowTransform();
            }
        }
        else
        {
            ShadowSprite.Visible = false;
        }
    }

    /// <summary>
    /// 将一个节点扔到地上
    /// </summary>
    /// <param name="position">放置的位置</param>
    /// <param name="layer">放入的层</param>
    /// <param name="showShadow">是否显示阴影</param>
    public void PutDown(Vector2 position, RoomLayerEnum layer, bool showShadow = true)
    {
        PutDown(layer);
        Position = position;
    }

    /// <summary>
    /// 将该节点投抛出去
    /// </summary>
    /// <param name="altitude">初始高度</param>
    /// <param name="verticalSpeed">纵轴速度</param>
    /// <param name="velocity">移动速率</param>
    /// <param name="rotateSpeed">旋转速度</param>
    public void Throw(float altitude, float verticalSpeed, Vector2 velocity, float rotateSpeed)
    {
        var parent = GetParent();
        if (parent == null)
        {
            GameApplication.Instance.World.YSortLayer.AddChild(this);
        }
        else if (parent != GameApplication.Instance.World.YSortLayer)
        {
            parent.RemoveChild(this);
            GameApplication.Instance.World.YSortLayer.AddChild(this);
        }
        
        Altitude = altitude;
        //Position = Position + new Vector2(0, altitude);
        VerticalSpeed = verticalSpeed;
        //ThrowRotationDegreesSpeed = rotateSpeed;
        if (_throwForce != null)
        {
            MoveController.RemoveForce(_throwForce);
        }

        _throwForce = new ExternalForce(ForceNames.Throw);
        _throwForce.Velocity = velocity;
        _throwForce.RotationSpeed = Mathf.DegToRad(rotateSpeed);
        MoveController.AddForce(_throwForce);

        InitThrowData();
    }

    /// <summary>
    /// 将该节点投抛出去
    /// </summary>
    /// <param name="position">初始位置</param>
    /// <param name="altitude">初始高度</param>
    /// <param name="verticalSpeed">纵轴速度</param>
    /// <param name="velocity">移动速率</param>
    /// <param name="rotateSpeed">旋转速度</param>
    public void Throw(Vector2 position, float altitude, float verticalSpeed, Vector2 velocity, float rotateSpeed)
    {
        GlobalPosition = position;
        Throw(altitude, verticalSpeed, velocity, rotateSpeed);
    }


    /// <summary>
    /// 强制停止投抛运动
    /// </summary>
    public void StopThrow()
    {
        _isFallOver = true;
        RestoreCollision();
    }

    /// <summary>
    /// 往当前物体上挂载一个组件
    /// </summary>
    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        if (_updatingComp)
        {
            _changeComponents.Add(new KeyValuePair<Component, bool>(component, true));
        }
        else
        {
            _components.Add(new KeyValuePair<Type, Component>(typeof(T), component));
        }

        component.Master = this;
        component.Ready();
        component.OnEnable();
        return component;
    }

    /// <summary>
    /// 往当前物体上挂载一个组件
    /// </summary>
    public Component AddComponent(Type type)
    {
        var component = (Component)Activator.CreateInstance(type);
        if (_updatingComp)
        {
            _changeComponents.Add(new KeyValuePair<Component, bool>(component, true));
        }
        else
        {
            _components.Add(new KeyValuePair<Type, Component>(type, component));
        }
        
        component.Master = this;
        component.Ready();
        component.OnEnable();
        return component;
    }

    /// <summary>
    /// 移除一个组件, 并且销毁
    /// </summary>
    /// <param name="component">组件对象</param>
    public void RemoveComponent(Component component)
    {
        if (component.IsDestroyed)
        {
            return;
        }

        if (_updatingComp)
        {
            _changeComponents.Add(new KeyValuePair<Component, bool>(component, false));
            component.Destroy();
        }
        else
        {
            for (var i = 0; i < _components.Count; i++)
            {
                if (_components[i].Value == component)
                {
                    _components.RemoveAt(i);
                    component.Destroy();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 根据类型获取一个组件
    /// </summary>
    public Component GetComponent(Type type)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var temp = _components[i];
            if (temp.Key.IsAssignableTo(type))
            {
                return temp.Value;
            }
        }

        if (_updatingComp)
        {
            for (var i = 0; i < _changeComponents.Count; i++)
            {
                var temp = _components[i];
                if (temp.Value.GetType().IsAssignableTo(type))
                {
                    return temp.Value;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 根据类型获取一个组件
    /// </summary>
    public T GetComponent<T>() where T : Component
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var temp = _components[i];
            if (temp.Value is T component)
            {
                return component;
            }
        }

        if (_updatingComp)
        {
            for (var i = 0; i < _changeComponents.Count; i++)
            {
                var temp = _components[i];
                if (temp.Value is T component)
                {
                    return component;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 根据类型获取所有相同类型的组件
    /// </summary>
    public Component[] GetComponents(Type type)
    {
        var list = new List<Component>();
        for (int i = 0; i < _components.Count; i++)
        {
            var temp = _components[i];
            if (temp.Key.IsAssignableTo(type))
            {
                list.Add(temp.Value);
            }
        }

        if (_updatingComp)
        {
            for (var i = 0; i < _changeComponents.Count; i++)
            {
                var temp = _components[i];
                if (temp.Value.GetType().IsAssignableTo(type))
                {
                    list.Add(temp.Value);
                }
            }
        }

        return list.ToArray();
    }
    
    /// <summary>
    /// 根据类型获取所有相同类型的组件
    /// </summary>
    public T[] GetComponents<T>() where T : Component
    {
        var list = new List<T>();
        for (int i = 0; i < _components.Count; i++)
        {
            var temp = _components[i];
            if (temp.Value is T component)
            {
                list.Add(component);
            }
        }

        if (_updatingComp)
        {
            for (var i = 0; i < _changeComponents.Count; i++)
            {
                var temp = _components[i];
                if (temp.Value is T component)
                {
                    list.Add(component);
                }
            }
        }

        return list.ToArray();
    }
    
    /// <summary>
    /// 设置混色材质的颜色
    /// </summary>
    public void SetBlendColor(Color color)
    {
        _blendShaderMaterial?.SetShaderParameter("blend", color);
    }

    /// <summary>
    /// 获取混色材质的颜色
    /// </summary>
    public Color GetBlendColor()
    {
        if (_blendShaderMaterial == null)
        {
            return Colors.White;
        }
        return _blendShaderMaterial.GetShaderParameter("blend").AsColor();
    }
    
    /// <summary>
    /// 设置混色材质的强度
    /// </summary>
    public void SetBlendSchedule(float value)
    {
        _blendShaderMaterial?.SetShaderParameter("schedule", value);
    }

    /// <summary>
    /// 获取混色材质的强度
    /// </summary>
    public float GetBlendSchedule()
    {
        if (_blendShaderMaterial == null)
        {
            return default;
        }
        return _blendShaderMaterial.GetShaderParameter("schedule").AsSingle();
    }

    /// <summary>
    /// 设置混色颜色
    /// </summary>
    public void SetBlendModulate(Color color)
    {
        _blendShaderMaterial?.SetShaderParameter("modulate", color);
        _shadowBlendShaderMaterial?.SetShaderParameter("modulate", color);
    }
    
    /// <summary>
    /// 获取混色颜色
    /// </summary>
    public Color SetBlendModulate()
    {
        if (_blendShaderMaterial == null)
        {
            return Colors.White;
        }
        return _blendShaderMaterial.GetShaderParameter("modulate").AsColor();
    }
    
    /// <summary>
    /// 每帧调用一次, 为了防止子类覆盖 _Process(), 给 _Process() 加上了 sealed, 子类需要帧循环函数请重写 Process() 函数
    /// </summary>
    public sealed override void _Process(double delta)
    {
#if TOOLS
        if (Engine.IsEditorHint())
        {
            return;
        }
#endif
        var newDelta = (float)delta;
        if (EnableCustomBehavior)
        {
            Process(newDelta);
        }
        
        //更新组件
        if (_components.Count > 0)
        {
            _updatingComp = true;
            if (EnableCustomBehavior) //启用所有组件
            {
                for (int i = 0; i < _components.Count; i++)
                {
                    if (IsDestroyed) return;
                    var temp = _components[i].Value;
                    if (temp != null && temp.Enable)
                    {
                        temp.Process(newDelta);
                    }
                }
            }
            else //只更新 MoveController 组件
            {
                if (MoveController.Enable)
                {
                    MoveController.Process(newDelta);
                }
            }
            _updatingComp = false;
            
            if (_changeComponents.Count > 0)
            {
                RefreshComponent();
            }
        }

        // 更新下坠处理逻辑
        UpdateFall(newDelta);

        //阴影
        UpdateShadowSprite(newDelta);
        
        // Hit 动画
        if (_playHit)
        {
            if (_playHitSchedule < 0.05f)
            {
                _blendShaderMaterial?.SetShaderParameter("schedule", 1);
            }
            else if (_playHitSchedule < 0.15f)
            {
                _blendShaderMaterial?.SetShaderParameter("schedule", Mathf.Lerp(1, 0, (_playHitSchedule - 0.05f) / 0.1f));
            }
            if (_playHitSchedule >= 0.15f)
            {
                _blendShaderMaterial?.SetShaderParameter("schedule", 0);
                _playHitSchedule = 0;
                _playHit = false;
            }
            else
            {
                _playHitSchedule += newDelta;
            }
        }
        
        //协程更新
        ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, newDelta);
        
        //调试绘制
        if (IsDebug)
        {
            QueueRedraw();
        }
    }

    /// <summary>
    /// 更新下坠处理逻辑
    /// </summary>
    public void UpdateFall(float delta)
    {
        // 下坠判定
        if (Altitude > 0 || VerticalSpeed != 0)
        {
            if (_isFallOver) // 没有处于下坠状态, 则进入下坠状态
            {
                InitThrowData();
            }
            else
            {
                if (EnableVerticalMotion) //如果启用了纵向运动, 则更新运动
                {
                    //GlobalRotationDegrees = GlobalRotationDegrees + ThrowRotationDegreesSpeed * newDelta;

                    var ysp = VerticalSpeed;

                    _altitude += VerticalSpeed * delta;
                    _verticalSpeed -= GameConfig.G * ActivityMaterial.GravityScale * delta;

                    //当高度大于16时, 显示在所有物体上, 并且关闭碰撞
                    if (Altitude >= 16)
                    {
                        AnimatedSprite.ZIndex = 20;
                    }
                    else
                    {
                        AnimatedSprite.ZIndex = 0;
                    }
                    //动态开关碰撞器
                    if (ActivityMaterial.DynamicCollision)
                    {
                        Collision.Disabled = Altitude >= 16;
                    }
                
                    //达到最高点
                    if (ysp > 0 && ysp * VerticalSpeed < 0)
                    {
                        OnThrowMaxHeight(Altitude);
                    }

                    //落地判断
                    if (Altitude <= 0)
                    {
                        _altitude = 0;

                        //第一次接触地面
                        if (_firstFall)
                        {
                            _firstFall = false;
                            OnFirstFallToGround();
                        }

                        if (_throwForce != null)
                        {
                            //缩放移动速度
                            //MoveController.ScaleAllForce(BounceSpeed);
                            _throwForce.Velocity *= ActivityMaterial.FallBounceSpeed;
                            //缩放旋转速度
                            //MoveController.ScaleAllRotationSpeed(BounceStrength);
                            _throwForce.RotationSpeed *= ActivityMaterial.FallBounceRotation;
                        }
                        //如果落地高度不够低, 再抛一次
                        if (ActivityMaterial.Bounce && (!_hasResilienceVerticalSpeed || _resilienceVerticalSpeed > 5))
                        {
                            if (!_hasResilienceVerticalSpeed)
                            {
                                _hasResilienceVerticalSpeed = true;
                                _resilienceVerticalSpeed = -VerticalSpeed * ActivityMaterial.FallBounceStrength;
                            }
                            else
                            {
                                if (_resilienceVerticalSpeed < 25)
                                {
                                    _resilienceVerticalSpeed = _resilienceVerticalSpeed * ActivityMaterial.FallBounceStrength * 0.4f;
                                }
                                else
                                {
                                    _resilienceVerticalSpeed = _resilienceVerticalSpeed * ActivityMaterial.FallBounceStrength;
                                }
                            }
                            _verticalSpeed = _resilienceVerticalSpeed;
                            _isFallOver = false;

                            OnFallToGround();
                        }
                        else //结束
                        {
                            _verticalSpeed = 0;

                            if (_throwForce != null)
                            {
                                MoveController.RemoveForce(_throwForce);
                                _throwForce = null;
                            }
                            _isFallOver = true;
                            
                            OnFallToGround();
                            ThrowOver();
                        }
                    }
                }

                //计算精灵位置
                CalcThrowAnimatedPosition();
            }
        }

    }

    /// <summary>
    /// 更新阴影逻辑
    /// </summary>
    public void UpdateShadowSprite(float delta)
    {
        // 阴影
        if (ShadowSprite.Visible)
        {
            if (!IsCustomShadowSprite)
            {
                //更新阴影贴图, 使其和动画一致
                var anim = AnimatedSprite.Animation;
                var frame = AnimatedSprite.Frame;
                if (_prevAnimation != anim || _prevAnimationFrame != frame)
                {
                    //切换阴影动画
                    ShadowSprite.Texture = AnimatedSprite.SpriteFrames.GetFrameTexture(anim, AnimatedSprite.Frame);
                }

                _prevAnimation = anim;
                _prevAnimationFrame = frame;
            }

            if (_freezeSprite == null || !_freezeSprite.IsFrozen)
            {
                //计算阴影
                CalcShadowTransform();
            }
        }

    }
    
    /// <summary>
    /// 每物理帧调用一次, 为了防止子类覆盖 _PhysicsProcess(), 给 _PhysicsProcess() 加上了 sealed, 子类需要帧循环函数请重写 PhysicsProcess() 函数
    /// </summary>
    public sealed override void _PhysicsProcess(double delta)
    {
#if TOOLS
        if (Engine.IsEditorHint())
        {
            return;
        }
#endif
        var newDelta = (float)delta;
        if (EnableCustomBehavior)
        {
            PhysicsProcess(newDelta);
        }
        
        //更新组件
        if (_components.Count > 0)
        {
            _updatingComp = true;
            if (EnableCustomBehavior) //启用所有组件
            {
                for (int i = 0; i < _components.Count; i++)
                {
                    if (IsDestroyed) return;
                    var temp = _components[i].Value;
                    if (temp != null && temp.Enable)
                    {
                        temp.PhysicsProcess(newDelta);
                    }
                }
            }
            else //只更新 MoveController 组件
            {
                if (MoveController.Enable)
                {
                    MoveController.PhysicsProcess(newDelta);
                }
            }
            _updatingComp = false;

            if (_changeComponents.Count > 0)
            {
                RefreshComponent();
            }
        }
    }

    //更新新增/移除的组件
    private void RefreshComponent()
    {
        for (var i = 0; i < _changeComponents.Count; i++)
        {
            var item = _changeComponents[i];
            if (item.Value) //添加组件
            {
                _components.Add(new KeyValuePair<Type, Component>(item.Key.GetType(), item.Key));
            }
            else //移除组件
            {
                for (var j = 0; j < _components.Count; j++)
                {
                    if (_components[i].Value == item.Key)
                    {
                        _components.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 绘制函数, 子类不允许重写, 需要绘制函数请重写 DebugDraw()
    /// </summary>
    public sealed override void _Draw()
    {
#if TOOLS
        if (Engine.IsEditorHint())
        {
            return;
        }
#endif
        if (IsDebug)
        {
            DebugDraw();
            if (_components.Count > 0)
            {
                var arr = _components.ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (IsDestroyed) return;
                    var temp = arr[i].Value;
                    if (temp != null && temp.Master == this && temp.Enable)
                    {
                        temp.DebugDraw();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 重新计算物体阴影的位置和旋转信息, 无论是否显示阴影
    /// </summary>
    public void CalcShadowTransform()
    {
        //偏移
        if (!IsCustomShadowSprite)
        {
            ShadowSprite.Offset = AnimatedSprite.Offset;
        }

        //缩放
        ShadowSprite.Scale = AnimatedSprite.Scale;
        //阴影角度
        ShadowSprite.Rotation = 0;
        //阴影位置计算
        var pos = AnimatedSprite.GlobalPosition;
        ShadowSprite.GlobalPosition = new Vector2(pos.X + ShadowOffset.X, pos.Y + ShadowOffset.Y + Altitude);
    }

    //计算位置
    private void CalcThrowAnimatedPosition()
    {
        if (Scale.Y < 0)
        {
            var pos = new Vector2(_fallData.OriginSpritePosition.X, -_fallData.OriginSpritePosition.Y);
            AnimatedSprite.GlobalPosition = GlobalPosition + new Vector2(0, -Altitude) - pos.Rotated(Rotation + Mathf.Pi);
        }
        else
        {
            AnimatedSprite.GlobalPosition = GlobalPosition + new Vector2(0, -Altitude) + _fallData.OriginSpritePosition.Rotated(Rotation);
        }
    }


    /// <summary>
    /// 销毁物体
    /// </summary>
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        if (AffiliationArea != null)
        {
            AffiliationArea.RemoveItem(this);
        }
        
        QueueFree();
        OnDestroy();

        if (_freezeSprite != null)
        {
            _freezeSprite.Destroy();
        }
        
        var arr = _components.ToArray();
        for (var i = 0; i < arr.Length; i++)
        {
            arr[i].Value?.Destroy();
        }

        _components.Clear();
    }

    /// <summary>
    /// 延时销毁
    /// </summary>
    public void DelayDestroy()
    {
        CallDeferred(nameof(Destroy));
    }

    /// <summary>
    /// 继承指定物体的运动速率
    /// </summary>
    /// <param name="other">目标对象</param>
    /// <param name="scale">继承的速率缩放</param>
    public void InheritVelocity(ActivityObject other, float scale = 0.5f)
    {
        MoveController.AddVelocity(other.Velocity * scale);
    }

    /// <summary>
    /// 触发投抛动作
    /// </summary>
    private void Throw()
    {
        var parent = GetParent();
        //投抛时必须要加入 YSortLayer 节点下
        if (parent == null)
        {
            this.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        }
        else if (parent == GameApplication.Instance.World.NormalLayer)
        {
            parent.RemoveChild(this);
            this.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        }

        CalcThrowAnimatedPosition();
        //显示阴影
        ShowShadowSprite();

        if (EnableVerticalMotion)
        {
            OnThrowStart();
        }
    }

    /// <summary>
    /// 设置下坠状态下的碰撞器
    /// </summary>
    private void SetFallCollision()
    {
        if (_fallData != null && _fallData.UseOrigin)
        {
            _fallData.OriginShape = Collision.Shape;
            _fallData.OriginPosition = Collision.Position;
            _fallData.OriginRotation = Collision.Rotation;
            _fallData.OriginScale = Collision.Scale;
            _fallData.OriginZIndex = ZIndex;
            _fallData.OriginSpritePosition = AnimatedSprite.Position;
            _fallData.OriginCollisionEnable = Collision.Disabled;
            _fallData.OriginCollisionPosition = Collision.Position;
            _fallData.OriginCollisionRotation = Collision.Rotation;
            _fallData.OriginCollisionScale = Collision.Scale;
            _fallData.OriginCollisionMask = CollisionMask;
            _fallData.OriginCollisionLayer = CollisionLayer;

            if (_throwRectangleShape == null)
            {
                _throwRectangleShape = new RectangleShape2D();
            }
            
            Collision.Shape = _throwRectangleShape;
            Collision.Position = Vector2.Zero;
            Collision.Rotation = 0;
            Collision.Scale = Vector2.One;
            ZIndex = 0;
            Collision.Disabled = false;
            Collision.Position = Vector2.Zero;
            Collision.Rotation = 0;
            Collision.Scale = Vector2.One;
            CollisionMask = 1;
            CollisionLayer = _fallData.OriginCollisionLayer | PhysicsLayer.Throwing;
            _fallData.UseOrigin = false;
        }
    }

    /// <summary>
    /// 重置碰撞器
    /// </summary>
    private void RestoreCollision()
    {
        if (_fallData != null && !_fallData.UseOrigin)
        {
            Collision.Shape = _fallData.OriginShape;
            Collision.Position = _fallData.OriginPosition;
            Collision.Rotation = _fallData.OriginRotation;
            Collision.Scale = _fallData.OriginScale;
            ZIndex = _fallData.OriginZIndex;
            AnimatedSprite.Position = _fallData.OriginSpritePosition;
            Collision.Disabled = _fallData.OriginCollisionEnable;
            Collision.Position = _fallData.OriginCollisionPosition;
            Collision.Rotation = _fallData.OriginCollisionRotation;
            Collision.Scale = _fallData.OriginCollisionScale;
            CollisionMask = _fallData.OriginCollisionMask;
            CollisionLayer = _fallData.OriginCollisionLayer;

            _fallData.UseOrigin = true;
        }
    }

    /// <summary>
    /// 投抛结束
    /// </summary>
    private void ThrowOver()
    {
        var parent = GetParent();
        var roomLayer = GameApplication.Instance.World.GetRoomLayer(_currLayer);
        if (parent != roomLayer)
        {
            parent.RemoveChild(this);
            roomLayer.AddChild(this);
        }
        RestoreCollision();

        OnThrowOver();
    }

    //初始化投抛状态数据
    private void InitThrowData()
    {
        SetFallCollision();

        _isFallOver = false;
        _firstFall = true;
        _hasResilienceVerticalSpeed = false;
        _resilienceVerticalSpeed = 0;
                
        if (ThrowCollisionSize.X < 0 && ThrowCollisionSize.Y < 0)
        {
            _throwRectangleShape.Size = GetDefaultTexture().GetSize();
        }
        else
        {
            _throwRectangleShape.Size = ThrowCollisionSize;
        }

        Throw();
    }

    /// <summary>
    /// 设置标记, 用于在物体上记录自定义数据
    /// </summary>
    /// <param name="name">标记名称</param>
    /// <param name="v">存入值</param>
    public void SetSign(string name, object v)
    {
        if (_signMap == null)
        {
            _signMap = new Dictionary<string, object>();
        }

        _signMap[name] = v;
    }

    /// <summary>
    /// 返回是否存在指定名称的标记数据
    /// </summary>
    public bool HasSign(string name)
    {
        return _signMap == null ? false : _signMap.ContainsKey(name);
    }

    /// <summary>
    /// 根据名称获取标记值
    /// </summary>
    public object GetSign(string name)
    {
        if (_signMap == null)
        {
            return null;
        }

        _signMap.TryGetValue(name, out var value);
        return value;
    }

    /// <summary>
    /// 根据名称获取标记值
    /// </summary>
    public T GetSign<T>(string name)
    {
        if (_signMap == null)
        {
            return default;
        }

        _signMap.TryGetValue(name, out var value);
        if (value is T v)
        {
            return v;
        }
        return default;
    }

    /// <summary>
    /// 根据名称删除标记
    /// </summary>
    public void RemoveSign(string name)
    {
        if (_signMap != null)
        {
            _signMap.Remove(name);
        }
    }

    /// <summary>
    /// 播放受伤动画, 该动画不与 Animation 节点的动画冲突
    /// </summary>
    public void PlayHitAnimation()
    {
        _playHit = true;
        _playHitSchedule = 0;
    }

    /// <summary>
    /// 获取当前摩擦力
    /// </summary>
    public float GetCurrentFriction()
    {
        return ActivityMaterial.Friction;
    }
    
    /// <summary>
    /// 获取当前旋转摩擦力
    /// </summary>
    public float GetCurrentRotationFriction()
    {
        return ActivityMaterial.RotationFriction;
    }
    
    public long StartCoroutine(IEnumerator able)
    {
        return ProxyCoroutineHandler.ProxyStartCoroutine(ref _coroutineList, able);
    }
    
    public void StopCoroutine(long coroutineId)
    {
        ProxyCoroutineHandler.ProxyStopCoroutine(ref _coroutineList, coroutineId);
    }

    public bool IsCoroutineOver(long coroutineId)
    {
        return ProxyCoroutineHandler.ProxyIsCoroutineOver(ref _coroutineList, coroutineId);
    }

    public void StopAllCoroutine()
    {
        ProxyCoroutineHandler.ProxyStopAllCoroutine(ref _coroutineList);
    }
    
    /// <summary>
    /// 播放 AnimatedSprite 上的动画, 如果没有这个动画, 则什么也不会发生
    /// </summary>
    /// <param name="name">动画名称</param>
    public void PlaySpriteAnimation(string name)
    {
        var spriteFrames = AnimatedSprite.SpriteFrames;
        if (spriteFrames != null && spriteFrames.HasAnimation(name))
        {
            AnimatedSprite.Play(name);
        }
    }

    /// <summary>
    /// 将当前 ActivityObject 变成静态图像绘制到地面上, 用于优化渲染大量物体<br/>
    /// 调用该函数后会排队进入渲染队列, 并且禁用所有行为, 当渲染完成后会销毁当前对象, 也就是调用 Destroy() 函数<br/>
    /// </summary>
    public void BecomesStaticImage()
    {
        if (_processingBecomesStaticImage)
        {
            return;
        }
        
        if (AffiliationArea == null)
        {
            Debug.LogError($"调用函数: BecomesStaticImage() 失败, 物体{Name}没有归属区域, 无法确定绘制到哪个ImageCanvas上, 直接执行销毁");
            Destroy();
            return;
        }

        _processingBecomesStaticImage = true;
        EnableBehavior = false;
        var roomInfo = AffiliationArea.RoomInfo;
        var position = roomInfo.ToCanvasPosition(GlobalPosition);
        roomInfo.StaticImageCanvas.DrawActivityObjectInCanvas(this, position.X, position.Y, () =>
        {
            Destroy();
        });
    }

    /// <summary>
    /// 是否正在处理成为静态图片
    /// </summary>
    public bool IsProcessingBecomesStaticImage()
    {
        return _processingBecomesStaticImage;
    }
    
    /// <summary>
    /// 冻结物体，多余的节点就会被移出场景树，逻辑也会被暂停，用于优化性能
    /// </summary>
    public void Freeze()
    {
        if (_freezeSprite == null)
        {
            _freezeSprite = new FreezeSprite(this);
        }
        _freezeSprite.Freeze();
    }

    /// <summary>
    /// 解冻物体, 恢复正常逻辑
    /// </summary>
    public void Unfreeze()
    {
        if (_freezeSprite == null)
        {
            return;
        }
        _freezeSprite.Unfreeze();
    }

    /// <summary>
    /// 获取中心点坐标
    /// </summary>
    public Vector2 GetCenterPosition()
    {
        return AnimatedSprite.Position + Position;
    }

    /// <summary>
    /// 设置物体朝向
    /// </summary>
    public void SetFace(FaceDirection face)
    {
        if ((face == FaceDirection.Left && Scale.X > 0) || (face == FaceDirection.Right && Scale.X < 0))
        {
            Scale *= new Vector2(-1, 1);
        }
    }
    
    /// <summary>
    /// 添加一个击退力
    /// </summary>
    public void AddRepelForce(Vector2 velocity)
    {
        if (_repelForce == null)
        {
            _repelForce = new ExternalForce(ForceNames.Repel);
        }

        //不在 MoveController 中
        if (_repelForce.MoveController == null)
        {
            _repelForce.Velocity = velocity;
            MoveController.AddForce(_repelForce);
        }
        else
        {
            _repelForce.Velocity += velocity;
        }
    }

    /// <summary>
    /// 获取击退力
    /// </summary>
    public Vector2 GetRepelForce()
    {
        if (_repelForce == null || _repelForce.MoveController == null)
        {
            return Vector2.Zero;
        }

        return _repelForce.Velocity;
    }
}