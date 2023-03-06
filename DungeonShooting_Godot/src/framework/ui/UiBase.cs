
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
    /// 当前ui打开时调用，并接收参数
    /// </summary>
    public abstract void OnOpen(params object[] args);

    /// <summary>
    /// 当前ui关闭时调用
    /// </summary>
    public abstract void OnClose();

    /// <summary>
    /// 创建当前ui时调用
    /// </summary>
    public virtual void OnCreate()
    {
    }

    /// <summary>
    /// 销毁当前ui时调用
    /// </summary>
    public virtual void OnDispose()
    {
    }
}