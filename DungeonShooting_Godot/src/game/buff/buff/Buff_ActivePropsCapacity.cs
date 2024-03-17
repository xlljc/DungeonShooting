
[BuffFragment("ActivePropsCapacity", "主动道具背包容量 buff, 参数‘1’为主动道具背包增加的容量")]
public class Buff_ActivePropsCapacity : BuffFragment
{
    private int _value;

    public override void InitParam(float arg1)
    {
        _value = (int)arg1;
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