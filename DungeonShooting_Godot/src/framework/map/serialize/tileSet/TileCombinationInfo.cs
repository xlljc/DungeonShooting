
using System.Text.Json.Serialization;

/// <summary>
/// 组合图块数据
/// </summary>
public class TileCombinationInfo
{
    /// <summary>
    /// 组合唯一Id
    /// </summary>
    [JsonInclude]
    public string Id;
    /// <summary>
    /// 组合名称
    /// </summary>
    [JsonInclude]
    public string Name;
    /// <summary>
    /// 组合图块数据, 在纹理中的坐标, 单位: 像素
    /// </summary>
    [JsonInclude]
    public SerializeVector2[] Cells;
    /// <summary>
    /// 组合图块数据, 显示位置, 单位: 像素
    /// </summary>
    [JsonInclude]
    public SerializeVector2[] Positions;
}