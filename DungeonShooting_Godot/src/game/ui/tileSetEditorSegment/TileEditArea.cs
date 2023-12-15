using System.Collections.Generic;
using Godot;

namespace UI.TileSetEditorSegment;

public partial class TileEditArea : ColorRect, IUiNodeScript
{
    private TileSetEditorSegment.LeftBg _leftBg;
    private DragBinder _dragBinder;
    private UiGrid<TileSetEditorSegment.MaskRect, bool> _maskGrid;
    private readonly HashSet<Vector2I> _useMask = new HashSet<Vector2I>();
    
    public void SetUiNode(IUiNode uiNode)
    {
        _leftBg = (TileSetEditorSegment.LeftBg)uiNode;
        var maskBrush = _leftBg.L_TileTexture.L_MaskBrush.Instance;
        maskBrush.TileTexture = _leftBg.L_TileTexture.Instance;
        maskBrush.TileEditArea = this;

        _dragBinder = DragUiManager.BindDrag(this, "mouse_middle", OnDrag);
        Resized += OnLeftBgResize;

        _maskGrid = new UiGrid<TileSetEditorSegment.MaskRect, bool>(_leftBg.L_TileTexture.L_MaskRoot.L_MaskRect, typeof(MaskRectCell));
        _maskGrid.SetCellOffset(Vector2I.Zero);
        
        _leftBg.UiPanel.AddEventListener(EventEnum.OnImportTileCell, OnImportCell);
        _leftBg.UiPanel.AddEventListener(EventEnum.OnRemoveTileCell, OnRemoveCell);
    }

    public void OnDestroy()
    {
        _dragBinder.UnBind();
        _maskGrid.Destroy();
    }

    private void OnDrag(DragState state, Vector2 pos)
    {
        if (state == DragState.DragMove)
        {
            _leftBg.L_TileTexture.Instance.Position += pos;
            OnLeftBgResize();
        }
    }

    public override void _Input(InputEvent @event)
    {
        //Ui未打开
        if (!_leftBg.UiPanel.IsOpen)
        {
            return;
        }
        if (@event is InputEventMouseButton mouseButton)
        {
            if (_leftBg.UiPanel.IsOpen)
            {
                if (mouseButton.ButtonIndex == MouseButton.WheelDown)
                {
                    if (GetGlobalRect().HasPoint(mouseButton.GlobalPosition))
                    {
                        //缩小
                        Shrink();
                    }
                }
                else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
                {
                    if (GetGlobalRect().HasPoint(mouseButton.GlobalPosition))
                    {
                        //放大
                        Magnify();
                    }
                }
            }
        }
    }

    public override void _Process(double delta)
    {
        //Ui未打开
        if (!_leftBg.UiPanel.IsOpen)
        {
            return;
        }

        if (Input.IsMouseButtonPressed(MouseButton.Left)) //左键导入
        {
            if (_leftBg.L_TileTexture.Instance.IsMouseInRect() && this.IsMouseInRect())
            {
                var cellPosition = GetMouseCellPosition();
                if (!_useMask.Contains(cellPosition))
                {
                    EventManager.EmitEvent(EventEnum.OnImportTileCell, cellPosition);
                }
            }
        }
        else if (Input.IsMouseButtonPressed(MouseButton.Right)) //右键移除
        {
            if (_leftBg.L_TileTexture.Instance.IsMouseInRect() && this.IsMouseInRect())
            {
                var cellPosition = GetMouseCellPosition();
                if (_useMask.Contains(cellPosition))
                {
                    EventManager.EmitEvent(EventEnum.OnRemoveTileCell, cellPosition);
                }
            }
        }
    }

    /// <summary>
    /// 导入选中的Cell图块
    /// </summary>
    private void OnImportCell(object arg)
    {
        if (arg is Vector2I cell)
        {
            var cellIndex = _leftBg.UiPanel.EditorPanel.CellPositionToIndex(cell);
            var uiCell = _maskGrid.GetCell(cellIndex);
            if (uiCell != null && !uiCell.Data)
            {
                _useMask.Add(cell);
                uiCell.SetData(true);
            }
        }
    }

    /// <summary>
    /// 移除选中的Cell图块
    /// </summary>
    private void OnRemoveCell(object arg)
    {
        if (arg is Vector2I cell)
        {
            var cellIndex = _leftBg.UiPanel.EditorPanel.CellPositionToIndex(cell);
            var uiCell = _maskGrid.GetCell(cellIndex);
            if (uiCell != null && uiCell.Data)
            {
                _useMask.Remove(cell);
                uiCell.SetData(false);
            }
        }
    }

    //缩小
    private void Shrink()
    {
        var textureRect = _leftBg.L_TileTexture.Instance;
        var offset = textureRect.GetLocalMousePosition();
        var prevScale = textureRect.Scale;
        var newScale = prevScale / 1.1f;
        if (newScale.LengthSquared() >= 0.5f)
        {
            textureRect.Scale = newScale;
            var position = textureRect.Position + offset * 0.1f * newScale;
            textureRect.Position = position;
            SetGridTransform(position, newScale.X);
        }
    }
    //放大
    private void Magnify()
    {
        var textureRect = _leftBg.L_TileTexture.Instance;
        var offset = textureRect.GetLocalMousePosition();
        var prevScale = textureRect.Scale;
        var newScale = prevScale * 1.1f;
        if (newScale.LengthSquared() <= 2000)
        {
            textureRect.Scale = newScale;
            var position = textureRect.Position - offset * 0.1f * prevScale;
            textureRect.Position = position;
            SetGridTransform(position, newScale.X);
        }
    }

    /// <summary>
    /// 当前Ui被显示出来时调用
    /// </summary>
    public void OnShow()
    {
        OnLeftBgResize();
    }

    //背景宽度变化
    private void OnLeftBgResize()
    {
        var sprite = _leftBg.L_TileTexture.Instance;
        var colorRect = _leftBg.L_Grid.Instance;
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.Size, Size);
        SetGridTransform(sprite.Position, sprite.Scale.X);
    }

    /// <summary>
    /// 改变TileSet纹理
    /// </summary>
    public void OnChangeTileSetTexture(Texture2D texture)
    {
        _leftBg.L_TileTexture.Instance.Texture = texture;
        var width = _leftBg.UiPanel.EditorPanel.CellHorizontal;
        var height = _leftBg.UiPanel.EditorPanel.CellVertical;
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

    //设置网格位置和缩放
    private void SetGridTransform(Vector2 pos, float scale)
    {
        var colorRect = _leftBg.L_Grid.Instance;
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.GridSize, GameConfig.TileCellSize * scale);
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.Offset, -pos);
    }
    
    /// <summary>
    /// 返回鼠标所在的单元格位置, 相对于纹理左上角
    /// </summary>
    public Vector2I GetMouseCellPosition()
    {
        var textureRect = _leftBg.L_TileTexture.Instance;
        var pos = textureRect.GetLocalMousePosition() / GameConfig.TileCellSize;
        return pos.AsVector2I();
    }
}