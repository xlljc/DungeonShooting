
using System.Collections.Generic;
using Godot;

public static class DungeonTileManager
{

    private static readonly List<Vector2I> FloorAtlasCoords = new List<Vector2I>(new[]
    {
        new Vector2I(0, 8),
    });

    private static readonly List<Vector2I> MiddleAtlasCoords = new List<Vector2I>(new[]
    {
        new Vector2I(1, 7),
        new Vector2I(2, 7),
        new Vector2I(3, 7),
    });
    
    public static void AutoFillRoomTile(TileMap tileMap, int floorLayer, int middleLayer, int topLayer,
        AutoTileConfig config,
        RoomInfo roomInfo)
    {
        foreach (var info in roomInfo.Next)
        {
            AutoFillRoomTile(tileMap, floorLayer, middleLayer, topLayer, config, info);
        }
        
        //铺房间
        if (roomInfo.RoomSplit == null)
        {
            FillRect(tileMap, floorLayer, config.Ground, roomInfo.Position + Vector2.One,
                roomInfo.Size - new Vector2(2, 2));

            FillRect(tileMap, topLayer, config.IN_LT, roomInfo.Position, Vector2.One);
            FillRect(tileMap, topLayer, config.L, roomInfo.Position + new Vector2(0, 1),
                new Vector2(1, roomInfo.Size.Y - 2));
            FillRect(tileMap, topLayer, config.IN_LB, roomInfo.Position + new Vector2(0, roomInfo.Size.Y - 1),
                new Vector2(1, 1));
            FillRect(tileMap, topLayer, config.B, roomInfo.Position + new Vector2(1, roomInfo.Size.Y - 1),
                new Vector2(roomInfo.Size.X - 2, 1));
            FillRect(tileMap, topLayer, config.IN_RB,
                roomInfo.Position + new Vector2(roomInfo.Size.X - 1, roomInfo.Size.Y - 1),
                Vector2.One);
            FillRect(tileMap, topLayer, config.R, roomInfo.Position + new Vector2(roomInfo.Size.X - 1, 1),
                new Vector2(1, roomInfo.Size.Y - 2));
            FillRect(tileMap, topLayer, config.IN_RT, roomInfo.Position + new Vector2(roomInfo.Size.X - 1, 0),
                Vector2.One);
            FillRect(tileMap, middleLayer, config.T, roomInfo.Position + Vector2.Right,
                new Vector2(roomInfo.Size.X - 2, 1));
        }
        else
        {
            var rectSize = roomInfo.RoomSplit.RoomInfo.Size;
            var rectPos = roomInfo.RoomSplit.RoomInfo.Position;
            var template = ResourceManager.Load<PackedScene>(roomInfo.RoomSplit.ScenePath);
            var tileInstance = template.Instantiate<DungeonRoomTemplate>();
            for (int i = 0; i < rectSize.X; i++)
            {
                for (int j = 0; j < rectSize.Y; j++)
                {
                    var atlasCoords =
                        tileInstance.GetCellAtlasCoords(0, new Vector2I((int)(rectPos.X + i), (int)(rectPos.Y + j)));

                    //判断层级
                    if (FloorAtlasCoords.Contains(atlasCoords))
                    {
                        tileMap.SetCell(floorLayer, new Vector2I(roomInfo.Position.X + i, roomInfo.Position.Y + j), 1,
                            atlasCoords);
                    }
                    else if (MiddleAtlasCoords.Contains(atlasCoords))
                    {
                        tileMap.SetCell(middleLayer, new Vector2I(roomInfo.Position.X + i, roomInfo.Position.Y + j), 1,
                            atlasCoords);
                    }
                    else
                    {
                        tileMap.SetCell(topLayer, new Vector2I(roomInfo.Position.X + i, roomInfo.Position.Y + j), 1,
                            atlasCoords);
                    }
                }
            }

            tileInstance.QueueFree();
        }

        //铺过道
        foreach (var doorInfo in roomInfo.Doors)
        {
            if (doorInfo.ConnectRoom.Id > roomInfo.Id)
            {
                //普通的直线连接
                var doorDir1 = doorInfo.Direction;
                var doorDir2 = doorInfo.ConnectDoor.Direction;
                if (!doorInfo.HasCross)
                {
                    //方向, 0横向, 1纵向
                    int dir = 0;
                    var rect = Utils.CalcRect(
                        doorInfo.OriginPosition.X,
                        doorInfo.OriginPosition.Y,
                        doorInfo.ConnectDoor.OriginPosition.X,
                        doorInfo.ConnectDoor.OriginPosition.Y
                    );
                    if (doorDir1 == DoorDirection.N || doorDir1 == DoorDirection.S)
                    {
                        rect.Size = new Vector2(GenerateDungeon.CorridorWidth, rect.Size.Y);
                        dir = 1;
                    }
                    else
                    {
                        rect.Size = new Vector2(rect.Size.X, GenerateDungeon.CorridorWidth);
                    }

                    if (dir == 0) //横向
                    {
                        FullHorizontalGalleryWall(tileMap, floorLayer, middleLayer, topLayer, config, rect);
                    }
                    else //纵向
                    {
                        FullVerticalGalleryWall(tileMap, floorLayer, middleLayer, topLayer, config, rect);
                    }
                }
                else //带交叉点
                {
                    //方向, 0横向, 1纵向
                    int dir1 = 0;
                    int dir2 = 0;

                    Rect2 rect;
                    Rect2 rect2;

                    //计算范围
                    switch (doorDir1)
                    {
                        case DoorDirection.E: //→
                            rect = new Rect2(
                                doorInfo.OriginPosition.X,
                                doorInfo.OriginPosition.Y,
                                doorInfo.Cross.X - doorInfo.OriginPosition.X,
                                GenerateDungeon.CorridorWidth
                            );
                            break;
                        case DoorDirection.W: //←
                            rect = new Rect2(
                                doorInfo.Cross.X + GenerateDungeon.CorridorWidth,
                                doorInfo.Cross.Y,
                                doorInfo.OriginPosition.X - (doorInfo.Cross.X + GenerateDungeon.CorridorWidth),
                                GenerateDungeon.CorridorWidth
                            );
                            break;
                        case DoorDirection.S: //↓
                            dir1 = 1;
                            rect = new Rect2(
                                doorInfo.OriginPosition.X,
                                doorInfo.OriginPosition.Y,
                                GenerateDungeon.CorridorWidth,
                                doorInfo.Cross.Y - doorInfo.OriginPosition.Y
                            );
                            break;
                        case DoorDirection.N: //↑
                            dir1 = 1;
                            rect = new Rect2(
                                doorInfo.Cross.X,
                                doorInfo.Cross.Y + GenerateDungeon.CorridorWidth,
                                GenerateDungeon.CorridorWidth,
                                doorInfo.OriginPosition.Y - (doorInfo.Cross.Y + GenerateDungeon.CorridorWidth)
                            );
                            break;
                        default:
                            rect = new Rect2();
                            break;
                    }

                    switch (doorDir2)
                    {
                        case DoorDirection.E: //→
                            rect2 = new Rect2(
                                doorInfo.ConnectDoor.OriginPosition.X,
                                doorInfo.ConnectDoor.OriginPosition.Y,
                                doorInfo.Cross.X - doorInfo.ConnectDoor.OriginPosition.X,
                                GenerateDungeon.CorridorWidth
                            );
                            break;
                        case DoorDirection.W: //←
                            rect2 = new Rect2(
                                doorInfo.Cross.X + GenerateDungeon.CorridorWidth,
                                doorInfo.Cross.Y,
                                doorInfo.ConnectDoor.OriginPosition.X -
                                (doorInfo.Cross.X + GenerateDungeon.CorridorWidth),
                                GenerateDungeon.CorridorWidth
                            );
                            break;
                        case DoorDirection.S: //↓
                            dir2 = 1;
                            rect2 = new Rect2(
                                doorInfo.ConnectDoor.OriginPosition.X,
                                doorInfo.ConnectDoor.OriginPosition.Y,
                                GenerateDungeon.CorridorWidth,
                                doorInfo.Cross.Y - doorInfo.ConnectDoor.OriginPosition.Y
                            );
                            break;
                        case DoorDirection.N: //↑
                            dir2 = 1;
                            rect2 = new Rect2(
                                doorInfo.Cross.X,
                                doorInfo.Cross.Y + GenerateDungeon.CorridorWidth,
                                GenerateDungeon.CorridorWidth,
                                doorInfo.ConnectDoor.OriginPosition.Y -
                                (doorInfo.Cross.Y + GenerateDungeon.CorridorWidth)
                            );
                            break;
                        default:
                            rect2 = new Rect2();
                            break;
                    }

                    FillRect(tileMap, floorLayer, config.Ground, doorInfo.Cross + Vector2.One,
                        new Vector2(GenerateDungeon.CorridorWidth - 2, GenerateDungeon.CorridorWidth - 2));

                    //墙壁
                    if (dir1 == 0)
                    {
                        FullHorizontalGalleryWall(tileMap, floorLayer, middleLayer, topLayer, config, rect);
                    }
                    else
                    {
                        FullVerticalGalleryWall(tileMap, floorLayer, middleLayer, topLayer, config, rect);
                    }

                    if (dir2 == 0)
                    {
                        FullHorizontalGalleryWall(tileMap, floorLayer, middleLayer, topLayer, config, rect2);
                    }
                    else
                    {
                        FullVerticalGalleryWall(tileMap, floorLayer, middleLayer, topLayer, config, rect2);
                    }

                    if ((doorDir1 == DoorDirection.N && doorDir2 == DoorDirection.E) || //↑→
                        (doorDir2 == DoorDirection.N && doorDir1 == DoorDirection.E))
                    {
                        FillRect(tileMap, topLayer, config.OUT_RT,
                            doorInfo.Cross + new Vector2(0, GenerateDungeon.CorridorWidth - 1),
                            Vector2.One);
                        FillRect(tileMap, topLayer, config.IN_RT, doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, 0),
                            Vector2.One);
                        FillRect(tileMap, middleLayer, config.T, doorInfo.Cross, new Vector2(GenerateDungeon.CorridorWidth - 1, 1));
                        FillRect(tileMap, topLayer, config.R, doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, 1),
                            new Vector2(1, GenerateDungeon.CorridorWidth - 1));
                    }
                    else if ((doorDir1 == DoorDirection.E && doorDir2 == DoorDirection.S) || //→↓
                             (doorDir2 == DoorDirection.E && doorDir1 == DoorDirection.S))
                    {
                        FillRect(tileMap, middleLayer, config.OUT_RB, doorInfo.Cross, Vector2.One);
                        FillRect(tileMap, topLayer, config.IN_RB,
                            doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1,
                                GenerateDungeon.CorridorWidth - 1),
                            Vector2.One);
                        FillRect(tileMap, topLayer, config.R, doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, 0),
                            new Vector2(1, GenerateDungeon.CorridorWidth - 1));
                        FillRect(tileMap, topLayer, config.B, doorInfo.Cross + new Vector2(0, GenerateDungeon.CorridorWidth - 1),
                            new Vector2(GenerateDungeon.CorridorWidth - 1, 1));
                    }
                    else if ((doorDir1 == DoorDirection.S && doorDir2 == DoorDirection.W) || //↓←
                             (doorDir2 == DoorDirection.S && doorDir1 == DoorDirection.W))
                    {
                        FillRect(tileMap, middleLayer, config.OUT_LB,
                            doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, 0), Vector2.One);
                        FillRect(tileMap, topLayer, config.IN_LB, doorInfo.Cross + new Vector2(0, GenerateDungeon.CorridorWidth - 1),
                            Vector2.One);
                        FillRect(tileMap, topLayer, config.L, doorInfo.Cross, new Vector2(1, GenerateDungeon.CorridorWidth - 1));
                        FillRect(tileMap, topLayer, config.B, doorInfo.Cross + new Vector2(1, GenerateDungeon.CorridorWidth - 1),
                            new Vector2(GenerateDungeon.CorridorWidth - 1, 1));
                    }
                    else if ((doorDir1 == DoorDirection.W && doorDir2 == DoorDirection.N) || //←↑
                             (doorDir2 == DoorDirection.W && doorDir1 == DoorDirection.N))
                    {
                        FillRect(tileMap, topLayer, config.OUT_LT,
                            doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1,
                                GenerateDungeon.CorridorWidth - 1),
                            Vector2.One);
                        FillRect(tileMap, topLayer, config.IN_LT, doorInfo.Cross, Vector2.One);
                        FillRect(tileMap, middleLayer, config.T, doorInfo.Cross + new Vector2(1, 0),
                            new Vector2(GenerateDungeon.CorridorWidth - 1, 1));
                        FillRect(tileMap, topLayer, config.L, doorInfo.Cross + new Vector2(0, 1),
                            new Vector2(1, GenerateDungeon.CorridorWidth - 1));
                    }

                    //在房间墙上开洞
                    switch (doorDir1)
                    {
                        case DoorDirection.E: //→
                            ClearRect(tileMap, topLayer, doorInfo.OriginPosition + new Vector2(-1, 1),
                                new Vector2(1, rect.Size.Y - 2));
                            FillRect(tileMap, floorLayer, config.Ground, doorInfo.OriginPosition + new Vector2(-1, 1),
                                new Vector2(1, rect.Size.Y - 2));
                            break;
                        case DoorDirection.W: //←
                            ClearRect(tileMap, topLayer, doorInfo.OriginPosition + new Vector2(0, 1),
                                new Vector2(1, rect.Size.Y - 2));
                            FillRect(tileMap, floorLayer, config.Ground, doorInfo.OriginPosition + new Vector2(0, 1),
                                new Vector2(1, rect.Size.Y - 2));
                            break;
                        case DoorDirection.S: //↓
                            ClearRect(tileMap, topLayer, doorInfo.OriginPosition + new Vector2(1, -1),
                                new Vector2(rect.Size.X - 2, 1));
                            FillRect(tileMap, floorLayer, config.Ground, doorInfo.OriginPosition + new Vector2(1, -1),
                                new Vector2(rect.Size.X - 2, 1));
                            break;
                        case DoorDirection.N: //↑
                            ClearRect(tileMap, middleLayer, doorInfo.OriginPosition + new Vector2(1, 2),
                                new Vector2(rect.Size.X - 2, 1));
                            FillRect(tileMap, floorLayer, config.Ground, doorInfo.OriginPosition + new Vector2(1, 0),
                                new Vector2(rect.Size.X - 2, 1));
                            break;
                    }

                    switch (doorDir2)
                    {
                        case DoorDirection.E: //→
                            ClearRect(tileMap, topLayer, doorInfo.ConnectDoor.OriginPosition + new Vector2(-1, 1),
                                new Vector2(1, rect2.Size.Y - 2));
                            FillRect(tileMap, floorLayer, config.Ground, doorInfo.ConnectDoor.OriginPosition + new Vector2(-1, 1),
                                new Vector2(1, rect2.Size.Y - 2));
                            break;
                        case DoorDirection.W: //←
                            ClearRect(tileMap, topLayer, doorInfo.ConnectDoor.OriginPosition + new Vector2(0, 1),
                                new Vector2(1, rect2.Size.Y - 2));
                            FillRect(tileMap, floorLayer, config.Ground, doorInfo.ConnectDoor.OriginPosition + new Vector2(0, 1),
                                new Vector2(1, rect2.Size.Y - 2));
                            break;
                        case DoorDirection.S: //↓
                            ClearRect(tileMap, topLayer, doorInfo.ConnectDoor.OriginPosition + new Vector2(1, -1),
                                new Vector2(rect2.Size.X - 2, 1));
                            FillRect(tileMap, floorLayer, config.Ground, doorInfo.ConnectDoor.OriginPosition + new Vector2(1, -1),
                                new Vector2(rect2.Size.X - 2, 1));
                            break;
                        case DoorDirection.N: //↑
                            ClearRect(tileMap, middleLayer, doorInfo.ConnectDoor.OriginPosition + new Vector2(1, 0),
                                new Vector2(rect2.Size.X - 2, 1));
                            FillRect(tileMap, floorLayer, config.Ground, doorInfo.ConnectDoor.OriginPosition + new Vector2(1, 0),
                                new Vector2(rect2.Size.X - 2, 1));
                            break;
                    }
                }
            }
        }
    }

    private static void FillRect(TileMap tileMap, int layer, TileCellInfo info, Vector2 pos, Vector2 size)
    {
        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                tileMap.SetCell(layer, new Vector2I((int)pos.X + i, (int)pos.Y + j), 1, info.AutotileCoord);
            }
        }
    }

    private static void ClearRect(TileMap tileMap, int layer, Vector2 pos, Vector2 size)
    {
        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                //tileMap.SetCell((int)pos.X + i, (int)pos.Y + j, -1);
                tileMap.SetCell(layer, new Vector2I((int)pos.X + i, (int)pos.Y + j), -1);
            }
        }
    }

    private static void FullHorizontalGalleryWall(TileMap tileMap, int floorLayer, int middleLayer, int topLayer,
        AutoTileConfig config, Rect2 rect)
    {
        FillRect(tileMap, floorLayer, config.Ground, rect.Position + new Vector2(0, 1), rect.Size - new Vector2(0, 2));
        FillRect(tileMap, middleLayer, config.T, rect.Position, new Vector2(rect.Size.X, 1));
        FillRect(tileMap, topLayer, config.B, rect.Position + new Vector2(0, rect.Size.Y - 1), new Vector2(rect.Size.X, 1));
        //左
        ClearRect(tileMap, topLayer, rect.Position + new Vector2(-1, 1), new Vector2(1, rect.Size.Y - 2));
        FillRect(tileMap, floorLayer, config.Ground, rect.Position + new Vector2(-1, 1), new Vector2(1, rect.Size.Y - 2));
        //右
        ClearRect(tileMap, topLayer, rect.Position + new Vector2(rect.Size.X, 1), new Vector2(1, rect.Size.Y - 2));
        FillRect(tileMap, floorLayer, config.Ground, rect.Position + new Vector2(rect.Size.X, 1), new Vector2(1, rect.Size.Y - 2));
    }

    private static void FullVerticalGalleryWall(TileMap tileMap, int floorLayer, int middleLayer, int topLayer,
        AutoTileConfig config, Rect2 rect)
    {
        FillRect(tileMap, floorLayer, config.Ground, rect.Position + new Vector2(1, 0), rect.Size - new Vector2(2, 0));
        FillRect(tileMap, topLayer, config.L, rect.Position, new Vector2(1, rect.Size.Y));
        FillRect(tileMap, topLayer, config.R, rect.Position + new Vector2(rect.Size.X - 1, 0), new Vector2(1, rect.Size.Y));
        //上
        ClearRect(tileMap, topLayer, rect.Position + new Vector2(1, -1), new Vector2(rect.Size.X - 2, 1));
        FillRect(tileMap, floorLayer, config.Ground, rect.Position + new Vector2(1, -1), new Vector2(rect.Size.X - 2, 1));
        //下
        ClearRect(tileMap, middleLayer, rect.Position + new Vector2(1, rect.Size.Y), new Vector2(rect.Size.X - 2, 1));
        FillRect(tileMap, floorLayer, config.Ground, rect.Position + new Vector2(1, rect.Size.Y), new Vector2(rect.Size.X - 2, 1));
    }

}