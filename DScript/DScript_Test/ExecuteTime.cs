using System;
using DScript;
using DScript.Runtime;

public static class ExecuteTime
{

    private static bool _init = false;
    
    public static void Run(string message, Action fun)
    {
        if (!_init)
        {
            _init = true;
            long time = DateTime.Now.Ticks;
            while (true)
            {
                if ((DateTime.Now.Ticks - time) >= 30000000)
                {
                    break;
                }
            }
        }
        var time1 = DateTime.Now.Ticks;
        fun();
        var time2 = DateTime.Now.Ticks;
        LogUtils.Log(message + ", 执行时间: " + (time2 - time1) / 10000f + "毫秒");
    }
}