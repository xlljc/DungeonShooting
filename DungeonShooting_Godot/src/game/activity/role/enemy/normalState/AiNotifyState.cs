namespace NnormalState;

/// <summary>
/// 发现目标, 通知其它敌人
/// </summary>
public class AiNotifyState : StateBase<Enemy, AINormalStateEnum>
{
    
    private float _timer;
    
    public AiNotifyState() : base(AINormalStateEnum.AiNotify)
    {
    }

    public override void Enter(AINormalStateEnum prev, params object[] args)
    {
        _timer = 1.5f;
        //通知其它角色
        Master.World.NotifyEnemyTarget(Master, Player.Current);
    }

    public override void Process(float delta)
    {
        _timer -= delta;
        if (_timer <= 0)
        {
            ChangeState(AINormalStateEnum.AiTailAfter);
        }
    }
}