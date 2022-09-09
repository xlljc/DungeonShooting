
public interface IFunction_SValue : ISValue
{
    /// <summary>
    /// 把自己当成函数执行
    /// </summary>
    public new ISValue Invoke()
    {
        throw new InvokeMethodException("The function does not support 0 argument!");
    }
    
    /// <summary>
    /// 把自己当成函数执行
    /// </summary>
    public new ISValue Invoke(ISValue v0)
    {
        throw new InvokeMethodException("The function does not support 1 argument!");
    }
}