
using System;

namespace DScript.Runtime
{
    /// <summary>
    /// 0参数回调函数
    /// </summary>
    public class Function_0_SValue : SValue
    {
        public readonly Function_0 Value;
    
        public Function_0_SValue(Function_0 value)
        {
            Value = value;
            dataType = DataType.Function_0;
        }

        public override object GetValue()
        {
            return Value;
        }

        public override SValue GetMember(string key)
        {
            return Null;
        }

        public override bool HasMember(string key)
        {
            throw new NotImplementedException();
        }

        public override void SetMember(string key, SValue value)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke()
        {
            return Value();
        }

        public override SValue Invoke(SValue v0)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
            SValue v9)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
            SValue v9, SValue v10)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
        {
            throw new NotImplementedException();
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
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

        public override SValue InvokeMethod(string key, SValue v0, SValue v1)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
            SValue v8)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
            SValue v8, SValue v9)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
        {
            throw new NotImplementedException();
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Equal_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Equal_String(string v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Equal_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Not_Equal_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Not_Equal_String(string v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Not_Equal_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Add_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Add_Double(double v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Add_String(string v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Add_String(string v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Add_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Subtract_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Subtract_Double(double v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Subtract_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Multiply_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Multiply_Double(double v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Multiply_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Divide_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Divide_Double(double v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Divide_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_SinceAdd()
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_SinceReduction()
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Greater_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Less_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Greater_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Less_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Greater_Equal_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Less_Equal_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Greater_Equal_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Positive()
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Negative()
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Not()
        {
            throw new NotImplementedException();
        }

        public override bool Operator_Less_Equal_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override bool Operator_True()
        {
            throw new NotImplementedException();
        }

        public override bool Operator_False()
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Modulus_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Modulus_Double(double v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Modulus_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_Negation()
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_Right(int v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_Left(int v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_Or_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Shift_Or_Double(double v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_Or_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_And_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Shift_And_Double(double v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_And_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_Xor_Double(double v2)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Append_Shift_Xor_Double(double v1)
        {
            throw new NotImplementedException();
        }

        public override SValue Operator_Shift_Xor_SValue(SValue v2)
        {
            throw new NotImplementedException();
        }
    }
}