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
    /// 当前选中的地形索引
    /// </summary>
    public int CurrTerrainIndex => TerrainTabGrid.SelectIndex;

    /// <summary>
    /// 当前选中的地形
    /// </summary>
    public TileSetTerrainInfo CurrTerrain
    {
        get
        {
            var terrain = EditorPanel.TileSetSourceInfo.Terrain;
            if (CurrTerrainIndex < 0 || CurrTerrainIndex >= terrain.Count)
            {
                return null;
            }
            return terrain[CurrTerrainIndex];
        }
    }
    
    /// <summary>
    /// 正在拖拽的图块
    /// </summary>
    public MaskCell DraggingCell { get; set; }

    public UiGrid<TerrainTab, TileSetTerrainInfo> TerrainTabGrid;
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

        //地形页签
        TerrainTabGrid = CreateUiGrid<TerrainTab, TileSetTerrainInfo, TerrainTabCell>(S_TerrainTab);
        TerrainTabGrid.SetCellOffset(Vector2I.Zero);
        TerrainTabGrid.SetColumns(1);
        TerrainTabGrid.SetHorizontalExpand(true);

        MaskGrid = CreateUiGrid<BottomCell, Rect2I, MaskCell>(S_BottomCell);
        MaskGrid.SetCellOffset(Vector2I.Zero);
        MaskGrid.GridContainer.MouseFilter = MouseFilterEnum.Ignore;

        TerrainGrid3x3 = InitTopGrid(S_TerrainRoot.L_TerrainTexture1.Instance, GameConfig.TerrainBit3x3, TileSetTerrainInfo.TerrainLayerType);
        TerrainGrid2x2 = InitTopGrid(S_TerrainRoot.L_TerrainTexture4.Instance, GameConfig.TerrainBit2x2, TileSetTerrainInfo.TerrainLayerType);
        TerrainGridMiddle = InitTopGrid(S_TerrainRoot.L_TerrainTexture2.Instance, GameConfig.TerrainBitMiddle, TileSetTerrainInfo.MiddleLayerType);
        TerrainGridFloor = InitTopGrid(S_TerrainRoot.L_TerrainTexture3.Instance, GameConfig.TerrainBitFloor, TileSetTerrainInfo.FloorLayerType);
        
        //删除地形按钮
        S_DeleteButton.Instance.Pressed += OnDeleteTerrainClick;
        //添加地形按钮
        S_AddButton.Instance.Pressed += OnAddTerrainClick;
        //编辑地形
        S_EditButton.Instance.Pressed += OnEditTerrainClick;
        
        OnSetTileTexture(EditorPanel.Texture);
        OnChangeTileSetBgColor(EditorPanel.BgColor);
        
        OnChangeTerrainType(-1);
        //改变选中的地形
        TerrainTabGrid.SelectEvent += OnChangeTerrain;
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

            var terrainList = EditorPanel.TileSetSourceInfo.Terrain;
            for (var i = 0; i < terrainList.Count; i++)
            {
                var terrain = terrainList[i];
                if (terrain.TerrainType == 0) //选中47格Terrain
                {
                    TerrainGrid3x3.ForEach(cell => RefreshTerrainCellUseFlag(terrain, cell));
                    if (EditorPanel.TileSetSourceIndex == 0 && CurrTerrainIndex == 0) //选中Main Source
                    {
                        TerrainGridMiddle.ForEach(cell => RefreshTerrainCellUseFlag(terrain, cell));
                        TerrainGridFloor.ForEach(cell => RefreshTerrainCellUseFlag(terrain, cell));
                    }
                }
                else //选中13格Terrain
                {
                    TerrainGrid2x2.ForEach(cell => RefreshTerrainCellUseFlag(terrain, cell));
                }
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
        TerrainTabGrid.RemoveAll();
        //加载Terrain
        if (obj != null)
        {
            var terrainList = ((TileSetSourceInfo)obj).Terrain;
            if (terrainList.Count > 0)
            {
                TerrainTabGrid.SetDataList(terrainList.ToArray());
                TerrainTabGrid.SelectIndex = 0;
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
    
    private void RefreshTerrainCellUseFlag(TileSetTerrainInfo terrain, UiCell<RightCell, byte> cell)
    {
        var data = terrain.GetTerrainCell(cell.Index, cell.Data);
        if (data != null)
        {
            var x = data[0];
            var y = data[1];
            var index = x / GameConfig.TileCellSize + y / GameConfig.TileCellSize * MaskGrid.GetColumns();
            var maskCell = (MaskCell)MaskGrid.GetCell(index);
            if (maskCell != null)
            {
                maskCell.SetUseFlag(true);
            }
        }
    }

    /// <summary>
    /// 放置地形Cell纹理
    /// </summary>
    public void OnDropCell(MaskCell maskCell)
    {
        var terrain = CurrTerrain;
        if (terrain == null)
        {
            return;
        }
        if (terrain.TerrainType == 0) //选中47个Terrain
        {
            var flag = true;
            TerrainGrid3x3.ForEach((cell) =>
            {
                flag = !((TerrainCell)cell).OnDropCell(maskCell);
                return flag;
            });
            if (EditorPanel.TileSetSourceIndex == 0 && CurrTerrainIndex == 0) //选中Main Source
            {
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

    //切换选中的地形
    private void OnChangeTerrain(int index)
    {
        //清除所有绑定的Terrain
        if (index >= 0)
        {
            TerrainGrid3x3.ForEach(cell => ((TerrainCell)cell).ClearCell());
            TerrainGridMiddle.ForEach(cell => ((TerrainCell)cell).ClearCell());
            TerrainGridFloor.ForEach(cell => ((TerrainCell)cell).ClearCell());
            TerrainGrid2x2.ForEach(cell => ((TerrainCell)cell).ClearCell());
        }
        S_TopBg.Instance.SetHoverCell(null);
        S_BottomBg.Instance.SetHoverCell(null);
        
        MaskGrid.ForEach(cell =>
        {
            ((MaskCell)cell).SetConnectTerrainCell(null);
        });
        
        var terrain = CurrTerrain;
        if (terrain != null)
        {
            if (terrain.TerrainType == 0) //选中47个Terrain
            {
                TerrainGrid3x3.ForEach(cell =>
                {
                    SetTerrainCellData(terrain, cell);
                    RefreshConnectTerrainCell(terrain, cell);
                });
                if (EditorPanel.TileSetSourceIndex == 0 && CurrTerrainIndex == 0) //选中Main Source
                {
                    TerrainGridMiddle.ForEach(cell =>
                    {
                        SetTerrainCellData(terrain, cell);
                        RefreshConnectTerrainCell(terrain, cell);
                    });
                    TerrainGridFloor.ForEach(cell =>
                    {
                        SetTerrainCellData(terrain, cell);
                        RefreshConnectTerrainCell(terrain, cell);
                    });
                }
            }
            else //选中13格Terrain
            {
                TerrainGrid2x2.ForEach(cell =>
                {
                    SetTerrainCellData(terrain, cell);
                    RefreshConnectTerrainCell(terrain, cell);
                });
            }
        }
        
        if (index >= 0)
        {
            OnChangeTerrainType(TerrainTabGrid.GetCell(index).Data.TerrainType);
        }
        else
        {
            OnChangeTerrainType(-1);
        }
    }
    
    /// <summary>
    /// 切换Terrain类型
    /// </summary>
    private void OnChangeTerrainType(long index)
    {
        if (EditorPanel.TileSetSourceIndex == 0 && CurrTerrainIndex == 0) //选中 Main Source
        {
            S_TerrainTexture2.Instance.Visible = true;
            S_TerrainTexture3.Instance.Visible = true;
            TerrainGridMiddle.Visible = true;
            TerrainGridFloor.Visible = true;
            S_TerrainTexture1.L_Label.Instance.Text = "顶部墙壁";
        }
        else
        {
            S_TerrainTexture2.Instance.Visible = false;
            S_TerrainTexture3.Instance.Visible = false;
            TerrainGridMiddle.Visible = false;
            TerrainGridFloor.Visible = false;
            S_TerrainTexture1.L_Label.Instance.Text = "地形";
        }
        
        if (index == 0) //47 格子
        {
            S_TerrainRoot.L_TerrainTexture1.Instance.Visible = true;
            TerrainGrid3x3.Visible = true;
            S_TerrainRoot.L_TerrainTexture4.Instance.Visible = false;
            TerrainGrid2x2.Visible = false;
        }
        else if (index == 1) //13 格子
        {
            S_TerrainRoot.L_TerrainTexture1.Instance.Visible = false;
            TerrainGrid3x3.Visible = false;
            S_TerrainRoot.L_TerrainTexture4.Instance.Visible = true;
            TerrainGrid2x2.Visible = true;
        }
        else
        {
            S_TerrainRoot.L_TerrainTexture1.Instance.Visible = false;
            TerrainGrid3x3.Visible = false;
            S_TerrainRoot.L_TerrainTexture4.Instance.Visible = false;
            TerrainGrid2x2.Visible = false;
        }
    }

    //删除地形
    private void OnDeleteTerrainClick()
    {
        if (EditorPanel.TileSetSourceIndex == 0 && CurrTerrainIndex == 0) //不能删除 Main Terrain
        {
            EditorWindowManager.ShowTips("警告", "不允许删除 Main Terrain！");
            return;
        }

        var terrain = CurrTerrain;
        if (terrain != null)
        {
            EditorWindowManager.ShowConfirm("提示", $"是否删除地形'{terrain.Name}'？", (v) =>
            {
                if (v)
                {
                    var index = TerrainTabGrid.SelectIndex;
                    //执行删除操作
                    
                    //清除Mask
                    if (terrain.TerrainType == 0) //47格
                    {
                        TerrainGrid3x3.ForEach(cell =>
                        {
                            var terrainCell = (TerrainCell)cell;
                            terrainCell.ClearCell();
                            if (terrainCell.ConnectMaskCell != null)
                            {
                                terrainCell.ConnectMaskCell.SetUseFlag(false);
                                terrainCell.ConnectMaskCell.SetConnectTerrainCell(null);
                            }
                        });
                    }
                    else //13格
                    {
                        TerrainGrid2x2.ForEach(cell =>
                        {
                            var terrainCell = (TerrainCell)cell;
                            terrainCell.ClearCell();
                            if (terrainCell.ConnectMaskCell != null)
                            {
                                terrainCell.ConnectMaskCell.SetUseFlag(false);
                                terrainCell.ConnectMaskCell.SetConnectTerrainCell(null);
                            }
                        });
                    }
                    
                    EditorPanel.TileSetSourceInfo.Terrain.Remove(terrain);
                    TerrainTabGrid.RemoveByIndex(index);
                    
                    if (index == TerrainTabGrid.Count)
                    {
                        TerrainTabGrid.SelectIndex = TerrainTabGrid.Count - 1;
                    }
                    else
                    {
                        TerrainTabGrid.SelectIndex = index;
                    }
                    EventManager.EmitEvent(EventEnum.OnTileMapDirty);
                }
            });
        }
        else
        {
            EditorWindowManager.ShowTips("提示", "清选择一个Terrain！");
        }
    }

    //创建地形
    private void OnAddTerrainClick()
    {
        EditorWindowManager.ShowCreateTerrain(EditorPanel.TileSetSourceInfo, terrainInfo =>
        {
            //执行添加操作
            EditorPanel.TileSetSourceInfo.Terrain.Add(terrainInfo);
            TerrainTabGrid.Add(terrainInfo, true);
            EventManager.EmitEvent(EventEnum.OnTileMapDirty);
        });
    }
    
    //编辑地形(重命名)
    private void OnEditTerrainClick()
    {
        if (EditorPanel.TileSetSourceIndex == 0 && CurrTerrainIndex == 0) //不能删除 Main Terrain
        {
            EditorWindowManager.ShowTips("警告", "不允许重命名 Main Terrain！");
            return;
        }
        
        var terrain = CurrTerrain;
        if (terrain != null)
        {
            EditorWindowManager.ShowInput("提示", $"是否删除地形'{terrain.Name}'？", terrain.Name, (v, isClose) =>
            {
                if (string.IsNullOrEmpty(v))
                {
                    EditorWindowManager.ShowTips("错误", $"名称不允许为空！");
                    return false;
                }
                if (terrain.Name != v && EditorPanel.TileSetSourceInfo.Terrain.FindIndex(info => info.Name == v) >= 0)
                {
                    EditorWindowManager.ShowTips("错误", $"已经有相同名称的Terrain了！");
                    return false;
                }

                terrain.Name = v;
                //刷新页签
                ((TerrainTabCell)TerrainTabGrid.GetCell(CurrTerrainIndex)).RefreshData();
                EventManager.EmitEvent(EventEnum.OnTileMapDirty);
                return true;
            });
        }
        else
        {
            EditorWindowManager.ShowTips("提示", "清选择一个Terrain！");
        }
    }
}
