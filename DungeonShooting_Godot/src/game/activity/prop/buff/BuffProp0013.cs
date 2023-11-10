using Godot;

/// <summary>
/// 武器背包 武器容量+1
/// </summary>
[Tool]
public partial class BuffProp0013 : BuffProp
{
    public override void OnPickUpItem()
    {
        if (Master is AdvancedRole advancedRole)
        {
            advancedRole.WeaponPack.SetCapacity(advancedRole.WeaponPack.Capacity + 1);
        }
    }

    public override void OnRemoveItem()
    {
        if (Master is AdvancedRole advancedRole)
        {
            advancedRole.WeaponPack.SetCapacity(advancedRole.WeaponPack.Capacity - 1);
        }
    }
}