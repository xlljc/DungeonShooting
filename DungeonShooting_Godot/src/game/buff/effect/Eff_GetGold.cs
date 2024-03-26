

using System.Text.Json;
using Godot;

[EffectFragment(
    "GetGold",
    "获得金币",
    Arg1 = "(int)金币的值"
)]
public class Eff_GetGold : EffectFragment
{
    private int _value;
    public override void InitParam(JsonElement[] args)
    {
        _value = args[0].GetInt32();
    }

    public override void OnUse()
    {
        Gold.CreateGold(Role.Position, _value);
    }
}