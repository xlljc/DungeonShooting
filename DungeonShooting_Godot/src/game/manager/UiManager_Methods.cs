/**
 * 该类为自动生成的, 请不要手动编辑, 以免造成代码丢失
 */
public static partial class UiManager
{

    public static class UiName
    {
        public const string BottomTips = "BottomTips";
        public const string EditorTools = "EditorTools";
        public const string Loading = "Loading";
        public const string Main = "Main";
        public const string MapEditor = "MapEditor";
        public const string RoomUI = "RoomUI";
        public const string Settlement = "Settlement";
    }

    /// <summary>
    /// 打开 BottomTips, 并返回UI实例
    /// </summary>
    public static UI.BottomTips.BottomTipsPanel Open_BottomTips()
    {
        return OpenUi<UI.BottomTips.BottomTipsPanel>(UiName.BottomTips);
    }

    /// <summary>
    /// 隐藏 BottomTips 的所有实例
    /// </summary>
    public static void Hide_BottomTips()
    {
        var uiInstance = Get_BottomTips_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 BottomTips 的所有实例
    /// </summary>
    public static void Dispose_BottomTips()
    {
        var uiInstance = Get_BottomTips_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 BottomTips 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.BottomTips.BottomTipsPanel[] Get_BottomTips_Instance()
    {
        return GetUiInstance<UI.BottomTips.BottomTipsPanel>(nameof(UI.BottomTips.BottomTips));
    }

    /// <summary>
    /// 打开 EditorTools, 并返回UI实例
    /// </summary>
    public static UI.EditorTools.EditorToolsPanel Open_EditorTools()
    {
        return OpenUi<UI.EditorTools.EditorToolsPanel>(UiName.EditorTools);
    }

    /// <summary>
    /// 隐藏 EditorTools 的所有实例
    /// </summary>
    public static void Hide_EditorTools()
    {
        var uiInstance = Get_EditorTools_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorTools 的所有实例
    /// </summary>
    public static void Dispose_EditorTools()
    {
        var uiInstance = Get_EditorTools_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 EditorTools 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorTools.EditorToolsPanel[] Get_EditorTools_Instance()
    {
        return GetUiInstance<UI.EditorTools.EditorToolsPanel>(nameof(UI.EditorTools.EditorTools));
    }

    /// <summary>
    /// 打开 Loading, 并返回UI实例
    /// </summary>
    public static UI.Loading.LoadingPanel Open_Loading()
    {
        return OpenUi<UI.Loading.LoadingPanel>(UiName.Loading);
    }

    /// <summary>
    /// 隐藏 Loading 的所有实例
    /// </summary>
    public static void Hide_Loading()
    {
        var uiInstance = Get_Loading_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 Loading 的所有实例
    /// </summary>
    public static void Dispose_Loading()
    {
        var uiInstance = Get_Loading_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 Loading 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.Loading.LoadingPanel[] Get_Loading_Instance()
    {
        return GetUiInstance<UI.Loading.LoadingPanel>(nameof(UI.Loading.Loading));
    }

    /// <summary>
    /// 打开 Main, 并返回UI实例
    /// </summary>
    public static UI.Main.MainPanel Open_Main()
    {
        return OpenUi<UI.Main.MainPanel>(UiName.Main);
    }

    /// <summary>
    /// 隐藏 Main 的所有实例
    /// </summary>
    public static void Hide_Main()
    {
        var uiInstance = Get_Main_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 Main 的所有实例
    /// </summary>
    public static void Dispose_Main()
    {
        var uiInstance = Get_Main_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 Main 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.Main.MainPanel[] Get_Main_Instance()
    {
        return GetUiInstance<UI.Main.MainPanel>(nameof(UI.Main.Main));
    }

    /// <summary>
    /// 打开 MapEditor, 并返回UI实例
    /// </summary>
    public static UI.MapEditor.MapEditorPanel Open_MapEditor()
    {
        return OpenUi<UI.MapEditor.MapEditorPanel>(UiName.MapEditor);
    }

    /// <summary>
    /// 隐藏 MapEditor 的所有实例
    /// </summary>
    public static void Hide_MapEditor()
    {
        var uiInstance = Get_MapEditor_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditor 的所有实例
    /// </summary>
    public static void Dispose_MapEditor()
    {
        var uiInstance = Get_MapEditor_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 MapEditor 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditor.MapEditorPanel[] Get_MapEditor_Instance()
    {
        return GetUiInstance<UI.MapEditor.MapEditorPanel>(nameof(UI.MapEditor.MapEditor));
    }

    /// <summary>
    /// 打开 RoomUI, 并返回UI实例
    /// </summary>
    public static UI.RoomUI.RoomUIPanel Open_RoomUI()
    {
        return OpenUi<UI.RoomUI.RoomUIPanel>(UiName.RoomUI);
    }

    /// <summary>
    /// 隐藏 RoomUI 的所有实例
    /// </summary>
    public static void Hide_RoomUI()
    {
        var uiInstance = Get_RoomUI_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 RoomUI 的所有实例
    /// </summary>
    public static void Dispose_RoomUI()
    {
        var uiInstance = Get_RoomUI_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 RoomUI 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.RoomUI.RoomUIPanel[] Get_RoomUI_Instance()
    {
        return GetUiInstance<UI.RoomUI.RoomUIPanel>(nameof(UI.RoomUI.RoomUI));
    }

    /// <summary>
    /// 打开 Settlement, 并返回UI实例
    /// </summary>
    public static UI.Settlement.SettlementPanel Open_Settlement()
    {
        return OpenUi<UI.Settlement.SettlementPanel>(UiName.Settlement);
    }

    /// <summary>
    /// 隐藏 Settlement 的所有实例
    /// </summary>
    public static void Hide_Settlement()
    {
        var uiInstance = Get_Settlement_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 Settlement 的所有实例
    /// </summary>
    public static void Dispose_Settlement()
    {
        var uiInstance = Get_Settlement_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 Settlement 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.Settlement.SettlementPanel[] Get_Settlement_Instance()
    {
        return GetUiInstance<UI.Settlement.SettlementPanel>(nameof(UI.Settlement.Settlement));
    }

}
