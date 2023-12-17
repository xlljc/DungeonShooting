
using Godot;

/// <summary>
/// 调试日志输出
/// </summary>
public static class Debug
{
    /// <summary>
    /// 所有日志信息,包括log和error
    /// </summary>
    public static string AllLogMessage { get; private set; } = "";
    
    /// <summary>
    /// 输出日志
    /// </summary>
    public static void Log(params object[] message)
    {
        var str = "[log]";
        foreach (var m in message)
        {
            if (m is null)
            {
                str += "null";
            }
            else
            {
                str += m;
            }
        }

        GD.Print(str);
        AllLogMessage = str + "\n" + AllLogMessage;
        if (AllLogMessage.Length > 10000)
        {
            AllLogMessage = AllLogMessage.Substring(0, 9500);
        }
    }
    
    /// <summary>
    /// 输出错误日志
    /// </summary>
    public static void LogError(params object[] message)
    {
        var str = "[error]";
        foreach (var m in message)
        {
            if (m is null)
            {
                str += "null";
            }
            else
            {
                str += m;
            }
        }

        GD.PrintErr(str);
        AllLogMessage = str + "\n" + AllLogMessage;
        if (AllLogMessage.Length > 10000)
        {
            AllLogMessage = AllLogMessage.Substring(0, 9500);
        }
    }

    /// <summary>
    /// 清除log
    /// </summary>
    public static void Clear()
    {
        AllLogMessage = "";
    }
}