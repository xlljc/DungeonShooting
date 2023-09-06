namespace UI.MapEditorSelectObject;

public class TypeButtonCell : UiCell<MapEditorSelectObject.TypeButton, MapEditorSelectObjectPanel.TypeButtonData>
{
    public override void OnInit()
    {
        CellNode.L_Select.Instance.Visible = false;
    }

    public override void OnSetData(MapEditorSelectObjectPanel.TypeButtonData data)
    {
        CellNode.Instance.Text = data.Name;
    }

    public override void OnSelect()
    {
        CellNode.L_Select.Instance.Visible = true;
        CellNode.UiPanel.OnSearch();
    }

    public override void OnUnSelect()
    {
        CellNode.L_Select.Instance.Visible = false;
    }
}