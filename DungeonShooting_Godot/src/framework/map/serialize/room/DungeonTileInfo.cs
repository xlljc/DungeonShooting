
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
    /// 底层数据, 四个一组, 分别是: 地图x坐标, 地图y坐标, Terrain 的 bit, Terrain 的 type
    /// </summary>
    [JsonInclude]
    public List<int> Floor;
    
    /// <summary>
    /// 中层数据, 四个一组, 分别是: 地图x坐标, 地图y坐标, Terrain 的 bit, Terrain 的 type
    /// </summary>
    [JsonInclude]
    public List<int> Middle;
    
    /// <summary>
    /// 顶层数据, 四个一组, 分别是: 地图x坐标, 地图y坐标, Terrain 的 bit, Terrain 的 type
    /// </summary>
    [JsonInclude]
    public List<int> Top;

    /// <summary>
    /// 自定义层
    /// </summary>
    [JsonInclude]
    public Dictionary<int, CustomLayerInfo> CustomLayers;

    public void InitData()
    {
        NavigationVertices = new List<SerializeVector2>();
        NavigationPolygon = new List<int[]>();
        Floor = new List<int>();
        Middle = new List<int>();
        Top = new List<int>();
        CustomLayers = new Dictionary<int, CustomLayerInfo>();
    }
}