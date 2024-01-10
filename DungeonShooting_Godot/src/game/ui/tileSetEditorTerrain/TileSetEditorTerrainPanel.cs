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
    /// 正在拖拽的图块
    /// </summary>
    public MaskCell DraggingCell { get; set; }

    public UiGrid<RightCell, byte> TopGrid1;
    public UiGrid<RightCell, byte> TopGrid2;
    public UiGrid<RightCell, byte> TopGrid3;
    public UiGrid<BottomCell, Rect2I> BottomGrid;

    private bool _refreshGridConnect = false;

    public override void OnCreateUi()
    {
        EditorPanel = (TileSetEditorPanel)ParentUi;
        S_DragSprite.Instance.Visible = false;
        
        //改变选中的TileSet资源
        AddEventListener(EventEnum.OnSelectTileSetSource, OnSelectTileSetSource);
        //改变纹理事件
        AddEventListener(EventEnum.OnSetTileTexture, OnSetTileTexture);
        //背景颜色改变
        AddEventListener(EventEnum.OnSetTileSetBgColor, OnChangeTileSetBgColor);
        
        BottomGrid = CreateUiGrid<BottomCell, Rect2I, MaskCell>(S_BottomCell);
        BottomGrid.SetCellOffset(Vector2I.Zero);
        BottomGrid.GridContainer.MouseFilter = MouseFilterEnum.Ignore;

        TopGrid1 = InitTopGrid(S_TerrainRoot.L_TerrainTexture1.Instance, GameConfig.TerrainBitSize1, 1);
        TopGrid2 = InitTopGrid(S_TerrainRoot.L_TerrainTexture2.Instance, GameConfig.TerrainBitSize2, 2);
        TopGrid3 = InitTopGrid(S_TerrainRoot.L_TerrainTexture3.Instance, GameConfig.TerrainBitSize3, 3);

        OnSetTileTexture(EditorPanel.Texture);
        OnChangeTileSetBgColor(EditorPanel.BgColor);
    }

    public override void OnDestroyUi()
    {
        
    }

    public override void Process(float delta)
    {
        S_MaskBrush.Instance.Visible = DraggingCell == null;

        if (_refreshGridConnect)
        {
            _refreshGridConnect = true;

            var terrain = EditorPanel.TileSetSourceInfo.Terrain;
            TopGrid1.ForEach(cell => RefreshConnectTerrainCell(terrain, cell));
            if (EditorPanel.TileSetSourceIndex == 0) //必须选中Main Source
            {
                TopGrid2.ForEach(cell => RefreshConnectTerrainCell(terrain, cell));
                TopGrid3.ForEach(cell => RefreshConnectTerrainCell(terrain, cell));
            }
        }
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
        TopGrid1.ForEach(cell => ((TerrainCell)cell).ClearCell());
        TopGrid2.ForEach(cell => ((TerrainCell)cell).ClearCell());
        TopGrid3.ForEach(cell => ((TerrainCell)cell).ClearCell());
        S_TopBg.Instance.SetHoverCell(null);
        S_BottomBg.Instance.SetHoverCell(null);
        
        //再加载Terrain
        var sourceIndex = EditorPanel.TileSetSourceIndex;
        if (obj != null)
        {
            var terrain = ((TileSetSourceInfo)obj).Terrain;
            TopGrid1.ForEach(cell => SetTerrainCellData(terrain, cell));
            if (sourceIndex == 0) //必须选中Main Source
            {
                S_TerrainTexture2.Instance.Visible = true;
                S_TerrainTexture3.Instance.Visible = true;
                TopGrid2.ForEach(cell => SetTerrainCellData(terrain, cell));
                TopGrid3.ForEach(cell => SetTerrainCellData(terrain, cell));
            }
            else
            {
                S_TerrainTexture2.Instance.Visible = false;
                S_TerrainTexture3.Instance.Visible = false;
            }
        }
        
        if (sourceIndex == 0)
        {
            S_TerrainTexture1.L_Label.Instance.Text = "顶部墙壁";
        }
        else
        {
            S_TerrainTexture1.L_Label.Instance.Text = "地形";
        }
    }

    private void SetTerrainCellData(TileSetTerrainInfo terrain, UiCell<RightCell, byte> cell)
    {
        var data = terrain.GetTerrainCell(cell.Index, cell.Data);
        if (data != null)
        {
            var terrainCell = (TerrainCell)cell;
            var x = data[0];
            var y = data[1];
            terrainCell.SetCell(new Rect2I(x, y, GameConfig.TileCellSize, GameConfig.TileCellSize));
        }
    }
    
    private void RefreshConnectTerrainCell(TileSetTerrainInfo terrain, UiCell<RightCell, byte> cell)
    {
        var data = terrain.GetTerrainCell(cell.Index, cell.Data);
        if (data != null)
        {
            var terrainCell = (TerrainCell)cell;
            var x = data[0];
            var y = data[1];
            var index = x / GameConfig.TileCellSize + y / GameConfig.TileCellSize * BottomGrid.GetColumns();
            var maskCell = (MaskCell)BottomGrid.GetCell(index);
            if (maskCell != null)
            {
                //绑定TerrainCell
                maskCell.SetConnectTerrainCell(terrainCell);
            }
        }
    }

    /// <summary>
    /// 放置地形Cell纹理
    /// </summary>
    public void OnDropCell(MaskCell maskCell)
    {
        var flag = true;
        TopGrid1.ForEach((cell) =>
        {
            flag = !((TerrainCell)cell).OnDropCell(maskCell);
            return flag;
        });
        if (EditorPanel.TileSetSourceIndex == 0) //必须选中Main Source
        {
            if (flag)
            {
                TopGrid2.ForEach((cell) =>
                {
                    flag = !((TerrainCell)cell).OnDropCell(maskCell);
                    return flag;
                });
            }
            if (flag)
            {
                TopGrid3.ForEach((cell) =>
                {
                    return ((TerrainCell)cell).OnDropCell(maskCell);
                });
            }
        }
    }
    
    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        S_BottomBg.Instance.OnChangeTileSetTexture();

        BottomGrid.RemoveAll();
        var cellHorizontal = EditorPanel.CellHorizontal;
        if (cellHorizontal <= 0)
        {
            return;
        }
        var cellVertical = EditorPanel.CellVertical;
        BottomGrid.SetColumns(cellHorizontal);
        for (var y = 0; y < cellVertical; y++)
        {
            for (var x = 0; x < cellHorizontal; x++)
            {
                BottomGrid.Add(new Rect2I(x * GameConfig.TileCellSize, y * GameConfig.TileCellSize, GameConfig.TileCellSize, GameConfig.TileCellSize));
            }
        }

        _refreshGridConnect = true;
    }
    
    //更改背景颜色
    private void OnChangeTileSetBgColor(object obj)
    {
        S_BottomBg.Instance.Color = EditorPanel.BgColor;
        S_TopBg.Instance.Color = EditorPanel.BgColor;
    }
}
