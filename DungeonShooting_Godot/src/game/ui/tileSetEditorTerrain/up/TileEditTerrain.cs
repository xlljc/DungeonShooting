using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TileEditTerrain : EditorGridBg<TileSetEditorTerrain.TopBg>
{
    private bool _dragMoveFlag = false;
    private TerrainCell _hoverCell = null;
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        var tileTexture = UiNode.L_TerrainRoot;
        InitNode(tileTexture.Instance, UiNode.L_Grid.Instance);
        var terrainBrush = tileTexture.L_Brush.Instance;
        terrainBrush.Root = tileTexture.Instance;
        terrainBrush.TerrainTextureList.Add(tileTexture.L_TerrainTexture1.Instance);
        terrainBrush.TerrainTextureList.Add(tileTexture.L_TerrainTexture2.Instance);
        terrainBrush.TerrainTextureList.Add(tileTexture.L_TerrainTexture3.Instance);
        terrainBrush.TerrainTextureList.Add(tileTexture.L_TerrainTexture4.Instance);
        
        //聚焦按钮点击
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
    }

    /// <summary>
    /// 改变TileSet纹理
    /// </summary>
    public void OnChangeTileSetTexture()
    {
        //UiNode.L_TileTexture.Instance.Size = UiNode.L_TileTexture.Instance.Texture.GetSize();
        OnFocusClick();
    }

    public override void _Process(double delta)
    {
        TerrainCell cell = null;
        var _panel = UiNode.UiPanel;
        if (_panel.S_TopBg.Instance.IsMouseInRect())
        {
            if (_panel.CurrTerrain.TerrainType == 0) //选中47个Terrain
            {
                cell = CalcMouseHoverCell(_panel.S_TerrainTexture1.Instance, _panel.TerrainGrid3x3);
                if (_panel.EditorPanel.TileSetSourceIndex == 0 && _panel.CurrTerrainIndex == 0) //选中Main Source
                {
                    if (cell == null)
                    {
                        cell = CalcMouseHoverCell(_panel.S_TerrainTexture2.Instance, _panel.TerrainGridMiddle);
                    }
                    if (cell == null)
                    {
                        cell = CalcMouseHoverCell(_panel.S_TerrainTexture3.Instance, _panel.TerrainGridFloor);
                    }
                }
            }
            else //选中13格Terrain
            {
                cell = CalcMouseHoverCell(_panel.S_TerrainTexture4.Instance, _panel.TerrainGrid2x2);
            }
        }
        SetHoverCell(cell);
    }

    /// <summary>
    /// 计算鼠标悬停在的地形单元。
    /// </summary>
    /// <param name="rectControl">矩形控件。</param>
    /// <param name="grid">网格。</param>
    /// <returns>鼠标悬停在的地形单元，如果鼠标不在矩形控件内或单元无效则返回null。</returns>
    private TerrainCell CalcMouseHoverCell(Control rectControl, UiGrid<TileSetEditorTerrain.RightCell, byte> grid)
    {
        if (rectControl.IsMouseInRect())
        {
            var cellPosition = Utils.GetMouseCellPosition(rectControl);
            var index = cellPosition.X + cellPosition.Y * grid.GetColumns();
            var tempCell = (TerrainCell)grid.GetCell(index);
            if (tempCell != null && tempCell.ConnectMaskCell != null)
            {
                return tempCell;
            }
        }

        return null;
    }
    
    /// <summary>
    /// 设置鼠标悬停Cell
    /// </summary>
    public void SetHoverCell(TerrainCell cell)
    {
        if (cell != _hoverCell)
        {
            if (_hoverCell != null)
            {
                _hoverCell.Hover = false;
            }

            if (cell != null)
            {
                cell.Hover = true;
            }

            _hoverCell = cell;
        }
    }

    //聚焦按钮点击
    private void OnFocusClick()
    {
        Vector2 rootSize;
        var panel = UiNode.UiPanel;
        if (panel.EditorPanel.TileSetSourceIndex == 0 && panel.CurrTerrainIndex == 0) //选中 Main Source
        {
            rootSize = UiNode.L_TerrainRoot.Instance.Size;
        }
        else if (panel.CurrTerrain.TerrainType == 0) //选中 47 格 Terrain
        {
            rootSize = UiNode.L_TerrainRoot.L_TerrainTexture1.Instance.Size;
        }
        else //13 格 Terrain
        {
            rootSize = UiNode.L_TerrainRoot.L_TerrainTexture4.Instance.Size;
        }
        Utils.DoFocusNode(ContainerRoot, Size, rootSize);
        RefreshGridTrans();
    }
    
}