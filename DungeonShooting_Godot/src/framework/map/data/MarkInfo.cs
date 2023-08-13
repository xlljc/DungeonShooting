
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 标记数据
/// </summary>
public class MarkInfo
{
    /// <summary>
    /// 所在坐标
    /// </summary>
    [JsonInclude]
    public SerializeVector2 Position;

    /// <summary>
    /// 区域大小
    /// </summary>
    [JsonInclude]
    public SerializeVector2 Size;
    
    /// <summary>
    /// 延时时间
    /// </summary>
    [JsonInclude]
    public float DelayTime;
    
    /// <summary>
    /// 标记列表数据
    /// </summary>
    [JsonInclude]
    public List<MarkInfoItem> MarkList;
}