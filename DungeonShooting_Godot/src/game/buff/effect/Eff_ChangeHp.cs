
using System.Text.Json;

[EffectFragment(
    "ChangeHp",
    "修改血量",
    Arg1 = "(int)血量变化的具体值"
)]
public class Eff_ChangeHp : EffectFragment
{
    private int _value;

    public override void InitParam(JsonElement[] arg)
    {
        _value = arg[0].GetInt32();
    }

    public override void OnUse()
    {
        Role.Hp += _value;
    }
}