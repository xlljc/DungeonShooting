
using Godot;

/// <summary>
/// 房间的数据描述
/// </summary>
public class RoomInfo
{
    /// <summary>
    /// 房间大小
    /// </summary>
    public Vector2 Size;

    /// <summary>
    /// 房间位置
    /// </summary>
    public Vector2 Position;
    
    public object Doors;

    /// <summary>
    /// 房间生成时所处方向: 0上, 1右, 2下, 3左
    /// </summary>
    public int Direction;

    //public RoomInfo Next;
    //public RoomInfo Prev;
}