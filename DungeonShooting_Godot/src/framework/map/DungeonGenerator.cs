
using System;
using System.Collections;
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
    public RoomInfo StartRoomInfo { get; private set; }

    /// <summary>
    /// 战斗房间
    /// </summary>
    public List<RoomInfo> BattleRoomInfos { get; } = new List<RoomInfo>();
    
    /// <summary>
    /// 结束房间
    /// </summary>
    public List<RoomInfo> EndRoomInfos { get; } = new List<RoomInfo>();

    /// <summary>
    /// boss房间
    /// </summary>
    public List<RoomInfo> BossRoomInfos { get; } = new List<RoomInfo>();
    
    /// <summary>
    /// 奖励房间
    /// </summary>
    public List<RoomInfo> RewardRoomInfos { get; } = new List<RoomInfo>();

    /// <summary>
    /// 商店房间
    /// </summary>
    public List<RoomInfo> ShopRoomInfos { get; } = new List<RoomInfo>();
    
    /// <summary>
    /// 地牢配置数据
    /// </summary>
    public DungeonConfig Config { get; }
    /// <summary>
    /// 所属地牢组
    /// </summary>
    public DungeonRoomGroup RoomGroup { get; }

    /// <summary>
    /// 随机数对象
    /// </summary>
    public SeedRandom Random;

    //用于标记地图上的坐标是否被占用
    private InfiniteGrid<bool> _roomGrid { get; } = new InfiniteGrid<bool>();
    
    //房间id
    private int _id;
    
    //下一个房间类型
    private DungeonRoomType _nextRoomType = DungeonRoomType.None;
    
    //区域限制
    private int _rangeX = 120;
    private int _rangeY = 120;
    
    //找房间失败次数, 过大则会关闭区域限制
    private int _maxFailCount = 10;
    private int _failCount = 0;

    //最大尝试次数
    private int _maxTryCount = 10;
    private int _currMaxLayer = 0;

    //地牢房间规则处理类
    private DungeonRule _rule;
    
    //上一个房间
    private RoomInfo prevRoomInfo = null;
    private readonly List<RoomInfo> _tempList = new List<RoomInfo>();
    
    public DungeonGenerator(DungeonConfig config, SeedRandom seedRandom)
    {
        Config = config;
        Random = seedRandom;
        RoomGroup = GameApplication.Instance.RoomConfig[config.GroupName];
        _rangeX = config.RangeX;
        _rangeY = config.RangeY;

        //验证该组是否满足生成地牢的条件
        var result = DungeonManager.CheckDungeon(config.GroupName);
        if (result.HasError)
        {
            throw new Exception("当前组'" + config.GroupName + "'" + result.ErrorMessage + ", 不能生成地牢!");
        }
        
        Debug.Log("创建地牢生成器, 随机种子: " + Random.Seed);
        RoomGroup.InitWeight(Random);
    }

    /// <summary>
    /// 遍历所有房间
    /// </summary>
    public void EachRoom(Action<RoomInfo> cb)
    {
        EachRoom(StartRoomInfo, cb);
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
    /// 用于协程中的遍历所有房间
    /// </summary>
    public IEnumerator EachRoomCoroutine(Action<RoomInfo> cb)
    {
        return EachRoomCoroutine(StartRoomInfo, cb);
    }
    
    private IEnumerator EachRoomCoroutine(RoomInfo roomInfo, Action<RoomInfo> cb)
    {
        if (roomInfo == null)
        {
            yield break;
        }

        cb(roomInfo);
        foreach (var next in roomInfo.Next)
        {
            yield return EachRoomCoroutine(next, cb);
        }
    }

    /// <summary>
    /// 生成房间
    /// </summary>
    public bool Generate(DungeonRule rule)
    {
        if (StartRoomInfo != null) return false;
        _rule = rule;
        
        //最大尝试次数
        var maxTryCount = 1000;

        //当前尝试次数
        var currTryCount = 0;

        //如果房间数量不够, 就一直生成
        while (!_rule.CanOverGenerator())
        {
            if (_nextRoomType == DungeonRoomType.None)
            {
                _nextRoomType = _rule.GetNextRoomType(prevRoomInfo);
            }
            var nextRoomType = _nextRoomType;
            
            //上一个房间
            var tempPrevRoomInfo = _rule.GetConnectPrevRoom(prevRoomInfo, nextRoomType);
            
            //生成下一个房间
            var errorCode = GenerateRoom(tempPrevRoomInfo, nextRoomType, out var nextRoom);
            if (errorCode == GenerateRoomErrorCode.NoError) //生成成功
            {
                _failCount = 0;
                RoomInfos.Add(nextRoom);
                if (nextRoomType == DungeonRoomType.Inlet)
                {
                    StartRoomInfo = nextRoom;
                }
                else if (nextRoomType == DungeonRoomType.Boss) //boss房间
                {
                    BossRoomInfos.Add(nextRoom);
                }
                else if (nextRoomType == DungeonRoomType.Outlet)
                {
                    EndRoomInfos.Add(nextRoom);
                }
                else if (nextRoomType == DungeonRoomType.Battle)
                {
                    BattleRoomInfos.Add(nextRoom);
                }
                else if (nextRoomType == DungeonRoomType.Reward)
                {
                    RewardRoomInfos.Add(nextRoom);
                }
                else if (nextRoomType == DungeonRoomType.Shop)
                {
                    ShopRoomInfos.Add(nextRoom);
                }
                prevRoomInfo = nextRoom;
                _rule.GenerateRoomSuccess(tempPrevRoomInfo, nextRoom);
                _nextRoomType = _rule.GetNextRoomType(nextRoom);
            }
            else //生成失败
            {
                _rule.GenerateRoomFail(tempPrevRoomInfo, nextRoomType);
                
                //Debug.Log("生成第" + (_count + 1) + "个房间失败! 失败原因: " + errorCode);
                if (errorCode == GenerateRoomErrorCode.OutArea)
                {
                    _failCount++;
                    Debug.Log("超出区域失败次数: " + _failCount);
                    if (_failCount >= _maxFailCount)
                    {
                        //_enableLimitRange = false;
                        _failCount = 0;
                        _rangeX += 50;
                        _rangeY += 50;
                        Debug.Log("生成房间失败次数过多, 增大区域");
                    }
                }
                currTryCount++;
                if (currTryCount >= maxTryCount)
                {
                    return false;
                }
            }
        }
        
        _roomGrid.Clear();
        Debug.Log("房间总数: " + RoomInfos.Count);
        Debug.Log("尝试次数: " + currTryCount);
        return true;
    }

    //生成房间
    private GenerateRoomErrorCode GenerateRoom(RoomInfo prevRoom, DungeonRoomType roomType, out RoomInfo resultRoomInfo)
    {
        // if (_count >= _config.RoomCount)
        // {
        //     resultRoom = null;
        //     return GenerateRoomErrorCode.RoomFull;
        // }

        DungeonRoomSplit roomSplit;
        if (Config.HasDesignatedRoom && Config.DesignatedType == roomType) //执行指定了房间
        {
            roomSplit = Random.RandomChoose(Config.DesignatedRoom);
        }
        else //没有指定房间
        {
            //随机选择一个房间
            var list = RoomGroup.GetRoomList(roomType);
            if (list.Count == 0) //如果没有指定类型的房间, 或者房间数量不够, 就生成战斗房间
            {
                roomSplit = RoomGroup.GetRandomRoom(DungeonRoomType.Battle);
            }
            else
            {
                roomSplit = RoomGroup.GetRandomRoom(roomType);
            }
        }
        
        var room = new RoomInfo(_id, roomType, roomSplit);

        //房间大小
        room.Size = new Vector2I((int)roomSplit.RoomInfo.Size.X, (int)roomSplit.RoomInfo.Size.Y);

        if (prevRoom != null) //表示这不是第一个房间, 就得判断当前位置下的房间是否被遮挡
        {
            room.Layer = prevRoom.Layer + 1;
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
                //下一个房间方向
                var direction = _rule.GetNextRoomDoorDirection(prevRoom, roomType);
                //房间间隔
                var space = _rule.GetNextRoomInterval(prevRoom, roomType, direction);
                //中心偏移
                var offset = _rule.GetNextRoomOffset(prevRoom, roomType, direction);

                //计算房间位置
                if (direction == RoomDirection.Up) //上
                {
                    room.Position = new Vector2I(prevRoom.Position.X + offset,
                        prevRoom.Position.Y - room.Size.Y - space);
                }
                else if (direction == RoomDirection.Right) //右
                {
                    room.Position = new Vector2I(prevRoom.Position.X + prevRoom.Size.Y + space,
                        prevRoom.Position.Y + offset);
                }
                else if (direction == RoomDirection.Down) //下
                {
                    room.Position = new Vector2I(prevRoom.Position.X + offset,
                        prevRoom.Position.Y + prevRoom.Size.Y + space);
                }
                else if (direction == RoomDirection.Left) //左
                {
                    room.Position = new Vector2I(prevRoom.Position.X - room.Size.X - space,
                        prevRoom.Position.Y + offset);
                }

                //是否在限制区域内
                if (Config.EnableLimitRange)
                {
                    if (room.GetHorizontalStart() < -_rangeX || room.GetHorizontalEnd() > _rangeX ||
                        room.GetVerticalStart() < -_rangeY || room.GetVerticalEnd() > _rangeY)
                    {
                        //超出区域, 直接跳出尝试的循环, 返回 null
                        resultRoomInfo = null;
                        return GenerateRoomErrorCode.OutArea;
                    }
                }

                //是否碰到其他房间或者过道
                if (_roomGrid.RectCollision(room.Position - new Vector2I(2, 2), room.Size + new Vector2I(4, 5)))
                {
                    //碰到其他墙壁, 再一次尝试
                    continue;
                    //return GenerateRoomErrorCode.HasCollision;
                }
                
                _roomGrid.SetRect(room.Position, room.Size, true);

                //找门, 与上一个房间是否能连通
                if (!ConnectDoor(prevRoom, room))
                {
                    _roomGrid.RemoveRect(room.Position, room.Size);
                    //Debug.Log("链接通道失败");
                    //房间过道没有连接上, 再一次尝试
                    continue;
                    //return GenerateRoomErrorCode.NoProperDoor;
                }
                break;
            }

            //尝试次数用光了, 还没有找到合适的位置
            if (tryCount >= maxTryCount)
            {
                resultRoomInfo = null;
                return GenerateRoomErrorCode.NoSuitableLocation;
            }
        }
        else //第一个房间
        {
            room.Layer = 0;
            _roomGrid.SetRect(room.Position, room.Size, true);
        }
        
        _id++;
        room.Prev = prevRoom;
        if (prevRoom != null)
        {
            prevRoom.Next.Add(room);
        }
        resultRoomInfo = room;
        return GenerateRoomErrorCode.NoError;
    }

    /// <summary>
    /// 设置上一个房间
    /// </summary>
    public void SetPrevRoom(RoomInfo roomInfo)
    {
        prevRoomInfo = roomInfo;
    }

    /// <summary>
    /// 回滚一个房间
    /// </summary>
    public bool RollbackRoom(RoomInfo roomInfo)
    {
        if (roomInfo.Next.Count > 0)
        {
            Debug.LogError("当前房间还有连接的子房间, 不能回滚!");
            return false;
        }

        if (!roomInfo.CanRollback)
        {
            Debug.LogError("当前房间不能回滚!");
            return false;
        }
        var prevRoom = roomInfo.Prev;
        
        //退掉占用的房间区域和过道占用区域
        _roomGrid.RemoveRect(roomInfo.Position, roomInfo.Size);
        foreach (var rect2 in roomInfo.AisleArea)
        {
            _roomGrid.RemoveRect(rect2.Position, rect2.Size);
        }
        
        //roomInfo.Doors[0].
        if (prevRoom != null)
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
        switch (roomInfo.RoomType)
        {
            case DungeonRoomType.Battle:
                BattleRoomInfos.Remove(roomInfo);
                break;
            case DungeonRoomType.Inlet:
                StartRoomInfo = null;
                break;
            case DungeonRoomType.Outlet:
                EndRoomInfos.Remove(roomInfo);
                break;
            case DungeonRoomType.Boss:
                BossRoomInfos.Remove(roomInfo);
                break;
            case DungeonRoomType.Reward:
                RewardRoomInfos.Remove(roomInfo);
                break;
            case DungeonRoomType.Shop:
                ShopRoomInfos.Remove(roomInfo);
                break;
            case DungeonRoomType.Event:
                break;
        }
        
        roomInfo.Destroy();
        _id--;
        _nextRoomType = DungeonRoomType.None;
        SetPrevRoom(prevRoom);
        return true;
    }

    /// <summary>
    /// 寻找层级最高的房间
    /// </summary>
    /// <param name="roomType">指定房间类型, 如果传 None 则表示选择所有类型房间</param>
    /// <param name="exclude">排除的房间</param>
    public RoomInfo FindMaxLayerRoom(DungeonRoomType roomType, List<RoomInfo> exclude = null)
    {
        RoomInfo temp = null;
        foreach (var roomInfo in RoomInfos)
        {
            if (roomInfo.CanRollback)
            {
                continue;
            }
            if ((temp == null || roomInfo.Layer > temp.Layer) && (roomType == DungeonRoomType.None || (roomInfo.RoomType & roomType) != DungeonRoomType.None) && (exclude == null || !exclude.Contains(roomInfo)))
            {
                temp = roomInfo;
            }
        }

        return temp;
    }

    /// <summary>
    /// 随机抽取层级小于 layer 的房间
    /// </summary>
    /// <param name="roomType">指定房间类型, 如果传 None 则表示选择所有类型房间</param>
    /// <param name="layer"></param>
    /// <param name="exclude">排除的房间</param>
    public RoomInfo RandomRoomLessThanLayer(DungeonRoomType roomType, int layer, List<RoomInfo> exclude = null)
    {
        _tempList.Clear();
        foreach (var roomInfo in RoomInfos)
        {
            if (roomInfo.CanRollback)
            {
                continue;
            }
            if (roomInfo.Layer < layer && (roomType == DungeonRoomType.None || (roomInfo.RoomType & roomType) != DungeonRoomType.None) && (exclude == null || !exclude.Contains(roomInfo)))
            {
                _tempList.Add(roomInfo);
            }
        }

        return Random.RandomChoose(_tempList);
    }
    
    /// <summary>
    /// 随机抽取层级大于 layer 的房间
    /// </summary>
    /// <param name="roomType">指定房间类型, 如果传 None 则表示选择所有类型房间</param>
    /// <param name="layer"></param>
    /// <param name="exclude">排除的房间</param>
    public RoomInfo RandomRoomGreaterThanLayer(DungeonRoomType roomType, int layer, List<RoomInfo> exclude = null)
    {
        _tempList.Clear();
        foreach (var roomInfo in RoomInfos)
        {
            if (roomInfo.CanRollback)
            {
                continue;
            }
            if (roomInfo.Layer > layer && (roomType == DungeonRoomType.None || (roomInfo.RoomType & roomType) != DungeonRoomType.None) && (exclude == null || !exclude.Contains(roomInfo)))
            {
                _tempList.Add(roomInfo);
            }
        }

        return Random.RandomChoose(_tempList);
    }
    
    /// <summary>
    /// 随机抽取房间
    /// </summary>
    /// <param name="roomType">指定房间类型, 如果传 None 则表示选择所有类型房间</param>
    public RoomInfo GetRandomRoom(DungeonRoomType roomType)
    {
        _tempList.Clear();
        foreach (var roomInfo in RoomInfos)
        {
            if (roomInfo.CanRollback)
            {
                continue;
            }

            if (roomType == DungeonRoomType.None || (roomInfo.RoomType & roomType) != DungeonRoomType.None)
            {
                _tempList.Add(roomInfo);
            }
        }

        return Random.RandomChoose(_tempList);
    }

    /// <summary>
    /// 提交所有可以回滚的房间
    /// </summary>
    public void SubmitCanRollbackRoom()
    {
        foreach (var roomInfo in RoomInfos)
        {
            roomInfo.CanRollback = false;
        }
    }
    
    /// <summary>
    /// 找两个房间的门
    /// </summary>
    private bool ConnectDoor(RoomInfo roomInfo, RoomInfo nextRoomInfo)
    {
        //门描述
        var roomDoor = new RoomDoorInfo();
        var nextRoomDoor = new RoomDoorInfo();
        roomDoor.RoomInfo = roomInfo;
        roomDoor.IsForward = true;
        nextRoomDoor.RoomInfo = nextRoomInfo;
        roomDoor.ConnectRoom = nextRoomInfo;
        roomDoor.ConnectDoor = nextRoomDoor;
        nextRoomDoor.ConnectRoom = roomInfo;
        nextRoomDoor.ConnectDoor = roomDoor;

        //先寻找直通门
        if (Random.RandomBoolean())
        {
            //直行通道, 优先纵轴
            if (TryConnectVerticalDoor(roomInfo, roomDoor, nextRoomInfo, nextRoomDoor)
                || TryConnectHorizontalDoor(roomInfo, roomDoor, nextRoomInfo, nextRoomDoor))
            {
                return true;
            }
        }
        else
        {
            //直行通道, 优先横轴
            if (TryConnectHorizontalDoor(roomInfo, roomDoor, nextRoomInfo, nextRoomDoor)
                || TryConnectVerticalDoor(roomInfo, roomDoor, nextRoomInfo, nextRoomDoor))
            {
                return true;
            }
        }

        //包含1个拐角的通道
        return TryConnectCrossDoor(roomInfo, roomDoor, nextRoomInfo, nextRoomDoor);
        //包含2个拐角的通道 (后面再开发)
    }

    /// <summary>
    /// 尝试寻找纵轴方向上两个房间的连通的门, 只查找直线通道, 返回是否找到
    /// </summary>
    private bool TryConnectVerticalDoor(RoomInfo roomInfo, RoomDoorInfo roomDoor, RoomInfo nextRoomInfo, RoomDoorInfo nextRoomDoor)
    {
        var overlapX = Mathf.Min(roomInfo.GetHorizontalEnd(), nextRoomInfo.GetHorizontalEnd()) -
                       Mathf.Max(roomInfo.GetHorizontalStart(), nextRoomInfo.GetHorizontalStart());
        //这种情况下x轴有重叠
        if (overlapX >= 6)
        {
            //填充通通道地板格子
            var floorCell = new HashSet<Vector2I>();
            
            //找到重叠区域
            var rangeList = FindPassage(roomInfo, nextRoomInfo, 
                roomInfo.GetVerticalStart() < nextRoomInfo.GetVerticalStart() ? DoorDirection.S : DoorDirection.N);
            
            while (rangeList.Count > 0)
            {
                //找到重叠区域
                var range = Random.RandomChooseAndRemove(rangeList);
                var x = Random.RandomRangeInt(range.X, range.Y) + 2;
                
                if (roomInfo.GetVerticalStart() < nextRoomInfo.GetVerticalStart()) //room在上, nextRoom在下
                {
                    roomDoor.Direction = DoorDirection.S;
                    nextRoomDoor.Direction = DoorDirection.N;
                    roomDoor.OriginPosition = new Vector2I(x, roomInfo.GetVerticalDoorEnd());
                    nextRoomDoor.OriginPosition = new Vector2I(x, nextRoomInfo.GetVerticalDoorStart());

                    var sv = roomInfo.GetVerticalDoorEnd() - 1;
                    var ev = nextRoomInfo.GetVerticalDoorStart() + 1;
                    for (var i = sv; i < ev; i++)
                    {
                        floorCell.Add(new Vector2I(x + 1, i));
                        floorCell.Add(new Vector2I(x + 2, i));
                    }
                }
                else //room在下, nextRoom在上
                {
                    roomDoor.Direction = DoorDirection.N;
                    nextRoomDoor.Direction = DoorDirection.S;
                    roomDoor.OriginPosition = new Vector2I(x, roomInfo.GetVerticalDoorStart());
                    nextRoomDoor.OriginPosition = new Vector2I(x, nextRoomInfo.GetVerticalDoorEnd());

                    var sv = nextRoomInfo.GetVerticalDoorEnd() - 1;
                    var ev = roomInfo.GetVerticalDoorStart() + 1;
                    for (var i = sv; i < ev; i++)
                    {
                        floorCell.Add(new Vector2I(x + 1, i));
                        floorCell.Add(new Vector2I(x + 2, i));
                    }
                }

                //判断门之间的通道是否有物体碰到
                if (!AddCorridorToGridRange(roomDoor, nextRoomDoor))
                {
                    //此门不能连通
                    floorCell.Clear();
                    continue;
                }

                //没有撞到物体
                roomInfo.Doors.Add(roomDoor);
                nextRoomInfo.Doors.Add(nextRoomDoor);

                roomDoor.AisleFloorCell = floorCell;
                nextRoomDoor.AisleFloorCell = floorCell;
                roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);;
                nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
                return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// 尝试寻找横轴方向上两个房间的连通的门, 只查找直线通道, 返回是否找到
    /// </summary>
    private bool TryConnectHorizontalDoor(RoomInfo roomInfo, RoomDoorInfo roomDoor, RoomInfo nextRoomInfo, RoomDoorInfo nextRoomDoor)
    {
        var overlapY = Mathf.Min(roomInfo.GetVerticalEnd(), nextRoomInfo.GetVerticalEnd()) -
                       Mathf.Max(roomInfo.GetVerticalStart(), nextRoomInfo.GetVerticalStart());
        //这种情况下y轴有重叠
        if (overlapY >= 6)
        {
            //填充通通道地板格子
            var floorCell = new HashSet<Vector2I>();
            
            //找到重叠区域
            var rangeList = FindPassage(roomInfo, nextRoomInfo, 
                roomInfo.GetHorizontalStart() < nextRoomInfo.GetHorizontalStart() ? DoorDirection.E : DoorDirection.W);

            while (rangeList.Count > 0)
            {
                //找到重叠区域
                var range = Random.RandomChooseAndRemove(rangeList);
                var y = Random.RandomRangeInt(range.X, range.Y) + 3;
                
                if (roomInfo.GetHorizontalStart() < nextRoomInfo.GetHorizontalStart()) //room在左, nextRoom在右
                {
                    roomDoor.Direction = DoorDirection.E;
                    nextRoomDoor.Direction = DoorDirection.W;
                    roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorEnd(), y);
                    nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorStart(), y);

                    var sv = roomInfo.GetHorizontalDoorEnd() - 1;
                    var ev = nextRoomInfo.GetHorizontalDoorStart() + 1;
                    for (var i = sv; i < ev; i++)
                    {
                        floorCell.Add(new Vector2I(i, y + 2));
                    }
                }
                else //room在右, nextRoom在左
                {
                    roomDoor.Direction = DoorDirection.W;
                    nextRoomDoor.Direction = DoorDirection.E;
                    roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorStart(), y);
                    nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorEnd(), y);

                    var sv = nextRoomInfo.GetHorizontalDoorEnd() - 1;
                    var ev = roomInfo.GetHorizontalDoorStart() + 1;
                    for (var i = sv; i < ev; i++)
                    {
                        floorCell.Add(new Vector2I(i, y + 2));
                    }
                }

                //判断门之间的通道是否有物体碰到
                if (!AddCorridorToGridRange(roomDoor, nextRoomDoor))
                {
                    //此门不能连通
                    floorCell.Clear();
                    continue;
                }

                //没有撞到物体
                roomInfo.Doors.Add(roomDoor);
                nextRoomInfo.Doors.Add(nextRoomDoor);
                
                roomDoor.AisleFloorCell = floorCell;
                nextRoomDoor.AisleFloorCell = floorCell;
                roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);;
                nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 尝试寻找包含拐角的两个房间的连通的门, 返回是否找到
    /// </summary>
    private bool TryConnectCrossDoor(RoomInfo roomInfo, RoomDoorInfo roomDoor, RoomInfo nextRoomInfo, RoomDoorInfo nextRoomDoor)
    {
        //焦点
        Vector2I cross = default;

        if (roomInfo.GetHorizontalStart() > nextRoomInfo.GetHorizontalStart())
        {
            if (roomInfo.GetVerticalStart() > nextRoomInfo.GetVerticalStart())
            {
                if (Random.RandomBoolean()) //↑ //→
                {
                    if (!TryConnect_NE_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_WS_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
                else //← //↓
                {
                    if (!TryConnect_WS_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_NE_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (Random.RandomBoolean()) //↓ //→
                {
                    if (!TryConnect_SE_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_WN_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
                else //← //↑
                {
                    if (!TryConnect_WN_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_SE_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            if (roomInfo.GetVerticalStart() > nextRoomInfo.GetVerticalStart()) //→ //↓
            {
                if (Random.RandomBoolean())
                {
                    if (!TryConnect_ES_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_NW_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
                else //↑ //←
                {
                    if (!TryConnect_NW_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_ES_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (Random.RandomBoolean()) //→ //↑
                {
                    if (!TryConnect_EN_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_SW_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross))
                    {
                        return false;
                    }
                }
                else //↓ //←
                {
                    if (!TryConnect_SW_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross) &&
                        !TryConnect_EN_Door(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref cross))
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

        roomInfo.Doors.Add(roomDoor);
        nextRoomInfo.Doors.Add(nextRoomDoor);
        return true;
    }

    private bool FindCrossPassage(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor,ref int offset1, ref int offset2)
    {
        var room1 = roomInfo.RoomSplit.RoomInfo;
        var room2 = nextRoomInfo.RoomSplit.RoomInfo;
        
        int? temp1 = null;
        int? temp2 = null;

        foreach (var areaInfo1 in room1.GetCompletionDoorArea())
        {
            if (areaInfo1.Direction == roomDoor.Direction)
            {
                FindCrossPassage_Area(areaInfo1, roomInfo, nextRoomInfo, ref temp1);
            }
        }
        
        if (temp1 == null)
        {
            return false;
        }

        foreach (var areaInfo2 in room2.GetCompletionDoorArea())
        {
            if (areaInfo2.Direction == nextRoomDoor.Direction)
            {
                FindCrossPassage_Area(areaInfo2, nextRoomInfo, roomInfo, ref temp2);
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
            var p1 = num + GetDoorAreaInfoStart(areaInfo) / GameConfig.TileCellSize;
            var p2 = num + GetDoorAreaInfoEnd(areaInfo) / GameConfig.TileCellSize;

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
            var p1 = num + GetDoorAreaInfoStart(areaInfo) / GameConfig.TileCellSize;
            var p2 = num + GetDoorAreaInfoEnd(areaInfo) / GameConfig.TileCellSize;

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

    private bool TryConnect_NE_Door(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2I cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.N; //↑
        nextRoomDoor.Direction = DoorDirection.E; //→

        if (!FindCrossPassage(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        offset1 += 1;
        offset2 += 1;
        roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorStart() + offset1, roomInfo.GetVerticalDoorStart());
        nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorEnd(), nextRoomInfo.GetVerticalDoorStart() + offset2);
        cross = new Vector2I(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);

        var floorCell = new HashSet<Vector2I>();

        //纵轴地板
        for (var y = cross.Y + 3; y <= roomDoor.OriginPosition.Y; y++)
        {
            floorCell.Add(new Vector2I(roomDoor.OriginPosition.X + 1, y));
            floorCell.Add(new Vector2I(roomDoor.OriginPosition.X + 2, y));
        }
        //横轴地板
        for (var x = nextRoomDoor.OriginPosition.X - 1; x <= cross.X; x++)
        {
            floorCell.Add(new Vector2I(x, nextRoomDoor.OriginPosition.Y + 2));
        }
        //交叉点地板
        floorCell.Add(new Vector2I(cross.X + 1, cross.Y + 2));
        floorCell.Add(new Vector2I(cross.X + 2, cross.Y + 2));
        
        roomDoor.AisleFloorCell = floorCell;
        nextRoomDoor.AisleFloorCell = floorCell;
        roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);
        nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
        return true;
    }

    private bool TryConnect_WS_Door(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2I cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.W; //←
        nextRoomDoor.Direction = DoorDirection.S; //↓
                
        if (!FindCrossPassage(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        offset1 += 1;
        offset2 += 1;
        roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorStart(), roomInfo.GetVerticalDoorStart() + offset1);
        nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorStart() + offset2, nextRoomInfo.GetVerticalDoorEnd());
        cross = new Vector2I(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
        
        var floorCell = new HashSet<Vector2I>();

        //横轴地板
        for (var x = cross.X + 3; x <= roomDoor.OriginPosition.X; x++)
        {
            floorCell.Add(new Vector2I(x, roomDoor.OriginPosition.Y + 2));
        }
        //纵轴地板
        for (var y = nextRoomDoor.OriginPosition.Y - 1; y <= cross.Y + 1; y++)
        {
            floorCell.Add(new Vector2I(nextRoomDoor.OriginPosition.X + 1, y));
            floorCell.Add(new Vector2I(nextRoomDoor.OriginPosition.X + 2, y));
        }

        //交叉点地板
        floorCell.Add(new Vector2I(cross.X + 1, cross.Y + 2));
        floorCell.Add(new Vector2I(cross.X + 2, cross.Y + 2));
        
        roomDoor.AisleFloorCell = floorCell;
        nextRoomDoor.AisleFloorCell = floorCell;
        roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);
        nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
        return true;
    }

    private bool TryConnect_SE_Door(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2I cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.S; //↓
        nextRoomDoor.Direction = DoorDirection.E; //→
                    
        if (!FindCrossPassage(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        offset1 += 1;
        offset2 += 1;
        roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorStart() + offset1, roomInfo.GetVerticalDoorEnd());
        nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorEnd(), nextRoomInfo.GetVerticalDoorStart() + offset2);
        cross = new Vector2I(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
        
        var floorCell = new HashSet<Vector2I>();
        
        //纵轴地板
        for (var y = roomDoor.OriginPosition.Y - 1; y <= cross.Y + 1; y++)
        {
            floorCell.Add(new Vector2I(roomDoor.OriginPosition.X + 1, y));
            floorCell.Add(new Vector2I(roomDoor.OriginPosition.X + 2, y));
        }
        
        //横轴地板
        for (var x = nextRoomDoor.OriginPosition.X - 1; x <= cross.X; x++)
        {
            floorCell.Add(new Vector2I(x, nextRoomDoor.OriginPosition.Y + 2));
        }
        
        //交叉点地板
        floorCell.Add(new Vector2I(cross.X + 1, cross.Y + 2));
        floorCell.Add(new Vector2I(cross.X + 2, cross.Y + 2));
        
        roomDoor.AisleFloorCell = floorCell;
        nextRoomDoor.AisleFloorCell = floorCell;
        roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);
        nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
        return true;
    }

    private bool TryConnect_WN_Door(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2I cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.W; //←
        nextRoomDoor.Direction = DoorDirection.N; //↑
                    
        if (!FindCrossPassage(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        offset1 += 1;
        offset2 += 1;
        roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorStart(), roomInfo.GetVerticalDoorStart() + offset1); //
        nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorStart() + offset2, nextRoomInfo.GetVerticalDoorStart());
        cross = new Vector2I(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
        
        var floorCell = new HashSet<Vector2I>();
        
        //横轴地板
        for (var x = cross.X + 3; x <= roomDoor.OriginPosition.X; x++)
        {
            floorCell.Add(new Vector2I(x, roomDoor.OriginPosition.Y + 2));
        }
        
        //纵轴地板
        for (var y = cross.Y + 3; y <= nextRoomDoor.OriginPosition.Y; y++)
        {
            floorCell.Add(new Vector2I(nextRoomDoor.OriginPosition.X + 1, y));
            floorCell.Add(new Vector2I(nextRoomDoor.OriginPosition.X + 2, y));
        }
        
        //交叉点地板
        floorCell.Add(new Vector2I(cross.X + 1, cross.Y + 2));
        floorCell.Add(new Vector2I(cross.X + 2, cross.Y + 2));
        
        roomDoor.AisleFloorCell = floorCell;
        nextRoomDoor.AisleFloorCell = floorCell;
        roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);
        nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
        return true;
    }
    
    private bool TryConnect_ES_Door(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2I cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.E; //→
        nextRoomDoor.Direction = DoorDirection.S; //↓

        if (!FindCrossPassage(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        offset1 += 1;
        offset2 += 1;
        roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorEnd(), roomInfo.GetVerticalDoorStart() + offset1);
        nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorStart() + offset2, nextRoomInfo.GetVerticalDoorEnd());
        cross = new Vector2I(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
        
        var floorCell = new HashSet<Vector2I>();
        
        //横轴地板
        for (var x = roomDoor.OriginPosition.X - 1; x <= cross.X; x++)
        {
            floorCell.Add(new Vector2I(x, roomDoor.OriginPosition.Y + 2));
        }
        
        //纵轴地板
        for (var y = nextRoomDoor.OriginPosition.Y - 1; y <= cross.Y + 1; y++)
        {
            floorCell.Add(new Vector2I(nextRoomDoor.OriginPosition.X + 1, y));
            floorCell.Add(new Vector2I(nextRoomDoor.OriginPosition.X + 2, y));
        }
        
        //交叉点地板
        floorCell.Add(new Vector2I(cross.X + 1, cross.Y + 2));
        floorCell.Add(new Vector2I(cross.X + 2, cross.Y + 2));
        
        roomDoor.AisleFloorCell = floorCell;
        nextRoomDoor.AisleFloorCell = floorCell;
        roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);
        nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
        return true;
    }
    
    private bool TryConnect_NW_Door(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2I cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.N; //↑
        nextRoomDoor.Direction = DoorDirection.W; //←

        if (!FindCrossPassage(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        offset1 += 1;
        offset2 += 1;
        roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorStart() + offset1, roomInfo.GetVerticalDoorStart());
        nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorStart(), nextRoomInfo.GetVerticalDoorStart() + offset2);
        cross = new Vector2I(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
        
        var floorCell = new HashSet<Vector2I>();
        
        //纵轴地板
        for (var y = cross.Y + 3; y <= roomDoor.OriginPosition.Y; y++)
        {
            floorCell.Add(new Vector2I(roomDoor.OriginPosition.X + 1, y));
            floorCell.Add(new Vector2I(roomDoor.OriginPosition.X + 2, y));
        }
        
        //横轴地板
        for (var x = cross.X + 3; x <= nextRoomDoor.OriginPosition.X; x++)
        {
            floorCell.Add(new Vector2I(x, nextRoomDoor.OriginPosition.Y + 2));
        }
        
        //交叉点地板
        floorCell.Add(new Vector2I(cross.X + 1, cross.Y + 2));
        floorCell.Add(new Vector2I(cross.X + 2, cross.Y + 2));
        
        roomDoor.AisleFloorCell = floorCell;
        nextRoomDoor.AisleFloorCell = floorCell;
        roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);
        nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
        return true;
    }
    
    private bool TryConnect_EN_Door(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2I cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.E; //→
        nextRoomDoor.Direction = DoorDirection.N; //↑

        if (!FindCrossPassage(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }
        
        offset1 += 1;
        offset2 += 2;
        roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorEnd(), roomInfo.GetVerticalDoorStart() + offset1);
        nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalStart() + offset2, nextRoomInfo.GetVerticalDoorStart());
        cross = new Vector2I(nextRoomDoor.OriginPosition.X, roomDoor.OriginPosition.Y);
        
        var floorCell = new HashSet<Vector2I>();
        
        //横轴地板
        for (var x = roomDoor.OriginPosition.X - 1; x <= cross.X; x++)
        {
            floorCell.Add(new Vector2I(x, roomDoor.OriginPosition.Y + 2));
        }
        
        //纵轴地板
        for (var y = cross.Y + 3; y <= nextRoomDoor.OriginPosition.Y; y++)
        {
            floorCell.Add(new Vector2I(nextRoomDoor.OriginPosition.X + 1, y));
            floorCell.Add(new Vector2I(nextRoomDoor.OriginPosition.X + 2, y));
        }
        
        //交叉点地板
        floorCell.Add(new Vector2I(cross.X + 1, cross.Y + 2));
        floorCell.Add(new Vector2I(cross.X + 2, cross.Y + 2));
        
        roomDoor.AisleFloorCell = floorCell;
        nextRoomDoor.AisleFloorCell = floorCell;
        roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);
        nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
        return true;
    }

    private bool TryConnect_SW_Door(RoomInfo roomInfo, RoomInfo nextRoomInfo, RoomDoorInfo roomDoor, RoomDoorInfo nextRoomDoor, ref Vector2I cross)
    {
        var offset1 = 0;
        var offset2 = 0;
        roomDoor.Direction = DoorDirection.S; //↓
        nextRoomDoor.Direction = DoorDirection.W; //←

        if (!FindCrossPassage(roomInfo, nextRoomInfo, roomDoor, nextRoomDoor, ref offset1, ref offset2))
        {
            return false;
        }

        offset1 += 1;
        offset2 += 1;
        roomDoor.OriginPosition = new Vector2I(roomInfo.GetHorizontalDoorStart() + offset1, roomInfo.GetVerticalDoorEnd());
        nextRoomDoor.OriginPosition = new Vector2I(nextRoomInfo.GetHorizontalDoorStart(), nextRoomInfo.GetVerticalDoorStart() + offset2);
        cross = new Vector2I(roomDoor.OriginPosition.X, nextRoomDoor.OriginPosition.Y);
        
        var floorCell = new HashSet<Vector2I>();
        
        //纵轴地板
        for (var y = roomDoor.OriginPosition.Y - 1; y <= cross.Y + 1; y++)
        {
            floorCell.Add(new Vector2I(roomDoor.OriginPosition.X + 1, y));
            floorCell.Add(new Vector2I(roomDoor.OriginPosition.X + 2, y));
        }
        
        //横轴地板
        for (var x = cross.X + 3; x <= nextRoomDoor.OriginPosition.X; x++)
        {
            floorCell.Add(new Vector2I(x, nextRoomDoor.OriginPosition.Y + 2));
        }
        
        //交叉点地板
        floorCell.Add(new Vector2I(cross.X + 1, cross.Y + 2));
        floorCell.Add(new Vector2I(cross.X + 2, cross.Y + 2));
        
        roomDoor.AisleFloorCell = floorCell;
        nextRoomDoor.AisleFloorCell = floorCell;
        roomDoor.AisleFloorRect = Utils.CalcRect(floorCell);
        nextRoomDoor.AisleFloorRect = roomDoor.AisleFloorRect;
        return true;
    }

    /// <summary>
    /// 查找房间的连接通道, 函数返回是否找到对应的门, 通过 result 返回 x/y 轴坐标
    /// </summary>
    /// <param name="roomInfo">第一个房间</param>
    /// <param name="nextRoomInfo">第二个房间</param>
    /// <param name="direction">第一个房间连接方向</param>
    private List<Vector2I> FindPassage(RoomInfo roomInfo, RoomInfo nextRoomInfo, DoorDirection direction)
    {
        var room1 = roomInfo.RoomSplit.RoomInfo;
        var room2 = nextRoomInfo.RoomSplit.RoomInfo;
        
        //用于存储符合生成条件的区域
        var rangeList = new List<Vector2I>();
        
        foreach (var doorAreaInfo1 in room1.GetCompletionDoorArea())
        {
            if (doorAreaInfo1.Direction == direction)
            {
                //第二个门的方向
                var direction2 = GetReverseDirection(direction);
                
                foreach (var doorAreaInfo2 in room2.GetCompletionDoorArea())
                {
                    if (doorAreaInfo2.Direction == direction2)
                    {
                        Vector2 range;
                        if (direction == DoorDirection.E || direction == DoorDirection.W) //第二个门向← 或者 第二个门向→
                        {
                            range = CalcOverlapRange(
                                roomInfo.GetVerticalStart() * GameConfig.TileCellSize + GetDoorAreaInfoStart(doorAreaInfo1), roomInfo.GetVerticalStart() * GameConfig.TileCellSize + GetDoorAreaInfoEnd(doorAreaInfo1),
                                nextRoomInfo.GetVerticalStart() * GameConfig.TileCellSize + GetDoorAreaInfoStart(doorAreaInfo2), nextRoomInfo.GetVerticalStart() * GameConfig.TileCellSize + GetDoorAreaInfoEnd(doorAreaInfo2)
                            );
                        }
                        else //第二个门向↑ 或者 第二个门向↓
                        {
                            range = CalcOverlapRange(
                                roomInfo.GetHorizontalStart() * GameConfig.TileCellSize + GetDoorAreaInfoStart(doorAreaInfo1), roomInfo.GetHorizontalStart() * GameConfig.TileCellSize + GetDoorAreaInfoEnd(doorAreaInfo1),
                                nextRoomInfo.GetHorizontalStart() * GameConfig.TileCellSize + GetDoorAreaInfoStart(doorAreaInfo2), nextRoomInfo.GetHorizontalStart() * GameConfig.TileCellSize + GetDoorAreaInfoEnd(doorAreaInfo2)
                            );
                        }
                        //交集范围够生成门
                        if (range.Y - range.X >= GameConfig.CorridorWidth * GameConfig.TileCellSize)
                        {
                            rangeList.Add(new Vector2I((int)(range.X / GameConfig.TileCellSize), (int)(range.Y / GameConfig.TileCellSize) - GameConfig.CorridorWidth));
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
        var pos = new Vector2I(Mathf.Min(point1.X, point2.X), Mathf.Min(point1.Y, point2.Y));
        var size = new Vector2I(
            point1.X == point2.X ? GameConfig.CorridorWidth : Mathf.Abs(point1.X - point2.X),
            point1.Y == point2.Y ? GameConfig.CorridorWidth : Mathf.Abs(point1.Y - point2.Y)
        );

        Vector2I collPos;
        Vector2I collSize;
        if (point1.X == point2.X) //纵向加宽, 防止贴到其它墙
        {
            pos.Y += 1;
            size.Y -= 3;
            collPos = new Vector2I(pos.X - GameConfig.RoomSpace, pos.Y);
            collSize = new Vector2I(size.X + GameConfig.RoomSpace * 2, size.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            pos.X += 1;
            size.X -= 2;
            collPos = new Vector2I(pos.X, pos.Y - GameConfig.RoomSpace);
            collSize = new Vector2I(size.X, size.Y + GameConfig.RoomSpace * 2);
        }

        if (_roomGrid.RectCollision(collPos, collSize))
        {
            return false;
        }

        door2.RoomInfo.AisleArea.Add(new Rect2I(pos, size));
        _roomGrid.SetRect(pos, size, true);
        return true;
    }

    //将两个门间的过道占用数据存入RoomGrid, 该重载加入拐角点
    private bool AddCorridorToGridRange(RoomDoorInfo door1, RoomDoorInfo door2, Vector2I cross)
    {
        if (_roomGrid.RectCollision(cross - new Vector2I(1, 2), new Vector2I(GameConfig.CorridorWidth + 2, GameConfig.CorridorWidth + 3)))
        {
            return false;
        }
        
        var point1 = door1.OriginPosition;
        var point2 = door2.OriginPosition;
        var pos1 = new Vector2I(Mathf.Min(point1.X, cross.X), Mathf.Min(point1.Y, cross.Y));
        var size1 = new Vector2I(
            point1.X == cross.X ? GameConfig.CorridorWidth : Mathf.Abs(point1.X - cross.X),
            point1.Y == cross.Y ? GameConfig.CorridorWidth : Mathf.Abs(point1.Y - cross.Y)
        );
        var pos2 = new Vector2I(Mathf.Min(point2.X, cross.X), Mathf.Min(point2.Y, cross.Y));
        var size2 = new Vector2I(
            point2.X == cross.X ? GameConfig.CorridorWidth : Mathf.Abs(point2.X - cross.X),
            point2.Y == cross.Y ? GameConfig.CorridorWidth : Mathf.Abs(point2.Y - cross.Y)
        );

        Vector2I collPos1;
        Vector2I collSize1;
        if (point1.X == cross.X) //纵向加宽, 防止贴到其它墙
        {
            if (door1.Direction == DoorDirection.N)
            {
                size1.Y -= 2;
            }
            else
            {
                pos1.Y += 1;
                size1.Y -= 1;
            }
            collPos1 = new Vector2I(pos1.X - GameConfig.RoomSpace, pos1.Y);
            collSize1 = new Vector2I(size1.X + GameConfig.RoomSpace * 2, size1.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            if (door1.Direction == DoorDirection.E)
            {
                pos1.X += 1;
                size1.X -= 1;
            }
            else
            {
                size1.X -= 1;
            }
            collPos1 = new Vector2I(pos1.X, pos1.Y - GameConfig.RoomSpace);
            collSize1 = new Vector2I(size1.X, size1.Y + GameConfig.RoomSpace * 2);
        }

        if (_roomGrid.RectCollision(collPos1, collSize1))
        {
            return false;
        }

        Vector2I collPos2;
        Vector2I collSize2;
        if (point2.X == cross.X) //纵向加宽, 防止贴到其它墙
        {
            if (door2.Direction == DoorDirection.N)
            {
                size2.Y -= 2;
            }
            else
            {
                pos2.Y += 1;
                size2.Y -= 1;
            }
            collPos2 = new Vector2I(pos2.X - GameConfig.RoomSpace, pos2.Y);
            collSize2 = new Vector2I(size2.X + GameConfig.RoomSpace * 2, size2.Y);
        }
        else //横向加宽, 防止贴到其它墙
        {
            if (door2.Direction == DoorDirection.E)
            {
                pos2.X += 1;
                size2.X -= 1;
            }
            else
            {
                size2.X -= 1;
            }
            collPos2 = new Vector2I(pos2.X, pos2.Y - GameConfig.RoomSpace);
            collSize2 = new Vector2I(size2.X, size2.Y + GameConfig.RoomSpace * 2);
        }

        if (_roomGrid.RectCollision(collPos2, collSize2))
        {
            return false;
        }

        door2.RoomInfo.AisleArea.Add(new Rect2I(pos1, size1));
        door2.RoomInfo.AisleArea.Add(new Rect2I(pos2, size2));
        _roomGrid.SetRect(pos1, size1, true);
        _roomGrid.SetRect(pos2, size2, true);
        return true;
    }

    private int GetDoorAreaInfoStart(DoorAreaInfo areaInfo)
    {
        return areaInfo.Start;
    }

    private int GetDoorAreaInfoEnd(DoorAreaInfo areaInfo)
    {
        return areaInfo.End;
    }
}