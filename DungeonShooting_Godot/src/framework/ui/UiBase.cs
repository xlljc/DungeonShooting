using System.Collections;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Ui 基类
/// </summary>
[Tool]
public abstract partial class UiBase : Control
{
    /// <summary>
    /// 当前 UI 所属层级
    /// </summary>
    [Export]
    public UiLayer Layer = UiLayer.Middle;

    /// <summary>
    /// Ui 模式, 单例/正常模式
    /// </summary>
    [Export]
    public UiMode Mode = UiMode.Normal;

    /// <summary>
    /// 阻止下层 Ui 点击
    /// </summary>
    [Export]
    public bool KeepOut = false;

    /// <summary>
    /// ui名称
    /// </summary>
    public string UiName { get; } 
    
    /// <summary>
    /// 是否已经打开ui
    /// </summary>
    public bool IsOpen { get; private set; } = false;
    
    /// <summary>
    /// 是否已经销毁
    /// </summary>
    public bool IsDisposed { get; private set; } = false;

    //开启的协程
    private List<CoroutineData> _coroutineList;
    
    public UiBase(string uiName)
    {
        UiName = uiName;
        //记录ui打开
        UiManager.RecordUi(this, UiManager.RecordType.Open);
    }
    
    /// <summary>
    /// 创建当前ui时调用
    /// </summary>
    public virtual void OnCreateUi()
    {
    }
    
    /// <summary>
    /// 当前ui显示时调用
    /// </summary>
    public abstract void OnShowUi();

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
    /// 每帧调用一次
    /// </summary>
    public virtual void Process(float delta)
    {
    }

    /// <summary>
    /// 显示ui
    /// </summary>
    public void ShowUi()
    {
        if (IsOpen)
        {
            return;
        }

        IsOpen = true;
        Visible = true;
        OnShowUi();
    }
    
    /// <summary>
    /// 隐藏ui, 不会执行销毁
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
        //记录ui关闭
        UiManager.RecordUi(this, UiManager.RecordType.Close);
        HideUi();
        IsDisposed = true;
        OnDisposeUi();
        QueueFree();
    }

    public sealed override void _Process(double delta)
    {
        var newDelta = (float)delta;
        Process(newDelta);
        
        //协程更新
        if (_coroutineList != null)
        {
            ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, newDelta);
        }
    }

    /// <summary>
    /// 开启一个协程, 返回协程 id, 协程是在普通帧执行的, 支持: 协程嵌套, WaitForSeconds, WaitForFixedProcess, Task
    /// </summary>
    public long StartCoroutine(IEnumerator able)
    {
        return ProxyCoroutineHandler.ProxyStartCoroutine(ref _coroutineList, able);
    }

    /// <summary>
    /// 根据协程 id 停止协程
    /// </summary>
    public void StopCoroutine(long coroutineId)
    {
        ProxyCoroutineHandler.ProxyStopCoroutine(ref _coroutineList, coroutineId);
    }
    
    /// <summary>
    /// 停止所有协程
    /// </summary>
    public void StopAllCoroutine()
    {
        ProxyCoroutineHandler.ProxyStopAllCoroutine(ref _coroutineList);
    }
}