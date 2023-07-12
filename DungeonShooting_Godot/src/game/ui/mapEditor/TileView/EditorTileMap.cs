using Godot;
using Godot.Collections;

namespace UI.MapEditor;

public partial class EditorTileMap : TileMap
{
    //鼠标坐标
    private Vector2 _mousePosition;
    //鼠标所在的cell坐标
    private Vector2I _mouseCellPosition;
    //上一帧鼠标所在的cell坐标
    private Vector2I _prevMouseCellPosition = new Vector2I(-99999, -99999);
    //左键开始按下时鼠标所在的坐标
    private Vector2I _mouseStartCellPosition;
    //鼠标中建是否按下
    private bool _isMiddlePressed = false;
    private Vector2 _moveOffset;
    //左键是否按下
    private bool _isLeftPressed = false;
    //右键是否按下
    private bool _isRightPressed = false;
    //绘制填充区域
    private bool _drawFullRect = false;

    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        _drawFullRect = false;
        var position = GetLocalMousePosition();
        // _mouseCellPosition = new Vector2I(
        //     Mathf.FloorToInt(position.X / GameConfig.TileCellSize),
        //     Mathf.FloorToInt(position.Y / GameConfig.TileCellSize)
        // );
        _mouseCellPosition = LocalToMap(position);
        _mousePosition = new Vector2(
            _mouseCellPosition.X * GameConfig.TileCellSize,
            _mouseCellPosition.Y * GameConfig.TileCellSize
        );
        
        //左键绘制
        if (_isLeftPressed)
        {
            if (Input.IsKeyPressed(Key.Shift)) //按住shift绘制矩形
            {
                _drawFullRect = true;
            }
            else if (_prevMouseCellPosition != _mouseCellPosition) //鼠标位置变过
            {
                _prevMouseCellPosition = _mouseCellPosition;
                //绘制单个图块
                //SetCell(GameConfig.FloorMapLayer, _mouseCellPosition, 0, new Vector2I(0,8));
                //绘制自动图块
                SetSingleAutoCell(_mouseCellPosition);
            }
        }
        else if (_isRightPressed) //右键擦除
        {
            if (Input.IsKeyPressed(Key.Shift)) //按住shift擦除矩形
            {
                _drawFullRect = true;
            }
            else if (_prevMouseCellPosition != _mouseCellPosition)
            {
                _prevMouseCellPosition = _mouseCellPosition;
                EraseSingleAutoCell(_mouseCellPosition);
            }
        }
        else if (_isMiddlePressed) //中建移动
        {
            //GD.Print("移动...");
            Position = GetGlobalMousePosition() + _moveOffset;
        }
    }

    /// <summary>
    /// 绘制辅助线
    /// </summary>
    public void DrawGuides(CanvasItem canvasItem)
    {
        //轴线
        canvasItem.DrawLine(new Vector2(0, 2000), new Vector2(0, -2000), Colors.Green);
        canvasItem.DrawLine(new Vector2(2000, 0), new Vector2( -2000, 0), Colors.Red);

        if (_drawFullRect) //绘制填充矩形
        {
            var size = TileSet.TileSize;
            var cellPos = _mouseStartCellPosition;
            var temp = size;
            if (_mouseStartCellPosition.X > _mouseCellPosition.X)
            {
                cellPos.X += 1;
                temp.X -= size.X;
            }
            if (_mouseStartCellPosition.Y > _mouseCellPosition.Y)
            {
                cellPos.Y += 1;
                temp.Y -= size.Y;
            }

            var pos = cellPos * size;
            canvasItem.DrawRect(new Rect2(pos, _mousePosition - pos + temp), Colors.Wheat, false);
        }
        else //绘制单格
        {
            canvasItem.DrawRect(new Rect2(_mousePosition, TileSet.TileSize), Colors.Wheat, false);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left) //左键
            {
                if (mouseButton.Pressed) //按下
                {
                    _mouseStartCellPosition = LocalToMap(GetLocalMousePosition());
                }
                else
                {
                    if (_drawFullRect) //松开, 提交绘制的矩形区域
                    {
                        SetRectAutoCell(_mouseStartCellPosition, _mouseCellPosition);
                        _drawFullRect = false;
                    }
                }

                _isLeftPressed = mouseButton.Pressed;
            }
            else if (mouseButton.ButtonIndex == MouseButton.Right) //右键
            {
                if (mouseButton.Pressed) //按下
                {
                    _mouseStartCellPosition = LocalToMap(GetLocalMousePosition());
                }
                else
                {
                    if (_drawFullRect) //松开, 提交擦除的矩形区域
                    {
                        EraseRectAutoCell(_mouseStartCellPosition, _mouseCellPosition);
                        _drawFullRect = false;
                    }
                }
                
                _isRightPressed = mouseButton.Pressed;
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelDown)
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

    //绘制单个自动贴图
    private void SetSingleAutoCell(Vector2I position)
    {
        //绘制自动图块
        var arr = new Array<Vector2I>(new [] { position });
        SetCellsTerrainConnect(GameConfig.FloorMapLayer, arr, 0, 0, false);
    }
    
    //绘制区域自动贴图
    private void SetRectAutoCell(Vector2I start, Vector2I end)
    {
        if (start.X > end.X)
        {
            var temp = end.X;
            end.X = start.X;
            start.X = temp;
        }
        if (start.Y > end.Y)
        {
            var temp = end.Y;
            end.Y = start.Y;
            start.Y = temp;
        }

        var x = end.X - start.X + 1;
        var y = end.Y - start.Y + 1;
        var index = 0;
        var array = new Vector2I[x * y];
        for (var i = 0; i < x; i++)
        {
            for (var j = 0; j < y; j++)
            {
                array[index++] = new Vector2I(start.X + i, start.Y + j);
            }
        }

        var arr = new Array<Vector2I>(array);
        SetCellsTerrainConnect(GameConfig.FloorMapLayer, arr, 0, 0, false);
    }

    //擦除单个自动图块
    private void EraseSingleAutoCell(Vector2I position)
    {
        EraseCell(GameConfig.FloorMapLayer, position);
    }
    
    //擦除一个区域内的自动贴图
    private void EraseRectAutoCell(Vector2I start, Vector2I end)
    {
        if (start.X > end.X)
        {
            var temp = end.X;
            end.X = start.X;
            start.X = temp;
        }
        if (start.Y > end.Y)
        {
            var temp = end.Y;
            end.Y = start.Y;
            start.Y = temp;
        }

        var x = end.X - start.X + 1;
        var y = end.Y - start.Y + 1;
        for (var i = 0; i < x; i++)
        {
            for (var j = 0; j < y; j++)
            {
                EraseCell(GameConfig.FloorMapLayer, new Vector2I(start.X + i, start.Y + j));
            }
        }
    }
}