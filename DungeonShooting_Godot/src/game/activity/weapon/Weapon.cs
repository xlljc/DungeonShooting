using Godot;
using System;
using System.Collections.Generic;
using Config;

/// <summary>
/// 武器的基类
/// </summary>
public abstract partial class Weapon : ActivityObject, IPackageItem
{
    /// <summary>
    /// 武器使用的属性数据, 该属性会根据是否是玩家使用武器, 如果是Ai使用武器, 则会返回 AiUseAttribute 的属性对象
    /// </summary>
    public ExcelConfig.Weapon Attribute => _weaponAttribute;

    /// <summary>
    /// Ai使用该武器的属性
    /// </summary>
    public ExcelConfig.Weapon AiUseAttribute => _aiWeaponAttribute;

    /// <summary>
    /// 玩家使用该武器的属性
    /// </summary>
    public ExcelConfig.Weapon PlayerUseAttribute => _playerWeaponAttribute;
    
    private ExcelConfig.Weapon _weaponAttribute;
    private ExcelConfig.Weapon _playerWeaponAttribute;
    private ExcelConfig.Weapon _aiWeaponAttribute;

    /// <summary>
    /// 武器攻击的目标阵营
    /// </summary>
    public CampEnum TargetCamp { get; set; }

    public Role Master { get; set; }

    public int PackageIndex { get; set; } = -1;

    /// <summary>
    /// 当前弹夹弹药剩余量
    /// </summary>
    public int CurrAmmo { get; private set; }

    /// <summary>
    /// 剩余弹药量(备用弹药)
    /// </summary>
    public int ResidueAmmo { get; private set; }

    /// <summary>
    /// 武器管的开火点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D FirePoint { get; set; }

    /// <summary>
    /// 弹壳抛出的点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D ShellPoint { get; set; }

    /// <summary>
    /// 武器握把位置
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D GripPoint { get; set; }
    
    /// <summary>
    /// 武器的当前散射半径
    /// </summary>
    public float CurrScatteringRange { get; private set; } = 0;

    /// <summary>
    /// 是否在换弹中
    /// </summary>
    /// <value></value>
    public bool Reloading { get; private set; } = false;

    /// <summary>
    /// 换弹进度 (从 0 到 1)
    /// </summary>
    public float ReloadProgress
    {
        get
        {
            if (!Reloading)
            {
                return 1;
            }

            if (Attribute.AloneReload)
            {
                //总时间
                var total = Attribute.AloneReloadBeginIntervalTime + (Attribute.ReloadTime * Attribute.AmmoCapacity) + Attribute.AloneReloadFinishIntervalTime;
                //当前时间
                float current;
                if (_aloneReloadState == 1)
                {
                    current = (Attribute.AloneReloadBeginIntervalTime - _reloadTimer) + Attribute.ReloadTime * CurrAmmo;
                }
                else if (_aloneReloadState == 2)
                {
                    current = Attribute.AloneReloadBeginIntervalTime + (Attribute.ReloadTime * (CurrAmmo + (1 - _reloadTimer / Attribute.ReloadTime)));
                }
                else
                {
                    current = Attribute.AloneReloadBeginIntervalTime + (Attribute.ReloadTime * CurrAmmo) + (Attribute.AloneReloadFinishIntervalTime - _reloadTimer);
                }

                return current / total;
            }

            return 1 - _reloadTimer / Attribute.ReloadTime;
        }
    }

    /// <summary>
    /// 返回是否在蓄力中,
    /// 注意, 属性仅在 Attribute.LooseShoot == false 时有正确的返回值, 否则返回 false
    /// </summary>
    public bool IsCharging => _looseShootFlag;

    /// <summary>
    /// 返回武器是否在武器背包中
    /// </summary>
    public bool IsInHolster => Master != null;

    /// <summary>
    /// 返回是否真正使用该武器
    /// </summary>
    public bool IsActive => Master != null && Master.WeaponPack.ActiveItem == this;
    
    /// <summary>
    /// 动画播放器
    /// </summary>
    [Export, ExportFillNode]
    public AnimationPlayer AnimationPlayer { get; set; }

    /// <summary>
    /// 是否自动播放 SpriteFrames 的动画
    /// </summary>
    public bool IsAutoPlaySpriteFrames { get; set; } = true;

    /// <summary>
    /// 在没有所属 Master 的时候是否可以触发扳机
    /// </summary>
    public bool NoMasterCanTrigger { get; set; } = true;
    
    /// <summary>
    /// 上一次触发改武器开火的角色是否是Ai
    /// </summary>
    public bool TriggerRoleIsAi { get; private set; }
    
    /// <summary>
    /// 上一次触发改武器开火的触发开火攻击的层级, 数据源自于: <see cref="PhysicsLayer"/>
    /// </summary>
    public long TriggerRoleAttackLayer { get; private set; }
    //--------------------------------------------------------------------------------------------

    //用于记录当前角色是否按下过扳机
    private bool _triggerRoleFlag = false;
    
    //是否按下
    private bool _triggerFlag = false;

    //扳机计时器
    private float _triggerTimer = 0;

    //开火前延时时间
    private float _delayedTime = 0;

    //开火间隙时间
    private float _fireInterval = 0;

    //开火武器口角度
    private float _fireAngle = 0;

    //攻击冷却计时
    private float _attackTimer = 0;

    //攻击状态
    private bool _attackFlag = false;
    
    //多久没开火了
    private float _noAttackTime = 0;

    //按下的时间
    private float _downTimer = 0;

    //松开的时间
    private float _upTimer = 0;

    //连发次数
    private int _continuousCount = 0;

    //连发状态记录
    private bool _continuousShootFlag = false;

    //松开扳机是否开火
    private bool _looseShootFlag = false;

    //蓄力攻击时长
    private float _chargeTime = 0;

    //是否需要重置武器数据
    private bool _dirtyFlag = false;

    //当前后坐力导致的偏移长度
    private float _currBacklashLength = 0;

    //临时存放动画精灵位置
    private Vector2 _tempAnimatedSpritePosition;

    //换弹计时器
    private float _reloadTimer = 0;
    
    //单独换弹设置下的换弹状态, 0: 未换弹, 1: 装第一颗子弹之前, 2: 单独装弹中, 3: 单独装弹完成
    private byte _aloneReloadState = 3;

    //单独换弹状态下是否强制结束换弹过程
    private bool _aloneReloadStop = false;
    
    //本次换弹已用时间
    private float _reloadUseTime = 0;

    //是否播放过换弹完成音效
    private bool _playReloadFinishSoundFlag = false;

    //上膛状态,-1: 等待执行自动上膛 , 0: 未上膛, 1: 上膛中, 2: 已经完成上膛
    private sbyte _beLoadedState = 2;

    //上膛计时器
    private float _beLoadedStateTimer = -1;

    // ----------------------------------------------
    private uint _tempLayer;

    private static bool _init = false;
    private static Dictionary<string, ExcelConfig.Weapon> _weaponAttributeMap =
        new Dictionary<string, ExcelConfig.Weapon>();

    /// <summary>
    /// 初始化武器属性数据
    /// </summary>
    public static void InitWeaponAttribute()
    {
        if (_init)
        {
            return;
        }

        _init = true;
        foreach (var weaponAttr in ExcelConfig.Weapon_List)
        {
            if (!string.IsNullOrEmpty(weaponAttr.WeaponId))
            {
                if (!_weaponAttributeMap.TryAdd(weaponAttr.WeaponId, weaponAttr))
                {
                    Debug.LogError("发现重复注册的武器属性: " + weaponAttr.Id);
                }
            }
        }
    }
    
    private static ExcelConfig.Weapon _GetWeaponAttribute(string itemId)
    {
        if (_weaponAttributeMap.TryGetValue(itemId, out var attr))
        {
            return attr.Clone();
        }

        throw new Exception($"武器'{itemId}'没有在 Weapon 表中配置属性数据!");
    }
    
    public override void OnInit()
    {
        InitWeapon(_GetWeaponAttribute(ItemConfig.Id));
        AnimatedSprite.AnimationFinished += OnAnimationFinished;
    }

    /// <summary>
    /// 初始化武器属性
    /// </summary>
    public void InitWeapon(ExcelConfig.Weapon attribute)
    {
        _playerWeaponAttribute = attribute;
        _weaponAttribute = attribute;
        if (attribute.AiUseAttribute != null)
        {
            _aiWeaponAttribute = attribute.AiUseAttribute;
        }
        else
        {
            _aiWeaponAttribute = attribute;
        }

        if (Attribute.AmmoCapacity > Attribute.MaxAmmoCapacity)
        {
            Attribute.AmmoCapacity = Attribute.MaxAmmoCapacity;
            Debug.LogError("弹夹的容量不能超过弹药上限, 武器id: " + ItemConfig.Id);
        }
        //弹药量
        CurrAmmo = Attribute.AmmoCapacity;
        //剩余弹药量
        ResidueAmmo = Mathf.Min(Attribute.StandbyAmmoCapacity + CurrAmmo, Attribute.MaxAmmoCapacity) - CurrAmmo;
        
        ThrowCollisionSize = attribute.ThrowCollisionSize.AsVector2();
    }

    /// <summary>
    /// 单次开火时调用的函数
    /// </summary>
    protected abstract void OnFire();

    /// <summary>
    /// 发射子弹时调用的函数, 每发射一枚子弹调用一次,
    /// 如果做霰弹武器效果, 一次开火发射5枚子弹, 则该函数调用5次
    /// </summary>
    /// <param name="fireRotation">开火时枪口旋转角度, 弧度制</param>
    protected abstract void OnShoot(float fireRotation);

    /// <summary>
    /// 上膛开始时调用
    /// </summary>
    protected virtual void OnBeginBeLoaded()
    {
    }
    
    /// <summary>
    /// 上膛结束时调用
    /// </summary>
    protected virtual void OnBeLoadedFinish()
    {
    }
    
    /// <summary>
    /// 当按下扳机时调用
    /// </summary>
    protected virtual void OnDownTrigger()
    {
    }

    /// <summary>
    /// 当松开扳机时调用
    /// </summary>
    protected virtual void OnUpTrigger()
    {
    }

    /// <summary>
    /// 开始蓄力时调用, 
    /// 注意, 该函数仅在 Attribute.LooseShoot == false 时才能被调用
    /// </summary>
    protected virtual void OnBeginCharge()
    {
    }

    /// <summary>
    /// 当换弹时调用, 如果设置单独装弹, 则每装一次弹调用一次该函数
    /// </summary>
    protected virtual void OnReload()
    {
    }

    /// <summary>
    /// 当开始换弹时调用
    /// </summary>
    protected virtual void OnBeginReload()
    {
    }
    
    /// <summary>
    /// 当换弹完成时调用
    /// </summary>
    protected virtual void OnReloadFinish()
    {
    }

    /// <summary>
    /// 单独装弹完成时调用
    /// </summary>
    protected virtual void OnAloneReloadStateFinish()
    {
    }
    
    /// <summary>
    /// 当武器被拾起时调用
    /// </summary>
    /// <param name="master">拾起该武器的角色</param>
    protected virtual void OnPickUp(Role master)
    {
    }

    /// <summary>
    /// 当武器从武器背包中移除时调用
    /// </summary>
    /// <param name="master">移除该武器的角色</param>
    protected virtual void OnRemove(Role master)
    {
    }

    /// <summary>
    /// 当武器被激活时调用, 也就是使用当武器时调用
    /// </summary>
    protected virtual void OnActive()
    {
    }

    /// <summary>
    /// 当武器被收起时调用
    /// </summary>
    protected virtual void OnConceal()
    {
    }

    /// <summary>
    /// 射击时调用, 返回消耗弹药数量, 默认为1, 如果返回为 0, 则不消耗弹药
    /// </summary>
    protected virtual int UseAmmoCount()
    {
        return 1;
    }

    public override void EnterTree()
    {
        base.EnterTree();
        //收集落在地上的武器
        if (IsInGround())
        {
            World.Weapon_UnclaimedWeapons.Add(this);
        }
    }

    public override void ExitTree()
    {
        base.ExitTree();
        World.Weapon_UnclaimedWeapons.Remove(this);
    }

    protected override void Process(float delta)
    {
        
        //未开火时间
        _noAttackTime += delta;
        
        //这把武器被扔在地上, 或者当前武器没有被使用
        //if (Master == null || Master.WeaponPack.ActiveItem != this)
        if ((Master != null && Master.WeaponPack.ActiveItem != this) || !_triggerRoleFlag) //在背上, 或者被扔出去了
        {
            //_triggerTimer
            _triggerTimer = _triggerTimer > 0 ? _triggerTimer - delta : 0;
            //攻击冷却计时
            _attackTimer = _attackTimer > 0 ? _attackTimer - delta : 0;
            //武器的当前散射半径
            ScatteringRangeBackHandler(delta);
            //松开扳机
            if (_triggerFlag || _downTimer > 0)
            {
                UpTrigger();
                _downTimer = 0;
            }
            
            _triggerFlag = false;

            //重置数据
            if (_dirtyFlag)
            {
                _dirtyFlag = false;
                //_aloneReloadState = 0;
                StopReload();
                _attackFlag = false;
                _continuousCount = 0;
                _delayedTime = 0;
                _upTimer = 0;
                _looseShootFlag = false;
                _chargeTime = 0;
                _beLoadedStateTimer = -1;
            }
        }
        else //正在使用中
        {
            _dirtyFlag = true;
            
            //上膛
            if (_beLoadedState == 1)
            {
                _beLoadedStateTimer -= delta;
                //上膛完成
                if (_beLoadedStateTimer <= 0)
                {
                    _beLoadedStateTimer = -1;
                    _beLoadedState = 2;
                    OnBeLoadedFinish();
                }
            }
            
            //换弹
            if (Reloading)
            {
                //换弹用时
                _reloadUseTime += delta;
                _reloadTimer -= delta;

                if (Attribute.AloneReload) //单独装弹模式
                {
                    switch (_aloneReloadState)
                    {
                        case 0:
                            Debug.LogError("AloneReload状态错误!");
                            break;
                        case 1: //装第一颗子弹之前
                        {
                            if (_reloadTimer <= 0)
                            {
                                //开始装第一颗子弹
                                _aloneReloadState = 2;
                                ReloadHandler();
                            }
                            _aloneReloadStop = false;
                        }
                            break;
                        case 2: //单独装弹中
                        {
                            if (_reloadTimer <= 0)
                            {
                                ReloadSuccess();
                                if (_aloneReloadStop || ResidueAmmo == 0 || CurrAmmo == Attribute.AmmoCapacity) //单独装弹完成
                                {
                                    AloneReloadStateFinish();
                                    if (Attribute.AloneReloadFinishIntervalTime <= 0)
                                    {
                                        //换弹完成
                                        StopReload();
                                        ReloadFinishHandler();
                                    }
                                    else
                                    {
                                        _reloadTimer = Attribute.AloneReloadFinishIntervalTime;
                                        _aloneReloadState = 3;
                                    }
                                }
                            }
                        }
                            break;
                        case 3: //单独装弹完成
                        {
                            //播放换弹完成音效
                            if (!_playReloadFinishSoundFlag && Attribute.ReloadFinishSound != null && _reloadTimer <= Attribute.ReloadFinishSoundAdvanceTime)
                            {
                                _playReloadFinishSoundFlag = true;
                                // Debug.Log("播放换弹完成音效.");
                                PlayReloadFinishSound();
                            }
                            
                            if (_reloadTimer <= 0)
                            {
                                //换弹完成
                                StopReload();
                                ReloadFinishHandler();
                            }
                            _aloneReloadStop = false;
                        }
                            break;
                    }
                }
                else //普通换弹模式
                {
                    //播放换弹完成音效
                    if (!_playReloadFinishSoundFlag && Attribute.ReloadFinishSound != null && _reloadTimer <= Attribute.ReloadFinishSoundAdvanceTime)
                    {
                        _playReloadFinishSoundFlag = true;
                        // Debug.Log("播放换弹完成音效.");
                        PlayReloadFinishSound();
                    }

                    if (_reloadTimer <= 0)
                    {
                        ReloadSuccess();
                    }
                }
            }

            //攻击的计时器
            if (_attackTimer > 0)
            {
                _attackTimer -= delta;
                if (_attackTimer < 0)
                {
                    _delayedTime += _attackTimer;
                    _attackTimer = 0;
                    //枪口默认角度
                    if (Master != null)
                    {
                        RotationDegrees = -Attribute.DefaultAngle;
                    }

                    //自动上膛
                    if (_beLoadedState == -1)
                    {
                        BeLoadedHandler();
                    }
                }
            }
            else if (_delayedTime > 0) //攻击延时
            {
                _delayedTime -= delta;
                if (_attackTimer < 0)
                {
                    _delayedTime = 0;
                }
            }
            
            //扳机判定
            if (_triggerFlag)
            {
                if (_looseShootFlag) //蓄力时长
                {
                    _chargeTime += delta;
                }

                _downTimer += delta;
                if (_upTimer > 0) //第一帧按下扳机
                {
                    DownTrigger();
                    _upTimer = 0;
                }
            }
            else
            {
                _upTimer += delta;
                if (_downTimer > 0) //第一帧松开扳机
                {
                    UpTrigger();
                    _downTimer = 0;
                }
            }

            //连发判断
            if (!_looseShootFlag && _continuousCount > 0 && _delayedTime <= 0 && _attackTimer <= 0)
            {
                //连发开火
                TriggerFire();
                //连发最后一发打完了
                if (Attribute.ManualBeLoaded && _continuousCount <= 0)
                {
                    //执行上膛逻辑
                    RunBeLoaded();
                }
            }

            //散射值销退
            if (_noAttackTime >= Attribute.ScatteringRangeBackDelayTime)
            {
                ScatteringRangeBackHandler(delta);
            }

            _triggerTimer = _triggerTimer > 0 ? _triggerTimer - delta : 0;
            _triggerFlag = false;
            _attackFlag = false;
            
            //武器身回归
            //Position = Position.MoveToward(Vector2.Zero, Attribute.BacklashRegressionSpeed * delta).Rotated(Rotation);
            _currBacklashLength = Mathf.MoveToward(_currBacklashLength, 0, Attribute.BacklashRegressionSpeed * delta);
            if (Master != null)
            {
                Position = new Vector2(_currBacklashLength, 0).Rotated(Rotation);
                if (_attackTimer > 0)
                {
                    RotationDegrees = Mathf.Lerp(
                        _fireAngle, -Attribute.DefaultAngle,
                        Mathf.Clamp((_fireInterval - _attackTimer) * Attribute.UpliftAngleRestore / _fireInterval, 0, 1)
                    );
                }
            }
        }
    }

    /// <summary>
    /// 返回武器是否在地上
    /// </summary>
    /// <returns></returns>
    public bool IsInGround()
    {
        return Master == null && GetParent() == GameApplication.Instance.World.NormalLayer;
    }
    
    /// <summary>
    /// 扳机函数, 调用即视为按下扳机
    /// </summary>
    /// <param name="triggerRole">按下扳机的角色, 如果传 null, 则视为走火</param>
    public void Trigger(Role triggerRole)
    {
        //不能触发扳机
        if (!NoMasterCanTrigger && Master == null) return;

        //这一帧已经按过了, 不需要再按下
        if (_triggerFlag) return;

        //更新武器属性信息
        _triggerFlag = true;
        _triggerRoleFlag = true;
        if (triggerRole != null)
        {
            TriggerRoleIsAi = triggerRole.IsAi;
            TriggerRoleAttackLayer = triggerRole.AttackLayer;
            _weaponAttribute = TriggerRoleIsAi ? _aiWeaponAttribute : _playerWeaponAttribute;
        }
        else if (Master != null)
        {
            TriggerRoleIsAi = Master.IsAi;
            TriggerRoleAttackLayer = Master.AttackLayer;
            _weaponAttribute = TriggerRoleIsAi ? _aiWeaponAttribute : _playerWeaponAttribute;
        }
        
        //是否第一帧按下
        var justDown = _downTimer == 0;
        if (_beLoadedState == 0 || _beLoadedState == -1)  //需要执行上膛操作
        {
            if (justDown && !Reloading && triggerRole != null)
            {
                if (CurrAmmo <= 0)
                {
                    //子弹不够, 触发换弹
                    Reload();
                }
                else if (_attackTimer <= 0)
                {
                    //触发上膛操作
                    BeLoadedHandler();
                }
            }
        }
        else if (_beLoadedState == 1)  //上膛中
        {
            
        }
        else //上膛完成
        {
            //是否能发射
            var flag = false;
            if (_continuousCount <= 0) //不能处于连发状态下
            {
                if (Attribute.ContinuousShoot) //自动射击
                {
                    if (_triggerTimer > 0)
                    {
                        if (_continuousShootFlag)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                        if (_delayedTime <= 0 && _attackTimer <= 0)
                        {
                            _continuousShootFlag = true;
                        }
                    }
                }
                else //半自动
                {
                    if (justDown && _triggerTimer <= 0 && _attackTimer <= 0)
                    {
                        flag = true;
                    }
                }
            }

            if (flag)
            {
                var fireFlag = true; //是否能开火
                if (Reloading) //换弹中
                {
                    fireFlag = false;
                    if (CurrAmmo > 0 && Attribute.AloneReload && Attribute.AloneReloadCanShoot)
                    {
                        //检查是否允许停止换弹
                        if (_aloneReloadState == 2 || _aloneReloadState == 1)
                        {
                            //强制结束
                            _aloneReloadStop = true;
                        }
                    }
                }
                else if (CurrAmmo <= 0) //子弹不够
                {
                    fireFlag = false;
                    if (justDown && triggerRole != null)
                    {
                        //第一帧按下, 触发换弹
                        Reload();
                    }
                }

                if (fireFlag)
                {
                    if (justDown)
                    {
                        //开火前延时
                        if (!Attribute.LooseShoot)
                        {
                            _delayedTime = Attribute.DelayedTime;
                        }
                        //扳机按下间隔
                        _triggerTimer = Attribute.TriggerInterval;
                        //连发数量
                        if (!Attribute.ContinuousShoot)
                        {
                            _continuousCount = Utils.Random.RandomConfigRange(Attribute.ContinuousCountRange);
                        }
                    }

                    if (_delayedTime <= 0 && _attackTimer <= 0)
                    {
                        if (Attribute.LooseShoot) //松发开火
                        {
                            _looseShootFlag = true;
                            OnBeginCharge();
                        }
                        else
                        {
                            //开火
                            TriggerFire();

                            //非连射模式
                            if (!Attribute.ContinuousShoot && Attribute.ManualBeLoaded && _continuousCount <= 0)
                            {
                                //执行上膛逻辑
                                RunBeLoaded();
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 返回是否按下扳机
    /// </summary>
    public bool IsPressTrigger()
    {
        return _triggerFlag;
    }
    
    /// <summary>
    /// 获取本次扳机按下的时长, 单位: 秒
    /// </summary>
    public float GetTriggerDownTime()
    {
        return _downTimer;
    }

    /// <summary>
    /// 获取扳机蓄力时长, 计算按下扳机后从可以开火到当前一共经过了多长时间, 可用于计算蓄力攻击
    /// 注意, 该函数仅在 Attribute.LooseShoot == false 时有正确的返回值, 否则返回 0
    /// </summary>
    public float GetTriggerChargeTime()
    {
        return _chargeTime;
    }
    
    /// <summary>
    /// 获取延时射击倒计时, 单位: 秒
    /// </summary>
    public float GetDelayedAttackTime()
    {
        return _delayedTime;
    }

    /// <summary>
    /// 获取当前需要连发弹药的数量, 配置了 ContinuousCountRange 时生效
    /// </summary>
    public int GetContinuousCount()
    {
        return _continuousCount;
    }

    /// <summary>
    /// 返回是否是攻击间隙时间
    /// </summary>
    public bool IsAttackIntervalTime()
    {
        return _attackTimer > 0 || _triggerTimer > 0;
    }

    /// <summary>
    /// 获取上膛状态,-1: 等待执行自动上膛 , 0: 未上膛, 1: 上膛中, 2: 已经完成上膛
    /// </summary>
    public sbyte GetBeLoadedStateState()
    {
        return _beLoadedState;
    }
    
    /// <summary>
    /// 刚按下扳机
    /// </summary>
    private void DownTrigger()
    {
        OnDownTrigger();
    }

    /// <summary>
    /// 刚松开扳机
    /// </summary>
    private void UpTrigger()
    {
        _continuousShootFlag = false;
        if (_delayedTime > 0)
        {
            _continuousCount = 0;
        }

        //松发开火执行
        if (_looseShootFlag)
        {
            _looseShootFlag = false;
            if (_chargeTime >= Attribute.MinChargeTime) //判断蓄力是否够了
            {
                TriggerFire();
                //非连射模式
                if (!Attribute.ContinuousShoot && Attribute.ManualBeLoaded && _continuousCount <= 0)
                {
                    //执行上膛逻辑
                    RunBeLoaded();
                }
            }
            else //不能攻击
            {
                _continuousCount = 0;
            }
            _chargeTime = 0;
        }

        OnUpTrigger();
    }

    /// <summary>
    /// 触发开火
    /// </summary>
    private void TriggerFire()
    {
        _attackFlag = true;
        _noAttackTime = 0;

        //减子弹数量
        if (_playerWeaponAttribute != _weaponAttribute) //Ai使用该武器, 有一定概率不消耗弹药
        {
            if (Utils.Random.RandomRangeFloat(0, 1) < _weaponAttribute.AiAttackAttr.AmmoConsumptionProbability) //触发消耗弹药
            {
                CurrAmmo -= UseAmmoCount();
            }
        }
        else
        {
            CurrAmmo -= UseAmmoCount();
        }

        if (CurrAmmo == 0)
        {
            _continuousCount = 0;
        }
        else
        {
            _continuousCount = _continuousCount > 0 ? _continuousCount - 1 : 0;
        }

        //开火间隙
        _fireInterval = 60 / Attribute.StartFiringSpeed;
        //攻击冷却
        _attackTimer += _fireInterval;

        //播放开火动画
        if (IsAutoPlaySpriteFrames)
        {
            PlaySpriteAnimation(AnimatorNames.Fire);
        }

        //播放射击音效
        PlayShootSound();
        
        //抛弹
        if ((Attribute.ContinuousShoot || !Attribute.ManualBeLoaded))
        {
            ThrowShellHandler(1f);
        }
        
        //触发开火函数
        OnFire();


        //武器口角度
        var angle = new Vector2(GameConfig.ScatteringDistance, CurrScatteringRange).Angle();

        //先算武器口方向
        var tempRotation = Utils.Random.RandomRangeFloat(-angle, angle);
        var tempAngle = Mathf.RadToDeg(tempRotation);

        //开火时枪口角度
        var fireRotation = tempRotation;
        
        //开火发射的子弹数量
        var bulletCount = Utils.Random.RandomConfigRange(Attribute.FireBulletCountRange);
        if (Master != null)
        {
            bulletCount = Master.RoleState.CallCalcBulletCountEvent(this, bulletCount);
            fireRotation += Master.MountPoint.RealRotation;
        }
        else
        {
            fireRotation += GlobalRotation;
        }
        
        //创建子弹
        for (var i = 0; i < bulletCount; i++)
        {
            //发射子弹
            OnShoot(fireRotation);
        }

        //开火添加散射值
        ScatteringRangeAddHandler();
        
        //武器的旋转角度
        tempAngle -= Attribute.UpliftAngle;
        _fireAngle = tempAngle;
        
        if (Master != null) //被拾起
        {
            //武器身位置
            var max = Mathf.Abs(Mathf.Max(Utils.GetConfigRangeStart(Attribute.BacklashRange), Utils.GetConfigRangeEnd(Attribute.BacklashRange)));
            _currBacklashLength = Mathf.Clamp(
                _currBacklashLength - Utils.Random.RandomConfigRange(Attribute.BacklashRange),
                -max, max
            );
            Position = new Vector2(_currBacklashLength, 0).Rotated(Rotation);
            RotationDegrees = tempAngle;
        }
        else //在地上
        {
            var v = Utils.Random.RandomConfigRange(Attribute.BacklashRange) * 5;
            var externalForce = MoveController.AddForce(new Vector2(-v, 0).Rotated(Rotation), v * 2);
            externalForce.RotationSpeed = -Mathf.DegToRad(40);
            externalForce.RotationResistance = Mathf.DegToRad(80);
        }

        if (IsInGround())
        {
            //在地上弹药打光
            if (IsTotalAmmoEmpty())
            {
                //停止动画
                AnimationPlayer.Stop();
                //清除泛白效果
                SetBlendSchedule(0);
            }
        }
    }

    // /// <summary>
    // /// 触发武器的近战攻击
    // /// </summary>
    // public virtual void TriggerMeleeAttack(Role trigger)
    // {
    //     
    // }
    
    /// <summary>
    /// 获取武器攻击的目标层级
    /// </summary>
    /// <returns></returns>
    public uint GetAttackLayer()
    {
        if (TriggerRoleAttackLayer > 0)
        {
            return (uint)TriggerRoleAttackLayer;
        }
        return Master != null ? Master.AttackLayer : Role.DefaultAttackLayer;
    }
    
    /// <summary>
    /// 返回弹药是否到达上限
    /// </summary>
    public bool IsAmmoFull()
    {
        return CurrAmmo + ResidueAmmo >= Attribute.MaxAmmoCapacity;
    }

    /// <summary>
    /// 返回弹夹是否打空
    /// </summary>
    public bool IsAmmoEmpty()
    {
        return CurrAmmo == 0;
    }
    
    /// <summary>
    /// 返回是否弹药耗尽
    /// </summary>
    public bool IsTotalAmmoEmpty()
    {
        return CurrAmmo + ResidueAmmo == 0;
    }

    /// <summary>
    /// 强制修改当前弹夹弹药量
    /// </summary>
    public void SetCurrAmmo(int count)
    {
        CurrAmmo = Mathf.Clamp(count, 0, Attribute.AmmoCapacity);
    }

    /// <summary>
    /// 强制修改备用弹药量
    /// </summary>
    public void SetResidueAmmo(int count)
    {
        ResidueAmmo = Mathf.Clamp(count, 0, Attribute.MaxAmmoCapacity - CurrAmmo);
    }
    
    /// <summary>
    /// 强制修改弹药量, 优先改动备用弹药
    /// </summary>
    public void SetTotalAmmo(int total)
    {
        if (total < 0)
        {
            return;
        }
        var totalAmmo = CurrAmmo + ResidueAmmo;
        if (totalAmmo == total)
        {
            return;
        }
        
        if (total > totalAmmo) //弹药增加
        {
            ResidueAmmo = Mathf.Min(total - CurrAmmo, Attribute.MaxAmmoCapacity - CurrAmmo);
        }
        else //弹药减少
        {
            if (CurrAmmo < total)
            {
                ResidueAmmo = total - CurrAmmo;
            }
            else
            {
                CurrAmmo = total;
                ResidueAmmo = 0;
            }
        }
    }

    /// <summary>
    /// 拾起的弹药数量, 如果到达容量上限, 则返回拾取完毕后剩余的弹药数量
    /// </summary>
    /// <param name="count">弹药数量</param>
    private int PickUpAmmo(int count)
    {
        var num = ResidueAmmo;
        ResidueAmmo = Mathf.Min(ResidueAmmo + count, Attribute.MaxAmmoCapacity - CurrAmmo);
        return count - ResidueAmmo + num;
    }

    /// <summary>
    /// 触发换弹
    /// </summary>
    public void Reload()
    {
        if (CurrAmmo < Attribute.AmmoCapacity && ResidueAmmo > 0 && !Reloading && _beLoadedState != 1)
        {
            Reloading = true;
            _playReloadFinishSoundFlag = false;

            //播放开始换弹音效
            PlayBeginReloadSound();
            
            // Debug.Log("开始换弹.");
            //抛弹
            if (!Attribute.ContinuousShoot && (_beLoadedState == 0 || _beLoadedState == -1) && Attribute.BeLoadedTime > 0)
            {
                ThrowShellHandler(0.6f);
            }
            
            //第一次换弹
            OnBeginReload();

            if (Attribute.AloneReload)
            {
                //单独换弹, 特殊处理
                AloneReloadHandler();
            }
            else
            {
                //普通换弹处理
                ReloadHandler();
            }
            
            //上膛标记完成
            _beLoadedState = 2;
        }
    }

    /// <summary>
    /// 强制停止换弹, 或者结束换弹状态
    /// </summary>
    public void StopReload()
    {
        _aloneReloadState = 0;
        Reloading = false;
        _reloadTimer = 0;
        _reloadUseTime = 0;
    }
    
    //播放换弹开始音效
    private void PlayBeginReloadSound()
    {
        if (Attribute.BeginReloadSound != null)
        {
            var position = GameApplication.Instance.ViewToGlobalPosition(GlobalPosition);
            var volume = SoundManager.CalcRoleVolume(Attribute.BeginReloadSound.Volume, Master);
            if (Attribute.BeginReloadSoundDelayTime <= 0)
            {
                SoundManager.PlaySoundEffectPosition(Attribute.BeginReloadSound.Path, position, volume);
            }
            else
            {
                SoundManager.PlaySoundEffectPositionDelay(Attribute.BeginReloadSound.Path, position, Attribute.BeginReloadSoundDelayTime, volume);
            }
        }
    }
    
    //播放换弹音效
    private void PlayReloadSound()
    {
        if (Attribute.ReloadSound != null)
        {
            var position = GameApplication.Instance.ViewToGlobalPosition(GlobalPosition);
            var volume = SoundManager.CalcRoleVolume(Attribute.ReloadSound.Volume, Master);
            if (Attribute.ReloadSoundDelayTime <= 0)
            {
                SoundManager.PlaySoundEffectPosition(Attribute.ReloadSound.Path, position, volume);
            }
            else
            {
                SoundManager.PlaySoundEffectPositionDelay(Attribute.ReloadSound.Path, position, Attribute.ReloadSoundDelayTime, volume);
            }
        }
    }
    
    //播放换弹完成音效
    private void PlayReloadFinishSound()
    {
        if (Attribute.ReloadFinishSound != null)
        {
            var volume = SoundManager.CalcRoleVolume(Attribute.ReloadFinishSound.Volume, Master);
            var position = GameApplication.Instance.ViewToGlobalPosition(GlobalPosition);
            SoundManager.PlaySoundEffectPosition(Attribute.ReloadFinishSound.Path, position, volume);
        }
    }

    //播放射击音效
    private void PlayShootSound()
    {
        if (Attribute.ShootSound != null)
        {
            var volume = SoundManager.CalcRoleVolume(Attribute.ShootSound.Volume, Master);
            var position = GameApplication.Instance.ViewToGlobalPosition(GlobalPosition);
            SoundManager.PlaySoundEffectPosition(Attribute.ShootSound.Path, position, volume);
        }
    }

    //播放上膛音效
    private void PlayBeLoadedSound()
    {
        if (Attribute.BeLoadedSound != null)
        {
            var position = GameApplication.Instance.ViewToGlobalPosition(GlobalPosition);
            var volume = SoundManager.CalcRoleVolume(Attribute.BeLoadedSound.Volume, Master);
            if (Attribute.BeLoadedSoundDelayTime <= 0)
            {
                SoundManager.PlaySoundEffectPosition(Attribute.BeLoadedSound.Path, position, volume);
            }
            else
            {
                SoundManager.PlaySoundEffectPositionDelay(Attribute.BeLoadedSound.Path, position, Attribute.BeLoadedSoundDelayTime, volume);
            }
        }
    }

    //执行上膛逻辑
    private void RunBeLoaded()
    {
        if (Attribute.AutoManualBeLoaded)
        {
            if (_attackTimer <= 0)
            {
                //执行自动上膛
                BeLoadedHandler();
            }
            else if (CurrAmmo > 0)
            {
                //等待执行自动上膛
                _beLoadedState = -1;
            }
            else
            {
                //没子弹了, 需要手动上膛
                _beLoadedState = 0;
            }
        }
        else
        {
            //手动上膛
            _beLoadedState = 0;
        }
    }

    //单独换弹处理
    private void AloneReloadHandler()
    {
        if (Attribute.AloneReloadBeginIntervalTime <= 0)
        {
            //开始装第一颗子弹
            _aloneReloadState = 2;
            ReloadHandler();
        }
        else
        {
            _aloneReloadState = 1;
            _reloadTimer = Attribute.AloneReloadBeginIntervalTime;
        }
    }

    //换弹处理逻辑
    private void ReloadHandler()
    {
        _reloadTimer = Attribute.ReloadTime;
        
        //播放换弹动画
        if (IsAutoPlaySpriteFrames)
        {
            PlaySpriteAnimation(AnimatorNames.Reloading);
        }
            
        //播放换弹音效
        PlayReloadSound();
            
        OnReload();
        // Debug.Log("装弹.");
    }
    
    //换弹完成处理逻辑
    private void ReloadFinishHandler()
    {
        // Debug.Log("装弹完成.");
        OnReloadFinish();
    }

    //单独装弹完成
    private void AloneReloadStateFinish()
    {
        // Debug.Log("单独装弹完成.");
        OnAloneReloadStateFinish();
    }

    //上膛处理
    private void BeLoadedHandler()
    {
        //上膛抛弹
        if (!Attribute.ContinuousShoot && Attribute.BeLoadedTime > 0)
        {
            ThrowShellHandler(0.6f);
        }

        //开始上膛
        OnBeginBeLoaded();

        //上膛时间为0, 直接结束上膛
        if (Attribute.BeLoadedTime <= 0)
        {
            //直接上膛完成
            _beLoadedState = 2;
            OnBeLoadedFinish();
            return;
        }
        
        //上膛中
        _beLoadedState = 1;
        _beLoadedStateTimer = Attribute.BeLoadedTime;
        
        //播放上膛动画
        if (IsAutoPlaySpriteFrames)
        {
            if (Attribute.BeLoadedSoundDelayTime <= 0)
            {
                PlaySpriteAnimation(AnimatorNames.BeLoaded);
            }
            else
            {
                this.CallDelay(Attribute.BeLoadedSoundDelayTime, PlaySpriteAnimation, AnimatorNames.BeLoaded);
            }
        }

        //播放上膛音效
        PlayBeLoadedSound();
    }

    //抛弹逻辑
    private void ThrowShellHandler(float speedScale)
    {
        if (string.IsNullOrEmpty(Attribute.ShellId))
        {
            return;
        }
        //创建一个弹壳
        if (Attribute.ThrowShellDelayTime > 0)
        {
            this.CallDelay(Attribute.ThrowShellDelayTime, () => ThrowShell(Attribute.ShellId, speedScale));
        }
        else if (Attribute.ThrowShellDelayTime == 0)
        {
            ThrowShell(Attribute.ShellId, speedScale);
        }
    }

    //散射值消退处理
    private void ScatteringRangeBackHandler(float delta)
    {
        var startScatteringRange = Attribute.StartScatteringRange;
        var finalScatteringRange = Attribute.FinalScatteringRange;
        if (Master != null)
        {
            startScatteringRange = Master.RoleState.CallCalcStartScatteringEvent(this, startScatteringRange);
            finalScatteringRange = Master.RoleState.CallCalcFinalScatteringEvent(this, finalScatteringRange);
        }
        if (startScatteringRange <= finalScatteringRange)
        {
            CurrScatteringRange = Mathf.Max(CurrScatteringRange - Attribute.ScatteringRangeBackSpeed * delta,
                startScatteringRange);
        }
        else
        {
            CurrScatteringRange = Mathf.Min(CurrScatteringRange + Attribute.ScatteringRangeBackSpeed * delta,
                startScatteringRange);
        }
    }

    //散射值添加处理
    private void ScatteringRangeAddHandler()
    {
        var startScatteringRange = Attribute.StartScatteringRange;
        var finalScatteringRange = Attribute.FinalScatteringRange;
        if (Master != null)
        {
            startScatteringRange = Master.RoleState.CallCalcStartScatteringEvent(this, startScatteringRange);
            finalScatteringRange = Master.RoleState.CallCalcFinalScatteringEvent(this, finalScatteringRange);
        }
        if (startScatteringRange <= finalScatteringRange)
        {
            CurrScatteringRange = Mathf.Min(CurrScatteringRange + Attribute.ScatteringRangeAddValue,
                finalScatteringRange);
        }
        else
        {
            CurrScatteringRange = Mathf.Min(CurrScatteringRange - Attribute.ScatteringRangeAddValue,
                finalScatteringRange);
        }
    }

    /// <summary>
    /// 换弹计时器时间到, 执行换弹操作
    /// </summary>
    private void ReloadSuccess()
    {
        if (Attribute.AloneReload) //单独装填
        {
            if (ResidueAmmo >= Attribute.AloneReloadCount) //剩余子弹充足
            {
                if (CurrAmmo + Attribute.AloneReloadCount <= Attribute.AmmoCapacity)
                {
                    ResidueAmmo -= Attribute.AloneReloadCount;
                    CurrAmmo += Attribute.AloneReloadCount;
                }
                else //子弹满了
                {
                    var num = Attribute.AmmoCapacity - CurrAmmo;
                    CurrAmmo = Attribute.AmmoCapacity;
                    ResidueAmmo -= num;
                }
            }
            else if (ResidueAmmo != 0) //剩余子弹不足
            {
                if (ResidueAmmo + CurrAmmo <= Attribute.AmmoCapacity)
                {
                    CurrAmmo += ResidueAmmo;
                    ResidueAmmo = 0;
                }
                else //子弹满了
                {
                    var num = Attribute.AmmoCapacity - CurrAmmo;
                    CurrAmmo = Attribute.AmmoCapacity;
                    ResidueAmmo -= num;
                }
            }

            if (!_aloneReloadStop && ResidueAmmo != 0 && CurrAmmo != Attribute.AmmoCapacity) //继续装弹
            {
                ReloadHandler();
            }
        }
        else //换弹结束
        {
            if (CurrAmmo + ResidueAmmo >= Attribute.AmmoCapacity)
            {
                ResidueAmmo -= Attribute.AmmoCapacity - CurrAmmo;
                CurrAmmo = Attribute.AmmoCapacity;
            }
            else
            {
                CurrAmmo += ResidueAmmo;
                ResidueAmmo = 0;
            }

            StopReload();
            ReloadFinishHandler();
        }
    }
    
    //播放动画
    private void PlaySpriteAnimation(string name)
    {
        var spriteFrames = AnimatedSprite.SpriteFrames;
        if (spriteFrames != null && spriteFrames.HasAnimation(name))
        {
            AnimatedSprite.Play(name);
        }
    }

    //帧动画播放结束
    private void OnAnimationFinished()
    {
        // Debug.Log("帧动画播放结束...");
        AnimatedSprite.Play(AnimatorNames.Default);
    }

    public override CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        var result = new CheckInteractiveResult(this);

        if (master is Role roleMaster) //碰到角色
        {
            if (Master == null)
            {
                var masterWeapon = roleMaster.WeaponPack.ActiveItem;
                //查找是否有同类型武器
                var index = roleMaster.WeaponPack.FindIndex(ItemConfig.Id);
                if (index != -1) //如果有这个武器
                {
                    if (CurrAmmo + ResidueAmmo != 0) //子弹不为空
                    {
                        var targetWeapon = roleMaster.WeaponPack.GetItem(index);
                        if (!targetWeapon.IsAmmoFull()) //背包里面的武器子弹未满
                        {
                            //可以互动拾起弹药
                            result.CanInteractive = true;
                            result.Type = CheckInteractiveResult.InteractiveType.Bullet;
                            return result;
                        }
                    }
                }
                else //没有武器
                {
                    if (roleMaster.WeaponPack.HasVacancy()) //有空位, 能拾起武器
                    {
                        //可以互动, 拾起武器
                        result.CanInteractive = true;
                        result.Type = CheckInteractiveResult.InteractiveType.PickUp;
                        return result;
                    }
                    else if (masterWeapon != null) //替换武器  // && masterWeapon.Attribute.WeightType == Attribute.WeightType)
                    {
                        //可以互动, 切换武器
                        result.CanInteractive = true;
                        result.Type = CheckInteractiveResult.InteractiveType.Replace;
                        return result;
                    }
                }
            }
        }

        return result;
    }

    public override void Interactive(ActivityObject master)
    {
        if (master is Role roleMaster) //与role互动
        {
            var holster = roleMaster.WeaponPack;
            //查找是否有同类型武器
            var index = holster.FindIndex(ItemConfig.Id);
            if (index != -1) //如果有这个武器
            {
                if (CurrAmmo + ResidueAmmo == 0) //没有子弹了
                {
                    return;
                }

                var weapon = holster.GetItem(index);
                //子弹上限
                var maxCount = Attribute.MaxAmmoCapacity;
                //是否捡到子弹
                var flag = false;
                if (ResidueAmmo > 0 && weapon.CurrAmmo + weapon.ResidueAmmo < maxCount)
                {
                    var count = weapon.PickUpAmmo(ResidueAmmo);
                    if (count != ResidueAmmo)
                    {
                        ResidueAmmo = count;
                        flag = true;
                    }
                }

                if (CurrAmmo > 0 && weapon.CurrAmmo + weapon.ResidueAmmo < maxCount)
                {
                    var count = weapon.PickUpAmmo(CurrAmmo);
                    if (count != CurrAmmo)
                    {
                        CurrAmmo = count;
                        flag = true;
                    }
                }

                //播放互动效果
                if (flag)
                {
                    Throw(GlobalPosition, 0, Utils.Random.RandomRangeInt(20, 50), Vector2.Zero, Utils.Random.RandomRangeInt(-180, 180));
                    //没有子弹了, 停止播放泛白效果
                    if (IsTotalAmmoEmpty())
                    {
                        //停止动画
                        AnimationPlayer.Stop();
                        //清除泛白效果
                        SetBlendSchedule(0);
                    }
                }
            }
            else //没有武器
            {
                if (!holster.HasVacancy()) //没有空位置, 扔掉当前武器
                {
                    //替换武器
                    roleMaster.ThrowWeapon();
                }
                roleMaster.PickUpWeapon(this);
            }
        }
    }

    /// <summary>
    /// 获取当前武器真实的旋转角度(弧度制), 由于武器旋转时加入了旋转吸附, 所以需要通过该函数来来知道当前武器的真实旋转角度
    /// </summary>
    public float GetRealGlobalRotation()
    {
        return Mathf.DegToRad(Master.MountPoint.RealRotationDegrees) + Rotation;
    }

    /// <summary>
    /// 触发扔掉武器时抛出的效果, 并不会管武器是否在武器背包中
    /// </summary>
    /// <param name="master">触发扔掉该武器的的角色</param>
    public void ThrowWeapon(Role master)
    {
        ThrowWeapon(master, master.GlobalPosition);
    }

    /// <summary>
    /// 触发扔掉武器时抛出的效果, 并不会管武器是否在武器背包中
    /// </summary>
    /// <param name="master">触发扔掉该武器的的角色</param>
    /// <param name="startPosition">投抛起始位置</param>
    public void ThrowWeapon(Role master, Vector2 startPosition)
    {
        //阴影偏移
        ShadowOffset = new Vector2(0, 2);

        if (master.Face == FaceDirection.Left)
        {
            Scale *= new Vector2(1, -1);
        }

        var rotation = master.MountPoint.GlobalRotation;
        GlobalRotation = rotation;

        startPosition -= GripPoint.Position.Rotated(rotation);
        var startHeight = -master.MountPoint.Position.Y;
        var velocity = new Vector2(20, 0).Rotated(rotation);
        var yf = Utils.Random.RandomRangeInt(50, 70);
        Throw(startPosition, startHeight, yf, velocity, 0);
        
        //继承role的移动速度
        InheritVelocity(master);
    }

    protected override void OnThrowStart()
    {
        //禁用碰撞
        //Collision.Disabled = true;
        //AnimationPlayer.Play(AnimatorNames.Floodlight);
    }

    protected override void OnThrowOver()
    {
        //启用碰撞
        //Collision.Disabled = false;
        //还有弹药, 播放泛白效果
        if (!IsTotalAmmoEmpty())
        {
            AnimationPlayer.Play(AnimatorNames.Floodlight);
        }
    }

    /// <summary>
    /// 触发启用武器, 这个函数由 Holster 对象调用
    /// </summary>
    private void Active()
    {
        //调整阴影
        //ShadowOffset = new Vector2(0, Master.GlobalPosition.Y - GlobalPosition.Y);
        ShadowOffset = new Vector2(0, -Master.MountPoint.Position.Y);
        //枪口默认抬起角度
        RotationDegrees = -Attribute.DefaultAngle;
        ShowShadowSprite();
        OnActive();
    }

    /// <summary>
    /// 触发收起武器, 这个函数由 Holster 对象调用
    /// </summary>
    private void Conceal()
    {
        HideShadowSprite();
        OnConceal();
    }
    
    public void OnRemoveItem()
    {
        //要重置部分重要属性属性
        if (Master.IsAi)
        {
            _triggerTimer = 0;
        }
        GetParent().RemoveChild(this);
        _triggerRoleFlag = false;
        _weaponAttribute = _playerWeaponAttribute;
        CollisionLayer = _tempLayer;
        AnimatedSprite.Position = _tempAnimatedSpritePosition;
        //清除 Ai 拾起标记
        RemoveSign(SignNames.AiFindWeaponSign);
        OnRemove(Master);
    }

    public void OnPickUpItem()
    {
        Pickup();
        _triggerRoleFlag = false;
        _weaponAttribute = Master.IsAi ? _aiWeaponAttribute : _playerWeaponAttribute;
        //停止动画
        AnimationPlayer.Stop();
        //清除泛白效果
        SetBlendSchedule(0);
        ZIndex = 0;
        //禁用碰撞
        //Collision.Disabled = true;
        //精灵位置
        _tempAnimatedSpritePosition = AnimatedSprite.Position;
        var position = GripPoint.Position;
        AnimatedSprite.Position = new Vector2(-position.X, -position.Y);
        //修改层级
        _tempLayer = CollisionLayer;
        CollisionLayer = PhysicsLayer.OnHand;
        //清除 Ai 拾起标记
        RemoveSign(SignNames.AiFindWeaponSign);
        OnPickUp(Master);
    }

    public void OnActiveItem()
    {
        //更改父节点
        var parent = GetParentOrNull<Node>();
        if (parent == null)
        {
            Master.MountPoint.AddChild(this);
        }
        else if (parent != Master.MountPoint)
        {
            parent.RemoveChild(this);
            Master.MountPoint.AddChild(this);
        }

        Position = Vector2.Zero;
        Scale = Vector2.One;
        RotationDegrees = 0;
        Visible = true;
        Active();
    }

    public void OnConcealItem()
    {
        var tempParent = GetParentOrNull<Node2D>();
        if (tempParent != null)
        {
            tempParent.RemoveChild(this);
            Master.BackMountPoint.AddChild(this);
            Master.OnPutBackMount(this, PackageIndex);
            Conceal();
        }
    }

    public void OnOverflowItem()
    {
        Master.ThrowWeapon(PackageIndex);
    }

    /// <summary>
    /// 获取相对于武器本地坐标的开火位置
    /// </summary>
    public Vector2 GetLocalFirePosition()
    {
        return AnimatedSprite.Position + FirePoint.Position;
    }
    
    /// <summary>
    /// 获取相对于武器本地坐标的抛壳位置
    /// </summary>
    public Vector2 GetLocalShellPosition()
    {
        return AnimatedSprite.Position + ShellPoint.Position;
    }
    
    //-------------------------- ----- 子弹相关 -----------------------------

    /// <summary>
    /// 投抛弹壳的默认实现方式, shellId为弹壳id
    /// </summary>
    protected ActivityObject ThrowShell(string shellId, float speedScale = 1)
    {
        var startPos = ShellPoint.GlobalPosition;
        float startHeight;
        if (Master != null)
        {
            var shellPosition = (Master != null ? Master.MountPoint.Position : Position) + ShellPoint.Position;
            startHeight = -shellPosition.Y;
            startPos.Y += startHeight;
        }
        else
        {
            startHeight = Altitude;
        }

        var direction = GlobalRotationDegrees + Utils.Random.RandomRangeInt(-30, 30) + 180;
        var verticalSpeed = Utils.Random.RandomRangeInt((int)(60 * speedScale), (int)(120 * speedScale));
        var velocity = new Vector2(Utils.Random.RandomRangeInt((int)(20 * speedScale), (int)(60 * speedScale)), 0).Rotated(direction * Mathf.Pi / 180);
        var rotate = Utils.Random.RandomRangeInt((int)(-720 * speedScale), (int)(720 * speedScale));
        var shell = Create(shellId);
        shell.Rotation = (Master != null ? Master.MountPoint.RealRotation : Rotation);
        shell.Throw(startPos, startHeight, verticalSpeed, velocity, rotate);
        shell.InheritVelocity(Master != null ? Master : this);
        if (Master == null)
        {
            AffiliationArea.InsertItem(shell);
        }
        else
        {
            Master.AffiliationArea.InsertItem(shell);
        }
        
        return shell;
    }

    /// <summary>
    /// 发射子弹的默认实现方式, bulletId为子弹id
    /// </summary>
    protected Bullet ShootBullet(float fireRotation, string bulletId)
    {
        var speed = Utils.Random.RandomConfigRange(Attribute.BulletSpeedRange);
        var distance = Utils.Random.RandomConfigRange(Attribute.BulletDistanceRange);
        var deviationAngle = Utils.Random.RandomConfigRange(Attribute.BulletDeviationAngleRange);
        if (Master != null)
        {
            speed = Master.RoleState.CallCalcBulletSpeedEvent(this, speed);
            distance = Master.RoleState.CallCalcBulletDistanceEvent(this, distance);
            deviationAngle = Master.RoleState.CallCalcBulletDeviationAngleEvent(this, deviationAngle);
        }

        var attackLayer = GetAttackLayer();
        //创建子弹
        var bullet = Create<Bullet>(bulletId);
        bullet.Init(
            TriggerRoleIsAi,
            this,
            speed,
            distance,
            FirePoint.GlobalPosition,
            fireRotation + Mathf.DegToRad(deviationAngle),
            attackLayer
        );
        bullet.MinHarm = Utils.GetConfigRangeStart(Attribute.HarmRange);
        bullet.MaxHarm = Utils.GetConfigRangeEnd(Attribute.HarmRange);
        bullet.PutDown(RoomLayerEnum.YSortLayer);
        return bullet;
    }
    
    //-------------------------------- Ai相关 -----------------------------

    /// <summary>
    /// Ai 调用, 刷新 Ai 攻击状态并返回, 并不会调用相应的函数
    /// </summary>
    public AiAttackState AiRefreshAttackState()
    {
        AiAttackState flag;
        if (IsTotalAmmoEmpty()) //当前武器弹药打空
        {
            //切换到有子弹的武器
            var index = Master.WeaponPack.FindIndex((we, i) => !we.IsTotalAmmoEmpty());
            if (index != -1)
            {
                flag = AiAttackState.ExchangeWeapon;
            }
            else //所有子弹打光
            {
                flag = AiAttackState.NoAmmo;
            }
        }
        else if (Reloading) //换弹中
        {
            flag = AiAttackState.TriggerReload;
        }
        else if (IsAmmoEmpty()) //弹夹已经打空
        {
            flag = AiAttackState.Reloading;
        }
        else if (_beLoadedState == 0 || _beLoadedState == -1) //需要上膛
        {
            flag = AiAttackState.AttackInterval;
        }
        else if (_beLoadedState == 1) //上膛中
        {
            flag = AiAttackState.AttackInterval;
        }
        else if (_continuousCount >= 1) //连发中
        {
            flag = AiAttackState.Attack;
        }
        else if (IsAttackIntervalTime()) //开火间隙
        {
            flag = AiAttackState.AttackInterval;
        }
        else
        {
            var enemy = (Enemy)Master;
            if (enemy.GetLockTargetTime() >= Attribute.AiAttackAttr.LockingTime) //正常射击
            {
                if (GetDelayedAttackTime() > 0)
                {
                    flag = AiAttackState.Attack;
                }
                else
                {
                    if (Attribute.ContinuousShoot) //连发
                    {
                        flag = AiAttackState.Attack;
                    }
                    else //单发
                    {
                        flag = AiAttackState.Attack;
                    }
                }
            }
            else //锁定时间没到
            {
                flag = AiAttackState.LockingTime;
            }
        }

        return flag;
    }
    
    /// <summary>
    /// Ai调用, 触发扣动扳机, 并返回攻击状态
    /// </summary>
    public AiAttackState AiTriggerAttackState()
    {
        AiAttackState flag;
        if (IsTotalAmmoEmpty()) //当前武器弹药打空
        {
            //切换到有子弹的武器
            var index = Master.WeaponPack.FindIndex((we, i) => !we.IsTotalAmmoEmpty());
            if (index != -1)
            {
                flag = AiAttackState.ExchangeWeapon;
                Master.WeaponPack.ExchangeByIndex(index);
            }
            else //所有子弹打光
            {
                flag = AiAttackState.NoAmmo;
            }
        }
        else if (Reloading) //换弹中
        {
            flag = AiAttackState.TriggerReload;
        }
        else if (IsAmmoEmpty()) //弹夹已经打空
        {
            flag = AiAttackState.Reloading;
            Reload();
        }
        else if (_beLoadedState == 0 || _beLoadedState == -1) //需要上膛
        {
            flag = AiAttackState.AttackInterval;
            Master.Attack();
        }
        else if (_beLoadedState == 1) //上膛中
        {
            flag = AiAttackState.AttackInterval;
        }
        else if (_continuousCount >= 1) //连发中
        {
            flag = AiAttackState.Attack;
        }
        else if (IsAttackIntervalTime()) //开火间隙
        {
            flag = AiAttackState.AttackInterval;
        }
        else
        {
            var enemy = (Enemy)Master;
            if (enemy.GetLockTargetTime() >= Attribute.AiAttackAttr.LockingTime) //正常射击
            {
                if (GetDelayedAttackTime() > 0)
                {
                    flag = AiAttackState.Attack;
                    enemy.Attack();
                    if (_attackFlag)
                    {
                        enemy.SetLockTargetTime(0);
                    }
                }
                else
                {
                    if (Attribute.ContinuousShoot) //连发
                    {
                        flag = AiAttackState.Attack;
                        enemy.Attack();
                        if (_attackFlag)
                        {
                            enemy.SetLockTargetTime(0);
                        }
                    }
                    else //单发
                    {
                        flag = AiAttackState.Attack;
                        enemy.Attack();
                        if (_attackFlag)
                        {
                            enemy.SetLockTargetTime(0);
                        }
                    }
                }
            }
            else //锁定时间没到
            {
                flag = AiAttackState.LockingTime;
            }
        }

        Debug.Log("state: " + flag);
        return flag;
    }

    // /// <summary>
    // /// 获取 Ai 对于该武器的评分, 评分越高, 代表 Ai 会越优先选择该武器, 如果为 -1, 则表示 Ai 不会使用该武器
    // /// </summary>
    // public float GetAiScore()
    // {
    //     return 1;
    // }
}