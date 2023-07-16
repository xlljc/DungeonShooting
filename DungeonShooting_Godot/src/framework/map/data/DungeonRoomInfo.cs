
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
    /// 当前房间所属分组的名称
    /// </summary>
    [JsonInclude]
    public string GroupName = "unclaimed";
    
    /// <summary>
    /// 房间类型
    /// </summary>
    [JsonInclude]
    public DungeonRoomType RoomType = DungeonRoomType.Battle;

    /// <summary>
    /// 房间文件名称
    /// </summary>
    [JsonInclude]
    public string FileName;
    
    /// <summary>
    /// 房间权重, 值越大, 生成地牢是越容易出现该房间
    /// </summary>
    [JsonInclude]
    public int Weight = ResourceManager.DefaultWeight;
}