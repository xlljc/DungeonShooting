

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
        var goldList = Utils.GetGoldList(Mathf.FloorToInt(_value));
        foreach (var id in goldList)
        {
            var o = ObjectManager.GetActivityObject<Gold>(id);
            o.Position = Role.Position;
            o.Throw(0,
                Utils.Random.RandomRangeInt(50, 110),
                new Vector2(Utils.Random.RandomRangeInt(-20, 20), Utils.Random.RandomRangeInt(-20, 20)),
                0
            );
        }
    }
}