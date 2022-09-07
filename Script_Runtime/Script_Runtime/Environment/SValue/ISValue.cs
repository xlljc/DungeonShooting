
/// <summary>
/// 脚本中的数据描述接口
/// </summary>
public interface ISValue
{
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

    /// <summary>
    /// 把自己当成函数执行
    /// </summary>
    ISValue Invoke(params ISValue[] ps);

    /// <summary>
    /// 执行该对象的成员函数
    /// </summary>
    ISValue InvokeMethod(string key, params ISValue[] ps);

    /// <summary>
    /// 获取成员属性
    /// </summary>
    ISValue GetProperty(string key);

    /// <summary>
    /// 设置成员属性
    /// </summary>
    void SetProperty(string key, ISValue value);

    // public static SValue operator +(ISValue v1, double num)
    // {
    //
    // }

    public static ISValue Null => NullSValue.Instance;

    public static ISValue Create(ISValue value)
    {
        return value;
    }
    
    public static ISValue Create(double value)
    {
        return new Number_SValue(value);
    }
    
    public static ISValue Create(IObject value)
    {
        return Null;
    }

    public static ISValue Create(Function_0 value)
    {
        return new Function_0_SValue(value);
    }
}