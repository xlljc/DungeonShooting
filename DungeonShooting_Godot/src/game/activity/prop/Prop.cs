
/// <summary>
/// 道具基类
/// </summary>
public abstract partial class Prop : ActivityObject
{
    /// <summary>
    /// 道具的拥有者
    /// </summary>
    public Role Master { get; private set; }
    
    /// <summary>
    /// 当被动被道具被拾起时调用
    /// </summary>
    /// <param name="master">拾起该道具的角色</param>
    protected abstract void OnPickUp(Role master);

    /// <summary>
    /// 当被动道具被移除时调用
    /// </summary>
    /// <param name="master">移除该道具的角色</param>
    protected abstract void OnRemove(Role master);


    /// <summary>
    /// 如果道具放入了角色背包中, 则每帧调用
    /// </summary>
    public virtual void PackProcess(float delta)
    {
    }

    public override void Interactive(ActivityObject master)
    {
        if (master is Role role)
        {
            Pickup();
            Master = role;
            role.PushProp(this);
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