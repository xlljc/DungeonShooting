using System.Collections.Generic;
using Godot;

namespace UI.TileSetEditorCombination;

public partial class TileEditArea : GridBg<TileSetEditorCombination.LeftBottomBg>
{
    private UiGrid<TileSetEditorCombination.MaskRect, bool> _maskGrid;
    private readonly HashSet<Vector2I> _useMask = new HashSet<Vector2I>();
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        Grid = UiNode.L_Grid.Instance;
        ContainerRoot = UiNode.L_TileTexture.Instance;
        
        var maskBrush = UiNode.L_TileTexture.L_MaskBrush.Instance;
        maskBrush.TileTexture = UiNode.L_TileTexture.Instance;
        maskBrush.TileEditArea = this;

        _maskGrid = new UiGrid<TileSetEditorCombination.MaskRect, bool>(UiNode.L_TileTexture.L_MaskRoot.L_MaskRect, typeof(MaskRectCell));
        _maskGrid.SetCellOffset(Vector2I.Zero);
        _maskGrid.GridContainer.MouseFilter = MouseFilterEnum.Ignore;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        _maskGrid.Destroy();
    }

    public override void _Process(double delta)
    {
        //Ui未打开
        if (!UiNode.UiPanel.IsOpen)
        {
            return;
        }

        if (Input.IsMouseButtonPressed(MouseButton.Left)) //左键选中
        {
            if (this.IsMouseInRect())
            {
                var cellPosition = GetMouseCellPosition();
                if (UiNode.UiPanel.EditorPanel.IsCellPositionInTexture(cellPosition))
                {
                    if (Input.IsActionJustPressed("mouse_left")) //刚按下, 清除之前的选中
                    {
                        OnRemoveCell();
                    }
                    OnSelectCell(cellPosition);
                }
            }
        }
    }

    /// <summary>
    /// 选中Cell图块
    /// </summary>
    private void OnSelectCell(Vector2I cell)
    {
        if (!_useMask.Contains(cell))
        {
            var cellIndex = UiNode.UiPanel.EditorPanel.CellPositionToIndex(cell);
            var uiCell = _maskGrid.GetCell(cellIndex);
            if (uiCell != null && !uiCell.Data)
            {
                _useMask.Add(cell);
                uiCell.SetData(true);
                
                EventManager.EmitEvent(EventEnum.OnSelectContainerCell, cell);
            }
        }
    }

    /// <summary>
    /// 移除选中的Cell图块
    /// </summary>
    private void OnRemoveCell()
    {
        _useMask.Clear();
        var count = _maskGrid.Count;
        for (var i = 0; i < count; i++)
        {
            var uiCell = _maskGrid.GetCell(i);
            if (uiCell.Data)
            {
                uiCell.SetData(false);
            }
        }
        
        EventManager.EmitEvent(EventEnum.OnClearContainerCell);
    }

    /// <summary>
    /// 改变TileSet纹理
    /// </summary>
    public void OnChangeTileSetTexture(Texture2D texture)
    {
        UiNode.L_TileTexture.Instance.Texture = texture;
        var width = UiNode.UiPanel.EditorPanel.CellHorizontal;
        var height = UiNode.UiPanel.EditorPanel.CellVertical;
        _maskGrid.RemoveAll();
        _useMask.Clear();
        _maskGrid.SetColumns(width);
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                _maskGrid.Add(false);
            }
        }
    }
    
    /// <summary>
    /// 返回鼠标所在的单元格位置, 相对于纹理左上角
    /// </summary>
    public Vector2I GetMouseCellPosition()
    {
        var textureRect = UiNode.L_TileTexture.Instance;
        var pos = textureRect.GetLocalMousePosition() / GameConfig.TileCellSize;
        return pos.AsVector2I();
    }
}