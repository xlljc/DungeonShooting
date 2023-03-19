/**
 * 该类为自动生成的, 请不要手动编辑, 以免造成代码丢失
 */
public static partial class UiManager
{

    public static class UiName
    {
        public const string EditorTools = "EditorTools";
        public const string RoomUI = "RoomUI";
    }

    /// <summary>
    /// 打开 EditorTools, 并返回UI实例
    /// </summary>
    public static UI.EditorTools.EditorToolsPanel Open_EditorTools()
    {
        return OpenUi<UI.EditorTools.EditorToolsPanel>(UiName.EditorTools);
    }

    /// <summary>
    /// 获取所有 EditorTools 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.EditorTools.EditorToolsPanel[] Get_EditorTools_Instance()
    {
        return GetUiInstance<UI.EditorTools.EditorToolsPanel>(nameof(UI.EditorTools.EditorTools));
    }

    /// <summary>
    /// 打开 RoomUI, 并返回UI实例
    /// </summary>
    public static UI.RoomUI.RoomUIPanel Open_RoomUI()
    {
        return OpenUi<UI.RoomUI.RoomUIPanel>(UiName.RoomUI);
    }

    /// <summary>
    /// 获取所有 RoomUI 的实例, 如果没有实例, 则返回一个空数组
    /// </summary>
    public static UI.RoomUI.RoomUIPanel[] Get_RoomUI_Instance()
    {
        return GetUiInstance<UI.RoomUI.RoomUIPanel>(nameof(UI.RoomUI.RoomUI));
    }

}
