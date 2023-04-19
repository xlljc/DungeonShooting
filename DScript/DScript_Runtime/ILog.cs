namespace DScript
{
    /// <summary>
    /// 日志输出接口
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 输出普通日志
        /// </summary>
        void Log(params object[] args);
        /// <summary>
        /// 输出警告日志
        /// </summary>
        void Warn(params object[] args);
        /// <summary>
        /// 输出错误日志
        /// </summary>
        void Error(params object[] args);
    }
}