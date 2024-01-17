
public static class MapLayer
{
    /// <summary>
    /// 自动图块地板层
    /// </summary>
    public const int AutoFloorLayer = 0;
    /// <summary>
    /// 自定义图块地板层1
    /// </summary>
    public const int CustomFloorLayer1 = AutoFloorLayer + 1;
    /// <summary>
    /// 自定义图块地板层2
    /// </summary>
    public const int CustomFloorLayer2 = CustomFloorLayer1 + 1;
    /// <summary>
    /// 自定义图块地板层3
    /// </summary>
    public const int CustomFloorLayer3 = CustomFloorLayer2 + 1;
    /// <summary>
    /// 自动图块中间层
    /// </summary>
    public const int AutoMiddleLayer = CustomFloorLayer3 + 1;
    /// <summary>
    /// 自定义图块中间层1
    /// </summary>
    public const int CustomMiddleLayer1 = AutoMiddleLayer + 1;
    /// <summary>
    /// 自定义图块中间层2
    /// </summary>
    public const int CustomMiddleLayer2 = CustomMiddleLayer1 + 1;
    /// <summary>
    /// 自动图块顶层
    /// </summary>
    public const int AutoTopLayer = CustomMiddleLayer2 + 1;
    /// <summary>
    /// 自定义图块顶层
    /// </summary>
    public const int CustomTopLayer = AutoTopLayer + 1;
    /// <summary>
    /// 自动图块过道中的地板层, 该层只会出现在 World 场景中的 TileMap
    /// </summary>
    public const int AutoAisleFloorLayer = CustomTopLayer + 1;
    
    /// <summary>
    /// 标记数据层, 特殊层, 不会出现在 TileMap 中
    /// </summary>
    public const int MarkLayer = 9999;
}