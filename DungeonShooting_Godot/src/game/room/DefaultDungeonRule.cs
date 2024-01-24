
using System.Collections.Generic;
using Godot;

/// <summary>
/// 默认实现地牢房间规则
/// </summary>
public class DefaultDungeonRule : DungeonRule
{
    //用于排除上一级房间
    private List<RoomInfo> excludePrevRoom = new List<RoomInfo>();
    
    //战斗房间尝试链接次数
    private int battleTryCount = 0;
    private int battleMaxTryCount = 3;

    //结束房间尝试链接次数
    private int outletTryCount = 0;
    
    private readonly RoomDirection[] _roomDirections = new []{ RoomDirection.Up, RoomDirection.Down, RoomDirection.Left, RoomDirection.Right };
    
    public DefaultDungeonRule(DungeonGenerator generator) : base(generator)
    {
    }

    public override bool CanOverGenerator()
    {
        return Generator.BattleRoomInfos.Count >= Config.BattleRoomCount && Generator.EndRoomInfos.Count > 0;
    }

    public override RoomInfo GetConnectPrevRoom(RoomInfo prevRoom, DungeonRoomType nextRoomType)
    {
        if (nextRoomType == DungeonRoomType.Inlet)
        {
            return null;
        }
        else if (nextRoomType == DungeonRoomType.Boss)
        {
            return Generator.FindMaxLayerRoom(excludePrevRoom);
        }
        else if (nextRoomType == DungeonRoomType.Outlet || nextRoomType == DungeonRoomType.Reward || nextRoomType == DungeonRoomType.Shop || nextRoomType == DungeonRoomType.Event)
        {
            return prevRoom;
        }
        else if (nextRoomType == DungeonRoomType.Battle)
        {
            if (battleTryCount < battleMaxTryCount)
            {
                if (prevRoom != null && prevRoom.Layer >= Config.MaxLayer - 1) //层数太高, 下一个房间生成在低层级
                {
                    return Generator.RandomRoomLessThanLayer(Mathf.Max(1, Config.MaxLayer / 2));
                }

                return prevRoom;
            }
        }
        
        return Generator.GetRandomRoom();
    }

    public override DungeonRoomType GetNextRoomType(RoomInfo prevRoom)
    {
        if (Generator.StartRoomInfo == null) //生成第一个房间
        {
            return DungeonRoomType.Inlet;
        }
        // else if (Generator.BattleRoomInfos.Count == 0) //奖励房间
        // {
        //     return DungeonRoomType.Reward;
        // }
        else if (prevRoom.RoomType == DungeonRoomType.Boss) //boss房间后生成结束房间
        {
            return DungeonRoomType.Outlet;
        }
        else if (Generator.BattleRoomInfos.Count >= Config.BattleRoomCount) //战斗房间已满
        {
            if (Generator.BossRoomInfos.Count < Config.BossRoomCount) //最后一个房间是boss房间
            {
                if (RoomGroup.BossList.Count == 0) //没有预设boss房间
                {
                    return DungeonRoomType.Battle;
                }
                //生成boss房间
                return DungeonRoomType.Boss;
            }
        }
        return DungeonRoomType.Battle;
    }

    public override void GenerateRoomSuccess(RoomInfo prevRoom, RoomInfo roomInfo)
    {
        if (roomInfo.RoomType == DungeonRoomType.Boss) //boss房间
        {
            roomInfo.CanRollback = true;
            excludePrevRoom.Clear();
        }
        else if (roomInfo.RoomType == DungeonRoomType.Battle)
        {
            battleTryCount = 0;
            battleMaxTryCount = Random.RandomRangeInt(1, 3);
        }
        else if (roomInfo.RoomType == DungeonRoomType.Outlet)
        {
            outletTryCount = 0;
            Generator.SubmitCanRollbackRoom();
        }

        if (prevRoom != null && prevRoom.CanRollback)
        {
            roomInfo.CanRollback = true;
        }
    }

    public override void GenerateRoomFail(RoomInfo prevRoom, DungeonRoomType roomType)
    {
        if (roomType == DungeonRoomType.Boss)
        {
            //生成boss房间成功
            excludePrevRoom.Add(prevRoom);
            if (excludePrevRoom.Count >= Generator.RoomInfos.Count)
            {
                //全都没找到合适的, 那就再来一遍
                excludePrevRoom.Clear();
            }
        }
        else if (roomType == DungeonRoomType.Outlet)
        {
            outletTryCount++;
            if (outletTryCount >= 3 && prevRoom != null) //生成结束房间失败, 那么只能回滚boss房间
            {
                outletTryCount = 0;
                Generator.RollbackRoom(prevRoom);
            }
        }
        else if (roomType == DungeonRoomType.Battle)
        {
            battleTryCount++;
        }
    }

    public override RoomDirection GetNextRoomDoorDirection(RoomInfo prevRoom, DungeonRoomType roomType)
    {
        return Random.RandomChoose(_roomDirections);
    }

    public override int GetNextRoomInterval(RoomInfo prevRoom, DungeonRoomType roomType, RoomDirection direction)
    {
        return Random.RandomRangeInt(Config.RoomMinInterval, Config.RoomMaxInterval);
    }

    public override int GetNextRoomOffset(RoomInfo prevRoom, DungeonRoomType roomType, RoomDirection direction)
    {
        //为什么最后的值要减4或者5? 因为这个值是房间地板向外扩充的格子数量
        
        if (roomType == DungeonRoomType.Outlet)
        {
            if (direction == RoomDirection.Up || direction == RoomDirection.Down)
            {
                return (int)(prevRoom.Size.X * 0.5f - 4);
            }
            return (int)(prevRoom.Size.Y * 0.5f - 5);
        }
        if (direction == RoomDirection.Up || direction == RoomDirection.Down)
        {
            return Random.RandomRangeInt((int)(prevRoom.Size.X * Config.RoomVerticalMinDispersion),
                (int)(prevRoom.Size.X * Config.RoomVerticalMaxDispersion)) + (int)(prevRoom.Size.X * 0.5f - 4);
        }
        return Random.RandomRangeInt((int)(prevRoom.Size.Y * Config.RoomHorizontalMinDispersion),
            (int)(prevRoom.Size.Y * Config.RoomHorizontalMaxDispersion)) + (int)(prevRoom.Size.Y * 0.5f - 5);
    }
}