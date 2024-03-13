
/// <summary>
/// 子弹射程 buff
/// </summary>
[Buff("BulletDistance", "子弹射程 buff, 参数‘1’为射程增加类型: 1:具体射程, 2:百分比射程(小数), 参数‘2’为子弹增加的射程值")]
public class Buff_BulletDistance : BuffFragment
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
            Role.RoleState.CalcBulletDistanceEvent += CalcBulletDistanceEvent1;
        }
        else
        {
            Role.RoleState.CalcBulletDistanceEvent += CalcBulletDistanceEvent2;
        }
    }
    
    public override void OnRemoveItem()
    {
        if (_type == 1)
        {
            Role.RoleState.CalcBulletDistanceEvent -= CalcBulletDistanceEvent1;
        }
        else
        {
            Role.RoleState.CalcBulletDistanceEvent -= CalcBulletDistanceEvent2;
        }
    }

    private void CalcBulletDistanceEvent1(float originDistance, RefValue<float> distance)
    {
        distance.Value += _value;
    }
    
    private void CalcBulletDistanceEvent2(float originDistance, RefValue<float> distance)
    {
        distance.Value += originDistance * _value;
    }
}