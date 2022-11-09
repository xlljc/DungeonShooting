
public class Enemy : Role
{
    public Enemy() : base(ResourcePath.prefab_role_Enemy_tscn)
    {
        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Player;
        Camp = CampEnum.Camp2;

        MoveSpeed = 20;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        Attack();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        Move(delta);

    }

    public void Move(float delta)
    {
        var player = GameApplication.Instance.Room.Player;
        var dir = (player.GlobalPosition - GlobalPosition).Normalized() * MoveSpeed;

        MoveAndSlide(dir);

    }
}
