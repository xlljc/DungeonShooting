
using System.Text.Json;

[BuffFragment(
    "WeaponCapacity",
    "武器背包容量 buff",
    Arg1 = "(int)武器背包增加的容量"
)]
public class Buff_WeaponCapacity : BuffFragment
{
    private int _value;

    public override void InitParam(JsonElement[] args)
    {
        _value = args[0].GetInt32();
    }

    public override void OnPickUpItem()
    {
        Role.WeaponPack.SetCapacity(Role.WeaponPack.Capacity + _value);
    }

    public override void OnRemoveItem()
    {
        Role.WeaponPack.SetCapacity(Role.WeaponPack.Capacity - _value);
    }
}