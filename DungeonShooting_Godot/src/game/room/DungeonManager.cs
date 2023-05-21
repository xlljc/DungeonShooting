﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 地牢管理器
/// </summary>
public partial class DungeonManager : Node2D
{
    /// <summary>
    /// 起始房间
    /// </summary>
    public RoomInfo StartRoom => _dungeonGenerator?.StartRoom;
    
    /// <summary>
    /// 当前玩家所在的房间
    /// </summary>
    public RoomInfo ActiveRoom => Player.Current?.Affiliation?.RoomInfo;
    
    /// <summary>
    /// 当前玩家所在的区域
    /// </summary>
    public AffiliationArea ActiveAffiliation => Player.Current?.Affiliation;

    /// <summary>
    /// 是否在地牢里
    /// </summary>
    public bool IsInDungeon { get; private set; }

    private DungeonConfig _config;
    private DungeonTile _dungeonTile;
    private AutoTileConfig _autoTileConfig;
    private DungeonGenerator _dungeonGenerator;
    //房间内所有静态导航网格数据
    private List<NavigationPolygonData> _roomStaticNavigationList;
    private World _world;
    
    //用于检查房间敌人的计时器
    private int _affiliationIndex = 0;
    private float _checkEnemyTimer = 0;


    public DungeonManager()
    {
        //绑定事件
        EventManager.AddEventListener(EventEnum.OnPlayerFirstEnterRoom, OnPlayerFirstEnterRoom);
        EventManager.AddEventListener(EventEnum.OnPlayerEnterRoom, OnPlayerEnterRoom);
    }
    
    /// <summary>
    /// 加载地牢
    /// </summary>
    public void LoadDungeon(DungeonConfig config)
    {
        _config = config;
        GameApplication.Instance.StartCoroutine(RunLoadDungeonCoroutine());
    }

    /// <summary>
    /// 退出地牢
    /// </summary>
    public void ExitDungeon()
    {
        IsInDungeon = false;
        GameApplication.Instance.StartCoroutine(RunExitDungeonCoroutine());
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsInDungeon)
        {
            _checkEnemyTimer += (float)delta;
            if (_checkEnemyTimer >= 1)
            {
                _checkEnemyTimer %= 1;
                //检查房间内的敌人存活状况
                OnCheckEnemy();
            }
        }
    }

    public override void _Process(double delta)
    {
        if (IsInDungeon)
        {
            UpdateEnemiesView();
            if (GameApplication.Instance.Debug)
            {
                QueueRedraw();
            }
        }
    }

    //执行加载地牢协程
    private IEnumerator RunLoadDungeonCoroutine()
    {
        yield return 0;
        //打开 loading UI
        UiManager.Open_Loading();
        //创建世界场景
        _world = GameApplication.Instance.CreateNewWorld();
        yield return new WaitForFixedProcess(10);
        //生成地牢房间
        _dungeonGenerator = new DungeonGenerator(_config);
        _dungeonGenerator.Generate();
        yield return 0;
        
        //填充地牢
        _autoTileConfig = new AutoTileConfig();
        _dungeonTile = new DungeonTile(_world.TileRoot);
        _dungeonTile.AutoFillRoomTile(_autoTileConfig, _dungeonGenerator.StartRoom);
        yield return 0;
        
        //生成寻路网格， 这一步操作只生成过道的导航
        _dungeonTile.GenerateNavigationPolygon(GameConfig.AisleFloorMapLayer);
        yield return 0;
        //挂载过道导航区域
        _dungeonTile.MountNavigationPolygon(_world.TileRoot);
        yield return 0;
        //过道导航区域数据
        _roomStaticNavigationList = new List<NavigationPolygonData>();
        _roomStaticNavigationList.AddRange(_dungeonTile.GetPolygonData());
        yield return 0;
        //门导航区域数据
        _roomStaticNavigationList.AddRange(_dungeonTile.GetConnectDoorPolygonData());
        yield return new WaitForFixedProcess(10);
        //初始化所有房间
        _dungeonGenerator.EachRoom(InitRoom);
        yield return new WaitForFixedProcess(10);

        //播放bgm
        //SoundManager.PlayMusic(ResourcePath.resource_sound_bgm_Intro_ogg, -17f);

        //初始房间创建玩家标记
        var playerBirthMark = StartRoom.ActivityMarks.FirstOrDefault(mark => mark.Type == ActivityIdPrefix.ActivityPrefixType.Player);
        //创建玩家
        var player = ActivityObject.Create<Player>(ActivityIdPrefix.Role + "0001");
        if (playerBirthMark != null)
        {
            player.Position = playerBirthMark.Position;
        }
        player.Name = "Player";
        Player.SetCurrentPlayer(player);
        player.PutDown(RoomLayerEnum.YSortLayer);
        player.PickUpWeapon(ActivityObject.Create<Weapon>(ActivityIdPrefix.Weapon + "0001"));
        
        GameApplication.Instance.Cursor.SetGuiMode(false);
        yield return 0;
        
        //打开游戏中的ui
        var roomUi = UiManager.Open_RoomUI();
        roomUi.InitData(player);
        //派发进入地牢事件
        EventManager.EmitEvent(EventEnum.OnEnterDungeon);
        //关闭 loading UI
        UiManager.Dispose_Loading();
        
        IsInDungeon = true;
        yield return 0;
    }

    private IEnumerator RunExitDungeonCoroutine()
    {
        yield return 0;
        _world.Pause = true;
        yield return 0;
        //打开 loading UI
        UiManager.Open_Loading();
        _dungeonGenerator.EachRoom(DisposeRoomInfo);
        yield return 0;
        _dungeonTile = null;
        _autoTileConfig = null;
        _dungeonGenerator = null;
        _roomStaticNavigationList.Clear();
        _roomStaticNavigationList = null;
        
        UiManager.Hide_RoomUI();
        yield return new WaitForFixedProcess(10);
        Player.SetCurrentPlayer(null);
        _world = null;
        GameApplication.Instance.DestroyWorld();
        yield return new WaitForFixedProcess(10);
        //鼠标还原
        GameApplication.Instance.Cursor.SetGuiMode(false);
        //派发退出地牢事件
        EventManager.EmitEvent(EventEnum.OnExitDungeon);
        //关闭 loading UI
        UiManager.Dispose_Loading();
        yield return 0;
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
        _world.TileRoot.AddChild(navigationPolygon);
    }

    //创建门
    private void CreateDoor(RoomInfo roomInfo)
    {
        foreach (var doorInfo in roomInfo.Doors)
        {
            RoomDoor door;
            switch (doorInfo.Direction)
            {
                case DoorDirection.E:
                    door = ActivityObject.Create<RoomDoor>(ActivityIdPrefix.Other + "door_e");
                    door.Position = (doorInfo.OriginPosition + new Vector2(0.5f, 2)) * GameConfig.TileCellSize;
                    door.ZIndex = GameConfig.TopMapLayer;
                    break;
                case DoorDirection.W:
                    door = ActivityObject.Create<RoomDoor>(ActivityIdPrefix.Other + "door_w");
                    door.Position = (doorInfo.OriginPosition + new Vector2(-0.5f, 2)) * GameConfig.TileCellSize;
                    door.ZIndex = GameConfig.TopMapLayer;
                    break;
                case DoorDirection.S:
                    door = ActivityObject.Create<RoomDoor>(ActivityIdPrefix.Other + "door_s");
                    door.Position = (doorInfo.OriginPosition + new Vector2(2f, 1.5f)) * GameConfig.TileCellSize;
                    door.ZIndex = GameConfig.TopMapLayer;
                    break;
                case DoorDirection.N:
                    door = ActivityObject.Create<RoomDoor>(ActivityIdPrefix.Other + "door_n");
                    door.Position = (doorInfo.OriginPosition + new Vector2(2f, -0.5f)) * GameConfig.TileCellSize;
                    door.ZIndex = GameConfig.MiddleMapLayer;
                    break;
                default:
                    return;
            }
            doorInfo.Door = door;
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
        _world.AddChild(affiliation);
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
            if (activeRoom.IsSeclusion) //房间处于关上状态
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
    }

    /// <summary>
    /// 更新敌人视野
    /// </summary>
    private void UpdateEnemiesView()
    {
        _world.Enemy_IsFindTarget = false;
        _world.Enemy_FindTargetAffiliationSet.Clear();
        for (var i = 0; i < _world.Enemy_InstanceList.Count; i++)
        {
            var enemy = _world.Enemy_InstanceList[i];
            var state = enemy.StateController.CurrState;
            if (state == AiStateEnum.AiFollowUp || state == AiStateEnum.AiSurround) //目标在视野内
            {
                if (!_world.Enemy_IsFindTarget)
                {
                    _world.Enemy_IsFindTarget = true;
                    _world.EnemyFindTargetPosition = Player.Current.GetCenterPosition();
                    _world.Enemy_FindTargetAffiliationSet.Add(Player.Current.Affiliation);
                }
                _world.Enemy_FindTargetAffiliationSet.Add(enemy.Affiliation);
            }
        }
    }

    private void DisposeRoomInfo(RoomInfo roomInfo)
    {
        foreach (var activityMark in roomInfo.ActivityMarks)
        {
            activityMark.QueueFree();
        }
        roomInfo.ActivityMarks.Clear();
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
            // if (_dungeonGenerator != null)
            // {
            //     DrawRoomInfo(StartRoom);
            // }
            //绘制边缘线
            
        }
    }
    
    //绘制房间区域, debug 用
    private void DrawRoomInfo(RoomInfo room)
    {
        var cellSize = _world.TileRoot.CellQuadrantSize;
        var pos1 = (room.Position + room.Size / 2) * cellSize;
        
        //绘制下一个房间
        foreach (var nextRoom in room.Next)
        {
            var pos2 = (nextRoom.Position + nextRoom.Size / 2) * cellSize;
            DrawLine(pos1, pos2, Colors.Red);
            DrawRoomInfo(nextRoom);
        }

        DrawString(ResourceManager.DefaultFont16Px, pos1 - new Vector2I(0, 10), "Id: " + room.Id.ToString());
        DrawString(ResourceManager.DefaultFont16Px, pos1 + new Vector2I(0, 10), "Layer: " + room.Layer.ToString());

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