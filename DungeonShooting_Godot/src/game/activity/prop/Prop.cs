
/// <summary>
/// 道具基类
/// </summary>
public abstract partial class Prop : ActivityObject, IPackageItem
{
    public Role Master { get; set; }

    public int PackageIndex { get; set; } = -1;

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

        
    public virtual void OnRemoveItem()
    {
        
    }

    public virtual void OnPickUpItem()
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
    
    /// <summary>
    /// 执行将当前道具放入角色背包的操作
    /// </summary>
    protected void PushToRole(Role role)
    {
        Pickup();
        role.PushProp(this);
        OnPickUp(role);
    }
    
    public override void Interactive(ActivityObject master)
    {
        if (master is Role role)
        {
            PushToRole(role);
        }
    }

    public override CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        if (master is Player)
        {
            return new CheckInteractiveResult(this, true, CheckInteractiveResult.InteractiveType.PickUp);
        }
        return base.CheckInteractive(master);
    }

}