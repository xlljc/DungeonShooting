/**
 * 该类为自动生成的, 请不要手动编辑, 以免造成代码丢失
 */
public static partial class UiManager
{

    public static class UiName
    {
        public const string BottomTips = "BottomTips";
        public const string EditorTips = "EditorTips";
        public const string EditorTools = "EditorTools";
        public const string EditorWindow = "EditorWindow";
        public const string Loading = "Loading";
        public const string Main = "Main";
        public const string MapEditor = "MapEditor";
        public const string MapEditorCreateRoom = "MapEditorCreateRoom";
        public const string MapEditorProject = "MapEditorProject";
        public const string MapEditorTools = "MapEditorTools";
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
    /// 打开 EditorTips, 并返回UI实例
    /// </summary>
    public static UI.EditorTips.EditorTipsPanel Open_EditorTips()
    {
        return OpenUi<UI.EditorTips.EditorTipsPanel>(UiName.EditorTips);
    }

    /// <summary>
    /// 隐藏 EditorTips 的所有实例
    /// </summary>
    public static void Hide_EditorTips()
    {
        var uiInstance = Get_EditorTips_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorTips 的所有实例
    /// </summary>
    public static void Dispose_EditorTips()
    {
        var uiInstance = Get_EditorTips_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 EditorTips 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorTips.EditorTipsPanel[] Get_EditorTips_Instance()
    {
        return GetUiInstance<UI.EditorTips.EditorTipsPanel>(nameof(UI.EditorTips.EditorTips));
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
    /// 打开 EditorWindow, 并返回UI实例
    /// </summary>
    public static UI.EditorWindow.EditorWindowPanel Open_EditorWindow()
    {
        return OpenUi<UI.EditorWindow.EditorWindowPanel>(UiName.EditorWindow);
    }

    /// <summary>
    /// 隐藏 EditorWindow 的所有实例
    /// </summary>
    public static void Hide_EditorWindow()
    {
        var uiInstance = Get_EditorWindow_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorWindow 的所有实例
    /// </summary>
    public static void Dispose_EditorWindow()
    {
        var uiInstance = Get_EditorWindow_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 EditorWindow 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorWindow.EditorWindowPanel[] Get_EditorWindow_Instance()
    {
        return GetUiInstance<UI.EditorWindow.EditorWindowPanel>(nameof(UI.EditorWindow.EditorWindow));
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
    /// 打开 MapEditorCreateRoom, 并返回UI实例
    /// </summary>
    public static UI.MapEditorCreateRoom.MapEditorCreateRoomPanel Open_MapEditorCreateRoom()
    {
        return OpenUi<UI.MapEditorCreateRoom.MapEditorCreateRoomPanel>(UiName.MapEditorCreateRoom);
    }

    /// <summary>
    /// 隐藏 MapEditorCreateRoom 的所有实例
    /// </summary>
    public static void Hide_MapEditorCreateRoom()
    {
        var uiInstance = Get_MapEditorCreateRoom_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorCreateRoom 的所有实例
    /// </summary>
    public static void Dispose_MapEditorCreateRoom()
    {
        var uiInstance = Get_MapEditorCreateRoom_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorCreateRoom 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorCreateRoom.MapEditorCreateRoomPanel[] Get_MapEditorCreateRoom_Instance()
    {
        return GetUiInstance<UI.MapEditorCreateRoom.MapEditorCreateRoomPanel>(nameof(UI.MapEditorCreateRoom.MapEditorCreateRoom));
    }

    /// <summary>
    /// 打开 MapEditorProject, 并返回UI实例
    /// </summary>
    public static UI.MapEditorProject.MapEditorProjectPanel Open_MapEditorProject()
    {
        return OpenUi<UI.MapEditorProject.MapEditorProjectPanel>(UiName.MapEditorProject);
    }

    /// <summary>
    /// 隐藏 MapEditorProject 的所有实例
    /// </summary>
    public static void Hide_MapEditorProject()
    {
        var uiInstance = Get_MapEditorProject_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorProject 的所有实例
    /// </summary>
    public static void Dispose_MapEditorProject()
    {
        var uiInstance = Get_MapEditorProject_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorProject 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorProject.MapEditorProjectPanel[] Get_MapEditorProject_Instance()
    {
        return GetUiInstance<UI.MapEditorProject.MapEditorProjectPanel>(nameof(UI.MapEditorProject.MapEditorProject));
    }

    /// <summary>
    /// 打开 MapEditorTools, 并返回UI实例
    /// </summary>
    public static UI.MapEditorTools.MapEditorToolsPanel Open_MapEditorTools()
    {
        return OpenUi<UI.MapEditorTools.MapEditorToolsPanel>(UiName.MapEditorTools);
    }

    /// <summary>
    /// 隐藏 MapEditorTools 的所有实例
    /// </summary>
    public static void Hide_MapEditorTools()
    {
        var uiInstance = Get_MapEditorTools_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorTools 的所有实例
    /// </summary>
    public static void Dispose_MapEditorTools()
    {
        var uiInstance = Get_MapEditorTools_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.DisposeUi();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorTools 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorTools.MapEditorToolsPanel[] Get_MapEditorTools_Instance()
    {
        return GetUiInstance<UI.MapEditorTools.MapEditorToolsPanel>(nameof(UI.MapEditorTools.MapEditorTools));
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
