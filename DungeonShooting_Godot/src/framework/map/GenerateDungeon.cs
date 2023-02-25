
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 地牢生成器
/// </summary>
public class GenerateDungeon
{
    /// <summary>
    /// 过道宽度
    /// </summary>
    public const int CorridorWidth = 4;

    /// <summary>
    /// tilemap 网格大小
    /// </summary>
    public const int TileCellSize = 16;
    
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

    //用于标记地图上的坐标是否被占用
    private Grid<bool> _roomGrid { get; } = new Grid<bool>();
    
    //当前房间数量
    private int _count = 0;
    
    //宽高
    private int _roomMinWidth = 15;
    private int _roomMaxWidth = 35;
    private int _roomMinHeight = 15;
    private int _roomMaxHeight = 30;

    //间隔
    private int _roomMinInterval = 6;
    private int _roomMaxInterval = 10;

    //房间横轴分散程度
    private float _roomHorizontalMinDispersion = 0.7f;
    private float _roomHorizontalMaxDispersion = 1.1f;

    //房间纵轴分散程度
    private float _roomVerticalMinDispersion = 0.7f;
    private float _roomVerticalMaxDispersion = 1.1f;

    //区域限制
    private bool _enableLimitRange = true;
    private int _rangeX = 110;
    private int _rangeY = 110;
    
    //找房间失败次数, 过大则会关闭区域限制
    private int _maxFailCount = 10;
    private int _failCount = 0;

    private enum GenerateRoomErrorCode
    {
        NoError,
        //房间已满
        RoomFull,
        //超出区域
        OutArea,
        //碰到其他房间或过道
        HasCollision,
        //没有合适的门
        NoProperDoor,
    }
    
    /// <summary>
    /// 生成房间
    /// </summary>
    public void Generate()
    {
        if (StartRoom != null) return;

        //第一个房间
        GenerateRoom(null, 0, out var startRoom);
        StartRoom = startRoom;
        
        //如果房间数量不够, 就一直生成
        while (_count < _maxCount)
        {
            var room = Utils.RandomChoose(RoomInfos);
            var errorCode = GenerateRoom(room, Utils.RandomRangeInt(0, 3), out var nextRoom);
            if (errorCode == GenerateRoomErrorCode.NoError)
            {
                _failCount = 0;
                room.Next.Add(nextRoom);
            }
            else
            {
                GD.Print("生成第" + (_count + 1) + "个房间失败! 失败原因: " + errorCode);
                if (errorCode == GenerateRoomErrorCode.OutArea)
                {
                    _failCount++;
                    GD.Print("超出区域失败次数: " + _failCount);
                    if (_failCount >= _maxFailCount)
                    {
                        _enableLimitRange = false;
                        GD.Print("生成房间失败次数过多, 关闭区域限制!");
                    }
                }
            }
        }
        
        _roomGrid.Clear();
    }

    //生成房间
    private GenerateRoomErrorCode GenerateRoom(RoomInfo prevRoomInfo, int direction, out RoomInfo resultRoom)
    {
        if (_count >= _maxCount)
        {
            resultRoom = null;
            return GenerateRoomErrorCode.RoomFull;
        }

        //随机选择一个房间
        var roomSplit = Utils.RandomChoose(GameApplication.Instance.RoomConfig);
        var room = new RoomInfo(_count, roomSplit);
        
        //房间大小
        room.Size = new Vector2I((int)roomSplit.RoomInfo.Size.X, (int)roomSplit.RoomInfo.Size.Y);

        if (prevRoomInfo != null) //表示这不是第一个房间, 就得判断当前位置下的房间是否被遮挡
        {
            //房间间隔
            var space = Utils.RandomRangeInt(_roomMinInterval, _roomMaxInterval);
            //中心偏移
            int offset;
            if (direction == 0 || direction == 2)
            {
                offset = Utils.RandomRangeInt(-(int)(prevRoomInfo.Size.X * _roomVerticalMinDispersion),
                    (int)(prevRoomInfo.Size.X * _roomVerticalMaxDispersion));
            }
            else
            {
                offset = Utils.RandomRangeInt(-(int)(prevRoomInfo.Size.Y * _roomHorizontalMinDispersion),
                    (int)(prevRoomInfo.Size.Y * _roomHorizontalMaxDispersion));
            }

            //计算房间位置
            if (direction == 0) //上
            {
                room.Position = new Vector2I(prevRoomInfo.Position.X + offset,
                    prevRoomInfo.Position.Y - room.Size.Y - space);
            }
            else if (direction == 1) //右
            {
                room.Position = new Vector2I(prevRoomInfo.Position.X + prevRoomInfo.Size.Y + space,
                    prevRoomInfo.Position.Y + offset);
            }
            else if (direction == 2) //下
            {
                room.Position = new Vector2I(prevRoomInfo.Position.X + offset,
                    prevRoomInfo.Position.Y + prevRoomInfo.Size.Y + space);
            }
            else if (direction == 3) //左
            {
                room.Position = new Vector2I(prevRoomInfo.Position.X - room.Size.X - space,
                    prevRoomInfo.Position.Y + offset);
            }
            
            //是否在限制区域内
            if (_enableLimitRange)
            {
                if (room.Position.X < -_rangeX || room.Position.X + room.Size.X > _rangeX || room.Position.Y < -_rangeY || room.Position.Y + room.Size.Y > _rangeY)
                {
                    resultRoom = null;
                    return GenerateRoomErrorCode.OutArea;
                }
            }

            //是否碰到其他房间或者过道
            if (_roomGrid.RectCollision(room.Position - new Vector2(3, 3), room.Size + new Vector2(6, 6)))
            {
                resultRoom = null;
                return GenerateRoomErrorCode.HasCollision;
            }

            _roomGrid.AddRect(room.Position, room.Size, true);

            //找门, 与上一个房间是否能连通
            if (!ConnectDoor(prevRoomInfo, room))
            {
                _roomGrid.RemoveRect(room.Position, room.Size);
                resultRoom = null;
                return GenerateRoomErrorCode.NoProperDoor;
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
        if (Utils.RandomRangeInt(0, 2) != 0)
        {
            while (dirList.Count > 0)
            {
                var randDir = Utils.RandomChoose(dirList);
                GenerateRoom(room, randDir, out var nextRoom);
                if (nextRoom == null)
                {
                    break;
                }

                nextRoom.Prev = room;
                room.Next.Add(nextRoom);

                dirList.Remove(randDir);
            }
        }

        resultRoom = room;
        return GenerateRoomErrorCode.NoError;
    }

    /// <summary>
    /// 找两个房间的门
    /// </summary>
    private bool ConnectDoor(RoomInfo room, RoomInfo nextRoom)
    {
        //门描述
        var roomDoor = new RoomDoorInfo();
        var nextRoomDoor = new RoomDoorInfo();
        roomDoor.RoomInfo = room;
        nextRoomDoor.RoomInfo = nextRoom;
        roomDoor.ConnectRoom = nextRoom;
        roomDoor.ConnectDoor = nextRoomDoor;
        nextRoomDoor.ConnectRoom = room;
        nextRoomDoor.ConnectDoor = roomDoor;

        //先寻找直通门
        if (Utils.RandomBoolean())
        {
            //直行通道, 优先横轴
            if (TryConnectHorizontalDoor(room, roomDoor, nextRoom, nextRoomDoor)
                || TryConnectVerticalDoor(room, roomDoor, nextRoom, nextRoomDoor))
            {
                return true;
            }
        }
        else
        {
            //直行通道, 优先纵轴
            if (TryConnectVerticalDoor(room, roomDoor, nextRoom, nextRoomDoor)
                || TryConnectHorizontalDoor(room, roomDoor, nextRoom, nextRoomDoor))
            {
                return true;
            }
        }
        
        //包含拐角的通道
        return TryConnectCrossDoor(room, roomDoor, nextRoom, nextRoomDoor);
    }

    /// <summary>
    /// 尝试寻找横轴方向上两个房间的连通的门, 只查找直线通道, 返回是否找到
    /// </summary>
    private bool TryConnectHorizontalDoor(RoomInfo room, RoomDoorInfo roomDoor, RoomInfo nextRoom, RoomDoorInfo nextRoomDoor)
    {
        var overlapX = Mathf.Min(room.Position.X + room.Size.X, nextRoom.Position.X + nextRoom.Size.X) -
                       Mathf.Max(room.Position.X, nextRoom.Position.X);
        //这种情况下x轴有重叠
        if (overlapX >= 6)
        {
            //找到重叠区域
            var rangeList = FindPassage(room, nextRoom, 
                room.Position.Y < nextRoom.Position.Y ? DoorDirection.S : DoorDirection.N);
            
            while (rangeList.Count > 0)
            {
                //找到重叠区域
                var range = Utils.RandomChooseAndRemove(rangeList);
                var x = Utils.RandomRangeInt(range.X, range.Y);
                
                if (room.Position.Y < nextRoom.Position.Y) //room在上, nextRoom在下
                {
                    roomDoor.Direction = DoorDirection.S;
                    nextRoomDoor.Direction = DoorDirection.N;
                    roomDoor.OriginPosition = new Vector2(x, room.Position.Y + room.Size.Y);
                    nextRoomDoor.OriginPosition = new Vector2(x, nextRoom.Position.Y);
                }
                else //room在下, nextRoom在上
                {
                    roomDoor.Direction = DoorDirection.N;
                    nextRoomDoor.Direction = DoorDirection.S;
                    roomDoor.OriginPosition = new Vector2(x, room.Position.Y);
                    nextRoomDoor.OriginPosition = new Vector2(x, nextRoom.Position.Y + nextRoom.Size.Y);
                }

                //判断门之间的通道是否有物体碰到
                if (!AddCorridorToGridRange(roomDoor, nextRoomDoor))
                {
                    //此门不能连通
                    continue;
                }

                //没有撞到物体
                room.Doors.Add(roomDoor);
                nextRoom.Doors.Add(nextRoomDoor);
                return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// 尝试寻找纵轴方向上两个房间的连通的门, 只查找直线通道, 返回是否找到
    /// </summary>
    private bool TryConnectVerticalDoor(RoomInfo room, RoomDoorInfo roomDoor, RoomInfo nextRoom, RoomDoorInfo nextRoomDoor)
    {
        var overlapY = Mathf.Min(room.Position.Y + room.Size.Y, nextRoom.Position.Y + nextRoom.Size.Y) -
                       Mathf.Max(room.Position.Y, nextRoom.Position.Y);
        //这种情况下y轴有重叠
        if (overlapY >= 6)
        {
            //找到重叠区域
            var rangeList = FindPassage(room, nextRoom, 
                room.Position.X < nextRoom.Position.X ? DoorDirection.E : DoorDirection.W);

            while (rangeList.Count > 0)
            {
                //找到重叠区域
                var range = Utils.RandomChooseAndRemove(rangeList);
                var y = Utils.RandomRangeInt(range.X, range.Y);
                
                if (room.Position.X < nextRoom.Position.X) //room在左, nextRoom在右
                {
                    roomDoor.Direction = DoorDirection.E;
                    nextRoomDoor.Direction = DoorDirection.W;
                    roomDoor.OriginPosition = new Vector2(room.Position.X + room.Size.X, y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X, y);
                }
                else //room在右, nextRoom在左
                {
                    roomDoor.Direction = DoorDirection.W;
                    nextRoomDoor.Direction = DoorDirection.E;
                    roomDoor.OriginPosition = new Vector2(room.Position.X, y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X + nextRoom.Size.X, y);
                }

                //判断门之间的通道是否有物体碰到
                if (!AddCorridorToGridRange(roomDoor, nextRoomDoor))
                {
                    //此门不能连通
                    continue;
                }

                //没有撞到物体
                room.Doors.Add(roomDoor);
                nextRoom.Doors.Add(nextRoomDoor);
                return true;
            }
        }

        return false;
    }

    private bool TryConnectCrossDoor(RoomInfo room, RoomDoorInfo roomDoor, RoomInfo nextRoom, RoomDoorInfo nextRoomDoor)
    {
        //这种情况下x和y轴都没有重叠, 那么就只能生成拐角通道了
        
        var overlapX = Mathf.Min(room.Position.X + room.Size.X, nextRoom.Position.X + nextRoom.Size.X) -
                       Mathf.Max(room.Position.X, nextRoom.Position.X);
        var overlapY = Mathf.Min(room.Position.Y + room.Size.Y, nextRoom.Position.Y + nextRoom.Size.Y) -
                       Mathf.Max(room.Position.Y, nextRoom.Position.Y);

        //var offset1 = Mathf.Clamp(overlapX + 2, 2, 6);
        //var offset2 = Mathf.Clamp(overlapY + 2, 2, 6);
        
        var offset1 = 0;
        var offset2 = 0;

        //焦点
        Vector2 cross;

        if (room.Position.X > nextRoom.Position.X)
        {
            if (room.Position.Y > nextRoom.Position.Y)
            {
                if (Utils.RandomBoolean())
                {
                    roomDoor.Direction = DoorDirection.N; //↑
                    nextRoomDoor.Direction = DoorDirection.E; //→

                    FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor);
                    
                    roomDoor.OriginPosition = new Vector2(room.Position.X + offset1, room.Position.Y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X + nextRoom.Size.X,
                        nextRoom.Position.Y + nextRoom.Size.Y - offset2 - 6);
                    cross = new Vector2(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.W; //←
                    nextRoomDoor.Direction = DoorDirection.S; //↓
                
                    FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor);
                    
                    roomDoor.OriginPosition = new Vector2(room.Position.X, room.Position.Y + offset2);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X + nextRoom.Size.X - offset1 - 6,
                        nextRoom.Position.Y + nextRoom.Size.Y);
                    cross = new Vector2(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
                }
            }
            else
            {
                if (Utils.RandomBoolean())
                {
                    roomDoor.Direction = DoorDirection.S; //↓
                    nextRoomDoor.Direction = DoorDirection.E; //→
                    
                    FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor);

                    roomDoor.OriginPosition = new Vector2(room.Position.X + offset1, room.Position.Y + room.Size.Y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X + nextRoom.Size.X,
                        nextRoom.Position.Y + offset2);
                    cross = new Vector2(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.W; //←
                    nextRoomDoor.Direction = DoorDirection.N; //↑
                    
                    FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor);

                    roomDoor.OriginPosition =
                        new Vector2(room.Position.X, room.Position.Y + room.Size.Y - offset2 - 6); //
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X + nextRoom.Size.X - offset2 - 6,
                        nextRoom.Position.Y);
                    cross = new Vector2(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
                }
            }
        }
        else
        {
            if (room.Position.Y > nextRoom.Position.Y)
            {
                if (Utils.RandomBoolean())
                {
                    roomDoor.Direction = DoorDirection.E; //→
                    nextRoomDoor.Direction = DoorDirection.S; //↓

                    roomDoor.OriginPosition = new Vector2(room.Position.X + room.Size.X, room.Position.Y + offset2);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X + offset1,
                        nextRoom.Position.Y + nextRoom.Size.Y);
                    cross = new Vector2(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.N; //↑
                    nextRoomDoor.Direction = DoorDirection.W; //←

                    roomDoor.OriginPosition = new Vector2(room.Position.X + room.Size.X - offset1 - 6, room.Position.Y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X,
                        nextRoom.Position.Y + nextRoom.Size.Y - offset2 - 6);
                    cross = new Vector2(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
                }
            }
            else
            {
                if (Utils.RandomBoolean())
                {
                    roomDoor.Direction = DoorDirection.E; //→
                    nextRoomDoor.Direction = DoorDirection.N; //↑

                    roomDoor.OriginPosition = new Vector2(room.Position.X + room.Size.X,
                        room.Position.Y + room.Size.Y - offset2 - 6);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X + offset1, nextRoom.Position.Y);
                    cross = new Vector2(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
                }
                else
                {
                    roomDoor.Direction = DoorDirection.S; //↓
                    nextRoomDoor.Direction = DoorDirection.W; //←

                    roomDoor.OriginPosition = new Vector2(room.Position.X + room.Size.X - offset1 - 6,
                        room.Position.Y + room.Size.Y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.Position.X, nextRoom.Position.Y + offset2);
                    cross = new Vector2(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
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
        nextRoomDoor.HasCross = true;
        nextRoomDoor.Cross = cross;

        room.Doors.Add(roomDoor);
        nextRoom.Doors.Add(nextRoomDoor);
        return true;
    }
    
    private void FindCrossPassage(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor)
    {
        var room1 = room.RoomSplit.RoomInfo;
        var room2 = nextRoom.RoomSplit.RoomInfo;

        DoorAreaInfo tempArea1 = null;
        DoorAreaInfo tempArea2 = null;
        
        var tempX = room.GetHorizontalStart() * TileCellSize;
        var tempY = room.GetVerticalStart() * TileCellSize;

        foreach (var areaInfo1 in room1.DoorAreaInfos)
        {
            if (areaInfo1.Direction == roomDoor.Direction)
            {
                if (areaInfo1.Direction == DoorDirection.N || areaInfo1.Direction == DoorDirection.S) //纵向门
                {
                    var p1 = tempX + areaInfo1.Start;
                    var p2 = tempX + areaInfo1.End;
                    if (room.Position.X > nextRoom.Position.X)
                    {
                        if (IsInRange(nextRoom.GetHorizontalEnd() * TileCellSize,
                                room.GetHorizontalEnd() * TileCellSize, p1, p2))
                        {
                            if (tempArea1 == null || areaInfo1.Start < tempArea1.Start)
                            {
                                tempArea1 = areaInfo1;
                            }
                        }
                    }
                    else
                    {
                        if (IsInRange(room.GetHorizontalStart() * TileCellSize,
                                nextRoom.GetHorizontalStart() * TileCellSize, p1, p2))
                        {
                            if (tempArea1 == null || areaInfo1.End > tempArea1.End)
                            {
                                tempArea1 = areaInfo1;
                            }
                        }
                    }
                }
                else //横向门
                {
                    var p1 = tempY + areaInfo1.Start;
                    var p2 = tempY + areaInfo1.End;

                    if (room.Position.Y > nextRoom.Position.Y)
                    {
                        if (IsInRange(nextRoom.GetVerticalEnd() * TileCellSize,
                                room.GetVerticalEnd() * TileCellSize, p1, p2))
                        {
                            if (tempArea1 == null || areaInfo1.Start < tempArea1.Start)
                            {
                                tempArea1 = areaInfo1;
                            }
                        }
                    }
                    else
                    {
                        if (IsInRange(room.GetVerticalStart() * TileCellSize,
                                nextRoom.GetVerticalStart() * TileCellSize, p1, p2))
                        {
                            if (tempArea1 == null || areaInfo1.End > tempArea1.End)
                            {
                                tempArea1 = areaInfo1;
                            }
                        }
                    }
                }
            }
        }

        foreach (var areaInfo2 in room2.DoorAreaInfos)
        {
            if (areaInfo2.Direction == nextRoomDoor.Direction)
            {
                //if (IsInRange(room.Position.Y * TileCellSize, nextRoom.Position.Y * TileCellSize,
                //      room.Position.Y * TileCellSize + areaInfo1.Start, room.Position.Y * TileCellSize + areaInfo1.End))
            }
        }
    }

    /// <summary>
    /// 查找房间的连接通道, 函数返回是否找到对应的门, 通过 result 返回 x/y 轴坐标
    /// </summary>
    /// <param name="room">第一个房间</param>
    /// <param name="nextRoom">第二个房间</param>
    /// <param name="direction">第一个房间连接方向</param>
    private List<Vector2I> FindPassage(RoomInfo room, RoomInfo nextRoom, DoorDirection direction)
    {
        var room1 = room.RoomSplit.RoomInfo;
        var room2 = nextRoom.RoomSplit.RoomInfo;
        
        //用于存储符合生成条件的区域
        var rangeList = new List<Vector2I>();
        
        foreach (var doorAreaInfo1 in room1.DoorAreaInfos)
        {
            if (doorAreaInfo1.Direction == direction)
            {
                //第二个门的方向
                var direction2 = GetReverseDirection(direction);
                
                foreach (var doorAreaInfo2 in room2.DoorAreaInfos)
                {
                    if (doorAreaInfo2.Direction == direction2)
                    {
                        Vector2 range;
                        if (direction == DoorDirection.E || direction == DoorDirection.W) //第二个门向← 或者 第二个门向→
                        {
                            range = CalcOverlapRange(
                                room.Position.Y * TileCellSize + doorAreaInfo1.Start, room.Position.Y * TileCellSize + doorAreaInfo1.End,
                                nextRoom.Position.Y * TileCellSize + doorAreaInfo2.Start, nextRoom.Position.Y * TileCellSize + doorAreaInfo2.End
                            );
                        }
                        else //第二个门向↑ 或者 第二个门向↓
                        {
                            range = CalcOverlapRange(
                                room.Position.X * TileCellSize + doorAreaInfo1.Start, room.Position.X * TileCellSize + doorAreaInfo1.End,
                                nextRoom.Position.X * TileCellSize + doorAreaInfo2.Start, nextRoom.Position.X * TileCellSize + doorAreaInfo2.End
                            );
                        }
                        //交集范围够生成门
                        if (range.Y - range.X >= CorridorWidth * TileCellSize)
                        {
                            rangeList.Add(new Vector2I((int)(range.X / 16), (int)(range.Y / 16) - CorridorWidth));
                        }
                    }
                }
            }
        }
        
        return rangeList;
    }
    
    /// <summary>
    /// 用于计算重叠区域坐标, 可以理解为一维轴上4个点的中间两个点, 返回的x为起始点, y为结束点
    /// </summary>
    private Vector2 CalcOverlapRange(float start1, float end1, float start2, float end2)
    {
        return new Vector2(Mathf.Max(start1, start2), Mathf.Min(end1, end2));
    }

    /// <summary>
    /// 返回 p1 - p2 是否在 start - end 范围内
    /// </summary>
    private bool IsInRange(float start, float end, float p1, float p2)
    {
        return p1 >= start && p2 <= end;
    }
    
    //返回指定方向的反方向
    //0上, 1右, 2下, 3左
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
    
    //返回参数方向的反方向
    private DoorDirection GetReverseDirection(DoorDirection direction)
    {
        switch (direction)
        {
            case DoorDirection.E:
                return DoorDirection.W;
            case DoorDirection.W:
                return DoorDirection.E;
            case DoorDirection.S:
                return DoorDirection.N;
            case DoorDirection.N:
                return DoorDirection.S;
        }

        return DoorDirection.S;
    }

    //将两个门间的过道占用数据存入RoomGrid
    private bool AddCorridorToGridRange(RoomDoorInfo door1, RoomDoorInfo door2)
    {
        var point1 = door1.OriginPosition;
        var point2 = door2.OriginPosition;
        var pos = new Vector2(Mathf.Min(point1.X, point2.X), Mathf.Min(point1.Y, point2.Y));
        var size = new Vector2(
            point1.X == point2.X ? CorridorWidth : Mathf.Abs(point1.X - point2.X),
            point1.Y == point2.Y ? CorridorWidth : Mathf.Abs(point1.Y - point2.Y)
        );

        Vector2 collPos;
        Vector2 collSize;
        if (point1.X == point2.X) //纵向加宽, 防止贴到其它墙
        {
            collPos = new Vector2(pos.X - 3, pos.Y);
            collSize = new Vector2(size.X + 6, size.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos = new Vector2(pos.X, pos.Y - 3);
            collSize = new Vector2(size.X, size.Y + 6);
        }

        if (_roomGrid.RectCollision(collPos, collSize))
        {
            return false;
        }

        _roomGrid.AddRect(pos, size, true);
        return true;
    }

    //将两个门间的过道占用数据存入RoomGrid, 该重载加入拐角点
    private bool AddCorridorToGridRange(RoomDoorInfo door1, RoomDoorInfo door2, Vector2 cross)
    {
        var point1 = door1.OriginPosition;
        var point2 = door2.OriginPosition;
        var pos1 = new Vector2(Mathf.Min(point1.X, cross.X), Mathf.Min(point1.Y, cross.Y));
        var size1 = new Vector2(
            point1.X == cross.X ? CorridorWidth : Mathf.Abs(point1.X - cross.X),
            point1.Y == cross.Y ? CorridorWidth : Mathf.Abs(point1.Y - cross.Y)
        );
        var pos2 = new Vector2(Mathf.Min(point2.X, cross.X), Mathf.Min(point2.Y, cross.Y));
        var size2 = new Vector2(
            point2.X == cross.X ? CorridorWidth : Mathf.Abs(point2.X - cross.X),
            point2.Y == cross.Y ? CorridorWidth : Mathf.Abs(point2.Y - cross.Y)
        );

        Vector2 collPos1;
        Vector2 collSize1;
        if (point1.X == cross.X) //纵向加宽, 防止贴到其它墙
        {
            collPos1 = new Vector2(pos1.X - 3, pos1.Y);
            collSize1 = new Vector2(size1.X + 6, size1.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos1 = new Vector2(pos1.X, pos1.Y - 3);
            collSize1 = new Vector2(size1.X, size1.Y + 6);
        }

        if (_roomGrid.RectCollision(collPos1, collSize1))
        {
            return false;
        }

        Vector2 collPos2;
        Vector2 collSize2;
        if (point2.X == cross.X) //纵向加宽, 防止贴到其它墙
        {
            collPos2 = new Vector2(pos2.X - 3, pos2.Y);
            collSize2 = new Vector2(size2.X + 6, size2.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos2 = new Vector2(pos2.X, pos2.Y - 3);
            collSize2 = new Vector2(size2.X, size2.Y + 6);
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