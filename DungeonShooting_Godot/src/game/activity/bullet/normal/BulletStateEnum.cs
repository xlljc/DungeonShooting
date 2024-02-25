
/// <summary>
/// 子弹逻辑完成条件
/// </summary>
public enum BulletStateEnum
{
    /// <summary>
    /// 正常飞行状态
    /// </summary>
    Normal,
    /// <summary>
    /// 移动并撞到物体
    /// </summary>
    MoveCollision,
    /// <summary>
    /// 飞行到最大距离
    /// </summary>
    MaxDistance,
    /// <summary>
    /// 子弹生命周期结束
    /// </summary>
    LimeOver,
    /// <summary>
    /// 落地
    /// </summary>
    FallToGround,
    /// <summary>
    /// 碰撞到攻击目标物体
    /// </summary>
    CollisionTarget,
}