
using Godot;

/// <summary>
/// 护盾上限buff, 护盾 + 1
/// </summary>
[GlobalClass, Tool]
public partial class Buff0003 : Buff
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