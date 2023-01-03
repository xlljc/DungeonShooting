
using Godot;

/// <summary>
/// 房间的门
/// </summary>
public class RoomDoor
{
    /// <summary>
    /// 所在墙面方向
    /// </summary>
    public DoorDirection Direction;
    
    /// <summary>
    /// 连接的房间
    /// </summary>
    public RoomInfo ConnectRoom;

    /// <summary>
    /// 原点坐标
    /// </summary>
    public Vector2 OriginPosition;
}