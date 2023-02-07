
/// <summary>
/// Ai 不确定玩家位置
/// </summary>
public class AiProbeState : StateBase<Enemy, AiStateEnum>
{
    public AiProbeState() : base(AiStateEnum.AiProbe)
    {
    }

    public override void PhysicsProcess(float delta)
    {
        //其他敌人发现玩家
        if (Enemy.IsFindTarget)
        {
            ChangeStateLate(AiStateEnum.AiLeaveFor);
            return;
        }
    }
}