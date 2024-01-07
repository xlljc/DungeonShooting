using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TerrainCellDropHandler : Control
{
    /// <summary>
    /// 是否放置了图块
    /// </summary>
    public bool IsPutDownTexture { get; private set; }
    
    /// <summary>
    /// 图块在 Source 中的位置, 单位: 像素
    /// </summary>
    public Vector2I TextureCell { get; private set; }
    
    private TerrainCell _cell;
    private TileSetEditorTerrainPanel _panel;
    
    public void Init(TerrainCell cell)
    {
        _cell = cell;
        _panel = cell.CellNode.UiPanel;
    }
    
    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return data.VariantType == Variant.Type.Rect2I;
    }
    
    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var rect = data.AsRect2I();
        SetCell(rect);
        SetTerrainBitData(new []{ TextureCell.X, TextureCell.Y });
    }

    public override void _GuiInput(InputEvent @event)
    {
        //右键擦除图块
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.Pressed)
        {
            AcceptEvent();
            var flag = IsPutDownTexture;
            ClearCell();
            if (flag)
            {
                SetTerrainBitData(null);
            }
        }
    }

    /// <summary>
    /// 设置选择的Cell
    /// </summary>
    public void SetCell(Rect2I rect)
    {
        TextureCell = rect.Position;
        var sprite2D = _cell.CellNode.L_CellTexture.Instance;
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
        _cell.CellNode.L_CellTexture.Instance.Texture = null;
        IsPutDownTexture = false;
    }

    private void SetTerrainBitData(int[] cellData)
    {
        EditorTileSetManager.SetTileSetTerrainBit(_panel.EditorPanel.TileSetSourceInfo.Terrain, _cell.Index, _cell.Data, cellData);
        EventManager.EmitEvent(EventEnum.OnTileSetDirty);
    }
}