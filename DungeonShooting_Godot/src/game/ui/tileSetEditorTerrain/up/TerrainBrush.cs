using System.Collections.Generic;
using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TerrainBrush : Control, IUiNodeScript
{
    public Control Root { get; set; }
    public List<Control> TerrainTextureList { get; } = new List<Control>();

    private TileSetEditorTerrain.Brush _uiNode;

    public void SetUiNode(IUiNode uiNode)
    {
        _uiNode = (TileSetEditorTerrain.Brush) uiNode;
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
        var scale = Root.Scale;
        var sourceIndex = _uiNode.UiPanel.EditorPanel.TileSetSourceIndex;

        //绘制区域
        for (var i = 0; i < TerrainTextureList.Count; i++)
        {
            if (sourceIndex != 0 && i > 0)
            {
                break;
            }
            var control = TerrainTextureList[i];
            DrawRect(
                new Rect2(control.Position, control.Size.AsVector2I()), new Color(1, 1, 0, 0.5f), false,
                2f / scale.X
            );
        }


        //绘制鼠标悬停区域
        for (var i = 0; i < TerrainTextureList.Count; i++)
        {
            if (sourceIndex != 0 && i > 0)
            {
                break;
            }
            var control = TerrainTextureList[i];
            if (control.IsMouseInRect())
            {
                var pos = Utils.GetMouseCellPosition(control) * GameConfig.TileCellSize;
                DrawRect(
                    new Rect2(pos + control.Position, GameConfig.TileCellSizeVector2I),
                    Colors.Green, false, 3f / scale.X
                );
                break;
            }
        }
    }
}