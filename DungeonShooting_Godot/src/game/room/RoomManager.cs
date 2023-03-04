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
    
    private DungeonTile _dungeonTile;
    private AutoTileConfig _autoTileConfig;
    
    private Font _font;
    private GenerateDungeon _generateDungeon;

    //房间内所有静态导航网格数据
    private static List<NavigationPolygonData> _roomStaticNavigationList = new List<NavigationPolygonData>();

    public override void _EnterTree()
    {
        //创建玩家
        Player = new Player();
        Player.Position = new Vector2(30, 30);
        Player.Name = "Player";
        Player.PutDown(RoomLayerEnum.YSortLayer);
    }

    public override void _Ready()
    {
        TileRoot.YSortEnabled = false;
        //FloorTileMap.NavigationVisibilityMode = TileMap.VisibilityMode.ForceShow;
        
        _font = ResourceManager.Load<Font>(ResourcePath.resource_font_cn_font_36_tres);

        var nowTicks = DateTime.Now.Ticks;
        //生成地牢房间
        _generateDungeon = new GenerateDungeon();
        _generateDungeon.Generate();
        
        //填充地牢
        _autoTileConfig = new AutoTileConfig();
        _dungeonTile = new DungeonTile(TileRoot);
        _dungeonTile.AutoFillRoomTile(_autoTileConfig, _generateDungeon.StartRoom);
        
        //生成寻路网格， 这一步操作只生成过道的导航
        _dungeonTile.GenerateNavigationPolygon(DungeonTile.AisleFloorMapLayer);
        //挂载过道导航区域
        _dungeonTile.MountNavigationPolygon(this);
        _roomStaticNavigationList.AddRange(_dungeonTile.GetPolygonData());
        _roomStaticNavigationList.AddRange(_dungeonTile.GetConnectDoorPolygonData());
        //挂载所有房间的导航区域
        _generateDungeon.EachRoom(MountNavFromRoomInfo);
        
        GD.Print("生成地牢用时: " + (DateTime.Now.Ticks - nowTicks) / 10000 + "毫秒");

        //播放bgm
        SoundManager.PlayMusic(ResourcePath.resource_sound_bgm_Intro_ogg, -17f);

        Player.PickUpWeapon(WeaponManager.GetGun("1001"));
        // Player.PickUpWeapon(WeaponManager.GetGun("1002"));
        Player.PickUpWeapon(WeaponManager.GetGun("1004"));
        Player.PickUpWeapon(WeaponManager.GetGun("1003"));
        
        var enemy1 = new Enemy();
        enemy1.PutDown(new Vector2(160, 160), RoomLayerEnum.YSortLayer);
        enemy1.PickUpWeapon(WeaponManager.GetGun("1001"));
        
        // for (int i = 0; i < 10; i++)
        // {
        //     var enemyTemp = new Enemy();
        //     enemyTemp.PutDown(new Vector2(30 + (i + 1) * 20, 30), RoomLayerEnum.YSortLayer);
        //     // enemyTemp.PickUpWeapon(WeaponManager.GetGun("1003"));
        //     // enemyTemp.PickUpWeapon(WeaponManager.GetGun("1001"));
        // }

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

        // WeaponManager.GetGun("1004").PutDown(new Vector2(80, 100), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1001").PutDown(new Vector2(220, 120), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1001").PutDown(new Vector2(230, 120), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1001").PutDown(new Vector2(80, 80), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1002").PutDown(new Vector2(80, 120), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1003").PutDown(new Vector2(120, 80), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1003").PutDown(new Vector2(130, 80), RoomLayerEnum.NormalLayer);
        // WeaponManager.GetGun("1003").PutDown(new Vector2(140, 80), RoomLayerEnum.NormalLayer);
        
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
            if (_dungeonTile != null)
            {
                // DrawLine(new Vector2(0, -5000), new Vector2(0, 5000), Colors.Green);
                // DrawLine(new Vector2(-5000, 0), new Vector2(5000, 0), Colors.Green);
                
                //绘制ai寻路区域
                Utils.DrawNavigationPolygon(this, _roomStaticNavigationList.ToArray());
            }
            //绘制房间区域
            //DrawRoomInfo(_generateDungeon.StartRoom);
        }
    }

    private void MountNavFromRoomInfo(RoomInfo roomInfo)
    {
        var polygonArray = roomInfo.RoomSplit.RoomInfo.NavigationList.ToArray();
        var polygon = new NavigationPolygon();
        for (var i = 0; i < polygonArray.Length; i++)
        {
            var navigationPolygonData = polygonArray[i];
            var polygonPointArray = navigationPolygonData.ConvertPointsToVector2Array();
            //这里的位置需要加上房间位置
            for (var j = 0; j < polygonPointArray.Length; j++)
            {
                polygonPointArray[j] = polygonPointArray[j] + (roomInfo.Position + Vector2I.One) * GenerateDungeon.TileCellSize;
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