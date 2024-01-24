
/// <summary>
/// 默认实现地牢房间规则
/// </summary>
public class DefaultDungeonRule : DungeonRule
{
    public DefaultDungeonRule(DungeonGenerator generator) : base(generator)
    {
    }

    public override DungeonRoomType CalcNextRoomType(RoomInfo prev)
    {
        return DungeonRoomType.Battle;
    }
}