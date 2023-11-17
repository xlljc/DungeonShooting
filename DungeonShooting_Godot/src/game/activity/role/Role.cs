
using System.Collections;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 角色基类
/// </summary>
public abstract partial class Role : ActivityObject
{
    /// <summary>
    /// 是否是 Ai
    /// </summary>
    public bool IsAi { get; protected set; } = false;

    /// <summary>
    /// 角色属性
    /// </summary>
    public RoleState RoleState { get; } = new RoleState();
    
    /// <summary>
    /// 默认攻击对象层级
    /// </summary>
    public const uint DefaultAttackLayer = PhysicsLayer.Player | PhysicsLayer.Enemy | PhysicsLayer.Wall;
    
    /// <summary>
    /// 伤害区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D HurtArea { get; set; }
    
    /// <summary>
    /// 伤害区域碰撞器
    /// </summary>
    [Export, ExportFillNode]
    public CollisionShape2D HurtCollision { get; set; }

    /// <summary>
    /// 所属阵营
    /// </summary>
    public CampEnum Camp;

    /// <summary>
    /// 攻击目标的碰撞器所属层级, 数据源自于: <see cref="PhysicsLayer"/>
    /// </summary>
    public uint AttackLayer { get; set; } = PhysicsLayer.Wall;
    
    /// <summary>
    /// 该角色敌对目标的碰撞器所属层级, 数据源自于: <see cref="PhysicsLayer"/>
    /// </summary>
    public uint EnemyLayer { get; set; } = PhysicsLayer.Enemy;

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
    /// 动画播放器
    /// </summary>
    [Export, ExportFillNode]
    public AnimationPlayer AnimationPlayer { get; set; }
    
    /// <summary>
    /// 脸的朝向
    /// </summary>
    public FaceDirection Face { get => _face; set => SetFace(value); }
    private FaceDirection _face;

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
                    HurtArea.CollisionLayer = _currentLayer;
                    _flashingInvincibleTimer = -1;
                    _flashingInvincibleFlag = false;
                }
                else //正常状态
                {
                    HurtArea.CollisionLayer = _currentLayer;
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
    
    //初始缩放
    private Vector2 _startScale;
    //当前可互动的物体
    private CheckInteractiveResult _currentResultData;
    private uint _currentLayer;
    //闪烁计时器
    private float _flashingInvincibleTimer = -1;
    //闪烁状态
    private bool _flashingInvincibleFlag = false;
    //闪烁动画协程id
    private long _invincibleFlashingId = -1;
    //护盾恢复计时器
    private float _shieldRecoveryTimer = 0;

    
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
    /// <param name="realHarm">是否受到真实伤害, 如果为false, 则表示该伤害被互动格挡掉了</param>
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
    
    
    /// <summary>
    /// 触发攻击
    /// </summary>
    public virtual void Attack()
    {
    }
    
    public override void OnInit()
    {
        ActivePropsPack = AddComponent<Package<ActiveProp, Role>>();
        ActivePropsPack.SetCapacity(1);
        
        _startScale = Scale;
        
        HurtArea.CollisionLayer = CollisionLayer;
        HurtArea.CollisionMask = 0;
        _currentLayer = HurtArea.CollisionLayer;
        
        Face = FaceDirection.Right;
        
        //连接互动物体信号
        InteractiveArea.BodyEntered += _OnPropsEnter;
        InteractiveArea.BodyExited += _OnPropsExit;
    }

    protected override void Process(float delta)
    { 
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
            var buffProps = BuffPropPack.ToArray();
            foreach (var prop in buffProps)
            {
                if (!prop.IsDestroyed)
                {
                    prop.PackProcess(delta);
                }
            }
        }
        
        //主动道具调用更新
        var props = ActivePropsPack.ItemSlot;
        if (props.Length > 0)
        {
            props = (ActiveProp[])props.Clone();
            foreach (var prop in props)
            {
                if (prop != null && !prop.IsDestroyed)
                {
                    prop.PackProcess(delta);
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
    /// 使角色看向指定的坐标的方向
    /// </summary>
    public virtual void LookTargetPosition(Vector2 pos)
    {
        LookPosition = pos;
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
    public virtual void Hurt(ActivityObject target, int damage, float angle)
    {
        //受伤闪烁, 无敌状态
        if (Invincible)
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
        OnHit(target, damage, angle, !flag);

        //受伤特效
        PlayHitAnimation();
        
        //死亡判定
        if (Hp <= 0)
        {
            //死亡
            if (!IsDie)
            {
                IsDie = true;
                OnDie();
            }
        }
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
    private void SetFace(FaceDirection face)
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
        return this == Player.Current;
    }
    
    /// <summary>
    /// 是否是玩家的敌人
    /// </summary>
    public bool IsEnemyWithPlayer()
    {
        return CollisionWithMask(Player.Current.EnemyLayer);
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
        return -AnimatedSprite.Position.Y;
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
    }
}