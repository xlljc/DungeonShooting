using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TileEditArea : EditorGridBg<TileSetEditorTerrain.BottomBg>
{
    private bool _dragMoveFlag = false;
    private MaskCell _hoverCell = null;
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        InitNode(UiNode.L_TileTexture.Instance, UiNode.L_Grid.Instance);
        UiNode.L_TileTexture.L_MaskBrush.Instance.Init(UiNode.L_TileTexture.Instance, UiNode.UiPanel.EditorPanel);

        UiNode.L_TileTexture.Instance.Texture = UiNode.UiPanel.EditorPanel.Texture;
        
        //聚焦按钮点击
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
        
        //拖拽Cell
        UiNode.Instance.AddDragListener(OnDrag);
    }

    public override void _Process(double delta)
    {
        MaskCell cell = null;
        var _panel = UiNode.UiPanel;
        if (!_panel.MaskGrid.IsDestroyed && _panel.S_BottomBg.Instance.IsMouseInRect() && _panel.S_TileTexture.Instance.IsMouseInRect())
        {
            var cellPosition = Utils.GetMouseCellPosition(_panel.S_TileTexture.Instance);
            var index = cellPosition.X + cellPosition.Y * _panel.MaskGrid.GetColumns();
            var tempCell = (MaskCell)_panel.MaskGrid.GetCell(index);
            if (tempCell != null && tempCell.ConnectTerrainCell != null && tempCell.ConnectTerrainIndex == _panel.CurrTerrainIndex)
            {
                cell = tempCell;
            }
        }
        SetHoverCell(cell);
    }

    /// <summary>
    /// 设置鼠标悬停Cell
    /// </summary>
    public void SetHoverCell(MaskCell cell)
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

    //拖拽操作
    private void OnDrag(DragState state, Vector2 delta)
    {
        var _panel = UiNode.UiPanel;
        var sprite = _panel.S_DragSprite.Instance;
        if (state == DragState.DragStart) //拖拽开始
        {
            //这里要判断一下是否在BottomBg和TileTexture区域内
            if (_panel.S_BottomBg.Instance.IsMouseInRect() && _panel.S_TileTexture.Instance.IsMouseInRect())
            {
                var cellPosition = Utils.GetMouseCellPosition(_panel.S_TileTexture.Instance);
                var index = cellPosition.X + cellPosition.Y * _panel.MaskGrid.GetColumns();
                var cell = (MaskCell)_panel.MaskGrid.GetCell(index);
                if (cell != null && cell.ConnectTerrainCell == null) //必须要没有使用的Cell
                {
                    _panel.DraggingCell = cell;
                    _panel.MaskGrid.SelectIndex = index;
                    _dragMoveFlag = false;
                }
            }
        }
        else if (state == DragState.DragEnd) //拖拽结束
        {
            if (_panel.DraggingCell != null)
            {
                if (_panel.S_TopBg.Instance.IsMouseInRect()) //找到放置的Cell
                {
                    _panel.OnDropCell(_panel.DraggingCell);
                }
                sprite.Visible = false;
                _dragMoveFlag = false;
                _panel.DraggingCell = null;
            }
        }
        else if (_panel.DraggingCell != null) //拖拽移动
        {
            if (!_dragMoveFlag)
            {
                _dragMoveFlag = true;
                sprite.Texture = _panel.S_TileTexture.Instance.Texture;
                sprite.RegionRect = _panel.DraggingCell.Data;
                sprite.Scale = _panel.S_TopBg.L_TerrainRoot.Instance.Scale;
                sprite.Visible = true;
                sprite.GlobalPosition = sprite.GetGlobalMousePosition();
            }
            sprite.GlobalPosition = sprite.GetGlobalMousePosition();
        }
    }

    /// <summary>
    /// 改变TileSet纹理
    /// </summary>
    public void OnChangeTileSetTexture()
    {
        UiNode.L_TileTexture.Instance.Size = UiNode.L_TileTexture.Instance.Texture.GetSize();
        OnFocusClick();
    }
    
    //聚焦按钮点击
    private void OnFocusClick()
    {
        var texture = UiNode.L_TileTexture.Instance.Texture;
        Utils.DoFocusNode(ContainerRoot, Size, texture != null ? texture.GetSize() : Vector2.Zero);
        RefreshGridTrans();
    }
}