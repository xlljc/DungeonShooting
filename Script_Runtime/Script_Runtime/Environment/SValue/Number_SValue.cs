
using System;

/// <summary>
/// 数字类型
/// </summary>
internal struct Number_SValue : ISValue
{
    private double _value;
    
    public Number_SValue(double value)
    {
        _value = value;
    }
    
    public SValueType GetValueType()
    {
        return SValueType.Number;
    }

    public SDataType GetDataType()
    {
        return SDataType.Number;
    }

    public object GetValue()
    {
        return _value;
    }
    
    public ISValue SinceAdd()
    {
        return new Number_SValue(_value + 1);
    }
    
    public ISValue SinceReduction()
    {
        return new Number_SValue(_value - 1);
    }
}