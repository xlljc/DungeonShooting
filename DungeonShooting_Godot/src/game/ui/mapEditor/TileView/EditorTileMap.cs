using System.Collections.Generic;
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
    private DungeonTile _dungeonTile;
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
    
    //--------- 配置数据 -------------
    private int _sourceId = 0;
    private int _terrainSet = 0;
    private int _terrain = 0;
    private Vector2I _floorAtlasCoords = new Vector2I(0, 8);
    private AutoTileConfig _autoTileConfig = new AutoTileConfig();
    //-------------------------------

    public override void _Ready()
    {
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

        _dungeonTile = new DungeonTile(this);
        _dungeonTile.SetFloorAtlasCoords(new List<Vector2I>(new []{ _floorAtlasCoords }));
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
        
        if (_checkTerrainFlag) //已经通过地形检测
        {
            //绘制导航网格
            var result = _dungeonTile.GetGenerateNavigationResult();
            if (result != null && result.Success)
            {
                var polygonData = _dungeonTile.GetPolygonData();
                Utils.DrawNavigationPolygon(canvasItem, polygonData, 1);
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
        SetCell(GetFloorLayer(), position, _sourceId, _floorAtlasCoords);
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
                SetCell(GetFloorLayer(), new Vector2I(start.X + i, start.Y + j), _sourceId, _floorAtlasCoords);
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
        SetLayerEnabled(AutoTopLayer, false);
        SetLayerEnabled(AutoMiddleLayer, false);
    }
    
    //检测是否有不合规的图块, 返回true表示图块正常
    private bool CheckTerrain()
    {
        var usedRect = GetUsedRect();
        var x = usedRect.Position.X;
        var y = usedRect.Position.Y;
        var w = usedRect.Size.X;
        var h = usedRect.Size.Y;

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
        SetLayerEnabled(AutoTopLayer, true);
        SetLayerEnabled(AutoMiddleLayer, true);
        ClearLayer(AutoTopLayer);
        ClearLayer(AutoMiddleLayer);
        
        var usedRect = GetUsedRect();
        var x = usedRect.Position.X;
        var y = usedRect.Position.Y;
        var w = usedRect.Size.X;
        var h = usedRect.Size.Y;

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
        _dungeonTile.GenerateNavigationPolygon(AutoFloorLayer);
        var result = _dungeonTile.GetGenerateNavigationResult();
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
        Position = MapEditorPanel.S_SubViewport.Instance.Size / 2;
    }
}