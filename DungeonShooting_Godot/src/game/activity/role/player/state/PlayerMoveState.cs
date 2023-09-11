
using Godot;

/// <summary>
/// 移动状态
/// </summary>
public class PlayerMoveState : StateBase<Player, PlayerStateEnum>
{
    public PlayerMoveState() : base(PlayerStateEnum.Move)
    {
    }

    public override void Enter(PlayerStateEnum prev, params object[] args)
    {
        Master.HandleMoveInput((float)Master.GetProcessDeltaTime());
        PlayAnim();
    }

    public override void Process(float delta)
    {
        if (InputManager.MoveAxis == Vector2.Zero) //停止移动
        {
            ChangeState(PlayerStateEnum.Idle);
        }
        else
        {
            if (InputManager.Roll) //翻滚
            {
                ChangeState(PlayerStateEnum.Roll);
            }
            else //执行移动
            {
                Master.HandleMoveInput(delta);
                PlayAnim();
            }
        }
    }
    
    // 播放动画
    private void PlayAnim()
    {
        if ((Master.Face == FaceDirection.Right && Master.BasisVelocity.X >= 0) || Master.Face == FaceDirection.Left && Master.BasisVelocity.X <= 0) //向前走
        {
            Master.AnimatedSprite.Play(AnimatorNames.Run);
        }
        else if ((Master.Face == FaceDirection.Right && Master.BasisVelocity.X < 0) || Master.Face == FaceDirection.Left && Master.BasisVelocity.X > 0) //向后走
        {
            Master.AnimatedSprite.Play(AnimatorNames.ReverseRun);
        }
    }
}