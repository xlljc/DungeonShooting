
/// <summary>
/// 主动道具使用效果基类
/// </summary>
public abstract class EffectFragment : PropFragment
{
    /// <summary>
    /// 当前组件所挂载的游戏对象
    /// </summary>
    public new ActiveProp Master => (ActiveProp)base.Master;

    /// <summary>
    /// 当检测是否可以使用时调用
    /// </summary>
    public virtual bool OnCheckUse()
    {
        return true;
    }
    
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

    /// <summary>
    /// 返回是否正在使用当前道具
    /// </summary>
    public bool IsActive()
    {
        return Role != null && Role.ActivePropsPack.ActiveItem == Master;
    }
}