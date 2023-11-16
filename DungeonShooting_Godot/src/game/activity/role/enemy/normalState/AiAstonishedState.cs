namespace NnormalState;

/// <summary>
/// 发现目标时的惊讶状态
/// </summary>
public class AiAstonishedState : StateBase<Enemy, AINormalStateEnum>
{
    /// <summary>
    /// 下一个状态
    /// </summary>
    public AINormalStateEnum NextState;

    private float _timer;
    
    public AiAstonishedState() : base(AINormalStateEnum.AiAstonished)
    {
    }

    public override void Enter(AINormalStateEnum prev, params object[] args)
    {
        if (args.Length == 0)
        {
            Debug.Log("进入 AINormalStateEnum.AiAstonished 状态必传入下一个状态做完参数!");
            ChangeState(prev);
            return;
        }

        NextState = (AINormalStateEnum)args[0];
        _timer = 1.5f;
    }

    public override void Process(float delta)
    {
        Master.DoIdle();
        _timer -= delta;
        if (_timer <= 0)
        {
            ChangeState(NextState);
        }
    }
}