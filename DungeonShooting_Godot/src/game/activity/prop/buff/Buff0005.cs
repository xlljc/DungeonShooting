
using Godot;

/// <summary>
/// 提升伤害buff, 子弹伤害提升20%
/// </summary>
[GlobalClass, Tool]
public partial class Buff0005 : Buff
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.CalcDamageEvent += CalcDamage;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.CalcDamageEvent -= CalcDamage;
    }

    private void CalcDamage(RefValue<int> refValue)
    {
        refValue.Value = Mathf.CeilToInt(refValue.Value * 1.2f);
    }
}