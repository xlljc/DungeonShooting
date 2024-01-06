
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
    /// 游戏地图网格大小, 值为 16
    /// </summary>
    public const int TileCellSize = 16;
    /// <summary>
    /// 游戏地图网格大小, 向量表示
    /// </summary>
    public static readonly Vector2I TileCellSizeVector2I = new Vector2I(TileCellSize, TileCellSize);
    /// <summary>
    /// 房间最小间距
    /// </summary>
    public const int RoomSpace = 4;

    /// <summary>
    /// 地图配置路径
    /// </summary>
    public const string RoomTileDir = "resource/map/tileMaps/";
    /// <summary>
    /// 房间TileSet路径
    /// </summary>
    public const string RoomTileSetDir = "resource/map/tileSet/";
    /// <summary>
    /// 房间组配置文件名称
    /// </summary>
    public const string RoomGroupConfigFile = "GroupConfig.json";
    /// <summary>
    /// ui预制体路径
    /// </summary>
    public const string UiPrefabDir = "prefab/ui/";
    /// <summary>
    /// ui代码根路径
    /// </summary>
    public const string UiCodeDir = "src/game/ui/";
    
    /// <summary>
    /// TileMap 地板的层级
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

    /// <summary>
    /// 预览图大小
    /// </summary>
    public const int PreviewImageSize = 196;

    /// <summary>
    /// 变暗迷雾的透明度值
    /// </summary>
    public const float DarkFogAlpha = 0.2f;

    /// <summary>
    /// 迷雾过渡时间
    /// </summary>
    public const float FogTransitionTime = 0.3f;

    /// <summary>
    /// 导航网格代理收缩半径
    /// </summary>
    public const float NavigationAgentRadius = 6f;

    /// <summary>
    /// 寻路导航单格步长
    /// </summary>
    public const float NavigationCellSize = 4;
    
    /// <summary>
    /// 地形掩码纹理大小, 顶部墙壁
    /// </summary>
    public static readonly Vector2I TerrainBitSize1 = new Vector2I(12, 4);
    /// <summary>
    /// 地形掩码纹理大小, 侧方墙壁
    /// </summary>
    public static readonly Vector2I TerrainBitSize2 = new Vector2I(4, 1);
    /// <summary>
    /// 地形掩码纹理大小, 地板
    /// </summary>
    public static readonly Vector2I TerrainBitSize3 = new Vector2I(1, 1);
}