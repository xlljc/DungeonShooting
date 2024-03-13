
using Godot;

/// <summary>
/// 弹药箱, 使用后补全当前武器备用弹药
/// </summary>
[Tool]
public partial class ActiveProp5001 : ActivePropActivity
{
    public override void OnInit()
    {
        base.OnInit();
        Superposition = true;
        AutoDestroy = true;
        MaxCount = 10;
    }

    public override bool OnCheckUse()
    {
        return Master.WeaponPack.ActiveItem != null && !Master.WeaponPack.ActiveItem.IsAmmoFull();
    }

    protected override int OnUse()
    {
        var weapon = Master.WeaponPack.ActiveItem;
        if (weapon != null)
        {
            weapon.SetTotalAmmo(weapon.Attribute.MaxAmmoCapacity);
            return 1;
        }

        return 0;
    }
}