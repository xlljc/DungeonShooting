
using System.Text.Json;
using Godot;

[EffectFragment("PiggyBank", "存钱罐, 使用后返还存入的金币, 参数1为返还金币的倍率(小数)")]
public class Eff_PiggyBank : EffectFragment
{
    private float _value;
    //当前存入的金币数量
    private float _currValue;
    
    public override void InitParam(JsonElement[] arg)
    {
        _value = arg[0].GetSingle();
    }

    public override bool OnCheckUse()
    {
        return _currValue > 0;
    }

    public override void OnUse()
    {
        var goldList = Utils.GetGoldList(Mathf.FloorToInt(_currValue * _value));
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

        _currValue = 0;
    }

    public override void OnPickUpItem()
    {
        Role.RoleState.CalcGetGoldEvent += OnCalcGetGoldEvent;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.CalcGetGoldEvent -= OnCalcGetGoldEvent;
    }
    
    private void OnCalcGetGoldEvent(int origin, RefValue<int> refValue)
    {
        _currValue += refValue.Value;
        refValue.Value = 0;
    }
}