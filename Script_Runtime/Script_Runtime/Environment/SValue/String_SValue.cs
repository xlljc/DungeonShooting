
/// <summary>
/// 字符串
/// </summary>
internal class String_SValue : ISValue
{
    internal string _value;

    public String_SValue(string value)
    {
        _value = value;
    }
    
    public SValueType GetValueType()
    {
        return SValueType.String;
    }

    public SDataType GetDataType()
    {
        return SDataType.String;
    }

    public object GetValue()
    {
        return _value;
    }
}