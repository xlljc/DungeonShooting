
public class Enemy : Role
{
    public Enemy() : base(ResourcePath.prefab_role_Enemy_tscn)
    {
        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        MoveSpeed = 20;
        LookTarget = GameApplication.Instance.Room.Player;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        Attack();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (LookTarget != null)
        {
            AnimatedSprite.Animation = AnimatorNames.ReverseRun;
            Velocity = (LookTarget.GlobalPosition - GlobalPosition).Normalized() * MoveSpeed;
            CalcMove(delta);
        }
    }
}
