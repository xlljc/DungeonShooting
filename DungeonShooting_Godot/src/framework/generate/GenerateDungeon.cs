
using System.Collections.Generic;
using Godot;

/// <summary>
/// 地牢生成器
/// </summary>
public class GenerateDungeon
{
    public readonly TileMap TileMap;
    
    public GenerateDungeon(TileMap tileMap)
    {
        TileMap = tileMap;
    }

    public void Generate()
    {
        List<RoomInfo> roomInfos = new List<RoomInfo>();
        var x = 0;
        var y = 0;
        for (int i = 0; i < 10; i++)
        {
            var roomInfo = GenerateRoom();
            roomInfos.Add(roomInfo);
            roomInfo.Position = new Vector2(x + 2, y + 2);
            
            if (Utils.RandBoolean())
            {
                x = x + 2 + (int)roomInfo.Size.x;
            }
            else
            {
                y = y + 2 + (int)roomInfo.Size.y;
            }
            
        }

        foreach (var info in roomInfos)
        {
            //临时铺上地砖
            var id = (int)TileMap.TileSet.GetTilesIds()[0];
            for (int i = 0; i < info.Size.x; i++)
            {
                for (int j = 0; j < info.Size.y; j++)
                {
                    TileMap.SetCell(i + (int)info.Position.x, j + (int)info.Position.y, id);
                }
            }
        }
    }

    public RoomInfo GenerateRoom()
    {
        var info = new RoomInfo();
        //房间大小
        info.Size = new Vector2(Utils.RandRange(5, 10), Utils.RandRange(5, 10));
        

        return info;
    }
}