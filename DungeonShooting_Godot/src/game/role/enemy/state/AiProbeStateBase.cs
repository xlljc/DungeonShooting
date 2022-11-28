
/// <summary>
/// Ai 不确定玩家位置
/// </summary>
public class AiProbeStateBase : StateBase<Enemy, AIStateEnum>
{
    public AiProbeStateBase() : base(AIStateEnum.AIProbe)
    {
    }
}