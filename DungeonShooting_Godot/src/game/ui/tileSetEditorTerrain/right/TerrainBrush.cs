using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TerrainBrush : Control
{
    public TextureRect TileTexture { get; set; }
    

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        //绘制区域
        DrawRect(
            new Rect2(GameConfig.TileCellSizeVector2I,
                TileTexture.Texture.GetSize() - GameConfig.TileCellSizeVector2I * 2), new Color(1, 1, 0, 0.5f), false,
            2f / TileTexture.Scale.X);
        
        //绘制鼠标悬停区域
        if (TileTexture.IsMouseInRect(GameConfig.TileCellSize))
        {
            var pos = Utils.GetMouseCellPosition(TileTexture) * GameConfig.TileCellSize;
            DrawRect(
                new Rect2(pos,GameConfig.TileCellSizeVector2I),
                Colors.Green, false, 3f / TileTexture.Scale.X
            );
        }
    }
}