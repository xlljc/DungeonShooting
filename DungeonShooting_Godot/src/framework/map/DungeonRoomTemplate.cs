
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Godot;

[Tool]
public partial class DungeonRoomTemplate : TileMap
{
    /// <summary>
    /// 地图路径
    /// </summary>
    public static readonly string RoomTileDir = System.Environment.CurrentDirectory + "/resource/map/tileMaps/";
    
    /// <summary>
    /// 地图描述数据路径
    /// </summary>
    public static readonly string RoomTileDataDir = System.Environment.CurrentDirectory + "/resource/map/tiledata/";
    
    /// <summary>
    /// 房间配置汇总
    /// </summary>
    public static readonly string RoomTileConfigFile = System.Environment.CurrentDirectory + "/resource/map/RoomConfig.json";
    
    /// <summary>
    /// 是否启用编辑模式
    /// </summary>
    [Export(PropertyHint.None, "是否启用编辑模式")]
    public bool EnableEdit = false;
    
#if TOOLS
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
    
    
    //拖拽
    private bool _isDrag = false;
    private float _startDragValue;
    private Vector2 _startDragPositionValue;
    private bool _dragHasCollision = false;

    private bool _mouseDown = false;

    //门区域数据
    private List<DoorAreaInfo> _doorConfigs;
    private Rect2 _prevRect;

    //是否能保存
    private bool _canSave = false;
    private bool _clickSave = false;

    private DungeonTile _dungeonTile;

    //计算导航的计时器
    private float _calcTileNavTimer = 0;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            return;
        }
        EnableEdit = false;
    }

    public override void _Process(double delta)
    {
        if (!Engine.IsEditorHint())
        {
            return;
        }

        if (_dungeonTile == null)
        {
            _dungeonTile = new DungeonTile(this);
            _dungeonTile.SetFloorAtlasCoords(new List<Vector2I>() { new Vector2I(0, 8) });
            OnTileChanged();
            var callable = new Callable(this, nameof(OnTileChanged));
            if (!IsConnected("changed", callable))
            {
                Connect("changed", callable);
            }
        }
        
        //导航计算
        if (_calcTileNavTimer > 0)
        {
            _calcTileNavTimer -= (float)delta;
            //重新计算导航
            if (_calcTileNavTimer <= 0)
            {
                _dungeonTile.GenerateNavigationPolygon(0);
            }
        }

        //加载配置
        var initConfigs = false;
        if (_doorConfigs == null)
        {
            initConfigs = true;
            _doorConfigs = ReadConfig(CalcTileRange(this), Name);
        }

        //按键检测
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

        if (Input.IsMouseButtonPressed(MouseButton.Middle)) //中键移除门
        {
            if (EnableEdit && _activeArea != null)
            {
                RemoveDoorArea(_activeArea);
                _hasActivePoint = false;
                _activeArea = null;
            }
        }
        else if (TileSet != null) //编辑操作
        {
            var mapRect = CalcTileRange(this);
            var mousePosition = GetLocalMousePosition();

            if (mapRect != _prevRect)
            {
                if (!initConfigs)
                {
                    OnMapRectChange();
                }
            }

            _prevRect = mapRect;
            
            if (EnableEdit)
            {
                var tileSize = TileSet.TileSize;
                if (_isDrag) //拖拽中
                {
                    if (_activeArea != null)
                    {
                        //拖拽节点操作
                        if (_activeArea.Direction == DoorDirection.N || _activeArea.Direction == DoorDirection.S)
                        {
                            if (_activePointType == 0)
                            {
                                var mouseOffset = Approach(mousePosition.X, tileSize.X);
                                _activeArea.StartPosition = new Vector2(mouseOffset, _activeArea.StartPosition.Y);
                                _activeArea.Start = mouseOffset - mapRect.Position.X;
                                _dragHasCollision = _activeArea.StartPosition.X <= mapRect.Position.X ||
                                                    _activeArea.StartPosition.X + 3 * tileSize.X >=
                                                    _activeArea.EndPosition.X ||
                                                    CheckDoorCollision(_activeArea.Direction, _activeArea);
                            }
                            else
                            {
                                var mouseOffset = Approach(mousePosition.X, tileSize.X);
                                _activeArea.EndPosition = new Vector2(mouseOffset, _activeArea.EndPosition.Y);
                                _activeArea.End = mouseOffset - mapRect.Position.X;
                                _dragHasCollision = _activeArea.EndPosition.X >= mapRect.Position.X + mapRect.Size.X ||
                                                    _activeArea.EndPosition.X - 3 * tileSize.X <=
                                                    _activeArea.StartPosition.X ||
                                                    CheckDoorCollision(_activeArea.Direction, _activeArea);
                            }
                        }
                        else
                        {
                            if (_activePointType == 0)
                            {
                                var mouseOffset = Approach(mousePosition.Y, tileSize.Y);
                                _activeArea.StartPosition = new Vector2(_activeArea.StartPosition.X, mouseOffset);
                                _activeArea.Start = mouseOffset - mapRect.Position.Y;
                                _dragHasCollision = _activeArea.StartPosition.Y <= mapRect.Position.Y ||
                                                    _activeArea.StartPosition.Y + 3 * tileSize.Y >=
                                                    _activeArea.EndPosition.Y ||
                                                    CheckDoorCollision(_activeArea.Direction, _activeArea);
                            }
                            else
                            {
                                var mouseOffset = Approach(mousePosition.Y, tileSize.Y);
                                _activeArea.EndPosition = new Vector2(_activeArea.EndPosition.X, mouseOffset);
                                _activeArea.End = mouseOffset - mapRect.Position.Y;
                                _dragHasCollision = _activeArea.EndPosition.Y >= mapRect.Position.Y + mapRect.Size.Y ||
                                                    _activeArea.EndPosition.Y - 3 * tileSize.Y <=
                                                    _activeArea.StartPosition.Y ||
                                                    CheckDoorCollision(_activeArea.Direction, _activeArea);
                            }
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(mousePosition.Y - mapRect.Position.Y) <= 8 && mousePosition.X >= mapRect.Position.X &&
                        mousePosition.X <= mapRect.Position.X + mapRect.Size.X) //上
                    {
                        _hover = true;
                        _hoverDirection = DoorDirection.N;
                        var mouseOffset = Approach(mousePosition.X, tileSize.X);
                        _hoverPoint1 = new Vector2(mouseOffset - tileSize.X * 2, mapRect.Position.Y);
                        _hoverPoint2 = new Vector2(_hoverPoint1.X + tileSize.X * 4, _hoverPoint1.Y);

                        //判断是否能放下新的门
                        if (_hoverPoint1.X <= mapRect.Position.X ||
                            _hoverPoint2.X >= mapRect.Position.X + mapRect.Size.X ||
                            CheckDoorCollision())
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
                    else if (Mathf.Abs(mousePosition.X - mapRect.Position.X) <= 8 &&
                             mousePosition.Y >= mapRect.Position.Y &&
                             mousePosition.Y <= mapRect.Position.Y + mapRect.Size.Y) //左
                    {
                        _hover = true;
                        _hoverDirection = DoorDirection.W;
                        var mouseOffset = Approach(mousePosition.Y, tileSize.Y);
                        _hoverPoint1 = new Vector2(mapRect.Position.X, mouseOffset - tileSize.Y * 2);
                        _hoverPoint2 = new Vector2(_hoverPoint1.X, _hoverPoint1.Y + tileSize.X * 4);

                        //判断是否能放下新的门
                        if (_hoverPoint1.Y <= mapRect.Position.Y ||
                            _hoverPoint2.Y >= mapRect.Position.Y + mapRect.Size.Y ||
                            CheckDoorCollision())
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
                        if (_hoverPoint1.X <= mapRect.Position.X ||
                            _hoverPoint2.X >= mapRect.Position.X + mapRect.Size.X ||
                            CheckDoorCollision())
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
                        if (_hoverPoint1.Y <= mapRect.Position.Y ||
                            _hoverPoint2.Y >= mapRect.Position.Y + mapRect.Size.Y ||
                            CheckDoorCollision())
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
                    else
                    {
                        ClearState();
                    }
                }

                if (isClick && _canPut) //判断是否可以创建新的点
                {
                    CreateDoorArea(mapRect);
                }
                else if (_mouseDown && !_isDrag) //拖拽节点
                {
                    _isDrag = true;
                    _dragHasCollision = false;
                    if (_activeArea != null)
                    {
                        if (_activePointType == 0)
                        {
                            _startDragValue = _activeArea.Start;
                            _startDragPositionValue = _activeArea.StartPosition;
                        }
                        else
                        {
                            _startDragValue = _activeArea.End;
                            _startDragPositionValue = _activeArea.EndPosition;
                        }
                    }
                }
                else if (!_mouseDown && _isDrag) //松开拖拽的点
                {
                    _isDrag = false;
                    if (_activeArea != null) //提交拖拽结果
                    {
                        if (_dragHasCollision)
                        {
                            if (_activePointType == 0)
                            {
                                _activeArea.Start = _startDragValue;
                                _activeArea.StartPosition = _startDragPositionValue;
                            }
                            else
                            {
                                _activeArea.End = _startDragValue;
                                _activeArea.EndPosition = _startDragPositionValue;
                            }
                        }

                        OnDoorAreaChange();
                    }

                    _dragHasCollision = false;
                }
            }
            else
            {
                ClearState();
            }

            QueueRedraw();
        }
        else
        {
            ClearState();
        }

        //按下 ctrl + s 保存
        if (Input.IsKeyPressed(Key.Ctrl) && Input.IsKeyPressed(Key.S))
        {
            _clickSave = true;
            if (_canSave)
            {
                _canSave = false;
                TriggerSave();
            }
        }
        else
        {
            _clickSave = false;
        }
    }

    public override void _Draw()
    {
        if (!Engine.IsEditorHint())
        {
            return;
        }
        if (TileSet != null)
        {
            //绘制地图轮廓
            var mapRange = CalcTileRange(this);
            mapRange.Position -= new Vector2(2, 2);
            mapRange.Size += new Vector2(4, 4);
            DrawRect(mapRange, _hover ? Colors.Green : new Color(0.03137255F, 0.59607846F, 0.03137255F), false, 2);

            //绘制悬停
            if (_hover && !_isDrag)
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

            //绘制区域
            if (_doorConfigs != null)
            {
                var color2 = new Color(0, 1, 0, 0.8f);
                //绘制已经存在的
                foreach (var doorAreaInfo in _doorConfigs)
                {
                    var flag = _hasActivePoint && _activeArea == doorAreaInfo;
                    var color3 = (flag && _activePointType == 0)
                        ? (_isDrag
                            ? (_dragHasCollision
                                ? new Color(1, 0, 0, 0.8f)
                                : new Color(0.2F, 0.4117647F, 0.8392157F, 0.8f))
                            : new Color(1, 1, 1, 0.8f))
                        : color2;
                    var color4 = (flag && _activePointType == 1)
                        ? (_isDrag
                            ? (_dragHasCollision
                                ? new Color(1, 0, 0, 0.8f)
                                : new Color(0.2F, 0.4117647F, 0.8392157F, 0.8f))
                            : new Color(1, 1, 1, 0.8f))
                        : color2;
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
                            DrawCircle(new Vector2(mapRange.Position.X, mapRange.Position.Y + doorAreaInfo.Start + 2),
                                5,
                                color3);
                            DrawCircle(new Vector2(mapRange.Position.X, mapRange.Position.Y + doorAreaInfo.End + 2), 5,
                                color4);
                            break;
                        case DoorDirection.S:
                            DrawRect(
                                new Rect2(
                                    new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2,
                                        mapRange.Position.Y + mapRange.Size.Y), doorAreaInfo.End - doorAreaInfo.Start,
                                    30),
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
                            DrawCircle(new Vector2(mapRange.Position.X + doorAreaInfo.Start + 2, mapRange.Position.Y),
                                5,
                                color3);
                            DrawCircle(new Vector2(mapRange.Position.X + doorAreaInfo.End + 2, mapRange.Position.Y), 5,
                                color4);
                            break;
                    }
                }
            }
            
            //绘制导航, 现在有点问题, 绘制的内容会被自身的 tile 所挡住
            if (_dungeonTile != null)
            {
                var result = _dungeonTile.GetGenerateNavigationResult();
                if (result != null)
                {
                    if (result.Success)
                    {
                        var polygonData = _dungeonTile.GetPolygonData();
                        Utils.DrawNavigationPolygon(this, polygonData);
                    }
                    else
                    {
                        DrawCircle(result.Exception.Point * GenerateDungeon.TileCellSize, 10, Colors.Red);
                    }
                }
            }
        }
    }

    private void ClearState()
    {
        _hover = false;
        _canPut = false;
        _hasActivePoint = false;
        _activeArea = null;
    }

    private void OnTileChanged()
    {
        _calcTileNavTimer = 1f;
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
        OnDoorAreaChange();
    }

    //移除门
    private void RemoveDoorArea(DoorAreaInfo doorAreaInfo)
    {
        _doorConfigs.Remove(doorAreaInfo);
        OnDoorAreaChange();
    }

    //检查门是否有碰撞
    private bool CheckDoorCollision()
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

    //检查门是否有碰撞
    private bool CheckDoorCollision(DoorDirection direction, DoorAreaInfo info)
    {
        foreach (var doorAreaInfo in _doorConfigs)
        {
            if (doorAreaInfo.Direction == direction && info != doorAreaInfo &&
                CheckValueCollision(doorAreaInfo.Start, doorAreaInfo.End, info.Start, info.End))
            {
                return true;
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
        if (_isDrag)
        {
            return;
        }
        
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

    private float Approach(float value, float period)
    {
        var temp = value % period;
        if (Mathf.Abs(temp) >= period / 2)
        {
            return ((int)(value / period) + (value >= 0 ? 1 : -1)) * period;
        }

        return (int)(value / period) * period;
    }

    //地图大小改变
    private void OnMapRectChange()
    {
        _doorConfigs.Clear();
        _canPut = false;
        _hasActivePoint = false;
        _activeArea = null;
        OnDoorAreaChange();
    }

    //区域数据修改
    private void OnDoorAreaChange()
    {
        _canSave = true;
    }

    //触发保存操作
    private void TriggerSave()
    {
        //计算导航网格
        _dungeonTile.GenerateNavigationPolygon(0);
        var polygonData = _dungeonTile.GetPolygonData();
        var rect = GetUsedRect();
        SaveConfig(_doorConfigs, rect.Position, rect.Size, polygonData.ToList(), Name);
    }
    
    /// <summary>
    /// 计算tile所占区域
    /// </summary>
    /// <returns></returns>
    public static Rect2 CalcTileRange(TileMap tileMap)
    {
        var usedRect = tileMap.GetUsedRect();
        var pos = usedRect.Position * tileMap.TileSet.TileSize;
        var size = usedRect.Size * tileMap.TileSet.TileSize;
        return new Rect2(tileMap.ToLocal(pos), size);
    }
    
    /// <summary>
    /// 保存房间配置
    /// </summary>
    public static void SaveConfig(List<DoorAreaInfo> doorConfigs, Vector2I position, Vector2I size, List<NavigationPolygonData> polygonData, string name)
    {
        //存入本地
        var path = RoomTileDataDir + name + ".json";
        var roomInfo = new DungeonRoomInfo();
        roomInfo.Position = new SerializeVector2(position);
        roomInfo.Size = new SerializeVector2(size);
        roomInfo.DoorAreaInfos = doorConfigs;
        roomInfo.NavigationList = polygonData;
        
        var config = new JsonSerializerOptions();
        config.WriteIndented = true;
        
        var jsonStr = JsonSerializer.Serialize(roomInfo, config);
        File.WriteAllText(path, jsonStr);
        GD.Print("保存房间配置成功！路径为：" + path);
    }
    
    /// <summary>
    /// 读取房间配置
    /// </summary>
    public static List<DoorAreaInfo> ReadConfig(Rect2 mapRect, string name)
    {
        var path = RoomTileDataDir + name + ".json";
        if (File.Exists(path))
        {
            var text = File.ReadAllText(path);
            try
            {
                var roomInfo = DeserializeDungeonRoomInfo(text);
                
                //填充 StartPosition 和 EndPosition 数据
                foreach (var doorAreaInfo in roomInfo.DoorAreaInfos)
                {
                    doorAreaInfo.CalcPosition(mapRect.Position, mapRect.Size);
                }
                return roomInfo.DoorAreaInfos;
            }
            catch (Exception e)
            {
                GD.PrintErr($"加载房间数据'{path}'发生异常: " + e);
                return new List<DoorAreaInfo>();
            }
        }
        else
        {
            return new List<DoorAreaInfo>();
        }
    }

    /// <summary>
    /// 反序列化 DungeonRoomInfo
    /// </summary>
    public static DungeonRoomInfo DeserializeDungeonRoomInfo(string text)
    {
        // 下面这句代码在 Godot4.0_rc2的编辑器模式下, 重载脚本会导致编辑器一直报错!, 所以暂时先用下面的方法
        //var roomInfo = JsonSerializer.Deserialize<DungeonRoomInfo>(text);

        var obj = Json.ParseString(text).AsGodotDictionary();
        var roomInfo = new DungeonRoomInfo();
        var position = obj["Position"].AsGodotDictionary();
        roomInfo.Position = new SerializeVector2(position["X"].AsInt32(), position["Y"].AsInt32());

        var size = obj["Size"].AsGodotDictionary();
        roomInfo.Size = new SerializeVector2(size["X"].AsInt32(), size["Y"].AsInt32());

        var doorAreaInfos = obj["DoorAreaInfos"].AsGodotArray<Variant>();
        roomInfo.DoorAreaInfos = new List<DoorAreaInfo>();
        foreach (var item in doorAreaInfos)
        {
            var temp = item.AsGodotDictionary();
            var doorInfo = new DoorAreaInfo();
            doorInfo.Direction = (DoorDirection)temp["Direction"].AsInt32();
            doorInfo.Start = temp["Start"].AsInt32();
            doorInfo.End = temp["End"].AsInt32();
            roomInfo.DoorAreaInfos.Add(doorInfo);
        }

        var navigationArray = obj["NavigationList"].AsGodotArray<Variant>();
        roomInfo.NavigationList = new List<NavigationPolygonData>();
        for (var i = 0; i < navigationArray.Count; i++)
        {
            var navigation = navigationArray[i].AsGodotDictionary();
            var polygonData = new NavigationPolygonData();

            polygonData.Type = (NavigationPolygonType)navigation["Type"].AsInt32();
            polygonData.Points = new List<SerializeVector2>();
            var pointArray = navigation["Points"].AsGodotArray<Variant>();
            for (var j = 0; j < pointArray.Count; j++)
            {
                var point = pointArray[j].AsGodotDictionary();
                polygonData.Points.Add(new SerializeVector2(point["X"].AsInt32(), point["Y"].AsInt32()));
            }

            roomInfo.NavigationList.Add(polygonData);
        }

        return roomInfo;
    }
#endif
}
