using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 房间管理器
/// </summary>
public partial class RoomManager : Node2D
{
    public const int FloorMapLayer = 0;
    public const int MiddleMapLayer = 1;
    public const int TopMapLayer = 2;
    
    /// <summary>
    /// //对象根节点
    /// </summary>
    [Export] public Node2D NormalLayer;
    
    /// <summary>
    /// 对象根节点, 带y轴排序功能
    /// </summary>
    [Export] public Node2D YSortLayer;
    
    /// <summary>
    /// 地图根节点
    /// </summary>
    [Export] public TileMap TileRoot;

    /// <summary>
    /// 玩家对象
    /// </summary>
    public Player Player { get; private set; }

    /// <summary>
    /// 导航区域形状
    /// </summary>
    public NavigationRegion2D NavigationPolygon { get; private set; }

    //已经标记过的点
    private HashSet<Vector2> _usePoints = new HashSet<Vector2>();

    //导航区域数据
    private List<NavigationPolygonData> _polygonDataList = new List<NavigationPolygonData>();

    private AutoTileConfig _autoTileConfig;
    
    private Font _font;
    private GenerateDungeon _generateDungeon;

    public override void _EnterTree()
    {
        //Engine.TimeScale = 0.2f;

        NavigationPolygon = new NavigationRegion2D();
        AddChild(NavigationPolygon);

        //_tileMap = GetNode<Godot.TileMap>(TileMap);
        
        // var node = child.GetNode("Config");
        // Color color = (Color)node.GetMeta("ClearColor");
        // GetTabAlignment.SetDefaultClearColor(color);

        //创建玩家
        Player = new Player();
        Player.Position = new Vector2(80, 80);
        Player.Name = "Player";
        Player.PutDown(RoomLayerEnum.YSortLayer);
    }

    public override void _Ready()
    {
        TileRoot.YSortEnabled = false;
        //FloorTileMap.NavigationVisibilityMode = TileMap.VisibilityMode.ForceShow;
        
        _font = ResourceManager.Load<Font>(ResourcePath.resource_font_cn_font_36_tres);

        //生成地牢房间
        _generateDungeon = new GenerateDungeon();
        _generateDungeon.Generate();
        
        //填充地牢
        _autoTileConfig = new AutoTileConfig();
        DungeonTileManager.AutoFillRoomTile(TileRoot, FloorMapLayer, MiddleMapLayer, TopMapLayer, _autoTileConfig, _generateDungeon.StartRoom);

        //根据房间数据创建填充 tiled
        
        var nowTicks = DateTime.Now.Ticks;
        //生成寻路网格
        GenerateNavigationPolygon();
        var polygon = new NavigationPolygon();
        foreach (var polygonData in _polygonDataList)
        {
            polygon.AddOutline(polygonData.Points.ToArray());
        }
        polygon.MakePolygonsFromOutlines();
        NavigationPolygon.NavigationPolygon = polygon;
        GD.Print("计算NavigationPolygon用时: " + (DateTime.Now.Ticks - nowTicks) / 10000 + "毫秒");

        //播放bgm
        SoundManager.PlayMusic(ResourcePath.resource_sound_bgm_Intro_ogg, -17f);

        Player.PickUpWeapon(WeaponManager.GetGun("1001"));
        // Player.PickUpWeapon(WeaponManager.GetGun("1002"));
        Player.PickUpWeapon(WeaponManager.GetGun("1004"));
        Player.PickUpWeapon(WeaponManager.GetGun("1003"));
        
        // var enemy1 = new Enemy();
        // enemy1.PutDown(new Vector2(100, 100), RoomLayerEnum.YSortLayer);
        // enemy1.PickUpWeapon(WeaponManager.GetGun("1001"));
        
        for (int i = 0; i < 10; i++)
        {
            var enemyTemp = new Enemy();
            enemyTemp.PutDown(new Vector2(30 + (i + 1) * 20, 30), RoomLayerEnum.YSortLayer);
            // enemyTemp.PickUpWeapon(WeaponManager.GetGun("1003"));
            // enemyTemp.PickUpWeapon(WeaponManager.GetGun("1001"));
        }

        // var enemy2 = new Enemy();
        // enemy2.Name = "Enemy2";
        // enemy2.PutDown(new Vector2(120, 100));
        // enemy2.PickUpWeapon(WeaponManager.GetGun("1002"));
        // //enemy2.PickUpWeapon(WeaponManager.GetGun("1004"));
        // //enemy2.PickUpWeapon(WeaponManager.GetGun("1003"));
        //
        // var enemy3 = new Enemy();
        // enemy3.Name = "Enemy3";
        // enemy3.PutDown(new Vector2(100, 120));
        // enemy3.PickUpWeapon(WeaponManager.GetGun("1003"));
        // enemy3.PickUpWeapon(WeaponManager.GetGun("1002"));

        WeaponManager.GetGun("1004").PutDown(new Vector2(80, 100), RoomLayerEnum.NormalLayer);
        WeaponManager.GetGun("1001").PutDown(new Vector2(220, 120), RoomLayerEnum.NormalLayer);
        WeaponManager.GetGun("1001").PutDown(new Vector2(230, 120), RoomLayerEnum.NormalLayer);
        WeaponManager.GetGun("1001").PutDown(new Vector2(80, 80), RoomLayerEnum.NormalLayer);
        WeaponManager.GetGun("1002").PutDown(new Vector2(80, 120), RoomLayerEnum.NormalLayer);
        WeaponManager.GetGun("1003").PutDown(new Vector2(120, 80), RoomLayerEnum.NormalLayer);
        WeaponManager.GetGun("1003").PutDown(new Vector2(130, 80), RoomLayerEnum.NormalLayer);
        WeaponManager.GetGun("1003").PutDown(new Vector2(140, 80), RoomLayerEnum.NormalLayer);
        
        // WeaponManager.GetGun("1003").PutDown(new Vector2(180, 80), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1003").PutDown(new Vector2(180, 180), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1002").PutDown(new Vector2(180, 120), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1002").PutDown(new Vector2(180, 130), RoomLayerEnum.NormalLayer);

    }

    /// <summary>
    /// 获取指定层级根节点
    /// </summary>
    /// <param name="layerEnum"></param>
    /// <returns></returns>
    public Node2D GetRoomLayer(RoomLayerEnum layerEnum)
    {
        switch (layerEnum)
        {
            case RoomLayerEnum.NormalLayer:
                return NormalLayer;
            case RoomLayerEnum.YSortLayer:
                return YSortLayer;
        }

        return null;
    }

    public override void _Process(double delta)
    {
        Enemy.UpdateEnemiesView();
        if (GameApplication.Instance.Debug)
        {
            QueueRedraw();
        }
    }

    public override void _Draw()
    {
        if (GameApplication.Instance.Debug)
        {
            //绘制ai寻路区域
            for (var i = 0; i < _polygonDataList.Count; i++)
            {
                var item = _polygonDataList[i];
                if (item.Points.Count >= 2)
                {
                    DrawPolyline(item.Points.Concat(new []{ item.Points[0] }).ToArray(), Colors.Red);
                }
            }
        }
        //绘制房间区域
        DrawRoomInfo(_generateDungeon.StartRoom);
    }

    /// <summary>
    /// 返回指定位置的Tile是否为可以行走
    /// </summary>
    public bool IsWayTile(int x, int y)
    {
        return TileRoot.GetCellTileData(FloorMapLayer, new Vector2I(x, y)) != null;
    }

    /// <summary>
    /// 返回指定坐标下对应的Tile是否为可以行走
    /// </summary>
    public bool IsWayPosition(float x, float y)
    {
        var tileMapCellSize = TileRoot.CellQuadrantSize;
        return IsWayTile((int)(x / tileMapCellSize), (int)(y / tileMapCellSize));
    }

    /// <summary>
    /// 自动生成导航区域
    /// </summary>
    private void GenerateNavigationPolygon()
    {
        var size = new Vector2(TileRoot.CellQuadrantSize, TileRoot.CellQuadrantSize);

        var rect = TileRoot.GetUsedRect();

        var x = rect.Position.X;
        var y = rect.Position.Y;
        var w = rect.Size.X;
        var h = rect.Size.Y;

        for (int j = y; j < h; j++)
        {
            for (int i = x; i < w; i++)
            {
                if (IsWayTile(i, j))
                {
                    if (!_usePoints.Contains(new Vector2(i, j)))
                    {
                        NavigationPolygonData polygonData = null;

                        if (!IsWayTile(i, j - 1))
                        {
                            polygonData = CalcOutline(i, j, TileRoot, size);
                        }
                        else if (!IsWayTile(i, j + 1))
                        {
                            polygonData = CalcInline(i, j, TileRoot, size);
                        }

                        if (polygonData != null)
                        {
                            _polygonDataList.Add(polygonData);
                        }
                    }
                }
            }
        }
    }
    
    //计算导航网格外轮廓
    private NavigationPolygonData CalcOutline(int i, int j, TileMap tileMap, Vector2 size)
    {
        var polygonData = new NavigationPolygonData();
        polygonData.Type = NavigationPolygonType.Out;
        var points = polygonData.Points;
        // 0:右, 1:下, 2:左, 3:上
        var dir = 0;
        var offset = new Vector2(size.X * 0.5f, size.Y * 0.5f);
        //找到路, 向右开始找边界
        var startPos = new Vector2(i, j);

        var tempI = i;
        var tempJ = j;

        while (true)
        {
            switch (dir)
            {
                case 0: //右
                {
                    if (IsWayTile(tempI, tempJ - 1)) //先向上找
                    {
                        dir = 3;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }
                    else if (IsWayTile(tempI + 1, tempJ)) //再向右找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempI++;
                        break;
                    }
                    else if (IsWayTile(tempI, tempJ + 1)) //向下找
                    {
                        dir = 1;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }

                    return null;
                }
                case 1: //下
                {
                    if (IsWayTile(tempI + 1, tempJ)) //先向右找
                    {
                        dir = 0;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }
                    else if (IsWayTile(tempI, tempJ + 1)) //再向下找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempJ++;
                        break;
                    }
                    else if (IsWayTile(tempI - 1, tempJ)) //向左找
                    {
                        dir = 2;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }

                    return null;
                }
                case 2: //左
                {
                    if (IsWayTile(tempI, tempJ + 1)) //先向下找
                    {
                        dir = 1;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }
                    else if (IsWayTile(tempI - 1, tempJ)) //再向左找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempI--;
                        break;
                    }
                    else if (IsWayTile(tempI, tempJ - 1)) //向上找
                    {
                        dir = 3;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }

                    return null;
                }
                case 3: //上
                {
                    if (IsWayTile(tempI - 1, tempJ)) //先向左找
                    {
                        dir = 2;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }
                    else if (IsWayTile(tempI, tempJ - 1)) //再向上找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempJ--;
                        break;
                    }
                    else if (IsWayTile(tempI + 1, tempJ)) //向右找
                    {
                        dir = 0;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }

                    return null;
                }
            }
        }
    }

    //计算导航网格内轮廓
    private NavigationPolygonData CalcInline(int i, int j, TileMap tileMap, Vector2 size)
    {
        var polygonData = new NavigationPolygonData();
        polygonData.Type = NavigationPolygonType.In;
        var points = polygonData.Points;
        // 0:右, 1:下, 2:左, 3:上
        var dir = 0;
        var offset = new Vector2(size.X * 0.5f, size.Y * 0.5f);
        //找到路, 向右开始找边界
        var startPos = new Vector2(i - 1, j);

        var tempI = i;
        var tempJ = j;

        while (true)
        {
            switch (dir)
            {
                case 0: //右
                {
                    if (IsWayTile(tempI, tempJ + 1)) //向下找
                    {
                        dir = 1;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }
                    else if (IsWayTile(tempI + 1, tempJ)) //再向右找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.X, tempJ * size.Y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempI++;
                        break;
                    }
                    else if (IsWayTile(tempI, tempJ - 1)) //先向上找
                    {
                        dir = 3;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }

                    return null;
                }
                case 1: //下
                {
                    if (IsWayTile(tempI - 1, tempJ)) //向左找
                    {
                        dir = 2;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }
                    else if (IsWayTile(tempI, tempJ + 1)) //再向下找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.X, tempJ * size.Y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempJ++;
                        break;
                    }
                    else if (IsWayTile(tempI + 1, tempJ)) //先向右找
                    {
                        dir = 0;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }

                    return null;
                }
                case 2: //左
                {
                    if (IsWayTile(tempI, tempJ - 1)) //向上找
                    {
                        dir = 3;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }
                    else if (IsWayTile(tempI - 1, tempJ)) //再向左找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.X, tempJ * size.Y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempI--;
                        break;
                    }
                    else if (IsWayTile(tempI, tempJ + 1)) //先向下找
                    {
                        dir = 1;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }

                    return null;
                }
                case 3: //上
                {
                    if (IsWayTile(tempI + 1, tempJ)) //向右找
                    {
                        dir = 0;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }
                    else if (IsWayTile(tempI, tempJ - 1)) //再向上找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.X, tempJ * size.Y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempJ--;
                        break;
                    }
                    else if (IsWayTile(tempI - 1, tempJ)) //先向左找
                    {
                        dir = 2;

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X, tempJ * size.Y) + offset);
                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }

                    return null;
                }
            }
        }
    }

    //记录导航网格中已经使用过的坐标
    private void PutUsePoint(Vector2 pos)
    {
        if (_usePoints.Contains(pos))
        {
            throw new Exception("生成导航多边形发生错误! 点: " + pos + "发生交错!");
        }

        _usePoints.Add(pos);
    }
    
    //绘制房间区域, debug 用
    private void DrawRoomInfo(RoomInfo room)
    {
        var cellSize = TileRoot.CellQuadrantSize;
        var pos1 = (room.Position + room.Size / 2) * cellSize;
        
        //绘制下一个房间
        foreach (var nextRoom in room.Next)
        {
            var pos2 = (nextRoom.Position + nextRoom.Size / 2) * cellSize;
            DrawLine(pos1, pos2, Colors.Red);
            DrawRoomInfo(nextRoom);
        }

        DrawString(_font, pos1, room.Id.ToString());

        //绘制门
        foreach (var roomDoor in room.Doors)
        {
            var originPos = roomDoor.OriginPosition * cellSize;
            switch (roomDoor.Direction)
            {
                case DoorDirection.E:
                    DrawLine(originPos, originPos + new Vector2(3, 0) * cellSize, Colors.Yellow);
                    DrawLine(originPos + new Vector2(0, 4) * cellSize, originPos + new Vector2(3, 4) * cellSize,
                        Colors.Yellow);
                    break;
                case DoorDirection.W:
                    DrawLine(originPos, originPos - new Vector2(3, 0) * cellSize, Colors.Yellow);
                    DrawLine(originPos + new Vector2(0, 4) * cellSize, originPos - new Vector2(3, -4) * cellSize,
                        Colors.Yellow);
                    break;
                case DoorDirection.S:
                    DrawLine(originPos, originPos + new Vector2(0, 3) * cellSize, Colors.Yellow);
                    DrawLine(originPos + new Vector2(4, 0) * cellSize, originPos + new Vector2(4, 3) * cellSize,
                        Colors.Yellow);
                    break;
                case DoorDirection.N:
                    DrawLine(originPos, originPos - new Vector2(0, 3) * cellSize, Colors.Yellow);
                    DrawLine(originPos + new Vector2(4, 0) * cellSize, originPos - new Vector2(-4, 3) * cellSize,
                        Colors.Yellow);
                    break;
            }
            
            //绘制房间区域
            DrawRect(new Rect2(room.Position * cellSize, room.Size * cellSize), Colors.Blue, false);

            if (roomDoor.HasCross && roomDoor.RoomInfo.Id < roomDoor.ConnectRoom.Id)
            {
                DrawRect(new Rect2(roomDoor.Cross * cellSize, new Vector2(cellSize * 4, cellSize * 4)), Colors.Yellow, false);
            }
        }
    }
}