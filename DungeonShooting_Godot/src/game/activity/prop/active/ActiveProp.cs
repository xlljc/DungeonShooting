
/// <summary>
/// 主动使用道具
/// </summary>
public abstract partial class ActiveProp : Prop
{
    /// <summary>
    /// 是否可以使用
    /// </summary>
    public bool CanUse { get; set; }
    
    /// <summary>
    /// 当道具被使用时调用
    /// </summary>
    protected abstract void OnUse();

    /// <summary>
    /// 触发使用道具
    /// </summary>
    public void Use()
    {
        if (CanUse)
        {
            OnUse();
        }
    }
}