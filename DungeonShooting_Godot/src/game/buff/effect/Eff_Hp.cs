
using System.Text.Json;

[EffectFragment("Hp", "修改血量, 参数1为血量变化的具体值")]
public class Eff_Hp : EffectFragment
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