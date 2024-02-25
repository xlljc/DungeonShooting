namespace UI.TileSetEditor;

public class TileSetEditorTabCell : UiCell<TileSetEditor.Tab, TileSetEditorTabData>
{
    private UiBase _uiInstance;

    public override void OnInit()
    {
        CellNode.L_Select.Instance.Visible = false;
        CellNode.L_ErrorIcon.Instance.Visible = false;
    }

    public override void OnSetData(TileSetEditorTabData data)
    {
        CellNode.Instance.Text = data.Text;
        _uiInstance = CellNode.UiPanel.S_RightRoot.OpenNestedUi(data.UiName);
        _uiInstance.HideUi();
    }

    public override void OnSelect()
    {
        CellNode.L_Select.Instance.Visible = true;
        _uiInstance.ShowUi();
    }

    public override void OnUnSelect()
    {
        CellNode.L_Select.Instance.Visible = false;
        _uiInstance.HideUi();
    }
}