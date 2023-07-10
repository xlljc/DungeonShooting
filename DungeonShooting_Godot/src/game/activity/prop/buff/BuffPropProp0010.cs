
using Godot;

/// <summary>
/// 分裂子弹	子弹数量翻倍, 但是精准度和伤害降低
/// </summary>
[Tool]
public partial class BuffPropProp0010 : BuffProp
{
    public override void OnPickUpItem()
    {
        Master.RoleState.CalcBulletCountEvent += CalcBulletCountEvent;
        Master.RoleState.CalcBulletDeviationAngleEvent += CalcBulletDeviationAngleEvent;
        Master.RoleState.CalcDamageEvent += CalcDamageEvent;
        Master.RoleState.CalcBulletSpeedEvent += CalcBulletSpeedEvent;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.CalcBulletCountEvent -= CalcBulletCountEvent;
        Master.RoleState.CalcBulletDeviationAngleEvent -= CalcBulletDeviationAngleEvent;
        Master.RoleState.CalcDamageEvent -= CalcDamageEvent;
        Master.RoleState.CalcBulletSpeedEvent -= CalcBulletSpeedEvent;
    }
    
    private void CalcBulletCountEvent(Weapon weapon, int originCount, RefValue<int> refValue)
    {
        refValue.Value += originCount;
    }

    private void CalcBulletDeviationAngleEvent(Weapon weapon, float originAngle, RefValue<float> refValue)
    {
        refValue.Value += Utils.RandomRangeFloat(-8, 8);
    }
    
    private void CalcDamageEvent(int originDamage, RefValue<int> refValue)
    {
        if (refValue.Value <= 0)
        {
            return;
        }

        refValue.Value = Mathf.Max(1, refValue.Value - Mathf.FloorToInt(originDamage * 0.4f));
    }
    
    private void CalcBulletSpeedEvent(Weapon weapon, float originSpeed, RefValue<float> speed)
    {
        speed.Value += originSpeed * Utils.RandomRangeFloat(-0.05f, 0.05f);
    }
}