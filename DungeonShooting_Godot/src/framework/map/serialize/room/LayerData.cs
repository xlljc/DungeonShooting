
using System.Text.Json.Serialization;

/// <summary>
/// 层级数据
/// </summary>
public class LayerData
{
    /// <summary>
    /// 层级名称
    /// </summary>
    [JsonInclude] public string Name;

    /// <summary>
    /// 层级
    /// </summary>
    [JsonInclude] public uint Layer;

    /// <summary>
    /// 是否可见(仅在编辑器中)
    /// </summary>
    [JsonInclude] public bool Visible;
}