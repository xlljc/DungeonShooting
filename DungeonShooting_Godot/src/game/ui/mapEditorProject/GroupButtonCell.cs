namespace UI.MapEditorProject;

public class GroupButtonCell : UiCell<MapEditorProject.GroupButton, string>
{
    protected override void OnSetData(string data)
    {
        CellNode.Instance.Text = data;
    }
}