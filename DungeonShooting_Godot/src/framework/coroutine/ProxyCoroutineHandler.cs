
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

/// <summary>
/// 协程代理类
/// </summary>
public static class ProxyCoroutineHandler
{
    /// <summary>
    /// 代理更新协程
    /// </summary>
    public static void ProxyUpdateCoroutine(ref List<CoroutineData> coroutineList, float delta)
    {
        if (coroutineList == null || coroutineList.Count == 0)
        {
            return;
        }
        var pairs = coroutineList.ToArray();
        for (var i = 0; i < pairs.Length; i++)
        {
            var item = pairs[i];
            // try
            // {
                var canNext = true;

                if (item.WaitState == CoroutineData.WaitTypeEnum.WaitForSeconds) //等待秒数
                {
                    if (!item.WaitForSeconds.NextStep(delta))
                    {
                        canNext = false;
                    }
                    else
                    {
                        item.WaitState = CoroutineData.WaitTypeEnum.None;
                        item.WaitForSeconds = null;
                    }
                }
                else if (item.WaitState == CoroutineData.WaitTypeEnum.WaitForFixedProcess) //等待帧数
                {
                    if (!item.WaitForFixedProcess.NextStep())
                    {
                        canNext = false;
                    }
                    else
                    {
                        item.WaitState = CoroutineData.WaitTypeEnum.None;
                        item.WaitForFixedProcess = null;
                    }
                }
                else if (item.WaitState == CoroutineData.WaitTypeEnum.WaitForTask) //等待Task
                {
                    if (!item.WaitTask.IsCompleted)
                    {
                        canNext = false;
                    }
                    else
                    {
                        item.WaitState = CoroutineData.WaitTypeEnum.None;
                        item.WaitTask = null;
                    }
                }
                else if (item.WaitState == CoroutineData.WaitTypeEnum.WaitForSignalAwaiter) //等待信号
                {
                    if (!item.WaitSignalAwaiter.IsCompleted)
                    {
                        canNext = false;
                    }
                    else
                    {
                        item.WaitState = CoroutineData.WaitTypeEnum.None;
                        item.WaitSignalAwaiter = null;
                    }
                }

                if (canNext)
                {
                    if (item.Enumerator.MoveNext())
                    {
                        var next = item.Enumerator.Current;
                        if (next is IEnumerator enumerator) //嵌套协程
                        {
                            if (item.EnumeratorStack == null)
                            {
                                item.EnumeratorStack = new Stack<IEnumerator>();
                            }

                            item.EnumeratorStack.Push(item.Enumerator);
                            item.Enumerator = enumerator;
                        }
                        else if (next is IEnumerable enumerable) //嵌套协程
                        {
                            if (item.EnumeratorStack == null)
                            {
                                item.EnumeratorStack = new Stack<IEnumerator>();
                            }

                            item.EnumeratorStack.Push(item.Enumerator);
                            item.Enumerator = enumerable.GetEnumerator();
                        }
                        else if (next is WaitForSeconds seconds) //等待秒数
                        {
                            item.WaitFor(seconds);
                        }
                        else if (next is WaitForFixedProcess process) //等待帧数
                        {
                            item.WaitFor(process);
                        }
                        else if (next is Task task) //Task对象
                        {
                            item.WaitFor(task);
                        }
                        else if (next is SignalAwaiter awaiter) //等待信号
                        {
                            item.WaitFor(awaiter);
                        }
                    }
                    else
                    {
                        if (item.EnumeratorStack == null || item.EnumeratorStack.Count == 0)
                        {
                            ProxyStopCoroutine(ref coroutineList, item.Id);
                        }
                        else
                        {
                            item.Enumerator = item.EnumeratorStack.Pop();
                        }
                    }
                }
            // }
            // catch (Exception e)
            // {
            //     Debug.LogError("执行协程发生异常: \n" + e);
            //     ProxyStopCoroutine(ref coroutineList, item.Id);
            // }
        }
    }
    
    /// <summary>
    /// 代理协程, 开启一个协程, 返回协程 id, 协程是在普通帧执行的, 支持: 协程嵌套, WaitForSeconds, WaitForFixedProcess, Task, SignalAwaiter
    /// </summary>
    public static long ProxyStartCoroutine(ref List<CoroutineData> coroutineList, IEnumerator able)
    {
        if (coroutineList == null)
        {
            coroutineList = new List<CoroutineData>();
        }

        var data = new CoroutineData(able);
        coroutineList.Add(data);
        return data.Id;
    }

    /// <summary>
    /// 代理协程, 根据协程 id 停止协程
    /// </summary>
    public static void ProxyStopCoroutine(ref List<CoroutineData> coroutineList, long coroutineId)
    {
        if (coroutineList != null)
        {
            for (var i = 0; i < coroutineList.Count; i++)
            {
                var item = coroutineList[i];
                if (item.Id == coroutineId)
                {
                    coroutineList.RemoveAt(i);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 代理协程, 根据 id 返回指定协程是否结束
    /// </summary>
    public static bool ProxyIsCoroutineOver(ref List<CoroutineData> coroutineList, long coroutineId)
    {
        if (coroutineList != null)
        {
            for (var i = 0; i < coroutineList.Count; i++)
            {
                var item = coroutineList[i];
                if (item.Id == coroutineId)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    /// <summary>
    /// 代理协程, 停止所有协程
    /// </summary>
    public static void ProxyStopAllCoroutine(ref List<CoroutineData> coroutineList)
    {
        if (coroutineList != null)
        {
            coroutineList.Clear();
        }
    }
    
}