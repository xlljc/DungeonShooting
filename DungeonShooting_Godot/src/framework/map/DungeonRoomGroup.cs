
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 房间组数据
/// </summary>
public class DungeonRoomGroup
{
    /// <summary>
    /// 组名称
    /// </summary>
    [JsonInclude]
    public string GroupName;
    
    /// <summary>
    /// 普通战斗房间, 进入该房间时会关上门, 并刷出若干波敌人, 消灭所有敌人后开门
    /// </summary>
    [JsonInclude]
    public List<DungeonRoomSplit> BattleList = new List<DungeonRoomSplit>();

    /// <summary>
    /// 起始房间, 由上一层地牢的结束房间进入该房间, 每层包含一个起始房间
    /// </summary>
    [JsonInclude]
    public List<DungeonRoomSplit> InletList = new List<DungeonRoomSplit>();

    /// <summary>
    /// 结束房间, 进入另一层地牢, 每层只是包含一个结束房间
    /// </summary>
    [JsonInclude]
    public List<DungeonRoomSplit> OutletList = new List<DungeonRoomSplit>();

    /// <summary>
    /// boss战房间, 进入房间时会关上没, 刷出boss, 消灭boss后开门
    /// </summary>
    [JsonInclude]
    public List<DungeonRoomSplit> BossList = new List<DungeonRoomSplit>();

    /// <summary>
    /// 奖励房间, 给予玩家武器或者道具奖励的房间
    /// </summary>
    [JsonInclude]
    public List<DungeonRoomSplit> RewardList = new List<DungeonRoomSplit>();

    /// <summary>
    /// 商店, 玩家买卖道具装备的房间
    /// </summary>
    [JsonInclude]
    public List<DungeonRoomSplit> ShopList = new List<DungeonRoomSplit>();

    /// <summary>
    /// 事件房间, 触发剧情或者解锁NPC的房间
    /// </summary>
    [JsonInclude]
    public List<DungeonRoomSplit> EventList = new List<DungeonRoomSplit>();

    /// <summary>
    /// 获取指定类型房间集合
    /// </summary>
    public List<DungeonRoomSplit> GetRoomList(DungeonRoomType roomType)
    {
        switch (roomType)
        {
            case DungeonRoomType.Battle: return BattleList;
            case DungeonRoomType.Inlet: return InletList;
            case DungeonRoomType.Outlet: return OutletList;
            case DungeonRoomType.Boss: return BossList;
            case DungeonRoomType.Reward: return RewardList;
            case DungeonRoomType.Shop: return ShopList;
            case DungeonRoomType.Event: return EventList;
        }

        return null;
    }
}