
using System.Collections;
using Godot;

/// <summary>
/// 翻滚状态
/// </summary>
public class PlayerRollState : StateBase<Player, PlayerStateEnum>
{
    private long _coroutineId = -1;
    private Vector2 _moveDir;
    
    public PlayerRollState() : base(PlayerStateEnum.Roll)
    {
    }

    public override void Enter(PlayerStateEnum prev, params object[] args)
    {
        if (_coroutineId >= 0)
        {
            Master.StopCoroutine(_coroutineId);
        }

        _coroutineId = Master.StartCoroutine(RunRoll());

        //隐藏武器
        Master.BackMountPoint.Visible = false;
        Master.MountPoint.Visible = false;
        //禁用伤害碰撞
        Master.HurtCollision.Disabled = true;
        
        //翻滚移动方向
        _moveDir = InputManager.MoveAxis;
        Master.BasisVelocity = _moveDir * Master.PlayerRoleState.RollSpeed;
    }

    public override void Exit(PlayerStateEnum next)
    {
        //显示武器
        Master.BackMountPoint.Visible = true;
        Master.MountPoint.Visible = true;
        //启用伤害碰撞
        Master.HurtCollision.Disabled = false;
        Master.BasisVelocity = Master.BasisVelocity.LimitLength(Master.RoleState.MoveSpeed);
    }

    public override void Process(float delta)
    {
        Master.BasisVelocity = _moveDir * Master.PlayerRoleState.RollSpeed;
    }

    //翻滚逻辑处理
    private IEnumerator RunRoll()
    {
        Master.AnimatedSprite.Play(AnimatorNames.Roll);

        Master.MountLookTarget = false;
        var face = Master.Face;
        var velocity = Master.BasisVelocity;
        if (velocity.X > 0 && face == FaceDirection.Left)
        {
            Master.Face = FaceDirection.Right;
        }
        else if (velocity.X < 0 && face == FaceDirection.Right)
        {
            Master.Face = FaceDirection.Left;
        }
        
        yield return Master.AnimatedSprite.ToSignal(Master.AnimatedSprite, AnimatedSprite2D.SignalName.AnimationFinished);
        _coroutineId = -1;
        
        Master.MountLookTarget = true;
        Master.Face = face;
        Master.OverRoll();
        if (InputManager.MoveAxis != Vector2.Zero) //切换到移动状态
        {
            ChangeState(PlayerStateEnum.Move);
        }
        else //切换空闲状态
        {
            ChangeState(PlayerStateEnum.Idle);
        }
    }
}