
public interface OldIObject
{
    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <param name="key">属性名称</param>
    OldSValue __GetValue(string key);

    /// <summary>
    /// 设置属性值
    /// </summary>
    /// <param name="key">属性名称</param>
    /// <param name="value">值</param>
    void __SetValue(string key, OldSValue value);
    
    /// <summary>
    /// 执行成员函数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <param name="ps">传入的参数</param>
    /// <returns>函数返回值</returns>
    OldSValue __InvokeMethod(string key, params OldSValue[] ps);
}