
/// <summary>
/// 主动道具使用效果基类
/// </summary>
public abstract class EffectFragment : PropFragment
{
    /// <summary>
    /// 使用道具的回调
    /// </summary>
    public abstract void OnUse();
    
    public override void OnPickUpItem()
    {
    }

    public override void OnRemoveItem()
    {
    }
}