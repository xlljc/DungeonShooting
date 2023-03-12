
using UI.RoomUI;

public static partial class UiManager
{
    public static RoomUIPanel Open_RoomUI()
    {
        return OpenUi<RoomUIPanel>(ResourcePath.prefab_ui_RoomUI_tscn);
    }
}