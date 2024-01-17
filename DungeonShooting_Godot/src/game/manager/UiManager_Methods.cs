
// 该类为自动生成的, 请不要手动编辑, 以免造成代码丢失
public static partial class UiManager
{

    public static class UiNames
    {
        public const string BottomTips = "BottomTips";
        public const string Debugger = "Debugger";
        public const string EditorColorPicker = "EditorColorPicker";
        public const string EditorForm = "EditorForm";
        public const string EditorImportCombination = "EditorImportCombination";
        public const string EditorInfo = "EditorInfo";
        public const string EditorInput = "EditorInput";
        public const string EditorManager = "EditorManager";
        public const string EditorTips = "EditorTips";
        public const string EditorTools = "EditorTools";
        public const string EditorWindow = "EditorWindow";
        public const string Loading = "Loading";
        public const string Main = "Main";
        public const string MapEditor = "MapEditor";
        public const string MapEditorCreateMark = "MapEditorCreateMark";
        public const string MapEditorCreatePreinstall = "MapEditorCreatePreinstall";
        public const string MapEditorCreateRoom = "MapEditorCreateRoom";
        public const string MapEditorMapLayer = "MapEditorMapLayer";
        public const string MapEditorMapMark = "MapEditorMapMark";
        public const string MapEditorMapTile = "MapEditorMapTile";
        public const string MapEditorProject = "MapEditorProject";
        public const string MapEditorSelectObject = "MapEditorSelectObject";
        public const string MapEditorTools = "MapEditorTools";
        public const string PauseMenu = "PauseMenu";
        public const string RoomMap = "RoomMap";
        public const string RoomUI = "RoomUI";
        public const string Setting = "Setting";
        public const string Settlement = "Settlement";
        public const string TileSetEditor = "TileSetEditor";
        public const string TileSetEditorCombination = "TileSetEditorCombination";
        public const string TileSetEditorImport = "TileSetEditorImport";
        public const string TileSetEditorProject = "TileSetEditorProject";
        public const string TileSetEditorTerrain = "TileSetEditorTerrain";
    }

    /// <summary>
    /// 创建 BottomTips, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.BottomTips.BottomTipsPanel Create_BottomTips()
    {
        return CreateUi<UI.BottomTips.BottomTipsPanel>(UiNames.BottomTips);
    }

    /// <summary>
    /// 打开 BottomTips, 并返回UI实例
    /// </summary>
    public static UI.BottomTips.BottomTipsPanel Open_BottomTips()
    {
        return OpenUi<UI.BottomTips.BottomTipsPanel>(UiNames.BottomTips);
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
    public static void Destroy_BottomTips()
    {
        var uiInstance = Get_BottomTips_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 Debugger, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.Debugger.DebuggerPanel Create_Debugger()
    {
        return CreateUi<UI.Debugger.DebuggerPanel>(UiNames.Debugger);
    }

    /// <summary>
    /// 打开 Debugger, 并返回UI实例
    /// </summary>
    public static UI.Debugger.DebuggerPanel Open_Debugger()
    {
        return OpenUi<UI.Debugger.DebuggerPanel>(UiNames.Debugger);
    }

    /// <summary>
    /// 隐藏 Debugger 的所有实例
    /// </summary>
    public static void Hide_Debugger()
    {
        var uiInstance = Get_Debugger_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 Debugger 的所有实例
    /// </summary>
    public static void Destroy_Debugger()
    {
        var uiInstance = Get_Debugger_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 Debugger 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.Debugger.DebuggerPanel[] Get_Debugger_Instance()
    {
        return GetUiInstance<UI.Debugger.DebuggerPanel>(nameof(UI.Debugger.Debugger));
    }

    /// <summary>
    /// 创建 EditorColorPicker, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorColorPicker.EditorColorPickerPanel Create_EditorColorPicker()
    {
        return CreateUi<UI.EditorColorPicker.EditorColorPickerPanel>(UiNames.EditorColorPicker);
    }

    /// <summary>
    /// 打开 EditorColorPicker, 并返回UI实例
    /// </summary>
    public static UI.EditorColorPicker.EditorColorPickerPanel Open_EditorColorPicker()
    {
        return OpenUi<UI.EditorColorPicker.EditorColorPickerPanel>(UiNames.EditorColorPicker);
    }

    /// <summary>
    /// 隐藏 EditorColorPicker 的所有实例
    /// </summary>
    public static void Hide_EditorColorPicker()
    {
        var uiInstance = Get_EditorColorPicker_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorColorPicker 的所有实例
    /// </summary>
    public static void Destroy_EditorColorPicker()
    {
        var uiInstance = Get_EditorColorPicker_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 EditorColorPicker 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorColorPicker.EditorColorPickerPanel[] Get_EditorColorPicker_Instance()
    {
        return GetUiInstance<UI.EditorColorPicker.EditorColorPickerPanel>(nameof(UI.EditorColorPicker.EditorColorPicker));
    }

    /// <summary>
    /// 创建 EditorForm, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorForm.EditorFormPanel Create_EditorForm()
    {
        return CreateUi<UI.EditorForm.EditorFormPanel>(UiNames.EditorForm);
    }

    /// <summary>
    /// 打开 EditorForm, 并返回UI实例
    /// </summary>
    public static UI.EditorForm.EditorFormPanel Open_EditorForm()
    {
        return OpenUi<UI.EditorForm.EditorFormPanel>(UiNames.EditorForm);
    }

    /// <summary>
    /// 隐藏 EditorForm 的所有实例
    /// </summary>
    public static void Hide_EditorForm()
    {
        var uiInstance = Get_EditorForm_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorForm 的所有实例
    /// </summary>
    public static void Destroy_EditorForm()
    {
        var uiInstance = Get_EditorForm_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 EditorForm 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorForm.EditorFormPanel[] Get_EditorForm_Instance()
    {
        return GetUiInstance<UI.EditorForm.EditorFormPanel>(nameof(UI.EditorForm.EditorForm));
    }

    /// <summary>
    /// 创建 EditorImportCombination, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorImportCombination.EditorImportCombinationPanel Create_EditorImportCombination()
    {
        return CreateUi<UI.EditorImportCombination.EditorImportCombinationPanel>(UiNames.EditorImportCombination);
    }

    /// <summary>
    /// 打开 EditorImportCombination, 并返回UI实例
    /// </summary>
    public static UI.EditorImportCombination.EditorImportCombinationPanel Open_EditorImportCombination()
    {
        return OpenUi<UI.EditorImportCombination.EditorImportCombinationPanel>(UiNames.EditorImportCombination);
    }

    /// <summary>
    /// 隐藏 EditorImportCombination 的所有实例
    /// </summary>
    public static void Hide_EditorImportCombination()
    {
        var uiInstance = Get_EditorImportCombination_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorImportCombination 的所有实例
    /// </summary>
    public static void Destroy_EditorImportCombination()
    {
        var uiInstance = Get_EditorImportCombination_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 EditorImportCombination 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorImportCombination.EditorImportCombinationPanel[] Get_EditorImportCombination_Instance()
    {
        return GetUiInstance<UI.EditorImportCombination.EditorImportCombinationPanel>(nameof(UI.EditorImportCombination.EditorImportCombination));
    }

    /// <summary>
    /// 创建 EditorInfo, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorInfo.EditorInfoPanel Create_EditorInfo()
    {
        return CreateUi<UI.EditorInfo.EditorInfoPanel>(UiNames.EditorInfo);
    }

    /// <summary>
    /// 打开 EditorInfo, 并返回UI实例
    /// </summary>
    public static UI.EditorInfo.EditorInfoPanel Open_EditorInfo()
    {
        return OpenUi<UI.EditorInfo.EditorInfoPanel>(UiNames.EditorInfo);
    }

    /// <summary>
    /// 隐藏 EditorInfo 的所有实例
    /// </summary>
    public static void Hide_EditorInfo()
    {
        var uiInstance = Get_EditorInfo_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorInfo 的所有实例
    /// </summary>
    public static void Destroy_EditorInfo()
    {
        var uiInstance = Get_EditorInfo_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 EditorInfo 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorInfo.EditorInfoPanel[] Get_EditorInfo_Instance()
    {
        return GetUiInstance<UI.EditorInfo.EditorInfoPanel>(nameof(UI.EditorInfo.EditorInfo));
    }

    /// <summary>
    /// 创建 EditorInput, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorInput.EditorInputPanel Create_EditorInput()
    {
        return CreateUi<UI.EditorInput.EditorInputPanel>(UiNames.EditorInput);
    }

    /// <summary>
    /// 打开 EditorInput, 并返回UI实例
    /// </summary>
    public static UI.EditorInput.EditorInputPanel Open_EditorInput()
    {
        return OpenUi<UI.EditorInput.EditorInputPanel>(UiNames.EditorInput);
    }

    /// <summary>
    /// 隐藏 EditorInput 的所有实例
    /// </summary>
    public static void Hide_EditorInput()
    {
        var uiInstance = Get_EditorInput_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorInput 的所有实例
    /// </summary>
    public static void Destroy_EditorInput()
    {
        var uiInstance = Get_EditorInput_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 EditorInput 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorInput.EditorInputPanel[] Get_EditorInput_Instance()
    {
        return GetUiInstance<UI.EditorInput.EditorInputPanel>(nameof(UI.EditorInput.EditorInput));
    }

    /// <summary>
    /// 创建 EditorManager, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorManager.EditorManagerPanel Create_EditorManager()
    {
        return CreateUi<UI.EditorManager.EditorManagerPanel>(UiNames.EditorManager);
    }

    /// <summary>
    /// 打开 EditorManager, 并返回UI实例
    /// </summary>
    public static UI.EditorManager.EditorManagerPanel Open_EditorManager()
    {
        return OpenUi<UI.EditorManager.EditorManagerPanel>(UiNames.EditorManager);
    }

    /// <summary>
    /// 隐藏 EditorManager 的所有实例
    /// </summary>
    public static void Hide_EditorManager()
    {
        var uiInstance = Get_EditorManager_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 EditorManager 的所有实例
    /// </summary>
    public static void Destroy_EditorManager()
    {
        var uiInstance = Get_EditorManager_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 EditorManager 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorManager.EditorManagerPanel[] Get_EditorManager_Instance()
    {
        return GetUiInstance<UI.EditorManager.EditorManagerPanel>(nameof(UI.EditorManager.EditorManager));
    }

    /// <summary>
    /// 创建 EditorTips, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorTips.EditorTipsPanel Create_EditorTips()
    {
        return CreateUi<UI.EditorTips.EditorTipsPanel>(UiNames.EditorTips);
    }

    /// <summary>
    /// 打开 EditorTips, 并返回UI实例
    /// </summary>
    public static UI.EditorTips.EditorTipsPanel Open_EditorTips()
    {
        return OpenUi<UI.EditorTips.EditorTipsPanel>(UiNames.EditorTips);
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
    public static void Destroy_EditorTips()
    {
        var uiInstance = Get_EditorTips_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 EditorTools, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorTools.EditorToolsPanel Create_EditorTools()
    {
        return CreateUi<UI.EditorTools.EditorToolsPanel>(UiNames.EditorTools);
    }

    /// <summary>
    /// 打开 EditorTools, 并返回UI实例
    /// </summary>
    public static UI.EditorTools.EditorToolsPanel Open_EditorTools()
    {
        return OpenUi<UI.EditorTools.EditorToolsPanel>(UiNames.EditorTools);
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
    public static void Destroy_EditorTools()
    {
        var uiInstance = Get_EditorTools_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 EditorWindow, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.EditorWindow.EditorWindowPanel Create_EditorWindow()
    {
        return CreateUi<UI.EditorWindow.EditorWindowPanel>(UiNames.EditorWindow);
    }

    /// <summary>
    /// 打开 EditorWindow, 并返回UI实例
    /// </summary>
    public static UI.EditorWindow.EditorWindowPanel Open_EditorWindow()
    {
        return OpenUi<UI.EditorWindow.EditorWindowPanel>(UiNames.EditorWindow);
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
    public static void Destroy_EditorWindow()
    {
        var uiInstance = Get_EditorWindow_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 Loading, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.Loading.LoadingPanel Create_Loading()
    {
        return CreateUi<UI.Loading.LoadingPanel>(UiNames.Loading);
    }

    /// <summary>
    /// 打开 Loading, 并返回UI实例
    /// </summary>
    public static UI.Loading.LoadingPanel Open_Loading()
    {
        return OpenUi<UI.Loading.LoadingPanel>(UiNames.Loading);
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
    public static void Destroy_Loading()
    {
        var uiInstance = Get_Loading_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 Main, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.Main.MainPanel Create_Main()
    {
        return CreateUi<UI.Main.MainPanel>(UiNames.Main);
    }

    /// <summary>
    /// 打开 Main, 并返回UI实例
    /// </summary>
    public static UI.Main.MainPanel Open_Main()
    {
        return OpenUi<UI.Main.MainPanel>(UiNames.Main);
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
    public static void Destroy_Main()
    {
        var uiInstance = Get_Main_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 MapEditor, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditor.MapEditorPanel Create_MapEditor()
    {
        return CreateUi<UI.MapEditor.MapEditorPanel>(UiNames.MapEditor);
    }

    /// <summary>
    /// 打开 MapEditor, 并返回UI实例
    /// </summary>
    public static UI.MapEditor.MapEditorPanel Open_MapEditor()
    {
        return OpenUi<UI.MapEditor.MapEditorPanel>(UiNames.MapEditor);
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
    public static void Destroy_MapEditor()
    {
        var uiInstance = Get_MapEditor_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 MapEditorCreateMark, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorCreateMark.MapEditorCreateMarkPanel Create_MapEditorCreateMark()
    {
        return CreateUi<UI.MapEditorCreateMark.MapEditorCreateMarkPanel>(UiNames.MapEditorCreateMark);
    }

    /// <summary>
    /// 打开 MapEditorCreateMark, 并返回UI实例
    /// </summary>
    public static UI.MapEditorCreateMark.MapEditorCreateMarkPanel Open_MapEditorCreateMark()
    {
        return OpenUi<UI.MapEditorCreateMark.MapEditorCreateMarkPanel>(UiNames.MapEditorCreateMark);
    }

    /// <summary>
    /// 隐藏 MapEditorCreateMark 的所有实例
    /// </summary>
    public static void Hide_MapEditorCreateMark()
    {
        var uiInstance = Get_MapEditorCreateMark_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorCreateMark 的所有实例
    /// </summary>
    public static void Destroy_MapEditorCreateMark()
    {
        var uiInstance = Get_MapEditorCreateMark_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorCreateMark 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorCreateMark.MapEditorCreateMarkPanel[] Get_MapEditorCreateMark_Instance()
    {
        return GetUiInstance<UI.MapEditorCreateMark.MapEditorCreateMarkPanel>(nameof(UI.MapEditorCreateMark.MapEditorCreateMark));
    }

    /// <summary>
    /// 创建 MapEditorCreatePreinstall, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorCreatePreinstall.MapEditorCreatePreinstallPanel Create_MapEditorCreatePreinstall()
    {
        return CreateUi<UI.MapEditorCreatePreinstall.MapEditorCreatePreinstallPanel>(UiNames.MapEditorCreatePreinstall);
    }

    /// <summary>
    /// 打开 MapEditorCreatePreinstall, 并返回UI实例
    /// </summary>
    public static UI.MapEditorCreatePreinstall.MapEditorCreatePreinstallPanel Open_MapEditorCreatePreinstall()
    {
        return OpenUi<UI.MapEditorCreatePreinstall.MapEditorCreatePreinstallPanel>(UiNames.MapEditorCreatePreinstall);
    }

    /// <summary>
    /// 隐藏 MapEditorCreatePreinstall 的所有实例
    /// </summary>
    public static void Hide_MapEditorCreatePreinstall()
    {
        var uiInstance = Get_MapEditorCreatePreinstall_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorCreatePreinstall 的所有实例
    /// </summary>
    public static void Destroy_MapEditorCreatePreinstall()
    {
        var uiInstance = Get_MapEditorCreatePreinstall_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorCreatePreinstall 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorCreatePreinstall.MapEditorCreatePreinstallPanel[] Get_MapEditorCreatePreinstall_Instance()
    {
        return GetUiInstance<UI.MapEditorCreatePreinstall.MapEditorCreatePreinstallPanel>(nameof(UI.MapEditorCreatePreinstall.MapEditorCreatePreinstall));
    }

    /// <summary>
    /// 创建 MapEditorCreateRoom, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorCreateRoom.MapEditorCreateRoomPanel Create_MapEditorCreateRoom()
    {
        return CreateUi<UI.MapEditorCreateRoom.MapEditorCreateRoomPanel>(UiNames.MapEditorCreateRoom);
    }

    /// <summary>
    /// 打开 MapEditorCreateRoom, 并返回UI实例
    /// </summary>
    public static UI.MapEditorCreateRoom.MapEditorCreateRoomPanel Open_MapEditorCreateRoom()
    {
        return OpenUi<UI.MapEditorCreateRoom.MapEditorCreateRoomPanel>(UiNames.MapEditorCreateRoom);
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
    public static void Destroy_MapEditorCreateRoom()
    {
        var uiInstance = Get_MapEditorCreateRoom_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 MapEditorMapLayer, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorMapLayer.MapEditorMapLayerPanel Create_MapEditorMapLayer()
    {
        return CreateUi<UI.MapEditorMapLayer.MapEditorMapLayerPanel>(UiNames.MapEditorMapLayer);
    }

    /// <summary>
    /// 打开 MapEditorMapLayer, 并返回UI实例
    /// </summary>
    public static UI.MapEditorMapLayer.MapEditorMapLayerPanel Open_MapEditorMapLayer()
    {
        return OpenUi<UI.MapEditorMapLayer.MapEditorMapLayerPanel>(UiNames.MapEditorMapLayer);
    }

    /// <summary>
    /// 隐藏 MapEditorMapLayer 的所有实例
    /// </summary>
    public static void Hide_MapEditorMapLayer()
    {
        var uiInstance = Get_MapEditorMapLayer_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorMapLayer 的所有实例
    /// </summary>
    public static void Destroy_MapEditorMapLayer()
    {
        var uiInstance = Get_MapEditorMapLayer_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorMapLayer 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorMapLayer.MapEditorMapLayerPanel[] Get_MapEditorMapLayer_Instance()
    {
        return GetUiInstance<UI.MapEditorMapLayer.MapEditorMapLayerPanel>(nameof(UI.MapEditorMapLayer.MapEditorMapLayer));
    }

    /// <summary>
    /// 创建 MapEditorMapMark, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorMapMark.MapEditorMapMarkPanel Create_MapEditorMapMark()
    {
        return CreateUi<UI.MapEditorMapMark.MapEditorMapMarkPanel>(UiNames.MapEditorMapMark);
    }

    /// <summary>
    /// 打开 MapEditorMapMark, 并返回UI实例
    /// </summary>
    public static UI.MapEditorMapMark.MapEditorMapMarkPanel Open_MapEditorMapMark()
    {
        return OpenUi<UI.MapEditorMapMark.MapEditorMapMarkPanel>(UiNames.MapEditorMapMark);
    }

    /// <summary>
    /// 隐藏 MapEditorMapMark 的所有实例
    /// </summary>
    public static void Hide_MapEditorMapMark()
    {
        var uiInstance = Get_MapEditorMapMark_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorMapMark 的所有实例
    /// </summary>
    public static void Destroy_MapEditorMapMark()
    {
        var uiInstance = Get_MapEditorMapMark_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorMapMark 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorMapMark.MapEditorMapMarkPanel[] Get_MapEditorMapMark_Instance()
    {
        return GetUiInstance<UI.MapEditorMapMark.MapEditorMapMarkPanel>(nameof(UI.MapEditorMapMark.MapEditorMapMark));
    }

    /// <summary>
    /// 创建 MapEditorMapTile, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorMapTile.MapEditorMapTilePanel Create_MapEditorMapTile()
    {
        return CreateUi<UI.MapEditorMapTile.MapEditorMapTilePanel>(UiNames.MapEditorMapTile);
    }

    /// <summary>
    /// 打开 MapEditorMapTile, 并返回UI实例
    /// </summary>
    public static UI.MapEditorMapTile.MapEditorMapTilePanel Open_MapEditorMapTile()
    {
        return OpenUi<UI.MapEditorMapTile.MapEditorMapTilePanel>(UiNames.MapEditorMapTile);
    }

    /// <summary>
    /// 隐藏 MapEditorMapTile 的所有实例
    /// </summary>
    public static void Hide_MapEditorMapTile()
    {
        var uiInstance = Get_MapEditorMapTile_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorMapTile 的所有实例
    /// </summary>
    public static void Destroy_MapEditorMapTile()
    {
        var uiInstance = Get_MapEditorMapTile_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorMapTile 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorMapTile.MapEditorMapTilePanel[] Get_MapEditorMapTile_Instance()
    {
        return GetUiInstance<UI.MapEditorMapTile.MapEditorMapTilePanel>(nameof(UI.MapEditorMapTile.MapEditorMapTile));
    }

    /// <summary>
    /// 创建 MapEditorProject, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorProject.MapEditorProjectPanel Create_MapEditorProject()
    {
        return CreateUi<UI.MapEditorProject.MapEditorProjectPanel>(UiNames.MapEditorProject);
    }

    /// <summary>
    /// 打开 MapEditorProject, 并返回UI实例
    /// </summary>
    public static UI.MapEditorProject.MapEditorProjectPanel Open_MapEditorProject()
    {
        return OpenUi<UI.MapEditorProject.MapEditorProjectPanel>(UiNames.MapEditorProject);
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
    public static void Destroy_MapEditorProject()
    {
        var uiInstance = Get_MapEditorProject_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 MapEditorSelectObject, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorSelectObject.MapEditorSelectObjectPanel Create_MapEditorSelectObject()
    {
        return CreateUi<UI.MapEditorSelectObject.MapEditorSelectObjectPanel>(UiNames.MapEditorSelectObject);
    }

    /// <summary>
    /// 打开 MapEditorSelectObject, 并返回UI实例
    /// </summary>
    public static UI.MapEditorSelectObject.MapEditorSelectObjectPanel Open_MapEditorSelectObject()
    {
        return OpenUi<UI.MapEditorSelectObject.MapEditorSelectObjectPanel>(UiNames.MapEditorSelectObject);
    }

    /// <summary>
    /// 隐藏 MapEditorSelectObject 的所有实例
    /// </summary>
    public static void Hide_MapEditorSelectObject()
    {
        var uiInstance = Get_MapEditorSelectObject_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 MapEditorSelectObject 的所有实例
    /// </summary>
    public static void Destroy_MapEditorSelectObject()
    {
        var uiInstance = Get_MapEditorSelectObject_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 MapEditorSelectObject 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.MapEditorSelectObject.MapEditorSelectObjectPanel[] Get_MapEditorSelectObject_Instance()
    {
        return GetUiInstance<UI.MapEditorSelectObject.MapEditorSelectObjectPanel>(nameof(UI.MapEditorSelectObject.MapEditorSelectObject));
    }

    /// <summary>
    /// 创建 MapEditorTools, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.MapEditorTools.MapEditorToolsPanel Create_MapEditorTools()
    {
        return CreateUi<UI.MapEditorTools.MapEditorToolsPanel>(UiNames.MapEditorTools);
    }

    /// <summary>
    /// 打开 MapEditorTools, 并返回UI实例
    /// </summary>
    public static UI.MapEditorTools.MapEditorToolsPanel Open_MapEditorTools()
    {
        return OpenUi<UI.MapEditorTools.MapEditorToolsPanel>(UiNames.MapEditorTools);
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
    public static void Destroy_MapEditorTools()
    {
        var uiInstance = Get_MapEditorTools_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 PauseMenu, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.PauseMenu.PauseMenuPanel Create_PauseMenu()
    {
        return CreateUi<UI.PauseMenu.PauseMenuPanel>(UiNames.PauseMenu);
    }

    /// <summary>
    /// 打开 PauseMenu, 并返回UI实例
    /// </summary>
    public static UI.PauseMenu.PauseMenuPanel Open_PauseMenu()
    {
        return OpenUi<UI.PauseMenu.PauseMenuPanel>(UiNames.PauseMenu);
    }

    /// <summary>
    /// 隐藏 PauseMenu 的所有实例
    /// </summary>
    public static void Hide_PauseMenu()
    {
        var uiInstance = Get_PauseMenu_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 PauseMenu 的所有实例
    /// </summary>
    public static void Destroy_PauseMenu()
    {
        var uiInstance = Get_PauseMenu_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 PauseMenu 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.PauseMenu.PauseMenuPanel[] Get_PauseMenu_Instance()
    {
        return GetUiInstance<UI.PauseMenu.PauseMenuPanel>(nameof(UI.PauseMenu.PauseMenu));
    }

    /// <summary>
    /// 创建 RoomMap, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.RoomMap.RoomMapPanel Create_RoomMap()
    {
        return CreateUi<UI.RoomMap.RoomMapPanel>(UiNames.RoomMap);
    }

    /// <summary>
    /// 打开 RoomMap, 并返回UI实例
    /// </summary>
    public static UI.RoomMap.RoomMapPanel Open_RoomMap()
    {
        return OpenUi<UI.RoomMap.RoomMapPanel>(UiNames.RoomMap);
    }

    /// <summary>
    /// 隐藏 RoomMap 的所有实例
    /// </summary>
    public static void Hide_RoomMap()
    {
        var uiInstance = Get_RoomMap_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 RoomMap 的所有实例
    /// </summary>
    public static void Destroy_RoomMap()
    {
        var uiInstance = Get_RoomMap_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 RoomMap 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.RoomMap.RoomMapPanel[] Get_RoomMap_Instance()
    {
        return GetUiInstance<UI.RoomMap.RoomMapPanel>(nameof(UI.RoomMap.RoomMap));
    }

    /// <summary>
    /// 创建 RoomUI, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.RoomUI.RoomUIPanel Create_RoomUI()
    {
        return CreateUi<UI.RoomUI.RoomUIPanel>(UiNames.RoomUI);
    }

    /// <summary>
    /// 打开 RoomUI, 并返回UI实例
    /// </summary>
    public static UI.RoomUI.RoomUIPanel Open_RoomUI()
    {
        return OpenUi<UI.RoomUI.RoomUIPanel>(UiNames.RoomUI);
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
    public static void Destroy_RoomUI()
    {
        var uiInstance = Get_RoomUI_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
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
    /// 创建 Setting, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.Setting.SettingPanel Create_Setting()
    {
        return CreateUi<UI.Setting.SettingPanel>(UiNames.Setting);
    }

    /// <summary>
    /// 打开 Setting, 并返回UI实例
    /// </summary>
    public static UI.Setting.SettingPanel Open_Setting()
    {
        return OpenUi<UI.Setting.SettingPanel>(UiNames.Setting);
    }

    /// <summary>
    /// 隐藏 Setting 的所有实例
    /// </summary>
    public static void Hide_Setting()
    {
        var uiInstance = Get_Setting_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 Setting 的所有实例
    /// </summary>
    public static void Destroy_Setting()
    {
        var uiInstance = Get_Setting_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 Setting 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.Setting.SettingPanel[] Get_Setting_Instance()
    {
        return GetUiInstance<UI.Setting.SettingPanel>(nameof(UI.Setting.Setting));
    }

    /// <summary>
    /// 创建 Settlement, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.Settlement.SettlementPanel Create_Settlement()
    {
        return CreateUi<UI.Settlement.SettlementPanel>(UiNames.Settlement);
    }

    /// <summary>
    /// 打开 Settlement, 并返回UI实例
    /// </summary>
    public static UI.Settlement.SettlementPanel Open_Settlement()
    {
        return OpenUi<UI.Settlement.SettlementPanel>(UiNames.Settlement);
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
    public static void Destroy_Settlement()
    {
        var uiInstance = Get_Settlement_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 Settlement 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.Settlement.SettlementPanel[] Get_Settlement_Instance()
    {
        return GetUiInstance<UI.Settlement.SettlementPanel>(nameof(UI.Settlement.Settlement));
    }

    /// <summary>
    /// 创建 TileSetEditor, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.TileSetEditor.TileSetEditorPanel Create_TileSetEditor()
    {
        return CreateUi<UI.TileSetEditor.TileSetEditorPanel>(UiNames.TileSetEditor);
    }

    /// <summary>
    /// 打开 TileSetEditor, 并返回UI实例
    /// </summary>
    public static UI.TileSetEditor.TileSetEditorPanel Open_TileSetEditor()
    {
        return OpenUi<UI.TileSetEditor.TileSetEditorPanel>(UiNames.TileSetEditor);
    }

    /// <summary>
    /// 隐藏 TileSetEditor 的所有实例
    /// </summary>
    public static void Hide_TileSetEditor()
    {
        var uiInstance = Get_TileSetEditor_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 TileSetEditor 的所有实例
    /// </summary>
    public static void Destroy_TileSetEditor()
    {
        var uiInstance = Get_TileSetEditor_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 TileSetEditor 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.TileSetEditor.TileSetEditorPanel[] Get_TileSetEditor_Instance()
    {
        return GetUiInstance<UI.TileSetEditor.TileSetEditorPanel>(nameof(UI.TileSetEditor.TileSetEditor));
    }

    /// <summary>
    /// 创建 TileSetEditorCombination, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.TileSetEditorCombination.TileSetEditorCombinationPanel Create_TileSetEditorCombination()
    {
        return CreateUi<UI.TileSetEditorCombination.TileSetEditorCombinationPanel>(UiNames.TileSetEditorCombination);
    }

    /// <summary>
    /// 打开 TileSetEditorCombination, 并返回UI实例
    /// </summary>
    public static UI.TileSetEditorCombination.TileSetEditorCombinationPanel Open_TileSetEditorCombination()
    {
        return OpenUi<UI.TileSetEditorCombination.TileSetEditorCombinationPanel>(UiNames.TileSetEditorCombination);
    }

    /// <summary>
    /// 隐藏 TileSetEditorCombination 的所有实例
    /// </summary>
    public static void Hide_TileSetEditorCombination()
    {
        var uiInstance = Get_TileSetEditorCombination_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 TileSetEditorCombination 的所有实例
    /// </summary>
    public static void Destroy_TileSetEditorCombination()
    {
        var uiInstance = Get_TileSetEditorCombination_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 TileSetEditorCombination 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.TileSetEditorCombination.TileSetEditorCombinationPanel[] Get_TileSetEditorCombination_Instance()
    {
        return GetUiInstance<UI.TileSetEditorCombination.TileSetEditorCombinationPanel>(nameof(UI.TileSetEditorCombination.TileSetEditorCombination));
    }

    /// <summary>
    /// 创建 TileSetEditorImport, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.TileSetEditorImport.TileSetEditorImportPanel Create_TileSetEditorImport()
    {
        return CreateUi<UI.TileSetEditorImport.TileSetEditorImportPanel>(UiNames.TileSetEditorImport);
    }

    /// <summary>
    /// 打开 TileSetEditorImport, 并返回UI实例
    /// </summary>
    public static UI.TileSetEditorImport.TileSetEditorImportPanel Open_TileSetEditorImport()
    {
        return OpenUi<UI.TileSetEditorImport.TileSetEditorImportPanel>(UiNames.TileSetEditorImport);
    }

    /// <summary>
    /// 隐藏 TileSetEditorImport 的所有实例
    /// </summary>
    public static void Hide_TileSetEditorImport()
    {
        var uiInstance = Get_TileSetEditorImport_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 TileSetEditorImport 的所有实例
    /// </summary>
    public static void Destroy_TileSetEditorImport()
    {
        var uiInstance = Get_TileSetEditorImport_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 TileSetEditorImport 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.TileSetEditorImport.TileSetEditorImportPanel[] Get_TileSetEditorImport_Instance()
    {
        return GetUiInstance<UI.TileSetEditorImport.TileSetEditorImportPanel>(nameof(UI.TileSetEditorImport.TileSetEditorImport));
    }

    /// <summary>
    /// 创建 TileSetEditorProject, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.TileSetEditorProject.TileSetEditorProjectPanel Create_TileSetEditorProject()
    {
        return CreateUi<UI.TileSetEditorProject.TileSetEditorProjectPanel>(UiNames.TileSetEditorProject);
    }

    /// <summary>
    /// 打开 TileSetEditorProject, 并返回UI实例
    /// </summary>
    public static UI.TileSetEditorProject.TileSetEditorProjectPanel Open_TileSetEditorProject()
    {
        return OpenUi<UI.TileSetEditorProject.TileSetEditorProjectPanel>(UiNames.TileSetEditorProject);
    }

    /// <summary>
    /// 隐藏 TileSetEditorProject 的所有实例
    /// </summary>
    public static void Hide_TileSetEditorProject()
    {
        var uiInstance = Get_TileSetEditorProject_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 TileSetEditorProject 的所有实例
    /// </summary>
    public static void Destroy_TileSetEditorProject()
    {
        var uiInstance = Get_TileSetEditorProject_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 TileSetEditorProject 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.TileSetEditorProject.TileSetEditorProjectPanel[] Get_TileSetEditorProject_Instance()
    {
        return GetUiInstance<UI.TileSetEditorProject.TileSetEditorProjectPanel>(nameof(UI.TileSetEditorProject.TileSetEditorProject));
    }

    /// <summary>
    /// 创建 TileSetEditorTerrain, 并返回UI实例, 该函数不会打开 Ui
    /// </summary>
    public static UI.TileSetEditorTerrain.TileSetEditorTerrainPanel Create_TileSetEditorTerrain()
    {
        return CreateUi<UI.TileSetEditorTerrain.TileSetEditorTerrainPanel>(UiNames.TileSetEditorTerrain);
    }

    /// <summary>
    /// 打开 TileSetEditorTerrain, 并返回UI实例
    /// </summary>
    public static UI.TileSetEditorTerrain.TileSetEditorTerrainPanel Open_TileSetEditorTerrain()
    {
        return OpenUi<UI.TileSetEditorTerrain.TileSetEditorTerrainPanel>(UiNames.TileSetEditorTerrain);
    }

    /// <summary>
    /// 隐藏 TileSetEditorTerrain 的所有实例
    /// </summary>
    public static void Hide_TileSetEditorTerrain()
    {
        var uiInstance = Get_TileSetEditorTerrain_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.HideUi();
        }
    }

    /// <summary>
    /// 销毁 TileSetEditorTerrain 的所有实例
    /// </summary>
    public static void Destroy_TileSetEditorTerrain()
    {
        var uiInstance = Get_TileSetEditorTerrain_Instance();
        foreach (var uiPanel in uiInstance)
        {
            uiPanel.Destroy();
        }
    }

    /// <summary>
    /// 获取所有 TileSetEditorTerrain 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.TileSetEditorTerrain.TileSetEditorTerrainPanel[] Get_TileSetEditorTerrain_Instance()
    {
        return GetUiInstance<UI.TileSetEditorTerrain.TileSetEditorTerrainPanel>(nameof(UI.TileSetEditorTerrain.TileSetEditorTerrain));
    }

}
