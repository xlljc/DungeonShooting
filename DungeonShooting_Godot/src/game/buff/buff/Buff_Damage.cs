
using Godot;

[BuffFragment("Damage", 
    "提升伤害buff, " +
    "参数‘1’为伤害增加类型: 1:具体伤害, 2:百分比伤害(小数), " +
    "参数‘2’为增益伤害值")]
public class Buff_Damage : BuffFragment
{
    private int _type;
    private float _value;
    
    public override void InitParam(float arg1, float arg2)
    {
        _type = (int)arg1;
        _value = arg2;
    }

    public override void OnPickUpItem()
    {
        if (_type == 1)
        {
            Role.RoleState.CalcDamageEvent += CalcDamage1;
        }
        else
        {
            Role.RoleState.CalcDamageEvent += CalcDamage2;
        }
    }

    public override void OnRemoveItem()
    {
        if (_type == 1)
        {
            Role.RoleState.CalcDamageEvent -= CalcDamage1;
        }
        else
        {
            Role.RoleState.CalcDamageEvent -= CalcDamage2;
        }
    }

    private void CalcDamage1(int originDamage, RefValue<int> refValue)
    {
        refValue.Value += Mathf.CeilToInt(_value);
    }
    
    private void CalcDamage2(int originDamage, RefValue<int> refValue)
    {
        if (_value > 0)
        {
            refValue.Value += Mathf.CeilToInt(originDamage * _value);
        }
        else
        {
            refValue.Value = Mathf.Max(1, refValue.Value + Mathf.FloorToInt(refValue.Value * _value));
        }
    }
}