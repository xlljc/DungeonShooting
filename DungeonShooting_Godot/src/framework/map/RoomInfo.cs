
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间的数据描述
/// </summary>
public class RoomInfo
{
    public RoomInfo(int id, DungeonRoomSplit roomSplit)
    {
        Id = id;
        RoomSplit = roomSplit;
    }

    /// <summary>
    /// 房间 id
    /// </summary>
    public int Id;

    /// <summary>
    /// 生成该房间使用的模板
    /// </summary>
    public DungeonRoomSplit RoomSplit;
    
    /// <summary>
    /// 房间大小
    /// </summary>
    public Vector2I Size;

    /// <summary>
    /// 房间位置
    /// </summary>
    public Vector2I Position;
    
    /// <summary>
    /// 门
    /// </summary>
    public List<RoomDoorInfo> Doors = new List<RoomDoorInfo>();

    /// <summary>
    /// 下一个房间
    /// </summary>
    public List<RoomInfo> Next = new List<RoomInfo>();
    
    /// <summary>
    /// 上一个房间
    /// </summary>
    public RoomInfo Prev;
}