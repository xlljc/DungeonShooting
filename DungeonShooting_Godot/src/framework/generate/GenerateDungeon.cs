
using System.Collections.Generic;
using Godot;

/// <summary>
/// 地牢生成器
/// </summary>
public class GenerateDungeon
{
    /// <summary>
    /// 所有生成的房间, 调用过 Generate() 函数才能获取到值
    /// </summary>
    public List<RoomInfo> RoomInfos { get; } = new List<RoomInfo>();

    /// <summary>
    /// 起始房间
    /// </summary>
    public RoomInfo StartRoom { get; private set; }
    
    /// <summary>
    /// 生成的房间数量
    /// </summary>
    private int _maxCount = 15;

    /// <summary>
    /// 过道宽度
    /// </summary>
    private int _corridorWidth = 4;
    
    //用于标记地图上的坐标是否被占用
    private Grid<bool> _roomGrid { get; } = new Grid<bool>();
    
    //当前房间数量
    private int _count = 0;
    
    //宽高
    private int _roomMinWidth = 15;
    private int _roomMaxWidth = 35;
    private int _roomMinHeight = 10;
    private int _roomMaxHeight = 25;

    //间隔
    private int _roomMinInterval = 6;
    private int _roomMaxInterval = 10;

    //房间横轴分散程度
    private float _roomHorizontalMinDispersion = 0.7f;
    private float _roomHorizontalMaxDispersion = 1.1f;

    //房间纵轴分散程度
    private float _roomVerticalMinDispersion = 0.7f;
    private float _roomVerticalMaxDispersion = 1.1f;

    /// <summary>
    /// 生成房间
    /// </summary>
    public void Generate()
    {
        if (StartRoom != null) return;

        //第一个房间
        StartRoom = GenerateRoom(null, 0);

        //如果房间数量不够, 就一直生成
        while (_count < _maxCount)
        {
            var room = Utils.RandChoose(RoomInfos);
            var nextRoom = GenerateRoom(room, Utils.RandRangeInt(0, 3));
            if (nextRoom != null)
            {
                room.Next.Add(nextRoom);
            }
        }
        
        _roomGrid.Clear();
    }

    //生成房间
    private RoomInfo GenerateRoom(RoomInfo prevRoomInfo, int direction)
    {
        if (_count >= _maxCount)
        {
            return null;
        }

        var room = new RoomInfo(_count);
        //房间大小
        room.Size = new Vector2(Utils.RandRangeInt(_roomMinWidth, _roomMaxWidth),
            Utils.RandRangeInt(_roomMinHeight, _roomMaxHeight));

        if (prevRoomInfo != null) //表示这不是第一个房间, 就得判断当前位置下的房间是否被遮挡
        {
            //房间间隔
            var space = Utils.RandRangeInt(_roomMinInterval, _roomMaxInterval);
            //中心偏移
            int offset;
            if (direction == 0 || direction == 2)
            {
                offset = Utils.RandRangeInt(-(int)(prevRoomInfo.Size.x * _roomVerticalMinDispersion),
                    (int)(prevRoomInfo.Size.x * _roomVerticalMaxDispersion));
            }
            else
            {
                offset = Utils.RandRangeInt(-(int)(prevRoomInfo.Size.y * _roomHorizontalMinDispersion),
                    (int)(prevRoomInfo.Size.y * _roomHorizontalMaxDispersion));
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
            if (_roomGrid.RectCollision(room.Position - new Vector2(3, 3), room.Size + new Vector2(6, 6)))
            {
                return null;
            }

            _roomGrid.AddRect(room.Position, room.Size, true);

            //找门, 与上一个房间是否能连通
            if (!ConnectDoor(prevRoomInfo, room))
            {
                _roomGrid.RemoveRect(room.Position, room.Size);
                return null;
            }
        }

        _count++;
        RoomInfos.Add(room);
        if (prevRoomInfo == null)
        {
            _roomGrid.AddRect(room.Position, room.Size, true);
        }

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
            }
        }

        return room;
    }

    /// <summary>
    /// 找两个房间的门
    /// </summary>
    private bool ConnectDoor(RoomInfo room, RoomInfo nextRoom)
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
            //找到重叠区域
            var range = CalcRange(room.Position.x, room.Position.x + room.Size.x,
                nextRoom.Position.x, nextRoom.Position.x + nextRoom.Size.x);
            var x = Utils.RandRangeInt((int)range.x + 1, (int)range.y - _corridorWidth - 1);

            if (room.Position.y < nextRoom.Position.y) //room在上, nextRoom在下
            {
                roomDoor.Direction = DoorDirection.S;
                nextRoomDoor.Direction = DoorDirection.N;
                roomDoor.OriginPosition = new Vector2(x, room.Position.y + room.Size.y);
                nextRoomDoor.OriginPosition = new Vector2(x, nextRoom.Position.y);
            }
            else //room在下, nextRoom在上
            {
                roomDoor.Direction = DoorDirection.N;
                nextRoomDoor.Direction = DoorDirection.S;
                roomDoor.OriginPosition = new Vector2(x, room.Position.y);
                nextRoomDoor.OriginPosition = new Vector2(x, nextRoom.Position.y + nextRoom.Size.y);
            }

            //判断门之间的通道是否有物体碰到
            if (!AddCorridorToGridRange(roomDoor, nextRoomDoor))
            {
                //此门不能连通
                return false;
            }

            //没有撞到物体
            room.Doors.Add(roomDoor);
            nextRoom.Doors.Add(nextRoomDoor);
            return true;
        }

        var overlapY = Mathf.Min(room.Position.y + room.Size.y, nextRoom.Position.y + nextRoom.Size.y) -
                       Mathf.Max(room.Position.y, nextRoom.Position.y);
        //这种情况下y轴有重叠
        if (overlapY >= 6)
        {
            //找到重叠区域
            var range = CalcRange(room.Position.y, room.Position.y + room.Size.y,
                nextRoom.Position.y, nextRoom.Position.y + nextRoom.Size.y);
            var y = Utils.RandRangeInt((int)range.x + 1, (int)range.y - _corridorWidth - 1);

            if (room.Position.x < nextRoom.Position.x) //room在左, nextRoom在右
            {
                roomDoor.Direction = DoorDirection.E;
                nextRoomDoor.Direction = DoorDirection.W;
                roomDoor.OriginPosition = new Vector2(room.Position.x + room.Size.x, y);
                nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x, y);
            }
            else //room在右, nextRoom在左
            {
                roomDoor.Direction = DoorDirection.W;
                nextRoomDoor.Direction = DoorDirection.E;
                roomDoor.OriginPosition = new Vector2(room.Position.x, y);
                nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + nextRoom.Size.x, y);
            }

            //判断门之间的通道是否有物体碰到
            if (!AddCorridorToGridRange(roomDoor, nextRoomDoor))
            {
                //此门不能连通
                return false;
            }

            //没有撞到物体
            room.Doors.Add(roomDoor);
            nextRoom.Doors.Add(nextRoomDoor);
            return true;
        }


        var offset1 = Mathf.Max((int)overlapX + 2, 2);
        var offset2 = Mathf.Max((int)overlapY + 2, 2);

        //焦点
        Vector2 cross;

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
                    cross = new Vector2(roomDoor.OriginPosition.x, nextRoomDoor.OriginPosition.y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.W; //←
                    nextRoomDoor.Direction = DoorDirection.S; //↓

                    roomDoor.OriginPosition = new Vector2(room.Position.x, room.Position.y + offset2);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + nextRoom.Size.x - offset1 - 6,
                        nextRoom.Position.y + nextRoom.Size.y);
                    cross = new Vector2(nextRoomDoor.OriginPosition.x, roomDoor.OriginPosition.y);
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
                    cross = new Vector2(roomDoor.OriginPosition.x, nextRoomDoor.OriginPosition.y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.W; //←
                    nextRoomDoor.Direction = DoorDirection.N; //↑

                    roomDoor.OriginPosition =
                        new Vector2(room.Position.x, room.Position.y + room.Size.y - offset2 - 6); //
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x + nextRoom.Size.x - offset2 - 6,
                        nextRoom.Position.y);
                    cross = new Vector2(nextRoomDoor.OriginPosition.x, roomDoor.OriginPosition.y);
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
                    cross = new Vector2(nextRoomDoor.OriginPosition.x, roomDoor.OriginPosition.y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.N; //↑
                    nextRoomDoor.Direction = DoorDirection.W; //←

                    roomDoor.OriginPosition = new Vector2(room.Position.x + room.Size.x - offset1 - 6, room.Position.y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x,
                        nextRoom.Position.y + nextRoom.Size.y - offset2 - 6);
                    cross = new Vector2(roomDoor.OriginPosition.x, nextRoomDoor.OriginPosition.y);
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
                    cross = new Vector2(nextRoomDoor.OriginPosition.x, roomDoor.OriginPosition.y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.S; //↓
                    nextRoomDoor.Direction = DoorDirection.W; //←

                    roomDoor.OriginPosition = new Vector2(room.Position.x + room.Size.x - offset1 - 6,
                        room.Position.y + room.Size.y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.x, nextRoom.Position.y + offset2);
                    cross = new Vector2(roomDoor.OriginPosition.x, nextRoomDoor.OriginPosition.y);
                }
            }
        }

        //判断门之间的通道是否有物体碰到
        if (!AddCorridorToGridRange(roomDoor, nextRoomDoor, cross))
        {
            //此门不能连通
            return false;
        }

        roomDoor.HasCross = true;
        roomDoor.Cross = cross;

        room.Doors.Add(roomDoor);
        nextRoom.Doors.Add(nextRoomDoor);
        return true;
    }

    //返回的x为宽, y为高
    private Vector2 CalcRange(float start1, float end1, float start2, float end2)
    {
        return new Vector2(Mathf.Max(start1, start2), Mathf.Min(end1, end2));
    }

    //返回参数方向的反方向
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

    //将两个门间的过道占用数据存入RoomGrid
    private bool AddCorridorToGridRange(RoomDoor door1, RoomDoor door2)
    {
        var point1 = door1.OriginPosition;
        var point2 = door2.OriginPosition;
        var pos = new Vector2(Mathf.Min(point1.x, point2.x), Mathf.Min(point1.y, point2.y));
        var size = new Vector2(
            point1.x == point2.x ? _corridorWidth : Mathf.Abs(point1.x - point2.x),
            point1.y == point2.y ? _corridorWidth : Mathf.Abs(point1.y - point2.y)
        );

        Vector2 collPos;
        Vector2 collSize;
        if (point1.x == point2.x) //纵向加宽, 防止贴到其它墙
        {
            collPos = new Vector2(pos.x - 3, pos.y);
            collSize = new Vector2(size.x + 6, size.y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos = new Vector2(pos.x, pos.y - 3);
            collSize = new Vector2(size.x, size.y + 6);
        }

        if (_roomGrid.RectCollision(collPos, collSize))
        {
            return false;
        }

        _roomGrid.AddRect(pos, size, true);
        return true;
    }

    //将两个门间的过道占用数据存入RoomGrid, 该重载
    private bool AddCorridorToGridRange(RoomDoor door1, RoomDoor door2, Vector2 cross)
    {
        var point1 = door1.OriginPosition;
        var point2 = door2.OriginPosition;
        var pos1 = new Vector2(Mathf.Min(point1.x, cross.x), Mathf.Min(point1.y, cross.y));
        var size1 = new Vector2(
            point1.x == cross.x ? _corridorWidth : Mathf.Abs(point1.x - cross.x),
            point1.y == cross.y ? _corridorWidth : Mathf.Abs(point1.y - cross.y)
        );
        var pos2 = new Vector2(Mathf.Min(point2.x, cross.x), Mathf.Min(point2.y, cross.y));
        var size2 = new Vector2(
            point2.x == cross.x ? _corridorWidth : Mathf.Abs(point2.x - cross.x),
            point2.y == cross.y ? _corridorWidth : Mathf.Abs(point2.y - cross.y)
        );

        Vector2 collPos1;
        Vector2 collSize1;
        if (point1.x == cross.x) //纵向加宽, 防止贴到其它墙
        {
            collPos1 = new Vector2(pos1.x - 3, pos1.y);
            collSize1 = new Vector2(size1.x + 6, size1.y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos1 = new Vector2(pos1.x, pos1.y - 3);
            collSize1 = new Vector2(size1.x, size1.y + 6);
        }

        if (_roomGrid.RectCollision(collPos1, collSize1))
        {
            return false;
        }

        Vector2 collPos2;
        Vector2 collSize2;
        if (point2.x == cross.x) //纵向加宽, 防止贴到其它墙
        {
            collPos2 = new Vector2(pos2.x - 3, pos2.y);
            collSize2 = new Vector2(size2.x + 6, size2.y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos2 = new Vector2(pos2.x, pos2.y - 3);
            collSize2 = new Vector2(size2.x, size2.y + 6);
        }

        if (_roomGrid.RectCollision(collPos2, collSize2))
        {
            return false;
        }

        _roomGrid.AddRect(pos1, size1, true);
        _roomGrid.AddRect(pos2, size2, true);
        return true;
    }
}