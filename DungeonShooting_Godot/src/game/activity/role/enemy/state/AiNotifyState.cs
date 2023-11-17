using System;

namespace EnemyState;

/// <summary>
/// 发现目标, 通知其它敌人
/// </summary>
public class AiNotifyState : StateBase<Enemy, AIStateEnum>
{
    private float _timer;
    
    public AiNotifyState() : base(AIStateEnum.AiNotify)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
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
            ChangeState(AIStateEnum.AiTailAfter);
        }
    }
}