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

    public UiGrid<RightCell, byte> TerrainGrid3x3;
    public UiGrid<RightCell, byte> TerrainGrid2x2;
    public UiGrid<RightCell, byte> TerrainGridMiddle;
    public UiGrid<RightCell, byte> TerrainGridFloor;
    public UiGrid<BottomCell, Rect2I> MaskGrid;

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
        
        MaskGrid = CreateUiGrid<BottomCell, Rect2I, MaskCell>(S_BottomCell);
        MaskGrid.SetCellOffset(Vector2I.Zero);
        MaskGrid.GridContainer.MouseFilter = MouseFilterEnum.Ignore;

        TerrainGrid3x3 = InitTopGrid(S_TerrainRoot.L_TerrainTexture1.Instance, GameConfig.TerrainBit3x3, TileSetTerrainInfo.TerrainLayerType);
        TerrainGrid2x2 = InitTopGrid(S_TerrainRoot.L_TerrainTexture4.Instance, GameConfig.TerrainBit2x2, TileSetTerrainInfo.TerrainLayerType);
        TerrainGridMiddle = InitTopGrid(S_TerrainRoot.L_TerrainTexture2.Instance, GameConfig.TerrainBitMiddle, TileSetTerrainInfo.MiddleLayerType);
        TerrainGridFloor = InitTopGrid(S_TerrainRoot.L_TerrainTexture3.Instance, GameConfig.TerrainBitFloor, TileSetTerrainInfo.FloorLayerType);

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
            _refreshGridConnect = false;

            var terrain = EditorPanel.TileSetSourceInfo.Terrain;
            TerrainGrid3x3.ForEach(cell => RefreshConnectTerrainCell(terrain, cell));
            if (EditorPanel.TileSetSourceIndex == 0) //必须选中Main Source
            {
                TerrainGridMiddle.ForEach(cell => RefreshConnectTerrainCell(terrain, cell));
                TerrainGridFloor.ForEach(cell => RefreshConnectTerrainCell(terrain, cell));
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
        //清除所有绑定的Terrain
        TerrainGrid3x3.ForEach(cell => ((TerrainCell)cell).ClearCell());
        TerrainGridMiddle.ForEach(cell => ((TerrainCell)cell).ClearCell());
        TerrainGridFloor.ForEach(cell => ((TerrainCell)cell).ClearCell());
        TerrainGrid2x2.ForEach(cell => ((TerrainCell)cell).ClearCell());
        S_TopBg.Instance.SetHoverCell(null);
        S_BottomBg.Instance.SetHoverCell(null);

        var sourceIndex = EditorPanel.TileSetSourceIndex;
        if (sourceIndex == 0) //选中Main Source时就只能使用 47 Terrain
        {
            S_TerrainTexture2.Instance.Visible = true;
            S_TerrainTexture3.Instance.Visible = true;
            TerrainGridMiddle.Visible = true;
            TerrainGridFloor.Visible = true;
            S_TerrainTexture1.L_Label.Instance.Text = "顶部墙壁";
            S_TerrainTypeButton.Instance.Visible = false;
            S_TerrainTypeButton.Instance.Selected = 0;
            S_TopBg.Instance.ChangeTerrainType(0, false);
        }
        else
        {
            S_TerrainTexture2.Instance.Visible = false;
            S_TerrainTexture3.Instance.Visible = false;
            TerrainGridMiddle.Visible = false;
            TerrainGridFloor.Visible = false;
            S_TerrainTexture1.L_Label.Instance.Text = "地形";
            S_TerrainTypeButton.Instance.Visible = true;
            var selectIndex = EditorPanel.TileSetSourceInfo.Terrain.TerrainType;
            S_TerrainTypeButton.Instance.Selected = selectIndex;
            S_TopBg.Instance.ChangeTerrainType(selectIndex, false);
        }
        
        //再加载Terrain
        if (obj != null)
        {
            var terrain = ((TileSetSourceInfo)obj).Terrain;
            if (sourceIndex == 0) //选中Main Source
            {
                TerrainGrid3x3.ForEach(cell => SetTerrainCellData(terrain, cell));
                TerrainGridMiddle.ForEach(cell => SetTerrainCellData(terrain, cell));
                TerrainGridFloor.ForEach(cell => SetTerrainCellData(terrain, cell));
            }
            else if (S_TerrainTypeButton.Instance.Selected == 0) //选中47个Terrain
            {
                TerrainGrid3x3.ForEach(cell => SetTerrainCellData(terrain, cell));
            }
            else //选中13格Terrain
            {
                TerrainGrid2x2.ForEach(cell => SetTerrainCellData(terrain, cell));
            }
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
            var index = x / GameConfig.TileCellSize + y / GameConfig.TileCellSize * MaskGrid.GetColumns();
            var maskCell = (MaskCell)MaskGrid.GetCell(index);
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
        if (EditorPanel.TileSetSourceIndex == 0) //选中Main Source
        {
            var flag = true;
            TerrainGrid3x3.ForEach((cell) =>
            {
                flag = !((TerrainCell)cell).OnDropCell(maskCell);
                return flag;
            });
            if (flag)
            {
                TerrainGridMiddle.ForEach((cell) =>
                {
                    flag = !((TerrainCell)cell).OnDropCell(maskCell);
                    return flag;
                });
            }
            if (flag)
            {
                TerrainGridFloor.ForEach((cell) =>
                {
                    return ((TerrainCell)cell).OnDropCell(maskCell);
                });
            }
        }
        else if (S_TerrainTypeButton.Instance.Selected == 0) //选中47个Terrain
        {
            TerrainGrid3x3.ForEach((cell) =>
            {
                return !((TerrainCell)cell).OnDropCell(maskCell);
            });
        }
        else //选中13格Terrain
        {
            TerrainGrid2x2.ForEach((cell) =>
            {
                return !((TerrainCell)cell).OnDropCell(maskCell);
            });
        }
    }
    
    //改变TileSet纹理
    private void OnSetTileTexture(object arg)
    {
        S_BottomBg.Instance.OnChangeTileSetTexture();

        MaskGrid.RemoveAll();
        var cellHorizontal = EditorPanel.CellHorizontal;
        if (cellHorizontal <= 0)
        {
            return;
        }
        var cellVertical = EditorPanel.CellVertical;
        MaskGrid.SetColumns(cellHorizontal);
        for (var y = 0; y < cellVertical; y++)
        {
            for (var x = 0; x < cellHorizontal; x++)
            {
                MaskGrid.Add(new Rect2I(x * GameConfig.TileCellSize, y * GameConfig.TileCellSize, GameConfig.TileCellSize, GameConfig.TileCellSize));
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
