
using Godot;

/// <summary>
/// Ui 基类
/// </summary>
public abstract partial class UiBase : Control
{
    /// <summary>
    /// 当前 UI 所属层级
    /// </summary>
    [Export]
    public UiLayer Layer = UiLayer.Middle;

    /// <summary>
    /// Ui 模式
    /// </summary>
    [Export]
    public UiMode Mode = UiMode.Normal;

    /// <summary>
    /// 阻止下层 Ui 点击
    /// </summary>
    [Export]
    public bool KeepOut = false;

    /// <summary>
    /// 是否已经打开ui
    /// </summary>
    public bool IsOpen { get; private set; } = false;
    
    /// <summary>
    /// 是否已经销毁
    /// </summary>
    public bool IsDisposed { get; private set; } = false;

    /// <summary>
    /// 创建当前ui时调用
    /// </summary>
    public virtual void OnCreateUi()
    {
    }
    
    /// <summary>
    /// 当前ui显示时调用，并接收参数
    /// </summary>
    public abstract void OnShowUi(params object[] args);

    /// <summary>
    /// 当前ui隐藏时调用
    /// </summary>
    public abstract void OnHideUi();

    /// <summary>
    /// 销毁当前ui时调用
    /// </summary>
    public virtual void OnDisposeUi()
    {
    }

    /// <summary>
    /// 显示ui, 并传入参数
    /// </summary>
    public void ShowUi(params object[] args)
    {
        if (IsOpen)
        {
            return;
        }

        IsOpen = true;
        Visible = true;
        OnShowUi(args);
    }
    
    /// <summary>
    /// 隐藏ui
    /// </summary>
    public void HideUi()
    {
        if (!IsOpen)
        {
            return;
        }

        IsOpen = false;
        Visible = false;
        OnHideUi();
    }

    /// <summary>
    /// 关闭并销毁ui
    /// </summary>
    public void DisposeUi()
    {
        if (IsDisposed)
        {
            return;
        }
        HideUi();
        IsDisposed = true;
        OnDisposeUi();
        QueueFree();
    }
}