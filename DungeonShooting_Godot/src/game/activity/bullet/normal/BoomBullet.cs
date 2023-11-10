
using Godot;

/// <summary>
/// 爆炸子弹
/// </summary>
[Tool]
public partial class BoomBullet : Bullet
{
    /// <summary>
    /// 轨迹粒子
    /// </summary>
    [Export]
    public GpuParticles2D Particles;

    public override void InitData(BulletData data, uint attackLayer)
    {
        base.InitData(data, attackLayer);
        if (Particles != null)
        {
            Particles.Restart();
        }
    }

    public override void OnLimeOver()
    {
        PlayBoom();
        DoReclaim();
    }

    public override void OnMaxDistance()
    {
        PlayBoom();
        DoReclaim();
    }

    public override void OnCollisionTarget(ActivityObject o)
    {
        PlayBoom();
        DoReclaim();
    }

    public override void OnMoveCollision(KinematicCollision2D lastSlideCollision)
    {
        CurrentBounce++;
        if (CurrentBounce > BulletData.BounceCount) //反弹次数超过限制
        {
            PlayBoom();
        }
        else
        {
            //播放撞击音效
            SoundManager.PlaySoundByConfig("collision0001", Position, BulletData.TriggerRole);
        }
    }

    protected override void OnFallToGround()
    {
        //播放撞击音效
        SoundManager.PlaySoundByConfig("collision0001", Position, BulletData.TriggerRole);
    }

    /// <summary>
    /// 播放爆炸
    /// </summary>
    public void PlayBoom()
    {
        var explode = ObjectManager.GetPoolItem<Explode>(ResourcePath.prefab_bullet_explode_Explode0001_tscn);
        var pos = Position;
        explode.Position = pos;
        explode.RotationDegrees = Utils.Random.RandomRangeInt(0, 360);
        explode.AddToActivityRootDeferred(RoomLayerEnum.YSortLayer);
        explode.Init(BulletData.TriggerRole?.AffiliationArea, AttackLayer, 25, BulletData.Harm, 50, BulletData.Repel);
        explode.RunPlay(BulletData.TriggerRole);
        if (AffiliationArea != null)
        {
            var texture = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_effects_explode_Explode_pit0001_png);
            var tempPos = AffiliationArea.RoomInfo.ToImageCanvasPosition(pos);
            AffiliationArea.RoomInfo.StaticImageCanvas.DrawImageInCanvas(
                texture, null, tempPos.X, tempPos.Y, Utils.Random.RandomRangeInt(0, 360),
                texture.GetWidth() / 2, texture.GetHeight() / 2, false
            );
        }
    }
}