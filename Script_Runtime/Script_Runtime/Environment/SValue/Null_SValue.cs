
using System;

/// <summary>
/// null类型
/// </summary>
internal class Null_SValue : ISValue
{
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
        return null;
    }

    public ISValue GetMember(string key)
    {
        throw new NullReferenceException();
    }

    public void SetMember(string key, ISValue value)
    {
        throw new NullReferenceException();
    }
}