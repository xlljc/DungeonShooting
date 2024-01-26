
using Godot;

[Tool]
public partial class TrailBullet : Bullet
{
    private Trail trail;

    public override void InitData(BulletData data, uint attackLayer)
    {
        base.InitData(data, attackLayer);
        
        trail = ObjectManager.GetPoolItem<Trail>(ResourcePath.prefab_effect_common_Trail0001_tscn);
        trail.SetTarget(AnimatedSprite);
        trail.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        trail.AddPoint(trail.Target.GlobalPosition, 0);
        trail.ZIndex = 1;
    }
    

    public override void OnReclaim()
    {
        base.OnReclaim();
        trail.SetTarget(null);
        trail = null;
    }
}