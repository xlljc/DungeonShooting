
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
    /// 道具当前叠加数量
    /// </summary>
    public int OverlaysCount { get; set; } = 1;

    /// <summary>
    /// 道具最大叠加数量
    /// </summary>
    public int MaxOverlaysCount { get; set; } = 1;
    
    /// <summary>
    /// 使用一次后的冷却时间, 单位: 秒
    /// </summary>
    public float CooldownTime { get; set; } = 1f;
    
    /// <summary>
    /// 当道具使用完后是否自动销毁
    /// </summary>
    public bool AutoDestroy { get; set; } = true;

    /// <summary>
    /// 道具充能进度, 必须要充能完成才能使用, 值范围: 0 - 1
    /// </summary>
    public float ChargeProgress { get; set; } = 1;
    
    /// <summary>
    /// 是否可以使用
    /// </summary>
    public abstract bool CanUse();
    
    /// <summary>
    /// 当道具被使用时调用
    /// </summary>
    protected abstract void OnUse();

    protected override void OnPickUp(Role master)
    {
    }

    protected override void OnRemove(Role master)
    {
    }

    protected virtual void OnChangeCount()
    {
    }

    protected virtual void OnChangeMaxCount()
    {
    }
    
    /// <summary>
    /// 触发使用道具
    /// </summary>
    public void Use()
    {
        if (ChargeProgress >= 1 && CanUse())
        {
            OnUse();
        }
    }
}