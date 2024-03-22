
using System.Text.Json;
using Godot;

[BuffFragment(
    "BulletCount", 
    "子弹数量 buff",
    Arg1 = "(int)子弹数量添加类型, 1: 具体数量, 2:百分比",
    Arg2 = "(float)增加子弹的数量"
)]
public class Buff_BulletCount : BuffFragment
{
    private int _type;
    private float _value;

    public override void InitParam(JsonElement[] arg)
    {
        _type = arg[0].GetInt32();
        _value = arg[1].GetSingle();
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