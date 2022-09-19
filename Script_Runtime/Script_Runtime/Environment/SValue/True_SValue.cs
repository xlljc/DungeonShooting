
/// <summary>
/// true值
/// </summary>
internal class True_SValue : SValue
{
    public override ScriptType GetScriptType()
    {
        return ScriptType.True;
    }

    public override DataType GetDataType()
    {
        return DataType.True;
    }

    public override object GetValue()
    {
        return true;
    }

    public override SValue GetMember(string key)
    {
        return Null;
    }

    public override bool HasMember(string key)
    {
        return false;
    }

    public override void SetMember(string key, SValue value)
    {
        throw new OperationMemberException($"Member '{key}' not defined.");
    }

    public override SValue Invoke()
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
    {
        throw new InvokeMethodException($"'true' is not a function.");
    }

    public override SValue InvokeMethod(string key)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
    {
        throw new InvokeMethodException($"The member function 'true.{key}' was not found");
    }

    internal override bool Operator_Equal_Double(double v2)
    {
        return 1 == v2;
    }

    internal override bool Operator_Equal_String(string v2)
    {
        return false;
    }

    internal override bool Operator_Equal_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return 1 == ((Number_SValue)v2)._value;
            case ScriptType.True:
                return true;
        }

        return false;
    }

    internal override bool Operator_Not_Equal_Double(double v2)
    {
        return 1 != v2;
    }

    internal override bool Operator_Not_Equal_String(string v2)
    {
        return true;
    }

    internal override bool Operator_Not_Equal_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return 1 != ((Number_SValue)v2)._value;
            case ScriptType.True:
                return false;
        }

        return true;
    }

    internal override SValue Operator_Add_Double(double v2)
    {
        return new Number_SValue(1 + v2);
    }

    internal override SValue Operator_Append_Add_Double(double v1)
    {
        return new Number_SValue(v1 + 1);
    }

    internal override SValue Operator_Add_String(string v2)
    {
        return new String_SValue("true" + v2);
    }

    internal override SValue Operator_Append_Add_String(string v1)
    {
        return new String_SValue(v1 + "true");
    }

    internal override SValue Operator_Add_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(1 + ((Number_SValue)v2)._value);
            case ScriptType.String:
                return new String_SValue("true" + ((String_SValue)v2)._value);
            case ScriptType.False:
            case ScriptType.Null:
                return One;
            case ScriptType.True:
                return Two;
        }

        return new String_SValue("true" + v2.GetValue());
    }

    internal override SValue Operator_Subtract_Double(double v2)
    {
        return new Number_SValue(1 - v2);
    }

    internal override SValue Operator_Append_Subtract_Double(double v1)
    {
        return new Number_SValue(v1 - 1);
    }

    internal override SValue Operator_Subtract_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(1 - ((Number_SValue)v2)._value);
            case ScriptType.False:
            case ScriptType.Null:
                return One;
            case ScriptType.True:
                return Zero;
        }

        return NaN;
    }

    internal override SValue Operator_Multiply_Double(double v2)
    {
        return new Number_SValue(1 * v2);
    }

    internal override SValue Operator_Append_Multiply_Double(double v1)
    {
        return new Number_SValue(v1 * 1);
    }

    internal override SValue Operator_Multiply_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(1 * ((Number_SValue)v2)._value);
            case ScriptType.False:
            case ScriptType.Null:
                return Zero;
            case ScriptType.True:
                return One;
        }

        return NaN;
    }

    internal override SValue Operator_Divide_Double(double v2)
    {
        return new Number_SValue(1 / v2);
    }

    internal override SValue Operator_Append_Divide_Double(double v1)
    {
        return new Number_SValue(v1 / 1);
    }

    internal override SValue Operator_Divide_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(1 / ((Number_SValue)v2)._value);
            case ScriptType.False:
            case ScriptType.Null:
                return PositiveInfinity;
            case ScriptType.True:
                return One;
        }

        return NaN;
    }

    internal override SValue Operator_SinceAdd()
    {
        return Two;
    }

    internal override SValue Operator_SinceReduction()
    {
        return Zero;
    }

    internal override bool Operator_Greater_Double(double v2)
    {
        return 1 > v2;
    }

    internal override bool Operator_Less_Double(double v2)
    {
        return 1 < v2;
    }

    internal override bool Operator_Greater_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return 1 > ((Number_SValue)v2)._value;
            case ScriptType.False:
            case ScriptType.Null:
                return true;
        }

        return false;
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
        return true;
    }

    internal override bool Operator_False()
    {
        return false;
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