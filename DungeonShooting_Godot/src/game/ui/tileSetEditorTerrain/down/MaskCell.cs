using Godot;

namespace UI.TileSetEditorTerrain;

public class MaskCell : UiCell<TileSetEditorTerrain.BottomCell, Rect2I>
{
    /// <summary>
    /// 已经赋值并连接的TerrainCell
    /// </summary>
    public TerrainCell ConnectTerrainCell { get; private set; }
    
    /// <summary>
    /// 鼠标是否悬停
    /// </summary>
    public bool Hover { get; set; }
    
    private TextureRect _textureRect;
    private TileSetEditorTerrainPanel _panel;

    public override void OnInit()
    {
        _panel = CellNode.UiPanel;
        _textureRect = _panel.S_BottomBg.L_TileTexture.Instance;
        CellNode.Instance.Draw += Draw;
    }

    public override void OnDisable()
    {
        SetConnectTerrainCell(null);
    }

    public override void Process(float delta)
    {
        CellNode.Instance.QueueRedraw();
    }

    /// <summary>
    /// 设置连接的Cell
    /// </summary>
    public void SetConnectTerrainCell(TerrainCell terrainCell)
    {
        if (terrainCell == null)
        {
            if (ConnectTerrainCell != null)
            {
                ConnectTerrainCell.ConnectMaskCell = null;
            }
            ConnectTerrainCell = null;
        }
        else if (ConnectTerrainCell != terrainCell)
        {
            ConnectTerrainCell = terrainCell;
            terrainCell.ConnectMaskCell = this;
        }
    }

    private void Draw()
    {
        if (Hover || (ConnectTerrainCell != null && ConnectTerrainCell.Hover))
        {
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero, CellNode.Instance.Size),
                new Color(0, 1, 0, 0.3f)
            );
        }
        if (ConnectTerrainCell != null)
        {
            //选中时绘制轮廓
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero, CellNode.Instance.Size),
                new Color(0, 1, 1), false, 2f / _textureRect.Scale.X
            );
        }
    }
}