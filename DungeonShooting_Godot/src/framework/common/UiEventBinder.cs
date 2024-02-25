
using Godot;

/// <summary>
/// Ui事件绑定数据对象
/// </summary>
public class UiEventBinder
{
    
    private Control Control;
    private Control.GuiInputEventHandler Callback;

    public UiEventBinder(Control control, Control.GuiInputEventHandler callback)
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