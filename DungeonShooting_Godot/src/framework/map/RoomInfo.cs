
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
    /// 生成该房间使用的配置数据
    /// </summary>
    public DungeonRoomSplit RoomSplit;
    
    /// <summary>
    /// 房间大小, 单位: 格
    /// </summary>
    public Vector2I Size;

    /// <summary>
    /// 房间位置, 单位: 格
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

    /// <summary>
    /// 物体生成标记
    /// </summary>
    public List<ActivityMark> ActivityMarks = new List<ActivityMark>();

    /// <summary>
    /// 当前房间归属区域
    /// </summary>
    public AffiliationArea Affiliation;

    /// <summary>
    /// 是否处于闭关状态, 也就是房间门没有打开
    /// </summary>
    public bool IsSeclusion { get; private set; } = false;
    
    /// <summary>
    /// 获取房间的全局坐标, 单位: 像素
    /// </summary>
    public Vector2 GetWorldPosition()
    {
        return new Vector2(
            Position.X * GameConfig.TileCellSize,
            Position.Y * GameConfig.TileCellSize
        );
    }

    /// <summary>
    /// 获取房间左上角的 Tile 距离全局坐标原点的偏移, 单位: 像素
    /// </summary>
    /// <returns></returns>
    public Vector2 GetOffsetPosition()
    {
        return RoomSplit.RoomInfo.Position.AsVector2() * GameConfig.TileCellSize;
    }
    
    /// <summary>
    /// 获取房间横轴结束位置, 单位: 格
    /// </summary>
    public int GetHorizontalEnd()
    {
        return Position.X + Size.X;
    }

    /// <summary>
    /// 获取房间纵轴结束位置, 单位: 格
    /// </summary>
    public int GetVerticalEnd()
    {
        return Position.Y + Size.Y;
    }
    
    /// <summary>
    /// 获取房间横轴开始位置, 单位: 格
    /// </summary>
    public int GetHorizontalStart()
    {
        return Position.X;
    }

    /// <summary>
    /// 获取房间纵轴开始位置, 单位: 格
    /// </summary>
    public int GetVerticalStart()
    {
        return Position.Y;
    }

    /// <summary>
    /// 房间准备好了, 准备刷敌人, 并且关闭所有门
    /// </summary>
    public void BeReady()
    {
        IsSeclusion = true;
        //关门
        foreach (var doorInfo in Doors)
        {
            doorInfo.Door.CloseDoor();
        }
        //根据标记生成对象
        foreach (var mark in ActivityMarks)
        {
            mark.BeReady(this);
        }
    }

    /// <summary>
    /// 当前房间所有敌人都被清除了
    /// </summary>
    public void OnClearRoom()
    {
        IsSeclusion = false;
        //开门
        foreach (var doorInfo in Doors)
        {
            doorInfo.Door.OpenDoor();
        }
    }
}