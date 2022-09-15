
/// <summary>
/// true值
/// </summary>
internal class True_SValue : SValue
{
    public override SValueType GetValueType()
    {
        return SValueType.True;
    }

    public override SDataType GetDataType()
    {
        return SDataType.True;
    }

    public override object GetValue()
    {
        return true;
    }
    
    public override bool Operator_True()
    {
        return true;
    }

    public override bool Operator_False()
    {
        return false;
    }
    
}