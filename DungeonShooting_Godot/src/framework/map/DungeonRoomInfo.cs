
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 房间配置数据
/// </summary>
public class DungeonRoomInfo
{
    /// <summary>
    /// 房间位置, 在tile坐标系中的位置, 不是坐标
    /// </summary>
    [JsonInclude]
    public SerializeVector2 Position;
    
    /// <summary>
    /// 房间大小, 在tile坐标系中占用的格子, 不是像素大小
    /// </summary>
    [JsonInclude]
    public SerializeVector2 Size;
    
    /// <summary>
    /// 房间连通门
    /// </summary>
    [JsonInclude]
    public List<DoorAreaInfo> DoorAreaInfos;
}