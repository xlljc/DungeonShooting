
using Godot;

public static class GameConfig
{
    // --------------------- 可配置项 ---------------------
    
    /// <summary>
    /// 散射计算的默认距离
    /// </summary>
    public static float ScatteringDistance = 300;
    /// <summary>
    /// 重力加速度
    /// </summary>
    public static float G = 250f;

    
    // ----------------------- 常量 -----------------------
    
    /// <summary>
    /// 连接房间的过道宽度
    /// </summary>
    public const int CorridorWidth = 4;
    /// <summary>
    /// 游戏地图网格大小
    /// </summary>
    public const int TileCellSize = 16;
    /// <summary>
    /// 房间最小间距
    /// </summary>
    public const int RoomSpace = 4;
    
    /// <summary>
    /// 地图场景路径
    /// </summary>
    public const string RoomTileDir = "resource/map/tileMaps/";
    /// <summary>
    /// 地图描述数据路径
    /// </summary>
    public const string RoomTileDataDir = "resource/map/tileData/";
    /// <summary>
    /// 房间配置汇总数据路径
    /// </summary>
    public const string RoomTileConfigFile = "resource/map/RoomConfig.json";
    /// <summary>
    /// ui预制体路径
    /// </summary>
    public const string UiPrefabDir = "prefab/ui/";
    /// <summary>
    /// ui代码根路径
    /// </summary>
    public const string UiCodeDir = "src/game/ui/";
    
    /// <summary>
    /// TileMap 底板的层级
    /// </summary>
    public const int FloorMapLayer = 0;
    /// <summary>
    /// TileMap 中层的层级
    /// </summary>
    public const int MiddleMapLayer = 1;
    /// <summary>
    /// TileMap 上层的层级
    /// </summary>
    public const int TopMapLayer = 2;
    /// <summary>
    /// 连接房间的过道的地板层级
    /// </summary>
    public const int AisleFloorMapLayer = 3;

    /// <summary>
    /// 配置层级的自定义数据名称
    /// </summary>
    public const string CustomTileLayerName = "TileLayer";
}