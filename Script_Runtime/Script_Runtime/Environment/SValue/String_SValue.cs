
/// <summary>
/// 字符串
/// </summary>
internal class String_SValue : SValue
{
    internal string _value;

    public String_SValue(string value)
    {
        _value = value;
    }
    
    public override SValueType GetValueType()
    {
        return SValueType.String;
    }

    public override SDataType GetDataType()
    {
        return SDataType.String;
    }

    public override object GetValue()
    {
        return _value;
    }
}