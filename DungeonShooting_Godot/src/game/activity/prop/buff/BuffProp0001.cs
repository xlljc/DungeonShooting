
using Godot;

/// <summary>
/// 移速 buff, 移速 + 3
/// </summary>
[Tool]
public partial class BuffProp0001 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.RoleState.MoveSpeed += 30;
        Master.RoleState.Acceleration += 400;
        Master.RoleState.Friction += 300;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.MoveSpeed -= 30;
        Master.RoleState.Acceleration -= 400;
        Master.RoleState.Friction -= 300;
    }
}