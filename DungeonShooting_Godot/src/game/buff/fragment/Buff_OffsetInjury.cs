
/// <summary>
/// 受伤时有概率抵消伤害的buff
/// </summary>
[Buff("OffsetInjury", "受伤时有概率抵消伤害的buff, 参数‘1’为抵消伤害概率百分比(小数)")]
public class Buff_OffsetInjury : BuffFragment
{
    private float _value;

    public override void InitParam(float arg1)
    {
        _value = arg1;
    }

    public override void OnPickUpItem()
    {
        Role.RoleState.CalcHurtDamageEvent += CalcHurtDamageEvent;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.CalcHurtDamageEvent -= CalcHurtDamageEvent;
    }

    private void CalcHurtDamageEvent(int originDamage, RefValue<int> refValue)
    {
        if (refValue.Value > 0 && Utils.Random.RandomBoolean(_value))
        {
            refValue.Value = 0;
        }
    }
}