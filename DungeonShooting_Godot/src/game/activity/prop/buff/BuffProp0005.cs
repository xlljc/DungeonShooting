
using Godot;

/// <summary>
/// 提升伤害buff, 子弹伤害提升20%
/// </summary>
[Tool]
public partial class BuffProp0005 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.RoleState.CalcDamageEvent += CalcDamage;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.CalcDamageEvent -= CalcDamage;
    }

    private void CalcDamage(int originDamage, RefValue<int> refValue)
    {
        refValue.Value += Mathf.CeilToInt(originDamage * 0.2f);
    }
}