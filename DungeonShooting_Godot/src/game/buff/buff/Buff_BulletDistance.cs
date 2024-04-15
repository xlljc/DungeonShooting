
using System.Text.Json;

[BuffFragment(
    "BulletDistance", 
    "子弹射程 buff, ",
    Arg1 = "(int)子弹射程增加类型: 1:具体射程, 2:百分比射程",
    Arg2 = "(float)子弹增加的射程值"
)]
public class Buff_BulletDistance : BuffFragment
{
    private int _type;
    private float _value;

    public override void InitParam(JsonElement[] args)
    {
        _type = args[0].GetInt32();
        _value = args[1].GetSingle();
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