
using Godot;

/// <summary>
/// 房间的门
/// </summary>
public class RoomDoorInfo
{
    /// <summary>
    /// 所在墙面方向
    /// </summary>
    public DoorDirection Direction;

    /// <summary>
    /// 所在的房间
    /// </summary>
    public RoomInfo RoomInfo;

    /// <summary>
    /// 连接的门
    /// </summary>
    public RoomDoorInfo ConnectDoor;

    /// <summary>
    /// 连接的房间
    /// </summary>
    public RoomInfo ConnectRoom;

    /// <summary>
    /// 原点坐标, 单位: 格
    /// </summary>
    public Vector2 OriginPosition;

    /// <summary>
    /// 与下一道门是否有交叉点
    /// </summary>
    public bool HasCross;

    /// <summary>
    /// 与下一道门的交叉点, 单位: 格
    /// </summary>
    public Vector2 Cross;

    /// <summary>
    /// 占位导航网格
    /// </summary>
    public DoorNavigationInfo Navigation;
}