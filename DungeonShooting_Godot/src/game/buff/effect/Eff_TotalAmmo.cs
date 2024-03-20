
using System.Text.Json;

[EffectFragment("TotalAmmo", 
    "修改武器总弹药量, " +
    "参数1(选填)为弹药变化的具体值, 如果不传则表示补满弹药")]
public class Eff_TotalAmmo : EffectFragment
{
    private bool _initParam = false;
    private int _value;

    public override void InitParam(JsonElement[] args)
    {
        _initParam = true;
        _value = args[0].GetInt32();
    }

    public override void OnUse()
    {
        var weapon = Role.WeaponPack.ActiveItem;
        if (_initParam)
        {
            weapon.SetTotalAmmo(weapon.TotalAmmon + _value);
        }
        else
        {
            weapon.SetTotalAmmo(weapon.Attribute.MaxAmmoCapacity);
        }
    }
}