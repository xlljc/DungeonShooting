
using System;

/// <summary>
/// 事件绑定对象
/// </summary>
public class EventBinder
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public readonly EventEnum EventType;
    /// <summary>
    /// 事件回调函数
    /// </summary>
    public readonly Action<object> Callback;
    /// <summary>
    /// 该监听事件是否被移除
    /// </summary>
    public bool IsDiscard;
    
    public EventBinder(EventEnum eventType, Action<object> callback)
    {
        EventType = eventType;
        Callback = callback;
    }

    /// <summary>
    /// 移除当前监听事件
    /// </summary>
    public void RemoveEventListener()
    {
        EventManager.RemoveEventListener(this);
    }
}