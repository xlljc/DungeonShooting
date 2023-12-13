using System;
using System.Collections.Generic;
using Godot;

public static class DragUiManager
{
    private static readonly List<DragBinder> _list = new List<DragBinder>();
    private static readonly List<DragBinder> _removeList = new List<DragBinder>();
    private static readonly List<DragBinder> _addList = new List<DragBinder>();
    
    /// <summary>
    /// 绑定拖拽事件
    /// </summary>
    public static DragBinder BindDrag(Control control, Action<DragState, Vector2> callback)
    {
        var binder = new DragBinder();
        binder.Control = control;
        control.MouseEntered += binder.OnMouseEntered;
        control.MouseExited += binder.OnMouseExited;
        binder.Callback = callback;
        _addList.Add(binder);
        return binder;
    }

    /// <summary>
    /// 解绑拖拽事件
    /// </summary>
    public static void UnBindDrag(DragBinder binder)
    {
        if (!_removeList.Contains(binder))
        {
            _removeList.Add(binder);
        }
    }

    public static void Update(float delta)
    {
        //更新拖拽位置
        if (_list.Count > 0)
        {
            foreach (var dragBinder in _list)
            {
                if (dragBinder.Dragging && !Input.IsActionPressed("mouse_left")) //松开鼠标, 结束拖拽
                {
                    dragBinder.Dragging = false;
                    dragBinder.Callback(DragState.DragEnd, Vector2.Zero);
                }
                else if (!dragBinder.Dragging) //开始拖拽
                {
                    if (dragBinder.MouseEntered && Input.IsActionJustPressed("mouse_left"))
                    {
                        dragBinder.Dragging = true;
                        dragBinder.PrevPosition = dragBinder.Control.GetGlobalMousePosition();
                        dragBinder.Callback(DragState.DragStart, Vector2.Zero);
                    }
                }
                else //拖拽更新
                {
                    var mousePos = dragBinder.Control.GetGlobalMousePosition();
                    if (mousePos != dragBinder.PrevPosition)
                    {
                        var deltaPosition = mousePos - dragBinder.PrevPosition;
                        dragBinder.PrevPosition = mousePos;
                        dragBinder.Callback(DragState.DragMove, deltaPosition);
                    }
                }
            }
        }

        //移除绑定
        if (_removeList.Count > 0)
        {
            foreach (var binder in _removeList)
            {
                if (_list.Remove(binder) && GodotObject.IsInstanceValid(binder.Control))
                {
                    binder.Control.MouseEntered -= binder.OnMouseEntered;
                    binder.Control.MouseExited -= binder.OnMouseExited;
                }
            }
            _removeList.Clear();
        }
        
        //添加绑定
        if (_addList.Count > 0)
        {
            _list.AddRange(_addList);
            _addList.Clear();
        }
    }
}