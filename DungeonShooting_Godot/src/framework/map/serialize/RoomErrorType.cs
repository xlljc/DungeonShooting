
/// <summary>
/// 房间错误类型
/// </summary>
public enum RoomErrorType
{
    /// <summary>
    /// 没有错误
    /// </summary>
    None = 0,
    /// <summary>
    /// 地图绘制错误
    /// </summary>
    TileError = 1,
    /// <summary>
    /// 门区域绘制错误
    /// </summary>
    DoorAreaError = 2, 
    /// <summary>
    /// 没有预设
    /// </summary>
    NoPreinstallError = 3
}