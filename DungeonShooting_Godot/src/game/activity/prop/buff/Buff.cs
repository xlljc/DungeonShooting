
/// <summary>
/// 增益被动道具
/// </summary>
public abstract partial class Buff : Prop
{
    /// <summary>
    /// buff的拥有者
    /// </summary>
    public Role Master { get; private set; }
    
    /// <summary>
    /// 当被动被道具被拾起时调用
    /// </summary>
    /// <param name="master">拾起该buff的角色</param>
    protected abstract void OnPickUp(Role master);

    /// <summary>
    /// 当被动道具被移除时调用
    /// </summary>
    /// <param name="master">移除该buff的角色</param>
    protected abstract void OnRemove(Role master);

    public override void Interactive(ActivityObject master)
    {
        if (master is Role role)
        {
            Pickup();
            Master = role;
            role.PushBuff(this);
            OnPickUp(role);
        }
    }

    public override CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        if (master is Player)
        {
            return new CheckInteractiveResult(this, true, ResourcePath.resource_sprite_ui_icon_icon_pickup_png);
        }
        return base.CheckInteractive(master);
    }
}