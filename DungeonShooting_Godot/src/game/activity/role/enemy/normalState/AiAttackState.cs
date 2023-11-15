using Godot;

namespace NnormalState;

public class AiAttackState : StateBase<Enemy, AINormalStateEnum>
{
    //上一个状态
    public AINormalStateEnum PrevState;
    //攻击步骤, 0.锁定目标, 1.执行攻击, 2.攻击完成
    private byte _attackStep = 0;
    
    public AiAttackState() : base(AINormalStateEnum.AiAttack)
    {
    }

    public override void Enter(AINormalStateEnum prev, params object[] args)
    {
        if (Master.GetAttackTimer() > 0)
        {
            Debug.LogError("攻击冷却还未完成就进入了 AINormalStateEnum.AiAttack 状态!");
            ChangeState(prev);
        }
        _attackStep = 0;
        PrevState = prev;
        Master.AnimationPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void Exit(AINormalStateEnum next)
    {
        Master.AnimationPlayer.AnimationFinished -= OnAnimationFinished;
    }

    public override void Process(float delta)
    {
        if (_attackStep == 0) //锁定目标步骤
        {
            if (Master.GetLockRemainderTime() > 0) //锁定目标时间
            {
                if (!Master.AttackAttribute.FiringStand && !Master.NavigationAgent2D.IsNavigationFinished())
                {
                    //移动
                    Master.DoMove();
                }
                else
                {
                    //站立
                    Master.DoIdle();
                }
            }
            else
            {
                _attackStep = 1;
            }
        }
        else if (_attackStep == 1) //攻击步骤
        {
            Master.BasisVelocity = Vector2.Zero;
            Master.AnimationPlayer.Play(AnimatorNames.Attack);
            _attackStep = 2;
        }
        
    }

    public void OnAnimationFinished(StringName name)
    {
        ChangeState(PrevState);
    }
}