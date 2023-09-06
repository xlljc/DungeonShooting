
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 标记项数据
/// </summary>
public class MarkInfoItem
{
    /// <summary>
    /// 物体Id
    /// </summary>
    [JsonInclude]
    public string Id;
    
    /// <summary>
    /// 权重
    /// </summary>
    [JsonInclude]
    public int Weight;
    
    /// <summary>
    /// 额外属性
    /// </summary>
    [JsonInclude]
    public Dictionary<string, string> Attr;
    
    /// <summary>
    /// 物体初始海拔高度
    /// </summary>
    [JsonInclude]
    public int Altitude;

    /// <summary>
    /// 物体初始纵轴速度
    /// </summary>
    [JsonInclude]
    public float VerticalSpeed;

    /// <summary>
    /// 所属标记类型
    /// </summary>
    [JsonIgnore]
    public SpecialMarkType SpecialMarkType = SpecialMarkType.Normal;
}