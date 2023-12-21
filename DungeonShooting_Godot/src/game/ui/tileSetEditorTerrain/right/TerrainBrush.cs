using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TerrainBrush : Control, IUiNodeScript
{
    public TileSetEditorTerrain.TileTexture_1 TileTexture { get; set; }
    
    private TileSetEditorTerrain.Brush _brush;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _brush = (TileSetEditorTerrain.Brush)uiNode;
    }

    public void OnDestroy()
    {
        
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        DrawRect(
            new Rect2(GameConfig.TileCellSizeVector2I,
                TileTexture.Instance.Texture.GetSize() - GameConfig.TileCellSizeVector2I * 2), Colors.Green, false,
            2f / TileTexture.Instance.Scale.X);
    }
}