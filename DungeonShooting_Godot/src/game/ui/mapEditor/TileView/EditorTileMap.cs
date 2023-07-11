using Godot;

namespace UI.MapEditor;

public partial class EditorTileMap : TileMap
{
    private Vector2 _mousePosition;
    private Vector2I _mouseCellPosition;

    public override void _Process(double delta)
    {
        var position = GetLocalMousePosition();
        _mouseCellPosition = new Vector2I(
            (int)(position.X / GameConfig.TileCellSize),
            (int)(position.Y / GameConfig.TileCellSize)
        );
        _mousePosition = new Vector2(
            _mouseCellPosition.X * GameConfig.TileCellSize,
            _mouseCellPosition.Y * GameConfig.TileCellSize
        );

        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            SetCell(GameConfig.FloorMapLayer, _mouseCellPosition, 0, new Vector2I(0,8));
        }
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(_mousePosition, GameConfig.TileCellSize, GameConfig.TileCellSize), Colors.Wheat, false);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.WheelDown)
            {
                //缩小
                Shrink();
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                //放大
                Magnify();
            }
        }
    }

    //缩小
    private void Shrink()
    {
        var scale = Scale / 1.1f;
        if (scale.LengthSquared() >= 0.5f)
        {
            Scale = scale;
        }
        else
        {
            GD.Print("太小了");
        }
    }
    //放大
    private void Magnify()
    {
        var scale = Scale * 1.1f;
        if (scale.LengthSquared() <= 2000)
        {
            Scale = scale;
        }
        else
        {
            GD.Print("太大了");
        }
    }
}