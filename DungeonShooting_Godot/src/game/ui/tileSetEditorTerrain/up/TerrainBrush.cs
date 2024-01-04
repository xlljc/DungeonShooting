using System.Collections.Generic;
using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TerrainBrush : Control
{
    public Control Root { get; set; }
    public List<Control> TerrainTextureList { get; } = new List<Control>();
    

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        var scale = Root.Scale;
        //绘制区域
        foreach (var control in TerrainTextureList)
        {
            DrawRect(
                new Rect2(control.Position, control.Size.AsVector2I()), new Color(1, 1, 0, 0.5f), false,
                2f / scale.X
            );
        }

        
        //绘制鼠标悬停区域
        foreach (var control in TerrainTextureList)
        {
            if (control.IsMouseInRect())
            {
                var pos = Utils.GetMouseCellPosition(control) * GameConfig.TileCellSize;
                DrawRect(
                    new Rect2(pos + control.Position,GameConfig.TileCellSizeVector2I),
                    Colors.Green, false, 3f / scale.X
                );
                break;
            }
        }
    }
}