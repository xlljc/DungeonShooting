using System.Collections.Generic;
using Godot;

/// <summary>
/// 角色基类
/// </summary>
public abstract class Role : ActivityObject
{
    /// <summary>
    /// 默认攻击对象层级
    /// </summary>
    public const uint DefaultAttackLayer = PhysicsLayer.Player | PhysicsLayer.Enemy | PhysicsLayer.Wall | PhysicsLayer.Props;
    
    /// <summary>
    /// 动画播放器
    /// </summary>
    public AnimationPlayer AnimationPlayer { get; private set; }
    
    /// <summary>
    /// 重写的纹理
    /// </summary>
    public Texture OverrideTexture { get; protected set; }

    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed = 120f;

    /// <summary>
    /// 所属阵营
    /// </summary>
    public CampEnum Camp;

    /// <summary>
    /// 攻击目标的碰撞器所属层级, 数据源自于: PhysicsLayer
    /// </summary>
    public uint AttackLayer { get; set; } = PhysicsLayer.Wall;

    /// <summary>
    /// 携带的道具包裹
    /// </summary>
    public List<object> PropsPack { get; } = new List<object>();

    /// <summary>
    /// 角色携带的枪套
    /// </summary>
    public Holster Holster { get; }

    /// <summary>
    /// 武器挂载点
    /// </summary>
    public MountRotation MountPoint { get; private set; }
    /// <summary>
    /// 背后武器的挂载点
    /// </summary>
    public Position2D BackMountPoint { get; private set; }

    /// <summary>
    /// 互动碰撞区域
    /// </summary>
    public Area2D InteractiveArea { get; private set; }
    
    /// <summary>
    /// 脸的朝向
    /// </summary>
    public FaceDirection Face { get => _face; set => SetFace(value); }
    private FaceDirection _face;

    /// <summary>
    /// 血量
    /// </summary>
    public int Hp
    {
        get => _hp;
        protected set
        {
            int temp = _hp;
            _hp = value;
            if (temp != _hp) OnChangeHp(_hp);
        }
    }
    private int _hp = 0;

    /// <summary>
    /// 最大血量
    /// </summary>
    public int MaxHp
    {
        get => _maxHp;
        protected set
        {
            int temp = _maxHp;
            _maxHp = value;
            if (temp != _maxHp) OnChangeMaxHp(_maxHp);
        }
    }
    private int _maxHp = 0;

    /// <summary>
    /// 当前角色所看向的对象, 也就是枪口指向的对象
    /// </summary>
    public ActivityObject LookTarget { get; set; }
    
    //初始缩放
    private Vector2 StartScele;
    //所有角色碰撞的道具
    private readonly List<ActivityObject> InteractiveItemList = new List<ActivityObject>();

    private CheckInteractiveResult TempResultData;

    /// <summary>
    /// 可以互动的道具
    /// </summary>
    protected ActivityObject InteractiveItem { get; private set; }

    /// <summary>
    /// 当血量改变时调用
    /// </summary>
    protected virtual void OnChangeHp(int hp)
    {
    }

    /// <summary>
    /// 当最大血量改变时调用
    /// </summary>
    protected virtual void OnChangeMaxHp(int maxHp)
    {
    }

    /// <summary>
    /// 当受伤时调用
    /// </summary>
    /// <param name="damage">受到的伤害</param>
    public virtual void OnHit(int damage)
    {
    }

    /// <summary>
    /// 当可互动的物体改变时调用, result 参数为 null 表示变为不可互动
    /// </summary>
    /// <param name="result">检测是否可互动时的返回值</param>
    protected virtual void ChangeInteractiveItem(CheckInteractiveResult result)
    {
    }

    public Role() : this(ResourcePath.prefab_role_Role_tscn)
    {
    }
    
    public Role(string scenePath) : base(scenePath)
    {
        Holster = new Holster(this);
    }
    
    public override void _Ready()
    {
        base._Ready();
        AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        StartScele = Scale;
        MountPoint = GetNode<MountRotation>("MountPoint");
        MountPoint.Master = this;
        BackMountPoint = GetNode<Position2D>("BackMountPoint");
        //即将弃用
        if (OverrideTexture != null)
        {
            // 更改纹理
            ChangeFrameTexture(AnimatorNames.Idle, AnimatedSprite, OverrideTexture);
            ChangeFrameTexture(AnimatorNames.Run, AnimatedSprite, OverrideTexture);
            ChangeFrameTexture(AnimatorNames.ReverseRun, AnimatedSprite, OverrideTexture);
        }
        Face = FaceDirection.Right;

        //连接互动物体信号
        InteractiveArea = GetNode<Area2D>("InteractiveArea");
        InteractiveArea.Connect("area_entered", this, nameof(_OnPropsEnter));
        InteractiveArea.Connect("area_exited", this, nameof(_OnPropsExit));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        
        //看向目标
        if (LookTarget != null)
        {
            Vector2 pos = LookTarget.GlobalPosition;
            //脸的朝向
            var gPos = GlobalPosition;
            if (pos.x > gPos.x && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (pos.x < gPos.x && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随目标
            MountPoint.SetLookAt(pos);
        }
        
        //检查可互动的道具
        bool findFlag = false;
        for (int i = 0; i < InteractiveItemList.Count; i++)
        {
            var item = InteractiveItemList[i];
            if (item == null)
            {
                InteractiveItemList.RemoveAt(i--);
            }
            else
            {
                //找到可互动的道具了
                if (!findFlag)
                {
                    var result = item.CheckInteractive(this);
                    if (result.CanInteractive) //可以互动
                    {
                        findFlag = true;
                        if (InteractiveItem != item) //更改互动物体
                        {
                            InteractiveItem = item;
                            ChangeInteractiveItem(result);
                        }
                        else if (result.ShowIcon != TempResultData.ShowIcon) //切换状态
                        {
                            ChangeInteractiveItem(result);
                        }
                    }
                    TempResultData = result;
                }
            }
        }
        //没有可互动的道具
        if (!findFlag && InteractiveItem != null)
        {
            InteractiveItem = null;
            ChangeInteractiveItem(null);
        }
    }

    /// <summary>
    /// 拾起一个武器, 并且切换到这个武器
    /// </summary>
    /// <param name="weapon">武器对象</param>
    public virtual void PickUpWeapon(Weapon weapon)
    {
        if (Holster.PickupWeapon(weapon) != -1)
        {
            //从可互动队列中移除
            InteractiveItemList.Remove(weapon);
        }
    }

    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public virtual void ExchangeNext()
    {
        Holster.ExchangeNext();
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public virtual void ExchangePrev()
    {
        Holster.ExchangePrev();
    }

    /// <summary>
    /// 扔掉当前使用的武器, 切换到上一个武器
    /// </summary>
    public virtual void ThrowWeapon()
    {
        var weapon = Holster.RemoveWeapon(Holster.ActiveIndex);
        //播放抛出效果
        if (weapon != null)
        {
            weapon.ThrowWeapon(this);
        }
    }

    /// <summary>
    /// 返回是否存在可互动的物体
    /// </summary>
    public bool HasInteractive()
    {
        return InteractiveItem != null;
    }

    /// <summary>
    /// 触发与碰撞的物体互动, 并返回与其互动的物体
    /// </summary>
    public ActivityObject TriggerInteractive()
    {
        if (HasInteractive())
        {
            var item = InteractiveItem;
            item.Interactive(this);
            return item;
        }
        return null;
    }

    /// <summary>
    /// 触发换弹
    /// </summary>
    public virtual void Reload()
    {
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon.Reload();
        }
    }

    /// <summary>
    /// 触发攻击
    /// </summary>
    public virtual void Attack()
    {
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon.Trigger();
        }
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害的量</param>
    public virtual void Hit(int damage)
    {
        Hp -= damage;
        AnimationPlayer.Stop();
        AnimationPlayer.Play("hit");
        OnHit(damage);
    }

    /// <summary>
    /// 设置脸的朝向
    /// </summary>
    private void SetFace(FaceDirection face)
    {
        if (_face != face)
        {
            _face = face;
            if (face == FaceDirection.Right)
            {
                RotationDegrees = 0;
                Scale = StartScele;
            }
            else
            {
                RotationDegrees = 180;
                Scale = new Vector2(StartScele.x, -StartScele.y);
            }
        }
    }

    /// <summary>
    /// 更改指定动画的纹理
    /// </summary>
    private void ChangeFrameTexture(string anim, AnimatedSprite animatedSprite, Texture texture)
    {
        SpriteFrames spriteFrames = animatedSprite.Frames;
        if (spriteFrames != null)
        {
            int count = spriteFrames.GetFrameCount(anim);
            for (int i = 0; i < count; i++)
            {
                AtlasTexture temp = spriteFrames.GetFrame(anim, i) as AtlasTexture;
                temp.Atlas = OverrideTexture;
            }
        }
    }

    /// <summary>
    /// 连接信号: InteractiveArea.area_entered
    /// 与物体碰撞
    /// </summary>
    private void _OnPropsEnter(Area2D other)
    {
        ActivityObject propObject = other.AsActivityObject();
        if (propObject != null)
        {
            if (!InteractiveItemList.Contains(propObject))
            {
                InteractiveItemList.Add(propObject);
            }
        }
    }

    /// <summary>
    /// 连接信号: InteractiveArea.area_exited
    /// 物体离开碰撞区域
    /// </summary>
    private void _OnPropsExit(Area2D other)
    {
        ActivityObject propObject = other.AsActivityObject();
        if (propObject != null)
        {
            if (InteractiveItemList.Contains(propObject))
            {
                InteractiveItemList.Remove(propObject);
            }
            if (InteractiveItem == propObject)
            {
                InteractiveItem = null;
                ChangeInteractiveItem(null);
            }
        }
    }
}