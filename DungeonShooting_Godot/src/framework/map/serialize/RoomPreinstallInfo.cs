
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 房间预设数据
/// </summary>
public class RoomPreinstallInfo
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

    /// <summary>
    /// 从指定对象浅拷贝数据
    /// </summary>
    public void CloneFrom(RoomPreinstallInfo preinstallInfo)
    {
        Name = preinstallInfo.Name;
        Weight = preinstallInfo.Weight;
        Remark = preinstallInfo.Remark;
        WaveList = preinstallInfo.WaveList;
    }

    /// <summary>
    /// 初始化波数据
    /// </summary>
    public void InitWaveList()
    {
        WaveList = new List<List<MarkInfo>>
        {
            new List<MarkInfo>()
        };
    }
}