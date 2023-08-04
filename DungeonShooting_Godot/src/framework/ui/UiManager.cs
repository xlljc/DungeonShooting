using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// ui管理类
/// </summary>
public static partial class UiManager
{
    public enum RecordType
    {
        Open,
        Close,
    }
    
    private static bool _init = false;

    private static CanvasLayer _bottomLayer;
    private static CanvasLayer _middleLayer;
    private static CanvasLayer _heightLayer;
    private static CanvasLayer _popLayer;

    private static Dictionary<string, List<UiBase>> _recordUiMap = new Dictionary<string, List<UiBase>>();

    /// <summary>
    /// 初始化Ui管理器
    /// </summary>
    public static void Init()
    {
        if (_init)
        {
            return;
        }

        _init = true;
        
        //创建ui层
        
        //Bottom
        _bottomLayer = new CanvasLayer();
        _bottomLayer.Name = "BottomLayer";
        _bottomLayer.Layer = 5;
        GameApplication.Instance.AddChild(_bottomLayer);
        
        //Middle
        _middleLayer = new CanvasLayer();
        _middleLayer.Name = "MiddleLayer";
        _middleLayer.Layer = 15;
        GameApplication.Instance.AddChild(_middleLayer);
        
        //Height
        _heightLayer = new CanvasLayer();
        _heightLayer.Name = "HeightLayer";
        _heightLayer.Layer = 25;
        GameApplication.Instance.AddChild(_heightLayer);
        
        //Pop
        _popLayer = new CanvasLayer();
        _popLayer.Name = "PopLayer";
        _popLayer.Layer = 35;
        GameApplication.Instance.AddChild(_popLayer);
    }

    /// <summary>
    /// 获取指定的Ui层根节点
    /// </summary>
    public static CanvasLayer GetUiLayer(UiLayer uiLayer)
    {
        switch (uiLayer)
        {
            case UiLayer.Bottom:
                return _bottomLayer;
            case UiLayer.Middle:
                return _middleLayer;
            case UiLayer.Height:
                return _heightLayer;
            case UiLayer.Pop:
                return _popLayer;
        }

        return null;
    }

    /// <summary>
    /// 记录ui的创建或者销毁, 子 ui 通过 UiBase.RecordNestedUi() 来记录
    /// </summary>
    public static void RecordUi(UiBase uiBase, RecordType type)
    {
        if (type == RecordType.Open)
        {
            if (_recordUiMap.TryGetValue(uiBase.UiName, out var list))
            {
                list.Add(uiBase);
            }
            else
            {
                list = new List<UiBase>();
                list.Add(uiBase);
                _recordUiMap.Add(uiBase.UiName, list);
            }
        }
        else
        {
            if (_recordUiMap.TryGetValue(uiBase.UiName, out var list))
            {
                list.Remove(uiBase);
                if (list.Count == 0)
                {
                    _recordUiMap.Remove(uiBase.UiName);
                }
            }
        }
    }

    /// <summary>
    /// 根据Ui资源路径创建Ui, 并返回Ui实例, 该Ui资源的场景根节点必须继承<see cref="UiBase"/><br/>
    /// 该函数不会自动打开Ui, 需要手动调用 ShowUi() 函数来显示Ui
    /// </summary>
    /// <param name="uiName">Ui名称</param>
    /// <param name="prevUi">上一级Ui, 用于UIBase.OpenPrevUi()函数返回上一级Ui</param>
    public static UiBase CreateUi(string uiName, UiBase prevUi = null)
    {
        if (!_init)
        {
            throw new Exception("未初始化 UiManager!, 请先调用 UiManager.Init() 函数!");
        }
        var packedScene = ResourceLoader.Load<PackedScene>("res://" + GameConfig.UiPrefabDir + uiName + ".tscn");
        var uiBase = packedScene.Instantiate<UiBase>();
        uiBase.PrevUi = prevUi;
        var canvasLayer = GetUiLayer(uiBase.Layer);
        canvasLayer.AddChild(uiBase);
        uiBase.OnCreateUi();
        uiBase.OnInitNestedUi();
        return uiBase;
    }

    /// <summary>
    /// 根据Ui资源路径创建Ui, 并返回Ui实例, 该Ui资源的场景根节点必须继承<see cref="UiBase"/><br/>
    /// 该函数不会自动打开Ui, 需要手动调用 ShowUi() 函数来显示Ui
    /// </summary>
    /// <param name="uiName">Ui名称</param>
    /// <param name="prevUi">上一级Ui, 用于UIBase.OpenPrevUi()函数返回上一级Ui</param>
    public static T CreateUi<T>(string uiName, UiBase prevUi = null) where T : UiBase
    {
        return (T)CreateUi(uiName, prevUi);
    }

    /// <summary>
    /// 根据Ui资源路径打开Ui, 并返回Ui实例, 该Ui资源的场景根节点必须继承<see cref="UiBase"/>
    /// </summary>
    /// <param name="uiName">Ui名称</param>
    /// <param name="prevUi">上一级Ui, 用于UIBase.OpenPrevUi()函数返回上一级Ui</param>
    public static UiBase OpenUi(string uiName, UiBase prevUi = null)
    {
        var uiBase = CreateUi(uiName, prevUi);
        uiBase.ShowUi();
        return uiBase;
    }

    /// <summary>
    /// 根据Ui资源路径打开Ui, 并返回Ui实例, 该Ui资源的场景根节点必须继承<see cref="UiBase"/>
    /// </summary>
    /// <param name="uiName">Ui名称</param>
    /// <param name="prevUi">上一级Ui, 用于UIBase.OpenPrevUi()函数返回上一级Ui</param>
    public static T OpenUi<T>(string uiName, UiBase prevUi = null) where T : UiBase
    {
        return (T)OpenUi(uiName, prevUi);
    }


    /// <summary>
    /// 销毁指定Ui
    /// </summary>
    public static void DestroyUi(UiBase uiBase)
    {
        uiBase.Destroy();
    }

    /// <summary>
    /// 
    /// </summary>
    public static void HideUi(UiBase uiBase)
    {
        uiBase.HideUi();
    }
    
    /// <summary>
    /// 销毁所有Ui
    /// </summary>
    public static void DestroyAllUi()
    {
        var map = new Dictionary<string, List<UiBase>>();
        foreach (var item in _recordUiMap)
        {
            map.Add(item.Key, new List<UiBase>(item.Value));
        }
        
        foreach (var item in map)
        {
            foreach (var uiBase in item.Value)
            {
                uiBase.Destroy();
            }
        }
    }

    /// <summary>
    /// 隐藏所有Ui
    /// </summary>
    public static void HideAllUi()
    {
        var map = new Dictionary<string, List<UiBase>>();
        foreach (var item in _recordUiMap)
        {
            map.Add(item.Key, new List<UiBase>(item.Value));
        }
        
        foreach (var item in map)
        {
            foreach (var uiBase in item.Value)
            {
                uiBase.HideUi();
            }
        }
    }

    /// <summary>
    /// 获取Ui实例
    /// </summary>
    public static T[] GetUiInstance<T>(string uiName) where T : UiBase
    {
        if (_recordUiMap.TryGetValue(uiName, out var list))
        {
            var result = new T[list.Count];
            for (var i = 0; i < list.Count; i++)
            {
                result[i] = (T)list[i];
            }
            return result;
        }

        return new T[0];
    }
}