using Godot;

namespace UI.MapEditorProject;

public class RoomButtonCell : UiCell<MapEditorProject.RoomButton, MapProjectManager.MapRoomInfo>
{
    private bool _focus = false;
    
    protected override void OnInit()
    {
        CellNode.Instance.Pressed += OnClick;
        CellNode.Instance.FocusExited += OnFocusExited;
    }

    protected override void OnSetData(MapProjectManager.MapRoomInfo data)
    {
        _focus = false;
        CellNode.L_RoomName.Instance.Text = data.Name;
        CellNode.L_RoomType.Instance.Text = DungeonManager.DungeonRoomTypeToDescribeString(data.RoomType);
        CellNode.Instance.TooltipText = "路径: " + data.FullPath;
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