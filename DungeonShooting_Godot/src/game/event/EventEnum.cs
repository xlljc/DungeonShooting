
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
    /// <summary>
    /// 玩家可互动对象改变, 参数为 CheckInteractiveResult
    /// </summary>
    OnPlayerChangeInteractiveItem,
    /// <summary>
    /// 玩家血量发生改变, 参数为玩家血量
    /// </summary>
    OnPlayerHpChange,
    /// <summary>
    /// 玩家最大血量发生改变, 参数为玩家最大血量
    /// </summary>
    OnPlayerMaxHpChange,
    /// <summary>
    /// 玩家护盾值发生改变, 参数为玩家护盾值
    /// </summary>
    OnPlayerShieldChange,
    /// <summary>
    /// 玩家最大护盾值发生改变, 参数为玩家最大护盾值
    /// </summary>
    OnPlayerMaxShieldChange,
    /// <summary>
    /// 刷新玩家手持武器纹理, 参数为 Texture2D
    /// </summary>
    OnPlayerRefreshWeaponTexture,
}
