namespace UI.MapEditorTools;

public class ToolButtonCell : UiCell<MapEditorTools.ToolButton, MapEditorToolsPanel.ToolBtnData>
{
    public override void OnInit()
    {
        CellNode.L_Select.Instance.Visible = false;
    }

    public override void OnSetData(MapEditorToolsPanel.ToolBtnData data)
    {
        CellNode.Instance.TextureNormal = ResourceManager.LoadTexture2D(data.Icon);
    }

    public override bool CanSelect()
    {
        return Data.CanSelect;
    }

    public override void OnSelect()
    {
        CellNode.L_Select.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_Select.Instance.Visible = false;
    }

    public override void OnClick()
    {
        Data.OnClick();
    }

}