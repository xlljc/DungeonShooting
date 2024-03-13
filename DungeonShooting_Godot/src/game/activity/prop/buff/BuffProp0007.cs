
using Godot;

/// <summary>
/// 受伤时有15%概率抵消伤害
/// </summary>
[Tool]
public partial class BuffProp0007 : BuffActivity
{
    public override void OnPickUpItem()
    {
        Master.RoleState.CalcHurtDamageEvent += CalcHurtDamageEvent;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.CalcHurtDamageEvent -= CalcHurtDamageEvent;
    }

    private void CalcHurtDamageEvent(int originDamage, RefValue<int> refValue)
    {
        if (refValue.Value > 0 && Utils.Random.RandomBoolean(0.15f))
        {
            refValue.Value = 0;
        }
    }
}