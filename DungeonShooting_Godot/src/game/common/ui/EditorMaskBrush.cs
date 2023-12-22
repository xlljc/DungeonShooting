using Godot;
using UI.TileSetEditor;

/// <summary>
/// 地图编辑器通用遮罩笔刷
/// </summary>
public partial class EditorMaskBrush : Control
{
    /// <summary>
    /// 绑定的地图纹理节点
    /// </summary>
    public TextureRect TileTexture { get; private set; }
    
    /// <summary>
    /// 绑定的 TileSetEditorPanel Ui
    /// </summary>
    public TileSetEditorPanel TileSetEditorPanel { get; private set; }

    /// <summary>
    /// 初始化笔刷数绑定的节点
    /// </summary>
    public void Init(TextureRect tileTexture, TileSetEditorPanel tileSetEditorPanel)
    {
        TileTexture = tileTexture;
        TileSetEditorPanel = tileSetEditorPanel;
    }
    
    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (TileTexture == null)
        {
            return;
        }
        //绘制texture区域
        if (TileTexture.Texture != null)
        {
            DrawRect(
                new Rect2(Vector2.Zero, TileSetEditorPanel.CellHorizontal * GameConfig.TileCellSize, TileSetEditorPanel.CellVertical * GameConfig.TileCellSize),
                new Color(1, 1, 0, 0.5f), false,
                2f / TileTexture.Scale.X
            );
        }

        //绘制鼠标悬停区域
        if (TileTexture.IsMouseInRect())
        {
            var pos = Utils.GetMouseCellPosition(TileTexture) * GameConfig.TileCellSize;
            DrawRect(
                new Rect2(pos,GameConfig.TileCellSizeVector2I),
                Colors.Green, false, 3f / TileTexture.Scale.X
            );
        }
    }
}