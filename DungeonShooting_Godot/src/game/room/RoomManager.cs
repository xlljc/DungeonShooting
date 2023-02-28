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
    /// 导航区域形状
    /// </summary>
    public NavigationRegion2D NavigationPolygon { get; private set; }


    private DungeonTile _dungeonTile;
    private AutoTileConfig _autoTileConfig;
    
    private Font _font;
    private GenerateDungeon _generateDungeon;

    public override void _EnterTree()
    {
        NavigationPolygon = new NavigationRegion2D();
        AddChild(NavigationPolygon);
        
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

        //生成地牢房间
        _generateDungeon = new GenerateDungeon();
        _generateDungeon.Generate();
        
        //填充地牢
        _autoTileConfig = new AutoTileConfig();
        _dungeonTile = new DungeonTile(TileRoot);
        _dungeonTile.AutoFillRoomTile(_autoTileConfig, _generateDungeon.StartRoom);

        //根据房间数据创建填充 tiled
        
        var nowTicks = DateTime.Now.Ticks;
        //生成寻路网格
        _dungeonTile.GenerateNavigationPolygon(NavigationPolygon);
        GD.Print("计算NavigationPolygon用时: " + (DateTime.Now.Ticks - nowTicks) / 10000 + "毫秒");

        //播放bgm
        SoundManager.PlayMusic(ResourcePath.resource_sound_bgm_Intro_ogg, -17f);

        Player.PickUpWeapon(WeaponManager.GetGun("1001"));
        // Player.PickUpWeapon(WeaponManager.GetGun("1002"));
        Player.PickUpWeapon(WeaponManager.GetGun("1004"));
        Player.PickUpWeapon(WeaponManager.GetGun("1003"));
        
        var enemy1 = new Enemy();
        enemy1.PutDown(new Vector2(150, 150), RoomLayerEnum.YSortLayer);
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
                //绘制ai寻路区域
                foreach (var item in _dungeonTile.GetPolygonData())
                {
                    if (item.Points.Count >= 2)
                    {
                        DrawPolyline(item.Points.Concat(new []{ item.Points[0] }).ToArray(), Colors.Red);
                    }
                }
            }
            //绘制房间区域
            DrawRoomInfo(_generateDungeon.StartRoom);
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