using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Godot;
using Godot.Collections;

namespace UI.MapEditor;

public partial class EditorTileMap : TileMap
{
    /// <summary>
    /// 自动图块地板层
    /// </summary>
    public const int AutoFloorLayer = 0;
    /// <summary>
    /// 自定义图块地板层
    /// </summary>
    public const int CustomFloorLayer = 1;
    /// <summary>
    /// 自动图块中间层
    /// </summary>
    public const int AutoMiddleLayer = 2;
    /// <summary>
    /// 自定义图块中间层
    /// </summary>
    public const int CustomMiddleLayer = 3;
    /// <summary>
    /// 自动图块顶层
    /// </summary>
    public const int AutoTopLayer = 4;
    /// <summary>
    /// 自定义图块顶层
    /// </summary>
    public const int CustomTopLayer = 5;
    
    /// <summary>
    /// 所属地图编辑器UI
    /// </summary>
    public MapEditorPanel MapEditorPanel { get; set; }
    
    //鼠标坐标
    private Vector2 _mousePosition;
    //鼠标所在的cell坐标
    private Vector2I _mouseCellPosition;
    //上一帧鼠标所在的cell坐标
    private Vector2I _prevMouseCellPosition = new Vector2I(-99999, -99999);
    //单次绘制是否改变过tile数据
    private bool _changeFlag = false;
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
    //负责存储自动图块数据
    private Grid<bool> _autoCellLayerGrid = new Grid<bool>();
    //用于生成导航网格
    private DungeonTileMap _dungeonTileMap;
    //停止绘制多久后开始执行生成操作
    private float _generateInterval = 3f;
    //生成自动图块和导航网格的计时器
    private float _generateTimer = -1;
    //检测地形结果
    private bool _checkTerrainFlag = true;
    //错误地形位置
    private Vector2I _checkTerrainErrorPosition = Vector2I.Zero;
    //是否执行生成地形成功
    private bool _isGenerateTerrain = false;

    private bool _initLayer = false;
    
    //--------- 配置数据 -------------
    private int _sourceId = 0;
    private int _terrainSet = 0;
    private int _terrain = 0;
    private AutoTileConfig _autoTileConfig = new AutoTileConfig();

    private string _dir;
    private string _groupName = "testGroup1";
    private string _fileName = "Room2";
    private Vector2I _roomPosition;
    private Vector2I _roomSize;
    private List<DoorAreaInfo> _doorConfigs = new List<DoorAreaInfo>();
    private DungeonRoomType _roomType = DungeonRoomType.Battle;
    private int _weight = 100;
    //-------------------------------

    public override void _Ready()
    {
        InitLayer();
    }

    public override void _Process(double delta)
    {
        var newDelta = (float)delta;
        _drawFullRect = false;
        var position = GetLocalMousePosition();
        _mouseCellPosition = LocalToMap(position);
        _mousePosition = new Vector2(
            _mouseCellPosition.X * GameConfig.TileCellSize,
            _mouseCellPosition.Y * GameConfig.TileCellSize
        );

        if (!MapEditorPanel.ToolsPanel.S_HBoxContainer.Instance.IsPositionOver(GetGlobalMousePosition())) //不在Ui节点上
        {
            //左键绘制
            if (_isLeftPressed)
            {
                if (Input.IsKeyPressed(Key.Shift)) //按住shift绘制矩形
                {
                    _drawFullRect = true;
                }
                else if (_prevMouseCellPosition != _mouseCellPosition || !_changeFlag) //鼠标位置变过
                {
                    _changeFlag = true;
                    _prevMouseCellPosition = _mouseCellPosition;
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
                else if (_prevMouseCellPosition != _mouseCellPosition || !_changeFlag) //鼠标位置变过
                {
                    _changeFlag = true;
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

        //绘制停止指定时间后, 生成导航网格
        if (_generateTimer > 0)
        {
            _generateTimer -= newDelta;
            if (_generateTimer <= 0)
            {
                //计算区域
                CalcTileRect();
                GD.Print("开始检测是否可以生成地形...");
                if (CheckTerrain())
                {
                    GD.Print("开始绘制导航网格...");
                    if (GenerateNavigation())
                    {
                        GD.Print("开始绘制自动贴图...");
                        GenerateTerrain();
                        _isGenerateTerrain = true;
                    }
                }
                else
                {
                    SetErrorCell(_checkTerrainErrorPosition);
                }
            }
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
        
        //绘制房间区域
        if (_roomSize.X != 0 && _roomSize.Y != 0)
        {
            var size = TileSet.TileSize;
            canvasItem.DrawRect(new Rect2((_roomPosition - Vector2I.One) * size, (_roomSize + new Vector2I(2, 2)) * size),
                Colors.Aqua, false, 5f / Scale.X);
        }
        
        if (_checkTerrainFlag) //已经通过地形检测
        {
            //绘制导航网格
            var result = _dungeonTileMap.GetGenerateNavigationResult();
            if (result != null && result.Success)
            {
                var polygonData = _dungeonTileMap.GetPolygonData();
                Utils.DrawNavigationPolygon(canvasItem, polygonData, 3f / Scale.X);
            }
        }

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
            canvasItem.DrawRect(new Rect2(pos, _mousePosition - pos + temp), Colors.White, false, 2f / Scale.X);
        }
        else //绘制单格
        {
            canvasItem.DrawRect(new Rect2(_mousePosition, TileSet.TileSize), Colors.White, false, 2f / Scale.X);
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
                    _changeFlag = false;
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
                    _changeFlag = false;
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
        else if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed && eventKey.Keycode == Key.M)
            {
                GD.Print("测试保存地牢房间数据...");
                TriggerSave();
            }
        }
    }

    //将指定层数据存入list中
    private void PushLayerDataToList(int layer, int sourceId, List<int> list)
    {
        var layerArray = GetUsedCellsById(layer, sourceId);
        foreach (var pos in layerArray)
        {
            var atlasCoords = GetCellAtlasCoords(layer, pos);
            list.Add(pos.X);
            list.Add(pos.Y);
            list.Add(_sourceId);
            list.Add(atlasCoords.X);
            list.Add(atlasCoords.Y);
        }
    }

    private void SetLayerDataFromList(int layer, List<int> list)
    {
        for (var i = 0; i < list.Count; i += 5)
        {
            var pos = new Vector2I(list[i], list[i + 1]);
            var sourceId = list[i + 2];
            var atlasCoords = new Vector2I(list[i + 3], list[i + 4]);
            SetCell(layer, pos, sourceId, atlasCoords);
            if (layer == AutoFloorLayer)
            {
                _autoCellLayerGrid.Set(pos, true);
            }
        }
    }

    //保存地牢
    private void TriggerSave()
    {
        SaveRoomInfoConfig();
        SaveTileInfoConfig();
    }

    /// <summary>
    /// 加载地牢, 返回是否加载成功
    /// </summary>
    /// <param name="dir">文件夹路径</param>
    /// <param name="groupName">房间组名</param>
    /// <param name="roomType">房间类型</param>
    /// <param name="roomName">房间名称</param>
    public bool Load(string dir, string groupName, DungeonRoomType roomType, string roomName)
    {
        _dir = dir;
        _groupName = groupName;
        _roomType = roomType;
        _fileName = roomName;

        var path = GetConfigPath(dir, groupName, roomType, roomName) + "/";
        var tileInfoConfigPath = path + GetTileInfoConfigName(_fileName);
        var roomInfoConfigPath = path + GetRoomInfoConfigName(_fileName);

        if (!File.Exists(tileInfoConfigPath))
        {
            GD.PrintErr("地牢编辑器加载地牢时未找到文件: " + tileInfoConfigPath);
            return false;
        }
        if (!File.Exists(roomInfoConfigPath))
        {
            GD.PrintErr("地牢编辑器加载地牢时未找到文件: " + roomInfoConfigPath);
            return false;
        }
        
        var text = ResourceManager.LoadText(tileInfoConfigPath);
        var tileInfo = JsonSerializer.Deserialize<DungeonTileInfo>(text);
        
        var text2 = ResourceManager.LoadText(roomInfoConfigPath);
        var roomInfo = JsonSerializer.Deserialize<DungeonRoomInfo>(text2);

        _roomPosition = roomInfo.Position.AsVector2I();
        _roomSize = roomInfo.Size.AsVector2I();
        _doorConfigs.Clear();
        _doorConfigs.AddRange(roomInfo.DoorAreaInfos);
        _weight = roomInfo.Weight;
        
        //初始化层级数据
        InitLayer();
        
        //地块数据
        SetLayerDataFromList(AutoFloorLayer, tileInfo.Floor);
        SetLayerDataFromList(AutoMiddleLayer, tileInfo.Middle);
        SetLayerDataFromList(AutoTopLayer, tileInfo.Top);
        
        //导航网格数据
        _dungeonTileMap.SetPolygonData(tileInfo.NavigationList);

        //聚焦
        //MapEditorPanel.CallDelay(0.1f, OnClickCenterTool);
        CallDeferred(nameof(OnClickCenterTool));
        return true;
    }

    private void InitLayer()
    {
        if (_initLayer)
        {
            return;
        }

        _initLayer = true;
        //初始化层级数据
        AddLayer(CustomFloorLayer);
        SetLayerZIndex(CustomFloorLayer, CustomFloorLayer);
        AddLayer(AutoMiddleLayer);
        SetLayerZIndex(AutoMiddleLayer, AutoMiddleLayer);
        AddLayer(CustomMiddleLayer);
        SetLayerZIndex(CustomMiddleLayer, CustomMiddleLayer);
        AddLayer(AutoTopLayer);
        SetLayerZIndex(AutoTopLayer, AutoTopLayer);
        AddLayer(CustomTopLayer);
        SetLayerZIndex(CustomTopLayer, CustomTopLayer);

        _dungeonTileMap = new DungeonTileMap(this);
        _dungeonTileMap.SetFloorAtlasCoords(new List<Vector2I>(new []{ _autoTileConfig.Floor.AutoTileCoord }));
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
        SetCell(GetFloorLayer(), position, _sourceId, _autoTileConfig.Floor.AutoTileCoord);
        if (!_autoCellLayerGrid.Contains(position.X, position.Y))
        {
            ResetGenerateTimer();
            _autoCellLayerGrid.Set(position.X, position.Y, true);
        }
    }
    
    //绘制区域自动贴图
    private void SetRectAutoCell(Vector2I start, Vector2I end)
    {
        ResetGenerateTimer();
        
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

        var width = end.X - start.X + 1;
        var height = end.Y - start.Y + 1;
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                SetCell(GetFloorLayer(), new Vector2I(start.X + i, start.Y + j), _sourceId, _autoTileConfig.Floor.AutoTileCoord);
            }
        }

        _autoCellLayerGrid.SetRect(start, new Vector2I(width, height), true);
    }

    //擦除单个自动图块
    private void EraseSingleAutoCell(Vector2I position)
    {
        EraseCell(GetFloorLayer(), position);
        if (_autoCellLayerGrid.Remove(position.X, position.Y))
        {
            ResetGenerateTimer();
        }
    }
    
    //擦除一个区域内的自动贴图
    private void EraseRectAutoCell(Vector2I start, Vector2I end)
    {
        ResetGenerateTimer();
        
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

        var width = end.X - start.X + 1;
        var height = end.Y - start.Y + 1;
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                EraseCell(GetFloorLayer(), new Vector2I(start.X + i, start.Y + j));
            }
        }
        _autoCellLayerGrid.RemoveRect(start, new Vector2I(width, height));
    }

    //重置计时器
    private void ResetGenerateTimer()
    {
        _generateTimer = _generateInterval;
        _isGenerateTerrain = false;
        _dungeonTileMap.ClearPolygonData();
        ClearLayer(AutoTopLayer);
        ClearLayer(AutoMiddleLayer);
    }

    //从新计算房间区域
    private void CalcTileRect()
    {
        var rect = GetUsedRect();
        _roomPosition = rect.Position;
        _roomSize = rect.Size;
    }
    
    //检测是否有不合规的图块, 返回true表示图块正常
    private bool CheckTerrain()
    {
        var x = _roomPosition.X;
        var y = _roomPosition.Y;
        var w = _roomSize.X;
        var h = _roomSize.Y;

        for (var i = 0; i < w; i++)
        {
            for (var j = 0; j < h; j++)
            {
                var pos = new Vector2I(x + i, y + j);
                if (GetCellSourceId(AutoFloorLayer, pos) == -1)
                {
                    //先检测对角是否有地板
                    var left = _autoCellLayerGrid.Get(pos.X - 1, pos.Y);
                    var right = _autoCellLayerGrid.Get(pos.X + 1, pos.Y);
                    var top = _autoCellLayerGrid.Get(pos.X, pos.Y + 1);
                    var down = _autoCellLayerGrid.Get(pos.X, pos.Y - 1);
                    
                    if ((left && right) || (top && down))
                    {
                        _checkTerrainFlag = false;
                        _checkTerrainErrorPosition = pos;
                        return false;
                    }
                }
            }
        }

        _checkTerrainFlag = true;
        return true;
    }
    
    //生成自动图块 (地形)
    private void GenerateTerrain()
    {
        ClearLayer(AutoFloorLayer);
        
        var list = new List<Vector2I>();
        _autoCellLayerGrid.ForEach((x, y, data) =>
        {
            if (data)
            {
                list.Add(new Vector2I(x, y));
            }
        });
        var arr = new Array<Vector2I>(list);
        SetCellsTerrainConnect(AutoFloorLayer, arr, _terrainSet, _terrain, false);
        MoveTerrainCell();
    }

    //将自动生成的图块从 AutoFloorLayer 移动到指定图层中
    private void MoveTerrainCell()
    {
        ClearLayer(AutoTopLayer);
        ClearLayer(AutoMiddleLayer);
        
        var x = _roomPosition.X;
        var y = _roomPosition.Y;
        var w = _roomSize.X;
        var h = _roomSize.Y;

        for (var i = 0; i < w; i++)
        {
            for (var j = 0; j < h; j++)
            {
                var pos = new Vector2I(x + i, y + j);
                if (!_autoCellLayerGrid.Contains(pos) && GetCellSourceId(AutoFloorLayer, pos) != -1)
                {
                    var atlasCoords = GetCellAtlasCoords(AutoFloorLayer, pos);
                    var layer = _autoTileConfig.GetLayer(atlasCoords);
                    if (layer == GameConfig.MiddleMapLayer)
                    {
                        layer = AutoMiddleLayer;
                    }
                    else if (layer == GameConfig.TopMapLayer)
                    {
                        layer = AutoTopLayer;
                    }
                    else
                    {
                        GD.PrintErr($"异常图块: {pos}, 这个图块的图集坐标'{atlasCoords}'不属于'MiddleMapLayer'和'TopMapLayer'!");
                        continue;
                    }
                    EraseCell(AutoFloorLayer, pos);
                    SetCell(layer, pos, _sourceId, atlasCoords);
                }
            }
        }
    }

    //生成导航网格
    private bool GenerateNavigation()
    {
        _dungeonTileMap.GenerateNavigationPolygon(AutoFloorLayer);
        var result = _dungeonTileMap.GetGenerateNavigationResult();
        if (result.Success)
        {
            CloseErrorCell();
        }
        else
        {
            SetErrorCell(result.Exception.Point);
        }

        return result.Success;
    }

    //设置显示的错误cell, 会标记上红色的闪烁动画
    private void SetErrorCell(Vector2I pos)
    {
        MapEditorPanel.S_ErrorCell.Instance.Position = pos * CellQuadrantSize;
        MapEditorPanel.S_ErrorCellAnimationPlayer.Instance.Play(AnimatorNames.Show);
    }

    //关闭显示的错误cell
    private void CloseErrorCell()
    {
        MapEditorPanel.S_ErrorCellAnimationPlayer.Instance.Stop();
    }

    private int GetFloorLayer()
    {
        return AutoFloorLayer;
    }

    private int GetMiddleLayer()
    {
        return AutoMiddleLayer;
    }

    private int GetTopLayer()
    {
        return AutoTopLayer;
    }

    /// <summary>
    /// 选中拖拽功能
    /// </summary>
    public void OnSelectHandTool()
    {
        
    }
    
    /// <summary>
    /// 选中画笔攻击
    /// </summary>
    public void OnSelectPenTool()
    {
        
    }

    /// <summary>
    /// 选中绘制区域功能
    /// </summary>
    public void OnSelectRectTool()
    {
        
    }

    /// <summary>
    /// 聚焦
    /// </summary>
    public void OnClickCenterTool()
    {
        var pos = MapEditorPanel.S_SubViewport.Instance.Size / 2;
        if (_roomSize.X == 0 && _roomSize.Y == 0) //聚焦原点
        {
            Position = pos;
        }
        else //聚焦地图中心点
        {
            Position = pos - (_roomPosition + _roomSize / 2) * TileSet.TileSize * Scale;
        }
    }

    //保存房间配置
    private void SaveRoomInfoConfig()
    {
        //存入本地
        var path = GetConfigPath(_dir, _groupName, _roomType, _fileName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        var roomInfo = new DungeonRoomInfo();
        roomInfo.Position = new SerializeVector2(_roomPosition);
        roomInfo.Size = new SerializeVector2(_roomSize);
        roomInfo.DoorAreaInfos = _doorConfigs;
        roomInfo.RoomType = _roomType;
        roomInfo.GroupName = _groupName;
        roomInfo.FileName = _fileName;
        roomInfo.Weight = _weight;

        path += "/" + GetRoomInfoConfigName(_fileName);
        var jsonStr = JsonSerializer.Serialize(roomInfo);
        File.WriteAllText(path, jsonStr);
    }

    //保存地块数据
    public void SaveTileInfoConfig()
    {
        //存入本地
        var path = GetConfigPath(_dir, _groupName, _roomType, _fileName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        var tileInfo = new DungeonTileInfo();
        tileInfo.NavigationList = _dungeonTileMap.GetPolygonData().ToList();
        tileInfo.Floor = new List<int>();
        tileInfo.Middle = new List<int>();
        tileInfo.Top = new List<int>();

        PushLayerDataToList(AutoFloorLayer, _sourceId, tileInfo.Floor);
        PushLayerDataToList(AutoMiddleLayer, _sourceId, tileInfo.Middle);
        PushLayerDataToList(AutoTopLayer, _sourceId, tileInfo.Top);
        
        path += "/" + GetTileInfoConfigName(_fileName);
        var jsonStr = JsonSerializer.Serialize(tileInfo);
        File.WriteAllText(path, jsonStr);
    }

    private string GetConfigPath(string dir, string groupName, DungeonRoomType roomType, string fileName)
    {
        return dir + groupName + "/" + DungeonManager.DungeonRoomTypeToString(roomType) + "/" + fileName;
    }

    private string GetTileInfoConfigName(string roomName)
    {
        return roomName + "_tileInfo.json";
    }
    
    private string GetRoomInfoConfigName(string roomName)
    {
        return roomName + "_roomInfo.json";
    }
}