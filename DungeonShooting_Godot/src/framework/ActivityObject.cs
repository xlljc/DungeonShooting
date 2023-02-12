
using System;
using System.Collections.Generic;
using Godot;
using Plugin;

/// <summary>
/// 房间内活动物体基类
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
    public AnimatedSprite2D AnimatedSprite { get; }

    /// <summary>
    /// 当前物体显示的阴影图像, 节点名称必须叫 "ShadowSprite", 类型为 Sprite2D
    /// </summary>
    public Sprite2D ShadowSprite { get; }

    /// <summary>
    /// 当前物体碰撞器节点, 节点名称必须叫 "Collision", 类型为 CollisionShape2D
    /// </summary>
    public CollisionShape2D Collision { get; }

    /// <summary>
    /// 动画播放器
    /// </summary>
    /// <value></value>
    public AnimationPlayer AnimationPlayer { get; }

    /// <summary>
    /// 是否调用过 Destroy() 函数
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 是否正在投抛过程中
    /// </summary>
    public bool IsThrowing => _throwData != null && !_throwData.IsOver;

    /// <summary>
    /// 阴影偏移
    /// </summary>
    public Vector2 ShadowOffset { get; protected set; } = new Vector2(0, 2);
    
    /// <summary>
    /// 移动控制器
    /// </summary>
    public MoveController MoveController { get; }

    /// <summary>
    /// 物体移动基础速率
    /// </summary>
    public Vector2 BasisVelocity
    {
        get => MoveController.BasisVelocity;
        set => MoveController.BasisVelocity = value;
    }

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
    private ObjectThrowData _throwData;
    
    //所在层级
    private RoomLayerEnum _currLayer;
    
    //标记字典
    private Dictionary<string, object> _signMap;

    private static long _index = 0;
    
    public ActivityObject(string scenePath)
    {
        Name = GetType().Name + (_index++);
        //加载预制体
        var tempPrefab = ResourceManager.Load<PackedScene>(scenePath);
        if (tempPrefab == null)
        {
            throw new Exception("创建 ActivityObject 没有找到指定挂载的预制体: " + scenePath);
        }

        var tempNode = tempPrefab.Instantiate<ActivityObjectTemplate>();
        ZIndex = tempNode.z_index;
        CollisionLayer = tempNode.collision_layer;
        CollisionMask = tempNode.collision_mask;
        Scale = tempNode.scale;
        Visible = tempNode.visible;

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
                case "AnimationPlayer":
                    AnimationPlayer = (AnimationPlayer)body;
                    break;
            }
        }
        
        MoveController = AddComponent<MoveController>();
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
    /// 将一个节点扔到地上, 并设置显示的阴影
    /// <param name="layer">放入的层</param>
    /// </summary>
    public virtual void PutDown(RoomLayerEnum layer)
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

    /// <summary>
    /// 将一个节点扔到地上, 并设置显示的阴影
    /// </summary>
    /// <param name="position">放置的位置</param>
    /// <param name="layer">放入的层</param>
    public void PutDown(Vector2 position, RoomLayerEnum layer)
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
        if (_throwData == null)
        {
            _throwData = new ObjectThrowData();
        }

        SetThrowCollision();

        _throwData.IsOver = false;
        _throwData.Size = size;
        _throwData.StartPosition = _throwData.CurrPosition = start;
        _throwData.Direction = direction;
        _throwData.XSpeed = xSpeed;
        _throwData.YSpeed = ySpeed;
        _throwData.StartXSpeed = xSpeed;
        _throwData.StartYSpeed = ySpeed;
        _throwData.RotateSpeed = rotate;
        _throwData.ThrowForce = MoveController.AddConstantForce("ThrowForce");
        _throwData.ThrowForce.Velocity =
            new Vector2(_throwData.XSpeed, 0).Rotated(_throwData.Direction * Mathf.Pi / 180);
        _throwData.Y = startHeight;
        _throwData.Bounce = bounce;
        _throwData.BounceStrength = bounceStrength;
        _throwData.BounceSpeed = bounceSpeed;

        _throwData.RectangleShape.Size = _throwData.Size * 0.5f;

        Throw();
    }

    /// <summary>
    /// 强制停止投抛运动
    /// </summary>
    public void StopThrow()
    {
        _throwData.IsOver = true;
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

        //投抛计算
        if (_throwData != null && !_throwData.IsOver)
        {
            GlobalRotationDegrees = GlobalRotationDegrees + _throwData.RotateSpeed * newDelta;
            CalcThrowAnimatedPosition();

            var ysp = _throwData.YSpeed;

            _throwData.Y += _throwData.YSpeed * newDelta;
            _throwData.YSpeed -= GameConfig.G * newDelta;

            //当高度大于16时, 显示在所有物体上
            if (_throwData.Y >= 16)
            {
                AnimatedSprite.ZIndex = 20;
            }
            else
            {
                AnimatedSprite.ZIndex = 0;
            }
            
            //达到最高点
            if (ysp * _throwData.YSpeed < 0)
            {
                OnThrowMaxHeight(_throwData.Y);
            }

            //落地判断
            if (_throwData.Y <= 0)
            {

                _throwData.IsOver = true;

                //第一次接触地面
                if (_throwData.FirstOver)
                {
                    _throwData.FirstOver = false;
                    OnFirstFallToGround();
                }

                //如果落地高度不够低, 再抛一次
                if (_throwData.StartYSpeed > 1 && _throwData.Bounce)
                {
                    _throwData.StartPosition = Position;
                    _throwData.Y = 0;
                    _throwData.XSpeed = _throwData.StartXSpeed = _throwData.StartXSpeed * _throwData.BounceSpeed;
                    _throwData.YSpeed = _throwData.StartYSpeed = _throwData.StartYSpeed * _throwData.BounceStrength;
                    _throwData.RotateSpeed = _throwData.RotateSpeed * _throwData.BounceStrength;
                    _throwData.ThrowForce.Velocity *= _throwData.BounceSpeed;
                    _throwData.FirstOver = false;
                    _throwData.IsOver = false;

                    OnFallToGround();
                }
                else //结束
                {
                    OnFallToGround();
                    ThrowOver();
                }
            }
        }

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
        if (_throwData != null && !_throwData.IsOver)
        {
            ShadowSprite.GlobalPosition = new Vector2(pos.X + ShadowOffset.X, pos.Y + ShadowOffset.Y + _throwData.Y);
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
            AnimatedSprite.GlobalPosition = GlobalPosition + new Vector2(0, -_throwData.Y) - _throwData.OriginSpritePosition.Rotated(Rotation) * Scale.Abs();
        }
        else
        {
            AnimatedSprite.GlobalPosition = GlobalPosition + new Vector2(0, -_throwData.Y) + _throwData.OriginSpritePosition.Rotated(Rotation);
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

        OnDestroy();
        QueueFree();
        var arr = _components.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Value?.Destroy();
        }
    }

    /// <summary>
    /// 延时销毁
    /// </summary>
    public void DelayDestroy()
    {
        CallDeferred(nameof(Destroy));
    }

    //返回该组件是否被挂载到当前物体上
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

        GlobalPosition = _throwData.StartPosition;

        CalcThrowAnimatedPosition();
        //显示阴影
        ShowShadowSprite();
        OnThrowStart();
    }

    /// <summary>
    /// 设置投抛状态下的碰撞器
    /// </summary>
    private void SetThrowCollision()
    {
        if (_throwData != null && _throwData.UseOrigin)
        {
            _throwData.OriginShape = Collision.Shape;
            _throwData.OriginPosition = Collision.Position;
            _throwData.OriginRotation = Collision.Rotation;
            _throwData.OriginScale = Collision.Scale;
            _throwData.OriginZIndex = ZIndex;
            _throwData.OriginSpritePosition = AnimatedSprite.Position;
            _throwData.OriginCollisionEnable = Collision.Disabled;
            _throwData.OriginCollisionPosition = Collision.Position;
            _throwData.OriginCollisionRotation = Collision.Rotation;
            _throwData.OriginCollisionScale = Collision.Scale;
            _throwData.OriginCollisionMask = CollisionMask;
            _throwData.OriginCollisionLayer = CollisionLayer;

            if (_throwData.RectangleShape == null)
            {
                _throwData.RectangleShape = new RectangleShape2D();
            }

            Collision.Shape = _throwData.RectangleShape;
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
            _throwData.UseOrigin = false;
        }
    }

    /// <summary>
    /// 重置碰撞器
    /// </summary>
    private void RestoreCollision()
    {
        if (_throwData != null && !_throwData.UseOrigin)
        {
            Collision.Shape = _throwData.OriginShape;
            Collision.Position = _throwData.OriginPosition;
            Collision.Rotation = _throwData.OriginRotation;
            Collision.Scale = _throwData.OriginScale;
            ZIndex = _throwData.OriginZIndex;
            AnimatedSprite.Position = _throwData.OriginSpritePosition;
            Collision.Disabled = _throwData.OriginCollisionEnable;
            Collision.Position = _throwData.OriginCollisionPosition;
            Collision.Rotation = _throwData.OriginCollisionRotation;
            Collision.Scale = _throwData.OriginCollisionScale;
            CollisionMask = _throwData.OriginCollisionMask;
            CollisionLayer = _throwData.OriginCollisionLayer;

            _throwData.UseOrigin = true;
        }
    }

    /// <summary>
    /// 投抛结束
    /// </summary>
    private void ThrowOver()
    {
        //移除投抛的力
        MoveController.RemoveForce(_throwData.ThrowForce);
        
        GetParent().RemoveChild(this);
        this.AddToActivityRoot(_currLayer);
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
    /// 通过 ItemId 实例化 ActivityObject 对象
    /// </summary>
    public static T Create<T>(string itemId) where T : ActivityObject
    {
        return null;
    }
}