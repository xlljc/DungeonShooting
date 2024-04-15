
using System.Text.Json;
using Godot;

[BuffFragment(
    "BulletDamage", 
    "提升子弹伤害buff, ",
    Arg1 = "(int)伤害增加类型: 1:具体伤害, 2:百分比伤害",
    Arg2 = "(float)增益伤害值"
)]
public class Buff_BulletDamage : BuffFragment
{
    private int _type;
    private float _value;
    
    public override void InitParam(JsonElement[] arg)
    {
        _type = arg[0].GetInt32();
        _value = arg[1].GetSingle();
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
        if (Role.WeaponPack.ActiveItem != null && Role.WeaponPack.ActiveItem.Attribute.IsMelee)
        {
            return;
        }
        refValue.Value += Mathf.CeilToInt(_value);
    }
    
    private void CalcDamage2(int originDamage, RefValue<int> refValue)
    {
        if (Role.WeaponPack.ActiveItem != null && Role.WeaponPack.ActiveItem.Attribute.IsMelee)
        {
            return;
        }
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