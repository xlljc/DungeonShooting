
[Buff("WoundedInvincibleTime", "延长无敌时间buff, 参数‘1’为延长时间, 单位秒")]
public class Buff_WoundedInvincibleTime : BuffFragment
{
    private float _time;

    public override void InitParam(float arg1)
    {
        _time = arg1;
    }

    public override void OnPickUpItem()
    {
        Role.RoleState.WoundedInvincibleTime += _time;
    }

    public override void OnRemoveItem()
    {
        Role.RoleState.WoundedInvincibleTime -= _time;
    }
}