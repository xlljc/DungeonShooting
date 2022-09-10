
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
    
    public bool Operator_True()
    {
        return true;
    }

    public bool Operator_False()
    {
        return false;
    }
    
}