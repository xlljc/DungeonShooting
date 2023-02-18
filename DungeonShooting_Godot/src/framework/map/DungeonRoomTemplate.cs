
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
    //选中左/右点
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
            if (Mathf.Abs(mousePosition.Y - mapRect.Position.Y) <= 8 && mousePosition.X >= mapRect.Position.X &&
                mousePosition.X <= mapRect.Position.X + mapRect.Size.X) //上
            {
                _hover = true;
                _hoverDirection = DoorDirection.N;
                var mouseOffset = Approach(mousePosition.X, tileSize.X);
                _hoverPoint1 = new Vector2(mouseOffset - tileSize.X * 2, mapRect.Position.Y);
                _hoverPoint2 = new Vector2(_hoverPoint1.X + tileSize.X * 4, _hoverPoint1.Y);

                //判断是否能放下新的门
                if (_hoverPoint1.X <= mapRect.Position.X || _hoverPoint2.X >= mapRect.Position.X + mapRect.Size.X || CheckDoorCollision(mapRect))
                {
                    _canPut = false;
                    FindHoverPoint(mouseOffset);
                }
                else
                {
                    _canPut = true;
                    _hasActivePoint = false;
                    _activeArea = null;
                }
            }
            else if (Mathf.Abs(mousePosition.X - mapRect.Position.X) <= 8 && mousePosition.Y >= mapRect.Position.Y &&
                     mousePosition.Y <= mapRect.Position.Y + mapRect.Size.Y) //左
            {
                _hover = true;
                _hoverDirection = DoorDirection.W;
                var mouseOffset = Approach(mousePosition.Y, tileSize.Y);
                _hoverPoint1 = new Vector2(mapRect.Position.X, mouseOffset - tileSize.Y * 2);
                _hoverPoint2 = new Vector2(_hoverPoint1.X, _hoverPoint1.Y + tileSize.X * 4);

                //判断是否能放下新的门
                if (_hoverPoint1.Y <= mapRect.Position.Y || _hoverPoint2.Y >= mapRect.Position.Y + mapRect.Size.Y || CheckDoorCollision(mapRect))
                {
                    _canPut = false;
                    FindHoverPoint(mouseOffset);
                }
                else
                {
                    _canPut = true;
                }
            }
            else if (Mathf.Abs(mousePosition.Y - (mapRect.Position.Y + mapRect.Size.Y)) <= 8 &&
                     mousePosition.X >= mapRect.Position.X &&
                     mousePosition.X <= mapRect.Position.X + mapRect.Size.X) //下
            {
                _hover = true;
                _hoverDirection = DoorDirection.S;
                var mouseOffset = Approach(mousePosition.X, tileSize.X);
                _hoverPoint1 = new Vector2(mouseOffset - tileSize.X * 2,
                    mapRect.Position.Y + mapRect.Size.Y);
                _hoverPoint2 = new Vector2(_hoverPoint1.X + tileSize.X * 4, _hoverPoint1.Y);

                //判断是否能放下新的门
                if (_hoverPoint1.X <= mapRect.Position.X || _hoverPoint2.X >= mapRect.Position.X + mapRect.Size.X || CheckDoorCollision(mapRect))
                {
                    _canPut = false;
                    FindHoverPoint(mouseOffset);
                }
                else
                {
                    _canPut = true;
                }
            }
            else if (Mathf.Abs(mousePosition.X - (mapRect.Position.X + mapRect.Size.X)) <= 8 &&
                     mousePosition.Y >= mapRect.Position.Y &&
                     mousePosition.Y <= mapRect.Position.Y + mapRect.Size.Y) //右
            {
                _hover = true;
                _hoverDirection = DoorDirection.E;
                var mouseOffset = Approach(mousePosition.Y, tileSize.Y);
                _hoverPoint1 = new Vector2(mapRect.Position.X + mapRect.Size.X,
                    mouseOffset - tileSize.Y * 2);
                _hoverPoint2 = new Vector2(_hoverPoint1.X, _hoverPoint1.Y + tileSize.X * 4);

                //判断是否能放下新的门
                if (_hoverPoint1.Y <= mapRect.Position.Y || _hoverPoint2.Y >= mapRect.Position.Y + mapRect.Size.Y || CheckDoorCollision(mapRect))
                {
                    _canPut = false;
                    FindHoverPoint(mouseOffset);
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
                _hasActivePoint = false;
                _activeArea = null;
            }

            //判断是否可以创建新的点
            if (isClick && _canPut)
            {
                CreateDoorArea(mapRect);
            }
            
            QueueRedraw();
        }
        else
        {
            _hover = false;
            _canPut = false;
            _hasActivePoint = false;
            _activeArea = null;
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
                if (!_hasActivePoint) //这里判断是否悬停在拖动点上
                {
                    var color = _canPut ? new Color(0, 1, 0, 0.2f) : new Color(1, 0, 0, 0.2f);
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
                var flag = _hasActivePoint && _activeArea == doorAreaInfo;
                var color3 = (flag && _activePointType == 0) ? new Color(1, 1, 1, 0.8f) : color2;
                var color4 = (flag && _activePointType == 1) ? new Color(1, 1, 1, 0.8f) : color2;
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
                                mapRange.Position.Y + doorAreaInfo.Start + 2), 5, color3);
                        DrawCircle(
                            new Vector2(mapRange.Position.X + mapRange.Size.X,
                                mapRange.Position.Y + doorAreaInfo.End + 2),
                            5, color4);
                        break;
                    case DoorDirection.W:
                        DrawRect(
                            new Rect2(
                                new Vector2(mapRange.Position.X - 30, mapRange.Position.Y + doorAreaInfo.Start + 2),
                                30, doorAreaInfo.End - doorAreaInfo.Start), color2);
                        DrawCircle(new Vector2(mapRange.Position.X, mapRange.Position.Y + doorAreaInfo.Start + 2), 5,
                            color3);
                        DrawCircle(new Vector2(mapRange.Position.X, mapRange.Position.Y + doorAreaInfo.End + 2), 5,
                            color4);
                        break;
                    case DoorDirection.S:
                        DrawRect(
                            new Rect2(
                                new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2,
                                    mapRange.Position.Y + mapRange.Size.Y), doorAreaInfo.End - doorAreaInfo.Start, 30),
                            color2);
                        DrawCircle(
                            new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2,
                                mapRange.Position.Y + mapRange.Size.Y), 5, color3);
                        DrawCircle(
                            new Vector2(mapRange.Position.X + doorAreaInfo.End + 2,
                                mapRange.Position.Y + mapRange.Size.Y),
                            5, color4);
                        break;
                    case DoorDirection.N:
                        DrawRect(
                            new Rect2(
                                new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2, mapRange.Position.Y - 30),
                                doorAreaInfo.End - doorAreaInfo.Start, 30), color2);
                        DrawCircle(new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2, mapRange.Position.Y), 5,
                            color3);
                        DrawCircle(new Vector2(mapRange.Position.X + doorAreaInfo.End + 2, mapRange.Position.Y), 5,
                            color4);
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
        doorAreaInfo.StartPosition = _hoverPoint1;
        doorAreaInfo.EndPosition = _hoverPoint2;
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

    //检查是否能放下门
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
                        if (CheckValueCollision(doorAreaInfo.StartPosition.Y, doorAreaInfo.EndPosition.Y,  _hoverPoint1.Y, _hoverPoint2.Y))
                        {
                            return true;
                        }
                        break;
                    case DoorDirection.S:
                    case DoorDirection.N:
                        if (CheckValueCollision(doorAreaInfo.StartPosition.X, doorAreaInfo.EndPosition.X,  _hoverPoint1.X, _hoverPoint2.X))
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
        return !(h2 < o1 - 3 * size || o2 + 3 * size < h1);
    }

    private void FindHoverPoint(float mouseOffset)
    {
        //检测是否有碰撞的点
        var flag = false;
        foreach (var doorAreaInfo in _doorConfigs)
        {
            if (doorAreaInfo.Direction == _hoverDirection)
            {
                if (_hoverDirection == DoorDirection.N || _hoverDirection == DoorDirection.S)
                {
                    if (Math.Abs(doorAreaInfo.StartPosition.X - mouseOffset) < 0.0001f)
                    {
                        _hasActivePoint = true;
                        _activePointType = 0;
                        _activeArea = doorAreaInfo;
                        flag = true;
                        break;
                    }
                    else if (Math.Abs(doorAreaInfo.EndPosition.X - mouseOffset) < 0.0001f)
                    {
                        _hasActivePoint = true;
                        _activePointType = 1;
                        _activeArea = doorAreaInfo;
                        flag = true;
                        break;
                    }
                }
                else
                {
                    if (Math.Abs(doorAreaInfo.StartPosition.Y - mouseOffset) < 0.0001f)
                    {
                        _hasActivePoint = true;
                        _activePointType = 0;
                        _activeArea = doorAreaInfo;
                        flag = true;
                        break;
                    }
                    else if (Math.Abs(doorAreaInfo.EndPosition.Y - mouseOffset) < 0.0001f)
                    {
                        _hasActivePoint = true;
                        _activePointType = 1;
                        _activeArea = doorAreaInfo;
                        flag = true;
                        break;
                    }
                }
            }
        }

        if (!flag)
        {
            _hasActivePoint = false;
            _activeArea = null;
        }
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