using Godot;

namespace UI.MapEditorProject;

public class RoomButtonCell : UiCell<MapEditorProject.RoomButton, DungeonRoomSplit>
{
    public override void OnSetData(DungeonRoomSplit data)
    {
        CellNode.L_RoomName.Instance.Text = data.RoomInfo.RoomName;
        CellNode.L_RoomType.Instance.Text = DungeonManager.DungeonRoomTypeToDescribeString(data.RoomInfo.RoomType);
        CellNode.Instance.TooltipText = "路径: " + data.RoomPath;
    }

    public override void OnDoubleClick()
    {
        //打开房间编辑器
        CellNode.UiPanel.SelectRoom(Data);
    }
}