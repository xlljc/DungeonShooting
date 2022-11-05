
public class Enemy : Role
{
    public Enemy() : base(ResourcePath.prefab_role_Enemy_tscn)
    {
        Camp = CampEnum.Camp2;
    }
}
