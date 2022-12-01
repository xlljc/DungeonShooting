
using Godot;

/// <summary>
/// 目标在视野范围内, 发起攻击
/// </summary>
public class AIAttackState : StateBase<Enemy, AIStateEnum>
{
    public AIAttackState() : base(AIStateEnum.AIAttack)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        
    }

    public override void PhysicsProcess(float delta)
    {
        
    }

    public override void DebugDraw()
    {
        var playerPos = GameApplication.Instance.Room.Player.GlobalPosition;
        Master.DrawLine(Vector2.Zero, Master.ToLocal(playerPos), Colors.Red);
    }
}