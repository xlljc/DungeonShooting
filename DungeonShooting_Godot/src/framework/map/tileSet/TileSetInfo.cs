
using System.Text.Json.Serialization;

/// <summary>
/// 图块集数据
/// </summary>
public class TileSetInfo
{
    /// <summary>
    /// 图块集名称
    /// </summary>
    [JsonInclude]
    public string Name;
}