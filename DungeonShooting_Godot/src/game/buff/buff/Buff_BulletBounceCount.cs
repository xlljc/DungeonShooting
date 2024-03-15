
[Buff("BulletBounceCount", "子弹弹射数量 buff, 参数‘1’为增加的弹射次数")]
public class Buff_BulletBounceCount : BuffFragment
{
    private int _value;

    public override void InitParam(float arg1)
    {
        _value = (int)arg1;
    }

    public override void OnPickUpItem()
    {
        Role.RoleState.CalcBulletBounceCountEvent += CalcBulletBounceCountEvent;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.CalcBulletBounceCountEvent -= CalcBulletBounceCountEvent;
    }

    private void CalcBulletBounceCountEvent(int originBounce, RefValue<int> bounce)
    {
        bounce.Value += _value;
    }
}