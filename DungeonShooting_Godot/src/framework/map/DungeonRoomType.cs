
using System;

/// <summary>
/// 模板房间类型
/// </summary>
[Flags]
public enum DungeonRoomType
{
    /// <summary>
    /// 无
    /// </summary>
    None = 0,
    /// <summary>
    /// 普通战斗房间, 进入该房间时会关上门, 并刷出若干波敌人, 消灭所有敌人后开门
    /// </summary>
    Battle = 0b1,
    /// <summary>
    /// 起始房间, 由上一层地牢的结束房间进入该房间, 每层包含一个起始房间
    /// </summary>
    Inlet = 0b10,
    /// <summary>
    /// 结束房间, 进入另一层地牢, 每层只是包含一个结束房间
    /// </summary>
    Outlet = 0b100,
    /// <summary>
    /// boss战房间, 进入房间时会关上没, 刷出boss, 消灭boss后开门
    /// </summary>
    Boss = 0b1000,
    /// <summary>
    /// 奖励房间, 给予玩家武器或者道具奖励的房间
    /// </summary>
    Reward = 0b10000,
    /// <summary>
    /// 商店, 玩家买卖道具装备的房间
    /// </summary>
    Shop = 0b100000,
    /// <summary>
    /// 事件房间, 触发剧情或者解锁NPC的房间
    /// </summary>
    Event = 0b1000000,
}