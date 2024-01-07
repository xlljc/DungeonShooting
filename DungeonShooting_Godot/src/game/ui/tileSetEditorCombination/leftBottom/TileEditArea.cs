using System.Collections.Generic;
using Godot;

namespace UI.TileSetEditorCombination;

public partial class TileEditArea : EditorGridBg<TileSetEditorCombination.LeftBottomBg>
{
    private UiGrid<TileSetEditorCombination.MaskRect, bool> _maskGrid;
    private readonly HashSet<Vector2I> _useMask = new HashSet<Vector2I>();
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        InitNode(UiNode.L_TileTexture.Instance, UiNode.L_Grid.Instance);
        UiNode.L_TileTexture.Instance.Texture = UiNode.UiPanel.EditorPanel.Texture;
        var maskBrush = UiNode.L_TileTexture.L_MaskBrush.Instance;
        maskBrush.Init(UiNode.L_TileTexture.Instance, UiNode.UiPanel.EditorPanel);

        _maskGrid = new UiGrid<TileSetEditorCombination.MaskRect, bool>(UiNode.L_TileTexture.L_MaskRoot.L_MaskRect, typeof(MaskRectCell));
        _maskGrid.SetCellOffset(Vector2I.Zero);
        _maskGrid.GridContainer.MouseFilter = MouseFilterEnum.Ignore;

        //聚焦按钮点击
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        _maskGrid.Destroy();
    }

    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
        if (@event is InputEventMouse)
        {
            AcceptEvent();
            if (Input.IsMouseButtonPressed(MouseButton.Left)) //左键选中
            {
                var cellPosition = GetMouseCellPosition();
                if (UiNode.UiPanel.EditorPanel.IsCellPositionInTexture(cellPosition))
                {
                    if (Input.IsActionJustPressed(InputAction.MouseLeft)) //刚按下, 清除之前的选中
                    {
                        ClearSelectCell();
                    }
                    OnSelectCell(cellPosition);
                }
            }
            else if (Input.IsMouseButtonPressed(MouseButton.Right)) //右键键擦除
            {
                var cellPosition = GetMouseCellPosition();
                if (UiNode.UiPanel.EditorPanel.IsCellPositionInTexture(cellPosition))
                {
                    OnRemoveCell(cellPosition);
                }
            }
        }
    }

    //聚焦按钮点击
    private void OnFocusClick()
    {
        var texture = UiNode.L_TileTexture.Instance.Texture;
        Utils.DoFocusNode(ContainerRoot, Size, texture != null ? texture.GetSize() : Vector2.Zero);
        RefreshGridTrans();
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
                
                EventManager.EmitEvent(EventEnum.OnSelectCombinationCell, cell);
            }
        }
    }

    /// <summary>
    /// 移除指定的Cell图块
    /// </summary>
    private void OnRemoveCell(Vector2I cell)
    {
        if (_useMask.Contains(cell))
        {
            var cellIndex = UiNode.UiPanel.EditorPanel.CellPositionToIndex(cell);
            var uiCell = _maskGrid.GetCell(cellIndex);
            if (uiCell != null && uiCell.Data)
            {
                _useMask.Remove(cell);
                uiCell.SetData(false);
                
                EventManager.EmitEvent(EventEnum.OnRemoveCombinationCell, cell);
            }
        }
    }
    
    /// <summary>
    /// 移除所有选中的Cell图块
    /// </summary>
    public void ClearSelectCell()
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
        
        EventManager.EmitEvent(EventEnum.OnClearCombinationCell);
    }

    /// <summary>
    /// 改变TileSet纹理
    /// </summary>
    public void OnChangeTileSetTexture()
    {
        var width = UiNode.UiPanel.EditorPanel.CellHorizontal;
        var height = UiNode.UiPanel.EditorPanel.CellVertical;
        UiNode.L_TileTexture.Instance.Size = UiNode.L_TileTexture.Instance.Texture.GetSize();
        _maskGrid.RemoveAll();
        _useMask.Clear();
        _maskGrid.SetColumns(Mathf.Max(1, width));
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                _maskGrid.Add(false);
            }
        }

        OnFocusClick();
    }
    
    /// <summary>
    /// 返回鼠标所在的单元格位置, 相对于纹理左上角
    /// </summary>
    public Vector2I GetMouseCellPosition()
    {
        return Utils.GetMouseCellPosition(UiNode.L_TileTexture.Instance);
    }
}