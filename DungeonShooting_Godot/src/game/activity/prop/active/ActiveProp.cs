
using Godot;

/// <summary>
/// 主动使用道具
/// </summary>
public abstract partial class ActiveProp : Prop
{
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
    /// 道具最大可使用次数
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

    protected override void OnPickUp(Role master)
    {
    }

    protected override void OnRemove(Role master)
    {
    }

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
    
    public override void PackProcess(float delta)
    {
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
            if (Count == 0 && AutoDestroy) //用光了, 自动销毁
            {
                Master.RemoveProp(this);
                Destroy();
            }
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
}