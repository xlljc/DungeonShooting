
using Godot;

/// <summary>
/// 护盾恢复时间buff, 恢复时间 - 1.5s
/// </summary>
[GlobalClass, Tool]
public partial class ShieldRecoveryTimeBuff : Buff
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.ShieldRecoveryTime -= 1.5f;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.ShieldRecoveryTime += 1.5f;
    }
}