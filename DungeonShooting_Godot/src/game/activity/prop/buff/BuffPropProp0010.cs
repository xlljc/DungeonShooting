
using Godot;

/// <summary>
/// 分裂子弹	子弹数量翻倍, 但是精准度, 击退和伤害降低
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
        Master.RoleState.CalcBulletRepelEvent += CalcBulletRepelEvent;
    }

    public override void OnRemoveItem()
    {
        Master.RoleState.CalcBulletCountEvent -= CalcBulletCountEvent;
        Master.RoleState.CalcBulletDeviationAngleEvent -= CalcBulletDeviationAngleEvent;
        Master.RoleState.CalcDamageEvent -= CalcDamageEvent;
        Master.RoleState.CalcBulletSpeedEvent -= CalcBulletSpeedEvent;
        Master.RoleState.CalcBulletRepelEvent -= CalcBulletRepelEvent;
    }
    
    private void CalcBulletCountEvent(Weapon weapon, int originCount, RefValue<int> refValue)
    {
        refValue.Value += originCount * 10;
    }

    private void CalcBulletDeviationAngleEvent(Weapon weapon, float originAngle, RefValue<float> refValue)
    {
        refValue.Value += Utils.Random.RandomRangeFloat(-8, 8);
    }
    
    private void CalcDamageEvent(int originDamage, RefValue<int> refValue)
    {
        if (refValue.Value <= 0)
        {
            return;
        }

        refValue.Value = Mathf.Max(1, refValue.Value - Mathf.FloorToInt(refValue.Value * 0.35f));
    }
    
    private void CalcBulletSpeedEvent(Weapon weapon, float originSpeed, RefValue<float> speed)
    {
        speed.Value += originSpeed * Utils.Random.RandomRangeFloat(-0.05f, 0.05f);
    }
    
    private void CalcBulletRepelEvent(Weapon weapon, float originRepel, RefValue<float> repel)
    {
        if (weapon.Attribute.IsMelee || repel.Value < 0)
        {
            return;
        }
        
        repel.Value = Mathf.Max(1, repel.Value - Mathf.FloorToInt(repel.Value * 0.35f));
    }
}