using System;

public static class ExecuteTime
{
    public static void Run(string message, Action fun)
    {
        var time1 = DateTime.Now.Ticks;
        fun();
        var time2 = DateTime.Now.Ticks;
        UnitTest.Console.WriteLine(message + ", 执行时间: " + (time2 - time1) / 10000f + "毫秒");
    }
}