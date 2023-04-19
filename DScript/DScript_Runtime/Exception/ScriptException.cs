

namespace DScript.Exception
{
    using System;
    
    /// <summary>
    /// 脚本异常基类
    /// </summary>
    public abstract class ScriptException : Exception
    {
        public ScriptException(string message) : base(message)
        {

        }
    }
}