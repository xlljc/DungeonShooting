
/// <summary>
/// Ai 不确定玩家位置
/// </summary>
public class AiProbeState : StateBase<Enemy, AiStateEnum>
{
    public AiProbeState() : base(AiStateEnum.AiProbe)
    {
    }

    public override void Process(float delta)
    {
        //其他敌人发现玩家
        if (Master.CanChangeLeaveFor())
        {
            ChangeStateLate(AiStateEnum.AiLeaveFor);
            return;
        }
    }
}