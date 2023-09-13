
using Godot;

/// <summary>
/// 空闲状态
/// </summary>
public class PlayerIdleState : StateBase<Player, PlayerStateEnum>
{
    public PlayerIdleState() : base(PlayerStateEnum.Idle)
    {
    }

    public override void Enter(PlayerStateEnum prev, params object[] args)
    {
        Master.HandleMoveInput((float)Master.GetProcessDeltaTime());
        Master.AnimatedSprite.Play(AnimatorNames.Idle);
    }

    public override void Process(float delta)
    {
        var dir = InputManager.MoveAxis;
        if (dir != Vector2.Zero)
        {
            if (InputManager.Roll && Master.CanRoll) //按下翻滚
            {
                ChangeState(PlayerStateEnum.Roll);
            }
            else //移动
            {
                ChangeState(PlayerStateEnum.Move);
            }
        }
        else
        {
            Master.HandleMoveInput(delta);
        }
    }
}