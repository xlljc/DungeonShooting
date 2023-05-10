using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 房间管理器
/// </summary>
public partial class RoomManager : Node2D
{
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
    /// 起始房间
    /// </summary>
    public RoomInfo StartRoom => _dungeonGenerator.StartRoom;
    
    /// <summary>
    /// 当前玩家所在的房间
    /// </summary>
    public RoomInfo ActiveRoom => Player?.Affiliation?.RoomInfo;
    
    /// <summary>
    /// 当前玩家所在的区域
    /// </summary>
    public AffiliationArea ActiveAffiliation => Player?.Affiliation;
    
    private DungeonTile _dungeonTile;
    private AutoTileConfig _autoTileConfig;
    
    private Font _font;
    private DungeonGenerator _dungeonGenerator;

    private int _affiliationIndex = 0;

    private float _checkEnemyTimer = 0;

    //房间内所有静态导航网格数据
    private static List<NavigationPolygonData> _roomStaticNavigationList = new List<NavigationPolygonData>();
    
    public override void _Ready()
    {
        TileRoot.YSortEnabled = false;
        
        _font = ResourceManager.Load<Font>(ResourcePath.resource_font_cn_font_36_tres);

        //绑定事件
        EventManager.AddEventListener(EventEnum.OnPlayerFirstEnterRoom, OnPlayerFirstEnterRoom);
        EventManager.AddEventListener(EventEnum.OnPlayerEnterRoom, OnPlayerEnterRoom);

        var nowTicks = DateTime.Now.Ticks;
        //生成地牢房间
        _dungeonGenerator = new DungeonGenerator("testGroup");
        _dungeonGenerator.Generate();
        
        //填充地牢
        _autoTileConfig = new AutoTileConfig();
        _dungeonTile = new DungeonTile(TileRoot);
        _dungeonTile.AutoFillRoomTile(_autoTileConfig, _dungeonGenerator.StartRoom);
        
        //生成寻路网格， 这一步操作只生成过道的导航
        _dungeonTile.GenerateNavigationPolygon(GameConfig.AisleFloorMapLayer);
        //挂载过道导航区域
        _dungeonTile.MountNavigationPolygon(this);
        //过道导航区域数据
        _roomStaticNavigationList.AddRange(_dungeonTile.GetPolygonData());
        //门导航区域数据
        _roomStaticNavigationList.AddRange(_dungeonTile.GetConnectDoorPolygonData());
        //初始化所有房间
        _dungeonGenerator.EachRoom(InitRoom);

        GD.Print("生成地牢用时: " + (DateTime.Now.Ticks - nowTicks) / 10000 + "毫秒");

        //播放bgm
        //SoundManager.PlayMusic(ResourcePath.resource_sound_bgm_Intro_ogg, -17f);

        //初始房间创建玩家标记
        var playerBirthMark = StartRoom.ActivityMarks.FirstOrDefault(mark => mark.Type == ActivityIdPrefix.ActivityPrefixType.Player);
        //创建玩家
        Player = ActivityObject.Create<Player>(ActivityIdPrefix.Role + "0001");
        if (playerBirthMark != null)
        {
            Player.Position = playerBirthMark.Position;
        }
        Player.Name = "Player";
        Player.PutDown(RoomLayerEnum.YSortLayer);
        Player.PickUpWeapon(ActivityObject.Create<Weapon>(ActivityIdPrefix.Weapon + "0001"));

        // var weapon = ActivityObject.Create(ActivityIdPrefix.Test + "0001");
        // weapon.PutDown(new  Vector2(200, 200), RoomLayerEnum.NormalLayer);
        // //weapon.Altitude = 50;

        // for (int i = 0; i < 10; i++)
        // {
        //     var enemy = ActivityObject.Create<Enemy>(ActivityIdPrefix.Enemy + "0001");
        //     enemy.PutDown(new Vector2(100 + i * 20, 100), RoomLayerEnum.YSortLayer);
        //     enemy.PickUpWeapon(ActivityObject.Create<Weapon>(ActivityIdPrefix.Weapon + Utils.RandomChoose("0001", "0002", "0003")));
        // }

        //相机跟随玩家
        GameCamera.Main.SetFollowTarget(Player);
        
        //修改鼠标指针
        var cursor = GameApplication.Instance.Cursor;
        cursor.SetGuiMode(false);
        cursor.SetMountRole(Player);
    }

    public override void _PhysicsProcess(double delta)
    {
        _checkEnemyTimer += (float)delta;
        if (_checkEnemyTimer >= 1)
        {
            _checkEnemyTimer %= 1;
            //检查房间内的敌人存活状况
            OnCheckEnemy();
        }
    }

    /// <summary>
    /// 获取指定层级根节点
    /// </summary>
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
            if (_dungeonTile != null)
            {
                //绘制ai寻路区域
                Utils.DrawNavigationPolygon(this, _roomStaticNavigationList.ToArray());
            }
            //绘制房间区域
            //DrawRoomInfo(_generateDungeon.StartRoom);
        }
    }

    // 初始化房间
    private void InitRoom(RoomInfo roomInfo)
    {
        //挂载房间导航区域
        MountNavFromRoomInfo(roomInfo);
        //创建门
        CreateDoor(roomInfo);
        
        //创建房间归属区域
        CreateRoomAisleAffiliation(roomInfo);
    }

    //挂载房间导航区域
    private void MountNavFromRoomInfo(RoomInfo roomInfo)
    {
        var polygonArray = roomInfo.RoomSplit.RoomInfo.NavigationList.ToArray();
        var polygon = new NavigationPolygon();
        var offset = roomInfo.GetOffsetPosition();
        for (var i = 0; i < polygonArray.Length; i++)
        {
            var navigationPolygonData = polygonArray[i];
            var polygonPointArray = navigationPolygonData.ConvertPointsToVector2Array();
            //这里的位置需要加上房间位置
            for (var j = 0; j < polygonPointArray.Length; j++)
            {
                polygonPointArray[j] = polygonPointArray[j] + roomInfo.GetWorldPosition() - offset;
            }
            polygon.AddOutline(polygonPointArray);
            
            var points = new List<SerializeVector2>();
            for (var j = 0; j < polygonPointArray.Length; j++)
            {
                points.Add(new SerializeVector2(polygonPointArray[j]));
            }
            
            //存入汇总列表
            _roomStaticNavigationList.Add(new NavigationPolygonData(navigationPolygonData.Type, points));
        }
        polygon.MakePolygonsFromOutlines();
        var navigationPolygon = new NavigationRegion2D();
        navigationPolygon.Name = "NavigationRegion" + (GetChildCount() + 1);
        navigationPolygon.NavigationPolygon = polygon;
        AddChild(navigationPolygon);
    }

    //创建门
    private void CreateDoor(RoomInfo roomInfo)
    {
        foreach (var doorInfo in roomInfo.Doors)
        {
            var door = ActivityObject.Create<RoomDoor>(ActivityIdPrefix.Other + "0001");
            doorInfo.Door = door;
            Vector2 offset;
            switch (doorInfo.Direction)
            {
                case DoorDirection.E:
                    offset = new Vector2(0.5f, 2);
                    break;
                case DoorDirection.W:
                    offset = new Vector2(-0.5f, 2);
                    break;
                case DoorDirection.S:
                    offset = new Vector2(2f, 1.5f);
                    break;
                case DoorDirection.N:
                    offset = new Vector2(2f, -0.5f);
                    break;
                default: offset = new Vector2();
                    break;
            }
            door.Position = (doorInfo.OriginPosition + offset) * GameConfig.TileCellSize;
            door.Init(doorInfo);
            door.PutDown(RoomLayerEnum.NormalLayer, false);
        }
    }

    //创建房间归属区域
    private void CreateRoomAisleAffiliation(RoomInfo roomInfo)
    {
        var affiliation = new AffiliationArea();
        affiliation.Name = "AffiliationArea" + (_affiliationIndex++);
        affiliation.Init(roomInfo, new Rect2(
            roomInfo.GetWorldPosition() + new Vector2(GameConfig.TileCellSize, GameConfig.TileCellSize),
            (roomInfo.Size - new Vector2I(2, 2)) * GameConfig.TileCellSize));
        
        roomInfo.Affiliation = affiliation;
        TileRoot.AddChild(affiliation);
    }

    /// <summary>
    /// 玩家第一次进入某个房间回调
    /// </summary>
    private void OnPlayerFirstEnterRoom(object o)
    {
        var room = (RoomInfo)o;
        room.BeReady();
    }

    /// <summary>
    /// 玩家进入某个房间回调
    /// </summary>
    private void OnPlayerEnterRoom(object o)
    {
    }
    
    /// <summary>
    /// 检测当前房间敌人是否已经消灭干净, 应当每秒执行一次
    /// </summary>
    private void OnCheckEnemy()
    {
        var activeRoom = ActiveRoom;
        if (activeRoom != null)// && //activeRoom.IsSeclusion)
        {
            if (activeRoom.IsCurrWaveOver()) //所有标记执行完成
            {
                //存活敌人数量
                var count = ActiveAffiliation.FindIncludeItemsCount(
                    activityObject => activityObject.CollisionWithMask(PhysicsLayer.Enemy)
                );
                GD.Print("当前房间存活数量: " + count);
                if (count == 0)
                {
                    activeRoom.OnClearRoom();
                }
            }
        }
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