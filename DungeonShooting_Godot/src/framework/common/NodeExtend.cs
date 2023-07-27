using System;
using System.Collections;
using Godot;

/// <summary>
/// 该类为 node 节点通用扩展函数类
/// </summary>
public static class NodeExtend
{
    /// <summary>
    /// 尝试将一个 Node2d 节点转换成一个 ActivityObject 对象, 如果转换失败, 则返回 null
    /// </summary>
    public static ActivityObject AsActivityObject(this Node2D node2d)
    {
        if (node2d is ActivityObject p)
        {
            return p;
        }
        var parent = node2d.GetParent();
        if (parent != null && parent is ActivityObject p2)
        {
            return p2;
        }
        return null;
    }
    
    /// <summary>
    /// 尝试将一个 Node2d 节点转换成一个 ActivityObject 对象, 如果转换失败, 则返回 null
    /// </summary>
    public static T AsActivityObject<T>(this Node2D node2d) where T : ActivityObject
    {
        if (node2d is T p)
        {
            return p;
        }
        var parent = node2d.GetParent();
        if (parent != null && parent is T p2)
        {
            return p2;
        }
        return null;
    }

    /// <summary>
    /// 将节点插入的房间物体根节点
    /// </summary>
    /// <param name="node">实例</param>
    /// <param name="layer">放入的层</param>
    public static void AddToActivityRoot(this Node2D node, RoomLayerEnum layer)
    {
        GameApplication.Instance.World.GetRoomLayer(layer).AddChild(node);
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelay(this ICoroutine coroutine, float delayTime, Action cb)
    {
        coroutine.StartCoroutine(_CallDelay(delayTime, cb));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelay<T1>(this ICoroutine coroutine, float delayTime, Action<T1> cb, T1 arg1)
    {
        coroutine.StartCoroutine(_CallDelay(delayTime, cb, arg1));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelay<T1, T2>(this ICoroutine coroutine, float delayTime, Action<T1, T2> cb, T1 arg1, T2 arg2)
    {
        coroutine.StartCoroutine(_CallDelay(delayTime, cb, arg1, arg2));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelay<T1, T2, T3>(this ICoroutine coroutine, float delayTime, Action<T1, T2, T3> cb, T1 arg1, T2 arg2, T3 arg3)
    {
        coroutine.StartCoroutine(_CallDelay(delayTime, cb, arg1, arg2, arg3));
    }
    
    //---------------------------
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelayInNode(this Node node, float delayTime, Action cb)
    {
        GameApplication.Instance.StartCoroutine(_CallDelay(delayTime, cb));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelayInNode<T1>(this Node node, float delayTime, Action<T1> cb, T1 arg1)
    {
        GameApplication.Instance.StartCoroutine(_CallDelay(delayTime, cb, arg1));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelayInNode<T1, T2>(this Node node, float delayTime, Action<T1, T2> cb, T1 arg1, T2 arg2)
    {
        GameApplication.Instance.StartCoroutine(_CallDelay(delayTime, cb, arg1, arg2));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelayInNode<T1, T2, T3>(this Node node, float delayTime, Action<T1, T2, T3> cb, T1 arg1, T2 arg2, T3 arg3)
    {
        GameApplication.Instance.StartCoroutine(_CallDelay(delayTime, cb, arg1, arg2, arg3));
    }
    
    private static IEnumerator _CallDelay(float delayTime, Action cb)
    {
        yield return new WaitForSeconds(delayTime);
        cb();
    }
    
    private static IEnumerator _CallDelay<T1>(float delayTime, Action<T1> cb, T1 arg1)
    {
        yield return new WaitForSeconds(delayTime);
        cb(arg1);
    }
    
    private static IEnumerator _CallDelay<T1, T2>(float delayTime, Action<T1, T2> cb, T1 arg1, T2 arg2)
    {
        yield return new WaitForSeconds(delayTime);
        cb(arg1, arg2);
    }
    
    private static IEnumerator _CallDelay<T1, T2, T3>(float delayTime, Action<T1, T2, T3> cb, T1 arg1, T2 arg2, T3 arg3)
    {
        yield return new WaitForSeconds(delayTime);
        cb(arg1,arg2, arg3);
    }
}