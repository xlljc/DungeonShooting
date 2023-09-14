
using Godot;

/// <summary>
/// 主动使用道具
/// </summary>
public abstract partial class ActiveProp : Prop, IPackageItem
{
    public int PackageIndex { get; set; }
    
    /// <summary>
    /// 道具是否可以叠加
    /// </summary>
    public bool Superposition { get; set; } = false;
    
    /// <summary>
    /// 道具可使用次数
    /// </summary>
    public int Count
    {
        get => _count;
        set
        {
            var temp = _count;
            _count = Mathf.Clamp(value, 0, _maxCount);
            if (temp != _count)
            {
                OnChangeCount();
            }
        }
    }

    private int _count = 1;

    /// <summary>
    /// 道具最大叠加用次数
    /// </summary>
    public int MaxCount
    {
        get => _maxCount;
        set
        {
            var temp = _maxCount;
            _maxCount = Mathf.Max(1, value);
            if (temp != _maxCount)
            {
                OnChangeMaxCount();
            }

            if (Count > _maxCount)
            {
                Count = _maxCount;
            }
        }
    }

    private int _maxCount = 1;

    /// <summary>
    /// 使用一次后的冷却时间, 单位: 秒
    /// </summary>
    public float CooldownTime { get; set; } = 2f;
    
    /// <summary>
    /// 当道具使用完后是否自动销毁
    /// </summary>
    public bool AutoDestroy { get; set; } = false;

    /// <summary>
    /// 道具充能进度, 必须要充能完成才能使用, 值范围: 0 - 1
    /// </summary>
    public float ChargeProgress
    {
        get => _chargeProgress;
        set
        {
            var temp = _chargeProgress;
            _chargeProgress = Mathf.Clamp(value, 0, 1);
            if (temp != _chargeProgress)
            {
                OnChangeChargeProgress();
            }
        }
    }

    private float _chargeProgress = 1;
    
    /// <summary>
    /// 自动充能速度, 也就是每秒充能进度, 如果为0则表示不就行自动充能
    /// </summary>
    public float AutoChargeSpeed { get; set; }

    //冷却计时器
    private float _cooldownTimer = 0;
    
    /// <summary>
    /// 当检测是否可以使用时调用
    /// </summary>
    public abstract bool OnCheckUse();

    /// <summary>
    /// 当道具被使用时调用, 函数返回值为消耗数量
    /// </summary>
    protected abstract int OnUse();

    /// <summary>
    /// 道具数量改变时调用
    /// </summary>
    protected virtual void OnChangeCount()
    {
    }

    /// <summary>
    /// 道具最大数量改变时调用
    /// </summary>
    protected virtual void OnChangeMaxCount()
    {
    }

    /// <summary>
    /// 道具充能进度改变时调用
    /// </summary>
    protected virtual void OnChangeChargeProgress()
    {
    }

    /// <summary>
    /// 冷却完成时调用
    /// </summary>
    protected virtual void OnCooldownFinish()
    {
    }

    protected override void ProcessOver(float delta)
    {
        RunUpdate(delta);
    }

    public override void PackProcess(float delta)
    {
        RunUpdate(delta);
    }

    private void RunUpdate(float delta)
    {
        if (CheckAutoDestroy())
        {
            return;
        }
        //冷却
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= delta;

            //冷却完成
            if (_cooldownTimer <= 0)
            {
                _cooldownTimer = 0;
                OnCooldownFinish();
            }
        }
        
        //自动充能
        if (AutoChargeSpeed > 0 && ChargeProgress < 1)
        {
            ChargeProgress += AutoChargeSpeed * delta;
        }
    }

    //检测是否达到自动销毁的条件
    private bool CheckAutoDestroy()
    {
        if (Count == 0 && AutoDestroy) //用光了, 自动销毁
        {
            if (Master != null)
            {
                Master.ActivePropsPack.RemoveItem(this);
            }
            Destroy();
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// 检测是否可以使用当前道具
    /// </summary>
    public bool CheckUse()
    {
        return ChargeProgress >= 1 && _cooldownTimer <= 0 && Count > 0 && OnCheckUse();
    }
    
    /// <summary>
    /// 触发使用道具
    /// </summary>
    public void Use()
    {
        if (Master == null)
        {
            return;
        }
        if (CheckUse()) //可以使用道具
        {
            var num = OnUse();
            if (num != 0)
            {
                Count -= num;
            }

            //冷却计时器
            _cooldownTimer = CooldownTime;
        }
    }

    /// <summary>
    /// 获取冷却进度 0 - 1
    /// </summary>
    public float GetCooldownProgress()
    {
        if (_cooldownTimer <= 0)
        {
            return 1;
        }

        return (CooldownTime - _cooldownTimer) / CooldownTime;
    }

    public override void Interactive(ActivityObject master)
    {
        if (master is Player player)
        {
            var item = player.ActivePropsPack.GetItemById(ItemConfig.Id);
            if (item == null) //没有同类型物体
            {
                if (!player.ActivePropsPack.HasVacancy()) //没有空位置, 扔掉当前道具
                {
                    player.ThrowActiveProp(player.ActivePropsPack.ActiveIndex);
                }
                //替换手中的道具
                if (player.PickUpActiveProp(this))
                {
                    Pickup();
                }
            }
            else
            {
                //处理同类型道具
                if (Superposition && item.Count < item.MaxCount) //允许叠加
                {
                    if (item.Count + Count > item.MaxCount)
                    {
                        Count -= item.MaxCount - item.Count;
                        item.Count = item.MaxCount;
                    }
                    else
                    {
                        item.Count += Count;
                        Count = 0;
                    }
                    Destroy();
                }
            }
        }
    }

    public override CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        if (master is Player player)
        {
            //查找相同类型的道具
            var item = player.ActivePropsPack.GetItemById(ItemConfig.Id);
            if (item == null) //没有同类型物体
            {
                if (player.ActivePropsPack.HasVacancy()) //还有空位, 拾起道具
                {
                    return new CheckInteractiveResult(this, true, CheckInteractiveResult.InteractiveType.PickUp);
                }

                //替换手中的道具
                return new CheckInteractiveResult(this, true, CheckInteractiveResult.InteractiveType.Replace);
            }

            //处理同类型道具
            if (Superposition && item.Count < item.MaxCount) //允许叠加
            {
                return new CheckInteractiveResult(this, true, CheckInteractiveResult.InteractiveType.Bullet);
            }

            //该道具不能拾起
            return new CheckInteractiveResult(this);
        }

        return new CheckInteractiveResult(this);
    }

    public override void OnPickUpItem()
    {
    }

    public override void OnRemoveItem()
    {
    }

    public virtual void OnActiveItem()
    {
    }

    public virtual void OnConcealItem()
    {
    }

    public virtual void OnOverflowItem()
    {
    }
}