
namespace DScript.Exception
{
    /// <summary>
    /// 函数执行异常
    /// </summary>
    public class InvokeMethodException : ScriptException
    {
        public InvokeMethodException(string message) : base(message)
        {
        }
    }
}