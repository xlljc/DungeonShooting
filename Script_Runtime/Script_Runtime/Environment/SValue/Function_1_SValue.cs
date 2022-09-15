
public class Function_1_SValue : SValue
{
    private Function_1 _value;
    
    public Function_1_SValue(Function_1 value)
    {
        _value = value;
    }
    
    public override SValueType GetValueType()
    {
        return SValueType.Function;
    }

    public override SDataType GetDataType()
    {
        return SDataType.Function_0;
    }

    public override object GetValue()
    {
        return _value;
    }

    public override SValue Invoke(SValue v0)
    {
        return _value(v0);
    }
}