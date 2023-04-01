
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 房间配置数据
/// </summary>
public class DungeonRoomInfo
{
    /// <summary>
    /// 房间位置, 在tile坐标系中的位置, 单位: 格
    /// </summary>
    [JsonInclude]
    public SerializeVector2 Position;
    
    /// <summary>
    /// 房间大小, 在tile坐标系中占用的格子, 单位: 格
    /// </summary>
    [JsonInclude]
    public SerializeVector2 Size;
    
    /// <summary>
    /// 房间连通门
    /// </summary>
    [JsonInclude]
    public List<DoorAreaInfo> DoorAreaInfos;

    /// <summary>
    /// 导航数据
    /// </summary>
    [JsonInclude]
    public List<NavigationPolygonData> NavigationList;

    /// <summary>
    /// 房间类型
    /// </summary>
    [JsonInclude]
    public DungeonRoomType RoomType = DungeonRoomType.Battle;
}