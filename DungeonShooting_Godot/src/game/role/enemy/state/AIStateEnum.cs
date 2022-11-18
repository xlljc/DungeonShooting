
public enum AIStateEnum
{
    /// <summary>
    /// ai状态, 正常, 未发现目标
    /// </summary>
    AINormal,
    /// <summary>
    /// 发现目标, 但不知道在哪
    /// </summary>
    AIProbe,
    /// <summary>
    /// 发现目标, 并且知道位置
    /// </summary>
    AITailAfter,
}