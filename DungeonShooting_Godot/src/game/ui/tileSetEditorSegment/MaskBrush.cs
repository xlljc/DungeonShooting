using Godot;

namespace UI.TileSetEditorSegment;

public partial class MaskBrush : Control
{
    /// <summary>
    /// 绑定的地图纹理节点
    /// </summary>
    public TextureRect TileTexture { get; set; }
    
    /// <summary>
    /// 绑定的TileSet编辑区域节点
    /// </summary>
    public TileEditArea TileEditArea { get; set; }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        //绘制texture区域
        if (TileTexture.Texture != null)
        {
            DrawRect(
                new Rect2(Vector2.Zero, TileTexture.Size),
                new Color(1, 1, 0, 0.5f), false,
                2f / TileTexture.Scale.X
            );
        }

        //绘制鼠标悬停区域
        if (TileEditArea.IsMouseInTexture())
        {
            var pos = TileEditArea.GetMouseCellPosition() * GameConfig.TileCellSize;
            DrawRect(
                new Rect2(pos,GameConfig.TileCellSizeVector2I),
                Colors.Green, false, 3f / TileTexture.Scale.X
            );
        }
    }
}