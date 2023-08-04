using Godot;

namespace UI.MapEditorProject;

public class RoomButtonCell : UiCell<MapEditorProject.RoomButton, DungeonRoomSplit>
{
    private bool _focus = false;

    public override void OnInit()
    {
        CellNode.Instance.FocusExited += OnFocusExited;
    }

    public override void OnSetData(DungeonRoomSplit data)
    {
        _focus = false;
        CellNode.L_RoomName.Instance.Text = data.RoomInfo.RoomName;
        CellNode.L_RoomType.Instance.Text = DungeonManager.DungeonRoomTypeToDescribeString(data.RoomInfo.RoomType);
        CellNode.Instance.TooltipText = "路径: " + data.RoomPath;
        CellNode.Instance.ReleaseFocus();
    }

    public override void OnDestroy()
    {
        CellNode.Instance.FocusExited -= OnFocusExited;
    }

    public override void OnClick()
    {
        if (_focus)
        {
            //打开房间编辑器
            ((MapEditorProjectPanel)CellNode.UiPanel).SelectRoom(Data);
            CellNode.Instance.ReleaseFocus();
        }
        else
        {
            _focus = true;
        }
    }
    
    private void OnFocusExited()
    {
        _focus = false;
    }
}