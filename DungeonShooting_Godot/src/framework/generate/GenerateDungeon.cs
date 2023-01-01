
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
            var room = Utils.RandChoose(_roomInfos);
            var nextRoom = GenerateRoom(room, Utils.RandRangeInt(0, 3));
            if (nextRoom != null)
            {
                room.Next.Add(nextRoom);
                
                //找门
                if (Mathf.Max(room.Position.x, nextRoom.Position.x) <= Mathf.Min(room.Position.x + room.Size.x, nextRoom.Position.x + nextRoom.Size.x)) //x轴
                {
                    GD.Print("----1: " + room.Id + ", " + nextRoom.Id + ", = " + (Mathf.Min(room.Position.x + room.Size.x, nextRoom.Position.x + nextRoom.Size.x) - Mathf.Max(room.Position.x, nextRoom.Position.x)));
                }
                else if (Mathf.Max(room.Position.y, nextRoom.Position.y) <= Mathf.Min(room.Position.y + room.Size.y, nextRoom.Position.y + nextRoom.Size.y)) //y轴
                {
                    GD.Print("----2: " + room.Id + ", " + nextRoom.Id + ", = " + (Mathf.Min(room.Position.y + room.Size.y, nextRoom.Position.y + nextRoom.Size.y) - Mathf.Max(room.Position.y, nextRoom.Position.y)));
                }
                else
                {
                    GD.Print("----3: " + room.Id + ", " + nextRoom.Id);
                }
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
        var room = new RoomInfo(_count);
        room.Size = new Vector2(Utils.RandRangeInt(25, 60), Utils.RandRangeInt(25, 45));
        room.Position = Vector2.Zero;
        room.Direction = direction;

        if (prevRoomInfo != null) //表示这不是第一个房间, 就得判断当前位置下的房间是否被遮挡
        {
            //房间间隔
            var space = Utils.RandRangeInt(6, 12);
            //中心偏移
            int offset;
            if (direction == 0 || direction == 2)
            {
                offset = Utils.RandRangeInt(-(int)(prevRoomInfo.Size.y * 0.7f), (int)(prevRoomInfo.Size.y * 0.7f));
            }
            else
            {
                offset = Utils.RandRangeInt(-(int)(prevRoomInfo.Size.x * 0.7f), (int)(prevRoomInfo.Size.x * 0.7f));
            }
            //计算房间位置
            if (direction == 0) //上
            {
                room.Position = new Vector2(prevRoomInfo.Position.x + offset,
                    prevRoomInfo.Position.y - room.Size.y - space);
            }
            else if (direction == 1) //右
            {
                room.Position = new Vector2(prevRoomInfo.Position.x + prevRoomInfo.Size.y + space, prevRoomInfo.Position.y + offset);
            }
            else if (direction == 2) //下
            {
                room.Position = new Vector2(prevRoomInfo.Position.x + offset, prevRoomInfo.Position.y + prevRoomInfo.Size.y + space);
            }
            else if (direction == 3) //左
            {
                room.Position = new Vector2(prevRoomInfo.Position.x - room.Size.x - space,
                    prevRoomInfo.Position.y + offset);
            }
            
            //是否碰到其他房间
            if (_roomGrid.RectCollision(room.Position - new Vector2(2, 2), room.Size + new Vector2(4, 4)))
            {
                return null;
            }
        }

        _count++;
        _roomInfos.Add(room);
        _roomGrid.AddRect(room.Position, room.Size, true);
        
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
                var nextRoom = GenerateRoom(room, randDir);
                if (nextRoom == null)
                {
                    break;
                }

                nextRoom.Prev = room;
                room.Next.Add(nextRoom);

                dirList.Remove(randDir);
                
                //找门
                if (Mathf.Max(room.Position.x, nextRoom.Position.x) <= Mathf.Min(room.Position.x + room.Size.x, nextRoom.Position.x + nextRoom.Size.x)) //x轴
                {
                    GD.Print("----1: " + room.Id + ", " + nextRoom.Id + ", = " + (Mathf.Min(room.Position.x + room.Size.x, nextRoom.Position.x + nextRoom.Size.x) - Mathf.Max(room.Position.x, nextRoom.Position.x)));
                }
                else if (Mathf.Max(room.Position.y, nextRoom.Position.y) <= Mathf.Min(room.Position.y + room.Size.y, nextRoom.Position.y + nextRoom.Size.y)) //y轴
                {
                    GD.Print("----2: " + room.Id + ", " + nextRoom.Id + ", = " + (Mathf.Min(room.Position.y + room.Size.y, nextRoom.Position.y + nextRoom.Size.y) - Mathf.Max(room.Position.y, nextRoom.Position.y)));
                }
                else
                {
                    GD.Print("----3: " + room.Id + ", " + nextRoom.Id);
                }
            }
        }

        return room;
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