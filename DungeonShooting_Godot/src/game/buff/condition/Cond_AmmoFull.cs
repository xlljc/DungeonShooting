
using System.Text.Json;

[ConditionFragment(
    "AmmoFull", 
    "判断当前武器弹药状态, ",
    Arg1 = "(boolean)是否判断满弹药"
)]
public class Cond_AmmoFull : ConditionFragment
{
    private bool _type;
    
    public override void InitParam(JsonElement[] arg)
    {
        _type = arg[0].GetBoolean();
    }

    public override bool OnCheckUse()
    {
        if (Role.WeaponPack.ActiveItem == null)
        {
            return false;
        }
        if (_type)
        {
            return !Role.WeaponPack.ActiveItem.IsAmmoFull();
        }
        else
        {
            return Role.WeaponPack.ActiveItem.IsAmmoFull();
        }
    }
}