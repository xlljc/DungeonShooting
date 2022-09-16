
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

    public override SValue GetMember(string key)
    {
        throw new System.NotImplementedException();
    }

    public override SValue HasMember(string key)
    {
        throw new System.NotImplementedException();
    }

    public override void SetMember(string key, SValue value)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke()
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Equal_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Equal_String(string v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Equal_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Not_Equal_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Not_Equal_String(string v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Not_Equal_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Add_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Add_Double(double v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Add_String(string v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Add_String(string v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Add_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Subtract_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Subtract_Double(double v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Subtract_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Multiply_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Multiply_Double(double v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Multiply_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Divide_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Divide_Double(double v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Divide_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_SinceAdd()
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_SinceReduction()
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Greater_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Less_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Greater_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Less_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Greater_Equal_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Less_Equal_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Greater_Equal_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Positive()
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Negative()
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Not()
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Less_Equal_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_True()
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_False()
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Modulus_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Modulus_Double(double v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Modulus_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_Negation()
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_Right(int v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_Left(int v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_Or_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Shift_Or_Double(double v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_Or_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_And_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Shift_And_Double(double v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_And_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_Xor_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Append_Shift_Xor_Double(double v1)
    {
        throw new System.NotImplementedException();
    }

    internal override SValue Operator_Shift_Xor_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }
}