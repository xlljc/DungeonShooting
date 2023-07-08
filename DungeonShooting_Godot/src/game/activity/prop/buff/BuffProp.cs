
/// <summary>
/// 被动增益道具
/// </summary>
public abstract partial class BuffProp : Prop
{
    public override void Interactive(ActivityObject master)
    {
        if (master is Player role)
        {
            Pickup();
            role.PickUpBuffProp(this);
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