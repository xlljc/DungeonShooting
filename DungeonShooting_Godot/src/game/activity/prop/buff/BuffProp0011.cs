
using Godot;

/// <summary>
/// 弹射子弹 子弹反弹次数 +2
/// </summary>
[Tool]
public partial class BuffProp0011 : BuffActivity
{
    public override void OnPickUpItem()
    {
        Master.RoleState.CalcBulletBounceCountEvent += CalcBulletBounceCountEvent;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.CalcBulletBounceCountEvent -= CalcBulletBounceCountEvent;
    }
    
    private void CalcBulletBounceCountEvent(int originBounce, RefValue<int> bounce)
    {
        bounce.Value += 2;
    }
}