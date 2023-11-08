
using Godot;

/// <summary>
/// 护盾恢复时间buff, 恢复时间 - 2.5s
/// </summary>
[Tool]
public partial class BuffProp0004 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.RoleState.ShieldRecoveryTime -= 2.5f;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.ShieldRecoveryTime += 2.5f;
    }
}