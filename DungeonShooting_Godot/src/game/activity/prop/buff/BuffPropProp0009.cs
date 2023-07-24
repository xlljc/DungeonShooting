
using Godot;

/// <summary>
/// 高速子弹	子弹速度和射程提升25%
/// </summary>
[Tool]
public partial class BuffPropProp0009 : BuffProp
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
    
    private void CalcBulletSpeedEvent(Weapon weapon, float originSpeed, RefValue<float> speed)
    {
        speed.Value += originSpeed * 0.25f;
    }
    
    private void CalcBulletDistanceEvent(Weapon weapon, float originDistance, RefValue<float> distance)
    {
        distance.Value += originDistance * 0.25f;
    }
}