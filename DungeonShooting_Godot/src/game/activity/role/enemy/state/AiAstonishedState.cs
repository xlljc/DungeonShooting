using Godot;

namespace EnemyState;

/// <summary>
/// 发现目标时的惊讶状态
/// </summary>
public class AiAstonishedState : StateBase<Enemy, AIStateEnum>
{
    /// <summary>
    /// 下一个状态
    /// </summary>
    public AIStateEnum NextState;
    
    private float _timer;
    private object[] _args;
    
    public AiAstonishedState() : base(AIStateEnum.AiAstonished)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        if (args.Length == 0)
        {
            Debug.Log("进入 AINormalStateEnum.AiAstonished 状态必传入下一个状态做完参数!");
            ChangeState(prev);
            return;
        }

        _args = args;
        
        NextState = (AIStateEnum)args[0];
        _timer = 0.6f;
        
        //播放惊讶表情
        Master.AnimationPlayer.Play(AnimatorNames.Astonished);
    }

    public override void Process(float delta)
    {
        Master.DoIdle();
        _timer -= delta;
        if (_timer <= 0)
        {
            if (_args.Length == 1)
            {
                ChangeState(NextState);
            }
            else if (_args.Length == 2)
            {
                ChangeState(NextState, _args[1]);
            }
        }
    }
}