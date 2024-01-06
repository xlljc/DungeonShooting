using Godot;

namespace UI.TileSetEditorTerrain;

public class TerrainCell : UiCell<TileSetEditorTerrain.RightCell, byte>
{
    public override void OnInit()
    {
        CellNode.Instance.Init(this);
    }
}