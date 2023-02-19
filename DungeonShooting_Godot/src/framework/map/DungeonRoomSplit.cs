
using System;
using System.Text.Json.Serialization;

/// <summary>
/// 房间配置文件相关信息, 将会在 RoomConfig.json 中汇总
/// </summary>
[Serializable]
public class DungeonRoomSplit
{
    /// <summary>
    /// 房间场景路径
    /// </summary>
    [JsonInclude]
    public string ScenePath;
    
    /// <summary>
    /// 房间配置路径
    /// </summary>
    [JsonInclude]
    public string ConfigPath;

    /// <summary>
    /// 房间配置数据
    /// </summary>
    [JsonInclude]
    public DungeonRoomInfo RoomInfo;
}