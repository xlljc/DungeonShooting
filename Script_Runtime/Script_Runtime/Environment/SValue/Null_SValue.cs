
using System;

/// <summary>
/// null类型
/// </summary>
internal class Null_SValue : SValue
{
    public override SValueType GetValueType()
    {
        return SValueType.Null;
    }

    public override SDataType GetDataType()
    {
        return SDataType.Null;
    }

    public override object GetValue()
    {
        return null;
    }

    public override SValue GetMember(string key)
    {
        throw new NullReferenceException();
    }

    public override void SetMember(string key, SValue value)
    {
        throw new NullReferenceException();
    }
}