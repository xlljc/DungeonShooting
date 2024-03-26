
using System.Text.Json;

[BuffFragment(
    "BulletBounceCount", "子弹弹射数量 buff",
    Arg1 = "(int)子弹增加的弹射次数"
)]
public class Buff_BulletBounceCount : BuffFragment
{
    private int _value;

    public override void InitParam(JsonElement[] arg)
    {
        _value = arg[0].GetInt32();
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