
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 地牢生成器
/// </summary>
public class DungeonGenerator
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
    /// 结束房间
    /// </summary>
    public RoomInfo EndRoom { get; private set; }

    /// <summary>
    /// boss房间
    /// </summary>
    public List<RoomInfo> BossRoom { get; } = new List<RoomInfo>();

    //用于标记地图上的坐标是否被占用
    private Grid<bool> _roomGrid { get; } = new Grid<bool>();
    
    //当前房间数量
    private int _count = 0;
    //房间id
    private int _id;
    
    //下一个房间类型
    private DungeonRoomType _nextRoomType = DungeonRoomType.Battle;
    
    //宽高
    // private int _roomMinWidth = 15;
    // private int _roomMaxWidth = 35;
    // private int _roomMinHeight = 15;
    // private int _roomMaxHeight = 30;

    //间隔
    private int _roomMinInterval = 6;
    private int _roomMaxInterval = 10;

    //房间横轴分散程度
    
    private float _roomHorizontalMinDispersion = 0f;
    private float _roomHorizontalMaxDispersion = 0.7f;

    //房间纵轴分散程度
    private float _roomVerticalMinDispersion = 0f;
    private float _roomVerticalMaxDispersion = 0.7f;

    //区域限制
    private bool _enableLimitRange = true;
    private int _rangeX = 110;
    private int _rangeY = 110;
    
    //找房间失败次数, 过大则会关闭区域限制
    private int _maxFailCount = 10;
    private int _failCount = 0;

    //最大尝试次数
    private int _maxTryCount = 10;
    private int _currMaxLayer = 0;

    //地牢配置
    private DungeonConfig _config;
    private DungeonRoomGroup _roomGroup;

    //指定只能生成的房间
    private static List<DungeonRoomSplit> _designatedRoom;

    private enum GenerateRoomErrorCode
    {
        NoError,
        //超出区域
        OutArea,
        //没有合适的位置
        NoSuitableLocation
        // //碰到其他房间或过道
        // HasCollision,
        // //没有合适的门
        // NoProperDoor,
    }

#if TOOLS
    /// <summary>
    /// 用于调试, 设置生成器只能生成哪些房间
    /// </summary>
    public static void SetDesignatedRoom(List<DungeonRoomSplit> list)
    {
        _designatedRoom = new List<DungeonRoomSplit>(list);
    }
#endif

    public DungeonGenerator(DungeonConfig config)
    {
        _config = config;
        _roomGroup = GameApplication.Instance.RoomConfig[config.GroupName];

        //验证该组是否满足生成地牢的条件
        if (_roomGroup.InletList.Count == 0)
        {
            throw new Exception("当前组'" + config.GroupName + "'中没有起始房间, 不能生成地牢!");
        }
        //没有指定房间
        if (_designatedRoom == null || _designatedRoom.Count == 0)
        {
            if (_roomGroup.OutletList.Count == 0)
            {
                throw new Exception("当前组'" + config.GroupName + "'中没有结束房间, 不能生成地牢!");
            }
            else if (_roomGroup.BattleList.Count == 0)
            {
                throw new Exception("当前组'" + config.GroupName + "'中没有战斗房间, 不能生成地牢!");
            }
        }

        _roomGroup.InitWeight();
    }

    /// <summary>
    /// 遍历所有房间
    /// </summary>
    public void EachRoom(Action<RoomInfo> cb)
    {
        EachRoom(StartRoom, cb);
    }

    private void EachRoom(RoomInfo roomInfo, Action<RoomInfo> cb)
    {
        if (roomInfo == null)
        {
            return;
        }

        cb(roomInfo);
        foreach (var next in roomInfo.Next)
        {
            EachRoom(next, cb);
        }
    }

    /// <summary>
    /// 生成房间
    /// </summary>
    public void Generate()
    {
        if (StartRoom != null) return;
        
        CalcNextRoomType(null);
        //用于排除上一级房间
        var excludePrevRoom = new List<RoomInfo>();
        //上一个房间
        RoomInfo prevRoom = null;
        
        var chainTryCount = 0;
        var chainMaxTryCount = 3;

        //如果房间数量不够, 就一直生成
        while (_count < _config.RoomCount || EndRoom == null)
        {
            var nextRoomType = GetNextRoomType();
            
            //上一个房间
            RoomInfo tempPrevRoom;
            if (nextRoomType == DungeonRoomType.Inlet)
            {
                tempPrevRoom = null;
            }
            else if (nextRoomType == DungeonRoomType.Boss)
            {
                tempPrevRoom = FindMaxLayerRoom(excludePrevRoom);
            }
            else if (nextRoomType == DungeonRoomType.Outlet)
            {
                tempPrevRoom = prevRoom;
            }
            else if (nextRoomType == DungeonRoomType.Battle)
            {
                if (chainTryCount < chainMaxTryCount)
                {
                    if (prevRoom != null && prevRoom.Layer > 6) //层数太高, 下一个房间生成在低层级
                    {
                        tempPrevRoom = RoundRoomLessThanLayer(3);
                    }
                    else
                    {
                        tempPrevRoom = prevRoom;
                    }
                }
                else
                {
                    tempPrevRoom = Utils.RandomChoose(RoomInfos);
                }
            }
            else
            {
                tempPrevRoom = Utils.RandomChoose(RoomInfos);
            }
            
            //生成下一个房间
            var errorCode = GenerateRoom(tempPrevRoom, nextRoomType, out var nextRoom);
            if (errorCode == GenerateRoomErrorCode.NoError) //生成成功
            {
                _failCount = 0;
                RoomInfos.Add(nextRoom);
                if (nextRoomType == DungeonRoomType.Inlet)
                {
                    StartRoom = nextRoom;
                }
                else if (nextRoomType == DungeonRoomType.Boss) //boss房间
                {
                    BossRoom.Add(nextRoom);
                    excludePrevRoom.Clear();
                }
                else if (nextRoomType == DungeonRoomType.Outlet)
                {
                    EndRoom = nextRoom;
                }
                else if (nextRoomType == DungeonRoomType.Battle)
                {
                    chainTryCount = 0;
                    chainMaxTryCount = Utils.RandomRangeInt(1, 3);
                }
                prevRoom = nextRoom;
                CalcNextRoomType(prevRoom);
            }
            else //生成失败
            {
                if (nextRoomType == DungeonRoomType.Boss)
                {
                    //生成boss房间成功
                    excludePrevRoom.Add(tempPrevRoom);
                    if (excludePrevRoom.Count >= RoomInfos.Count)
                    {
                        //全都没找到合适的, 那就再来一遍
                        excludePrevRoom.Clear();
                    }
                }
                else if (nextRoomType == DungeonRoomType.Outlet)
                {
                    //生成结束房间失败, 那么只能回滚boss房间
                    if (prevRoom != null)
                    {
                        var bossPrev = prevRoom.Prev;
                        BossRoom.Remove(prevRoom);
                        RollbackRoom(prevRoom);
                        CalcNextRoomType(bossPrev);
                        prevRoom = null;
                    }
                }
                else if (nextRoomType == DungeonRoomType.Battle)
                {
                    chainTryCount++;
                }
                
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
        GD.Print("房间总数: " + RoomInfos.Count);
    }

    //生成房间
    private GenerateRoomErrorCode GenerateRoom(RoomInfo prevRoomInfo, DungeonRoomType roomType, out RoomInfo resultRoom)
    {
        // if (_count >= _config.RoomCount)
        // {
        //     resultRoom = null;
        //     return GenerateRoomErrorCode.RoomFull;
        // }

        DungeonRoomSplit roomSplit;
        //没有指定房间
        if (roomType == DungeonRoomType.Inlet || _designatedRoom == null || _designatedRoom.Count == 0)
        {
            //随机选择一个房间
            var list = _roomGroup.GetRoomList(roomType);
            if (list.Count == 0) //如果没有指定类型的房间, 就生成战斗房间
            {
                roomSplit = _roomGroup.GetRandomRoom(DungeonRoomType.Battle);
            }
            else
            {
                roomSplit = _roomGroup.GetRandomRoom(roomType);
            }
        }
        else //指定了房间
        {
            roomSplit = Utils.RandomChoose(_designatedRoom);
        }
        
        var room = new RoomInfo(_id, roomType, roomSplit);

        //房间大小
        room.Size = new Vector2I((int)roomSplit.RoomInfo.Size.X, (int)roomSplit.RoomInfo.Size.Y);

        if (prevRoomInfo != null) //表示这不是第一个房间, 就得判断当前位置下的房间是否被遮挡
        {
            room.Layer = prevRoomInfo.Layer + 1;
            if (_currMaxLayer < room.Layer)
            {
                _currMaxLayer = room.Layer;
            }
            //生成的位置可能会和上一个房间对不上, 需要多次尝试
            var tryCount = 0; //当前尝试次数
            var maxTryCount = _maxTryCount; //最大尝试次数
            if (roomType == DungeonRoomType.Outlet)
            {
                maxTryCount *= 3;
            }
            else if (roomType == DungeonRoomType.Boss)
            {
                maxTryCount *= 2;
            }
            for (; tryCount < maxTryCount; tryCount++)
            {
                var direction = Utils.RandomRangeInt(0, 3);
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
                    if (room.GetHorizontalStart() < -_rangeX || room.GetHorizontalEnd() > _rangeX ||
                        room.GetVerticalStart() < -_rangeY || room.GetVerticalEnd() > _rangeY)
                    {
                        //超出区域, 直接跳出尝试的循环, 返回 null
                        resultRoom = null;
                        return GenerateRoomErrorCode.OutArea;
                    }
                }

                //是否碰到其他房间或者过道
                if (_roomGrid.RectCollision(room.Position - new Vector2(GameConfig.RoomSpace, GameConfig.RoomSpace), room.Size + new Vector2(GameConfig.RoomSpace * 2, GameConfig.RoomSpace * 2)))
                {
                    //碰到其他墙壁, 再一次尝试
                    continue;
                    //return GenerateRoomErrorCode.HasCollision;
                }

                _roomGrid.AddRect(room.Position, room.Size, true);

                //找门, 与上一个房间是否能连通
                if (!ConnectDoor(prevRoomInfo, room))
                {
                    _roomGrid.RemoveRect(room.Position, room.Size);
                    //房间过道没有连接上, 再一次尝试
                    continue;
                    //return GenerateRoomErrorCode.NoProperDoor;
                }
                break;
            }

            //尝试次数用光了, 还没有找到合适的位置
            if (tryCount >= _maxTryCount)
            {
                resultRoom = null;
                return GenerateRoomErrorCode.NoSuitableLocation;
            }
        }
        else //第一个房间
        {
            room.Layer = 0;
            _roomGrid.AddRect(room.Position, room.Size, true);
        }

        if (IsParticipateCounting(room))
        {
            _count++;
        }
        
        _id++;
        room.Prev = prevRoomInfo;
        if (prevRoomInfo != null)
        {
            prevRoomInfo.Next.Add(room);
        }
        resultRoom = room;
        return GenerateRoomErrorCode.NoError;
    }

    //判断房间是否参与计数
    private bool IsParticipateCounting(RoomInfo roomInfo)
    {
        return roomInfo.RoomType == DungeonRoomType.Battle || roomInfo.RoomType == DungeonRoomType.Boss;
    }

    //计算下一个房间类型
    private void CalcNextRoomType(RoomInfo prev)
    {
        if (prev == null) //生成第一个房间
        {
            _nextRoomType = DungeonRoomType.Inlet;
        }
        else if (_count == _config.RoomCount - 1) //最后一个房间是boss房间
        {
            _nextRoomType = DungeonRoomType.Boss;
        }
        else if (_count >= _config.RoomCount) //结束房间
        {
            _nextRoomType = DungeonRoomType.Outlet;
        }
        else if (prev.RoomType == DungeonRoomType.Boss) //生成结束房间
        {
            _nextRoomType = DungeonRoomType.Outlet;
        }
        else
        {
            _nextRoomType = DungeonRoomType.Battle;
        }
    }
    
    //获取下一个房间类型
    private DungeonRoomType GetNextRoomType()
    {
        return _nextRoomType;
    }

    //回滚一个房间
    private bool RollbackRoom(RoomInfo roomInfo)
    {
        if (roomInfo.Next.Count > 0)
        {
            GD.PrintErr("当前房间还有连接的子房间, 不能回滚!");
            return false;
        }
        //退掉占用的房间区域和过道占用区域
        _roomGrid.RemoveRect(roomInfo.Position, roomInfo.Size);
        foreach (var rect2 in roomInfo.AisleArea)
        {
            _roomGrid.RemoveRect(rect2.Position, rect2.Size);
        }
        
        //roomInfo.Doors[0].
        if (roomInfo.Prev != null)
        {
            roomInfo.Prev.Next.Remove(roomInfo);
        }

        roomInfo.Prev = null;
        foreach (var roomInfoDoor in roomInfo.Doors)
        {
            var connectDoor = roomInfoDoor.ConnectDoor;
            connectDoor.RoomInfo.Doors.Remove(connectDoor);
        }

        RoomInfos.Remove(roomInfo);
        roomInfo.Destroy();

        if (IsParticipateCounting(roomInfo))
        {
            _count--;
        }

        _id--;
        return true;
    }

    /// <summary>
    /// 寻找层级最高的房间
    /// </summary>
    /// <param name="exclude">排除的房间</param>
    private RoomInfo FindMaxLayerRoom(List<RoomInfo> exclude)
    {
        RoomInfo temp = null;
        foreach (var roomInfo in RoomInfos)
        {
            if (temp == null || roomInfo.Layer > temp.Layer)
            {
                if (exclude == null || !exclude.Contains(roomInfo))
                {
                    temp = roomInfo;
                }
            }
        }

        return temp;
    }

    /// <summary>
    /// 随机抽取层级小于 layer 的房间
    /// </summary>
    private RoomInfo RoundRoomLessThanLayer(int layer)
    {
        var list = new List<RoomInfo>();
        foreach (var roomInfo in RoomInfos)
        {
            if (roomInfo.Layer < layer)
            {
                list.Add(roomInfo);
            }
        }

        return Utils.RandomChoose(list);
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
        var overlapX = Mathf.Min(room.GetHorizontalEnd(), nextRoom.GetHorizontalEnd()) -
                       Mathf.Max(room.GetHorizontalStart(), nextRoom.GetHorizontalStart());
        //这种情况下x轴有重叠
        if (overlapX >= 6)
        {
            //找到重叠区域
            var rangeList = FindPassage(room, nextRoom, 
                room.GetVerticalStart() < nextRoom.GetVerticalStart() ? DoorDirection.S : DoorDirection.N);
            
            while (rangeList.Count > 0)
            {
                //找到重叠区域
                var range = Utils.RandomChooseAndRemove(rangeList);
                var x = Utils.RandomRangeInt(range.X, range.Y);
                
                if (room.GetVerticalStart() < nextRoom.GetVerticalStart()) //room在上, nextRoom在下
                {
                    roomDoor.Direction = DoorDirection.S;
                    nextRoomDoor.Direction = DoorDirection.N;
                    roomDoor.OriginPosition = new Vector2(x, room.GetVerticalEnd());
                    nextRoomDoor.OriginPosition = new Vector2(x, nextRoom.GetVerticalStart());
                }
                else //room在下, nextRoom在上
                {
                    roomDoor.Direction = DoorDirection.N;
                    nextRoomDoor.Direction = DoorDirection.S;
                    roomDoor.OriginPosition = new Vector2(x, room.GetVerticalStart());
                    nextRoomDoor.OriginPosition = new Vector2(x, nextRoom.GetVerticalEnd());
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
        var overlapY = Mathf.Min(room.GetVerticalEnd(), nextRoom.GetVerticalEnd()) -
                       Mathf.Max(room.GetVerticalStart(), nextRoom.GetVerticalStart());
        //这种情况下y轴有重叠
        if (overlapY >= 6)
        {
            //找到重叠区域
            var rangeList = FindPassage(room, nextRoom, 
                room.GetHorizontalStart() < nextRoom.GetHorizontalStart() ? DoorDirection.E : DoorDirection.W);

            while (rangeList.Count > 0)
            {
                //找到重叠区域
                var range = Utils.RandomChooseAndRemove(rangeList);
                var y = Utils.RandomRangeInt(range.X, range.Y);
                
                if (room.GetHorizontalStart() < nextRoom.GetHorizontalStart()) //room在左, nextRoom在右
                {
                    roomDoor.Direction = DoorDirection.E;
                    nextRoomDoor.Direction = DoorDirection.W;
                    roomDoor.OriginPosition = new Vector2(room.GetHorizontalEnd(), y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalStart(), y);
                }
                else //room在右, nextRoom在左
                {
                    roomDoor.Direction = DoorDirection.W;
                    nextRoomDoor.Direction = DoorDirection.E;
                    roomDoor.OriginPosition = new Vector2(room.GetHorizontalStart(), y);
                    nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalEnd(), y);
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
    /// 尝试寻找包含拐角的两个房间的连通的门, 返回是否找到
    /// </summary>
    private bool TryConnectCrossDoor(RoomInfo room, RoomDoorInfo roomDoor, RoomInfo nextRoom, RoomDoorInfo nextRoomDoor)
    {
        //焦点
        Vector2 cross = default;

        if (room.GetHorizontalStart() > nextRoom.GetHorizontalStart())
        {
            if (room.GetVerticalStart() > nextRoom.GetVerticalStart())
            {
                if (Utils.RandomBoolean()) //↑ //→
                {
                    if (!TryConnect_NE_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_WS_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
                else //← //↓
                {
                    if (!TryConnect_WS_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_NE_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (Utils.RandomBoolean()) //↓ //→
                {
                    if (!TryConnect_SE_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_WN_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
                else //← //↑
                {
                    if (!TryConnect_WN_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_SE_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            if (room.GetVerticalStart() > nextRoom.GetVerticalStart()) //→ //↓
            {
                if (Utils.RandomBoolean())
                {
                    if (!TryConnect_ES_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_NW_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
                else //↑ //←
                {
                    if (!TryConnect_NW_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_ES_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (Utils.RandomBoolean()) //→ //↑
                {
                    if (!TryConnect_EN_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_SW_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
                else //↓ //←
                {
                    if (!TryConnect_SW_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_EN_Door(room, nextRoom, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
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

    private bool FindCrossPassage(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor,ref int offset1, ref int offset2)
    {
        var room1 = room.RoomSplit.RoomInfo;
        var room2 = nextRoom.RoomSplit.RoomInfo;
        
        int? temp1 = null;
        int? temp2 = null;

        foreach (var areaInfo1 in room1.DoorAreaInfos)
        {
            if (areaInfo1.Direction == roomDoor.Direction)
            {
                FindCrossPassage_Area(areaInfo1, room, nextRoom, ref temp1);
            }
        }
        
        if (temp1 == null)
        {
            return false;
        }

        foreach (var areaInfo2 in room2.DoorAreaInfos)
        {
            if (areaInfo2.Direction == nextRoomDoor.Direction)
            {
                FindCrossPassage_Area(areaInfo2, nextRoom, room, ref temp2);
            }
        }

        if (temp2 == null)
        {
            return false;
        }
        
        offset1 = temp1.Value;
        offset2 = temp2.Value;
        return true;
    }

    private void FindCrossPassage_Area(DoorAreaInfo areaInfo, RoomInfo room1, RoomInfo room2, ref int? areaRange)
    {
        if (areaInfo.Direction == DoorDirection.N || areaInfo.Direction == DoorDirection.S) //纵向门
        {
            var num = room1.GetHorizontalStart();
            var p1 = num + areaInfo.Start / GameConfig.TileCellSize;
            var p2 = num + areaInfo.End / GameConfig.TileCellSize;

            if (room1.Position.X > room2.Position.X)
            {
                var range = CalcOverlapRange(room2.GetHorizontalEnd() + GameConfig.RoomSpace,
                    room1.GetHorizontalEnd(), p1, p2);
                //交集范围够生成门
                if (range.Y - range.X >= GameConfig.CorridorWidth)
                {
                    // var tempRange = new Vector2I(Mathf.Abs(room1.Position.X - (int)range.X),
                    //     Mathf.Abs(room1.Position.X - (int)range.Y) - GameConfig.CorridorWidth);
                    
                    var rangeValue = Mathf.Abs(room1.Position.X - (int)range.X);

                    if (areaRange == null || rangeValue < areaRange)
                    {
                        areaRange = rangeValue;
                    }
                }
            }
            else
            {
                var range = CalcOverlapRange(room1.GetHorizontalStart(),
                    room2.GetHorizontalStart() -  + GameConfig.RoomSpace, p1, p2);
                //交集范围够生成门
                if (range.Y - range.X >= GameConfig.CorridorWidth)
                {
                    // var tempRange = new Vector2I(Mathf.Abs(room1.Position.X - (int)range.X),
                    //     Mathf.Abs(room1.Position.X - (int)range.Y) - GameConfig.CorridorWidth);

                    var rangeValue = Mathf.Abs(room1.Position.X - (int)range.Y) - GameConfig.CorridorWidth;

                    if (areaRange == null || rangeValue > areaRange)
                    {
                        areaRange = rangeValue;
                    }
                }
            }
        }
        else //横向门
        {
            var num = room1.GetVerticalStart();
            var p1 = num + areaInfo.Start / GameConfig.TileCellSize;
            var p2 = num + areaInfo.End / GameConfig.TileCellSize;

            if (room1.Position.Y > room2.Position.Y)
            {
                var range = CalcOverlapRange(room2.GetVerticalEnd() + GameConfig.RoomSpace,
                    room1.GetVerticalEnd(), p1, p2);
                //交集范围够生成门
                if (range.Y - range.X >= GameConfig.CorridorWidth)
                {
                    // var tempRange = new Vector2I(Mathf.Abs(room1.Position.Y - (int)range.X),
                    //     Mathf.Abs(room1.Position.Y - (int)range.Y) - GameConfig.CorridorWidth);

                    var rangeValue = Mathf.Abs(room1.Position.Y - (int)range.X);

                    if (areaRange == null || rangeValue < areaRange)
                    {
                        areaRange = rangeValue;
                    }
                }
            }
            else
            {
                var range = CalcOverlapRange(room1.GetVerticalStart(),
                    room2.GetVerticalStart() - GameConfig.RoomSpace, p1, p2);
                //交集范围够生成门
                if (range.Y - range.X >= GameConfig.CorridorWidth)
                {
                    // var tempRange = new Vector2I(Mathf.Abs(room1.Position.Y - (int)range.X),
                    //     Mathf.Abs(room1.Position.Y - (int)range.Y) - GameConfig.CorridorWidth);
                    
                    var rangeValue = Mathf.Abs(room1.Position.Y - (int)range.Y) - GameConfig.CorridorWidth;

                    if (areaRange == null || rangeValue > areaRange)
                    {
                        areaRange = rangeValue;
                    }
                }
            }
        }
    }

    private bool TryConnect_NE_Door(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2 cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.N; //↑
        nextRoomDoor.Direction = DoorDirection.E; //→

        if (!FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }
                    
        roomDoor.OriginPosition = new Vector2(room.GetHorizontalStart() + offset1, room.GetVerticalStart());
        nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalEnd(),
            nextRoom.GetVerticalStart() + offset2);
        cross = new Vector2(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
        return true;
    }

    private bool TryConnect_WS_Door(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2 cross)
    {
        //ok
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.W; //←
        nextRoomDoor.Direction = DoorDirection.S; //↓
                
        if (!FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }
                    
        roomDoor.OriginPosition = new Vector2(room.GetHorizontalStart(), room.GetVerticalStart() + offset1);
        nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalStart() + offset2, nextRoom.GetVerticalEnd());
        cross = new Vector2(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
        return true;
    }

    private bool TryConnect_SE_Door(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2 cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.S; //↓
        nextRoomDoor.Direction = DoorDirection.E; //→
                    
        if (!FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        roomDoor.OriginPosition = new Vector2(room.GetHorizontalStart() + offset1, room.GetVerticalEnd());
        nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalEnd(),
            nextRoom.GetVerticalStart() + offset2);
        cross = new Vector2(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
        return true;
    }

    private bool TryConnect_WN_Door(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2 cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.W; //←
        nextRoomDoor.Direction = DoorDirection.N; //↑
                    
        if (!FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        roomDoor.OriginPosition =
            new Vector2(room.GetHorizontalStart(), room.GetVerticalStart() + offset1); //
        nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalStart() + offset2,
            nextRoom.GetVerticalStart());
        cross = new Vector2(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
        return true;
    }
    
    private bool TryConnect_ES_Door(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2 cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.E; //→
        nextRoomDoor.Direction = DoorDirection.S; //↓

        if (!FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }
                    
        roomDoor.OriginPosition = new Vector2(room.GetHorizontalEnd(), room.GetVerticalStart() + offset1);
        nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalStart() + offset2,
            nextRoom.GetVerticalEnd());
        cross = new Vector2(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
        return true;
    }
    
    private bool TryConnect_NW_Door(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2 cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.N; //↑
        nextRoomDoor.Direction = DoorDirection.W; //←

        if (!FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }
                    
        roomDoor.OriginPosition = new Vector2(room.GetHorizontalStart() + offset1, room.GetVerticalStart());
        nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalStart(),
            nextRoom.GetVerticalStart() + offset2);
        cross = new Vector2(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
        return true;
    }
    
    private bool TryConnect_EN_Door(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2 cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.E; //→
        nextRoomDoor.Direction = DoorDirection.N; //↑

        if (!FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }
                    
        roomDoor.OriginPosition = new Vector2(room.GetHorizontalEnd(),
            room.GetVerticalStart() + offset1);
        nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalStart() + offset2, nextRoom.GetVerticalStart());
        cross = new Vector2(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
        return true;
    }

    private bool TryConnect_SW_Door(RoomInfo room, RoomInfo nextRoom, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2 cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.S; //↓
        nextRoomDoor.Direction = DoorDirection.W; //←

        if (!FindCrossPassage(room, nextRoom, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }
                    
        roomDoor.OriginPosition = new Vector2(room.GetHorizontalStart() + offset1,
            room.GetVerticalEnd());
        nextRoomDoor.OriginPosition = new Vector2(nextRoom.GetHorizontalStart(), nextRoom.GetVerticalStart() + offset2);
        cross = new Vector2(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
        return true;
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
                                room.GetVerticalStart() * GameConfig.TileCellSize + doorAreaInfo1.Start, room.GetVerticalStart() * GameConfig.TileCellSize + doorAreaInfo1.End,
                                nextRoom.GetVerticalStart() * GameConfig.TileCellSize + doorAreaInfo2.Start, nextRoom.GetVerticalStart() * GameConfig.TileCellSize + doorAreaInfo2.End
                            );
                        }
                        else //第二个门向↑ 或者 第二个门向↓
                        {
                            range = CalcOverlapRange(
                                room.GetHorizontalStart() * GameConfig.TileCellSize + doorAreaInfo1.Start, room.GetHorizontalStart() * GameConfig.TileCellSize + doorAreaInfo1.End,
                                nextRoom.GetHorizontalStart() * GameConfig.TileCellSize + doorAreaInfo2.Start, nextRoom.GetHorizontalStart() * GameConfig.TileCellSize + doorAreaInfo2.End
                            );
                        }
                        //交集范围够生成门
                        if (range.Y - range.X >= GameConfig.CorridorWidth * GameConfig.TileCellSize)
                        {
                            rangeList.Add(new Vector2I((int)(range.X / 16), (int)(range.Y / 16) - GameConfig.CorridorWidth));
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
            point1.X == point2.X ? GameConfig.CorridorWidth : Mathf.Abs(point1.X - point2.X),
            point1.Y == point2.Y ? GameConfig.CorridorWidth : Mathf.Abs(point1.Y - point2.Y)
        );

        Vector2 collPos;
        Vector2 collSize;
        if (point1.X == point2.X) //纵向加宽, 防止贴到其它墙
        {
            collPos = new Vector2(pos.X - GameConfig.RoomSpace, pos.Y);
            collSize = new Vector2(size.X + GameConfig.RoomSpace * 2, size.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos = new Vector2(pos.X, pos.Y - GameConfig.RoomSpace);
            collSize = new Vector2(size.X, size.Y + GameConfig.RoomSpace * 2);
        }

        if (_roomGrid.RectCollision(collPos, collSize))
        {
            return false;
        }

        door2.RoomInfo.AisleArea.Add(new Rect2(pos, size));
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
            point1.X == cross.X ? GameConfig.CorridorWidth : Mathf.Abs(point1.X - cross.X),
            point1.Y == cross.Y ? GameConfig.CorridorWidth : Mathf.Abs(point1.Y - cross.Y)
        );
        var pos2 = new Vector2(Mathf.Min(point2.X, cross.X), Mathf.Min(point2.Y, cross.Y));
        var size2 = new Vector2(
            point2.X == cross.X ? GameConfig.CorridorWidth : Mathf.Abs(point2.X - cross.X),
            point2.Y == cross.Y ? GameConfig.CorridorWidth : Mathf.Abs(point2.Y - cross.Y)
        );

        Vector2 collPos1;
        Vector2 collSize1;
        if (point1.X == cross.X) //纵向加宽, 防止贴到其它墙
        {
            collPos1 = new Vector2(pos1.X - GameConfig.RoomSpace, pos1.Y);
            collSize1 = new Vector2(size1.X + GameConfig.RoomSpace * 2, size1.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos1 = new Vector2(pos1.X, pos1.Y - GameConfig.RoomSpace);
            collSize1 = new Vector2(size1.X, size1.Y + GameConfig.RoomSpace * 2);
        }

        if (_roomGrid.RectCollision(collPos1, collSize1))
        {
            return false;
        }

        Vector2 collPos2;
        Vector2 collSize2;
        if (point2.X == cross.X) //纵向加宽, 防止贴到其它墙
        {
            collPos2 = new Vector2(pos2.X - GameConfig.RoomSpace, pos2.Y);
            collSize2 = new Vector2(size2.X + GameConfig.RoomSpace * 2, size2.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            collPos2 = new Vector2(pos2.X, pos2.Y - GameConfig.RoomSpace);
            collSize2 = new Vector2(size2.X, size2.Y + GameConfig.RoomSpace * 2);
        }

        if (_roomGrid.RectCollision(collPos2, collSize2))
        {
            return false;
        }

        door2.RoomInfo.AisleArea.Add(new Rect2(pos1, size1));
        door2.RoomInfo.AisleArea.Add(new Rect2(pos2, size2));
        _roomGrid.AddRect(pos1, size1, true);
        _roomGrid.AddRect(pos2, size2, true);
        return true;
    }
}