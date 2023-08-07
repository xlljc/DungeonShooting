
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 房间预设数据
/// </summary>
public class RoomPreinstall
{
    /// <summary>
    /// 预设名称
    /// </summary>
    [JsonInclude]
    public string Name;

    /// <summary>
    /// 预设权重
    /// </summary>
    [JsonInclude]
    public int Weight;

    /// <summary>
    /// 预设备注
    /// </summary>
    [JsonInclude]
    public string Remark;

    /// <summary>
    /// 波数数据
    /// </summary>
    [JsonInclude]
    public List<List<MarkInfo>> WaveList;
}