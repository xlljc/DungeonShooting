using Godot;

namespace UI.TileSetEditorTerrain;

public class MaskCell : UiCell<TileSetEditorTerrain.BottomCell, Rect2I>
{
    /// <summary>
    /// 已经赋值并连接的TerrainCell
    /// </summary>
    public TerrainCell ConnectTerrainCell { get; private set; }
    
    /// <summary>
    /// 是否使用
    /// </summary>
    public bool UseFlag { get; private set; }
    
    /// <summary>
    /// 鼠标是否悬停
    /// </summary>
    public bool Hover { get; set; }
    
    private TextureRect _textureRect;
    private TileSetEditorTerrainPanel _panel;
    private float _startA;

    public override void OnInit()
    {
        _startA = CellNode.Instance.Color.A;
        _panel = CellNode.UiPanel;
        _textureRect = _panel.S_BottomBg.L_TileTexture.Instance;
        CellNode.Instance.Draw += Draw;
    }

    public override void OnDisable()
    {
        SetUseFlag(false);
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
    
    /// <summary>
    /// 设置是否使用
    /// </summary>
    public void SetUseFlag(bool flag)
    {
        UseFlag = flag;
        CellNode.Instance.Color = new Color(0, 0, 0, flag ? 0 : _startA);
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
        
        //选中时绘制轮廓
        if (ConnectTerrainCell != null) //存在连接的Cell
        {
            var width = 2f / _textureRect.Scale.X;
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero + new Vector2(width / 2f, width / 2f), CellNode.Instance.Size - new Vector2(width, width)),
                new Color(0, 1, 0, 0.6f), false, width
            );
        }
        else if (UseFlag)
        {
            var width = 2f / _textureRect.Scale.X;
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero + new Vector2(width / 2f, width / 2f), CellNode.Instance.Size - new Vector2(width, width)),
                new Color(1, 1, 1, 0.6f), false, width
            );
        }
    }
}