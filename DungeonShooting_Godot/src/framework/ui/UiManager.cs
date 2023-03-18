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

    private static UiBase InstantiateUi(string resourcePath)
    {
        var packedScene = ResourceManager.Load<PackedScene>(resourcePath);
        var uiBase = packedScene.Instantiate<UiBase>();
        return uiBase;
    }

    /// <summary>
    /// 根据Ui资源路径打开Ui, 并返回Ui实例, 该Ui资源的场景根节点必须继承<see cref="UiBase"/>
    /// </summary>
    public static UiBase OpenUi(string resourcePath, params object[] args)
    {
        var uiBase = InstantiateUi(resourcePath);
        var canvasLayer = GetUiLayer(uiBase.Layer);
        canvasLayer.AddChild(uiBase);
        uiBase.OnCreateUi();
        uiBase.ShowUi(args);
        return uiBase;
    }

    /// <summary>
    /// 根据Ui资源路径打开Ui, 并返回Ui实例, 该Ui资源的场景根节点必须继承<see cref="UiBase"/>
    /// </summary>
    public static T OpenUi<T>(string resourcePath, params object[] args) where T : UiBase
    {
        return (T)OpenUi(resourcePath, args);
    }

    /// <summary>
    /// 销毁指定Ui
    /// </summary>
    public static void DisposeUi(UiBase uiBase)
    {
        uiBase.DisposeUi();
    }
}