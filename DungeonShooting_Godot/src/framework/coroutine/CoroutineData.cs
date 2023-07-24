
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

/// <summary>
/// 协程数据
/// </summary>
public class CoroutineData
{
    private static long _id = 1;
    
    public enum WaitTypeEnum
    {
        None,
        WaitForSeconds,
        WaitForFixedProcess,
        WaitForTask,
        WaitForSignalAwaiter,
    }

    /// <summary>
    /// 协程ID
    /// </summary>
    public readonly long Id;
    /// <summary>
    /// 当前协程等待状态
    /// </summary>
    public WaitTypeEnum WaitState = WaitTypeEnum.None;
    /// <summary>
    /// 协程迭代器
    /// </summary>
    public IEnumerator Enumerator;
    
    // ----------------------------------------------
    public Stack<IEnumerator> EnumeratorStack;
    public WaitForSeconds WaitForSeconds;
    public WaitForFixedProcess WaitForFixedProcess;
    public Task WaitTask;
    public SignalAwaiter WaitSignalAwaiter;

    public CoroutineData(IEnumerator enumerator)
    {
        Id = _id++;
        Enumerator = enumerator;
    }

    public void WaitFor(WaitForSeconds seconds)
    {
        WaitState = WaitTypeEnum.WaitForSeconds;
        WaitForSeconds = seconds;
    }
    
    public void WaitFor(WaitForFixedProcess process)
    {
        WaitState = WaitTypeEnum.WaitForFixedProcess;
        WaitForFixedProcess = process;
    }

    public void WaitFor(Task task)
    {
        WaitState = WaitTypeEnum.WaitForTask;
        WaitTask = task;
    }
    
    public void WaitFor(SignalAwaiter task)
    {
        WaitState = WaitTypeEnum.WaitForSignalAwaiter;
        WaitSignalAwaiter = task;
    }
}