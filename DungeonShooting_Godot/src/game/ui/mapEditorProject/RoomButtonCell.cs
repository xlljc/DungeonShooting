using Godot;

namespace UI.MapEditorProject;

public class RoomButtonCell : UiCell<MapEditorProject.RoomButton, DungeonRoomSplit>
{
    private bool _focus = false;
    
    protected override void OnInit()
    {
        CellNode.Instance.Pressed += OnClick;
        CellNode.Instance.FocusExited += OnFocusExited;
    }

    protected override void OnSetData(DungeonRoomSplit data)
    {
        _focus = false;
        CellNode.L_RoomName.Instance.Text = data.RoomInfo.RoomName;
        CellNode.L_RoomType.Instance.Text = DungeonManager.DungeonRoomTypeToDescribeString(data.RoomInfo.RoomType);
        CellNode.Instance.TooltipText = "路径: " + data.RoomPath;
        CellNode.Instance.ReleaseFocus();
    }

    protected override void OnDestroy()
    {
        CellNode.Instance.Pressed -= OnClick;
        CellNode.Instance.FocusExited -= OnFocusExited;
    }

    private void OnClick()
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