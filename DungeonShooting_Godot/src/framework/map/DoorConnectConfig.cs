
using Godot;

[Tool]
public partial class DoorConnectConfig : Node2D
{

    private TileMap _tileMap;
    private bool _hover = false;
    
    private Vector2? _hoverCircle = null;
    
    
    public override void _Ready()
    {
        _tileMap = GetParentOrNull<TileMap>();
    }

    public override void _Process(double delta)
    {
        if (_tileMap != null && _tileMap.TileSet != null)
        {
            var rect = CalcTileRange();
            var mousePosition = GetLocalMousePosition();

            if (Mathf.Abs(mousePosition.Y - rect.Position.Y) <= 2 && mousePosition.X >= rect.Position.X &&
                mousePosition.X <= rect.Position.X + rect.Size.X) //上
            {
                _hover = true;
                _hoverCircle = new Vector2(Approach(mousePosition.X, _tileMap.TileSet.TileSize.X), rect.Position.Y);
            }
            else if (Mathf.Abs(mousePosition.X - rect.Position.X) <= 2 && mousePosition.Y >= rect.Position.Y &&
                     mousePosition.Y <= rect.Position.Y + rect.Size.Y) //左
            {
                _hover = true;
                _hoverCircle = new Vector2(rect.Position.X, Approach(mousePosition.Y, _tileMap.TileSet.TileSize.Y));
            }
            else if (Mathf.Abs(mousePosition.Y - (rect.Position.Y + rect.Size.Y)) <= 2 &&
                     mousePosition.X >= rect.Position.X && mousePosition.X <= rect.Position.X + rect.Size.X) //下
            {
                _hover = true;
                _hoverCircle = new Vector2(Approach(mousePosition.X, _tileMap.TileSet.TileSize.X), rect.Position.Y + rect.Size.Y);
            }
            else if (Mathf.Abs(mousePosition.X - (rect.Position.X + rect.Size.X)) <= 2 &&
                     mousePosition.Y >= rect.Position.Y && mousePosition.Y <= rect.Position.Y + rect.Size.Y) //右
            {
                _hover = true;
                _hoverCircle = new Vector2(rect.Position.X + rect.Size.X, Approach(mousePosition.Y, _tileMap.TileSet.TileSize.Y));
            }
            else
            {
                _hover = false;
                _hoverCircle = null;
            }

            QueueRedraw();

        }
    }

    public override void _Draw()
    {
        if (_tileMap != null && _tileMap.TileSet != null)
        {
            DrawRect(CalcTileRange(), _hover ? Colors.Green : new Color(0.03137255F,0.59607846F,0.03137255F), false, 2);
            //DrawCircle(GetLocalMousePosition(), 10, new Color(1, 0, 0,  0.5f));
            if (_hoverCircle != null)
            {
                DrawCircle((Vector2)_hoverCircle, 3, new Color(1, 0, 0, 0.5f));
            }
        }
        //DrawLine(Vector2.Zero, GetLocalMousePosition(), Colors.Red);
    }

    private Rect2 CalcTileRange()
    {
        var usedRect = _tileMap.GetUsedRect();
        var pos = usedRect.Position * _tileMap.TileSet.TileSize;
        var size = usedRect.Size * _tileMap.TileSet.TileSize;
        return new Rect2(ToLocal(pos), size);
    }

    private float Approach(float value, float period)
    {
        var temp = value % period;
        if (Mathf.Abs(temp) >= period / 2)
        {
            return ((int)(value / period) + (value >= 0 ? 1 : -1)) * period;
        }

        return (int)(value / period) * period;
    }
}