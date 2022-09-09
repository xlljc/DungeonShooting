
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
    ISValue SinceAdd()
    {
        return NaN;
    }

    /// <summary>
    /// 自减
    /// </summary>
    ISValue SinceReduction()
    {
        return NaN;
    }
    
    #endregion

    #region 外部调用的运算符重载

    public static ISValue operator ++(ISValue v1)
    {
        return v1.SinceAdd();
    }
    
    public static ISValue operator --(ISValue v1)
    {
        return v1.SinceReduction();
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