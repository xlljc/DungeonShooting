
using System;

/// <summary>
/// 数字类型
/// </summary>
internal class Number_SValue : ISValue
{
    internal double _value;
    
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
    
    public ISValue Operator_SinceAdd()
    {
        return new Number_SValue(_value + 1);
    }
    
    public ISValue Operator_SinceReduction()
    {
        return new Number_SValue(_value - 1);
    }
    
    public ISValue Operator_Greater_Double(double v2)
    {
        return _value > v2 ? ISValue.True : ISValue.False;
    }

    public ISValue Operator_Less_Double(double v2)
    {
        return _value < v2 ? ISValue.True : ISValue.False;
    }

    public ISValue Operator_Greater_ISValue(ISValue v2)
    {
        switch (v2.GetDataType())
        {
            case SDataType.Number:
                return _value > ((Number_SValue)v2)._value ? ISValue.True : ISValue.False;
        }
        return ISValue.False;
    }
    
    public ISValue Operator_Less_ISValue(ISValue v2)
    {
        switch (v2.GetDataType())
        {
            case SDataType.Number:
                return _value < ((Number_SValue)v2)._value ? ISValue.True : ISValue.False;
        }
        return ISValue.False;
    }

    public ISValue Operator_Greater_Equal_Double(double v2)
    {
        return _value >= v2 ? ISValue.True : ISValue.False;
    }

    public ISValue Operator_Less_Equal_Double(double v2)
    {
        return _value <= v2 ? ISValue.True : ISValue.False;
    }
    
    public ISValue Operator_Greater_Equal_ISValue(ISValue v2)
    {
        switch (v2.GetDataType())
        {
            case SDataType.Number:
                return _value >= ((Number_SValue)v2)._value ? ISValue.True : ISValue.False;
        }
        return ISValue.False;
    }
    
    public ISValue Operator_Less_Equal_ISValue(ISValue v2)
    {
        switch (v2.GetDataType())
        {
            case SDataType.Number:
                return _value <= ((Number_SValue)v2)._value ? ISValue.True : ISValue.False;
        }
        return ISValue.False;
    }

    public ISValue Operator_Negative()
    {
        return new Number_SValue(-_value);
    }
    
    public ISValue Operator_Not()
    {
        return _value > 0 ? ISValue.False : ISValue.True;
    }
    
    public bool Operator_True()
    {
        return _value > 0;
    }
    
    public bool Operator_False()
    {
        return _value <= 0;
    }
    
}