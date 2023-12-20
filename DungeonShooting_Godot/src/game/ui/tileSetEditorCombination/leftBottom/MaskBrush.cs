using Godot;

namespace UI.TileSetEditorCombination;

public partial class MaskBrush : Control, IUiNodeScript
{
    /// <summary>
    /// 绑定的地图纹理节点
    /// </summary>
    public TextureRect TileTexture { get; private set; }
    
    /// <summary>
    /// 绑定的TileSet编辑区域节点
    /// </summary>
    public TileEditArea TileEditArea { get; private set; }
    
    private TileSetEditorCombination.MaskBrush _maskBrush;
    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        //绘制texture区域
        if (TileTexture.Texture != null)
        {
            var editorPanel = TileEditArea.UiNode.UiPanel.EditorPanel;
            DrawRect(
                new Rect2(Vector2.Zero, editorPanel.CellHorizontal * GameConfig.TileCellSize, editorPanel.CellVertical * GameConfig.TileCellSize),
                new Color(1, 1, 0, 0.5f), false,
                2f / TileTexture.Scale.X
            );
        }

        //绘制鼠标悬停区域
        if (TileTexture.IsMouseInRect())
        {
            var pos = TileEditArea.GetMouseCellPosition() * GameConfig.TileCellSize;
            DrawRect(
                new Rect2(pos,GameConfig.TileCellSizeVector2I),
                Colors.Green, false, 3f / TileTexture.Scale.X
            );
        }
    }

    public void SetUiNode(IUiNode uiNode)
    {
        _maskBrush = (TileSetEditorCombination.MaskBrush)uiNode;
        TileTexture = _maskBrush.UiPanel.S_TileTexture.Instance;
        TileEditArea = _maskBrush.UiPanel.S_LeftBottomBg.Instance;
    }

    public void OnDestroy()
    {
        
    }
}