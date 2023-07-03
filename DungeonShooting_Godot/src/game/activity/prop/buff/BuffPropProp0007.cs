
using Godot;

/// <summary>
/// 受伤时有15%概率抵消伤害
/// </summary>
[Tool]
public partial class BuffPropProp0007 : BuffProp
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.CalcHurtDamageEvent += CalcHurtDamageEvent;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.CalcHurtDamageEvent -= CalcHurtDamageEvent;
    }

    private void CalcHurtDamageEvent(int originDamage, RefValue<int> refValue)
    {
        if (refValue.Value > 0 && Utils.RandomBoolean(0.15f))
        {
            refValue.Value = 0;
        }
    }
}