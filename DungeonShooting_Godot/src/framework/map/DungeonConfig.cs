
using System.Collections.Generic;

/// <summary>
/// 生成地牢的配置
/// </summary>
public class DungeonConfig
{
    /// <summary>
    /// 地牢组名称
    /// </summary>
    public string GroupName;

    /// <summary>
    /// 战斗房间数量
    /// </summary>
    public int BattleRoomCount = 15;

    /// <summary>
    /// 奖励房间数量
    /// </summary>
    public int RewardRoomCount = 2;

    /// <summary>
    /// 商店数量
    /// </summary>
    public int ShopRoomCount = 1;

    /// <summary>
    /// 出口房间数量
    /// </summary>
    public int OutRoomCount = 1;

    /// <summary>
    /// Boss房间数量
    /// </summary>
    public int BossRoomCount = 1;
    
    //----------------------- 地牢编辑使用 -------------------------
    /// <summary>
    /// 是否指定了房间
    /// </summary>
    public bool HasDesignatedRoom => DesignatedRoom != null && DesignatedRoom.Count > 0;
    /// <summary>
    /// 指定房间类型
    /// </summary>
    public DungeonRoomType DesignatedType;
    /// <summary>
    /// 指定房间列表
    /// </summary>
    public List<DungeonRoomSplit> DesignatedRoom;
}