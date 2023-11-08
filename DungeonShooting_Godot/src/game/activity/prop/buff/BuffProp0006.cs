
using Godot;

/// <summary>
/// 延长无敌时间buff, 受伤后无敌时间 + 2s
/// </summary>
[Tool]
public partial class BuffProp0006 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.RoleState.WoundedInvincibleTime += 2f;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.WoundedInvincibleTime -= 2f;
    }
}