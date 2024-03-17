
using Godot;

[BuffFragment("BulletCount", 
    "子弹数量 buff, " +
    "参数‘1’为子弹数量添加类型, 1: 具体数量, 2:百分比(小数), " +
    "参数‘2’为增加子弹的数量")]
public class Buff_BulletCount : BuffFragment
{
    private int _type;
    private float _value;

    public override void InitParam(float arg1, float arg2)
    {
        _type = (int)arg1;
        _value = (int)arg2;
    }
    
    public override void OnPickUpItem()
    {
        if (_type == 1)
        {
            Role.RoleState.CalcBulletCountEvent += CalcBulletCountEvent1;
        }
        else
        {
            Role.RoleState.CalcBulletCountEvent += CalcBulletCountEvent2;
        }
    }

    public override void OnRemoveItem()
    {
        if (_type == 1)
        {
            Role.RoleState.CalcBulletCountEvent -= CalcBulletCountEvent1;
        }
        else
        {
            Role.RoleState.CalcBulletCountEvent -= CalcBulletCountEvent2;
        }
    }

    private void CalcBulletCountEvent1(int originCount, RefValue<int> refValue)
    {
        refValue.Value += Mathf.CeilToInt(_value);
    }
    
    private void CalcBulletCountEvent2(int originCount, RefValue<int> refValue)
    {
        refValue.Value += Mathf.CeilToInt(originCount * _value);
    }
}