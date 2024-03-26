
using System.Collections.Generic;

/// <summary>
/// 生成地牢的配置
/// </summary>
public class DungeonConfig
{
    /// <summary>
    /// 地牢使用的随机种子
    /// </summary>
    public int? RandomSeed = null;
    
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
    
    /// <summary>
    /// 房间数量
    /// </summary>
    public int RoomCount => BattleRoomCount + RewardRoomCount + ShopRoomCount + OutRoomCount + BossRoomCount;
    
    /// <summary>
    /// 房间最大层级
    /// </summary>
    public int MaxLayer = 5;
    
    /// <summary>
    /// 房间最小间隔
    /// </summary>
    public int RoomMinInterval = 2;
    
    /// <summary>
    /// 房间最大间隔
    /// </summary>
    public int RoomMaxInterval = 2;
    
    /// <summary>
    /// 房间横轴最小分散程度
    /// </summary>
    public float RoomHorizontalMinDispersion = -0.6f;
    
    /// <summary>
    /// 房间横轴最大分散程度
    /// </summary>
    public float RoomHorizontalMaxDispersion = 0.6f;

    /// <summary>
    /// 房间纵轴最小分散程度
    /// </summary>
    public float RoomVerticalMinDispersion = -0.6f;
    
    /// <summary>
    /// 房间纵轴最大分散程度
    /// </summary>
    public float RoomVerticalMaxDispersion = 0.6f;
    
    /// <summary>
    /// 是否启用区域限制
    /// </summary>
    public bool EnableLimitRange = true;
    
    /// <summary>
    /// 横轴范围
    /// </summary>
    public int RangeX = 120;
    
    /// <summary>
    /// 纵轴范围
    /// </summary>
    public int RangeY = 120;
    
    //----------------------- 地牢编辑使用 -------------------------
    /// <summary>
    /// 是否指定了房间
    /// </summary>
    public bool HasDesignatedRoom => DesignatedRoom != null && DesignatedRoom.Count > 0;
    /// <summary>
    /// 指定预设的房间类型
    /// </summary>
    public DungeonRoomType DesignatedType;
    /// <summary>
    /// 指定房间列表
    /// </summary>
    public List<DungeonRoomSplit> DesignatedRoom;
}