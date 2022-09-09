
/// <summary>
/// true值
/// </summary>
internal class True_SValue : ISValue
{
    public SValueType GetValueType()
    {
        return SValueType.True;
    }

    public SDataType GetDataType()
    {
        return SDataType.True;
    }

    public object GetValue()
    {
        return true;
    }
}