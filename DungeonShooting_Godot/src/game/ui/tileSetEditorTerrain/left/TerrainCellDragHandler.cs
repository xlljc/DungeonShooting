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
    
    public void InitTexture(TextureRect textureRect)
    {
        _textureRect = textureRect;
        _texture = textureRect.Texture;
    }

    public void SetRect(Rect2I rect)
    {
        _rect2I = rect;
    }

    public override void _Process(double delta)
    {
        if (DragOutline)
        {
            QueueRedraw();
        }
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        var sprite = new Sprite2D();
        sprite.Texture = _texture;
        sprite.RegionEnabled = true;
        sprite.RegionRect = _rect2I;
        var control = new Control();
        control.AddChild(sprite);
        control.ZIndex = 10;
        control.Scale = _textureRect.Scale;
        SetDragPreview(control);
        return "";
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