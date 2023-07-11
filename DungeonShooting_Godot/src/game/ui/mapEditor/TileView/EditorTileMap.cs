using Godot;

namespace UI.MapEditor;

public partial class EditorTileMap : TileMap
{
    //鼠标坐标
    private Vector2 _mousePosition;
    //鼠标所在的cell坐标
    private Vector2I _mouseCellPosition;
    //上一帧鼠标所在的cell坐标
    private Vector2I _prevMouseCellPosition = new Vector2I(-99999, -99999);
    //鼠标中建是否按下
    private bool _isMiddlePressed = false;
    private Vector2 _moveOffset;

    public override void _Process(double delta)
    {
        var position = GetLocalMousePosition();
        _mouseCellPosition = new Vector2I(
            Mathf.FloorToInt(position.X / GameConfig.TileCellSize),
            Mathf.FloorToInt(position.Y / GameConfig.TileCellSize)
        );
        _mousePosition = new Vector2(
            _mouseCellPosition.X * GameConfig.TileCellSize,
            _mouseCellPosition.Y * GameConfig.TileCellSize
        );

        //左键绘制
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            if (_prevMouseCellPosition != _mouseCellPosition)
            {
                _prevMouseCellPosition = _mouseCellPosition;
                SetCell(GameConfig.FloorMapLayer, _mouseCellPosition, 0, new Vector2I(0,8));
            }
        }
        else if (Input.IsMouseButtonPressed(MouseButton.Right)) //右键擦除
        {
            if (_prevMouseCellPosition != _mouseCellPosition)
            {
                _prevMouseCellPosition = _mouseCellPosition;
                EraseCell(GameConfig.FloorMapLayer, _mouseCellPosition);
            }
        }
        else if (_isMiddlePressed) //中建移动
        {
            //GD.Print("移动...");
            Position = GetGlobalMousePosition() + _moveOffset;
        }
    }

    public override void _Draw()
    {
        DrawRect(new Rect2(_mousePosition, GameConfig.TileCellSize, GameConfig.TileCellSize), Colors.Wheat, false);
        DrawLine(new Vector2(0, 2000), new Vector2(0, -2000), Colors.Green);
        DrawLine(new Vector2(2000, 0), new Vector2( -2000, 0), Colors.Red);
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
            else if (mouseButton.ButtonIndex == MouseButton.Middle)
            {
                _isMiddlePressed = mouseButton.Pressed;
                if (_isMiddlePressed)
                {
                    _moveOffset = Position - mouseButton.GlobalPosition;
                    GD.Print(_moveOffset);
                }
            }
        }
    }

    //缩小
    private void Shrink()
    {
        var pos = GetLocalMousePosition();
        var scale = Scale / 1.1f;
        if (scale.LengthSquared() >= 0.5f)
        {
            Scale = scale;
            Position += pos * 0.1f * scale;
        }
        else
        {
            GD.Print("太小了");
        }
    }
    //放大
    private void Magnify()
    {
        var pos = GetLocalMousePosition();
        var prevScale = Scale;
        var scale = prevScale * 1.1f;
        if (scale.LengthSquared() <= 2000)
        {
            Scale = scale;
            Position -= pos * 0.1f * prevScale;
        }
        else
        {
            GD.Print("太大了");
        }
    }
}