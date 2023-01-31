
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间的数据描述
/// </summary>
public class RoomInfo
{
    public RoomInfo(int id)
    {
        Id = id;
    }

    public int Id;
    
    /// <summary>
    /// 房间大小
    /// </summary>
    public Vector2 Size;

    /// <summary>
    /// 房间位置
    /// </summary>
    public Vector2 Position;
    
    /// <summary>
    /// 门
    /// </summary>
    public List<RoomDoor> Doors = new List<RoomDoor>();

    public List<RoomInfo> Next = new List<RoomInfo>();
    
    public RoomInfo Prev;
}