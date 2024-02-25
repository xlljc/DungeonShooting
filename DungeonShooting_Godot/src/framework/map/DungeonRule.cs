
/// <summary>
/// 用于自定义地牢房间生成规则
/// </summary>
public abstract class DungeonRule
{
    public DungeonGenerator Generator { get; }
    
    public DungeonConfig Config { get; }
    
    public SeedRandom Random { get; }

    public DungeonRoomGroup RoomGroup { get; }

    public DungeonRule(DungeonGenerator generator)
    {
        Generator = generator;
        Config = generator.Config;
        Random = generator.Random;
        RoomGroup = generator.RoomGroup;
    }

    /// <summary>
    /// 是否可以结束生成了
    /// </summary>
    public abstract bool CanOverGenerator();

    /// <summary>
    /// 获取指定房间类型与之相连的上一个房间对象, prevRoom 可能为 null
    /// </summary>
    public abstract RoomInfo GetConnectPrevRoom(RoomInfo prevRoom, DungeonRoomType nextRoomType);
    
    /// <summary>
    /// 计算下一个房间类型, prevRoom 可能为 null
    /// </summary>
    public abstract DungeonRoomType GetNextRoomType(RoomInfo prevRoom);

    /// <summary>
    /// 执行生成指定房间成功, prevRoom 可能为 null
    /// </summary>
    public abstract void GenerateRoomSuccess(RoomInfo prevRoom, RoomInfo roomInfo);
    
    /// <summary>
    /// 执行生成指定类型房间失败, prevRoom 可能为 null
    /// </summary>
    public abstract void GenerateRoomFail(RoomInfo prevRoom, DungeonRoomType roomType);
    
    //-------------------------- 下面的函数 prevRoom 一定不会为 null --------------------------
    
    /// <summary>
    /// 获取下一个房间的方向, prevRoom 一定不为 null
    /// </summary>
    public abstract RoomDirection GetNextRoomDoorDirection(RoomInfo prevRoom, DungeonRoomType roomType);

    /// <summary>
    /// 获取下一个房间的间隔距离, prevRoom 一定不为 null
    /// </summary>
    public abstract int GetNextRoomInterval(RoomInfo prevRoom, DungeonRoomType roomType, RoomDirection direction);
    
    /// <summary>
    /// 获取下一个房间相对于当前房间的原点偏移 (单位: 格), prevRoom 一定不为 null
    /// </summary>
    public abstract int GetNextRoomOffset(RoomInfo prevRoom, DungeonRoomType roomType, RoomDirection direction);
}