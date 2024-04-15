using System;

namespace AiState;

/// <summary>
/// 发现目标, 通知其它敌人
/// </summary>
public class AiNotifyState : StateBase<AiRole, AIStateEnum>
{
    private float _timer;
    
    public AiNotifyState() : base(AIStateEnum.AiNotify)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        if (Master.LookTarget == null)
        {
            ChangeState(AIStateEnum.AiNormal);
            return;
            //throw new Exception("进入 AIAdvancedStateEnum.AiNotify 没有攻击目标!");
        }
        _timer = 1.2f;
        //通知其它角色
        Master.World.NotifyEnemyTarget(Master, Master.LookTarget);
        if (Master.AnimationPlayer.HasAnimation(AnimatorNames.Notify))
        {
            Master.AnimationPlayer.Play(AnimatorNames.Notify);
        }
    }

    public override void Process(float delta)
    {
        Master.DoIdle();
        _timer -= delta;
        if (_timer <= 0)
        {
            ChangeState(AIStateEnum.AiTailAfter);
        }
    }
}