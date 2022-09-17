
public class Function_1_SValue : SValue
{
    private Function_1 _value;
    
    public Function_1_SValue(Function_1 value)
    {
        _value = value;
    }
    
    public override ScriptType GetScriptType()
    {
        return ScriptType.Function;
    }

    public override DataType GetDataType()
    {
        return DataType.Function_0;
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
        return _value(v0);
    }

    public override SValue Invoke(SValue v0, SValue v1)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new System.NotImplementedException();
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
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

    public override SValue InvokeMethod(string key, SValue v0, SValue v1)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new System.NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Equal_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Equal_String(string v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Equal_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Not_Equal_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Not_Equal_String(string v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Not_Equal_SValue(SValue v2)
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

    internal override bool Operator_Greater_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Less_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Greater_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Less_SValue(SValue v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Greater_Equal_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Less_Equal_Double(double v2)
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Greater_Equal_SValue(SValue v2)
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

    internal override bool Operator_Not()
    {
        throw new System.NotImplementedException();
    }

    internal override bool Operator_Less_Equal_SValue(SValue v2)
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