
using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间内活动物体基类, 所有物体都必须继承该类,
/// ActivityObject 使用的时候代码和场景分离的设计模式, 所以创建时必须指定模板场景路径, 这样做的好处是一个模板场景可以用在多个代码类上, 同样一个代码类也可以指定不同的目模板场景, 
/// ActivityObject 子类实例化请不要直接使用 new, 而用该在类上标上 [RegisterActivity(id, prefabPath)],
/// ActivityObject 类会自动扫描并注册物体, 然后使用而是使用 ActivityObject.Create(id) 来创建实例
/// </summary>
public abstract partial class ActivityObject : CharacterBody2D
{
    /// <summary>
    /// 是否是调试模式
    /// </summary>
    public static bool IsDebug { get; set; }

    /// <summary>
    /// 当前物体类型id, 用于区分是否是同一种物体, 如果不是通过 ActivityObject.Create() 函数创建出来的对象那么 ItemId 为 null
    /// </summary>
    public string ItemId { get; private set; }

    /// <summary>
    /// 当前物体显示的精灵图像, 节点名称必须叫 "AnimatedSprite2D", 类型为 AnimatedSprite2D
    /// </summary>
    public AnimatedSprite2D AnimatedSprite { get; private set; }

    /// <summary>
    /// 当前物体显示的阴影图像, 节点名称必须叫 "ShadowSprite", 类型为 Sprite2D
    /// </summary>
    public Sprite2D ShadowSprite { get; private set; }

    /// <summary>
    /// 当前物体碰撞器节点, 节点名称必须叫 "Collision", 类型为 CollisionShape2D
    /// </summary>
    public CollisionShape2D Collision { get; private set; }

    /// <summary>
    /// 是否调用过 Destroy() 函数
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 是否正在投抛过程中
    /// </summary>
    public bool IsThrowing => _fallData != null && !_fallData.IsFallOver;

    /// <summary>
    /// 阴影偏移
    /// </summary>
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
        get => MoveController.BasisVelocity;
        set => MoveController.BasisVelocity = value;
    }

    /// <summary>
    /// 当前物体归属的区域, 如果为 null 代表不属于任何一个区域
    /// </summary>
    public AffiliationArea Affiliation
    {
        get => _affiliationArea;
        set
        {
            if (value != _affiliationArea)
            {
                _affiliationArea = value;
                OnAffiliationChange();
            }
        }
    }
    
    /// <summary>
    /// 当前物体的海拔高度, 如果大于0, 则会做自由落体运动 
    /// </summary>
    public float Altitude { get; set; }

    /// <summary>
    /// 物体纵轴移动速度, 如果设置大于0, 就可以营造向上投抛物体的效果, 该值会随着重力加速度衰减
    /// </summary>
    public float VerticalSpeed { get; set; }
    
    /// <summary>
    /// 物体旋转速度, 角度制
    /// </summary>
    public float RotationDegreesSpeed { get; set; }
    
    //组件集合
    private List<KeyValuePair<Type, Component>> _components = new List<KeyValuePair<Type, Component>>();
    //是否初始化阴影
    private bool _initShadow;
    //上一帧动画名称
    private string _prevAnimation;
    //上一帧动画
    private int _prevAnimationFrame;

    //播放 Hit 动画
    private bool _playHit;
    private float _playHitSchedule;

    //混色shader材质
    private ShaderMaterial _blendShaderMaterial;
    
    //存储投抛该物体时所产生的数据
    private ActivityThrowData _fallData;
    
    //所在层级
    private RoomLayerEnum _currLayer;
    
    //标记字典
    private Dictionary<string, object> _signMap;
    
    //开启的协程
    private List<CoroutineData> _coroutineList;
    //模板实例
    private ActivityObjectTemplate _templateInstance;
    
    //物体所在区域
    private AffiliationArea _affiliationArea;

    //落到地上回弹的速度
    private float _resilienceVerticalSpeed = 0;
    private bool _hasResilienceVerticalSpeed = false;
    
    //实例索引
    private static long _instanceIndex = 0;

    //初始化节点
    private void _InitNode(string itemId, string scenePath)
    {
        //加载预制体
        var tempPrefab = ResourceManager.Load<PackedScene>(scenePath);
        if (tempPrefab == null)
        {
            throw new Exception("创建 ActivityObject 没有找到指定挂载的预制体: " + scenePath);
        }

        ItemId = itemId;
        Name = GetType().Name + (_instanceIndex++);
        
        _templateInstance = tempPrefab.Instantiate<ActivityObjectTemplate>();
        //移动子节点
        var count = _templateInstance.GetChildCount();
        for (int i = 0; i < count; i++)
        {
            var body = _templateInstance.GetChild(0);
            _templateInstance.RemoveChild(body);
            AddChild(body);
            body.Owner = this;
            switch (body.Name)
            {
                case "AnimatedSprite":
                    AnimatedSprite = (AnimatedSprite2D)body;
                    _blendShaderMaterial = AnimatedSprite.Material as ShaderMaterial;
                    break;
                case "ShadowSprite":
                    ShadowSprite = (Sprite2D)body;
                    ShadowSprite.Visible = false;
                    break;
                case "Collision":
                    Collision = (CollisionShape2D)body;
                    break;
            }
        }
        
        ZIndex = _templateInstance.z_index;
        CollisionLayer = _templateInstance.collision_layer;
        CollisionMask = _templateInstance.collision_mask;
        Scale = _templateInstance.scale;
        Visible = _templateInstance.visible;

        MotionMode = MotionModeEnum.Floating;

        MoveController = AddComponent<MoveController>();
        
        //临时处理, 4.0 有bug, 不能销毁模板实例, 不然关闭游戏会报错!!!
        //_templateInstance.CallDeferred(Node.MethodName.QueueFree);

        OnInit();
    }

    /// <summary>
    /// 显示阴影
    /// </summary>
    public void ShowShadowSprite()
    {
        if (!_initShadow)
        {
            _initShadow = true;
            ShadowSprite.Material = ResourceManager.BlendMaterial;
        }

        var anim = AnimatedSprite.Animation;
        
        var frame = AnimatedSprite.Frame;
        if (_prevAnimation != anim || _prevAnimationFrame != frame)
        {
            var frames = AnimatedSprite.SpriteFrames;
            if (frames.HasAnimation(anim))
            {
                //切换阴影动画
                ShadowSprite.Texture = frames.GetFrameTexture(anim, frame);
            }
        }

        _prevAnimation = anim;
        _prevAnimationFrame = frame;

        CalcShadow();
        ShadowSprite.Visible = true;
    }

    /// <summary>
    /// 隐藏阴影
    /// </summary>
    public void HideShadowSprite()
    {
        ShadowSprite.Visible = false;
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
        return AnimatedSprite.SpriteFrames.GetFrameTexture(AnimatedSprite.Name, AnimatedSprite.Frame);
    }

    /// <summary>
    /// 物体初始化时调用
    /// </summary>
    public virtual void OnInit()
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
    /// 每帧调用一次, ProcessOver() 会在组件的 Process() 之后调用
    /// </summary>
    protected virtual void ProcessOver(float delta)
    {
    }
    
    /// <summary>
    /// 每物理帧调用一次, 物体的 PhysicsProcess() 会在组件的 PhysicsProcess() 之前调用
    /// </summary>
    protected virtual void PhysicsProcess(float delta)
    {
    }
    
    /// <summary>
    /// 每物理帧调用一次, PhysicsProcessOver() 会在组件的 PhysicsProcess() 之后调用
    /// </summary>
    protected virtual void PhysicsProcessOver(float delta)
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
    protected virtual void OnAffiliationChange()
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
        var root = GameApplication.Instance.RoomManager.GetRoomLayer(layer);
        if (parent != root)
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
            }

            this.AddToActivityRoot(layer);
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
    /// <param name="size">碰撞器大小</param>
    /// <param name="start">起始坐标 (全局)</param>
    /// <param name="startHeight">起始高度</param>
    /// <param name="direction">投抛角度 (0-360)</param>
    /// <param name="xSpeed">移动速度</param>
    /// <param name="ySpeed">下坠速度</param>
    /// <param name="rotate">旋转速度</param>
    /// <param name="bounce">落地时是否回弹</param>
    /// <param name="bounceStrength">落地回弹力度, 1为不消耗能量, 值越小回弹力度越小</param>
    /// <param name="bounceSpeed">落地回弹后的速度, 1为不消速度, 值越小回弹速度消耗越大</param>
    public void Throw(Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed,
        float ySpeed, float rotate, bool bounce = false, float bounceStrength = 0.5f, float bounceSpeed = 0.8f)
    {
        GlobalPosition = start;
        VerticalSpeed = ySpeed;
        RotationDegreesSpeed = rotate;
        Altitude = startHeight;
        // _fallData.Bounce = bounce;
        // _fallData.BounceStrength = bounceStrength;
        // _fallData.BounceSpeed = bounceSpeed;
        //
        // _fallData.RectangleShape.Size = _fallData.Size * 0.5f;
    }

    /// <summary>
    /// 强制停止投抛运动
    /// </summary>
    public void StopThrow()
    {
        _fallData.IsFallOver = true;
        RestoreCollision();
    }

    /// <summary>
    /// 往当前物体上挂载一个组件
    /// </summary>
    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        _components.Add(new KeyValuePair<Type, Component>(typeof(T), component));
        component.ActivityInstance = this;
        return component;
    }

    /// <summary>
    /// 移除一个组件, 并且销毁
    /// </summary>
    /// <param name="component">组件对象</param>
    public void RemoveComponent(Component component)
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i].Value == component)
            {
                _components.RemoveAt(i);
                component.Destroy();
                return;
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
            if (temp.Key == type)
            {
                return temp.Value;
            }
        }

        return null;
    }

    /// <summary>
    /// 根据类型获取一个组件
    /// </summary>
    public T GetComponent<T>() where T : Component
    {
        var component = GetComponent(typeof(T));
        if (component == null) return null;
        return (T)component;
    }
    
    /// <summary>
    /// 每帧调用一次, 为了防止子类覆盖 _Process(), 给 _Process() 加上了 sealed, 子类需要帧循环函数请重写 Process() 函数
    /// </summary>
    public sealed override void _Process(double delta)
    {
        var newDelta = (float)delta;
        Process(newDelta);
        
        //更新组件
        if (_components.Count > 0)
        {
            var arr = _components.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (IsDestroyed) return;
                var temp = arr[i].Value;
                if (temp != null && temp.ActivityInstance == this && temp.Enable)
                {
                    if (!temp.IsReady)
                    {
                        temp.Ready();
                        temp.IsReady = true;
                    }

                    temp.Process(newDelta);
                }
            }
        }

        // 下坠判定
        if (Altitude > 0 || VerticalSpeed != 0)
        {
            if (_fallData == null)
            {
                _fallData = new ActivityThrowData();
            }
            
            if (_fallData.IsFallOver) // 没有处于下坠状态, 则进入下坠状态
            {
                SetFallCollision();

                _fallData.IsFallOver = false;
                _fallData.FirstFall = true;
                _fallData.Size = new Vector2(10, 10);
                _fallData.Bounce = true;
                _fallData.BounceStrength = 0.5f;
                _fallData.BounceSpeed = 0.8f;
                _hasResilienceVerticalSpeed = false;
                _resilienceVerticalSpeed = 0;

                _fallData.RectangleShape.Size = _fallData.Size * 0.5f;

                Throw();
            }
            else
            {
                GlobalRotationDegrees = GlobalRotationDegrees + RotationDegreesSpeed * newDelta;
                CalcThrowAnimatedPosition();

                var ysp = VerticalSpeed;

                Altitude += VerticalSpeed * newDelta;
                VerticalSpeed -= GameConfig.G * newDelta;

                //当高度大于16时, 显示在所有物体上
                if (Altitude >= 16)
                {
                    AnimatedSprite.ZIndex = 20;
                }
                else
                {
                    AnimatedSprite.ZIndex = 0;
                }
            
                //达到最高点
                if (ysp > 0 && ysp * VerticalSpeed < 0)
                {
                    OnThrowMaxHeight(Altitude);
                }

                //落地判断
                if (Altitude <= 0)
                {
                    _fallData.IsFallOver = true;
                    Altitude = 0;

                    //第一次接触地面
                    if (_fallData.FirstFall)
                    {
                        _fallData.FirstFall = false;
                        OnFirstFallToGround();
                    }

                    //如果落地高度不够低, 再抛一次
                    if (_fallData.Bounce && (!_hasResilienceVerticalSpeed || _resilienceVerticalSpeed > 1))
                    {
                        Velocity = Velocity * _fallData.BounceSpeed;
                        if (!_hasResilienceVerticalSpeed)
                        {
                            _hasResilienceVerticalSpeed = true;
                            _resilienceVerticalSpeed = -VerticalSpeed * _fallData.BounceStrength;
                        }
                        else
                        {
                            _resilienceVerticalSpeed = _resilienceVerticalSpeed * _fallData.BounceStrength;
                        }
                        VerticalSpeed = _resilienceVerticalSpeed;
                        RotationDegreesSpeed = RotationDegreesSpeed * _fallData.BounceStrength;
                        _fallData.IsFallOver = false;

                        OnFallToGround();
                    }
                    else //结束
                    {
                        VerticalSpeed = 0;
                        OnFallToGround();
                        ThrowOver();
                    }
                }
            }
        }
        
        /*
        //投抛计算
        if (_fallData != null && !_fallData.IsFallOver)
        {
            GlobalRotationDegrees = GlobalRotationDegrees + _fallData.RotateSpeed * newDelta;
            CalcThrowAnimatedPosition();

            var ysp = _fallData.YSpeed;

            Altitude += _fallData.YSpeed * newDelta;
            _fallData.YSpeed -= GameConfig.G * newDelta;

            //当高度大于16时, 显示在所有物体上
            if (Altitude >= 16)
            {
                AnimatedSprite.ZIndex = 20;
            }
            else
            {
                AnimatedSprite.ZIndex = 0;
            }
            
            //达到最高点
            if (ysp * _fallData.YSpeed < 0)
            {
                OnThrowMaxHeight(Altitude);
            }

            //落地判断
            if (Altitude <= 0)
            {
                _fallData.IsFallOver = true;

                //第一次接触地面
                if (_fallData.FirstFallOver)
                {
                    _fallData.FirstFallOver = false;
                    OnFirstFallToGround();
                }

                //如果落地高度不够低, 再抛一次
                if (_fallData.StartYSpeed > 1 && _fallData.Bounce)
                {
                    _fallData.StartPosition = Position;
                    Altitude = 0;
                    _fallData.XSpeed = _fallData.StartXSpeed = _fallData.StartXSpeed * _fallData.BounceSpeed;
                    _fallData.YSpeed = _fallData.StartYSpeed = _fallData.StartYSpeed * _fallData.BounceStrength;
                    _fallData.RotateSpeed = _fallData.RotateSpeed * _fallData.BounceStrength;
                    _fallData.ThrowForce.Velocity *= _fallData.BounceSpeed;
                    _fallData.FirstFallOver = false;
                    _fallData.IsFallOver = false;

                    OnFallToGround();
                }
                else //结束
                {
                    OnFallToGround();
                    ThrowOver();
                }
            }
        }
        */

        //阴影
        if (ShadowSprite.Visible)
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

            //计算阴影
            CalcShadow();
        }

        // Hit 动画
        if (_playHit && _blendShaderMaterial != null)
        {
            if (_playHitSchedule < 0.05f)
            {
                _blendShaderMaterial.SetShaderParameter("schedule", 1);
            }
            else if (_playHitSchedule < 0.15f)
            {
                _blendShaderMaterial.SetShaderParameter("schedule", Mathf.Lerp(1, 0, (_playHitSchedule - 0.05f) / 0.1f));
            }
            if (_playHitSchedule >= 0.15f)
            {
                _blendShaderMaterial.SetShaderParameter("schedule", 0);
                _playHitSchedule = 0;
                _playHit = false;
            }
            else
            {
                _playHitSchedule += newDelta;
            }
        }
        
        //协程更新
        if (_coroutineList != null)
        {
            var pairs = _coroutineList.ToArray();
            for (var i = 0; i < pairs.Length; i++)
            {
                var item = pairs[i];
                var canNext = true;

                if (item.WaitType == CoroutineData.WaitTypeEnum.WaitForSeconds) //等待秒数
                {
                    if (!item.WaitForSeconds.NextStep(newDelta))
                    {
                        canNext = false;
                    }
                    else
                    {
                        item.WaitType = CoroutineData.WaitTypeEnum.None;
                        item.WaitForSeconds = null;
                    }
                }
                else if (item.WaitType == CoroutineData.WaitTypeEnum.WaitForFixedProcess) //等待帧数
                {
                    if (!item.WaitForFixedProcess.NextStep())
                    {
                        canNext = false;
                    }
                    else
                    {
                        item.WaitType = CoroutineData.WaitTypeEnum.None;
                        item.WaitForFixedProcess = null;
                    }
                }

                if (canNext)
                {
                    if (item.Enumerator.MoveNext()) //嵌套协程
                    {
                        var next = item.Enumerator.Current;
                        if (next is IEnumerable enumerable)
                        {
                            if (item.EnumeratorStack == null)
                            {
                                item.EnumeratorStack = new Stack<IEnumerator>();
                            }

                            item.EnumeratorStack.Push(item.Enumerator);
                            item.Enumerator = enumerable.GetEnumerator();
                        }
                        else if (next is IEnumerator enumerator)
                        {
                            if (item.EnumeratorStack == null)
                            {
                                item.EnumeratorStack = new Stack<IEnumerator>();
                            }

                            item.EnumeratorStack.Push(item.Enumerator);
                            item.Enumerator = enumerator;
                        }
                        else if (next is WaitForSeconds seconds) //等待秒数
                        {
                            item.WaitFor(seconds);
                        }
                        else if (next is WaitForFixedProcess process) //等待帧数
                        {
                            item.WaitFor(process);
                        }
                    }
                    else
                    {
                        if (item.EnumeratorStack == null || item.EnumeratorStack.Count == 0)
                        {
                            StopCoroutine(item.Id);
                        }
                        else
                        {
                            item.Enumerator = item.EnumeratorStack.Pop();
                        }
                    }
                }
            }
        }

        ProcessOver(newDelta);
        
        //调试绘制
        if (IsDebug)
        {
            QueueRedraw();
        }
    }

    /// <summary>
    /// 每物理帧调用一次, 为了防止子类覆盖 _PhysicsProcess(), 给 _PhysicsProcess() 加上了 sealed, 子类需要帧循环函数请重写 PhysicsProcess() 函数
    /// </summary>
    public sealed override void _PhysicsProcess(double delta)
    {
        var newDelta = (float)delta;
        PhysicsProcess(newDelta);
        
        //更新组件
        if (_components.Count > 0)
        {
            var arr = _components.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (IsDestroyed) return;
                var temp = arr[i].Value;
                if (temp != null && temp.ActivityInstance == this && temp.Enable)
                {
                    if (!temp.IsReady)
                    {
                        temp.Ready();
                        temp.IsReady = true;
                    }

                    temp.PhysicsProcess(newDelta);
                }
            }
        }

        PhysicsProcessOver(newDelta);
    }

    /// <summary>
    /// 绘制函数, 子类不允许重写, 需要绘制函数请重写 DebugDraw()
    /// </summary>
    public sealed override void _Draw()
    {
        if (IsDebug)
        {
            DebugDraw();
            var arr = _components.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (IsDestroyed) return;
                var temp = arr[i].Value;
                if (temp != null && temp.ActivityInstance == this && temp.Enable)
                {
                    temp.DebugDraw();
                }
            }
        }
    }

    /// <summary>
    /// 重新计算物体阴影的位置和旋转信息, 无论是否显示阴影
    /// </summary>
    public void CalcShadow()
    {
        //缩放
        ShadowSprite.Scale = AnimatedSprite.Scale;
        //阴影角度
        ShadowSprite.Rotation = 0;
        //阴影位置计算
        var pos = AnimatedSprite.GlobalPosition;
        if (_fallData != null && !_fallData.IsFallOver)
        {
            ShadowSprite.GlobalPosition = new Vector2(pos.X + ShadowOffset.X, pos.Y + ShadowOffset.Y + Altitude);
        }
        else
        {
            ShadowSprite.GlobalPosition = pos + ShadowOffset;
        }
    }
    
    //计算位置
    private void CalcThrowAnimatedPosition()
    {
        if (Scale.Y < 0)
        {
            AnimatedSprite.GlobalPosition = GlobalPosition + new Vector2(0, -Altitude) - _fallData.OriginSpritePosition.Rotated(Rotation) * Scale.Abs();
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
        QueueFree();
        OnDestroy();

        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Value?.Destroy();
        }
        
        if (Affiliation != null)
        {
            Affiliation.RemoveItem(this);
        }
        
        //临时处理, 4.0 有bug, 不能提前销毁模板实例, 不然关闭游戏会报错!!!
        _templateInstance.QueueFree();
    }

    /// <summary>
    /// 延时销毁
    /// </summary>
    public void DelayDestroy()
    {
        CallDeferred(nameof(Destroy));
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
        else if (parent == GameApplication.Instance.RoomManager.NormalLayer)
        {
            parent.RemoveChild(this);
            this.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        }

        CalcThrowAnimatedPosition();
        //显示阴影
        ShowShadowSprite();
        OnThrowStart();
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

            if (_fallData.RectangleShape == null)
            {
                _fallData.RectangleShape = new RectangleShape2D();
            }

            Collision.Shape = _fallData.RectangleShape;
            Collision.Position = Vector2.Zero;
            Collision.Rotation = 0;
            Collision.Scale = Vector2.One;
            ZIndex = 0;
            Collision.Disabled = false;
            Collision.Position = Vector2.Zero;
            Collision.Rotation = 0;
            Collision.Scale = Vector2.One;
            CollisionMask = 1;
            CollisionLayer = 0;
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
        var roomLayer = GameApplication.Instance.RoomManager.GetRoomLayer(_currLayer);
        if (parent != roomLayer)
        {
            parent.RemoveChild(this);
            roomLayer.AddChild(this);
        }
        RestoreCollision();

        OnThrowOver();
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
    /// 开启一个协程, 返回协程 id, 协程是在普通帧执行的, 支持: 协程嵌套, WaitForSeconds, WaitForFixedProcess
    /// </summary>
    public long StartCoroutine(IEnumerator able)
    {
        if (_coroutineList == null)
        {
            _coroutineList = new List<CoroutineData>();
        }

        var data = new CoroutineData(able);
        _coroutineList.Add(data);
        return data.Id;
    }
    
    /// <summary>
    /// 开启一个协程, 返回协程 id, 协程是在普通帧执行的, 支持: 协程嵌套, WaitForSeconds, WaitForFixedProcess
    /// </summary>
    public long StartCoroutine(IEnumerable able)
    {
        return StartCoroutine(able.GetEnumerator());
    }

    /// <summary>
    /// 根据协程 id 停止协程
    /// </summary>
    public void StopCoroutine(long coroutineId)
    {
        if (_coroutineList != null)
        {
            for (var i = 0; i < _coroutineList.Count; i++)
            {
                var item = _coroutineList[i];
                if (item.Id == coroutineId)
                {
                    _coroutineList.RemoveAt(i);
                    return;
                }
            }
        }
    }
    
    /// <summary>
    /// 停止所有协程
    /// </summary>
    public void StopAllCoroutine()
    {
        if (_coroutineList != null)
        {
            _coroutineList.Clear();
        }
    }
}