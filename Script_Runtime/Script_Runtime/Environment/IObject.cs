
using DScript.Runtime;

public interface IObject
{
    /// <summary>
    /// 获取成员值
    /// </summary>
    /// <param name="key">成员名称</param>
    SValue __GetMember(string key);

    /// <summary>
    /// 设置成员值
    /// </summary>
    /// <param name="key">成员名称</param>
    /// <param name="value">值</param>
    void __SetMember(string key, SValue value);

    /// <summary>
    /// 返回是否存在成员
    /// </summary>
    /// <param name="key">成员名称</param>
    bool __HasMember(string key);

    /// <summary>
    /// 执行成员函数, 传入0个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key);

    /// <summary>
    /// 执行成员函数, 传入1个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0);

    /// <summary>
    /// 执行成员函数, 传入2个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1);

    /// <summary>
    /// 执行成员函数, 传入3个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2);

    /// <summary>
    /// 执行成员函数, 传入4个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3);

    /// <summary>
    /// 执行成员函数, 传入5个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4);

    /// <summary>
    /// 执行成员函数, 传入6个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5);

    /// <summary>
    /// 执行成员函数, 传入7个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6);

    /// <summary>
    /// 执行成员函数, 传入8个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7);

    /// <summary>
    /// 执行成员函数, 传入9个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8);

    /// <summary>
    /// 执行成员函数, 传入10个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8, SValue v9);

    /// <summary>
    /// 执行成员函数, 传入11个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8, SValue v9, SValue v10);

    /// <summary>
    /// 执行成员函数, 传入12个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8, SValue v9, SValue v10, SValue v11);

    /// <summary>
    /// 执行成员函数, 传入13个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12);

    /// <summary>
    /// 执行成员函数, 传入14个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13);

    /// <summary>
    /// 执行成员函数, 传入15个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14);

    /// <summary>
    /// 执行成员函数, 传入16个参数
    /// </summary>
    /// <param name="key">函数名称</param>
    /// <returns>函数返回值</returns>
    SValue __InvokeMethod(string key, SValue v0, SValue v1, SValue v2, SValue v3, SValue v4, SValue v5, SValue v6,
        SValue v7, SValue v8, SValue v9, SValue v10, SValue v11, SValue v12, SValue v13, SValue v14, SValue v15);
}