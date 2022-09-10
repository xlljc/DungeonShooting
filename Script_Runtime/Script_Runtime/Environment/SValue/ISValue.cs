
/// <summary>
/// 脚本中的数据描述接口
/// </summary>
public interface ISValue
{
    #region 基础函数

    /// <summary>
    /// 获取该类在脚本中的数据类型
    /// </summary>
    SValueType GetValueType();

    /// <summary>
    /// 获取该类存放的数据类型
    /// </summary>
    SDataType GetDataType();

    /// <summary>
    /// 获取数据对象
    /// </summary>
    object GetValue();

    #endregion

    #region 默认实现函数

    /// <summary>
    /// 获取成员属性
    /// </summary>
    ISValue GetMember(string key)
    {
        return Null;
    }

    /// <summary>
    /// 返回是否存在属性
    /// </summary>
    ISValue HasMember(string key)
    {
        return False;
    }

    /// <summary>
    /// 设置成员属性
    /// </summary>
    void SetMember(string key, ISValue value)
    {
        throw new OperationMemberException($"Member '{key}' not defined.");
    }

    /// <summary>
    /// 把自己当成函数执行
    /// </summary>
    public ISValue Invoke()
    {
        throw new InvokeMethodException("Instance is not a function.");
    }
    
    /// <summary>
    /// 把自己当成函数执行
    /// </summary>
    public ISValue Invoke(ISValue v0)
    {
        throw new InvokeMethodException("Instance is not a function.");
    }

    /// <summary>
    /// 执行该对象的成员函数
    /// </summary>
    ISValue InvokeMethod(string key)
    {
        throw new InvokeMethodException("No overloaded member function was found.");
    }
    
    /// <summary>
    /// 执行该对象的成员函数
    /// </summary>
    ISValue InvokeMethod(string key, ISValue v0)
    {
        throw new InvokeMethodException("No overloaded member function was found.");
    }

    #endregion

    #region 内部执行的重载处理

    /// <summary>
    /// 自加
    /// </summary>
    ISValue Operator_SinceAdd()
    {
        return NaN;
    }

    /// <summary>
    /// 自减
    /// </summary>
    ISValue Operator_SinceReduction()
    {
        return NaN;
    }
    
    /// <summary>
    /// 大于
    /// </summary>
    ISValue Operator_Greater_ISValue_Double(double v2)
    {
        return False;
    }
    
    /// <summary>
    /// 小于
    /// </summary>
    ISValue Operator_Less_ISValue_Double(double v2)
    {
        return False;
    }    
    /// <summary>
    /// 大于
    /// </summary>
    ISValue Operator_Greater_ISValue_ISValue(ISValue v2)
    {
        return False;
    }
    
    /// <summary>
    /// 小于
    /// </summary>
    ISValue Operator_Less_ISValue_ISValue(ISValue v2)
    {
        return False;
    }
    
    //
    /// <summary>
    /// 大于等于
    /// </summary>
    ISValue Operator_Greater_Equal_ISValue_Double(double v2)
    {
        return False;
    }
    
    /// <summary>
    /// 小于等于
    /// </summary>
    ISValue Operator_Less_Equal_ISValue_Double(double v2)
    {
        return False;
    }    
    /// <summary>
    /// 大于等于
    /// </summary>
    ISValue Operator_Greater_Equal_ISValue_ISValue(ISValue v2)
    {
        return False;
    }
    
    /// <summary>
    /// 正数
    /// </summary>
    ISValue Operator_Positive()
    {
        return this;
    }
    
    /// <summary>
    /// 负数
    /// </summary>
    ISValue Operator_Negative()
    {
        return NaN;
    }
    
    /// <summary>
    /// 取反
    /// </summary>
    ISValue Operator_Not()
    {
        return True;
    }
    
    /// <summary>
    /// 小于等于
    /// </summary>
    ISValue Operator_Less_Equal_ISValue_ISValue(ISValue v2)
    {
        return False;
    }
    //

    /// <summary>
    /// 判断true
    /// </summary>
    bool Operator_True()
    {
        return true;
    }
    
    /// <summary>
    /// 判断false
    /// </summary>
    bool Operator_False()
    {
        return false;
    }
    
    #endregion

    #region 外部调用的运算符重载

    public static ISValue operator ++(ISValue v1)
    {
        return v1.Operator_SinceAdd();
    }
    
    public static ISValue operator --(ISValue v1)
    {
        return v1.Operator_SinceReduction();
    }

    public static ISValue operator >(ISValue v1, double v2)
    {
        return v1.Operator_Greater_ISValue_Double(v2);
    }
    
    public static ISValue operator >(double v1, ISValue v2)
    {
        return v2.Operator_Less_Equal_ISValue_Double(v1);
    }

    public static ISValue operator >(ISValue v1, ISValue v2)
    {
        return v1.Operator_Greater_ISValue_ISValue(v2);
    }
    
    public static ISValue operator <(ISValue v1, double v2)
    {
        return v1.Operator_Less_ISValue_Double(v2);
    }
    
    public static ISValue operator <(double v1, ISValue v2)
    {
        return v2.Operator_Greater_Equal_ISValue_Double(v1);
    }
    
    public static ISValue operator <(ISValue v1, ISValue v2)
    {
        return v1.Operator_Less_ISValue_ISValue(v2);
    }

    //
    public static ISValue operator >=(ISValue v1, double v2)
    {
        return v1.Operator_Greater_Equal_ISValue_Double(v2);
    }
    
    public static ISValue operator >=(double v1, ISValue v2)
    {
        return v2.Operator_Less_ISValue_Double(v1);
    }

    public static ISValue operator >=(ISValue v1, ISValue v2)
    {
        return v1.Operator_Greater_Equal_ISValue_ISValue(v2);
    }
    
    public static ISValue operator <=(ISValue v1, double v2)
    {
        return v1.Operator_Less_Equal_ISValue_Double(v2);
    }
    
    public static ISValue operator <=(double v1, ISValue v2)
    {
        return v2.Operator_Greater_ISValue_Double(v1);
    }
    
    public static ISValue operator <=(ISValue v1, ISValue v2)
    {
        return v1.Operator_Less_Equal_ISValue_ISValue(v2);
    }
    //
    
    public static ISValue operator +(ISValue v1)
    {
        return v1.Operator_Positive();
    }
    
    public static ISValue operator -(ISValue v1)
    {
        return v1.Operator_Negative();
    }
    
    public static ISValue operator !(ISValue v1)
    {
        return v1.Operator_Not();
    }
    
    public static bool operator true(ISValue v1)
    {
        return v1.Operator_True();
    }

    public static bool operator false(ISValue v1)
    {
        return v1.Operator_False();
    }
    
    #endregion
    
    #region 创建ISValue
    
    /// <summary>
    /// true
    /// </summary>
    public static readonly ISValue True = new True_SValue();
    /// <summary>
    /// false
    /// </summary>
    public static readonly ISValue False = new False_SValue();
    /// <summary>
    /// 空值
    /// </summary>
    public static readonly ISValue Null = new Null_SValue();
    /// <summary>
    /// NaN
    /// </summary>
    public static readonly ISValue NaN = new Number_SValue(double.NaN);
    /// <summary>
    /// 负无穷大
    /// </summary>
    public static readonly ISValue NegativeInfinity = new Number_SValue(double.NegativeInfinity);
    /// <summary>
    /// 正无穷大
    /// </summary>
    public static readonly ISValue PositiveInfinity = new Number_SValue(double.PositiveInfinity);

    public static ISValue Create(ref ISValue value)
    {
        return value;
    }
    
    public static ISValue Create(double value)
    {
        return new Number_SValue(value);
    }
    
    public static ISValue Create(string value)
    {
        return new String_SValue(value);
    }
    
    public static ISValue Create(bool value)
    {
        return value ? True : False;
    }
    
    public static ISValue Create(IObject value)
    {
        return Null;
    }

    public static ISValue Create(Function_0 value)
    {
        return new Function_0_SValue(value);
    }
    
    public static ISValue Create(Function_1 value)
    {
        return new Function_1_SValue(value);
    }
    
    #endregion
}