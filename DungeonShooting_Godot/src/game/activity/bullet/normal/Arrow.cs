
using Godot;

[Tool]
public partial class Arrow : Bullet, IMountItem
{
    [Export, ExportFillNode]
    public AnimatedSprite2D HalfSprite { get; set; }

    public override void InitData(BulletData data, uint attackLayer)
    {
        base.InitData(data, attackLayer);
        EnableVerticalMotion = false;
        DefaultLayer = RoomLayerEnum.NormalLayer;
    }

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
        var slideCollision = GetLastSlideCollision();
        if (slideCollision != null)
        {
            Position -= slideCollision.GetTravel();
        }
    }

    private void OnBindTarget(ActivityObject activityObject)
    {
        Altitude = -activityObject.ToLocal(GlobalPosition).Y;
        activityObject.AddMountObject(this);
    }

    public void OnMount(ActivityObject target)
    {
        Reparent(target);
        AnimatedSprite.Play(AnimatorNames.HalfEnd);
        HalfSprite.Visible = true;
    }

    public void OnUnmount(ActivityObject target)
    {
        AnimatedSprite.Play(AnimatorNames.Default);
        HalfSprite.Visible = false;
        SetEnableMovement(true);
        EnableVerticalMotion = true;
        MoveController.ClearForce();
        MoveController.BasisVelocity = Vector2.Zero;
        ShadowOffset = new Vector2(0, 1);
        Throw(10, 60, new Vector2(20, 0), 0);
    }
}