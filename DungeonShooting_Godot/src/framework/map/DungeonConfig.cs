
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
    /// 房间数量
    /// </summary>
    public int RoomCount = 5;

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