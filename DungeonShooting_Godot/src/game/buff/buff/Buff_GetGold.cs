
using Godot;

[BuffFragment("GetGold", "计算获取的金币buff, " +
                         "参数‘1’为金币数量添加类型, 1: 具体数量, 2:百分比(小数), " +
                         "参数‘2’为增加金币的数量值")]
public class Buff_GetGold : BuffFragment
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
            Role.RoleState.CalcGetGoldEvent += OnCalcGetGoldEvent1;
        }
        else
        {
            Role.RoleState.CalcGetGoldEvent += OnCalcGetGoldEvent2;
        }
    }

    public override void OnRemoveItem()
    {
        if (_type == 1)
        {
            Role.RoleState.CalcGetGoldEvent -= OnCalcGetGoldEvent1;
        }
        else
        {
            Role.RoleState.CalcGetGoldEvent -= OnCalcGetGoldEvent2;
        }
    }
    
    private void OnCalcGetGoldEvent1(int origin, RefValue<int> refValue)
    {
        refValue.Value += Mathf.CeilToInt(_value);
    }
    
    private void OnCalcGetGoldEvent2(int origin, RefValue<int> refValue)
    {
        refValue.Value += Mathf.CeilToInt(origin * _value);
    }
    
}