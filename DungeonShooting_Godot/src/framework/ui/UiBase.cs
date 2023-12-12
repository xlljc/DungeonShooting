using System.Collections;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Ui 基类
/// </summary>
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

    /// <summary>
    /// 所属父级Ui, 仅当通过 UiNode.OpenNestedUi() 打开时才会赋值<br/>
    /// 注意: 如果是在预制体中放置的子 Ui, 那么子 Ui 的该属性会在 父 Ui 的 OnCreateUi() 之后赋值
    /// </summary>
    public UiBase ParentUi { get; private set; }
    
    /// <summary>
    /// 所属父级节点, 仅当通过 UiNode.OpenNestedUi() 打开时才会赋值<br/>
    /// 注意: 如果是在预制体中放置的子 Ui, 那么子 Ui 的该属性会在 父 Ui 的 OnCreateUi() 之后赋值
    /// </summary>
    public IUiNode ParentNode { get; private set; }

    /// <summary>
    /// 是否是嵌套的子 Ui
    /// </summary>
    public bool IsNestedUi => ParentUi != null;
    
    //开启的协程
    private List<CoroutineData> _coroutineList;
    //嵌套打开的Ui列表
    private HashSet<UiBase> _nestedUiSet;
    //嵌套模式下是否打开Ui
    private bool _nestedOpen;
    //当前Ui包含的 IUiNodeScript 接口, 关闭Ui是需要调用 IUiNodeScript.OnDestroy()
    private HashSet<IUiNodeScript> _nodeScripts;

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
    /// 用于初始化打开的子Ui, 在 OnCreateUi() 之后调用
    /// </summary>
    public virtual void OnInitNestedUi()
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
    /// 如果 Ui 处于打开状态, 则每帧调用一次
    /// </summary>
    public virtual void Process(float delta)
    {
    }

    /// <summary>
    /// 显示ui
    /// </summary>
    public void ShowUi()
    {
        if (IsDestroyed)
        {
            Debug.LogError($"当前Ui: {UiName}已经被销毁!");
            return;
        }
        if (IsOpen)
        {
            return;
        }

        _nestedOpen = true;
        IsOpen = true;
        Visible = true;
        OnShowUi();
        
        //子Ui调用显示
        if (_nestedUiSet != null)
        {
            foreach (var uiBase in _nestedUiSet)
            {
                if (uiBase._nestedOpen || uiBase.Visible)
                {
                    uiBase.ShowUi();
                }
            }
        }
    }
    
    /// <summary>
    /// 隐藏ui, 不会执行销毁
    /// </summary>
    public void HideUi()
    {
        if (IsDestroyed)
        {
            Debug.LogError($"当前Ui: {UiName}已经被销毁!");
            return;
        }
        if (!IsOpen)
        {
            return;
        }

        _nestedOpen = false;
        IsOpen = false;
        Visible = false;
        OnHideUi();
        
        //子Ui调用隐藏
        if (_nestedUiSet != null)
        {
            foreach (var uiBase in _nestedUiSet)
            {
                if (uiBase._nestedOpen)
                {
                    uiBase.HideUi();
                    uiBase._nestedOpen = true;
                }
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
                uiBase.ParentUi = null;
                uiBase.Destroy();
            }
            _nestedUiSet.Clear();
        }
        
        //销毁 IUiNodeScript
        if (_nodeScripts != null)
        {
            foreach (var uiNodeScript in _nodeScripts)
            {
                uiNodeScript.OnDestroy();
            }
        }

        //在父Ui中移除当前Ui
        if (ParentUi != null)
        {
            ParentUi.RecordNestedUi(this, null, UiManager.RecordType.Close);
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
        ProxyCoroutineHandler.ProxyUpdateCoroutine(ref _coroutineList, newDelta);
    }

    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public UiBase OpenNestedUi(string uiName, UiBase prevUi = null)
    {
        var packedScene = ResourceManager.Load<PackedScene>("res://" + GameConfig.UiPrefabDir + uiName + ".tscn");
        var uiBase = packedScene.Instantiate<UiBase>();
        uiBase.Visible = false;
        uiBase.PrevUi = prevUi;
        AddChild(uiBase);
        RecordNestedUi(uiBase, null, UiManager.RecordType.Open);
        
        uiBase.OnCreateUi();
        uiBase.OnInitNestedUi();
        if (IsOpen)
        {
            uiBase.ShowUi();
        }
        
        return uiBase;
    }

    /// <summary>
    /// 嵌套打开子ui
    /// </summary>
    public T OpenNestedUi<T>(string uiName, UiBase prevUi = null) where T : UiBase
    {
        return (T)OpenNestedUi(uiName, prevUi);
    }

    /// <summary>
    /// 记录嵌套打开/关闭的UI
    /// </summary>
    public void RecordNestedUi(UiBase uiBase, IUiNode node, UiManager.RecordType type)
    {
        if (type == UiManager.RecordType.Open)
        {
            if (uiBase.ParentUi != null && uiBase.ParentUi != this)
            {
                Debug.LogError($"子Ui:'{uiBase.UiName}'已经被其他Ui:'{uiBase.ParentUi.UiName}'嵌套打开!");
                uiBase.ParentUi.RecordNestedUi(uiBase, node, UiManager.RecordType.Close);
            }
            if (_nestedUiSet == null)
            {
                _nestedUiSet = new HashSet<UiBase>();
            }

            uiBase.ParentUi = this;
            uiBase.ParentNode = node;
            _nestedUiSet.Add(uiBase);
        }
        else
        {
            if (uiBase.ParentUi == this)
            {
                uiBase.ParentUi = null;
                uiBase.ParentNode = null;
            }
            else
            {
                Debug.LogError($"当前Ui:'{UiName}'没有嵌套打开子Ui:'{uiBase.UiName}'!");
                return;
            }
            
            if (_nestedUiSet == null)
            {
                return;
            }
            _nestedUiSet.Remove(uiBase);
        }
    }

    /// <summary>
    /// 记录当前Ui包含的 IUiNodeScript 接口
    /// </summary>
    public void RecordUiNodeScript(IUiNodeScript nodeScript)
    {
        if (_nodeScripts == null)
        {
            _nodeScripts = new HashSet<IUiNodeScript>();
        }
        _nodeScripts.Add(nodeScript);
    }

    /// <summary>
    /// 打开下一级Ui, 当前Ui会被隐藏
    /// </summary>
    /// <param name="uiName">下一级Ui的名称</param>
    public UiBase OpenNextUi(string uiName)
    {
        UiBase uiBase;
        if (ParentUi != null) //说明当前Ui是嵌套Ui
        {
            if (ParentNode != null) //子层级打开
            {
                uiBase = ParentNode.OpenNestedUi(uiName, this);
            }
            else
            {
                uiBase = ParentUi.OpenNestedUi(uiName, this);
            }
        }
        else //正常打开
        {
            uiBase = UiManager.OpenUi(uiName, this);
        }
        HideUi();
        return uiBase;
    }
    
    
    /// <summary>
    /// 打开下一级Ui, 当前Ui会被隐藏
    /// </summary>
    /// <param name="uiName">下一级Ui的名称</param>
    public T OpenNextUi<T>(string uiName) where T : UiBase
    {
        return (T)OpenNextUi(uiName);
    }

    /// <summary>
    /// 返回上一级Ui, 当前Ui会被销毁
    /// </summary>
    public void OpenPrevUi()
    {
        Destroy();
        if (PrevUi == null)
        {
            Debug.LogError($"Ui: {UiName} 没有记录上一级Ui!");
        }
        else
        {
            PrevUi.ShowUi();
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
    
    public bool IsCoroutineOver(long coroutineId)
    {
        return ProxyCoroutineHandler.ProxyIsCoroutineOver(ref _coroutineList, coroutineId);
    }
    
    public void StopAllCoroutine()
    {
        ProxyCoroutineHandler.ProxyStopAllCoroutine(ref _coroutineList);
    }
}