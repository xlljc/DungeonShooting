using Godot;

namespace AdvancedState;

/// <summary>
/// 发现目标时的惊讶状态
/// </summary>
public class AiAstonishedState : StateBase<AdvancedEnemy, AIAdvancedStateEnum>
{
    /// <summary>
    /// 下一个状态
    /// </summary>
    public AIAdvancedStateEnum NextState;

    private object[] _args;
    private float _timer;
    
    public AiAstonishedState() : base(AIAdvancedStateEnum.AiAstonished)
    {
    }

    public override void Enter(AIAdvancedStateEnum prev, params object[] args)
    {
        if (args.Length == 0)
        {
            Debug.Log("进入 AINormalStateEnum.AiAstonished 状态必传入下一个状态做完参数!");
            ChangeState(prev);
            return;
        }

        NextState = (AIAdvancedStateEnum)args[0];
        _args = args;
        _timer = 0.6f;

        if (NextState == AIAdvancedStateEnum.AiLeaveFor)
        {
            var target = (ActivityObject)args[1];
            Master.LookTargetPosition(target.GetCenterPosition());
        }
        
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
            else if (_args.Length == 3)
            {
                ChangeState(NextState, _args[1], _args[2]);
            }
        }
    }
}