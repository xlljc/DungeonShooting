
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
    /// 空房间
    /// </summary>
    Empty = 1,
    /// <summary>
    /// 地图绘制错误
    /// </summary>
    TileError = 2,
    /// <summary>
    /// 门区域绘制错误
    /// </summary>
    DoorAreaError = 3, 
    /// <summary>
    /// 没有预设
    /// </summary>
    NoPreinstallError = 4
}