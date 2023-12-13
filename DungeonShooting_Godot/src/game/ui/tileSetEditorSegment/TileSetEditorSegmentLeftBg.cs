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
            var p = _leftBg.L_TileTexture.Instance.Position + pos;
            _leftBg.L_TileTexture.Instance.Position = p;
            _leftBg.L_Grid.Instance.Material.SetShaderMaterialParameter(ShaderParamNames.Offset, -p);
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
                        var scale = _leftBg.L_TileTexture.Instance.Scale;
                        scale = new Vector2(Mathf.Max(0.1f, scale.X / 1.1f), Mathf.Max(0.1f, scale.Y / 1.1f));
                        _leftBg.L_TileTexture.Instance.Scale = scale;
                        OnLeftBgResize();
                    }
                }
                else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
                {
                    if (GetGlobalRect().HasPoint(mouseButton.GlobalPosition))
                    {
                        //放大
                        var scale = _leftBg.L_TileTexture.Instance.Scale;
                        scale = new Vector2(Mathf.Min(20f, scale.X * 1.1f), Mathf.Min(20f, scale.Y * 1.1f));
                        _leftBg.L_TileTexture.Instance.Scale = scale;
                        OnLeftBgResize();
                    }
                }
            }
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
        colorRect.Material.SetShaderMaterialParameter(ShaderParamNames.GridSize, GameConfig.TileCellSize * sprite.Scale.X);
        
    }
}