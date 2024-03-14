
[Buff("BulletSpeed", "子弹速度 buff, 参数‘1’为射速增加类型: 1:具体射速, 2:百分比射速(小数), 参数‘2’为子弹增加的射速值")]
public class Buff_BulletSpeed : BuffFragment
{
    private int _type;
    private float _value;

    public override void InitParam(float arg1, float arg2)
    {
        _type = (int)arg1;
        _value = arg2;
    }
    
    public override void OnPickUpItem()
    {
        if (_type == 1)
        {
            Role.RoleState.CalcBulletSpeedEvent += CalcBulletSpeedEvent1;
        }
        else
        {
            Role.RoleState.CalcBulletSpeedEvent += CalcBulletSpeedEvent2;
        }
    }
    
    public override void OnRemoveItem()
    {
        if (_type == 1)
        {
            Role.RoleState.CalcBulletSpeedEvent -= CalcBulletSpeedEvent1;
        }
        else
        {
            Role.RoleState.CalcBulletSpeedEvent -= CalcBulletSpeedEvent2;
        }
    }
    
    private void CalcBulletSpeedEvent1(float originSpeed, RefValue<float> speed)
    {
        speed.Value += _value;
    }
    
    private void CalcBulletSpeedEvent2(float originSpeed, RefValue<float> speed)
    {
        speed.Value += originSpeed * _value;
    }
}