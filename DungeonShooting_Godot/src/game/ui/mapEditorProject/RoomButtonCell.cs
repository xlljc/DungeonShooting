using Godot;

namespace UI.MapEditorProject;

public class RoomButtonCell : UiCell<MapEditorProject.RoomButton, string>
{
    protected override void OnInit()
    {
        CellNode.Instance.Pressed += OnClick;
    }

    protected override void OnSetData(string data)
    {
        CellNode.L_RoomName.Instance.Text = data;
    }

    protected override void OnDestroy()
    {
        CellNode.Instance.Pressed -= OnClick;
    }

    private void OnClick()
    {
        GD.Print("点击了按钮: " + Data);
    }
}