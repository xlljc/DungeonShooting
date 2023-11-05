
using Godot;

/// <summary>
/// 爆炸子弹
/// </summary>
[Tool]
public partial class BoomBullet : Bullet
{
    public override void OnLimeOver()
    {
        PlayBoom();
        Destroy();
    }

    public override void OnMaxDistance()
    {
        PlayBoom();
        Destroy();
    }

    public override void OnCollisionTarget(ActivityObject o)
    {
        PlayBoom();
        Destroy();
    }

    public override void OnCollisionWall(KinematicCollision2D lastSlideCollision)
    {
        if (CurrentBounce > BulletData.BounceCount) //反弹次数超过限制
        {
            PlayBoom();
        }
    }

    /// <summary>
    /// 播放爆炸
    /// </summary>
    public void PlayBoom()
    {
        var explode = ObjectManager.GetExplode(ResourcePath.prefab_bullet_explode_Explode0001_tscn);
        explode.Position = Position;
        explode.RotationDegrees = Utils.Random.RandomRangeInt(0, 360);
        explode.AddToActivityRootDeferred(RoomLayerEnum.YSortLayer);
        explode.Init(BulletData.TriggerRole?.AffiliationArea, AttackLayer, 25, BulletData.MinHarm, BulletData.MaxHarm, 50, 150);
        explode.RunPlay();
    }
}