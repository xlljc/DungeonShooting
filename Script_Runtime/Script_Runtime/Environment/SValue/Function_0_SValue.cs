
using System;

/// <summary>
/// 0参数回调函数
/// </summary>
internal class Function_0_SValue : SValue
{
    private Function_0 _value;
    
    public Function_0_SValue(Function_0 value)
    {
        _value = value;
    }
    
    public override SValueType GetValueType()
    {
        return SValueType.Function;
    }

    public override SDataType GetDataType()
    {
        return SDataType.Function_0;
    }

    public override object GetValue()
    {
        return _value;
    }
    
    public override SValue Invoke()
    {
        return _value();
    }
}