
using Godot;

/// <summary>
/// 延长无敌时间buff, 受伤后无敌时间 + 2s
/// </summary>
[Tool]
public partial class BuffPropProp0006 : BuffProp
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.WoundedInvincibleTime += 2f;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.WoundedInvincibleTime -= 2f;
    }
}