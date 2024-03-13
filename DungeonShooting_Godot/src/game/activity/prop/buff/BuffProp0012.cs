
using Godot;

/// <summary>
/// 穿透子弹 子弹穿透+1
/// </summary>
[Tool]
public partial class BuffProp0012 : BuffActivity
{
    public override void OnPickUpItem()
    {
        Master.RoleState.CalcBulletPenetrationEvent += CalcBulletPenetrationEvent;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.CalcBulletPenetrationEvent -= CalcBulletPenetrationEvent;
    }
    
    private void CalcBulletPenetrationEvent(int origin, RefValue<int> penetration)
    {
        penetration.Value += 1;
    }
}