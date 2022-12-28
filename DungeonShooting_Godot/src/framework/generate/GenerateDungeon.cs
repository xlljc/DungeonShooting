
using System.Collections.Generic;
using Godot;

/// <summary>
/// 地牢生成器
/// </summary>
public class GenerateDungeon
{
    public readonly TileMap TileMap;

    private Grid<bool> _roomGrid = new Grid<bool>();
    private List<RoomInfo> _roomInfos = new List<RoomInfo>();
    private int _count = 0;
    private int _maxCount = 3;

    public GenerateDungeon(TileMap tileMap)
    {
        TileMap = tileMap;
    }

    public void Generate()
    {
        GenerateRoom(null);

        foreach (var info in _roomInfos)
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

    public void GenerateRoom(RoomInfo prevRoomInfo)
    {
        if (_count >= _maxCount)
        {
            return;
        }
        
        var pos = Vector2.Zero;
        var size = new Vector2(Utils.RandRangeInt(5, 10), Utils.RandRangeInt(5, 10));

        if (prevRoomInfo != null) //表示这不是第一个房间, 就得判断当前位置下的房间是否被遮挡
        {
            var dir = Utils.RandRangeInt(0, 3); //0上, 1右, 2下, 3左
            var space = Utils.RandRangeInt(3, 10);
            int offset;
            if (dir == 0 || dir == 2)
            {
                offset = Utils.RandRangeInt(-(int)prevRoomInfo.Size.y, (int)prevRoomInfo.Size.y);
            }
            else
            {
                offset = Utils.RandRangeInt(-(int)prevRoomInfo.Size.x, (int)prevRoomInfo.Size.x);
            }

            if (dir == 0)
            {
                pos = new Vector2(prevRoomInfo.Position.x + offset,
                    prevRoomInfo.Position.y - prevRoomInfo.Size.y - space);
            }
            else if (dir == 1)
            {
                pos = new Vector2(prevRoomInfo.Position.x + prevRoomInfo.Size.y + space, prevRoomInfo.Position.y + offset - space);
            }
            else if (dir == 2)
            {
                pos = new Vector2(prevRoomInfo.Position.x + offset, prevRoomInfo.Position.y + space);
            }
            else if (dir == 3)
            {
                pos = new Vector2(prevRoomInfo.Position.x - prevRoomInfo.Size.x - space,
                    prevRoomInfo.Position.y + prevRoomInfo.Size.y + offset);
            }

            if (_roomGrid.RectCollision(pos, size))
            {
                return;
            }
        }


        _count++;
        var info = new RoomInfo();
        info.Size = size;
        info.Position = pos;
        _roomInfos.Add(info);
        _roomGrid.AddRect(info.Position, info.Size, true);
        GenerateRoom(info);
    }
}