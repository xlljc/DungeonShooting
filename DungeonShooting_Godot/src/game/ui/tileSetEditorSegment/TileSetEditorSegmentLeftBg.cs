using Godot;

namespace UI.TileSetEditorSegment;

public partial class TileSetEditorSegmentLeftBg : ColorRect, IUiNodeScript
{
    private TileSetEditorSegment.LeftBg _leftBg;
    private DragBinder _dragBinder;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _leftBg = (TileSetEditorSegment.LeftBg)uiNode;
        _leftBg.L_TileTexture.L_Brush.Instance.Draw += OnBrushDraw;
        _dragBinder = DragUiManager.BindDrag(this, "mouse_middle", OnDrag);
        Resized += OnLeftBgResize;
    }

    public void OnDestroy()
    {
        _dragBinder.UnBind();
    }

    public override void _Process(double delta)
    {
        _leftBg.L_TileTexture.L_Brush.Instance.QueueRedraw();
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
        sprite.Texture = _leftBg.UiPanel.EditorPanel.Texture;
        
        var colorRect = _leftBg.L_Grid.Instance;
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.Size, Size);
        SetGridTransform(sprite.Position, sprite.Scale.X);
    }
    
    //设置网格位置和缩放
    private void SetGridTransform(Vector2 pos, float scale)
    {
        var colorRect = _leftBg.L_Grid.Instance;
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.GridSize, GameConfig.TileCellSize * scale);
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.Offset, -pos);
    }
    
    private void OnBrushDraw()
    {
        //绘制texture区域
        var textureRect = _leftBg.L_TileTexture.Instance;
        var brush = _leftBg.L_TileTexture.L_Brush;
        if (textureRect.Texture != null)
        {
            brush.Instance.DrawRect(new Rect2(Vector2.Zero, textureRect.Size),
                new Color(1, 1, 0, 0.5f), false,
                2f / textureRect.Scale.X);
        }

        //绘制鼠标悬停区域
        if (IsMouseInTexture())
        {
            var pos = textureRect.GetLocalMousePosition() / GameConfig.TileCellSize;
            pos = new Vector2((int)pos.X * GameConfig.TileCellSize, (int)pos.Y * GameConfig.TileCellSize);
            brush.Instance.DrawRect(
                new Rect2(pos,GameConfig.TileCellSizeVector2I),
                Colors.Green, false, 3f / textureRect.Scale.X
            );
        }
    }

    //返回鼠标是否在texture区域内
    private bool IsMouseInTexture()
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
}