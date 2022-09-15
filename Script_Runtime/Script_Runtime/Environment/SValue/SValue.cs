
/// <summary>
/// 脚本中的数据描述接口
/// </summary>
public abstract class SValue
{
    /// <summary>
    /// 获取该类在脚本中的数据类型
    /// </summary>
    public abstract SValueType GetValueType();

    /// <summary>
    /// 获取该类存放的数据类型
    /// </summary>
    public abstract SDataType GetDataType();

    /// <summary>
    /// 获取数据对象
    /// </summary>
    public abstract object GetValue();

    /// <summary>
    /// 获取成员属性
    /// </summary>
    public virtual SValue GetMember(string key)
    {
        return Null;
    }

    /// <summary>
    /// 返回是否存在属性
    /// </summary>
    public SValue HasMember(string key)
    {
        return False;
    }

    /// <summary>
    /// 设置成员属性
    /// </summary>
    public virtual void SetMember(string key, SValue value)
    {
        throw new OperationMemberException($"Member '{key}' not defined.");
    }

    /// <summary>
    /// 把自己当成函数执行
    /// </summary>
    public virtual SValue Invoke()
    {
        throw new InvokeMethodException("Instance is not a function.");
    }

    /// <summary>
    /// 把自己当成函数执行
    /// </summary>
    public virtual SValue Invoke(SValue v0)
    {
        throw new InvokeMethodException("Instance is not a function.");
    }

    /// <summary>
    /// 执行该对象的成员函数
    /// </summary>
    public virtual SValue InvokeMethod(string key)
    {
        throw new InvokeMethodException("No overloaded member function was found.");
    }

    /// <summary>
    /// 执行该对象的成员函数
    /// </summary>
    public virtual SValue InvokeMethod(string key, SValue v0)
    {
        throw new InvokeMethodException("No overloaded member function was found.");
    }

    #region 子类需重写的运算函数

    /// <summary>
    /// 判断等于, 和 double 比较
    /// </summary>
    public virtual SValue Operator_Equal_Double(double v2)
    {
        return False;
    }

    /// <summary>
    /// 判断等于, 和 string 比较
    /// </summary>
    public virtual SValue Operator_Equal_String(string v2)
    {
        return False;
    }

    /// <summary>
    /// 判断等于, 和 SValue 比较
    /// </summary>
    public virtual SValue Operator_Equal_SValue(SValue v2)
    {
        return GetValue() == v2.GetValue() ? True : False;
    }

    /// <summary>
    /// 判断不等于, 和 double 比较
    /// </summary>
    public virtual SValue Operator_Not_Equal_Double(double v2)
    {
        return True;
    }

    /// <summary>
    /// 判断不等于, 和 string 比较
    /// </summary>
    public virtual SValue Operator_Not_Equal_String(string v2)
    {
        return True;
    }

    /// <summary>
    /// 判断不等于, 和 SValue 比较
    /// </summary>
    public virtual SValue Operator_Not_Equal_SValue(SValue v2)
    {
        return GetValue() == v2.GetValue() ? False : True;
    }

    /// <summary>
    /// 和 double 相加
    /// </summary>
    public virtual SValue Operator_Add_Double(double v2)
    {
        return NaN;
    }

    /// <summary>
    /// 和 double 相加, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Add_Double(double v1)
    {
        return NaN;
    }

    /// <summary>
    /// 和 string 相加
    /// </summary>
    public virtual SValue Operator_Add_String(string v2)
    {
        return new String_SValue(GetValue() + v2);
    }

    /// <summary>
    /// 和 string 相加, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Add_String(string v1)
    {
        return new String_SValue(v1 + GetValue());
    }

    /// <summary>
    /// 和 SValue 相加
    /// </summary>
    public virtual SValue Operator_Add_SValue(SValue v2)
    {
        return new String_SValue(GetValue().ToString() + v2.GetValue());
    }

    /// <summary>
    /// 和 double 相减
    /// </summary>
    public virtual SValue Operator_Subtract_Double(double v2)
    {
        return NaN;
    }

    /// <summary>
    /// 和 double 相减, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Subtract_Double(double v1)
    {
        return NaN;
    }

    /// <summary>
    /// 和 SValue 相减
    /// </summary>
    public virtual SValue Operator_Subtract_SValue(SValue v2)
    {
        return NaN;
    }

    /// <summary>
    /// 和 double 相乘
    /// </summary>
    public virtual SValue Operator_Multiply_Double(double v2)
    {
        return NaN;
    }

    /// <summary>
    /// 和 double 相乘, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Multiply_Double(double v1)
    {
        return NaN;
    }

    /// <summary>
    /// 和 SValue 相乘
    /// </summary>
    public virtual SValue Operator_Multiply_SValue(SValue v2)
    {
        return NaN;
    }

    /// <summary>
    /// 和 double 相除
    /// </summary>
    public virtual SValue Operator_Divide_Double(double v2)
    {
        return NaN;
    }

    /// <summary>
    /// 和 double 相除, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Divide_Double(double v1)
    {
        return NaN;
    }

    /// <summary>
    /// 和 SValue 相除
    /// </summary>
    public virtual SValue Operator_Divide_SValue(SValue v2)
    {
        return NaN;
    }

    /// <summary>
    /// 自加
    /// </summary>
    public virtual SValue Operator_SinceAdd()
    {
        return NaN;
    }

    /// <summary>
    /// 自减
    /// </summary>
    public virtual SValue Operator_SinceReduction()
    {
        return NaN;
    }

    /// <summary>
    /// 大于
    /// </summary>
    public virtual SValue Operator_Greater_Double(double v2)
    {
        return False;
    }

    /// <summary>
    /// 小于
    /// </summary>
    public virtual SValue Operator_Less_Double(double v2)
    {
        return False;
    }

    /// <summary>
    /// 大于
    /// </summary>
    public virtual SValue Operator_Greater_SValue(SValue v2)
    {
        return False;
    }

    /// <summary>
    /// 小于
    /// </summary>
    public virtual SValue Operator_Less_SValue(SValue v2)
    {
        return False;
    }

    /// <summary>
    /// 大于等于
    /// </summary>
    public virtual SValue Operator_Greater_Equal_Double(double v2)
    {
        return False;
    }

    /// <summary>
    /// 小于等于
    /// </summary>
    public virtual SValue Operator_Less_Equal_Double(double v2)
    {
        return False;
    }

    /// <summary>
    /// 大于等于
    /// </summary>
    public virtual SValue Operator_Greater_Equal_SValue(SValue v2)
    {
        return False;
    }

    /// <summary>
    /// 正数
    /// </summary>
    public virtual SValue Operator_Positive()
    {
        return this;
    }

    /// <summary>
    /// 负数
    /// </summary>
    public virtual SValue Operator_Negative()
    {
        return NaN;
    }

    /// <summary>
    /// 取反
    /// </summary>
    public virtual SValue Operator_Not()
    {
        return True;
    }

    /// <summary>
    /// 小于等于
    /// </summary>
    public virtual SValue Operator_Less_Equal_SValue(SValue v2)
    {
        return False;
    }

    /// <summary>
    /// 判断true
    /// </summary>
    public virtual bool Operator_True()
    {
        return true;
    }

    /// <summary>
    /// 判断false
    /// </summary>
    public virtual bool Operator_False()
    {
        return false;
    }

    /// <summary>
    /// 取模运算
    /// </summary>
    public virtual SValue Operator_Modulus_Double(double v2)
    {
        return NaN;
    }

    /// <summary>
    /// 取模运算, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Modulus_Double(double v1)
    {
        return NaN;
    }

    /// <summary>
    /// 取模运算
    /// </summary>
    public virtual SValue Operator_Modulus_SValue(SValue v2)
    {
        return NaN;
    }

    /// <summary>
    /// 位运算取反, 符号 ~
    /// </summary>
    public virtual SValue Operator_Shift_Negation()
    {
        return NaN;
    }

    /// <summary>
    /// 位运算, 右移
    /// </summary>
    public virtual SValue Operator_Shift_Right(int v1)
    {
        return NaN;
    }

    /// <summary>
    /// 位运算, 左移
    /// </summary>
    public virtual SValue Operator_Shift_Left(int v1)
    {
        return NaN;
    }

    /// <summary>
    /// 或运算, 单位: |
    /// </summary>
    public virtual SValue Operator_Shift_Or_Double(double v2)
    {
        return NaN;
    }

    /// <summary>
    /// 或运算, 单位: |, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Shift_Or_Double(double v1)
    {
        return NaN;
    }

    /// <summary>
    /// 或运算, 单位: |
    /// </summary>
    public virtual SValue Operator_Shift_Or_SValue(SValue v2)
    {
        return NaN;
    }

    /// <summary>
    /// 与运算, 单位: &
    /// </summary>
    public virtual SValue Operator_Shift_And_Double(double v2)
    {
        return NaN;
    }

    /// <summary>
    /// 与运算, 单位: &, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Shift_And_Double(double v1)
    {
        return NaN;
    }

    /// <summary>
    /// 与运算, 单位: &
    /// </summary>
    public virtual SValue Operator_Shift_And_SValue(SValue v2)
    {
        return NaN;
    }

    /// <summary>
    /// 异或运算, 单位: ^
    /// </summary>
    public virtual SValue Operator_Shift_Xor_Double(double v2)
    {
        return NaN;
    }

    /// <summary>
    /// 异或运算, 单位: ^, v1 在前
    /// </summary>
    public virtual SValue Operator_Append_Shift_Xor_Double(double v1)
    {
        return NaN;
    }

    /// <summary>
    /// 异或运算, 单位: ^
    /// </summary>
    public virtual SValue Operator_Shift_Xor_SValue(SValue v2)
    {
        return NaN;
    }

    #endregion

    #region 运算符重载

    public static SValue operator +(SValue v1, double v2)
    {
        return v1.Operator_Add_Double(v2);
    }

    public static SValue operator +(double v1, SValue v2)
    {
        return v2.Operator_Append_Add_Double(v1);
    }

    public static SValue operator +(SValue v1, string v2)
    {
        return v1.Operator_Add_String(v2);
    }

    public static SValue operator +(string v1, SValue v2)
    {
        return v2.Operator_Append_Add_String(v1);
    }

    public static SValue operator +(SValue v1, SValue v2)
    {
        return v1.Operator_Add_SValue(v2);
    }

    public static SValue operator -(SValue v1, double v2)
    {
        return v1.Operator_Subtract_Double(v2);
    }

    public static SValue operator -(double v1, SValue v2)
    {
        return v2.Operator_Append_Subtract_Double(v1);
    }

    public static SValue operator -(SValue v1, SValue v2)
    {
        return v1.Operator_Subtract_SValue(v2);
    }

    public static SValue operator *(SValue v1, double v2)
    {
        return v1.Operator_Multiply_Double(v2);
    }

    public static SValue operator *(double v1, SValue v2)
    {
        return v2.Operator_Append_Multiply_Double(v1);
    }

    public static SValue operator *(SValue v1, SValue v2)
    {
        return v1.Operator_Multiply_SValue(v2);
    }

    public static SValue operator /(SValue v1, double v2)
    {
        return v1.Operator_Divide_Double(v2);
    }

    public static SValue operator /(double v1, SValue v2)
    {
        return v2.Operator_Append_Divide_Double(v1);
    }

    public static SValue operator /(SValue v1, SValue v2)
    {
        return v1.Operator_Divide_SValue(v2);
    }

    public static SValue operator ==(SValue v1, double v2)
    {
        return v1.Operator_Equal_Double(v2);
    }

    public static SValue operator ==(double v1, SValue v2)
    {
        return v2.Operator_Equal_Double(v1);
    }

    public static SValue operator ==(SValue v1, string v2)
    {
        return v1.Operator_Equal_String(v2);
    }

    public static SValue operator ==(string v1, SValue v2)
    {
        return v2.Operator_Equal_String(v1);
    }

    public static SValue operator ==(SValue v1, SValue v2)
    {
        return v1.Operator_Equal_SValue(v2);
    }

    public static SValue operator !=(SValue v1, double v2)
    {
        return v1.Operator_Not_Equal_Double(v2);
    }

    public static SValue operator !=(double v1, SValue v2)
    {
        return v2.Operator_Not_Equal_Double(v1);
    }

    public static SValue operator !=(SValue v1, string v2)
    {
        return v1.Operator_Not_Equal_String(v2);
    }

    public static SValue operator !=(string v1, SValue v2)
    {
        return v2.Operator_Not_Equal_String(v1);
    }

    public static SValue operator !=(SValue v1, SValue v2)
    {
        return v1.Operator_Not_Equal_SValue(v2);
    }

    public static SValue operator ++(SValue v1)
    {
        return v1.Operator_SinceAdd();
    }

    public static SValue operator --(SValue v1)
    {
        return v1.Operator_SinceReduction();
    }

    public static SValue operator >(SValue v1, double v2)
    {
        return v1.Operator_Greater_Double(v2);
    }

    public static SValue operator >(double v1, SValue v2)
    {
        return v2.Operator_Less_Equal_Double(v1);
    }

    public static SValue operator >(SValue v1, SValue v2)
    {
        return v1.Operator_Greater_SValue(v2);
    }

    public static SValue operator <(SValue v1, double v2)
    {
        return v1.Operator_Less_Double(v2);
    }

    public static SValue operator <(double v1, SValue v2)
    {
        return v2.Operator_Greater_Equal_Double(v1);
    }

    public static SValue operator <(SValue v1, SValue v2)
    {
        return v1.Operator_Less_SValue(v2);
    }

    public static SValue operator >=(SValue v1, double v2)
    {
        return v1.Operator_Greater_Equal_Double(v2);
    }

    public static SValue operator >=(double v1, SValue v2)
    {
        return v2.Operator_Less_Double(v1);
    }

    public static SValue operator >=(SValue v1, SValue v2)
    {
        return v1.Operator_Greater_Equal_SValue(v2);
    }

    public static SValue operator <=(SValue v1, double v2)
    {
        return v1.Operator_Less_Equal_Double(v2);
    }

    public static SValue operator <=(double v1, SValue v2)
    {
        return v2.Operator_Greater_Double(v1);
    }

    public static SValue operator <=(SValue v1, SValue v2)
    {
        return v1.Operator_Less_Equal_SValue(v2);
    }

    public static SValue operator +(SValue v1)
    {
        return v1.Operator_Positive();
    }

    public static SValue operator -(SValue v1)
    {
        return v1.Operator_Negative();
    }

    public static SValue operator !(SValue v1)
    {
        return v1.Operator_Not();
    }

    public static bool operator true(SValue v1)
    {
        return v1.Operator_True();
    }

    public static bool operator false(SValue v1)
    {
        return v1.Operator_False();
    }

    public static SValue operator %(SValue v1, double v2)
    {
        return v1.Operator_Modulus_Double(v2);
    }

    public static SValue operator %(double v1, SValue v2)
    {
        return v2.Operator_Append_Modulus_Double(v1);
    }

    public static SValue operator %(SValue v1, SValue v2)
    {
        return v1.Operator_Modulus_SValue(v2);
    }
    
    public static SValue operator ~(SValue v1)
    {
        return v1.Operator_Shift_Negation();
    }

    public static SValue operator >> (SValue v1, int v2)
    {
        return v1.Operator_Shift_Right(v2);
    }

    public static SValue operator <<(SValue v1, int v2)
    {
        return v1.Operator_Shift_Left(v2);
    }

    public static SValue operator |(SValue v1, double v2)
    {
        return v1.Operator_Shift_Or_Double(v2);
    }

    public static SValue operator |(double v1, SValue v2)
    {
        return v2.Operator_Append_Shift_Or_Double(v1);
    }

    public static SValue operator |(SValue v1, SValue v2)
    {
        return v1.Operator_Shift_Or_SValue(v2);
    }

    public static SValue operator &(SValue v1, double v2)
    {
        return v1.Operator_Shift_And_Double(v2);
    }

    public static SValue operator &(double v1, SValue v2)
    {
        return v2.Operator_Append_Shift_And_Double(v1);
    }

    public static SValue operator &(SValue v1, SValue v2)
    {
        return v1.Operator_Shift_And_SValue(v2);
    }

    public static SValue operator ^(SValue v1, double v2)
    {
        return v1.Operator_Shift_Xor_Double(v2);
    }

    public static SValue operator ^(double v1, SValue v2)
    {
        return v2.Operator_Append_Shift_Xor_Double(v1);
    }

    public static SValue operator ^(SValue v1, SValue v2)
    {
        return v1.Operator_Shift_Xor_SValue(v2);
    }

    #endregion

    /// <summary>
    /// true
    /// </summary>
    public static readonly SValue True = new True_SValue();

    /// <summary>
    /// false
    /// </summary>
    public static readonly SValue False = new False_SValue();

    /// <summary>
    /// 空值
    /// </summary>
    public static readonly SValue Null = new Null_SValue();

    /// <summary>
    /// NaN
    /// </summary>
    public static readonly SValue NaN = new Number_SValue(double.NaN);

    /// <summary>
    /// 负无穷大
    /// </summary>
    public static readonly SValue NegativeInfinity = new Number_SValue(double.NegativeInfinity);

    /// <summary>
    /// 正无穷大
    /// </summary>
    public static readonly SValue PositiveInfinity = new Number_SValue(double.PositiveInfinity);

    public static SValue Create(ref SValue value)
    {
        return value;
    }

    public static SValue Create(double value)
    {
        return new Number_SValue(value);
    }

    public static SValue Create(string value)
    {
        return new String_SValue(value);
    }

    public static SValue Create(bool value)
    {
        return value ? True : False;
    }

    public static SValue Create(IObject value)
    {
        return Null;
    }

    public static SValue Create(Function_0 value)
    {
        return new Function_0_SValue(value);
    }

    public static SValue Create(Function_1 value)
    {
        return new Function_1_SValue(value);
    }
}