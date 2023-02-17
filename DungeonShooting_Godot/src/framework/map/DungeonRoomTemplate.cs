
using System.Collections.Generic;
using Godot;

[Tool]
public partial class DungeonRoomTemplate : TileMap
{
    //是否悬停在线上
    private bool _hover = false;
    
    //悬停点
    private bool _hasHoverPoint = false;
    private Vector2 _hoverPoint1;
    private Vector2 _hoverPoint2;
    private bool _canPut = false;

    //选中点
    private bool _hasActivePoint = false;
    private byte _activePointType = 0;
    private DoorAreaInfo _activeArea = null;
    
    private bool _mouseDown = false;

    //
    private List<DoorAreaInfo> _doorConfigs = new List<DoorAreaInfo>();

    public override void _Process(double delta)
    {
        var isClick = false;
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            if (!_mouseDown)
            {
                _mouseDown = true;
                isClick = true;
            }
        }
        else if (_mouseDown)
        {
            _mouseDown = false;
            isClick = false;
        }
        
        if (TileSet != null)
        {
            var rect = CalcTileRange();
            var mousePosition = GetLocalMousePosition();

            var tileSize = TileSet.TileSize;
            if (Mathf.Abs(mousePosition.Y - rect.Position.Y) <= 2 && mousePosition.X >= rect.Position.X &&
                mousePosition.X <= rect.Position.X + rect.Size.X) //上
            {
                _hover = true;
                _hoverPoint1 = new Vector2(Approach(mousePosition.X, tileSize.X) - tileSize.X * 2, rect.Position.Y);
                _hoverPoint2 = new Vector2(_hoverPoint1.X + tileSize.X * 4, _hoverPoint1.Y);

                if (_hoverPoint1.X <= rect.Position.X && _hoverPoint2.X >= rect.Position.X + rect.Size.X)
                {
                    _canPut = false;
                }
                else
                {
                    _canPut = true;
                }
            }
            else if (Mathf.Abs(mousePosition.X - rect.Position.X) <= 2 && mousePosition.Y >= rect.Position.Y &&
                     mousePosition.Y <= rect.Position.Y + rect.Size.Y) //左
            {
                _hover = true;
                _hoverPoint1 = new Vector2(rect.Position.X, Approach(mousePosition.Y, tileSize.Y) - tileSize.Y * 2);
                _hoverPoint2 = new Vector2(_hoverPoint1.X, _hoverPoint1.Y + tileSize.X * 4);
            }
            else if (Mathf.Abs(mousePosition.Y - (rect.Position.Y + rect.Size.Y)) <= 2 &&
                     mousePosition.X >= rect.Position.X && mousePosition.X <= rect.Position.X + rect.Size.X) //下
            {
                _hover = true;
                _hoverPoint1 = new Vector2(Approach(mousePosition.X, tileSize.X) - tileSize.X * 2, rect.Position.Y + rect.Size.Y);
                _hoverPoint2 = new Vector2(_hoverPoint1.X + tileSize.X * 4, _hoverPoint1.Y);
                
                if (_hoverPoint1.X <= rect.Position.X && _hoverPoint2.X >= rect.Position.X + rect.Size.X)
                {
                    _canPut = false;
                }
                else
                {
                    _canPut = true;
                }
            }
            else if (Mathf.Abs(mousePosition.X - (rect.Position.X + rect.Size.X)) <= 2 &&
                     mousePosition.Y >= rect.Position.Y && mousePosition.Y <= rect.Position.Y + rect.Size.Y) //右
            {
                _hover = true;
                _hoverPoint1 = new Vector2(rect.Position.X + rect.Size.X, Approach(mousePosition.Y, tileSize.Y) - tileSize.Y * 2);
                _hoverPoint2 = new Vector2(_hoverPoint1.X, _hoverPoint1.Y + tileSize.X * 4);
            }
            else
            {
                _hover = false;
                _hasHoverPoint = false;
                _canPut = false;
            }

            QueueRedraw();
        }
    }

    public override void _Draw()
    {
        if (TileSet != null)
        {
            //绘制地图轮廓
            DrawRect(CalcTileRange(), _hover ? Colors.Green : new Color(0.03137255F,0.59607846F,0.03137255F), false, 2);
            //DrawCircle(GetLocalMousePosition(), 10, new Color(1, 0, 0,  0.5f));
            if (_hasHoverPoint)
            {
                DrawCircle(_hoverPoint1, 3, new Color(1, 0, 0, 0.5f));
                DrawCircle(_hoverPoint2, 3, new Color(1, 0, 0, 0.5f));
            }
        }
    }

    private void CreateDoorArea()
    {
        
    }

    private Rect2 CalcTileRange()
    {
        var usedRect = GetUsedRect();
        var pos = usedRect.Position * TileSet.TileSize;
        var size = usedRect.Size * TileSet.TileSize;
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