
using Godot;

/// <summary>
/// 拖拽绑定数据对象, 通过 NodeExtend.AddDragEventListener 创建
/// </summary>
public class DragBinder
{
    
    private Control Control;
    private Control.GuiInputEventHandler Callback;

    public DragBinder(Control control, Control.GuiInputEventHandler callback)
    {
        Control = control;
        Callback = callback;
    }

    /// <summary>
    /// 解除绑定事件
    /// </summary>
    public void UnBind()
    {
        Control.GuiInput -= Callback;
    }
}