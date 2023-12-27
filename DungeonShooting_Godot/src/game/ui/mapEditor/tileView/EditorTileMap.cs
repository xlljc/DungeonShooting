using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using UI.MapEditorTools;

namespace UI.MapEditor;

public partial class EditorTileMap : TileMap, IUiNodeScript
{
    
    public enum MouseButtonType
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None,
        /// <summary>
        /// 拖拽模式
        /// </summary>
        Drag,
        /// <summary>
        /// 笔
        /// </summary>
        Pen,
        /// <summary>
        /// 绘制区域模式
        /// </summary>
        Area,
        /// <summary>
        /// 编辑工具模式
        /// </summary>
        Edit,
    }
    
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
    /// 标记数据层
    /// </summary>
    public const int MarkLayer = 10;
    
    /// <summary>
    /// 所属地图编辑器UI
    /// </summary>
    public MapEditorPanel MapEditorPanel { get; private set; }
    
    /// <summary>
    /// 编辑器工具UI
    /// </summary>
    public MapEditorToolsPanel MapEditorToolsPanel { get; set; }
    
    /// <summary>
    /// 左键功能
    /// </summary>
    public MouseButtonType MouseType { get; private set; } = MouseButtonType.Pen;
    
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
    private InfiniteGrid<bool> _autoCellLayerGrid = new InfiniteGrid<bool>();
    //停止绘制多久后开始执行生成操作
    private float _generateInterval = 3f;
    //生成自动图块和导航网格的计时器
    private float _generateTimer = -1;
    //检测地形结果
    private bool _checkTerrainFlag = true;
    //错误地形位置
    private Vector2I _checkTerrainErrorPosition = Vector2I.Zero;
    //是否执行生成地形成功
    private bool _isGenerateTerrain = true;
    //导航网格数据
    private Vector2[][] _polygonData;
    private bool _initLayer = false;

    //--------- 配置数据 -------------
    private int _sourceId = 0;
    private int _terrainSet = 0;
    private int _terrain = 0;
    private AutoTileConfig _autoTileConfig;

    /// <summary>
    /// 正在编辑的房间数据
    /// </summary>
    public DungeonRoomSplit CurrRoomSplit;
    
    /// <summary>
    /// 数据是否脏了, 也就是是否有修改
    /// </summary>
    public bool IsDirty { get; private set; }

    /// <summary>
    /// 地图是否有绘制错误
    /// </summary>
    public bool HasTerrainError => !_isGenerateTerrain;

    //变动过的数据
    
    /// <summary>
    /// 地图位置, 单位: 格
    /// </summary>
    public Vector2I CurrRoomPosition { get; private set; }
    /// <summary>
    /// 当前地图大小, 单位: 格
    /// </summary>
    public Vector2I CurrRoomSize { get; private set; }
    /// <summary>
    /// 当前编辑的门数据
    /// </summary>
    public List<DoorAreaInfo> CurrDoorConfigs { get; } = new List<DoorAreaInfo>();
    //-------------------------------
    private MapEditor.TileMap _editorTileMap;
    private EventFactory _eventFactory;
    
    /// <summary>
    /// 初始化图块集。
    /// </summary>
    /// <param name="tileSet">要初始化的图块集</param>
    public void InitTileSet(TileSet tileSet)
    {
        TileSet = tileSet;

        // 创建AutoTileConfig对象
        // 使用第一个图块集源作为参数
        _autoTileConfig = new AutoTileConfig(0, tileSet.GetSource(0) as TileSetAtlasSource);
    }
    
    public void SetUiNode(IUiNode uiNode)
    {
        _editorTileMap = (MapEditor.TileMap)uiNode;
        MapEditorPanel = _editorTileMap.UiPanel;
        MapEditorToolsPanel = _editorTileMap.UiPanel.S_MapEditorTools.Instance;

        _editorTileMap.L_Brush.Instance.Draw += DrawGuides;
        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnSelectDragTool, OnSelectHandTool);
        _eventFactory.AddEventListener(EventEnum.OnSelectPenTool, OnSelectPenTool);
        _eventFactory.AddEventListener(EventEnum.OnSelectRectTool, OnSelectRectTool);
        _eventFactory.AddEventListener(EventEnum.OnSelectEditTool, OnSelectEditTool);
        _eventFactory.AddEventListener(EventEnum.OnClickCenterTool, OnClickCenterTool);
        _eventFactory.AddEventListener(EventEnum.OnEditorDirty, OnEditorDirty);

        RenderingServer.FramePostDraw += OnFramePostDraw;
        var navigationRegion = _editorTileMap.L_NavigationRegion.Instance;
        navigationRegion.Visible = false;
        navigationRegion.NavigationPolygon.AgentRadius = GameConfig.NavigationAgentRadius;
        navigationRegion.BakeFinished += OnBakeFinished;
    }

    public void OnDestroy()
    {
        _eventFactory.RemoveAllEventListener();
        RenderingServer.FramePostDraw -= OnFramePostDraw;
    }
    
    public override void _Ready()
    {
        InitLayer();
    }

    public override void _Process(double delta)
    {
        //触发绘制辅助线
        _editorTileMap.L_Brush.Instance.QueueRedraw();
        
        var newDelta = (float)delta;
        _drawFullRect = false;
        var position = GetLocalMousePosition();
        _mouseCellPosition = LocalToMap(position);
        _mousePosition = new Vector2(
            _mouseCellPosition.X * GameConfig.TileCellSize,
            _mouseCellPosition.Y * GameConfig.TileCellSize
        );
        
        if (!MapEditorToolsPanel.S_HBoxContainer.Instance.IsPositionOver(GetGlobalMousePosition())) //不在Ui节点上
        {
            //左键绘制
            if (_isLeftPressed)
            {
                if (MouseType == MouseButtonType.Pen) //绘制单格
                {
                    if (_prevMouseCellPosition != _mouseCellPosition || !_changeFlag) //鼠标位置变过
                    {
                        _changeFlag = true;
                        _prevMouseCellPosition = _mouseCellPosition;
                        //绘制自动图块
                        SetSingleAutoCell(_mouseCellPosition);
                    }
                }
                else if (MouseType == MouseButtonType.Area) //绘制区域
                {
                    _drawFullRect = true;
                }
                else if (MouseType == MouseButtonType.Drag) //拖拽
                {
                    SetMapPosition(GetGlobalMousePosition() + _moveOffset);
                }
            }
            else if (_isRightPressed) //右键擦除
            {
                if (MouseType == MouseButtonType.Pen) //绘制单格
                {
                    if (_prevMouseCellPosition != _mouseCellPosition || !_changeFlag) //鼠标位置变过
                    {
                        _changeFlag = true;
                        _prevMouseCellPosition = _mouseCellPosition;
                        EraseSingleAutoCell(_mouseCellPosition);
                    }
                }
                else if (MouseType == MouseButtonType.Area) //绘制区域
                {
                    _drawFullRect = true;
                }
                else if (MouseType == MouseButtonType.Drag) //拖拽
                {
                    SetMapPosition(GetGlobalMousePosition() + _moveOffset);
                }
            }
            else if (_isMiddlePressed) //中键移动
            {
                SetMapPosition(GetGlobalMousePosition() + _moveOffset);
            }
        }

        //绘制停止指定时间后, 生成导航网格
        if (_generateTimer > 0)
        {
            _generateTimer -= newDelta;
            if (_generateTimer <= 0)
            {
                //检测地形
                RunCheckHandler();
            }
        }
    }

    /// <summary>
    /// 绘制辅助线
    /// </summary>
    public void DrawGuides()
    {
        if (_hasPreviewImage)
        {
            return;
        }
        CanvasItem canvasItem = _editorTileMap.L_Brush.Instance;
        //轴线
        canvasItem.DrawLine(new Vector2(0, 2000), new Vector2(0, -2000), Colors.Green);
        canvasItem.DrawLine(new Vector2(2000, 0), new Vector2( -2000, 0), Colors.Red);
        
        //绘制房间区域
        if (CurrRoomSize.X != 0 && CurrRoomSize.Y != 0)
        {
            canvasItem.DrawRect(
                new Rect2(
                    (CurrRoomPosition + new Vector2I(1, 2)) * GameConfig.TileCellSize,
                    (CurrRoomSize - new Vector2I(2, 3)) * GameConfig.TileCellSize
                ),
                Colors.Aqua, false, 5f / Scale.X
            );
        }

        //绘制导航网格
        if (_checkTerrainFlag && _isGenerateTerrain && _polygonData != null)
        {
            foreach (var vector2s in _polygonData)
            {
                canvasItem.DrawPolygon(vector2s, new Color(0,1,1, 0.3f).MakeArray(vector2s.Length));
            }
        }

        if (MouseType == MouseButtonType.Pen || MouseType == MouseButtonType.Area)
        {
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
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left) //左键
            {
                if (mouseButton.Pressed) //按下
                {
                    _moveOffset = Position - GetGlobalMousePosition();
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
                    _moveOffset = Position - GetGlobalMousePosition();
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
                    _moveOffset = Position - GetGlobalMousePosition();
                }
            }
        }
    }

    /// <summary>
    /// 尝试运行检查, 如果已经运行过了, 则没有效果
    /// </summary>
    public void TryRunCheckHandler()
    {
        if (_generateTimer > 0)
        {
            _generateTimer = -1;
            RunCheckHandler();
        }
    }
    
    //执行检测地形操作
    private void RunCheckHandler()
    {
        _isGenerateTerrain = false;
        //计算区域
        CalcTileRect(false);
        Debug.Log("开始检测是否可以生成地形...");
        if (CheckTerrain())
        {
            Debug.Log("开始绘制自动贴图...");
            GenerateTerrain();
            _isGenerateTerrain = true;
        }
        else
        {
            SetErrorCell(_checkTerrainErrorPosition);
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

    /// <summary>
    /// 触发保存地图数据
    /// </summary>
    public void TriggerSave(RoomErrorType errorType, Action finish)
    {
        Debug.Log("保存地牢房间数据...");
        //执行创建预览图流程
        RunSavePreviewImage(() =>
        {
            //执行保存数据流程
            CurrRoomSplit.ErrorType = errorType;
            SaveRoomInfoConfig();
            SaveTileInfoConfig();
            SavePreinstallConfig();
            IsDirty = false;
            MapEditorPanel.SetTitleDirty(false);
            //派发保存事件
            EventManager.EmitEvent(EventEnum.OnEditorSave);
            if (finish != null)
            {
                finish();
            }
        });
    }

    /// <summary>
    /// 加载地牢, 返回是否加载成功
    /// </summary>
    public bool Load(DungeonRoomSplit roomSplit)
    {
        //重新加载数据
        roomSplit.ReloadRoomInfo();
        roomSplit.ReloadTileInfo();
        roomSplit.ReloadPreinstall();
        
        CurrRoomSplit = roomSplit;
        var roomInfo = roomSplit.RoomInfo;
        var tileInfo = roomSplit.TileInfo;

        CurrRoomPosition = roomInfo.Position.AsVector2I();
        SetMapSize(roomInfo.Size.AsVector2I(), true);
        CurrDoorConfigs.Clear();
        foreach (var doorAreaInfo in roomInfo.DoorAreaInfos)
        {
            CurrDoorConfigs.Add(doorAreaInfo.Clone());
        }

        //初始化层级数据
        InitLayer();
        
        //地块数据
        SetLayerDataFromList(AutoFloorLayer, tileInfo.Floor);
        SetLayerDataFromList(AutoMiddleLayer, tileInfo.Middle);
        SetLayerDataFromList(AutoTopLayer, tileInfo.Top);

        //如果有图块错误, 则找出错误的点位
        if (roomSplit.ErrorType == RoomErrorType.TileError)
        {
            RunCheckHandler();
        }
        else
        {
            //导航网格
            if (tileInfo.NavigationPolygon != null && tileInfo.NavigationVertices != null)
            {
                var polygon = _editorTileMap.L_NavigationRegion.Instance.NavigationPolygon;
                polygon.Vertices = tileInfo.NavigationVertices.Select(v => v.AsVector2()).ToArray();
                foreach (var p in tileInfo.NavigationPolygon)
                {
                    polygon.AddPolygon(p);
                }

                OnBakeFinished();
            }
        }
        //聚焦
        //MapEditorPanel.CallDelay(0.1f, OnClickCenterTool);
        //CallDeferred(nameof(OnClickCenterTool), null);
        
        //加载门编辑区域
        foreach (var doorAreaInfo in CurrDoorConfigs)
        {
            MapEditorToolsPanel.CreateDoorTool(doorAreaInfo);
        }
        
        //聚焦
        OnClickCenterTool(null);
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
    }

    //缩小
    private void Shrink()
    {
        var pos = GetLocalMousePosition();
        var scale = Scale / 1.1f;
        if (scale.LengthSquared() >= 0.5f)
        {
            Scale = scale;
            SetMapPosition(Position + pos * 0.1f * scale);
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
            SetMapPosition(Position - pos * 0.1f * prevScale);
        }
    }

    //绘制单个自动贴图
    private void SetSingleAutoCell(Vector2I position)
    {
        var tileCellData = _autoTileConfig.Floor;
        SetCell(GetFloorLayer(), position, tileCellData.SourceId, tileCellData.AutoTileCoords);
        //SetCell(GetFloorLayer(), position, _sourceId, _autoTileConfig.Floor.AutoTileCoords);
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
                var tileCellData = _autoTileConfig.Floor;
                SetCell(GetFloorLayer(), new Vector2I(start.X + i, start.Y + j), tileCellData.SourceId, tileCellData.AutoTileCoords);
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
        ClearLayer(AutoTopLayer);
        ClearLayer(AutoMiddleLayer);
        CloseErrorCell();
        //标记有修改数据
        EventManager.EmitEvent(EventEnum.OnEditorDirty);
    }

    //重新计算房间区域
    private void CalcTileRect(bool refreshDoorTrans)
    {
        var rect = GetUsedRect();
        CurrRoomPosition = rect.Position;
        SetMapSize(rect.Size, refreshDoorTrans);
    }
    
    //检测是否有不合规的图块, 返回true表示图块正常
    private bool CheckTerrain()
    {
        _checkTerrainFlag = true;   
        _autoCellLayerGrid.ForEach((x, y, flag) =>
        {
            if (flag)
            {
                if (!_autoCellLayerGrid.Contains(x, y + 1) && _autoCellLayerGrid.Contains(x, y + 2))
                {
                    _checkTerrainFlag = false;
                    _checkTerrainErrorPosition = new Vector2I(x, y + 1);
                    return false;
                }
            }

            return true;
        });
        return _checkTerrainFlag;
    }
    
    //生成自动图块 (地形)
    private void GenerateTerrain()
    {
        //ClearLayer(AutoFloorLayer);
        var list = new List<Vector2I>();
        var xStart = int.MaxValue;
        var yStart = int.MaxValue;
        var xEnd = int.MinValue;
        var yEnd = int.MinValue;
        
        _autoCellLayerGrid.ForEach((x, y, flag) =>
        {
            if (flag)
            {
                //计算范围
                if (x < xStart)
                    xStart = x;
                else if (x > xEnd)
                    xEnd = x;

                if (y < yStart)
                    yStart = y;
                else if (y > yEnd)
                    yEnd = y;
                
                //填充墙壁
                if (!_autoCellLayerGrid.Contains(x, y - 1))
                {
                    var left = _autoCellLayerGrid.Contains(x - 1, y - 1);
                    var right = _autoCellLayerGrid.Contains(x + 1, y - 1);
                    TileCellData tileCellData;
                    if (left && right)
                    {
                        tileCellData = _autoTileConfig.Wall_Vertical_Single;
                    }
                    else if (left)
                    {
                        tileCellData = _autoTileConfig.Wall_Vertical_Left;
                    }
                    else if (right)
                    {
                        tileCellData = _autoTileConfig.Wall_Vertical_Right;
                    }
                    else
                    {
                        tileCellData = _autoTileConfig.Wall_Vertical_Center;
                    }
                    SetCell(GetFloorLayer(), new Vector2I(x, y - 1), tileCellData.SourceId, tileCellData.AutoTileCoords);
                }
            }

            return true;
        });
        
        //绘制临时边界
        var temp1 = new List<Vector2I>();
        for (var x = xStart - 3; x <= xEnd + 3; x++)
        {
            var p1 = new Vector2I(x, yStart - 4);
            var p2 = new Vector2I(x, yEnd + 3);
            temp1.Add(p1);
            temp1.Add(p2);
            //上横
            SetCell(GetFloorLayer(), p1, _autoTileConfig.TopMask.SourceId, _autoTileConfig.TopMask.AutoTileCoords);
            //下横
            SetCell(GetFloorLayer(), p2, _autoTileConfig.TopMask.SourceId, _autoTileConfig.TopMask.AutoTileCoords);
        }
        for (var y = yStart - 4; y <= yEnd + 3; y++)
        {
            var p1 = new Vector2I(xStart - 3, y);
            var p2 = new Vector2I(xEnd + 3, y);
            temp1.Add(p1);
            temp1.Add(p2);
            //左竖
            SetCell(GetFloorLayer(), p1, _autoTileConfig.TopMask.SourceId, _autoTileConfig.TopMask.AutoTileCoords);
            //右竖
            SetCell(GetFloorLayer(), p2, _autoTileConfig.TopMask.SourceId, _autoTileConfig.TopMask.AutoTileCoords);
        }
        
        //计算需要绘制的图块
        var temp2 = new List<Vector2I>();
        for (var x = xStart - 2; x <= xEnd + 2; x++)
        {
            for (var y = yStart - 3; y <= yEnd + 2; y++)
            {
                if (!_autoCellLayerGrid.Contains(x, y) && !_autoCellLayerGrid.Contains(x, y + 1))
                {
                    list.Add(new Vector2I(x, y));
                    if (!IsMaskCollisionGround(_autoCellLayerGrid, x, y))
                    {
                        temp2.Add(new Vector2I(x, y));
                    }
                }
            }
        }
        var arr = new Array<Vector2I>(list);
        //绘制自动图块
        SetCellsTerrainConnect(AutoFloorLayer, arr, _terrainSet, _terrain, false);
        
        //擦除临时边界
        for (var i = 0; i < temp1.Count; i++)
        {
            EraseCell(GetFloorLayer(), temp1[i]);
        }

        //计算区域
        CalcTileRect(true);
        
        //开始绘制导航网格
        GenerateNavigation();

        //擦除临时边界2
        for (var i = 0; i < temp2.Count; i++)
        {
            EraseCell(GetFloorLayer(), temp2[i]);
        }
        
        //将墙壁移动到指定层
        MoveTerrainCell();
    }

    private bool IsMaskCollisionGround(InfiniteGrid<bool> autoCellLayerGrid, int x, int y)
    {
        for (var i = -2; i <= 2; i++)
        {
            for (var j = -2; j <= 3; j++)
            {
                if (autoCellLayerGrid.Contains(x + i, y + j))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    //将自动生成的图块从 AutoFloorLayer 移动到指定图层中
    private void MoveTerrainCell()
    {
        ClearLayer(AutoTopLayer);
        ClearLayer(AutoMiddleLayer);
        
        var x = CurrRoomPosition.X;
        var y = CurrRoomPosition.Y;
        var w = CurrRoomSize.X;
        var h = CurrRoomSize.Y;

        for (var i = 0; i < w; i++)
        {
            for (var j = 0; j < h; j++)
            {
                var pos = new Vector2I(x + i, y + j);
                if (!_autoCellLayerGrid.Contains(pos) && GetCellSourceId(AutoFloorLayer, pos) != -1)
                {
                    var atlasCoords = GetCellAtlasCoords(AutoFloorLayer, pos);
                    var layer = _autoTileConfig.GetLayer2(atlasCoords);
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
                        Debug.LogError($"异常图块: {pos}, 这个图块的图集坐标'{atlasCoords}'不属于'MiddleMapLayer'和'TopMapLayer'!");
                        continue;
                    }
                    EraseCell(AutoFloorLayer, pos);
                    SetCell(layer, pos, _sourceId, atlasCoords);
                }
            }
        }
    }

    //生成导航网格
    private void GenerateNavigation()
    {
        var navigationRegion = _editorTileMap.L_NavigationRegion.Instance;
        var navigationPolygon = navigationRegion.NavigationPolygon;
        navigationPolygon.Clear();
        navigationPolygon.ClearPolygons();
        navigationPolygon.ClearOutlines();
        var endPos = CurrRoomPosition + CurrRoomSize;
        navigationPolygon.AddOutline(new []
        {
            CurrRoomPosition * GameConfig.TileCellSize,
            new Vector2(endPos.X, CurrRoomPosition.Y) * GameConfig.TileCellSize,
            endPos * GameConfig.TileCellSize,
            new Vector2(CurrRoomPosition.X, endPos.Y) * GameConfig.TileCellSize
        });
        navigationRegion.BakeNavigationPolygon(false);
    }

    //设置显示的错误cell, 会标记上红色的闪烁动画
    private void SetErrorCell(Vector2I pos)
    {
        MapEditorPanel.S_ErrorCell.Instance.Position = pos * GameConfig.TileCellSize;
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
    private void OnSelectHandTool(object arg)
    {
        MouseType = MouseButtonType.Drag;
    }
    
    /// <summary>
    /// 选中画笔攻击
    /// </summary>
    private void OnSelectPenTool(object arg)
    {
        MouseType = MouseButtonType.Pen;
    }

    /// <summary>
    /// 选中绘制区域功能
    /// </summary>
    private void OnSelectRectTool(object arg)
    {
        MouseType = MouseButtonType.Area;
    }

    /// <summary>
    /// 选择编辑门区域
    /// </summary>
    private void OnSelectEditTool(object arg)
    {
        MouseType = MouseButtonType.Edit;
    }

    /// <summary>
    /// 聚焦
    /// </summary>
    private void OnClickCenterTool(object arg)
    {
        var pos = MapEditorPanel.S_SubViewport.Instance.Size / 2;
        if (CurrRoomSize.X == 0 && CurrRoomSize.Y == 0) //聚焦原点
        {
            SetMapPosition(pos);
        }
        else //聚焦地图中心点
        {
            SetMapPosition(pos - (CurrRoomPosition + CurrRoomSize / 2) * TileSet.TileSize * Scale);
        }
    }
    
    //房间数据有修改
    private void OnEditorDirty(object obj)
    {
        IsDirty = true;
        MapEditorPanel.SetTitleDirty(true);
    }

    /// <summary>
    /// 创建地牢房间门区域
    /// </summary>
    /// <param name="direction">门方向</param>
    /// <param name="start">起始坐标, 单位: 像素</param>
    /// <param name="end">结束坐标, 单位: 像素</param>
    public DoorAreaInfo CreateDoorArea(DoorDirection direction, int start, int end)
    {
        var doorAreaInfo = new DoorAreaInfo();
        doorAreaInfo.Direction = direction;
        doorAreaInfo.Start = start;
        doorAreaInfo.End = end;
        //doorAreaInfo.CalcPosition(_roomPosition, _roomSize);
        CurrDoorConfigs.Add(doorAreaInfo);
        return doorAreaInfo;
    }

    /// <summary>
    /// 检测门区域数据是否可以提交
    /// </summary>
    /// <param name="direction">门方向</param>
    /// <param name="start">起始坐标, 单位: 像素</param>
    /// <param name="end">结束坐标, 单位: 像素</param>
    /// <returns></returns>
    public bool CheckDoorArea(DoorDirection direction, int start, int end)
    {
        foreach (var item in CurrDoorConfigs)
        {
            if (item.Direction == direction)
            {
                if (CheckValueCollision(item.Start, item.End, start, end))
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    /// <summary>
    /// 检测门区域数据是否可以提交
    /// </summary>
    /// <param name="target">需要检测的门</param>
    /// <param name="start">起始坐标, 单位: 像素</param>
    /// <param name="end">结束坐标, 单位: 像素</param>
    public bool CheckDoorArea(DoorAreaInfo target, int start, int end)
    {
        foreach (var item in CurrDoorConfigs)
        {
            if (item.Direction == target.Direction && item != target)
            {
                if (CheckValueCollision(item.Start, item.End, start, end))
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    private bool CheckValueCollision(float o1, float o2, float h1, float h2)
    {
        var size = GameConfig.TileCellSize;
        return !(h2 < o1 - 3 * size || o2 + 3 * size < h1);
    }

    /// <summary>
    /// 移除门区域数据
    /// </summary>
    public void RemoveDoorArea(DoorAreaInfo doorAreaInfo)
    {
        CurrDoorConfigs.Remove(doorAreaInfo);
    }
    
    //保存房间配置
    private void SaveRoomInfoConfig()
    {
        //存入本地
        var roomInfo = CurrRoomSplit.RoomInfo;
        
        if (!HasTerrainError) //没有绘制错误
        {
            roomInfo.Size = new SerializeVector2(CurrRoomSize);
            roomInfo.Position = new SerializeVector2(CurrRoomPosition);
        }
        else
        {
            roomInfo.Position = new SerializeVector2(CurrRoomPosition - Vector2I.One);
            roomInfo.Size = new SerializeVector2(CurrRoomSize + new Vector2I(2, 2));
        }

        roomInfo.DoorAreaInfos.Clear();
        roomInfo.DoorAreaInfos.AddRange(CurrDoorConfigs);
        roomInfo.ClearCompletionDoorArea();
        MapProjectManager.SaveRoomInfo(CurrRoomSplit);
    }

    //保存地块数据
    public void SaveTileInfoConfig()
    {
        //存入本地
        var tileInfo = CurrRoomSplit.TileInfo;
        if (tileInfo.NavigationPolygon == null)
        {
            tileInfo.NavigationPolygon = new List<int[]>();
        }
        else
        {
            tileInfo.NavigationPolygon.Clear();
        }
        if (tileInfo.NavigationVertices == null)
        {
            tileInfo.NavigationVertices = new List<SerializeVector2>();
        }
        else
        {
            tileInfo.NavigationVertices.Clear();
        }
        var polygon = _editorTileMap.L_NavigationRegion.Instance.NavigationPolygon;
        tileInfo.NavigationPolygon.AddRange(polygon.Polygons);
        tileInfo.NavigationVertices.AddRange(polygon.Vertices.Select(v => new SerializeVector2(v)));
        tileInfo.Floor.Clear();
        tileInfo.Middle.Clear();
        tileInfo.Top.Clear();

        PushLayerDataToList(AutoFloorLayer, _sourceId, tileInfo.Floor);
        PushLayerDataToList(AutoMiddleLayer, _sourceId, tileInfo.Middle);
        PushLayerDataToList(AutoTopLayer, _sourceId, tileInfo.Top);
        MapProjectManager.SaveRoomTileInfo(CurrRoomSplit);
    }

    /// <summary>
    /// 保存预设数据
    /// </summary>
    public void SavePreinstallConfig()
    {
        //存入本地
        MapProjectManager.SaveRoomPreinstall(CurrRoomSplit);
    }

    /// <summary>
    /// 获取相机中心点坐标
    /// </summary>
    public Vector2I GetCenterPosition()
    {
        var pos = ToLocal(MapEditorPanel.S_SubViewport.Instance.Size / 2);
        return new Vector2I((int)pos.X, (int)pos.Y);
    }
    
    /// <summary>
    /// 设置相机看向的点
    /// </summary>
    public void SetLookPosition(Vector2 pos)
    {
        SetMapPosition(-pos * Scale + MapEditorPanel.S_SubViewport.Instance.Size / 2);
        //SetMapPosition(pos * Scale);
        //SetMapPosition(pos + MapEditorPanel.S_SubViewport.Instance.Size / 2);
    }
    
    /// <summary>
    /// 设置地图坐标
    /// </summary>
    public void SetMapPosition(Vector2 pos)
    {
        Position = pos;
        MapEditorToolsPanel.SetToolTransform(pos, Scale);
    }

    //设置地图大小
    private void SetMapSize(Vector2I size, bool refreshDoorTrans)
    {
        if (CurrRoomSize != size)
        {
            CurrRoomSize = size;

            if (refreshDoorTrans)
            {
                MapEditorToolsPanel.SetDoorHoverToolTransform(CurrRoomPosition, CurrRoomSize);
            }
        }
    }

    private bool _hasPreviewImage = false;
    private Action _previewFinish;
    private int _previewIndex = 0;
    private Vector2I _tempViewportSize;
    private Vector2 _tempMapPos;
    private Vector2 _tempMapScale;
    private bool _tempAutoFloorLayer;
    private bool _tempCustomFloorLayer;
    private bool _tempAutoMiddleLayer;
    private bool _tempCustomMiddleLayer;
    private bool _tempAutoTopLayer;
    private bool _tempCustomTopLayer;

    private void RunSavePreviewImage(Action action)
    {
        if (_hasPreviewImage)
        {
            return;
        }

        _previewIndex = 0;
        _previewFinish = action;
        _hasPreviewImage = true;
        //先截图, 将图像数据放置到 S_MapView2 节点上
        var subViewport = MapEditorPanel.S_SubViewport.Instance;
        var viewportTexture = subViewport.GetTexture();
        var tex = ImageTexture.CreateFromImage(viewportTexture.GetImage());
        var textureRect = MapEditorPanel.S_MapView2.Instance;
        textureRect.Texture = tex;
        textureRect.Visible = true;
        //调整绘制视图大小
        _tempViewportSize = subViewport.Size;
        subViewport.Size = new Vector2I(GameConfig.PreviewImageSize, GameConfig.PreviewImageSize);
        //调整tileMap
        _tempMapPos = Position;
        _tempMapScale = Scale;
        
        //中心点
        var pos = new Vector2(GameConfig.PreviewImageSize / 2f, GameConfig.PreviewImageSize / 2f);
        if (CurrRoomSize.X == 0 && CurrRoomSize.Y == 0) //聚焦原点
        {
            Position = pos;
        }
        else //聚焦地图中心点
        {
            var tempPos = new Vector2(CurrRoomSize.X, CurrRoomSize.Y);
            //var tempPos = new Vector2(CurrRoomSize.X + 2, CurrRoomSize.Y + 2);
            var mapSize = tempPos * TileSet.TileSize;
            var axis = Mathf.Max(mapSize.X, mapSize.Y);
            var targetScale = GameConfig.PreviewImageSize / axis;
            Scale = new Vector2(targetScale, targetScale);
            Position = pos - (CurrRoomPosition + tempPos / 2f) * TileSet.TileSize * targetScale;
        }
        
        //隐藏工具栏
        MapEditorToolsPanel.Visible = false;
        //显示所有层级
        _tempAutoFloorLayer = IsLayerEnabled(AutoFloorLayer);
        _tempCustomFloorLayer = IsLayerEnabled(CustomFloorLayer);
        _tempAutoMiddleLayer = IsLayerEnabled(AutoMiddleLayer);
        _tempCustomMiddleLayer = IsLayerEnabled(CustomMiddleLayer);
        _tempAutoTopLayer = IsLayerEnabled(AutoTopLayer);
        _tempCustomTopLayer = IsLayerEnabled(CustomTopLayer);

        SetLayerEnabled(AutoFloorLayer, true);
        SetLayerEnabled(CustomFloorLayer, true);
        SetLayerEnabled(AutoMiddleLayer, true);
        SetLayerEnabled(CustomMiddleLayer, true);
        SetLayerEnabled(AutoTopLayer, true);
        SetLayerEnabled(CustomTopLayer, true);
    }

    private void OnFramePostDraw()
    {
        if (_hasPreviewImage)
        {
            _previewIndex++;
            if (_previewIndex == 2)
            {
                var textureRect = MapEditorPanel.S_MapView2.Instance;
                var texture = textureRect.Texture;
                textureRect.Texture = null;
                texture.Dispose();
                textureRect.Visible = false;
                
                //还原工具栏
                MapEditorToolsPanel.Visible = true;
                //还原层级显示
                SetLayerEnabled(AutoFloorLayer, _tempAutoFloorLayer);
                SetLayerEnabled(CustomFloorLayer, _tempCustomFloorLayer);
                SetLayerEnabled(AutoMiddleLayer, _tempAutoMiddleLayer);
                SetLayerEnabled(CustomMiddleLayer, _tempCustomMiddleLayer);
                SetLayerEnabled(AutoTopLayer, _tempAutoTopLayer);
                SetLayerEnabled(CustomTopLayer, _tempCustomTopLayer);

                //保存预览图
                var subViewport = MapEditorPanel.S_SubViewport.Instance;
                var viewportTexture = subViewport.GetTexture();
                var image = viewportTexture.GetImage();
                image.Resize(GameConfig.PreviewImageSize, GameConfig.PreviewImageSize, Image.Interpolation.Nearest);
                CurrRoomSplit.PreviewImage = ImageTexture.CreateFromImage(image);
                MapProjectManager.SaveRoomPreviewImage(CurrRoomSplit, image);
                
                //还原tileMap
                Position = _tempMapPos;
                Scale = _tempMapScale;
                
                //还原绘制视图
                subViewport.Size = _tempViewportSize;
                
                _previewFinish();
                _hasPreviewImage = false;
            }
        }
    }
    
    private void OnBakeFinished()
    {
        var polygonData = _editorTileMap.L_NavigationRegion.Instance.NavigationPolygon;
        var polygons = polygonData.Polygons;
        var vertices = polygonData.Vertices;
        _polygonData = new Vector2[polygons.Count][];
        for (var i = 0; i < polygons.Count; i++)
        {
            var polygon = polygons[i];
            var v2Array = new Vector2[polygon.Length];
            for (var j = 0; j < polygon.Length; j++)
            {
                v2Array[j] = vertices[polygon[j]];
            }
            _polygonData[i] = v2Array;
        }
    }
}