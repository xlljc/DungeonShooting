
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
    /// 获取指定房间类型与之相连的上一个房间对象
    /// </summary>
    public abstract RoomInfo GetConnectPrevRoom(RoomInfo prevRoomInfo, DungeonRoomType nextRoomType);
    
    /// <summary>
    /// 计算下一个房间类型
    /// </summary>
    public abstract DungeonRoomType GetNextRoomType(RoomInfo prevRoomInfo);

    /// <summary>
    /// 执行生成指定房间成功
    /// </summary>
    public abstract void GenerateRoomSuccess(RoomInfo prevRoomInfo, RoomInfo roomInfo);
    
    /// <summary>
    /// 执行生成指定类型房间失败
    /// </summary>
    public abstract void GenerateRoomFail(RoomInfo prevRoomInfo, DungeonRoomType roomType);
}