
using System.Collections;

/// <summary>
/// 协程基础功能接口
/// </summary>
public interface ICoroutine
{
    /// <summary>
    /// 开启一个协程, 返回协程 id, 协程是在普通帧执行的, 支持: 协程嵌套, WaitForSeconds, WaitForFixedProcess, Task, SignalAwaiter
    /// </summary>
    public long StartCoroutine(IEnumerator able);

    /// <summary>
    /// 根据协程 id 停止协程
    /// </summary>
    public void StopCoroutine(long coroutineId);

    /// <summary>
    /// 根据协程 id 判断该协程是否结束
    /// </summary>
    public bool IsCoroutineOver(long coroutineId);
    
    /// <summary>
    /// 停止所有协程
    /// </summary>
    public void StopAllCoroutine();
}