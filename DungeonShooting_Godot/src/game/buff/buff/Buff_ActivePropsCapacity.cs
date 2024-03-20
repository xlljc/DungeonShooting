
using System.Text.Json;

[BuffFragment("ActivePropsCapacity", "主动道具背包容量 buff, 参数‘1’为主动道具背包增加的容量")]
public class Buff_ActivePropsCapacity : BuffFragment
{
    private int _value;

    public override void InitParam(JsonElement[] arg)
    {
        _value = arg[0].GetInt32();
    }

    public override void OnPickUpItem()
    {
        Role.ActivePropsPack.SetCapacity(Role.ActivePropsPack.Capacity + _value);
    }

    public override void OnRemoveItem()
    {
        Role.ActivePropsPack.SetCapacity(Role.ActivePropsPack.Capacity - _value);
    }
}