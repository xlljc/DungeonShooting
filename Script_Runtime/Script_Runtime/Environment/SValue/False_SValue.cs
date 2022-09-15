
/// <summary>
/// false值
/// </summary>
internal class False_SValue : SValue
{
    public override SValueType GetValueType()
    {
        return SValueType.False;
    }

    public override SDataType GetDataType()
    {
        return SDataType.False;
    }

    public override object GetValue()
    {
        return false;
    }
    
    public override bool Operator_True()
    {
        return false;
    }

    public override bool Operator_False()
    {
        return true;
    }
    
}