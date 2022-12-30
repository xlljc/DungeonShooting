
using System.Collections.Generic;
using Godot;

/// <summary>
/// 地牢生成器
/// </summary>
public class GenerateDungeon
{
    public readonly TileMap TileMap;

    public RoomInfo StartRoom;

    private Grid<bool> _roomGrid = new Grid<bool>();
    private List<RoomInfo> _roomInfos = new List<RoomInfo>();
    private int _count = 0;
    private int _maxCount = 15;

    public GenerateDungeon(TileMap tileMap)
    {
        TileMap = tileMap;
    }

    public void Generate()
    {
        StartRoom = GenerateRoom(null, 0);

        while (_count < _maxCount)
        {
            var info = Utils.RandChoose(_roomInfos);
            var nextInfo = GenerateRoom(info, Utils.RandRangeInt(0, 3));
            if (nextInfo != null)
            {
                info.Next.Add(nextInfo);
            }
        }

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

    private RoomInfo GenerateRoom(RoomInfo prevRoomInfo, int direction)
    {
        if (_count >= _maxCount)
        {
            return null;
        }
        var info = new RoomInfo(_count);
        info.Size = new Vector2(Utils.RandRangeInt(10, 30), Utils.RandRangeInt(10, 30));
        info.Position = Vector2.Zero;
        info.Direction = direction;

        if (prevRoomInfo != null) //表示这不是第一个房间, 就得判断当前位置下的房间是否被遮挡
        {
            //房间间隔
            var space = Utils.RandRangeInt(3, 4);
            //中心偏移
            int offset;
            if (direction == 0 || direction == 2)
            {
                offset = Utils.RandRangeInt(-(int)prevRoomInfo.Size.y, (int)prevRoomInfo.Size.y);
            }
            else
            {
                offset = Utils.RandRangeInt(-(int)prevRoomInfo.Size.x, (int)prevRoomInfo.Size.x);
            }
            //计算房间位置
            if (direction == 0) //上
            {
                info.Position = new Vector2(prevRoomInfo.Position.x + offset,
                    prevRoomInfo.Position.y - info.Size.y - space);
            }
            else if (direction == 1) //右
            {
                info.Position = new Vector2(prevRoomInfo.Position.x + prevRoomInfo.Size.y + space, prevRoomInfo.Position.y + offset);
            }
            else if (direction == 2) //下
            {
                info.Position = new Vector2(prevRoomInfo.Position.x + offset, prevRoomInfo.Position.y + prevRoomInfo.Size.y + space);
            }
            else if (direction == 3) //左
            {
                info.Position = new Vector2(prevRoomInfo.Position.x - info.Size.x - space,
                    prevRoomInfo.Position.y + offset);
            }
            
            //是否碰到其他房间
            if (_roomGrid.RectCollision(info.Position - new Vector2(1, 1), info.Size + new Vector2(2, 2)))
            {
                return null;
            }
        }

        _count++;
        _roomInfos.Add(info);
        _roomGrid.AddRect(info.Position, info.Size, true);
        
        //下一个房间
        //0上, 1右, 2下, 3左
        var dirList = new List<int>(new []{ 0, 1, 2, 3 });
        if (prevRoomInfo != null)
        {
            dirList.Remove(GetReverseDirection(direction));
        }

        if (Utils.RandRangeInt(0, 2) != 0)
        {
            while (dirList.Count > 0)
            {
                var randDir = Utils.RandChoose(dirList);
                var generateRoom = GenerateRoom(info, randDir);
                if (generateRoom == null)
                {
                    break;
                }

                generateRoom.Prev = info;
                info.Next.Add(generateRoom);

                dirList.Remove(randDir);
            }
        }

        return info;
    }

    private int GetReverseDirection(int direction)
    {
        switch (direction)
        {
            case 0: return 2;
            case 1: return 3;
            case 2: return 0;
            case 3: return 1;
        }

        return 2;
    }
}