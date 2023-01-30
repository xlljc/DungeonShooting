
using System.Collections.Generic;
using Godot;

/// <summary>
/// 地牢生成器
/// </summary>
public class GenerateDungeon
{
    public readonly TileMap TileMap;

    public RoomInfo StartRoom;

    public readonly Grid<bool> RoomGrid = new Grid<bool>();

    private List<RoomInfo> _roomInfos = new List<RoomInfo>();

    private int _count = 0;

    //房间数量
    private int _maxCount = 15;

    /// <summary>
    /// 过道宽度
    /// </summary>
    public const int CorridorWidth = 4;

    public GenerateDungeon(TileMap tileMap)
    {
        TileMap = tileMap;
    }

    public void Generate()
    {
        //第一个房间
        StartRoom = GenerateRoom(null, 0);

        //如果房间数量不够, 就一直生成
        while (_count < _maxCount)
        {
            var room = Utils.RandChoose(_roomInfos);
            var nextRoom = GenerateRoom(room, Utils.RandRangeInt(0, 3));
            if (nextRoom != null)
            {
                room.Next.Add(nextRoom);

                //找门
                FindDoor(room, nextRoom);
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

    //生成房间
    private RoomInfo GenerateRoom(RoomInfo prevRoomInfo, int direction)
    {
        if (_count >= _maxCount)
        {
            return null;
        }

        var room = new RoomInfo(_count);
        room.Size = new Vector2(Utils.RandRangeInt(25, 60), Utils.RandRangeInt(25, 45));
        room.Position = Vector2.Zero;

        if (prevRoomInfo != null) //表示这不是第一个房间, 就得判断当前位置下的房间是否被遮挡
        {
            //房间间隔
            var space = Utils.RandRangeInt(6, 12);
            //中心偏移
            int offset;
            if (direction == 0 || direction == 2)
            {
                offset = Utils.RandRangeInt(-(int)(prevRoomInfo.Size.y * 0.7f), (int)(prevRoomInfo.Size.y * 1.5f));
            }
            else
            {
                offset = Utils.RandRangeInt(-(int)(prevRoomInfo.Size.x * 0.7f), (int)(prevRoomInfo.Size.x * 1.5f));
            }

            //计算房间位置
            if (direction == 0) //上
            {
                room.Position = new Vector2(prevRoomInfo.Position.x + offset,
                    prevRoomInfo.Position.y - room.Size.y - space);
            }
            else if (direction == 1) //右
            {
                room.Position = new Vector2(prevRoomInfo.Position.x + prevRoomInfo.Size.y + space,
                    prevRoomInfo.Position.y + offset);
            }
            else if (direction == 2) //下
            {
                room.Position = new Vector2(prevRoomInfo.Position.x + offset,
                    prevRoomInfo.Position.y + prevRoomInfo.Size.y + space);
            }
            else if (direction == 3) //左
            {
                room.Position = new Vector2(prevRoomInfo.Position.x - room.Size.x - space,
                    prevRoomInfo.Position.y + offset);
            }

            //是否碰到其他房间或者过道
            if (RoomGrid.RectCollision(room.Position - new Vector2(3, 3), room.Size + new Vector2(6, 6)))
            {
                return null;
            }
        }

        _count++;
        _roomInfos.Add(room);
        RoomGrid.AddRect(room.Position, room.Size, true);

        //下一个房间
        //0上, 1右, 2下, 3左
        var dirList = new List<int>(new[] { 0, 1, 2, 3 });
        if (prevRoomInfo != null)
        {
            dirList.Remove(GetReverseDirection(direction));
        }

        //这条线有一定概率不生成下一个房间
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
                FindDoor(room, nextRoom);
            }
        }

        return room;
    }

    /// <summary>
    /// 找两个房间的门
    /// </summary>
    private void FindDoor(RoomInfo room, RoomInfo nextRoom)
    {
        //门描述
        var roomDoor = new RoomDoor();
        var nextRoomDoor = new RoomDoor();
        roomDoor.ConnectRoom = nextRoom;
        nextRoomDoor.ConnectRoom = room;

        var overlapX = Mathf.Min(room.Position.x + room.Size.x, nextRoom.Position.x + nextRoom.Size.x) -
                       Mathf.Max(room.Position.x, nextRoom.Position.x);
        //这种情况下x轴有重叠
        if (overlapX >= 6)
        {
            GD.Print("----1: " + room.Id + ", " + nextRoom.Id);

            //找到重叠区域
            var range = CalcRange(room.Position.x, room.Position.x + room.Size.x,
                nextRoom.Position.x, nextRoom.Position.x + nextRoom.Size.x);
            var x = Utils.RandRangeInt((int)range.x, (int)range.y - CorridorWidth);

            if (room.Position.y < nextRoom.Position.y) //room在上, nextRoom在下
            {
                roomDoor.Direction = DoorDirection.S;
                nextRoomDoor.Direction = DoorDirection.N;
                roomDoor.OriginPosition = new Vector2(x, room.Position.y + room.Size.y);
                nextRoomDoor.OriginPosition = new Vector2(x, nextRoom.Position.y);

                //RoomGrid.AddRect(roomDoor.OriginPosition, new Vector2(4, nextRoomDoor.OriginPosition.y - roomDoor.OriginPosition.y), true);
            }
            else //room在下, nextRoom在上
            {
                roomDoor.Direction = DoorDirection.N;
                nextRoomDoor.Direction = DoorDirection.S;
                roomDoor.OriginPosition = new Vector2(x, room.Position.y);
                nextRoomDoor.OriginPosition = new Vector2(x, nextRoom.Position.y + nextRoom.Size.y);

                //RoomGrid.AddRect(nextRoomDoor.OriginPosition, new Vector2(4, roomDoor.OriginPosition.y - nextRoomDoor.OriginPosition.y), true);
            }

            //判断门之间的通道是否有物体碰到


            //没有撞到物体
            room.Doors.Add(roomDoor);
            nextRoom.Doors.Add(nextRoomDoor);
            AddToGridRange(roomDoor.OriginPosition, nextRoomDoor.OriginPosition);

            return;
        }

        var overlapY = Mathf.Min(room.Position.y + room.Size.y, nextRoom.Position.y + nextRoom.Size.y) -
                       Mathf.Max(room.Position.y, nextRoom.Position.y);
        //这种情况下y轴有重叠
        if (overlapY >= 6)
        {
            GD.Print("----2: " + room.Id + ", " + nextRoom.Id);

            //找到重叠区域
            var range = CalcRange(room.Position.y, room.Position.y + room.Size.y,
                nextRoom.Position.y, nextRoom.Position.y + nextRoom.Size.y);
            var y = Utils.RandRangeInt((int)range.x, (int)range.y - CorridorWidth);

            if (room.Position.x < nextRoom.Position.x) //room在左, nextRoom在右
            {
                roomDoor.Direction = DoorDirection.E;
                nextRoomDoor.Direction = DoorDirection.W;
                roomDoor.OriginPosition = new Vector2(room.Position.x + room.Size.x, y);
                nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x, y);

                //RoomGrid.AddRect(roomDoor.OriginPosition, new Vector2(nextRoomDoor.OriginPosition.x - roomDoor.OriginPosition.x, 4), true);
            }
            else //room在右, nextRoom在左
            {
                roomDoor.Direction = DoorDirection.W;
                nextRoomDoor.Direction = DoorDirection.E;
                roomDoor.OriginPosition = new Vector2(room.Position.x, y);
                nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + nextRoom.Size.x, y);

                //RoomGrid.AddRect(nextRoomDoor.OriginPosition, new Vector2(roomDoor.OriginPosition.x - nextRoomDoor.OriginPosition.x, 4), true);
            }

            //判断门之间的通道是否有物体碰到


            //没有撞到物体
            room.Doors.Add(roomDoor);
            nextRoom.Doors.Add(nextRoomDoor);
            AddToGridRange(roomDoor.OriginPosition, nextRoomDoor.OriginPosition);

            return;
        }


        var offset1 = Mathf.Max((int)overlapX + 2, 2);
        var offset2 = Mathf.Max((int)overlapY + 2, 2);
        GD.Print("----3: " + room.Id + ", " + nextRoom.Id + " --- tempX: " + overlapX + ", tempY: " + overlapY +
                 ", offset1: " + offset1 + ", offset2: " + offset2);

        //焦点
        Vector2 focus;

        //这种情况下x和y轴都没有重叠, 那么就只能生成拐角通道了
        if (room.Position.x > nextRoom.Position.x)
        {
            if (room.Position.y > nextRoom.Position.y)
            {
                if (Utils.RandBoolean())
                {
                    roomDoor.Direction = DoorDirection.N; //↑
                    nextRoomDoor.Direction = DoorDirection.E; //→

                    roomDoor.OriginPosition = new Vector2(room.Position.x + offset1, room.Position.y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + nextRoom.Size.x,
                        nextRoom.Position.y + nextRoom.Size.y - offset2 - 6);
                    focus = new Vector2(roomDoor.OriginPosition.x, nextRoomDoor.OriginPosition.y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.W; //←
                    nextRoomDoor.Direction = DoorDirection.S; //↓

                    roomDoor.OriginPosition = new Vector2(room.Position.x, room.Position.y + offset2);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + nextRoom.Size.x - offset1 - 6,
                        nextRoom.Position.y + nextRoom.Size.y);
                    focus = new Vector2(nextRoomDoor.OriginPosition.x, roomDoor.OriginPosition.y);
                }
            }
            else
            {
                if (Utils.RandBoolean())
                {
                    roomDoor.Direction = DoorDirection.S; //↓
                    nextRoomDoor.Direction = DoorDirection.E; //→

                    roomDoor.OriginPosition = new Vector2(room.Position.x + offset1, room.Position.y + room.Size.y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + nextRoom.Size.x,
                        nextRoom.Position.y + offset2);
                    focus = new Vector2(roomDoor.OriginPosition.x, nextRoomDoor.OriginPosition.y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.W; //←
                    nextRoomDoor.Direction = DoorDirection.N; //↑

                    roomDoor.OriginPosition =
                        new Vector2(room.Position.x, room.Position.y + room.Size.y - offset2 - 6); //
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + nextRoom.Size.x - offset2 - 6,
                        nextRoom.Position.y);
                    focus = new Vector2(nextRoomDoor.OriginPosition.x, roomDoor.OriginPosition.y);
                }
            }
        }
        else
        {
            if (room.Position.y > nextRoom.Position.y)
            {
                if (Utils.RandBoolean())
                {
                    roomDoor.Direction = DoorDirection.E; //→
                    nextRoomDoor.Direction = DoorDirection.S; //↓

                    roomDoor.OriginPosition = new Vector2(room.Position.x + room.Size.x, room.Position.y + offset2);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + offset1,
                        nextRoom.Position.y + nextRoom.Size.y);
                    focus = new Vector2(nextRoomDoor.OriginPosition.x, roomDoor.OriginPosition.y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.N; //↑
                    nextRoomDoor.Direction = DoorDirection.W; //←

                    roomDoor.OriginPosition = new Vector2(room.Position.x + room.Size.x - offset1 - 6, room.Position.y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x,
                        nextRoom.Position.y + nextRoom.Size.y - offset2 - 6);
                    focus = new Vector2(roomDoor.OriginPosition.x, nextRoomDoor.OriginPosition.y);
                }
            }
            else
            {
                if (Utils.RandBoolean())
                {
                    roomDoor.Direction = DoorDirection.E; //→
                    nextRoomDoor.Direction = DoorDirection.N; //↑

                    roomDoor.OriginPosition = new Vector2(room.Position.x + room.Size.x,
                        room.Position.y + room.Size.y - offset2 - 6);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + offset1, nextRoom.Position.y);
                    focus = new Vector2(nextRoomDoor.OriginPosition.x, roomDoor.OriginPosition.y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.S; //↓
                    nextRoomDoor.Direction = DoorDirection.W; //←

                    roomDoor.OriginPosition = new Vector2(room.Position.x + room.Size.x - offset1 - 6,
                        room.Position.y + room.Size.y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x, nextRoom.Position.y + offset2);
                    focus = new Vector2(roomDoor.OriginPosition.x, nextRoomDoor.OriginPosition.y);
                }
            }
        }

        roomDoor.HasFocus = true;
        roomDoor.Focus = focus;

        room.Doors.Add(roomDoor);
        nextRoom.Doors.Add(nextRoomDoor);
    }

    private Vector2 CalcRange(float start1, float end1, float start2, float end2)
    {
        return new Vector2(Mathf.Max(start1, start2), Mathf.Min(end1, end2));
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

    private void AddToGridRange(Vector2 point1, Vector2 point2)
    {
        RoomGrid.AddRect(
            new Vector2(Mathf.Min(point1.x, point2.x), Mathf.Min(point1.y, point2.y)),
            new Vector2(
                point1.x == point2.x ? CorridorWidth : Mathf.Abs(point1.x - point2.x),
                point1.y == point2.y ? CorridorWidth : Mathf.Abs(point1.y - point2.y)
            ),
            true
        );
    }
}