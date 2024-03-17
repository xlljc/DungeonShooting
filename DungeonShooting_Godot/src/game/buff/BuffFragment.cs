
using System;

/// <summary>
/// 被动属性逻辑基类
/// </summary>
public abstract class BuffFragment : PropFragment
{
    /// <summary>
    /// 当道具被拾起时调用 (在 Master 赋值之后调用)
    /// </summary>
    public abstract void OnPickUpItem();

    /// <summary>
    /// 当道具被移除时调用 (在 Master 置为 null 之前调用)
    /// </summary>
    public abstract void OnRemoveItem();
}