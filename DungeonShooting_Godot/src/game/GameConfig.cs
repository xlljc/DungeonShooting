
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
    /// 像素缩放
    /// </summary>
    public const int WindowScale = 4;
    /// <summary>
    /// 游戏视图大小
    /// </summary>
    public static readonly Vector2 ViewportSize = new Vector2(480, 270);
    //public static Vector2 ViewportSize => OS.WindowSize / WindowScale;
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
    /// 地图数据路径
    /// </summary>
    public static readonly string RoomTileDir = System.Environment.CurrentDirectory + "/resource/map/tileMaps/";
    /// <summary>
    /// 地图描述数据路径
    /// </summary>
    public static readonly string RoomTileDataDir = System.Environment.CurrentDirectory + "/resource/map/tiledata/";
    /// <summary>
    /// 房间配置汇总数据路径
    /// </summary>
    public static readonly string RoomTileConfigFile = System.Environment.CurrentDirectory + "/resource/map/RoomConfig.json";
}