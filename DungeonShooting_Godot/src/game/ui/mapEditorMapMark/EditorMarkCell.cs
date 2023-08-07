using System.Collections.Generic;

namespace UI.MapEditorMapMark;

public class EditorMarkCell : UiCell<MapEditorMapMark.MarkItem, MarkInfo>
{
    public override void OnInit()
    {
        CellNode.L_MarkButton.Instance.Pressed += OnClick;
    }

    public override void OnClick()
    {
        CellNode.UiPanel.SetSelectCell(this, CellNode.Instance, MapEditorMapMarkPanel.SelectToolType.Mark);
    }

    public override void OnSelect()
    {
        CellNode.L_MarkButton.L_Select.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_MarkButton.L_Select.Instance.Visible = false;
    }
}