
[BuffFragment("BulletPenetration", "子弹穿透次数 buff, 参数‘1’为增加的穿透次数")]
public class Buff_BulletPenetration : BuffFragment
{
    private int _value;

    public override void InitParam(float arg1)
    {
        _value = (int)arg1;
    }

    public override void OnPickUpItem()
    {
        Role.RoleState.CalcBulletPenetrationEvent += CalcBulletPenetrationEvent;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.CalcBulletPenetrationEvent -= CalcBulletPenetrationEvent;
    }

    private void CalcBulletPenetrationEvent(int origin, RefValue<int> penetration)
    {
        penetration.Value += _value;
    }
}