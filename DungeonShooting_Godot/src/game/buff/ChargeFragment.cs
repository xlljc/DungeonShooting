
/// <summary>
/// 主动道具充能基类
/// </summary>
public abstract class ChargeFragment : PropFragment
{
    /// <summary>
    /// 当前组件所挂载的游戏对象
    /// </summary>
    public new ActiveProp Master => (ActiveProp)base.Master;
    
    /// <summary>
    /// 当道具被使用是调用
    /// </summary>
    public abstract void OnUse();
    
    /// <summary>
    /// 道具持续时间完成时调用
    /// </summary>
    public virtual void OnUsingFinish()
    {
    }
    
    public override void OnPickUpItem()
    {
    }

    public override void OnRemoveItem()
    {
    }
}