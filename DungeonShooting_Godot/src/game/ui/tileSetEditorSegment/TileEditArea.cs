using Godot;

namespace UI.TileSetEditorSegment;

public partial class TileEditArea : ColorRect, IUiNodeScript
{
    private TileSetEditorSegment.LeftBg _leftBg;
    private DragBinder _dragBinder;
    private UiGrid<TileSetEditorSegment.MaskRect, bool> _maskGrid;

    /// <summary>
    /// 网格横轴数量
    /// </summary>
    public int CellHorizontal { get; private set; }
    /// <summary>
    /// 网格纵轴数量
    /// </summary>
    public int CellVertical { get; private set; }
    
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
    }

    public void OnDestroy()
    {
        _dragBinder.UnBind();
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
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            if (IsMouseInTexture())
            {
                var cellPos = GetMouseCellPosition();
                var index = CellPositionToIndex(cellPos);
                Debug.Log($"cellPos: {cellPos}, index: {index}");
                _maskGrid.UpdateByIndex(index, true);
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
        //背景颜色
        Color = _leftBg.UiPanel.EditorPanel.BgColor;
        OnLeftBgResize();
    }

    //背景宽度变化
    private void OnLeftBgResize()
    {
        var sprite = _leftBg.L_TileTexture.Instance;
        if (sprite.Texture != _leftBg.UiPanel.EditorPanel.Texture)
        {
            sprite.Texture = _leftBg.UiPanel.EditorPanel.Texture;
            OnChangeTileSetTexture(_leftBg.UiPanel.EditorPanel.Texture);
        }
        
        var colorRect = _leftBg.L_Grid.Instance;
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.Size, Size);
        SetGridTransform(sprite.Position, sprite.Scale.X);
    }
    
    //改变TileSet纹理
    private void OnChangeTileSetTexture(Texture2D texture)
    {
        var width = texture.GetWidth() / GameConfig.TileCellSize;
        var height = texture.GetHeight() / GameConfig.TileCellSize;
        _maskGrid.RemoveAll();
        _maskGrid.SetColumns(width);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
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
    /// 返回鼠标是否在texture区域内
    /// </summary>
    public bool IsMouseInTexture()
    {
        var textureRect = _leftBg.L_TileTexture.Instance;
        var pos = textureRect.GetLocalMousePosition();
        if (pos.X < 0 || pos.Y < 0)
        {
            return false;
        }

        var size = textureRect.Size;
        return pos.X <= size.X && pos.Y <= size.Y;
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

    /// <summary>
    /// 将二维位置转换为索引的函数
    /// </summary>
    public int CellPositionToIndex(Vector2I pos)
    {
        var gridWidth = _maskGrid.GetColumns();
        return pos.Y * gridWidth + pos.X;
    }
}