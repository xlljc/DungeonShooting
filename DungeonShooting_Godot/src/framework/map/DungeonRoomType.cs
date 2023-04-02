
/// <summary>
/// 模板房间类型
/// </summary>
public enum DungeonRoomType
{
    /// <summary>
    /// 普通战斗房间, 进入该房间时会关上门, 并刷出若干波敌人, 消灭所有敌人后开门
    /// </summary>
    Battle,
    /// <summary>
    /// 起始房间, 由上一层地牢的结束房间进入该房间, 每层包含一个起始房间
    /// </summary>
    Inlet,
    /// <summary>
    /// 结束房间, 进入另一层地牢, 每层只是包含一个结束房间
    /// </summary>
    Outlet,
    /// <summary>
    /// boss战房间, 进入房间时会关上没, 刷出boss, 消灭boss后开门
    /// </summary>
    Boss,
    /// <summary>
    /// 奖励房间, 给予玩家武器或者道具奖励的房间
    /// </summary>
    Reward,
    /// <summary>
    /// 商店, 玩家买卖道具装备的房间
    /// </summary>
    Shop,
    /// <summary>
    /// 事件房间, 触发剧情或者解锁NPC的房间
    /// </summary>
    Event,
}