
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
    /// 标记数据是否在玩家进入房间前加载, 如果为 true, 则 DelayTime 会失效
    /// </summary>
    [JsonInclude]
    public bool Preloading;
    
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

    /// <summary>
    /// 从指定 MarkInfo 克隆数据, 浅拷贝
    /// </summary>
    public void CloneFrom(MarkInfo mark)
    {
        Position = mark.Position;
        Size = mark.Size;
        DelayTime = mark.DelayTime;
        Preloading = mark.Preloading;
        MarkList = mark.MarkList;
    }
}