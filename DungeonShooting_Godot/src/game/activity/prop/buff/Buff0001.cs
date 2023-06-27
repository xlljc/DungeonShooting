
using Godot;

/// <summary>
/// 移速 buff, 移速 + 3
/// </summary>
[GlobalClass, Tool]
public partial class Buff0001 : Buff
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.MoveSpeed += 30;
        master.RoleState.Acceleration += 400;
        master.RoleState.Friction += 300;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.MoveSpeed -= 30;
        master.RoleState.Acceleration -= 400;
        master.RoleState.Friction -= 300;
    }
}