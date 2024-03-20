

using System.Text.Json;

[EffectFragment("UseGold", "使用金币, 参数1为金币的值")]
public class Eff_UseGold : EffectFragment
{
    private int _value;
    public override void InitParam(JsonElement[] args)
    {
        _value = args[0].GetInt32();
    }

    public override void OnUse()
    {
        Role.UseGold(_value);
    }
}