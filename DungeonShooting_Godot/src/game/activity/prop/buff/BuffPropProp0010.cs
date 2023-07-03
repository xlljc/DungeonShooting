
using Godot;

/// <summary>
/// 分裂子弹	子弹数量翻倍, 但是精准度和伤害降低
/// </summary>
[Tool]
public partial class BuffPropProp0010 : BuffProp
{
    protected override void OnPickUp(Role master)
    {
        master.RoleState.CalcBulletCountEvent += CalcBulletCountEvent;
        master.RoleState.CalcBulletDeviationAngleEvent += CalcBulletDeviationAngleEvent;
        master.RoleState.CalcDamageEvent += CalcDamageEvent;
    }

    protected override void OnRemove(Role master)
    {
        master.RoleState.CalcBulletCountEvent -= CalcBulletCountEvent;
        master.RoleState.CalcBulletDeviationAngleEvent -= CalcBulletDeviationAngleEvent;
        master.RoleState.CalcDamageEvent -= CalcDamageEvent;
    }
    
    private void CalcBulletCountEvent(Weapon weapon, int originCount, RefValue<int> refValue)
    {
        refValue.Value += originCount;
    }

    private void CalcBulletDeviationAngleEvent(Weapon weapon, float originAngle, RefValue<float> refValue)
    {
        refValue.Value += Utils.RandomRangeFloat(-10, 10);
    }
    
    private void CalcDamageEvent(int originDamage, RefValue<int> refValue)
    {
        if (refValue.Value <= 0)
        {
            return;
        }

        refValue.Value = Mathf.Max(1, refValue.Value - Mathf.FloorToInt(originDamage * 0.4f));
    }
}