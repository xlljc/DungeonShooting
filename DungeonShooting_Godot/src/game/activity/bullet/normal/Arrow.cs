
using Godot;

[Tool]
public partial class Arrow : Bullet
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
        MoveController.Enable = false;
        CollisionArea.Monitoring = false;
        CollisionArea.Monitorable = false;
        Collision.Disabled = true;
    }

    private void OnBindTarget(ActivityObject activityObject)
    {
        Altitude = -activityObject.ToLocal(GlobalPosition).Y;
        Reparent(activityObject);
        activityObject.BindDestroyObject(this);
        AnimatedSprite.Play(AnimatorNames.HalfEnd);
        HalfSprite.Visible = true;
    }
}