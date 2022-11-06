
public class Enemy : Role
{
    public Enemy() : base(ResourcePath.prefab_role_Enemy_tscn)
    {
        AttackLayer = PhysicsLayer.Wall | PhysicsLayer.Props | PhysicsLayer.Player;
        Camp = CampEnum.Camp2;
    }
}
