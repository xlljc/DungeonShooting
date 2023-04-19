using DScript.Exception;

namespace DScript.Runtime
{
    /// <summary>
    /// true值
    /// </summary>
    public class True_SValue : SValue
    {
        public readonly bool Value = true;
        
        internal True_SValue()
        {
            dataType = DataType.True;
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

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue Invoke(SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
            SValue v7, SValue v8,
            SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
        {
            throw new InvokeMethodException($"'true' is not a function.");
        }

        public override SValue InvokeMethod(string key)
        {
            if (key == "toString")
            {
                return String_SValue._true;
            }
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override SValue InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5,
            SValue v6, SValue v7,
            SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15)
        {
            throw new InvokeMethodException($"The member function 'true.{key}' was not found.");
        }

        public override bool Operator_Equal_Double(double v2)
        {
            return 1 == v2;
        }

        public override bool Operator_Equal_String(string v2)
        {
            return false;
        }

        public override bool Operator_Equal_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return 1 == ((Number_SValue)v2).Value;
                case DataType.True:
                    return true;
            }

            return false;
        }

        public override bool Operator_Not_Equal_Double(double v2)
        {
            return 1 != v2;
        }

        public override bool Operator_Not_Equal_String(string v2)
        {
            return true;
        }

        public override bool Operator_Not_Equal_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return 1 != ((Number_SValue)v2).Value;
                case DataType.True:
                    return false;
            }

            return true;
        }

        public override SValue Operator_Add_Double(double v2)
        {
            return new Number_SValue(1 + v2);
        }

        public override SValue Operator_Append_Add_Double(double v1)
        {
            return new Number_SValue(v1 + 1);
        }

        public override SValue Operator_Add_String(string v2)
        {
            return new String_SValue("true" + v2);
        }

        public override SValue Operator_Append_Add_String(string v1)
        {
            return new String_SValue(v1 + "true");
        }

        public override SValue Operator_Add_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(1 + ((Number_SValue)v2).Value);
                case DataType.String:
                    return new String_SValue("true" + ((String_SValue)v2).Value);
                case DataType.False:
                case DataType.Null:
                    return One;
                case DataType.True:
                    return Two;
                case DataType.Object:
                    return new String_SValue("true" + ((Object_SValue)v2).Value);
                case DataType.Function_0:
                case DataType.Function_1:
                case DataType.Function_2:
                case DataType.Function_3:
                case DataType.Function_4:
                case DataType.Function_5:
                case DataType.Function_6:
                case DataType.Function_7:
                case DataType.Function_8:
                case DataType.Function_9:
                case DataType.Function_10:
                case DataType.Function_11:
                case DataType.Function_12:
                case DataType.Function_13:
                case DataType.Function_14:
                case DataType.Function_15:
                case DataType.Function_16:
                    return String_SValue._trueFunction;
            }

            return new String_SValue("true" + v2.GetValue());
        }

        public override SValue Operator_Subtract_Double(double v2)
        {
            return new Number_SValue(1 - v2);
        }

        public override SValue Operator_Append_Subtract_Double(double v1)
        {
            return new Number_SValue(v1 - 1);
        }

        public override SValue Operator_Subtract_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(1 - ((Number_SValue)v2).Value);
                case DataType.False:
                case DataType.Null:
                    return One;
                case DataType.True:
                    return Zero;
            }

            return NaN;
        }

        public override SValue Operator_Multiply_Double(double v2)
        {
            return new Number_SValue(1 * v2);
        }

        public override SValue Operator_Append_Multiply_Double(double v1)
        {
            return new Number_SValue(v1 * 1);
        }

        public override SValue Operator_Multiply_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(1 * ((Number_SValue)v2).Value);
                case DataType.False:
                case DataType.Null:
                    return Zero;
                case DataType.True:
                    return One;
            }

            return NaN;
        }

        public override SValue Operator_Divide_Double(double v2)
        {
            return new Number_SValue(1 / v2);
        }

        public override SValue Operator_Append_Divide_Double(double v1)
        {
            return new Number_SValue(v1 / 1);
        }

        public override SValue Operator_Divide_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(1 / ((Number_SValue)v2).Value);
                case DataType.False:
                case DataType.Null:
                    return PositiveInfinity;
                case DataType.True:
                    return One;
            }

            return NaN;
        }

        public override SValue Operator_SinceAdd()
        {
            return Two;
        }

        public override SValue Operator_SinceReduction()
        {
            return Zero;
        }

        public override bool Operator_Greater_Double(double v2)
        {
            return 1 > v2;
        }

        public override bool Operator_Less_Double(double v2)
        {
            return 1 < v2;
        }

        public override bool Operator_Greater_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return 1 > ((Number_SValue)v2).Value;
                case DataType.False:
                case DataType.Null:
                    return true;
            }

            return false;
        }

        public override bool Operator_Less_SValue(SValue v2)
        {
            if (v2.dataType == DataType.Number)
            {
                return 1 < ((Number_SValue)v2).Value;
            }

            return false;
        }

        public override bool Operator_Greater_Equal_Double(double v2)
        {
            return 1 >= v2;
        }

        public override bool Operator_Less_Equal_Double(double v2)
        {
            return 1 <= v2;
        }

        public override bool Operator_Greater_Equal_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return 1 >= ((Number_SValue)v2).Value;
                case DataType.False:
                case DataType.True:
                case DataType.Null:
                    return true;
            }

            return false;
        }

        public override SValue Operator_Positive()
        {
            return One;
        }

        public override SValue Operator_Negative()
        {
            return NegativeOne;
        }

        public override bool Operator_Not()
        {
            return false;
        }

        public override bool Operator_Less_Equal_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return 1 <= ((Number_SValue)v2).Value;
                case DataType.True:
                    return true;
            }

            return false;
        }

        public override bool Operator_True()
        {
            return true;
        }

        public override bool Operator_False()
        {
            return false;
        }

        public override SValue Operator_Modulus_Double(double v2)
        {
            return new Number_SValue(1 % v2);
        }

        public override SValue Operator_Append_Modulus_Double(double v1)
        {
            return new Number_SValue(v1 % 1);
        }

        public override SValue Operator_Modulus_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(1 % ((Number_SValue)v2).Value);
                case DataType.True:
                    return Zero;
            }

            return NaN;
        }

        public override SValue Operator_Shift_Negation()
        {
            return NegativeTwo;
        }

        public override SValue Operator_Shift_Right(int v1)
        {
            return new Number_SValue(1 >> v1);
        }

        public override SValue Operator_Shift_Left(int v1)
        {
            return new Number_SValue(1 << v1);
        }

        public override SValue Operator_Shift_Or_Double(double v2)
        {
            return new Number_SValue(1 | (int)v2);
        }

        public override SValue Operator_Append_Shift_Or_Double(double v1)
        {
            return new Number_SValue((int)v1 | 1);
        }

        public override SValue Operator_Shift_Or_SValue(SValue v2)
        {
            if (v2.dataType == DataType.Number)
            {
                return new Number_SValue(1 | (int)((Number_SValue)v2).Value);
            }

            return One;
        }

        public override SValue Operator_Shift_And_Double(double v2)
        {
            return new Number_SValue(1 & (int)v2);
        }

        public override SValue Operator_Append_Shift_And_Double(double v1)
        {
            return new Number_SValue((int)v1 & 1);
        }

        public override SValue Operator_Shift_And_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(1 & (int)((Number_SValue)v2).Value);
                case DataType.True:
                    return One;
            }

            return Zero;
        }

        public override SValue Operator_Shift_Xor_Double(double v2)
        {
            return new Number_SValue(1 ^ (int)v2);
        }

        public override SValue Operator_Append_Shift_Xor_Double(double v1)
        {
            return new Number_SValue((int)v1 ^ 1);
        }

        public override SValue Operator_Shift_Xor_SValue(SValue v2)
        {
            switch (v2.dataType)
            {
                case DataType.Number:
                    return new Number_SValue(1 ^ (int)((Number_SValue)v2).Value);
                case DataType.True:
                    return Zero;
            }

            return One;
        }
    }
}