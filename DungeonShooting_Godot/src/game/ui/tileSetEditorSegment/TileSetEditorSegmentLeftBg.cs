using Godot;

namespace UI.TileSetEditorSegment;

public partial class TileSetEditorSegmentLeftBg : ColorRect, IUiNodeScript
{
    private TileSetEditorSegment.LeftBg _leftBg;
    private DragBinder _dragBinder;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _leftBg = (TileSetEditorSegment.LeftBg)uiNode;
        _dragBinder = DragUiManager.BindDrag(this, OnDrag);
        Resized += OnLeftBgResize;
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
    
    //缩小
    private void Shrink()
    {
        // var sprite = _leftBg.L_TileTexture.Instance;
        // var pos = sprite.GetLocalMousePosition();
        // var scale = sprite.Scale / 1.1f;
        // if (scale.LengthSquared() >= 0.5f)
        // {
        //     sprite.Scale = scale;
        //     var tempPos = sprite.Position + pos * 0.1f * scale;
        //     SetGridTransform(tempPos, scale.X);
        // }
        // OnLeftBgResize();
        
        var scale = _leftBg.L_TileTexture.Instance.Scale;
        scale = new Vector2(Mathf.Max(0.1f, scale.X / 1.1f), Mathf.Max(0.1f, scale.Y / 1.1f));
        _leftBg.L_TileTexture.Instance.Scale = scale;
        OnLeftBgResize();
    }
    //放大
    private void Magnify()
    {
        // var sprite = _leftBg.L_TileTexture.Instance;
        // var pos = GetLocalMousePosition();
        // var prevScale = sprite.Scale;
        // var scale = prevScale * 1.1f;
        // if (scale.LengthSquared() <= 2000)
        // {
        //     sprite.Scale = scale;
        //     var tempPos = sprite.Position - pos * 0.1f * prevScale;
        //     SetGridTransform(tempPos, scale.X);
        // }
        // OnLeftBgResize();
        
        var scale = _leftBg.L_TileTexture.Instance.Scale;
        scale = new Vector2(Mathf.Min(20f, scale.X * 1.1f), Mathf.Min(20f, scale.Y * 1.1f));
        _leftBg.L_TileTexture.Instance.Scale = scale;
        OnLeftBgResize();
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
    
    private void SetGridTransform(Vector2 pos, float scale)
    {
        var colorRect = _leftBg.L_Grid.Instance;
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.GridSize, GameConfig.TileCellSize * scale);
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.Offset, -pos);
    }
}