
/// <summary>
/// false值
/// </summary>
internal class False_SValue : SValue
{
    public override ScriptType GetScriptType()
    {
        return ScriptType.False;
    }

    public override DataType GetDataType()
    {
        return DataType.False;
    }

    public override object GetValue()
    {
        return false;
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
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8,
        SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
    {
        throw new InvokeMethodException($"'false' is not a function.");
    }

    public override SValue InvokeMethod(string key)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
        SValue v6, SValue v7,
        SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
    {
        throw new InvokeMethodException($"The member function 'false.{key}' was not found");
    }

    internal override bool Operator_Equal_Double(double v2)
    {
        return 0 == v2;
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
                return 0 == ((Number_SValue)v2)._value;
            case ScriptType.False:
                return true;
        }

        return false;
    }

    internal override bool Operator_Not_Equal_Double(double v2)
    {
        return 0 != v2;
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
                return 0 != ((Number_SValue)v2)._value;
            case ScriptType.False:
                return false;
        }

        return true;
    }

    internal override SValue Operator_Add_Double(double v2)
    {
        return new Number_SValue(v2);
    }

    internal override SValue Operator_Append_Add_Double(double v1)
    {
        return new Number_SValue(v1);
    }

    internal override SValue Operator_Add_String(string v2)
    {
        return new String_SValue("false" + v2);
    }

    internal override SValue Operator_Append_Add_String(string v1)
    {
        return new String_SValue(v1 + "false");
    }

    internal override SValue Operator_Add_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return v2;
            case ScriptType.String:
                return new String_SValue("false" + ((String_SValue)v2)._value);
            case ScriptType.False:
            case ScriptType.Null:
                return Zero;
            case ScriptType.True:
                return One;
        }

        return new String_SValue("false" + v2.GetValue());
    }

    internal override SValue Operator_Subtract_Double(double v2)
    {
        return new Number_SValue(-v2);
    }

    internal override SValue Operator_Append_Subtract_Double(double v1)
    {
        return new Number_SValue(v1);
    }

    internal override SValue Operator_Subtract_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(-((Number_SValue)v2)._value);
            case ScriptType.False:
            case ScriptType.Null:
                return Zero;
            case ScriptType.True:
                return NegativeOne;
        }

        return NaN;
    }

    internal override SValue Operator_Multiply_Double(double v2)
    {
        return Zero;
    }

    internal override SValue Operator_Append_Multiply_Double(double v1)
    {
        return Zero;
    }

    internal override SValue Operator_Multiply_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
            case ScriptType.False:
            case ScriptType.Null:
            case ScriptType.True:
                return Zero;
        }

        return NaN;
    }

    internal override SValue Operator_Divide_Double(double v2)
    {
        return new Number_SValue(0 / v2);
    }

    internal override SValue Operator_Append_Divide_Double(double v1)
    {
        return new Number_SValue(v1 / 0);
    }

    internal override SValue Operator_Divide_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(0 / ((Number_SValue)v2)._value);
            case ScriptType.True:
                return One;
        }

        return NaN;
    }

    internal override SValue Operator_SinceAdd()
    {
        return One;
    }

    internal override SValue Operator_SinceReduction()
    {
        return NegativeOne;
    }

    internal override bool Operator_Greater_Double(double v2)
    {
        return 0 > v2;
    }

    internal override bool Operator_Less_Double(double v2)
    {
        return 0 < v2;
    }

    internal override bool Operator_Greater_SValue(SValue v2)
    {
        if (v2.GetScriptType() == ScriptType.Number)
        {
            return 0 > ((Number_SValue)v2)._value;
        }

        return false;
    }

    internal override bool Operator_Less_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return 0 < ((Number_SValue)v2)._value;
            case ScriptType.True:
                return true;
        }

        return false;
    }

    internal override bool Operator_Greater_Equal_Double(double v2)
    {
        return 0 >= v2;
    }

    internal override bool Operator_Less_Equal_Double(double v2)
    {
        return 0 <= v2;
    }

    internal override bool Operator_Greater_Equal_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return 0 >= ((Number_SValue)v2)._value;
            case ScriptType.False:
            case ScriptType.Null:
                return true;
        }

        return false;
    }

    internal override SValue Operator_Positive()
    {
        return Zero;
    }

    internal override SValue Operator_Negative()
    {
        return Zero;
    }

    internal override bool Operator_Not()
    {
        return true;
    }

    internal override bool Operator_Less_Equal_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return 0 <= ((Number_SValue)v2)._value;
            case ScriptType.True:
            case ScriptType.False:
            case ScriptType.Null:
                return true;
        }

        return false;
    }

    internal override bool Operator_True()
    {
        return false;
    }

    internal override bool Operator_False()
    {
        return true;
    }

    internal override SValue Operator_Modulus_Double(double v2)
    {
        return new Number_SValue(0 % v2);
    }

    internal override SValue Operator_Append_Modulus_Double(double v1)
    {
        return new Number_SValue(v1 % 0);
    }

    internal override SValue Operator_Modulus_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(0 % ((Number_SValue)v2)._value);
            case ScriptType.True:
                return Zero;
        }

        return NaN;
    }

    internal override SValue Operator_Shift_Negation()
    {
        return NegativeOne;
    }

    internal override SValue Operator_Shift_Right(int v1)
    {
        return Zero;
    }

    internal override SValue Operator_Shift_Left(int v1)
    {
        return Zero;
    }

    internal override SValue Operator_Shift_Or_Double(double v2)
    {
        return new Number_SValue(0 | (int)v2);
    }

    internal override SValue Operator_Append_Shift_Or_Double(double v1)
    {
        return new Number_SValue((int)v1 | 0);
    }

    internal override SValue Operator_Shift_Or_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(0 | (int)((Number_SValue)v2)._value);
            case ScriptType.True:
                return One;
        }

        return Zero;
    }

    internal override SValue Operator_Shift_And_Double(double v2)
    {
        return Zero;
    }

    internal override SValue Operator_Append_Shift_And_Double(double v1)
    {
        return Zero;
    }

    internal override SValue Operator_Shift_And_SValue(SValue v2)
    {
        return Zero;
    }

    internal override SValue Operator_Shift_Xor_Double(double v2)
    {
        return new Number_SValue(0 ^ (int)v2);
    }

    internal override SValue Operator_Append_Shift_Xor_Double(double v1)
    {
        return new Number_SValue((int)v1 ^ 0);
    }

    internal override SValue Operator_Shift_Xor_SValue(SValue v2)
    {
        switch (v2.GetScriptType())
        {
            case ScriptType.Number:
                return new Number_SValue(0 ^ (int)((Number_SValue)v2)._value);
            case ScriptType.True:
                return One;
        }

        return Zero;
    }
}