
using System;

/// <summary>
/// 数字类型
/// </summary>
internal class Number_SValue : SValue
{
    internal double _value;
    
    public Number_SValue(double value)
    {
        _value = value;
    }
    
    public override SValueType GetValueType()
    {
        return SValueType.Number;
    }

    public override SDataType GetDataType()
    {
        return SDataType.Number;
    }

    public override object GetValue()
    {
        return _value;
    }
    
    public override SValue Operator_SinceAdd()
    {
        return new Number_SValue(_value + 1);
    }
    
    public override SValue Operator_SinceReduction()
    {
        return new Number_SValue(_value - 1);
    }
    
    public override SValue Operator_Greater_Double(double v2)
    {
        return _value > v2 ? SValue.True : SValue.False;
    }

    public override SValue Operator_Less_Double(double v2)
    {
        return _value < v2 ? SValue.True : SValue.False;
    }

    public override SValue Operator_Greater_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case SDataType.Number:
                return _value > ((Number_SValue)v2)._value ? SValue.True : SValue.False;
        }
        return SValue.False;
    }
    
    public override SValue Operator_Less_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case SDataType.Number:
                return _value < ((Number_SValue)v2)._value ? SValue.True : SValue.False;
        }
        return SValue.False;
    }

    public override SValue Operator_Greater_Equal_Double(double v2)
    {
        return _value >= v2 ? SValue.True : SValue.False;
    }

    public override SValue Operator_Less_Equal_Double(double v2)
    {
        return _value <= v2 ? SValue.True : SValue.False;
    }
    
    public override SValue Operator_Greater_Equal_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case SDataType.Number:
                return _value >= ((Number_SValue)v2)._value ? SValue.True : SValue.False;
        }
        return SValue.False;
    }
    
    public override SValue Operator_Less_Equal_SValue(SValue v2)
    {
        switch (v2.GetDataType())
        {
            case SDataType.Number:
                return _value <= ((Number_SValue)v2)._value ? SValue.True : SValue.False;
        }
        return SValue.False;
    }

    public override SValue Operator_Negative()
    {
        return new Number_SValue(-_value);
    }
    
    public override SValue Operator_Not()
    {
        return _value > 0 ? SValue.False : SValue.True;
    }
    
    public override bool Operator_True()
    {
        return _value > 0;
    }
    
    public override bool Operator_False()
    {
        return _value <= 0;
    }
    
}