
using System;

/// <summary>
/// null类型
/// </summary>
internal class NullSValue : ISValue
{
    public static NullSValue Instance = new NullSValue();

    private NullSValue()
    {
    }

    public SValueType GetValueType()
    {
        return SValueType.Null;
    }

    public SDataType GetDataType()
    {
        return SDataType.Null;
    }

    public object GetValue()
    {
        throw new NullReferenceException();
    }

    public ISValue GetProperty(string key)
    {
        throw new NullReferenceException();
    }

    public void SetProperty(string key, ISValue value)
    {
        throw new NullReferenceException();
    }
}