
using System.Text.Json.Serialization;

/// <summary>
/// 标记数据
/// </summary>
public class MarkInfo
{
    [JsonInclude]
    public SerializeVector2 Position;
}