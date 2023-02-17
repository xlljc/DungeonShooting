
using System;
using System.Collections.Generic;
using Godot;

[Tool]
public partial class DungeonRoomTemplate : TileMap
{
    //是否悬停在线上
    private bool _hover = false;

    //悬停点
    private Vector2 _hoverPoint1;
    private Vector2 _hoverPoint2;
    private DoorDirection _hoverDirection;
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
            var mapRect = CalcTileRange();
            var mousePosition = GetLocalMousePosition();

            var tileSize = TileSet.TileSize;
            if (Mathf.Abs(mousePosition.Y - mapRect.Position.Y) <= 5 && mousePosition.X >= mapRect.Position.X &&
                mousePosition.X <= mapRect.Position.X + mapRect.Size.X) //上
            {
                _hover = true;
                _hoverDirection = DoorDirection.N;
                _hoverPoint1 = new Vector2(Approach(mousePosition.X, tileSize.X) - tileSize.X * 2, mapRect.Position.Y);
                _hoverPoint2 = new Vector2(_hoverPoint1.X + tileSize.X * 4, _hoverPoint1.Y);

                if (_hoverPoint1.X <= mapRect.Position.X || _hoverPoint2.X >= mapRect.Position.X + mapRect.Size.X || CheckDoorCollision(mapRect))
                {
                    _canPut = false;
                }
                else
                {
                    _canPut = true;
                }
            }
            else if (Mathf.Abs(mousePosition.X - mapRect.Position.X) <= 5 && mousePosition.Y >= mapRect.Position.Y &&
                     mousePosition.Y <= mapRect.Position.Y + mapRect.Size.Y) //左
            {
                _hover = true;
                _hoverDirection = DoorDirection.W;
                _hoverPoint1 = new Vector2(mapRect.Position.X, Approach(mousePosition.Y, tileSize.Y) - tileSize.Y * 2);
                _hoverPoint2 = new Vector2(_hoverPoint1.X, _hoverPoint1.Y + tileSize.X * 4);

                if (_hoverPoint1.Y <= mapRect.Position.Y || _hoverPoint2.Y >= mapRect.Position.Y + mapRect.Size.Y)
                {
                    _canPut = false;
                }
                else
                {
                    _canPut = true;
                }
            }
            else if (Mathf.Abs(mousePosition.Y - (mapRect.Position.Y + mapRect.Size.Y)) <= 5 &&
                     mousePosition.X >= mapRect.Position.X &&
                     mousePosition.X <= mapRect.Position.X + mapRect.Size.X) //下
            {
                _hover = true;
                _hoverDirection = DoorDirection.S;
                _hoverPoint1 = new Vector2(Approach(mousePosition.X, tileSize.X) - tileSize.X * 2,
                    mapRect.Position.Y + mapRect.Size.Y);
                _hoverPoint2 = new Vector2(_hoverPoint1.X + tileSize.X * 4, _hoverPoint1.Y);

                if (_hoverPoint1.X <= mapRect.Position.X || _hoverPoint2.X >= mapRect.Position.X + mapRect.Size.X)
                {
                    _canPut = false;
                }
                else
                {
                    _canPut = true;
                }
            }
            else if (Mathf.Abs(mousePosition.X - (mapRect.Position.X + mapRect.Size.X)) <= 5 &&
                     mousePosition.Y >= mapRect.Position.Y &&
                     mousePosition.Y <= mapRect.Position.Y + mapRect.Size.Y) //右
            {
                _hover = true;
                _hoverDirection = DoorDirection.E;
                _hoverPoint1 = new Vector2(mapRect.Position.X + mapRect.Size.X,
                    Approach(mousePosition.Y, tileSize.Y) - tileSize.Y * 2);
                _hoverPoint2 = new Vector2(_hoverPoint1.X, _hoverPoint1.Y + tileSize.X * 4);

                if (_hoverPoint1.Y <= mapRect.Position.Y || _hoverPoint2.Y >= mapRect.Position.Y + mapRect.Size.Y)
                {
                    _canPut = false;
                }
                else
                {
                    _canPut = true;
                }
            }
            else
            {
                _hover = false;
                _canPut = false;
            }

            QueueRedraw();

            if (isClick && _canPut)
            {
                CreateDoorArea(mapRect);
            }
        }
        else
        {
            _hover = false;
        }
    }

    public override void _Draw()
    {
        if (TileSet != null)
        {
            //绘制地图轮廓
            var mapRange = CalcTileRange();
            mapRange.Position -= new Vector2(2, 2);
            mapRange.Size += new Vector2(4, 4);
            DrawRect(mapRange, _hover ? Colors.Green : new Color(0.03137255F, 0.59607846F, 0.03137255F), false, 2);

            //绘制悬停
            if (_hover)
            {
                if (_canPut)
                {
                    var color = new Color(0, 1, 0, 0.2f);
                    switch (_hoverDirection)
                    {
                        case DoorDirection.E:
                            DrawRect(
                                new Rect2(new Vector2(_hoverPoint1.X + 2, _hoverPoint1.Y), 30,
                                    _hoverPoint2.Y - _hoverPoint1.Y), color);
                            DrawCircle(new Vector2(_hoverPoint1.X + 2, _hoverPoint1.Y), 5, color);
                            DrawCircle(new Vector2(_hoverPoint2.X + 2, _hoverPoint2.Y), 5, color);
                            break;
                        case DoorDirection.W:
                            DrawRect(
                                new Rect2(new Vector2(_hoverPoint1.X - 2 - 30, _hoverPoint1.Y), 30,
                                    _hoverPoint2.Y - _hoverPoint1.Y), color);
                            DrawCircle(new Vector2(_hoverPoint1.X - 2, _hoverPoint1.Y), 5, color);
                            DrawCircle(new Vector2(_hoverPoint2.X - 2, _hoverPoint2.Y), 5, color);
                            break;
                        case DoorDirection.S:
                            DrawRect(
                                new Rect2(new Vector2(_hoverPoint1.X, _hoverPoint1.Y + 2),
                                    _hoverPoint2.X - _hoverPoint1.X, 30), color);
                            DrawCircle(new Vector2(_hoverPoint1.X, _hoverPoint1.Y + 2), 5, color);
                            DrawCircle(new Vector2(_hoverPoint2.X, _hoverPoint2.Y + 2), 5, color);
                            break;
                        case DoorDirection.N:
                            DrawRect(
                                new Rect2(new Vector2(_hoverPoint1.X, _hoverPoint1.Y - 30 - 2),
                                    _hoverPoint2.X - _hoverPoint1.X, 30), color);
                            DrawCircle(new Vector2(_hoverPoint1.X, _hoverPoint1.Y - 2), 5, color);
                            DrawCircle(new Vector2(_hoverPoint2.X, _hoverPoint2.Y - 2), 5, color);
                            break;
                    }
                }
            }

            var color2 = new Color(0, 1, 0, 0.8f);
            //绘制已经存在的
            foreach (var doorAreaInfo in _doorConfigs)
            {
                switch (doorAreaInfo.Direction)
                {
                    case DoorDirection.E:
                        DrawRect(
                            new Rect2(
                                new Vector2(mapRange.Position.X + mapRange.Size.X,
                                    mapRange.Position.Y + doorAreaInfo.Start + 2), 30,
                                doorAreaInfo.End - doorAreaInfo.Start), color2);
                        DrawCircle(
                            new Vector2(mapRange.Position.X + mapRange.Size.X,
                                mapRange.Position.Y + doorAreaInfo.Start + 2), 5, color2);
                        DrawCircle(
                            new Vector2(mapRange.Position.X + mapRange.Size.X,
                                mapRange.Position.Y + doorAreaInfo.End + 2),
                            5, color2);
                        break;
                    case DoorDirection.W:
                        DrawRect(
                            new Rect2(
                                new Vector2(mapRange.Position.X - 30, mapRange.Position.Y + doorAreaInfo.Start + 2),
                                30, doorAreaInfo.End - doorAreaInfo.Start), color2);
                        DrawCircle(new Vector2(mapRange.Position.X, mapRange.Position.Y + doorAreaInfo.Start + 2), 5,
                            color2);
                        DrawCircle(new Vector2(mapRange.Position.X, mapRange.Position.Y + doorAreaInfo.End + 2), 5,
                            color2);
                        break;
                    case DoorDirection.S:
                        DrawRect(
                            new Rect2(
                                new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2,
                                    mapRange.Position.Y + mapRange.Size.Y), doorAreaInfo.End - doorAreaInfo.Start, 30),
                            color2);
                        DrawCircle(
                            new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2,
                                mapRange.Position.Y + mapRange.Size.Y), 5, color2);
                        DrawCircle(
                            new Vector2(mapRange.Position.X + doorAreaInfo.End + 2,
                                mapRange.Position.Y + mapRange.Size.Y),
                            5, color2);
                        break;
                    case DoorDirection.N:
                        DrawRect(
                            new Rect2(
                                new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2, mapRange.Position.Y - 30),
                                doorAreaInfo.End - doorAreaInfo.Start, 30), color2);
                        DrawCircle(new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2, mapRange.Position.Y), 5,
                            color2);
                        DrawCircle(new Vector2(mapRange.Position.X + doorAreaInfo.End + 2, mapRange.Position.Y), 5,
                            color2);
                        break;
                }
            }
        }
    }

    //创建门
    private void CreateDoorArea(Rect2 mapRect)
    {
        var doorAreaInfo = new DoorAreaInfo();
        doorAreaInfo.Direction = _hoverDirection;
        switch (_hoverDirection)
        {
            case DoorDirection.E:
            case DoorDirection.W:
                doorAreaInfo.Start = _hoverPoint1.Y - mapRect.Position.Y;
                doorAreaInfo.End = _hoverPoint2.Y - mapRect.Position.Y;
                break;
            case DoorDirection.N:
            case DoorDirection.S:
                doorAreaInfo.Start = _hoverPoint1.X - mapRect.Position.X;
                doorAreaInfo.End = _hoverPoint2.X - mapRect.Position.X;
                break;
        }

        _doorConfigs.Add(doorAreaInfo);
    }

    private bool CheckDoorCollision(Rect2 mapRect)
    {
        foreach (var doorAreaInfo in _doorConfigs)
        {
            if (doorAreaInfo.Direction == _hoverDirection)
            {
                switch (_hoverDirection)
                {
                    case DoorDirection.E:
                    case DoorDirection.W:
                        if (CheckValueCollision(mapRect.Position.Y + doorAreaInfo.Start, mapRect.Position.Y + doorAreaInfo.End,  _hoverPoint1.Y, _hoverPoint2.Y))
                        {
                            return true;
                        }
                        break;
                    case DoorDirection.S:
                    case DoorDirection.N:
                        if (CheckValueCollision(mapRect.Position.X + doorAreaInfo.Start, mapRect.Position.X + doorAreaInfo.End,  _hoverPoint1.X, _hoverPoint2.X))
                        {
                            return true;
                        }
                        break;
                }
            }
        }

        return false;
    }

    private bool CheckValueCollision(float o1, float o2, float h1, float h2)
    {
        var size = TileSet.TileSize.X;
        return !(o1 - 3 * size > h2 || o2 + 3 * size < h1);
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