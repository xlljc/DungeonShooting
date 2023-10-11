
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 事件管理器
/// </summary>
public static class EventManager
{

    private static readonly Dictionary<EventEnum, List<EventBinder>> _eventMap =
        new Dictionary<EventEnum, List<EventBinder>>();

    /// <summary>
    /// 添加监听事件, 并返回事件绑定对象
    /// </summary>
    /// <param name="eventType">监听事件类型</param>
    /// <param name="callback">回调函数</param>
    public static EventBinder AddEventListener(EventEnum eventType, Action<object> callback)
    {
        EventBinder binder;
        if (!_eventMap.TryGetValue(eventType, out var list))
        {
            list = new List<EventBinder>();
            _eventMap.Add(eventType, list);
            binder = new EventBinder(eventType, callback);
            list.Add(binder);
            return binder;
        }

        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];
            if (item.Callback == callback)
            {
                return item;
            }
        }

        binder = new EventBinder(eventType, callback);
        list.Add(binder);
        return binder;
    }

    /// <summary>
    /// 派发事件
    /// </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="arg">传递参数</param>
    public static void EmitEvent(EventEnum eventType, object arg = null)
    {
        if (_eventMap.TryGetValue(eventType, out var list))
        {
            var binders = list.ToArray();
            for (var i = 0; i < binders.Length; i++)
            {
                var binder = binders[i];
                if (!binder.IsDiscard)
                {
                    try
                    {
                        binder.Callback(arg);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"EventManager 派发事件: '{eventType}' 发生异常: \n" + e.Message + "\n" + e.StackTrace);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 根据事件绑定对象移除一个监听事件
    /// </summary>
    public static void RemoveEventListener(EventBinder binder)
    {
        if (_eventMap.TryGetValue(binder.EventType, out var list))
        {
            if (list.Remove(binder))
            {
                binder.IsDiscard = true;
                if (list.Count == 0)
                {
                    _eventMap.Remove(binder.EventType);
                }
            }
        }
    }

    /// <summary>
    /// 移除指定类型的所有事件
    /// </summary>
    public static void RemoveAllEventListener(EventEnum eventType)
    {
        if (_eventMap.TryGetValue(eventType, out var list))
        {
            foreach (var binder in list)
            {
                binder.IsDiscard = true;
            }

            _eventMap.Remove(eventType);
        }
    }

    /// <summary>
    /// 移除所有监听事件
    /// </summary>
    public static void ClearAllEventListener()
    {
        foreach (var kv in _eventMap)
        {
            foreach (var binder in kv.Value)
            {
                binder.IsDiscard = true;
            }
        }

        _eventMap.Clear();
    }

    /// <summary>
    /// 创建一个事件工厂
    /// </summary>
    public static EventFactory CreateEventFactory()
    {
        return new EventFactory();
    }
}