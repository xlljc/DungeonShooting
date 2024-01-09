using Godot;
using UI.TileSetEditor;

namespace UI.TileSetEditorTerrain;

public partial class TileSetEditorTerrainPanel : TileSetEditorTerrain
{
    /// <summary>
    /// 父Ui
    /// </summary>
    public TileSetEditorPanel EditorPanel;
    
    /// <summary>
    /// 是否正在拖拽图块
    /// </summary>
    public bool IsDraggingCell { get; set; }

    private UiGrid<RightCell, byte> _topGrid1;
    private UiGrid<RightCell, byte> _topGrid2;
    private UiGrid<RightCell, byte> _topGrid3;
    private UiGrid<BottomCell, Rect2I> _bottomGrid;
    
    public override void OnCreateUi()
    {
        EditorPanel = (TileSetEditorPanel)ParentUi;
        
        //改变选中的TileSet资源
        AddEventListener(EventEnum.OnSelectTileSetSource, OnSelectTileSetSource);
        //改变纹理事件
        AddEventListener(EventEnum.OnSetTileTexture, OnSetTileTexture);
        //背景颜色改变
        AddEventListener(EventEnum.OnSetTileSetBgColor, OnChangeTileSetBgColor);
        
        _bottomGrid = CreateUiGrid<BottomCell, Rect2I, MaskCell>(S_BottomCell);
        _bottomGrid.SetCellOffset(Vector2I.Zero);

        _topGrid1 = InitTopGrid(S_TerrainRoot.L_TerrainTexture1.Instance, GameConfig.TerrainBitSize1, 1);
        _topGrid2 = InitTopGrid(S_TerrainRoot.L_TerrainTexture2.Instance, GameConfig.TerrainBitSize2, 2);
        _topGrid3 = InitTopGrid(S_TerrainRoot.L_TerrainTexture3.Instance, GameConfig.TerrainBitSize3, 3);

        OnSetTileTexture(EditorPanel.Texture);
        OnChangeTileSetBgColor(EditorPanel.BgColor);
    }

    public override void OnDestroyUi()
    {
        
    }

    public override void Process(float delta)
    {
        S_MaskBrush.Instance.Visible = !IsDraggingCell;
    }

    private UiGrid<RightCell, byte> InitTopGrid(Control texture, Vector2I size, byte type)
    {
        var cellRoot = S_TopBg.L_TerrainRoot.L_CellRoot;
        var sRightCell = cellRoot.L_RightCell;
        sRightCell.Instance.Position = texture.Position;
        var grid = CreateUiGrid<RightCell, byte, TerrainCell>(sRightCell, cellRoot.Instance);
        grid.SetCellOffset(Vector2I.Zero);
        grid.SetColumns(size.X);
        for (var y = 0; y < size.Y; y++)
        {
            for (var x = 0; x < size.X; x++)
            {
                grid.Add(type);
            }
        }
        
        return grid;
    }

    //改变选中的TileSet资源
    private void OnSelectTileSetSource(object obj)
    {
        //先清除所有绑定的Terrain
        _topGrid1.ForEach(cell => cell.CellNode.Instance.ClearCell());
        _topGrid2.ForEach(cell => cell.CellNode.Instance.ClearCell());
        _topGrid3.ForEach(cell => cell.CellNode.Instance.ClearCell());
        
        //再加载Terrain
        if (obj != null)
        {
            var terrain = ((TileSetSourceInfo)obj).Terrain;
            _topGrid1.ForEach(cell =>
            {
                var ints = terrain.GetTerrainCell( cell.Index, cell.Data);
                if (ints != null)
                {
                    cell.CellNode.Instance.SetCell(new Rect2I(ints[0], ints[1], GameConfig.TileCellSize, GameConfig.TileCellSize));
                }
            });
            _topGrid2.ForEach(cell =>
            {
                var ints = terrain.GetTerrainCell(cell.Index, cell.Data);
                if (ints != null)
                {
                    cell.CellNode.Instance.SetCell(new Rect2I(ints[0], ints[1], GameConfig.TileCellSize, GameConfig.TileCellSize));
                }
            });
            _topGrid3.ForEach(cell =>
            {
                var ints = terrain.GetTerrainCell( cell.Index, cell.Data);
                if (ints != null)
                {
                    cell.CellNode.Instance.SetCell(new Rect2I(ints[0], ints[1], GameConfig.TileCellSize, GameConfig.TileCellSize));
                }
            });
        }
    }
    
    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        S_BottomBg.Instance.OnChangeTileSetTexture();

        _bottomGrid.RemoveAll();
        var cellHorizontal = EditorPanel.CellHorizontal;
        if (cellHorizontal <= 0)
        {
            return;
        }
        var cellVertical = EditorPanel.CellVertical;
        _bottomGrid.SetColumns(cellHorizontal);
        for (var y = 0; y < cellVertical; y++)
        {
            for (var x = 0; x < cellHorizontal; x++)
            {
                _bottomGrid.Add(new Rect2I(x * GameConfig.TileCellSize, y * GameConfig.TileCellSize, GameConfig.TileCellSize, GameConfig.TileCellSize));
            }
        }
    }
    
    //更改背景颜色
    private void OnChangeTileSetBgColor(object obj)
    {
        S_BottomBg.Instance.Color = EditorPanel.BgColor;
        S_TopBg.Instance.Color = EditorPanel.BgColor;
    }
}
