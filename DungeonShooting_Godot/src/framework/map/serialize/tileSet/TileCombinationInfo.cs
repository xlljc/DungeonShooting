
using System.Text.Json.Serialization;

/// <summary>
/// 组合图块数据
/// </summary>
public class TileCombinationInfo : IClone<TileCombinationInfo>
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

    public TileCombinationInfo Clone()
    {
        var combinationInfo = new TileCombinationInfo();
        combinationInfo.Id = Id;
        combinationInfo.Name = Name;
        if (Cells != null)
        {
            combinationInfo.Cells = new SerializeVector2[Cells.Length];
            for (int i = 0; i < Cells.Length; i++)
            {
                combinationInfo.Cells[i] = Cells[i].Clone();
            }
        }

        if (Positions != null)
        {
            combinationInfo.Positions = new SerializeVector2[Positions.Length];
            for (int i = 0; i < Positions.Length; i++)
            {
                combinationInfo.Positions[i] = Positions[i].Clone();
            }
        }
        return combinationInfo;
    }
}