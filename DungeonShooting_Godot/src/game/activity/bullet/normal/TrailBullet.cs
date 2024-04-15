
using Godot;

[Tool]
public partial class TrailBullet : Bullet
{
    private static Color TerrainColor = new Color(0xf5 / 255f + 0.8f, 0x7e / 255f + 0.45f, 0x7a / 255f + 0.45f, 0.7f);
    private static Color EnemyTerrainColor = new Color(1.5f, 0, 0, 0.7f);
    private Trail trail;

    public override void InitData(BulletData data, CampEnum camp)
    {
        base.InitData(data, camp);
        
        trail = ObjectManager.GetPoolItem<Trail>(ResourcePath.prefab_effect_common_Trail0001_tscn);
        trail.SetTarget(AnimatedSprite);
        trail.AddPoint(trail.Target.GlobalPosition);
        trail.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        trail.ZIndex = 1;
        
        if (IsEnemyBullet)
        {
            trail.SetColor(EnemyTerrainColor);
        }
        else
        {
            trail.SetColor(TerrainColor);
        }
    }


    public override void OnReclaim()
    {
        base.OnReclaim();
        trail.SetTarget(null);
        trail = null;
    }
}