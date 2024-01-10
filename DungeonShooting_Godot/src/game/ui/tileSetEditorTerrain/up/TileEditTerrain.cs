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
            if (_panel.S_TerrainTexture1.Instance.IsMouseInRect())
            {
                var cellPosition = Utils.GetMouseCellPosition(_panel.S_TerrainTexture1.Instance);
                var index = cellPosition.X + cellPosition.Y * _panel.TopGrid1.GetColumns();
                var tempCell = (TerrainCell)_panel.TopGrid1.GetCell(index);
                if (tempCell.ConnectMaskCell != null)
                {
                    cell = tempCell;
                }
            }
            //必须选中Main Source
            if (_panel.EditorPanel.TileSetSourceIndex == 0)
            {
                if (_panel.S_TerrainTexture2.Instance.IsMouseInRect())
                {
                    var cellPosition = Utils.GetMouseCellPosition(_panel.S_TerrainTexture2.Instance);
                    var index = cellPosition.X + cellPosition.Y * _panel.TopGrid2.GetColumns();
                    var tempCell = (TerrainCell)_panel.TopGrid2.GetCell(index);
                    if (tempCell.ConnectMaskCell != null)
                    {
                        cell = tempCell;
                    }
                }
                if (_panel.S_TerrainTexture3.Instance.IsMouseInRect())
                {
                    var cellPosition = Utils.GetMouseCellPosition(_panel.S_TerrainTexture3.Instance);
                    var index = cellPosition.X + cellPosition.Y * _panel.TopGrid3.GetColumns();
                    var tempCell = (TerrainCell)_panel.TopGrid3.GetCell(index);
                    if (tempCell.ConnectMaskCell != null)
                    {
                        cell = tempCell;
                    }
                }
            }
        }
        SetHoverCell(cell);
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
        var rootSize = UiNode.L_TerrainRoot.Instance.Size;
        if (UiNode.UiPanel.EditorPanel.TileSetSourceIndex != 0)
        {
            rootSize.Y -= 2 * GameConfig.TileCellSize;
        }
        Utils.DoFocusNode(ContainerRoot, Size, rootSize);
        RefreshGridTrans();
    }
}