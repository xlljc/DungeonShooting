
using Godot;

[Tool]
public partial class Arrow : Bullet, IMountItem
{
    [Export, ExportFillNode]
    public AnimatedSprite2D HalfSprite { get; set; }
    
    public override void OnPlayDisappearEffect()
    {
    }

    public override void OnPlayCollisionEffect(KinematicCollision2D collision)
    {
    }

    public override void OnCollisionTarget(IHurt hurt)
    {
        base.OnCollisionTarget(hurt);
        var activityObject = hurt.GetActivityObject();
        if (activityObject != null)
        {
            CallDeferred(nameof(OnBindTarget), activityObject);
        }
    }

    public override void LogicalFinish()
    {
        SetEnableMovement(false);
    }

    private void OnBindTarget(ActivityObject activityObject)
    {
        Altitude = -activityObject.ToLocal(GlobalPosition).Y;
        activityObject.BindMountObject(this);
        AnimatedSprite.Play(AnimatorNames.HalfEnd);
        HalfSprite.Visible = true;
    }

    public void OnMount(ActivityObject target)
    {
        Reparent(target);
    }

    public void OnUnmount(ActivityObject target)
    {
        SetEnableMovement(true);
        MoveController.ClearForce();
        MoveController.BasisVelocity = Vector2.Zero;
        Throw(10, 60, new Vector2(20, 0), 0);
    }
}