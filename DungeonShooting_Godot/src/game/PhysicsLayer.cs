/// <summary>
/// 物理碰撞层
/// </summary>
public class PhysicsLayer
{
    /// <summary>
    /// 墙壁
    /// </summary>
    public const uint Wall = 1;
    /// <summary>
    /// 子弹
    /// </summary>
    public const uint Bullet = 2;
    /// <summary>
    /// 道具
    /// </summary>
    public const uint Props = 4;
    /// <summary>
    /// 玩家
    /// </summary>
    public const uint Player = 8;
    /// <summary>
    /// 敌人
    /// </summary>
    public const uint Enemy = 16;
    /// <summary>
    /// 视野遮挡
    /// </summary>
    public const uint View = 32;
}