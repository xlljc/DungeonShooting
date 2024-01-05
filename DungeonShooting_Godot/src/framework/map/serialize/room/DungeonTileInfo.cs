
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Godot;

public class DungeonTileInfo
{
    /// <summary>
    /// 导航顶点数据
    /// </summary>
    [JsonInclude]
    public List<SerializeVector2> NavigationVertices;
    
    /// <summary>
    /// 导航多边形数据
    /// </summary>
    [JsonInclude]
    public List<int[]> NavigationPolygon;

    /// <summary>
    /// 底层数据, 五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> Floor;
    
    /// <summary>
    /// 中层数据, 五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> Middle;
    
    /// <summary>
    /// 顶层数据, 五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> Top;
}