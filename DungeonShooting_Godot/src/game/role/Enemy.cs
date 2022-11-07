
public class Enemy : Role
{
    public Enemy() : base(ResourcePath.prefab_role_Enemy_tscn)
    {
        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Player;
        Camp = CampEnum.Camp2;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        
        if (Holster.ActiveWeapon != null)
        {
            Holster.ActiveWeapon.Trigger();
        }
    }
}
