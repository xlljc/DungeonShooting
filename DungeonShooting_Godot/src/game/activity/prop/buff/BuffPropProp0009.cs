
using Godot;

/// <summary>
/// 高速子弹	子弹速度和射程提升25%
/// </summary>
[Tool]
public partial class BuffPropProp0009 : BuffProp
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.CalcBulletSpeedEvent += CalcBulletSpeedEvent;
        master.RoleState.CalcBulletDistanceEvent += CalcBulletDistanceEvent;
    }
    
    protected override void OnRemove(Role master)
    {
        master.RoleState.CalcBulletSpeedEvent -= CalcBulletSpeedEvent;
        master.RoleState.CalcBulletDistanceEvent -= CalcBulletDistanceEvent;
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