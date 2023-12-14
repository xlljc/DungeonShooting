using Godot;

namespace UI.TileSetEditorSegment;

public class MaskRectCell : UiCell<TileSetEditorSegment.MaskRect, bool>
{
    public override void OnSetData(bool data)
    {
        CellNode.Instance.Color = data ? new Color(0, 0, 0, 0) : new Color(0, 0, 0, 0.5882353F);
    }
}