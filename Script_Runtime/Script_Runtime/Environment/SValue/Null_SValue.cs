
using System;

/// <summary>
/// null类型
/// </summary>
internal class Null_SValue : SValue
{
    public override SValueType GetValueType()
    {
        return SValueType.Null;
    }

    public override SDataType GetDataType()
    {
        return SDataType.Null;
    }

    public override object GetValue()
    {
        return null;
    }

    public override SValue GetMember(string key)
    {
        throw new NullReferenceException();
    }

    public override SValue HasMember(string key)
    {
        throw new NotImplementedException();
    }

    public override void SetMember(string key, SValue value)
    {
        throw new NullReferenceException();
    }

    public override SValue Invoke()
    {
        throw new NotImplementedException();
    }

    public override SValue Invoke(SValue v0)
    {
        throw new NotImplementedException();
    }

    public override SValue InvokeMethod(string key)
    {
        throw new NotImplementedException();
    }

    public override SValue InvokeMethod(string key, SValue v0)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Equal_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Equal_String(string v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Equal_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Not_Equal_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Not_Equal_String(string v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Not_Equal_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Add_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Add_Double(double v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Add_String(string v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Add_String(string v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Add_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Subtract_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Subtract_Double(double v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Subtract_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Multiply_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Multiply_Double(double v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Multiply_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Divide_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Divide_Double(double v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Divide_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_SinceAdd()
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_SinceReduction()
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Greater_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Less_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Greater_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Less_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Greater_Equal_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Less_Equal_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Greater_Equal_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Positive()
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Negative()
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Not()
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Less_Equal_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override bool Operator_True()
    {
        throw new NotImplementedException();
    }

    internal override bool Operator_False()
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Modulus_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Modulus_Double(double v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Modulus_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_Negation()
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_Right(int v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_Left(int v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_Or_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Shift_Or_Double(double v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_Or_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_And_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Shift_And_Double(double v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_And_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_Xor_Double(double v2)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Append_Shift_Xor_Double(double v1)
    {
        throw new NotImplementedException();
    }

    internal override SValue Operator_Shift_Xor_SValue(SValue v2)
    {
        throw new NotImplementedException();
    }
}