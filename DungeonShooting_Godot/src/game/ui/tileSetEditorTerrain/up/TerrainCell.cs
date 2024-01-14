using Godot;

namespace UI.TileSetEditorTerrain;

public class TerrainCell : UiCell<TileSetEditorTerrain.RightCell, byte>
{
    /// <summary>
    /// 已经赋值并连接的MaskCell
    /// </summary>
    public MaskCell ConnectMaskCell { get; set; }
    
    /// <summary>
    /// 鼠标是否悬停
    /// </summary>
    public bool Hover { get; set; }
    
    /// <summary>
    /// 是否放置了图块
    /// </summary>
    public bool IsPutDownTexture { get; private set; }
    
    /// <summary>
    /// 图块在 Source 中的位置, 单位: 像素
    /// </summary>
    public Vector2I TextureCell { get; private set; }
    
    private TileSetEditorTerrainPanel _panel;
    
    public override void OnInit()
    {
        _panel = CellNode.UiPanel;
        CellNode.Instance.Draw += OnDraw;
    }

    /// <summary>
    /// 拖拽放置Cell
    /// </summary>
    public bool OnDropCell(MaskCell maskCell)
    {
        if (CellNode.Instance.IsMouseInRect())
        {
            OnDropData(maskCell);
            return true;
        }

        return false;
    }

    public override void Process(float delta)
    {
        CellNode.Instance.QueueRedraw();
    }
    
    /// <summary>
    /// 擦除当前选择的图块
    /// </summary>
    public void EraseCell()
    {
        var flag = IsPutDownTexture;
        ClearCellTexture();
        if (flag)
        {
            ClearTerrainBitData();
            if (ConnectMaskCell != null)
            {
                ConnectMaskCell.SetUseFlag(false);
                ConnectMaskCell.SetConnectTerrainCell(null);
            }
        }
    }

    /// <summary>
    /// 设置选择的Cell
    /// </summary>
    public void SetCell(Rect2I rect)
    {
        TextureCell = rect.Position;
        var sprite2D = CellNode.L_CellTexture.Instance;
        sprite2D.Texture = _panel.EditorPanel.Texture;
        sprite2D.RegionEnabled = true;
        sprite2D.RegionRect = rect;
        IsPutDownTexture = true;
    }

    /// <summary>
    /// 清除选中的cell
    /// </summary>
    public void ClearCellTexture()
    {
        CellNode.L_CellTexture.Instance.Texture = null;
        IsPutDownTexture = false;
    }

    /// <summary>
    /// 清除存储的地形掩码数据
    /// </summary>
    public void ClearTerrainBitData()
    {
        SetTerrainBitData(null);
    }

    private void OnDropData(MaskCell maskCell)
    {
        SetCell(maskCell.Data);
        SetTerrainBitData(new []{ TextureCell.X, TextureCell.Y });
        if (ConnectMaskCell != maskCell)
        {
            if (ConnectMaskCell != null)
            {
                ConnectMaskCell.SetUseFlag(false);
                ConnectMaskCell.SetConnectTerrainCell(null);
            }
            maskCell.SetConnectTerrainCell(this);
            maskCell.SetUseFlag(true);
        }
    }
    
    private void SetTerrainBitData(int[] cellData)
    {
        if (cellData == null)
        {
            _panel.CurrTerrain?.RemoveTerrainCell(Index, Data);
            _panel.CurrTerrain?.RefreshReady(_panel.CurrTerrainIndex);
        }
        else
        {
            _panel.CurrTerrain?.SetTerrainCell(Index, Data, cellData);
            _panel.CurrTerrain?.RefreshReady(_panel.CurrTerrainIndex);
        }

        EventManager.EmitEvent(EventEnum.OnTileSetDirty);
    }
    
    private void OnDraw()
    {
        if (ConnectMaskCell != null && (Hover || ConnectMaskCell.Hover))
        {
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero, CellNode.Instance.Size),
                new Color(0, 1, 0, 0.2f)
            );
        }
    }
}