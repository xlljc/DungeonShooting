
/// <summary>
/// false值
/// </summary>
internal class False_SValue : ISValue
{
    public SValueType GetValueType()
    {
        return SValueType.False;
    }

    public SDataType GetDataType()
    {
        return SDataType.False;
    }

    public object GetValue()
    {
        return false;
    }
    
    public bool Operator_True()
    {
        return false;
    }

    public bool Operator_False()
    {
        return true;
    }
    
}