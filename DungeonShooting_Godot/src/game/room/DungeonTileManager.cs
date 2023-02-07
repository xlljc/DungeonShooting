
using Godot;

public static class DungeonTileManager
{
    public static void AutoFillRoomTile(TileMap floor, TileMap middle, TileMap top, AutoTileConfig config,
        RoomInfo roomInfo)
    {
        foreach (var info in roomInfo.Next)
        {
            AutoFillRoomTile(floor, middle, top, config, info);
        }

        //铺房间
        FillRect(floor, config.Ground, roomInfo.Position + Vector2.One, roomInfo.Size - new Vector2(2, 2));

         FillRect(top, config.IN_LT, roomInfo.Position, Vector2.One);
         FillRect(top, config.L, roomInfo.Position + new Vector2(0, 1), new Vector2(1, roomInfo.Size.Y - 2));
         FillRect(top, config.IN_LB, roomInfo.Position + new Vector2(0, roomInfo.Size.Y - 1), new Vector2(1, 1));
         FillRect(top, config.B, roomInfo.Position + new Vector2(1, roomInfo.Size.Y - 1),
             new Vector2(roomInfo.Size.X - 2, 1));
         FillRect(top, config.IN_RB, roomInfo.Position + new Vector2(roomInfo.Size.X - 1, roomInfo.Size.Y - 1),
             Vector2.One);
         FillRect(top, config.R, roomInfo.Position + new Vector2(roomInfo.Size.X - 1, 1),
             new Vector2(1, roomInfo.Size.Y - 2));
         FillRect(top, config.IN_RT, roomInfo.Position + new Vector2(roomInfo.Size.X - 1, 0), Vector2.One);
         FillRect(middle, config.T, roomInfo.Position + Vector2.Right, new Vector2(roomInfo.Size.X - 2, 1));

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
                        FullHorizontalGalleryWall(floor, middle, top, config, rect);
                    }
                    else //纵向
                    {
                        FullVerticalGalleryWall(floor, middle, top, config, rect);
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
                                doorInfo.ConnectDoor.OriginPosition.X - (doorInfo.Cross.X + GenerateDungeon.CorridorWidth),
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
                                doorInfo.ConnectDoor.OriginPosition.Y - (doorInfo.Cross.Y + GenerateDungeon.CorridorWidth)
                            );
                            break;
                        default:
                            rect2 = new Rect2();
                            break;
                    }

                    FillRect(floor, config.Ground, doorInfo.Cross + Vector2.One, 
                        new Vector2(GenerateDungeon.CorridorWidth - 2, GenerateDungeon.CorridorWidth - 2));

                    //墙壁
                    if (dir1 == 0)
                    {
                        FullHorizontalGalleryWall(floor, middle, top, config, rect);
                    }
                    else
                    {
                        FullVerticalGalleryWall(floor, middle, top, config, rect);
                    }
                    if (dir2 == 0)
                    {
                        FullHorizontalGalleryWall(floor, middle, top, config, rect2);
                    }
                    else
                    {
                        FullVerticalGalleryWall(floor, middle, top, config, rect2);
                    }

                    if ((doorDir1 == DoorDirection.N && doorDir2 == DoorDirection.E) || //↑→
                        (doorDir2 == DoorDirection.N && doorDir1 == DoorDirection.E))
                    {
                        FillRect(top, config.OUT_RT, 
                            doorInfo.Cross + new Vector2(0, GenerateDungeon.CorridorWidth - 1),
                            Vector2.One);
                        FillRect(top, config.IN_RT, doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, 0), Vector2.One);
                        FillRect(middle, config.T, doorInfo.Cross, new Vector2(GenerateDungeon.CorridorWidth - 1, 1));
                        FillRect(top, config.R, doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, 1),
                            new Vector2(1, GenerateDungeon.CorridorWidth - 1));
                    }
                    else if ((doorDir1 == DoorDirection.E && doorDir2 == DoorDirection.S) || //→↓
                             (doorDir2 == DoorDirection.E && doorDir1 == DoorDirection.S))
                    {
                        FillRect(middle, config.OUT_RB, doorInfo.Cross, Vector2.One);
                        FillRect(top, config.IN_RB, doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, GenerateDungeon.CorridorWidth - 1),
                            Vector2.One);
                        FillRect(top, config.R, doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, 0),
                            new Vector2(1, GenerateDungeon.CorridorWidth - 1));
                        FillRect(top, config.B, doorInfo.Cross + new Vector2(0, GenerateDungeon.CorridorWidth - 1),
                            new Vector2(GenerateDungeon.CorridorWidth - 1, 1));
                    }
                    else if ((doorDir1 == DoorDirection.S && doorDir2 == DoorDirection.W) || //↓←
                             (doorDir2 == DoorDirection.S && doorDir1 == DoorDirection.W))
                    {
                        FillRect(middle, config.OUT_LB,
                            doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, 0), Vector2.One);
                        FillRect(top, config.IN_LB, doorInfo.Cross + new Vector2(0, GenerateDungeon.CorridorWidth - 1),
                            Vector2.One);
                        FillRect(top, config.L, doorInfo.Cross, new Vector2(1, GenerateDungeon.CorridorWidth - 1));
                        FillRect(top, config.B, doorInfo.Cross + new Vector2(1, GenerateDungeon.CorridorWidth - 1),
                            new Vector2(GenerateDungeon.CorridorWidth - 1, 1));
                    }
                    else if ((doorDir1 == DoorDirection.W && doorDir2 == DoorDirection.N) || //←↑
                             (doorDir2 == DoorDirection.W && doorDir1 == DoorDirection.N))
                    {
                        FillRect(top, config.OUT_LT, 
                            doorInfo.Cross + new Vector2(GenerateDungeon.CorridorWidth - 1, GenerateDungeon.CorridorWidth - 1),
                            Vector2.One);
                        FillRect(top, config.IN_LT, doorInfo.Cross, Vector2.One);
                        FillRect(middle, config.T, doorInfo.Cross + new Vector2(1, 0),
                            new Vector2(GenerateDungeon.CorridorWidth - 1, 1));
                        FillRect(top, config.L, doorInfo.Cross + new Vector2(0, 1),
                            new Vector2(1, GenerateDungeon.CorridorWidth - 1));
                    }
                    
                    //在房间墙上开洞
                    switch (doorDir1)
                    {
                        case DoorDirection.E: //→
                            ClearRect(top, doorInfo.OriginPosition + new Vector2(-1, 1), new Vector2(1, rect.Size.Y - 2));
                            FillRect(floor, config.Ground, doorInfo.OriginPosition + new Vector2(-1, 1), new Vector2(1, rect.Size.Y - 2));
                            break;
                        case DoorDirection.W: //←
                            ClearRect(top, doorInfo.OriginPosition + new Vector2(0, 1), new Vector2(1, rect.Size.Y - 2));
                            FillRect(floor, config.Ground, doorInfo.OriginPosition + new Vector2(0, 1), new Vector2(1, rect.Size.Y - 2));
                            break;
                        case DoorDirection.S: //↓
                            ClearRect(top, doorInfo.OriginPosition + new Vector2(1, -1), new Vector2(rect.Size.X - 2, 1));
                            FillRect(floor, config.Ground, doorInfo.OriginPosition + new Vector2(1, -1), new Vector2(rect.Size.X - 2, 1));
                            break;
                        case DoorDirection.N: //↑
                            ClearRect(middle, doorInfo.OriginPosition + new Vector2(1, 2), new Vector2(rect.Size.X - 2, 1));
                            FillRect(floor, config.Ground, doorInfo.OriginPosition + new Vector2(1, 0), new Vector2(rect.Size.X - 2, 1));
                            break;
                    }
                    switch (doorDir2)
                    {
                        case DoorDirection.E: //→
                            ClearRect(top, doorInfo.ConnectDoor.OriginPosition + new Vector2(-1, 1), new Vector2(1, rect2.Size.Y - 2));
                            FillRect(floor, config.Ground, doorInfo.ConnectDoor.OriginPosition + new Vector2(-1, 1), new Vector2(1, rect2.Size.Y - 2));
                            break;
                        case DoorDirection.W: //←
                            ClearRect(top, doorInfo.ConnectDoor.OriginPosition + new Vector2(0, 1), new Vector2(1, rect2.Size.Y - 2));
                            FillRect(floor, config.Ground, doorInfo.ConnectDoor.OriginPosition + new Vector2(0, 1), new Vector2(1, rect2.Size.Y - 2));
                            break;
                        case DoorDirection.S: //↓
                            ClearRect(top, doorInfo.ConnectDoor.OriginPosition + new Vector2(1, -1), new Vector2(rect2.Size.X - 2, 1));
                            FillRect(floor, config.Ground, doorInfo.ConnectDoor.OriginPosition + new Vector2(1, -1), new Vector2(rect2.Size.X - 2, 1));
                            break;
                        case DoorDirection.N: //↑
                            ClearRect(middle, doorInfo.ConnectDoor.OriginPosition + new Vector2(1, 0), new Vector2(rect2.Size.X - 2, 1));
                            FillRect(floor, config.Ground, doorInfo.ConnectDoor.OriginPosition + new Vector2(1, 0), new Vector2(rect2.Size.X - 2, 1));
                            break;
                    }
                }
            }
        }
    }

    private static void FillRect(TileMap tileMap, TileCellInfo info, Vector2 pos, Vector2 size)
    {
        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                //tileMap.SetCell((int)pos.X + i, (int)pos.Y + j, info.Id, false, false, false, info.AutotileCoord);
            }
        }
    }

    private static void ClearRect(TileMap tileMap, Vector2 pos, Vector2 size)
    {
        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                //tileMap.SetCell((int)pos.X + i, (int)pos.Y + j, -1);
            }
        }
    }

    private static void FullHorizontalGalleryWall(TileMap floor, TileMap middle, TileMap top, AutoTileConfig config, Rect2 rect)
    {
        FillRect(floor, config.Ground, rect.Position + new Vector2(0, 1), rect.Size - new Vector2(0, 2));
        FillRect(middle, config.T, rect.Position, new Vector2(rect.Size.X, 1));
        FillRect(top, config.B, rect.Position + new Vector2(0, rect.Size.Y - 1), new Vector2(rect.Size.X, 1));
        //左
        ClearRect(top, rect.Position + new Vector2(-1, 1), new Vector2(1, rect.Size.Y - 2));
        FillRect(floor, config.Ground, rect.Position + new Vector2(-1, 1), new Vector2(1, rect.Size.Y - 2));
        //右
        ClearRect(top, rect.Position + new Vector2(rect.Size.X, 1), new Vector2(1, rect.Size.Y - 2));
        FillRect(floor, config.Ground, rect.Position + new Vector2(rect.Size.X, 1), new Vector2(1, rect.Size.Y - 2));
    }

    private static void FullVerticalGalleryWall(TileMap floor, TileMap middle, TileMap top, AutoTileConfig config, Rect2 rect)
    {
        FillRect(floor, config.Ground, rect.Position + new Vector2(1, 0), rect.Size - new Vector2(2, 0));
        FillRect(top, config.L, rect.Position, new Vector2(1, rect.Size.Y));
        FillRect(top, config.R, rect.Position + new Vector2(rect.Size.X - 1, 0), new Vector2(1, rect.Size.Y));
        //上
        ClearRect(top, rect.Position + new Vector2(1, -1), new Vector2(rect.Size.X - 2, 1));
        FillRect(floor, config.Ground, rect.Position + new Vector2(1, -1), new Vector2(rect.Size.X - 2, 1));
        //下
        ClearRect(middle, rect.Position + new Vector2(1, rect.Size.Y), new Vector2(rect.Size.X - 2, 1));
        FillRect(floor, config.Ground, rect.Position + new Vector2(1, rect.Size.Y), new Vector2(rect.Size.X - 2, 1));
    }

}