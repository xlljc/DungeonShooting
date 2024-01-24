
/// <summary>
/// 默认实现地牢房间规则
/// </summary>
public class DefaultDungeonRule : DungeonRule
{
    public DefaultDungeonRule(DungeonGenerator generator) : base(generator)
    {
    }

    public override bool CanOverGenerator()
    {
        return Generator.BattleRoomInfos.Count >= Config.BattleRoomCount;
    }

    public override DungeonRoomType GetNextRoomType(RoomInfo prev)
    {
        if (Generator.StartRoomInfo == null) //生成第一个房间
        {
            return DungeonRoomType.Inlet;
        }
        // else if (Generator.BattleRoomInfos.Count == 0) //奖励房间
        // {
        //     return DungeonRoomType.Reward;
        // }
        else if (Generator.BattleRoomInfos.Count == Config.BattleRoomCount - 1) //最后一个房间是boss房间
        {
            return DungeonRoomType.Boss;
        }
        else if (Generator.BattleRoomInfos.Count >= Config.BattleRoomCount) //结束房间
        {
            return DungeonRoomType.Outlet;
        }
        else if (prev.RoomType == DungeonRoomType.Boss) //生成结束房间
        {
            return DungeonRoomType.Outlet;
        }
        return DungeonRoomType.Battle;
    }
}