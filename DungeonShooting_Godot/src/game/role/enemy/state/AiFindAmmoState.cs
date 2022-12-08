
using Godot;

/// <summary>
/// Ai 寻找弹药
/// </summary>
public class AiFindAmmoState : StateBase<Enemy, AiStateEnum>
{
    public AiFindAmmoState() : base(AiStateEnum.AiFindAmmo)
    {
    }

    public override void Enter(AiStateEnum prev, params object[] args)
    {
        GD.Print("寻找武器");
    }

    public override void PhysicsProcess(float delta)
    {
        
    }

    public override void DebugDraw()
    {
        
    }
}