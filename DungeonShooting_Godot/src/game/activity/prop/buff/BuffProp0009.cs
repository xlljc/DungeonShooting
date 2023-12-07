
using Godot;

/// <summary>
/// 高速子弹	子弹速度和射程提升25%
/// </summary>
[Tool]
public partial class BuffProp0009 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.RoleState.CalcBulletSpeedEvent += CalcBulletSpeedEvent;
        Master.RoleState.CalcBulletDistanceEvent += CalcBulletDistanceEvent;
    }
    
    public override void OnRemoveItem()
    {
        Master.RoleState.CalcBulletSpeedEvent -= CalcBulletSpeedEvent;
        Master.RoleState.CalcBulletDistanceEvent -= CalcBulletDistanceEvent;
    }
    
    private void CalcBulletSpeedEvent(float originSpeed, RefValue<float> speed)
    {
        speed.Value += originSpeed * 0.25f;
    }
    
    private void CalcBulletDistanceEvent(float originDistance, RefValue<float> distance)
    {
        distance.Value += originDistance * 0.25f;
    }
}