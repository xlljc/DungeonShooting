
using Godot;

/// <summary>
/// 延长无敌时间buff, 受伤后无敌时间 + 2s
/// </summary>
[GlobalClass, Tool]
public partial class Buff0006 : Buff
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