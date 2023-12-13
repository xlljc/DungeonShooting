
using System;
using Godot;

/// <summary>
/// 拖拽绑定数据对象, 通过 DragUiManager 创建
/// </summary>
public class DragBinder
{
    public Control Control;
    public bool MouseEntered;
    public bool Dragging;
    public Vector2 PrevPosition;
    public Action<DragState, Vector2> Callback;
    public StringName InputAction;

    public void OnMouseEntered()
    {
        MouseEntered = true;
    }
    
    public void OnMouseExited()
    {
        MouseEntered = false;
    }
    
    public void UnBind()
    {
        DragUiManager.UnBindDrag(this);
    }
}