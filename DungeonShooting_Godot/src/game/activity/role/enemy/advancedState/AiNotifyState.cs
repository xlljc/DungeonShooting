using System;

namespace AdvancedState;

/// <summary>
/// 发现目标, 通知其它敌人
/// </summary>
public class AiNotifyState : StateBase<AdvancedEnemy, AIAdvancedStateEnum>
{
    private float _timer;
    
    public AiNotifyState() : base(AIAdvancedStateEnum.AiNotify)
    {
    }

    public override void Enter(AIAdvancedStateEnum prev, params object[] args)
    {
        if (Master.LookTarget == null)
        {
            throw new Exception("进入 AIAdvancedStateEnum.AiNotify 没有攻击目标!");
        }
        _timer = 0.6f;
        //通知其它角色
        Master.World.NotifyEnemyTarget(Master, Master.LookTarget);
        Master.AnimationPlayer.Play(AnimatorNames.Notify);
    }

    public override void Process(float delta)
    {
        Master.DoIdle();
        _timer -= delta;
        if (_timer <= 0)
        {
            ChangeState(AIAdvancedStateEnum.AiTailAfter);
        }
    }
}