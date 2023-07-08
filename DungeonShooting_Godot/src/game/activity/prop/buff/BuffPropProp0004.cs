
using Godot;

/// <summary>
/// 护盾恢复时间buff, 恢复时间 - 1.5s
/// </summary>
[Tool]
public partial class BuffPropProp0004 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.RoleState.ShieldRecoveryTime -= 1.5f;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.ShieldRecoveryTime += 1.5f;
    }
}