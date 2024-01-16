
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
    /// 自定义底层1, 数据五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> CustomFloor1;
    
    /// <summary>
    /// 自定义底层2, 数据五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> CustomFloor2;
    
    /// <summary>
    /// 自定义底层3, 数据五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> CustomFloor3;
    
    /// <summary>
    /// 自定义中层1, 数据五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> CustomMiddle1;
    
    /// <summary>
    /// 自定义中层2, 数据五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> CustomMiddle2;
    
    /// <summary>
    /// 自定义顶层, 数据五个一组, 分别为: 地图x坐标, 地图y坐标, 资源id, 图集x坐标, 图集y坐标
    /// </summary>
    [JsonInclude]
    public List<int> CustomTop;
    
    public void InitData()
    {
        NavigationVertices = new List<SerializeVector2>();
        NavigationPolygon = new List<int[]>();
        Floor = new List<int>();
        Middle = new List<int>();
        Top = new List<int>();
        CustomFloor1 = new List<int>();
        CustomFloor2 = new List<int>();
        CustomFloor3 = new List<int>();
        CustomMiddle1 = new List<int>();
        CustomMiddle2 = new List<int>();
        CustomTop = new List<int>();
    }
}