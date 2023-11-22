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
            ChangeState(NextState);
        }
    }
}