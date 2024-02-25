using Godot;

namespace UI.EditorTileImage;

public partial class RectBrush : Control, IUiNodeScript
{
    private Control _parent;
    private EditorTileImage.Brush UiNode;
    
    public void SetUiNode(IUiNode uiNode)
    {
        UiNode = (EditorTileImage.Brush)uiNode;
        _parent = GetParent<Control>();
    }

    public void OnDestroy()
    {
        
    }
    public override void _Process(double delta)
    {
        if (UiNode.UiPanel.UseImage != null)
        {
            QueueRedraw();
        }
    }

    public override void _Draw()
    {
        var panel = UiNode.UiPanel;
        if (panel.UseImage != null)
        {
            var sx = _parent.Scale.X;
            var size = panel.ImageSize;
            var lineWidth = 2f / sx;
            var lineWidthHalf = new Vector2(lineWidth / 2f, lineWidth / 2f);
            DrawRect(new Rect2(Vector2.Zero, size), Colors.Yellow, false, lineWidth);

            var start = new Vector2(panel.StartXValue, panel.StartYValue);
            for (int i = 0; i < panel.HCountValue; i++)
            {
                for (int j = 0; j < panel.VCountValue; j++)
                {
                    var offset = new Vector2(i * (panel.OffsetXValue + GameConfig.TileCellSize), j * (panel.OffsetYValue + GameConfig.TileCellSize));
                    DrawRect(
                        new Rect2(
                            start + offset + lineWidthHalf,
                            new Vector2(GameConfig.TileCellSize, GameConfig.TileCellSize) - new Vector2(lineWidth, lineWidth)
                        ),
                        new Color(0, 1, 0, 0.5f), false, lineWidth
                    );
                }
            }
        }
    }
}