
using System.Text.Json;

[ConditionFragment("AmmoFull", 
    "判断当前武器弹药状态, " +
    "参数1可选值: 0:判断非满弹药, 1:判断满弹药")]
public class Cond_AmmoFull : ConditionFragment
{
    private int _type;
    
    public override void InitParam(JsonElement[] arg)
    {
        _type = arg[0].GetInt32();
    }

    public override bool OnCheckUse()
    {
        if (Role.WeaponPack.ActiveItem == null)
        {
            return false;
        }
        if (_type == 0)
        {
            return !Role.WeaponPack.ActiveItem.IsAmmoFull();
        }
        else
        {
            return Role.WeaponPack.ActiveItem.IsAmmoFull();
        }
    }
}