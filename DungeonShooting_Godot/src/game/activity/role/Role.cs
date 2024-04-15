
using System;
using System.Collections;
using System.Collections.Generic;
using Config;
using Godot;

/// <summary>
/// 角色基类
/// </summary>
public abstract partial class Role : ActivityObject
{
    /// <summary>
    /// 攻击目标的碰撞器所属层级, 数据源自于: <see cref="PhysicsLayer"/>
    /// </summary>
    public const uint AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Obstacle | PhysicsLayer.HurtArea;
    
    /// <summary>
    /// 当前角色对其他角色造成伤害时对回调
    /// 参数1为目标角色
    /// 参数2为造成对伤害值
    /// </summary>
    public event Action<Role, int> OnDamageEvent;
    
    /// <summary>
    /// 是否是 Ai
    /// </summary>
    public bool IsAi { get; protected set; } = false;

    /// <summary>
    /// 角色属性
    /// </summary>
    public RoleState RoleState { get; private set; }
    
    /// <summary>
    /// 伤害区域
    /// </summary>
    [Export, ExportFillNode]
    public HurtArea HurtArea { get; set; }
    
    /// <summary>
    /// 伤害区域碰撞器
    /// </summary>
    [Export, ExportFillNode]
    public CollisionShape2D HurtCollision { get; set; }

    /// <summary>
    /// 所属阵营
    /// </summary>
    public CampEnum Camp { get; set; }

    /// <summary>
    /// 携带的被动道具列表
    /// </summary>
    public List<BuffProp> BuffPropPack { get; } = new List<BuffProp>();

    /// <summary>
    /// 携带的主动道具包裹
    /// </summary>
    public Package<ActiveProp, Role> ActivePropsPack { get; private set; }
    
    /// <summary>
    /// 互动碰撞区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D InteractiveArea { get; set; }
    
    /// <summary>
    /// 互动区域碰撞器
    /// </summary>
    [Export, ExportFillNode]
    public CollisionShape2D InteractiveCollision { get; set; }
    
    /// <summary>
    /// 用于提示状态的根节点
    /// </summary>
    [Export, ExportFillNode]
    public Node2D TipRoot { get; set; }
    
    /// <summary>
    /// 用于提示当前敌人状态
    /// </summary>
    [Export, ExportFillNode]
    public AnimatedSprite2D TipSprite { get; set; }
    
    /// <summary>
    /// 动画播放器
    /// </summary>
    [Export, ExportFillNode]
    public AnimationPlayer AnimationPlayer { get; set; }
    
    /// <summary>
    /// 脸的朝向
    /// </summary>
    public FaceDirection Face { get => _face; set => _SetFace(value); }
    private FaceDirection _face;
    
    /// <summary>
    /// 角色携带的武器背包
    /// </summary>
    public Package<Weapon, Role> WeaponPack { get; private set; }

    /// <summary>
    /// 武器挂载点
    /// </summary>
    [Export, ExportFillNode]
    public MountRotation MountPoint { get; set; }

    /// <summary>
    /// 背后武器的挂载点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D BackMountPoint { get; set; }

    /// <summary>
    /// 近战碰撞检测区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D MeleeAttackArea { get; set; }
    
    /// <summary>
    /// 近战碰撞检测区域的碰撞器
    /// </summary>
    [Export, ExportFillNode]
    public CollisionPolygon2D MeleeAttackCollision { get; set; }

    /// <summary>
    /// 近战攻击时挥动武器的角度
    /// </summary>
    [Export]
    public float MeleeAttackAngle { get; set; } = 120;

    /// <summary>
    /// 武器挂载点是否始终指向目标
    /// </summary>
    public bool MountLookTarget { get; set; } = true;

    /// <summary>
    /// 是否处于攻击中, 近战攻击远程攻击都算
    /// </summary>
    public bool IsAttack
    {
        get
        {
            if (AttackTimer > 0 || MeleeAttackTimer > 0)
            {
                return true;
            }
            var weapon = WeaponPack.ActiveItem;
            if (weapon != null)
            {
                return weapon.GetAttackTimer() > 0 || weapon.GetContinuousCount() > 0;
            }
            return false;
        }
    }

    /// <summary>
    /// 攻击计时器
    /// </summary>
    public float AttackTimer { get; set; }
    
    /// <summary>
    /// 近战计时器
    /// </summary>
    public float MeleeAttackTimer { get; set; }

    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool IsDie { get; private set; }
    
    /// <summary>
    /// 血量
    /// </summary>
    public int Hp
    {
        get => _hp;
        set
        {
            int temp = _hp;
            _hp = Mathf.Clamp(value, 0, _maxHp);
            if (temp != _hp)
            {
                OnChangeHp(_hp);
            }
        }
    }
    private int _hp = 20;

    /// <summary>
    /// 最大血量
    /// </summary>
    public int MaxHp
    {
        get => _maxHp;
        set
        {
            int temp = _maxHp;
            _maxHp = value;
            //最大血量值改变
            if (temp != _maxHp)
            {
                OnChangeMaxHp(_maxHp);
            }
            //调整血量
            if (Hp > _maxHp)
            {
                Hp = _maxHp;
            }
        }
    }
    private int _maxHp = 20;
    
    /// <summary>
    /// 当前护盾值
    /// </summary>
    public int Shield
    {
        get => _shield;
        set
        {
            int temp = _shield;
            _shield = value;
            //护盾被破坏
            if (temp > 0 && _shield <= 0 && _maxShield > 0)
            {
                OnShieldDestroy();
            }
            //护盾值改变
            if (temp != _shield)
            {
                OnChangeShield(_shield);
            }
        }
    }
    private int _shield = 0;

    /// <summary>
    /// 最大护盾值
    /// </summary>
    public int MaxShield
    {
        get => _maxShield;
        set
        {
            int temp = _maxShield;
            _maxShield = value;
            //最大护盾值改变
            if (temp != _maxShield)
            {
                OnChangeMaxShield(_maxShield);
            }
            //调整护盾值
            if (Shield > _maxShield)
            {
                Shield = _maxShield;
            }
        }
    }
    private int _maxShield = 0;

    /// <summary>
    /// 无敌状态
    /// </summary>
    public bool Invincible
    {
        get => _invincible;
        set
        {
            if (_invincible != value)
            {
                if (value) //无敌状态
                {
                    _flashingInvincibleTimer = -1;
                    _flashingInvincibleFlag = false;
                }
                else //正常状态
                {
                    SetBlendModulate(new Color(1, 1, 1, 1));
                }
            }

            _invincible = value;
        }
    }

    private bool _invincible = false;
    
    /// <summary>
    /// 当前可以互动的物体
    /// </summary>
    public ActivityObject InteractiveItem { get; private set; }
    
    /// <summary>
    /// 瞄准辅助线, 需要手动调用 InitSubLine() 初始化
    /// </summary>
    public SubLine SubLine { get; private set; }
    
    /// <summary>
    /// 所有角色碰撞的物体
    /// </summary>
    public List<ActivityObject> InteractiveItemList { get; } = new List<ActivityObject>();
    
    /// <summary>
    /// 角色看向的坐标
    /// </summary>
    public Vector2 LookPosition { get; protected set; }
    
    /// <summary>
    /// 是否可以在没有武器时发动攻击
    /// </summary>
    public bool NoWeaponAttack { get; set; }
    
    /// <summary>
    /// 上一次受到伤害的角度, 弧度制
    /// </summary>
    public float PrevHitAngle { get; private set; }
    
    //初始缩放
    private Vector2 _startScale;
    //当前可互动的物体
    private CheckInteractiveResult _currentResultData;
    //闪烁计时器
    private float _flashingInvincibleTimer = -1;
    //闪烁状态
    private bool _flashingInvincibleFlag = false;
    //闪烁动画协程id
    private long _invincibleFlashingId = -1;
    //护盾恢复计时器
    private float _shieldRecoveryTimer = 0;

    protected override void OnExamineExportFillNode(string propertyName, Node node, bool isJustCreated)
    {
        base.OnExamineExportFillNode(propertyName, node, isJustCreated);
        if (propertyName == nameof(TipSprite))
        {
            var sprite = (AnimatedSprite2D)node;
            sprite.SpriteFrames =
                ResourceManager.Load<SpriteFrames>(ResourcePath.resource_spriteFrames_role_Role_tip_tres);
        }
    }

    /// <summary>
    /// 创建角色的 RoleState 对象
    /// </summary>
    protected virtual RoleState OnCreateRoleState()
    {
        return new RoleState();
    }
    
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
    /// 护盾值改变时调用
    /// </summary>
    protected virtual void OnChangeShield(int shield)
    {
    }

    /// <summary>
    /// 最大护盾值改变时调用
    /// </summary>
    protected virtual void OnChangeMaxShield(int maxShield)
    {
    }

    /// <summary>
    /// 当护盾被破坏时调用
    /// </summary>
    protected virtual void OnShieldDestroy()
    {
    }

    /// <summary>
    /// 当受伤时调用
    /// </summary>
    /// <param name="target">触发伤害的对象, 为 null 表示不存在对象或者对象已经被销毁</param>
    /// <param name="damage">受到的伤害</param>
    /// <param name="angle">伤害角度（弧度制）</param>
    /// <param name="realHarm">是否受到真实伤害, 如果为false, 则表示该伤害被护盾格挡掉了</param>
    protected virtual void OnHit(ActivityObject target, int damage, float angle, bool realHarm)
    {
    }

    /// <summary>
    /// 受到伤害时调用, 用于改变受到的伤害值
    /// </summary>
    /// <param name="damage">受到的伤害</param>
    protected virtual int OnHandlerHurt(int damage)
    {
        return damage;
    }
    
    /// <summary>
    /// 当可互动的物体改变时调用, result 参数为 null 表示变为不可互动
    /// </summary>
    /// <param name="prev">上一个互动的物体</param>
    /// <param name="result">当前物体, 检测是否可互动时的返回值</param>
    protected virtual void ChangeInteractiveItem(CheckInteractiveResult prev, CheckInteractiveResult result)
    {
    }

    /// <summary>
    /// 死亡时调用
    /// </summary>
    protected virtual void OnDie()
    {
    }
    
    
    /// <summary>
    /// 当拾起某个主动道具时调用
    /// </summary>
    protected virtual void OnPickUpActiveProp(ActiveProp activeProp)
    {
    }

    /// <summary>
    /// 当移除某个主动道具时调用
    /// </summary>
    protected virtual void OnRemoveActiveProp(ActiveProp activeProp)
    {
    }
    
    /// <summary>
    /// 当切换到某个主动道具时调用
    /// </summary>
    protected virtual void OnExchangeActiveProp(ActiveProp activeProp)
    {
    }
    
    /// <summary>
    /// 当拾起某个被动道具时调用
    /// </summary>
    protected virtual void OnPickUpBuffProp(BuffProp buffProp)
    {
    }

    /// <summary>
    /// 当移除某个被动道具时调用
    /// </summary>
    protected virtual void OnRemoveBuffProp(BuffProp buffProp)
    {
    }
    
    public override void EnterTree()
    {
        if (!World.Role_InstanceList.Contains(this))
        {
            World.Role_InstanceList.Add(this);
        }
    }

    public override void ExitTree()
    {
        World.Role_InstanceList.Remove(this);
    }

    
    public override void OnInit()
    {
        RoleState = OnCreateRoleState();
        ActivePropsPack = AddComponent<Package<ActiveProp, Role>>();
        ActivePropsPack.SetCapacity(RoleState.CanPickUpWeapon ? 1 : 0);
        
        _startScale = Scale;
        
        HurtArea.InitRole(this);
        
        Face = FaceDirection.Right;
        
        //连接互动物体信号
        InteractiveArea.BodyEntered += _OnPropsEnter;
        InteractiveArea.BodyExited += _OnPropsExit;
        
        //------------------------
        
        WeaponPack = AddComponent<Package<Weapon, Role>>();
        WeaponPack.SetCapacity(2);
        
        MountPoint.Master = this;
        
        MeleeAttackCollision.Disabled = true;
        //切换武器回调
        WeaponPack.ChangeActiveItemEvent += OnChangeActiveItem;
        //近战区域进入物体
        MeleeAttackArea.BodyEntered += OnMeleeAttackBodyEntered;
        MeleeAttackArea.AreaEntered += OnMeleeAttackAreaEntered;
    }

    protected override void Process(float delta)
    {
        if (IsDie)
        {
            return;
        }

        if (AttackTimer > 0)
        {
            AttackTimer -= delta;
        }

        if (MeleeAttackTimer > 0)
        {
            MeleeAttackTimer -= delta;
        }
        
        //检查可互动的物体
        bool findFlag = false;
        for (int i = 0; i < InteractiveItemList.Count; i++)
        {
            var item = InteractiveItemList[i];
            if (item == null || item.IsDestroyed)
            {
                InteractiveItemList.RemoveAt(i--);
            }
            else if (!item.IsThrowing)
            {
                //找到可互动的物体了
                if (!findFlag)
                {
                    var result = item.CheckInteractive(this);
                    var prev = _currentResultData;
                    _currentResultData = result;
                    if (result.CanInteractive) //可以互动
                    {
                        findFlag = true;
                        if (InteractiveItem != item) //更改互动物体
                        {
                            InteractiveItem = item;
                            ChangeInteractiveItem(prev, result);
                        }
                        else if (result.Type != _currentResultData.Type) //切换状态
                        {
                            ChangeInteractiveItem(prev, result);
                        }
                    }
                    
                }
            }
        }
        //没有可互动的物体
        if (!findFlag && InteractiveItem != null)
        {
            var prev = _currentResultData;
            _currentResultData = null;
            InteractiveItem = null;
            ChangeInteractiveItem(prev, null);
        }

        //无敌状态, 播放闪烁动画
        if (Invincible)
        {
            _flashingInvincibleTimer -= delta;
            if (_flashingInvincibleTimer <= 0)
            {
                _flashingInvincibleTimer = 0.15f;
                if (_flashingInvincibleFlag)
                {
                    _flashingInvincibleFlag = false;
                    SetBlendModulate(new Color(1, 1, 1, 0.7f));
                }
                else
                {
                    _flashingInvincibleFlag = true;
                    SetBlendModulate(new Color(1, 1, 1, 0));
                }
            }

            _shieldRecoveryTimer = 0;
        }
        else //恢复护盾
        {
            if (Shield < MaxShield)
            {
                _shieldRecoveryTimer += delta;
                if (_shieldRecoveryTimer >= RoleState.ShieldRecoveryTime) //时间到, 恢复
                {
                    Shield++;
                    _shieldRecoveryTimer = 0;
                }
            }
            else
            {
                _shieldRecoveryTimer = 0;
            }
        }

        //被动道具更新
        if (BuffPropPack.Count > 0)
        {
            var buffProps = BuffPropPack;
            for (var i = 0; i < buffProps.Count; i++)
            {
                var prop = buffProps[i];
                if (!prop.IsDestroyed && prop.Master != null)
                {
                    prop.UpdateProcess(delta);
                    prop.UpdateComponentProcess(delta);
                    prop.UpdateCoroutine(delta);
                }
            }
        }
        
        //主动道具调用更新
        var props = ActivePropsPack.ItemSlot;
        if (props.Length > 0)
        {
            for (var i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                if (prop != null && !prop.IsDestroyed && prop.Master != null)
                {
                    prop.UpdateProcess(delta);
                    prop.UpdateComponentProcess(delta);
                    prop.UpdateCoroutine(delta);
                }
            }
        }
        
        if (Face == FaceDirection.Right)
        {
            TipRoot.Scale = Vector2.One;
        }
        else
        {
            TipRoot.Scale = new Vector2(-1, 1);
        }
    }

    protected override void PhysicsProcess(float delta)
    {
        //被动道具更新
        if (BuffPropPack.Count > 0)
        {
            var buffProps = BuffPropPack;
            for (var i = 0; i < buffProps.Count; i++)
            {
                var prop = buffProps[i];
                if (!prop.IsDestroyed && prop.Master != null)
                {
                    prop.UpdatePhysicsProcess(delta);
                    prop.UpdateComponentPhysicsProcess(delta);
                }
            }
        }
        
        //主动道具调用更新
        var props = ActivePropsPack.ItemSlot;
        if (props.Length > 0)
        {
            for (var i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                if (prop != null && !prop.IsDestroyed && prop.Master != null)
                {
                    prop.UpdatePhysicsProcess(delta);
                    prop.UpdateComponentPhysicsProcess(delta);
                }
            }
        }
    }

    /// <summary>
    /// 初始化瞄准辅助线
    /// </summary>
    public void InitSubLine()
    {
        if (SubLine != null)
        {
            return;
        }

        SubLine = AddComponent<SubLine>();
    }
    
    
    /// <summary>
    /// 是否是满血
    /// </summary>
    public bool IsHpFull()
    {
        return Hp >= MaxHp;
    }
    
    /// <summary>
    /// 判断指定坐标是否在角色视野方向
    /// </summary>
    public bool IsPositionInForward(Vector2 pos)
    {
        var gps = GlobalPosition;
        return (Face == FaceDirection.Left && pos.X <= gps.X) ||
               (Face == FaceDirection.Right && pos.X >= gps.X);
    }
    
    
    /// <summary>
    /// 拾起主动道具, 返回是否成功拾起, 如果不想立刻切换到该道具, exchange 请传 false
    /// </summary>
    /// <param name="activeProp">主动道具对象</param>
    /// <param name="exchange">是否立即切换到该道具, 默认 true </param>
    public bool PickUpActiveProp(ActiveProp activeProp, bool exchange = true)
    {
        if (ActivePropsPack.PickupItem(activeProp, exchange) != -1)
        {
            activeProp.MoveController.Enable = false;
            //从可互动队列中移除
            InteractiveItemList.Remove(activeProp);
            OnPickUpActiveProp(activeProp);
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// 扔掉当前使用的道具
    /// </summary>
    public void ThrowActiveProp()
    {
        ThrowActiveProp(ActivePropsPack.ActiveIndex);
    }
    
    /// <summary>
    /// 扔掉指定位置上的主动道具
    /// </summary>
    public void ThrowActiveProp(int index)
    {
        var activeProp = ActivePropsPack.GetItem(index);
        if (activeProp == null)
        {
            return;
        }

        activeProp.MoveController.Enable = true;
        ActivePropsPack.RemoveItem(index);
        OnRemoveActiveProp(activeProp);
        //播放抛出效果
        activeProp.ThrowProp(this, GlobalPosition);
    }

    /// <summary>
    /// 拾起被动道具, 返回是否成功拾起
    /// </summary>
    /// <param name="buffProp">被动道具对象</param>
    public bool PickUpBuffProp(BuffProp buffProp)
    {
        if (BuffPropPack.Contains(buffProp))
        {
            Debug.LogError("被动道具已经在背包中了!");
            return false;
        }
        BuffPropPack.Add(buffProp);
        buffProp.Master = this;
        OnPickUpBuffProp(buffProp);
        buffProp.OnPickUpItem();
        return true;
    }

    /// <summary>
    /// 扔掉指定的被动道具
    /// </summary>
    /// <param name="buffProp"></param>
    public void ThrowBuffProp(BuffProp buffProp)
    {
        var index = BuffPropPack.IndexOf(buffProp);
        if (index < 0)
        {
            Debug.LogError("当前道具不在角色背包中!");
            return;
        }
        
        ThrowBuffProp(index);
    }
    
    /// <summary>
    /// 扔掉指定位置上的被动道具
    /// </summary>
    public void ThrowBuffProp(int index)
    {
        if (index < 0 || index >= BuffPropPack.Count)
        {
            return;
        }

        var buffProp = BuffPropPack[index];
        BuffPropPack.RemoveAt(index);
        buffProp.OnRemoveItem();
        OnRemoveBuffProp(buffProp);
        buffProp.Master = null;
        //播放抛出效果
        buffProp.ThrowProp(this, GlobalPosition);
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
    /// 触发使用道具
    /// </summary>
    public virtual void UseActiveProp()
    {
        var activeItem = ActivePropsPack.ActiveItem;
        if (activeItem != null)
        {
            activeItem.Use();
        }
    }
    
    /// <summary>
    /// 受到伤害, 如果是在碰撞信号处理函数中调用该函数, 请使用 CallDeferred 来延时调用, 否则很有可能导致报错
    /// </summary>
    /// <param name="target">触发伤害的对象, 为 null 表示不存在对象或者对象已经被销毁</param>
    /// <param name="damage">伤害的量</param>
    /// <param name="angle">伤害角度（弧度制）</param>
    public virtual void HurtHandler(ActivityObject target, int damage, float angle)
    {
        //受伤闪烁, 无敌状态, 或者已经死亡
        if (Invincible || IsDie)
        {
            return;
        }
        
        //计算真正受到的伤害
        damage = OnHandlerHurt(damage);
        var flag = Shield > 0;
        if (flag)
        {
            Shield -= damage;
        }
        else
        {
            damage = RoleState.CalcHurtDamage(damage);
            if (damage > 0)
            {
                Hp -= damage;
            }
            //播放血液效果
            // var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_Blood_tscn);
            // var blood = packedScene.Instance<Blood>();
            // blood.GlobalPosition = GlobalPosition;
            // blood.Rotation = angle;
            // GameApplication.Instance.Node3D.GetRoot().AddChild(blood);
        }

        PrevHitAngle = angle;
        OnHit(target, damage, angle, !flag);
        
        if (target is Role targetRole && !targetRole.IsDestroyed)
        {
            //造成伤害回调
            if (targetRole.OnDamageEvent != null)
            {
                targetRole.OnDamageEvent(this, damage);
            }
        }
        
        //受伤特效
        PlayHitAnimation();
        
        //死亡判定
        if (Hp <= 0)
        {
            //死亡
            if (!IsDie)
            {
                IsDie = true;
                
                //禁用状态机控制器
                var stateController = GetComponent(typeof(IStateController));
                if (stateController != null)
                {
                    stateController.Enable = false;
                }

                //播放死亡动画
                if (AnimationPlayer.HasAnimation(AnimatorNames.Die))
                {
                    StartCoroutine(DoDieWithAnimationPlayer());
                }
                else if (AnimatedSprite.SpriteFrames.HasAnimation(AnimatorNames.Die))
                {
                    StartCoroutine(DoDieWithAnimatedSprite());
                }
                else
                {
                    DoDieHandler();
                }
            }
        }
    }

    private IEnumerator DoDieWithAnimationPlayer()
    {
        AnimationPlayer.Play(AnimatorNames.Die);
        yield return ToSignal(AnimationPlayer, AnimationMixer.SignalName.AnimationFinished);
        DoDieHandler();
    }
    
    private IEnumerator DoDieWithAnimatedSprite()
    {
        AnimatedSprite.Play(AnimatorNames.Die);
        yield return ToSignal(AnimatedSprite, AnimatedSprite2D.SignalName.AnimationFinished);
        DoDieHandler();
    }

    //死亡逻辑
    private void DoDieHandler()
    {
        OnDie();
        //死亡事件
        World.OnRoleDie(this);
    }

    /// <summary>
    /// 播放无敌状态闪烁动画
    /// </summary>
    /// <param name="time">持续时间</param>
    public void PlayInvincibleFlashing(float time)
    {
        Invincible = true;
        if (_invincibleFlashingId >= 0) //上一个还没结束
        {
            StopCoroutine(_invincibleFlashingId);
        }

        _invincibleFlashingId = StartCoroutine(RunInvincibleFlashing(time));
    }

    /// <summary>
    /// 停止无敌状态闪烁动画
    /// </summary>
    public void StopInvincibleFlashing()
    {
        Invincible = false;
        if (_invincibleFlashingId >= 0)
        {
            StopCoroutine(_invincibleFlashingId);
            _invincibleFlashingId = -1;
        }
    }

    private IEnumerator RunInvincibleFlashing(float time)
    {
        yield return new WaitForSeconds(time);
        _invincibleFlashingId = -1;
        Invincible = false;
    }

    /// <summary>
    /// 设置脸的朝向
    /// </summary>
    private  void _SetFace(FaceDirection face)
    {
        if (_face != face)
        {
            _face = face;
            if (face == FaceDirection.Right)
            {
                RotationDegrees = 0;
                Scale = _startScale;
            }
            else
            {
                RotationDegrees = 180;
                Scale = new Vector2(_startScale.X, -_startScale.Y);
            }
        }
    }
    
    /// <summary>
    /// 连接信号: InteractiveArea.BodyEntered
    /// 与物体碰撞
    /// </summary>
    private void _OnPropsEnter(Node2D other)
    {
        if (other is ActivityObject propObject && !propObject.CollisionWithMask(PhysicsLayer.OnHand))
        {
            if (!InteractiveItemList.Contains(propObject))
            {
                InteractiveItemList.Add(propObject);
            }
        }
    }

    /// <summary>
    /// 连接信号: InteractiveArea.BodyExited
    /// 物体离开碰撞区域
    /// </summary>
    private void _OnPropsExit(Node2D other)
    {
        if (other is ActivityObject propObject)
        {
            if (InteractiveItemList.Contains(propObject))
            {
                InteractiveItemList.Remove(propObject);
            }
            if (InteractiveItem == propObject)
            {
                var prev = _currentResultData;
                _currentResultData = null;
                InteractiveItem = null;
                ChangeInteractiveItem(prev, null);
            }
        }
    }
    
    
    /// <summary>
    /// 返回当前角色是否是玩家
    /// </summary>
    public bool IsPlayer()
    {
        return this == World.Player;
    }
    
    
    /// <summary>
    /// 返回指定角色是否是敌人
    /// </summary>
    public bool IsEnemy(Role other)
    {
        if (this == other)
        {
            return false;
        }

        if (Camp == CampEnum.None || other.Camp == CampEnum.None)
        {
            return true;
        }
        
        if (other.Camp == Camp || other.Camp == CampEnum.Peace || Camp == CampEnum.Peace)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 返回指定阵营是否是敌人
    /// </summary>
    public bool IsEnemy(CampEnum otherCamp)
    {
        if (Camp == CampEnum.None || otherCamp == CampEnum.None)
        {
            return true;
        }
        
        if (otherCamp == Camp || otherCamp == CampEnum.Peace || Camp == CampEnum.Peace)
        {
            return false;
        }

        return true;   
    }
    
    /// <summary>
    /// 是否是玩家的敌人
    /// </summary>
    public bool IsEnemyWithPlayer()
    {
        if (World.Player == null)
        {
            return false;
        }
        return IsEnemy(World.Player);
    }

    /// <summary>
    /// 将 Role 子节点的旋转角度转换为正常的旋转角度<br/>
    /// 因为 Role 受到 Face 影响, 会出现转向动作, 所以需要该函数来转换旋转角度
    /// </summary>
    /// <param name="rotation">角度, 弧度制</param>
    public float ConvertRotation(float rotation)
    {
        if (Face == FaceDirection.Right)
        {
            return rotation;
        }

        return Mathf.Pi - rotation;
    }

    /// <summary>
    /// 获取开火点高度
    /// </summary>
    public virtual float GetFirePointAltitude()
    {
        return -MountPoint.Position.Y;
    }
    
    /// <summary>
    /// 当拾起某个武器时调用
    /// </summary>
    protected virtual void OnPickUpWeapon(Weapon weapon)
    {
    }
    
    /// <summary>
    /// 当扔掉某个武器时调用
    /// </summary>
    protected virtual void OnThrowWeapon(Weapon weapon)
    {
    }

    /// <summary>
    /// 当切换到某个武器时调用
    /// </summary>
    protected virtual void OnExchangeWeapon(Weapon weapon)
    {
    }
    
    /// <summary>
    /// 当武器放到后背时调用, 用于设置武器位置和角度
    /// </summary>
    /// <param name="weapon">武器实例</param>
    /// <param name="index">放入武器背包的位置</param>
    public virtual void OnPutBackMount(Weapon weapon, int index)
    {
        if (index < 8)
        {
            if (index % 2 == 0)
            {
                weapon.Position = new Vector2(-4, 3);
                weapon.RotationDegrees = 90 - (index / 2f) * 20;
                weapon.Scale = new Vector2(-1, 1);
            }
            else
            {
                weapon.Position = new Vector2(4, 3);
                weapon.RotationDegrees = 270 + (index - 1) / 2f * 20;
                weapon.Scale = new Vector2(1, 1);
            }
        }
        else
        {
            weapon.Visible = false;
        }
    }
    
    protected override void OnAffiliationChange(AffiliationArea prevArea)
    {
        //身上的武器的所属区域也得跟着变
        WeaponPack.ForEach((weapon, i) =>
        {
            if (AffiliationArea != null)
            {
                AffiliationArea.InsertItem(weapon);
            }
            else if (weapon.AffiliationArea != null)
            {
                weapon.AffiliationArea.RemoveItem(weapon);
            }

            weapon.World = World;
        });
    }
    
    /// <summary>
    /// 调整角色的朝向, 使其看向目标点
    /// </summary>
    public virtual void LookTargetPosition(Vector2 pos)
    {
        LookPosition = pos;
        if (MountLookTarget)
        {
            //脸的朝向
            var gPos = Position;
            if (pos.X > gPos.X && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (pos.X < gPos.X && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随目标
            MountPoint.SetLookAt(pos);
        }
    }

    /// <summary>
    /// 返回所有武器是否弹药都打光了
    /// </summary>
    public virtual bool IsAllWeaponTotalAmmoEmpty()
    {
        foreach (var weapon in WeaponPack.ItemSlot)
        {
            if (weapon != null && !weapon.IsTotalAmmoEmpty())
            {
                return false;
            }
        }

        return true;
    }
    
    //-------------------------------------------------------------------------------------
    
    /// <summary>
    /// 拾起一个武器, 返回是否成功拾起, 如果不想立刻切换到该武器, exchange 请传 false
    /// </summary>
    /// <param name="weapon">武器对象</param>
    /// <param name="exchange">是否立即切换到该武器, 默认 true </param>
    public bool PickUpWeapon(Weapon weapon, bool exchange = true)
    {
        if (WeaponPack.PickupItem(weapon, exchange) != -1)
        {
            //从可互动队列中移除
            InteractiveItemList.Remove(weapon);
            OnPickUpWeapon(weapon);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public void ExchangeNextWeapon()
    {
        var weapon = WeaponPack.ActiveItem;
        WeaponPack.ExchangeNext();
        if (WeaponPack.ActiveItem != weapon)
        {
            OnExchangeWeapon(WeaponPack.ActiveItem);
        }
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public void ExchangePrevWeapon()
    {
        var weapon = WeaponPack.ActiveItem;
        WeaponPack.ExchangePrev();
        if (WeaponPack.ActiveItem != weapon)
        {
            OnExchangeWeapon(WeaponPack.ActiveItem);
        }
    }

    /// <summary>
    /// 切换到指定索引到武器
    /// </summary>
    public void ExchangeWeaponByIndex(int index)
    {
        if (WeaponPack.ActiveIndex != index)
        {
            WeaponPack.ExchangeByIndex(index);
        }
    }

    /// <summary>
    /// 扔掉当前使用的武器, 切换到上一个武器
    /// </summary>
    public void ThrowWeapon()
    {
        ThrowWeapon(WeaponPack.ActiveIndex);
    }

    /// <summary>
    /// 扔掉指定位置的武器
    /// </summary>
    /// <param name="index">武器在武器背包中的位置</param>
    public void ThrowWeapon(int index)
    {
        var weapon = WeaponPack.GetItem(index);
        if (weapon == null)
        {
            return;
        }

        // var temp = weapon.AnimatedSprite.Position;
        // if (Face == FaceDirection.Left)
        // {
        //     temp.Y = -temp.Y;
        // }
        //var pos = GlobalPosition + temp.Rotated(weapon.GlobalRotation);
        WeaponPack.RemoveItem(index);
        //播放抛出效果
        weapon.ThrowWeapon(this, GlobalPosition);
    }

    /// <summary>
    /// 从背包中移除指定武器, 不会触发投抛效果
    /// </summary>
    /// <param name="index">武器在武器背包中的位置</param>
    public void RemoveWeapon(int index)
    {
        var weapon = WeaponPack.GetItem(index);
        if (weapon == null)
        {
            return;
        }
        
        WeaponPack.RemoveItem(index);
    }
    
    /// <summary>
    /// 扔掉所有武器
    /// </summary>
    public void ThrowAllWeapon()
    {
        var weapons = WeaponPack.GetAndClearItem();
        for (var i = 0; i < weapons.Length; i++)
        {
            weapons[i].ThrowWeapon(this);
        }
    }
    
    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public void ExchangeNextActiveProp()
    {
        var prop = ActivePropsPack.ActiveItem;
        ActivePropsPack.ExchangeNext();
        if (prop != ActivePropsPack.ActiveItem)
        {
            OnExchangeActiveProp(ActivePropsPack.ActiveItem);
        }
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public void ExchangePrevActiveProp()
    {
        var prop = ActivePropsPack.ActiveItem;
        ActivePropsPack.ExchangePrev();
        if (prop != ActivePropsPack.ActiveItem)
        {
            OnExchangeActiveProp(ActivePropsPack.ActiveItem);
        }
    }

    //-------------------------------------------------------------------------------------
    

    /// <summary>
    /// 触发换弹
    /// </summary>
    public virtual void Reload()
    {
        if (WeaponPack.ActiveItem != null)
        {
            WeaponPack.ActiveItem.Reload();
        }
    }
    
    /// <summary>
    /// 攻击函数
    /// </summary>
    public virtual void Attack()
    {
        if (MeleeAttackTimer <= 0 && WeaponPack.ActiveItem != null)
        {
            WeaponPack.ActiveItem.Trigger(this);
        }
    }

    /// <summary>
    /// 触发近战攻击
    /// </summary>
    public virtual void MeleeAttack()
    {
        if (IsAttack)
        {
            return;
        }

        if (WeaponPack.ActiveItem != null && !WeaponPack.ActiveItem.Reloading && WeaponPack.ActiveItem.Attribute.CanMeleeAttack)
        {
            MeleeAttackTimer = RoleState.MeleeAttackTime;
            MountLookTarget = false;
            
            //播放近战动画
            PlayAnimation_MeleeAttack(() =>
            {
                MountLookTarget = true;
            });
        }
    }
    
    /// <summary>
    /// 添加金币
    /// </summary>
    public virtual void AddGold(int goldCount)
    {
        RoleState.Gold += RoleState.CalcGetGold(goldCount);
        //播放音效
        SoundManager.PlaySoundByConfig(ExcelConfig.Sound_Map["gold"], Position, this);
    }

    /// <summary>
    /// 使用金币
    /// </summary>
    public virtual void UseGold(int goldCount)
    {
        RoleState.Gold -= goldCount;
    }

    /// <summary>
    /// 切换当前使用的武器的回调
    /// </summary>
    private void OnChangeActiveItem(Weapon weapon)
    {
        //这里处理近战区域
        if (weapon != null)
        {
            MeleeAttackCollision.Polygon = Utils.CreateSectorPolygon(
                Utils.ConvertAngle(-MeleeAttackAngle / 2f),
                (weapon.GetLocalFirePosition() + weapon.GetGripPosition()).Length() * 1.1f,
                MeleeAttackAngle,
                6
            );
            MeleeAttackArea.CollisionMask = AttackLayer | PhysicsLayer.Bullet;
        }
    }

    private void OnMeleeAttackAreaEntered(Area2D area)
    {
        var activeWeapon = WeaponPack.ActiveItem;
        if (activeWeapon == null)
        {
            return;
        }
        
        if (area is IHurt hurt)
        {
            HandlerCollision(hurt, activeWeapon);
        }
    }

    /// <summary>
    /// 近战区域碰到敌人
    /// </summary>
    private void OnMeleeAttackBodyEntered(Node2D body)
    {
        var activeWeapon = WeaponPack.ActiveItem;
        if (activeWeapon == null)
        {
            return;
        }
        
        if (body is IHurt hurt)
        {
            HandlerCollision(hurt, activeWeapon);
        }
        else if (body is Bullet bullet) //攻击子弹
        {
            if (IsEnemy(bullet.BulletData.TriggerRole))
            {
                bullet.OnPlayDisappearEffect();
                bullet.Destroy();
            }
        }
    }

    private void HandlerCollision(IHurt hurt, Weapon activeWeapon)
    {
        if (hurt.CanHurt(Camp))
        {
            var damage = Utils.Random.RandomConfigRange(activeWeapon.Attribute.MeleeAttackHarmRange);
            damage = RoleState.CalcDamage(damage);

            var o = hurt.GetActivityObject();
            var pos = hurt.GetPosition();
            if (o != null && o is not Player) //不是玩家才能被击退
            {
                var attr = IsAi ? activeWeapon.AiUseAttribute : activeWeapon.PlayerUseAttribute;
                var repel = Utils.Random.RandomConfigRange(attr.MeleeAttackRepelRange);
                var position = pos - MountPoint.GlobalPosition;
                var v2 = position.Normalized() * repel;
                o.AddRepelForce(v2);
            }
            
            hurt.Hurt(this, damage, (pos - GlobalPosition).Angle());
        }
    }

    protected override void OnDestroy()
    {
        //销毁道具
        foreach (var buffProp in BuffPropPack)
        {
            buffProp.Destroy();
        }
        BuffPropPack.Clear();
        ActivePropsPack.Destroy();
        
        WeaponPack.Destroy();
    }
}