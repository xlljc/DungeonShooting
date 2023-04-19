using DScript;
using Xunit.Abstractions;

public class UnitTest
{
    public static ITestOutputHelper Console;
    
    public UnitTest(ITestOutputHelper helper)
    {
        SetOutputHelper(helper);
    }

    private static void SetOutputHelper(ITestOutputHelper helper)
    {
        if (Console == null)
        {
            LogUtils.SetLogHandler(new UnitTestLog(helper));
        }
        Console = helper;
    }

    private class UnitTestLog : ILog
    {
        private ITestOutputHelper _helper;
        public UnitTestLog(ITestOutputHelper helper)
        {
            _helper = helper;
        }
        public void Log(params object[] args)
        {
            _helper.WriteLine("[log]: " + ToLogStr(args));
        }

        public void Warn(params object[] args)
        {
            _helper.WriteLine("[warn]: " + ToLogStr(args));
        }

        public void Error(params object[] args)
        {
            _helper.WriteLine("[error]: " + ToLogStr(args));
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