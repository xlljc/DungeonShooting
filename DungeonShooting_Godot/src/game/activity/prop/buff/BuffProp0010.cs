
using Godot;

/// <summary>
/// 分裂子弹	子弹数量翻倍, 但是精准度, 击退和伤害降低
/// </summary>
[Tool]
public partial class BuffProp0010 : BuffProp
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
    
    private void CalcBulletCountEvent(int originCount, RefValue<int> refValue)
    {
        refValue.Value += originCount;
    }

    private void CalcBulletDeviationAngleEvent(float originAngle, RefValue<float> refValue)
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
    
    private void CalcBulletSpeedEvent(float originSpeed, RefValue<float> speed)
    {
        speed.Value += originSpeed * Utils.Random.RandomRangeFloat(-0.05f, 0.05f);
    }
    
    private void CalcBulletRepelEvent(float originRepel, RefValue<float> repel)
    {
        if (repel.Value < 0 || (Master.WeaponPack.ActiveItem != null &&
                                Master.WeaponPack.ActiveItem.Attribute.IsMelee))
        {
            return;
        }
        
        repel.Value = Mathf.Max(1, repel.Value - Mathf.FloorToInt(repel.Value * 0.35f));
    }
}