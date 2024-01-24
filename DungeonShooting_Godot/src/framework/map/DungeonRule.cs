
/// <summary>
/// 用于自定义地牢房间生成规则
/// </summary>
public abstract class DungeonRule
{
    public DungeonGenerator Generator { get; }
    
    public DungeonConfig Config { get; }
    
    public SeedRandom Random { get; }

    public DungeonRule(DungeonGenerator generator)
    {
        Generator = generator;
        Config = generator.Config;
        Random = generator.Random;
    }
    
    /// <summary>
    /// 计算下一个房间类型
    /// </summary>
    public abstract DungeonRoomType CalcNextRoomType(RoomInfo prev);
}