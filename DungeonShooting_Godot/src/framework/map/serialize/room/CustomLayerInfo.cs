
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 自定义层数据
/// </summary>
public class CustomLayerInfo
{
    /// <summary>
    /// 层名称
    /// </summary>
    [JsonInclude]
    public string Name;

    /// <summary>
    /// Z 轴排序
    /// </summary>
    [JsonInclude]
    public int ZIndex;

    /// <summary>
    /// 数据五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> Data;

    public void InitData()
    {
        Data = new List<int>();
    }
}