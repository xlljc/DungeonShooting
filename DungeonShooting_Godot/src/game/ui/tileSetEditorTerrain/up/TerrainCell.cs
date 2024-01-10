using Godot;

namespace UI.TileSetEditorTerrain;

public class TerrainCell : UiCell<TileSetEditorTerrain.RightCell, byte>
{
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
    }

    public bool OnDropCell(MaskCell maskCell)
    {
        if (CellNode.Instance.IsMouseInRect())
        {
            OnDropData(maskCell);
            return true;
        }

        return false;
    }

    private void OnGuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.Pressed) //右键擦除图块
            {
                CellNode.Instance.AcceptEvent();
                var flag = IsPutDownTexture;
                ClearCell();
                if (flag)
                {
                    SetTerrainBitData(null);
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

    private void OnDropData(MaskCell maskCell)
    {
        SetCell(maskCell.Data);
        SetTerrainBitData(new []{ TextureCell.X, TextureCell.Y });
    }
    
    private void SetTerrainBitData(int[] cellData)
    {
        if (cellData == null)
        {
            _panel.EditorPanel.TileSetSourceInfo.Terrain.RemoveTerrainCell(Index, Data);
        }
        else
        {
            _panel.EditorPanel.TileSetSourceInfo.Terrain.SetTerrainCell(Index, Data, cellData);
        }

        EventManager.EmitEvent(EventEnum.OnTileSetDirty);
    }
}