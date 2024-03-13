
using Godot;

/// <summary>
/// 护盾上限buff, 护盾 + 1
/// </summary>
[Tool]
public partial class BuffProp0003 : BuffActivity
{
    public override void OnPickUpItem()
    {
        Master.MaxShield += 1;
        Master.Shield += 1;
    }

    public override void OnRemoveItem()
    {
        Master.MaxShield -= 1;
    }
}