
using System;

/// <summary>
/// 0参数回调函数
/// </summary>
internal class Function_0_SValue : ISValue
{
    private Function_0 _value;
    
    public Function_0_SValue(Function_0 value)
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
    
    public ISValue Invoke()
    {
        return _value();
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