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
        CellNode.Instance.GuiInput += OnGuiInput;
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

    private void OnGuiInput(InputEvent @event)
    {
        if (IsPutDownTexture && @event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.Pressed) //右键擦除图块
            {
                CellNode.Instance.AcceptEvent();
                var flag = IsPutDownTexture;
                ClearCell();
                if (flag)
                {
                    ClearTerrainBitData();
                    if (ConnectMaskCell != null)
                    {
                        ConnectMaskCell.SetConnectTerrainCell(null, -1);
                    }
                }
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
    public void ClearCell()
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
                ConnectMaskCell.SetConnectTerrainCell(null, -1);
            }
            maskCell.SetConnectTerrainCell(this, CellNode.UiPanel.CurrTerrainIndex);
        }
    }
    
    private void SetTerrainBitData(int[] cellData)
    {
        if (cellData == null)
        {
            _panel.CurrTerrain?.RemoveTerrainCell(Index, Data);
        }
        else
        {
            _panel.CurrTerrain?.SetTerrainCell(Index, Data, cellData);
        }

        EventManager.EmitEvent(EventEnum.OnTileSetDirty);
    }
    
    private void OnDraw()
    {
        if (ConnectMaskCell != null && (Hover || (ConnectMaskCell.Hover && CellNode.UiPanel.CurrTerrainIndex == ConnectMaskCell.ConnectTerrainIndex)))
        {
            CellNode.Instance.DrawRect(
                new Rect2(Vector2.Zero, CellNode.Instance.Size),
                new Color(0, 1, 0, 0.2f)
            );
        }
    }
}