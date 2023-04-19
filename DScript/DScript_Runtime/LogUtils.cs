namespace DScript
{
    /// <summary>
    /// log工具类
    /// </summary>
    public static class LogUtils
    {
        private static ILog _handler = new DefaultLog();

        /// <summary>
        /// 设置自定义log处理对象
        /// </summary>
        public static void SetLogHandler(ILog log)
        {
            _handler = log;
        }
        
        public static void Log(params object[] args)
        {
            _handler?.Log(args);
        }

        public static void Warn(params object[] args)
        {
            _handler?.Warn(args);
        }

        public static void Error(params object[] args)
        {
            _handler?.Error(args);
        }
    }
}