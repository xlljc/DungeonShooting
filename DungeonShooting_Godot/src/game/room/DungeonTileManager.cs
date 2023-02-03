
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
                if (!doorInfo.HasCross)
                {
                    var rect = Utils.CalcRect(
                        doorInfo.OriginPosition.x,
                        doorInfo.OriginPosition.y,
                        doorInfo.ConnectDoor.OriginPosition.x,
                        doorInfo.ConnectDoor.OriginPosition.y
                    );
                    if (doorInfo.Direction == DoorDirection.N || doorInfo.Direction == DoorDirection.S)
                    {
                        rect.Size = new Vector2(4, rect.Size.y);
                    }
                    else
                    {
                        rect.Size = new Vector2(rect.Size.x, 4);
                    }
                    FillRect(floor, 0, rect.Position, rect.Size);
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