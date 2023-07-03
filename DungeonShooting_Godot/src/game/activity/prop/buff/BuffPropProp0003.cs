
using Godot;

/// <summary>
/// 护盾上限buff, 护盾 + 1
/// </summary>
[Tool]
public partial class BuffPropProp0003 : BuffProp
{
    protected override void OnPickUp(Role master)
    {
        master.MaxShield += 1;
        master.Shield += 1;
    }

    protected override void OnRemove(Role master)
    {
        master.MaxShield -= 1;
    }
}