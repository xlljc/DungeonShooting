
using Godot;

/// <summary>
/// 弹药箱, 使用后补全当前武器备用弹药
/// </summary>
[Tool]
public partial class ActiveProp5001 : ActiveProp
{
    public override void OnInit()
    {
        Superposition = true;
        AutoDestroy = true;
        MaxCount = 10;
    }

    public override bool OnCheckUse()
    {
        if (Master is AdvancedRole advancedRole)
        {
            return advancedRole.WeaponPack.ActiveItem != null && !advancedRole.WeaponPack.ActiveItem.IsAmmoFull();
        }

        return false;
    }

    protected override int OnUse()
    {
        if (Master is AdvancedRole advancedRole)
        {
            var weapon = advancedRole.WeaponPack.ActiveItem;
            if (weapon != null)
            {
                weapon.SetTotalAmmo(weapon.Attribute.MaxAmmoCapacity);
                return 1;
            }
        }

        return 0;
    }
}