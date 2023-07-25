using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Ui 基类
/// </summary>
[Tool]
public abstract partial class UiBase : Control, IDestroy, ICoroutine
{
    /// <summary>
    /// 当前 UI 所属层级
    /// </summary>
    [Export]
    public UiLayer Layer = UiLayer.Middle;

    /// <summary>
    /// ui名称
    /// </summary>
    public string UiName { get; } 
    
    /// <summary>
    /// 是否已经打开ui
    /// </summary>
    public bool IsOpen { get; private set; } = false;

    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 负责记录上一个Ui
    /// </summary>
    public UiBase PrevUi { get; set; }

    //开启的协程
    private List<CoroutineData> _coroutineList;
    //嵌套打开的Ui列表
    private HashSet<UiBase> _nestedUiSet;
    //所属父级Ui, UiNode.RecordNestedUi() 嵌套打开的 Ui 将被赋予此值
    private UiBase _targetUi;

    public UiBase(string uiName)
    {
        UiName = uiName;
        //记录ui打开
        UiManager.RecordUi(this, UiManager.RecordType.Open);
    }

    /// <summary>
    /// 用于初始化打开的子Ui, 在 OnCreateUi() 之后调用
    /// </summary>
    public virtual void OnInitNestedUi()
    {
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
    public virtual void OnShowUi()
    {
    }

    /// <summary>
    /// 当前ui隐藏时调用
    /// </summary>
    public virtual void OnHideUi()
    {
    }

    /// <summary>
    /// 销毁当前ui时调用
    /// </summary>
    public virtual void OnDestroyUi()
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
        
        //子Ui调用显示
        if (_nestedUiSet != null)
        {
            foreach (var uiBase in _nestedUiSet)
            {
                uiBase.ShowUi();
            }
        }
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
        
        //子Ui调用隐藏
        if (_nestedUiSet != null)
        {
            foreach (var uiBase in _nestedUiSet)
            {
                uiBase.HideUi();
            }
        }
    }

    /// <summary>
    /// 关闭并销毁ui
    /// </summary>
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }
        //记录ui关闭
        UiManager.RecordUi(this, UiManager.RecordType.Close);
        HideUi();
        IsDestroyed = true;
        OnDestroyUi();
        
        //子Ui调用销毁
        if (_nestedUiSet != null)
        {
            foreach (var uiBase in _nestedUiSet)
            {
                uiBase._targetUi = null;
                uiBase.Destroy();
            }
            _nestedUiSet.Clear();
        }

        //在父Ui中移除当前Ui
        if (_targetUi != null)
        {
            _targetUi.RecordNestedUi(this, UiManager.RecordType.Close);
        }
        
        QueueFree();
    }

    public sealed override void _Process(double delta)
    {
        if (!IsOpen)
        {
            return;
        }
        var newDelta = (float)delta;
        Process(newDelta);
        
        //协程更新
        if (_coroutineList != null)
        {
            ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, newDelta);
        }
    }

    /// <summary>
    /// 记录嵌套打开/关闭的UI
    /// </summary>
    public void RecordNestedUi(UiBase uiBase, UiManager.RecordType type)
    {
        if (type == UiManager.RecordType.Open)
        {
            if (uiBase._targetUi != null && uiBase._targetUi != this)
            {
                GD.PrintErr($"子Ui:'{uiBase.UiName}'已经被其他Ui:'{uiBase._targetUi.UiName}'嵌套打开!");
                uiBase._targetUi.RecordNestedUi(uiBase, UiManager.RecordType.Close);
            }
            if (_nestedUiSet == null)
            {
                _nestedUiSet = new HashSet<UiBase>();
            }

            uiBase._targetUi = this;
            _nestedUiSet.Add(uiBase);
        }
        else
        {
            if (uiBase._targetUi == this)
            {
                uiBase._targetUi = null;
            }
            else
            {
                GD.PrintErr($"当前Ui:'{UiName}'没有嵌套打开子Ui:'{uiBase.UiName}'!");
                return;
            }
            
            if (_nestedUiSet == null)
            {
                return;
            }
            _nestedUiSet.Remove(uiBase);
        }
    }
    
    public long StartCoroutine(IEnumerator able)
    {
        return ProxyCoroutineHandler.ProxyStartCoroutine(ref _coroutineList, able);
    }
    
    public void StopCoroutine(long coroutineId)
    {
        ProxyCoroutineHandler.ProxyStopCoroutine(ref _coroutineList, coroutineId);
    }
    
    public void StopAllCoroutine()
    {
        ProxyCoroutineHandler.ProxyStopAllCoroutine(ref _coroutineList);
    }
}