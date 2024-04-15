
using System.Text.Json;

[BuffFragment(
    "WoundedInvincibleTime",
    "延长无敌时间 buff, 单位秒",
    Arg1 = "(float)延长时间"
)]
public class Buff_WoundedInvincibleTime : BuffFragment
{
    private float _time;

    public override void InitParam(JsonElement[] args)
    {
        _time = args[0].GetSingle();
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