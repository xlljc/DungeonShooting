/**
 * 该类为自动生成的, 请不要手动编辑, 以免造成代码丢失
 */
public static partial class UiManager
{

    public static UI.EditorTools.EditorToolsPanel Open_EditorTools()
    {
        return OpenUi<UI.EditorTools.EditorToolsPanel>(ResourcePath.prefab_ui_EditorTools_tscn);
    }

    public static UiBase[] Get_EditorTools_Instance()
    {
        return GetUiInstance(nameof(UI.EditorTools.EditorTools));
    }

    public static UI.RoomUI.RoomUIPanel Open_RoomUI()
    {
        return OpenUi<UI.RoomUI.RoomUIPanel>(ResourcePath.prefab_ui_RoomUI_tscn);
    }

    public static UiBase[] Get_RoomUI_Instance()
    {
        return GetUiInstance(nameof(UI.RoomUI.RoomUI));
    }

}
