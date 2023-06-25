
using Godot;

/// <summary>
/// 移速 buff
/// </summary>
[GlobalClass, Tool]
public partial class MoveSpeedBuff : Buff
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.MoveSpeed += 100;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.MoveSpeed -= 100;
    }
}