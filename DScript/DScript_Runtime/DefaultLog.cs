using System;

namespace DScript
{
    /// <summary>
    /// log输出处理类默认实现
    /// </summary>
    internal class DefaultLog : ILog
    {
        public void Log(params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(ToLogStr(args) + "\n");
        }

        public void Warn(params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(ToLogStr(args) + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Error(params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(ToLogStr(args) + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        private string ToLogStr(params object[] args)
        {
            string str = "";
            if (args != null && args.Length > 0)
            {
                foreach (var item in args)
                {
                    str += item + " ";
                }
            }
            return str;
        }
    }
}