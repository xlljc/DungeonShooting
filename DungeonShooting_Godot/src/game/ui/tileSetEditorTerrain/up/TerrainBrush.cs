using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TerrainBrush : Control
{
    public Control TerrainRoot { get; set; }
    

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        //绘制区域
        DrawRect(
            new Rect2(Vector2.Zero, TerrainRoot.Size.AsVector2I()), new Color(1, 1, 0, 0.5f), false,
            2f / TerrainRoot.Scale.X
        );
        
        //绘制鼠标悬停区域
        if (TerrainRoot.IsMouseInRect())
        {
            var pos = Utils.GetMouseCellPosition(TerrainRoot) * GameConfig.TileCellSize;
            DrawRect(
                new Rect2(pos,GameConfig.TileCellSizeVector2I),
                Colors.Green, false, 3f / TerrainRoot.Scale.X
            );
        }
    }
}