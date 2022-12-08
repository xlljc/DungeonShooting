
public enum AiStateEnum
{
    /// <summary>
    /// Ai 状态, 正常, 未发现目标
    /// </summary>
    AiNormal,
    /// <summary>
    /// 发现目标, 但不知道在哪
    /// </summary>
    AiProbe,
    /// <summary>
    /// 收到其他敌人通知, 前往发现目标的位置
    /// </summary>
    AiLeaveFor,
    /// <summary>
    /// 发现目标, 并且知道位置
    /// </summary>
    AiTailAfter,
    /// <summary>
    /// 目标在视野内, 跟进目标
    /// </summary>
    AiFollowUp,
    /// <summary>
    /// 距离足够进, 在目标附近随机移动
    /// </summary>
    AiSurround,
    /// <summary>
    /// Ai 寻找弹药
    /// </summary>
    AiFindAmmo,
}