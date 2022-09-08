
using System;

public class Function_1_SValue : ISValue
{
    private Function_1 _value;
    
    public Function_1_SValue(Function_1 value)
    {
        _value = value;
    }
    
    public SValueType GetValueType()
    {
        return SValueType.Function;
    }

    public SDataType GetDataType()
    {
        return SDataType.Function_0;
    }

    public object GetValue()
    {
        return _value;
    }

    void ISValue.SetValue(object value)
    {
        _value = (Function_1)value;
    }

    public ISValue Invoke(params ISValue[] ps)
    {
        return _value(ps[0]);
    }

    public ISValue InvokeMethod(string key, params ISValue[] ps)
    {
        throw new Exception($"Property {key} is not a function!");
    }

    public ISValue GetProperty(string key)
    {
        return NullSValue.Instance;
    }

    public void SetProperty(string key, ISValue value)
    {
        throw new Exception($"Property {key} is not define!");
    }
}