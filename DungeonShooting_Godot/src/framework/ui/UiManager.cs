
using System;
using Godot;

/// <summary>
/// ui管理类
/// </summary>
public static partial class UiManager
{
    private static bool _init = false;

    private static CanvasLayer _bottomLayer;
    private static CanvasLayer _middleLayer;
    private static CanvasLayer _heightLayer;
    private static CanvasLayer _popLayer;
    
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
    
    public static UiBase OpenUi(string resourcePath)
    {
        var packedScene = ResourceManager.Load<PackedScene>(resourcePath);
        var uiBase = packedScene.Instantiate<UiBase>();
        var canvasLayer = GetUiLayer(uiBase.Layer);
        canvasLayer.AddChild(uiBase);
        uiBase.OnCreate();
        uiBase.OnOpen();
        return uiBase;
    }

    public static T OpenUi<T>(string resourcePath) where T : UiBase
    {
        return (T)OpenUi(resourcePath);
    }
}