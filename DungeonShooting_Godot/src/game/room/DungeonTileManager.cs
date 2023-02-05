
using System;
using Godot;

public static class DungeonTileManager
{
    public static void AutoFillRoomTile(TileMap floor, TileMap middle, TileMap top, RoomInfo roomInfo)
    {
        foreach (var info in roomInfo.Next)
        {
            AutoFillRoomTile(floor, middle, top, info);
        }

        //铺房间
        FillRect(floor, 0, roomInfo.Position, roomInfo.Size);
        //铺过道
        foreach (var doorInfo in roomInfo.Doors)
        {
            if (doorInfo.ConnectRoom.Id < roomInfo.Id)
            {
                //普通的直线连接
                if (!doorInfo.HasCross)
                {
                    //方向, 0横向, 1纵向
                    int dir = 0;
                    var rect = Utils.CalcRect(
                        doorInfo.OriginPosition.x,
                        doorInfo.OriginPosition.y,
                        doorInfo.ConnectDoor.OriginPosition.x,
                        doorInfo.ConnectDoor.OriginPosition.y
                    );
                    if (doorInfo.Direction == DoorDirection.N || doorInfo.Direction == DoorDirection.S)
                    {
                        rect.Size = new Vector2(GenerateDungeon.CorridorWidth, rect.Size.y);
                        dir = 1;
                    }
                    else
                    {
                        rect.Size = new Vector2(rect.Size.x, GenerateDungeon.CorridorWidth);
                    }

                    FillRect(floor, 0, rect.Position, rect.Size);
                }
                else //带交叉点
                {
                    //方向, 0横向, 1纵向
                    int dir1 = 0;
                    int dir2 = 0;

                    Rect2 rect;
                    Rect2 rect2;

                    switch (doorInfo.Direction)
                    {
                        case DoorDirection.E: //→
                            rect = new Rect2(
                                doorInfo.OriginPosition.x,
                                doorInfo.OriginPosition.y,
                                doorInfo.Cross.x - doorInfo.OriginPosition.x,
                                GenerateDungeon.CorridorWidth
                            );
                            break;
                        case DoorDirection.W: //←
                            rect = new Rect2(
                                doorInfo.Cross.x + GenerateDungeon.CorridorWidth,
                                doorInfo.Cross.y,
                                doorInfo.OriginPosition.x - (doorInfo.Cross.x + GenerateDungeon.CorridorWidth),
                                GenerateDungeon.CorridorWidth
                            );
                            break;
                        case DoorDirection.S: //↓
                            dir1 = 1;
                            rect = new Rect2(
                                doorInfo.OriginPosition.x,
                                doorInfo.OriginPosition.y,
                                GenerateDungeon.CorridorWidth,
                                doorInfo.Cross.y - doorInfo.OriginPosition.y
                            );
                            break;
                        case DoorDirection.N: //↑
                            dir1 = 1;
                            rect = new Rect2(
                                doorInfo.Cross.x,
                                doorInfo.Cross.y + GenerateDungeon.CorridorWidth,
                                GenerateDungeon.CorridorWidth,
                                doorInfo.OriginPosition.y - doorInfo.Cross.y
                            );
                            break;
                        default:
                            rect = new Rect2();
                            break;
                    }


                    switch (doorInfo.ConnectDoor.Direction)
                    {
                        case DoorDirection.E: //→
                            rect2 = new Rect2(
                                doorInfo.ConnectDoor.OriginPosition.x,
                                doorInfo.ConnectDoor.OriginPosition.y,
                                doorInfo.Cross.x - doorInfo.ConnectDoor.OriginPosition.x,
                                GenerateDungeon.CorridorWidth
                            );
                            break;
                        case DoorDirection.W: //←
                            rect2 = new Rect2(
                                doorInfo.Cross.x + GenerateDungeon.CorridorWidth,
                                doorInfo.Cross.y,
                                doorInfo.ConnectDoor.OriginPosition.x -
                                (doorInfo.Cross.x + GenerateDungeon.CorridorWidth),
                                GenerateDungeon.CorridorWidth
                            );
                            break;
                        case DoorDirection.S: //↓
                            dir2 = 1;
                            rect2 = new Rect2(
                                doorInfo.ConnectDoor.OriginPosition.x,
                                doorInfo.ConnectDoor.OriginPosition.y,
                                GenerateDungeon.CorridorWidth,
                                doorInfo.Cross.y - doorInfo.ConnectDoor.OriginPosition.y
                            );
                            break;
                        case DoorDirection.N: //↑
                            dir2 = 1;
                            rect2 = new Rect2(
                                doorInfo.Cross.x,
                                doorInfo.Cross.y + GenerateDungeon.CorridorWidth,
                                GenerateDungeon.CorridorWidth,
                                doorInfo.ConnectDoor.OriginPosition.y - doorInfo.Cross.y
                            );
                            break;
                        default:
                            rect2 = new Rect2();
                            break;
                    }

                    FillRect(floor, 0, rect.Position, rect.Size);
                    FillRect(floor, 0, rect2.Position, rect2.Size);
                    FillRect(floor, 0, doorInfo.Cross, new Vector2(GenerateDungeon.CorridorWidth, GenerateDungeon.CorridorWidth));
                }
            }
        }
    }

    private static void FillRect(TileMap tileMap, int index, Vector2 pos, Vector2 size)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                tileMap.SetCell((int)pos.x + i, (int)pos.y + j, index, false, false, false, new Vector2(0, 8));
            }
        }
    }
}