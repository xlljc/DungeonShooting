
/// <summary>
/// Ai 不确定玩家位置
/// </summary>
public class AiProbeState : StateBase<Enemy, AiStateEnum>
{
    public AiProbeState() : base(AiStateEnum.AiProbe)
    {
    }
}