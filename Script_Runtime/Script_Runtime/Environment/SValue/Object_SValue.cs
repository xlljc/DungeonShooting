
/// <summary>
/// 对象类型
/// </summary>
internal class Object_SValue : SValue
{
    internal SObject _value;

    public Object_SValue(SObject value)
    {
        _value = value;
    }

    public override ScriptType GetScriptType()
    {
        return ScriptType.Object;
    }

    public override DataType GetDataType()
    {
        return DataType.Object;
    }

    public override object GetValue()
    {
        return _value;
    }

    public override SValue GetMember(string key)
    {
        return _value.__GetMember(key);
    }

    public override bool HasMember(string key)
    {
        return _value.__HasMember(key);
    }

    public override void SetMember(string key, SValue value)
    {
        _value.__SetMember(key, value);
    }

    public override SValue Invoke()
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
    {
        throw new InvokeMethodException($"'{_value}' is not a function.");
    }

    public override SValue InvokeMethod(string key)
    {
        return _value.__InvokeMethod(key);
    }

    public override SValue InvokeMethod(string key, SValue v0)
    {
        return _value.__InvokeMethod(key, v0);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1)
    {
        return _value.__InvokeMethod(key, v0, v1);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
    {
        return _value.__InvokeMethod(key, v0, v1, v2);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7, SValue v8)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7, SValue v8, SValue v9)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7, SValue v8, SValue v9, SValue v10)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14);
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14,
        SValue v15)
    {
        return _value.__InvokeMethod(key, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
    }

    internal override bool Operator_Equal_Double(double v2)
    {
        return false;
    }

    internal override bool Operator_Equal_String(string v2)
    {
        return false;
    }

    internal override bool Operator_Equal_SValue(SValue v2)
    {
        if (v2.GetDataType() == DataType.Object)
        {
            return _value == ((Object_SValue)v2)._value;
        }
        return GetValue() == v2.GetValue();
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
        return false;
    }

    internal override bool Operator_Less_Double(double v2)
    {
        return false;
    }

    internal override bool Operator_Greater_SValue(SValue v2)
    {
        return false;
    }

    internal override bool Operator_Less_SValue(SValue v2)
    {
        return false;
    }

    internal override bool Operator_Greater_Equal_Double(double v2)
    {
        return false;
    }

    internal override bool Operator_Less_Equal_Double(double v2)
    {
        return false;
    }

    internal override bool Operator_Greater_Equal_SValue(SValue v2)
    {
        return false;
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
        return false;
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
