using Godot;

public partial class HurtArea : Area2D, IHurt
{
    public delegate void HurtDelegate(ActivityObject target, int damage, float angle);

    public event HurtDelegate OnHurtEvent;
    
    public ActivityObject ActivityObject { get; private set; }

    public void InitActivityObject(ActivityObject activityObject)
    {
        ActivityObject = activityObject;
    }

    public override void _Ready()
    {
        Monitoring = false;
    }

    public void Hurt(ActivityObject target, int damage, float angle)
    {
        if (OnHurtEvent != null)
        {
            OnHurtEvent(target, damage, angle);
        }
    }
}