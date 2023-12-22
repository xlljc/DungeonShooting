using Godot;

namespace UI.TileSetEditorTerrain;

public class MaskCell : UiCell<TileSetEditorTerrain.LeftCell, Rect2I>
{
    public override void OnInit()
    {
        CellNode.Instance.Init(this);
    }

    public override void OnSetData(Rect2I data)
    {
        CellNode.Instance.SetRect(data);
    }
    
    public override void OnSelect()
    {
        CellNode.Instance.DragOutline = true;
    }

    public override void OnUnSelect()
    {
        CellNode.Instance.DragOutline = false;
    }
}