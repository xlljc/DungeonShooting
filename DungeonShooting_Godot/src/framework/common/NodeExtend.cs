using System;
using System.Collections;
using Godot;

/// <summary>
/// 该类为 node 节点通用扩展函数类
/// </summary>
public static class NodeExtend
{
    // /// <summary>
    // /// 尝试将一个 Node2d 节点转换成一个 ActivityObject 对象, 如果转换失败, 则返回 null
    // /// </summary>
    // public static ActivityObject AsActivityObject(this Node2D node2d)
    // {
    //     if (node2d is ActivityObject p)
    //     {
    //         return p;
    //     }
    //     var parent = node2d.GetParent();
    //     if (parent != null && parent is ActivityObject p2)
    //     {
    //         return p2;
    //     }
    //     return null;
    // }
    //
    // /// <summary>
    // /// 尝试将一个 Node2d 节点转换成一个 ActivityObject 对象, 如果转换失败, 则返回 null
    // /// </summary>
    // public static T AsActivityObject<T>(this Node2D node2d) where T : ActivityObject
    // {
    //     if (node2d is T p)
    //     {
    //         return p;
    //     }
    //     var parent = node2d.GetParent();
    //     if (parent != null && parent is T p2)
    //     {
    //         return p2;
    //     }
    //     return null;
    // }

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
    /// 将节点插入的房间物体根节点，延时调用
    /// </summary>
    /// <param name="node">实例</param>
    /// <param name="layer">放入的层</param>
    public static void AddToActivityRootDeferred(this Node2D node, RoomLayerEnum layer)
    {
        GameApplication.Instance.World.GetRoomLayer(layer).CallDeferred(Node.MethodName.AddChild, node);
    }

    /// <summary>
    /// 设置Ui布局方式是否横向扩展, 如果为 true, 则 GridContainer 的宽度会撑满父物体
    /// </summary>
    public static void SetHorizontalExpand(this Control control, bool flag)
    {
        if (flag)
        {
            control.SizeFlagsHorizontal |= Control.SizeFlags.Expand;
        }
        else if ((control.SizeFlagsHorizontal & Control.SizeFlags.Expand) != 0)
        {
            control.SizeFlagsHorizontal ^= Control.SizeFlags.Expand;
        }
    }

    /// <summary>
    /// 获取Ui布局方式是否横向扩展
    /// </summary>
    public static bool GetHorizontalExpand(this Control control)
    {
        return (control.SizeFlagsHorizontal & Control.SizeFlags.Expand) != 0;
    }
    
    /// <summary>
    /// 返回鼠标是否在Ui矩形内
    /// </summary>
    public static bool IsMouseInRect(this Control control, float border = 0)
    {
        var pos = control.GetLocalMousePosition();
        if (pos.X < border || pos.Y < border)
        {
            return false;
        }

        var size = control.Size;
        return pos.X <= size.X - border && pos.Y <= size.Y - border;
    }

    /// <summary>
    /// 设置是否启用节点
    /// </summary>
    public static void SetActive(this Node node, bool value)
    {
        if (node is CanvasItem canvasItem)
        {
            canvasItem.Visible = value;
        }
        node.SetProcess(value);
        node.SetPhysicsProcess(value);
        node.SetProcessInput(value);
        node.SetPhysicsProcessInternal(value);
        node.SetProcessInput(value);
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

    /// <summary>
    /// 给Ui节点添加拖拽事件
    /// </summary>
    /// <param name="control">需要绑定事件的节点对象</param>
    /// <param name="callback">拖拽回调函数</param>
    public static UiEventBinder AddDragListener(this Control control, Action<DragState, Vector2> callback)
    {
        return AddDragListener(control, DragButtonEnum.Left, callback);
    }

    /// <summary>
    /// 给Ui节点添加拖拽事件
    /// </summary>
    /// <param name="control">需要绑定拖拽的节点对象</param>
    /// <param name="triggerButton">可触发拖拽的按钮</param>
    /// <param name="callback">拖拽回调函数</param>
    public static UiEventBinder AddDragListener(this Control control, DragButtonEnum triggerButton, Action<DragState, Vector2> callback)
    {
        var dragFlag = false;
        Control.GuiInputEventHandler handler = (ev) =>
        {
            if (!dragFlag) //未开始拖拽
            {
                if (ev is InputEventMouseButton mouseButton && mouseButton.Pressed &&
                    CheckDragButton(mouseButton.ButtonIndex, triggerButton)) //按下按钮
                {
                    control.AcceptEvent();
                    dragFlag = true;
                    callback(DragState.DragStart, Vector2.Zero);
                }
            }
            else //拖拽中
            {
                if (ev is InputEventMouseButton mouseButton)
                {
                    if (!mouseButton.Pressed && CheckDragButton(mouseButton.ButtonIndex, triggerButton)) //松开按钮
                    {
                        control.AcceptEvent();
                        dragFlag = false;
                        callback(DragState.DragEnd, Vector2.Zero);
                    }
                } else if (ev is InputEventMouseMotion mouseMotion) //拖拽中
                {
                    control.AcceptEvent();
                    var delta = mouseMotion.Relative;
                    if (delta != Vector2.Zero)
                    {
                        callback(DragState.DragMove, mouseMotion.Relative);
                    }
                }
            }
        };
        control.GuiInput += handler;
        return new UiEventBinder(control, handler);
    }

    private static bool CheckDragButton(MouseButton button, DragButtonEnum triggerButton)
    {
        DragButtonEnum buttonEnum;
        switch (button)
        {
            case MouseButton.Left:
                buttonEnum = DragButtonEnum.Left;
                break;
            case MouseButton.Right:
                buttonEnum = DragButtonEnum.Right;
                break;
            case MouseButton.Middle:
                buttonEnum = DragButtonEnum.Middle;
                break;
            default: return false;
        }

        return (buttonEnum & triggerButton) != 0;
    }

    /// <summary>
    /// 给Ui节点添加鼠标滚轮事件
    /// </summary>
    /// <param name="control">需要绑定事件的节点对象</param>
    /// <param name="callback">滚轮回调, 参数 -1 表示滚轮向下滚动, 1 表示滚轮向上滚动</param>
    public static UiEventBinder AddMouseWheelListener(this Control control, Action<int> callback)
    {
        Control.GuiInputEventHandler handler = (ev) =>
        {
            if (ev is InputEventMouseButton mouseButton)
            {
                if (mouseButton.ButtonIndex == MouseButton.WheelDown)
                {
                    control.AcceptEvent();
                    callback(-1);
                }
                else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
                {
                    control.AcceptEvent();
                    callback(1);
                }
            }
        };
        control.GuiInput += handler;
        return new UiEventBinder(control, handler);
    }
}