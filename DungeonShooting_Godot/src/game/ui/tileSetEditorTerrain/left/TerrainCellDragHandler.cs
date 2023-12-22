using Godot;

namespace UI.TileSetEditorTerrain;

public partial class TerrainCellDragHandler : TextureButton
{
    /// <summary>
    /// 是否绘制轮廓
    /// </summary>
    public bool DragOutline { get; set; } = false;

    private TextureRect _textureRect;
    private Texture2D _texture;
    private Rect2I _rect2I;
    private MaskCell _maskCell;
    private TileSetEditorTerrainPanel _panel;
    
    public void Init(MaskCell maskCell)
    {
        _maskCell = maskCell;
        _panel = maskCell.CellNode.UiPanel;
        _textureRect = _panel.S_LeftBg.L_TileTexture.Instance;
        _texture = _textureRect.Texture;
    }

    public void SetRect(Rect2I rect)
    {
        _rect2I = rect;
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
        if (_maskCell.Grid.SelectIndex == _maskCell.Index && _panel.IsDraggingCell)
        {
            if (!Input.IsActionPressed(InputAction.MouseLeft))
            {
                _panel.IsDraggingCell = false;
            }
        }
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        _panel.IsDraggingCell = true;
        _maskCell.Grid.SelectIndex = _maskCell.Index;
        var sprite = new Sprite2D();
        sprite.Texture = _texture;
        sprite.RegionEnabled = true;
        sprite.RegionRect = _rect2I;
        var control = new Control();
        control.AddChild(sprite);
        control.ZIndex = 10;
        control.Scale = _panel.S_LeftBottomBg.L_TileTexture.Instance.Scale;
        SetDragPreview(control);
        return _rect2I;
    }

    public override void _Draw()
    {
        if (DragOutline)
        {
            //选中时绘制轮廓
            DrawRect(
                new Rect2(Vector2.Zero, Size),
                new Color(0, 1, 1), false, 2f / _textureRect.Scale.X
            );
        }
    }
}