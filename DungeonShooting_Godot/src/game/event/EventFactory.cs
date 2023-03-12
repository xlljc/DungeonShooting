
using System;
using System.Collections.Generic;

/// <summary>
/// 事件工厂, 用于统一绑定事件与销毁的情况
/// </summary>
public class EventFactory
{
    private List<EventBinder> _binders = new List<EventBinder>();

    /// <summary>
    /// 添加监听事件
    /// </summary>
    public void AddEventListener(EventEnum eventType, Action<object> callback)
    {
        _binders.Add(EventManager.AddEventListener(eventType, callback));
    }

    /// <summary>
    /// 清理所有监听事件
    /// </summary>
    public void Clear()
    {
        foreach (var eventBinder in _binders)
        {
            EventManager.RemoveEventListener(eventBinder);
        }
        _binders.Clear();
    }
}