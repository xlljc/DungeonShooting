using Godot;

namespace UI.TileSetEditorTerrain;

public class TerrainCell : UiCell<TileSetEditorTerrain.RightCell, bool>
{
    public override void OnInit()
    {
        CellNode.Instance.Init(this);
    }
}