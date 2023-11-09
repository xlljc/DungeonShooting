namespace NnormalState;

/// <summary>
/// AI 正常状态
/// </summary>
public class AiNormalState : StateBase<Enemy, AiStateEnum>
{
    public AiNormalState() : base(AiStateEnum.AiNormal)
    {
        
    }

    public override void Process(float delta)
    {
        //Master.BasisVelocity = (Player.Current.Position - Master.Position).LimitLength(10);
    }
}