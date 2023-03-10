
/// <summary>
/// 事件类型枚举
/// </summary>
public enum EventEnum
{
    /// <summary>
    /// 敌人死亡, 参数为死亡的敌人的实例
    /// </summary>
    OnEnemyDie,
    /// <summary>
    /// 玩家进入一个房间，参数为房间对象
    /// </summary>
    OnPlayerEnterRoom,
    /// <summary>
    /// 玩家第一次进入某个房间，参数为房间对象
    /// </summary>
    OnPlayerFirstEnterRoom,
}
