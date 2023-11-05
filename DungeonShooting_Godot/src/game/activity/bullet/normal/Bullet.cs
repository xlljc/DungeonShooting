using System.Collections;
using Config;
using Godot;

/// <summary>
/// 子弹类
/// </summary>
[Tool]
public partial class Bullet : ActivityObject, IBullet
{
    /// <summary>
    /// 碰撞区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D CollisionArea { get; set; }

    /// <summary>
    /// 攻击的层级
    /// </summary>
    public uint AttackLayer
    {
        get => CollisionArea.CollisionMask;
        set => CollisionArea.CollisionMask = value;
    }

    public BulletData BulletData { get; private set; }


    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    public override void OnInit()
    {
        CollisionArea.AreaEntered += OnArea2dEntered;
    }
    
    public void InitData(BulletData data, uint attackLayer)
    {
        BulletData = data;
        AttackLayer = attackLayer;
        Position = data.Position;
        Rotation = data.Rotation;
        ShadowOffset = new Vector2(0, 5);
        BasisVelocity = new Vector2(data.FlySpeed, 0).Rotated(Rotation);
        
        //如果子弹会对玩家造成伤害, 则显示红色描边
        if (Player.Current.CollisionWithMask(attackLayer))
        {
            ShowBorderFlashes();
        }
        PutDown(RoomLayerEnum.YSortLayer);
        //播放子弹移动动画
        PlaySpriteAnimation(AnimatorNames.Move);
    }
    
    /// <summary>
    /// 显示红色描边
    /// </summary>
    public void ShowBorderFlashes()
    {
        ShowOutline = true;
        OutlineColor = new Color(1, 0, 0);
        StartCoroutine(BorderFlashes());
    }
    
    private IEnumerator BorderFlashes()
    {
        while (true)
        {
            ShowOutline = !ShowOutline;
            yield return new WaitForSeconds(0.12f);
        }
    }

    /// <summary>
    /// 播放子弹消失的特效
    /// </summary>
    public virtual void PlayDisappearEffect()
    {
        var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_weapon_BulletDisappear_tscn);
        var node = packedScene.Instantiate<Node2D>();
        node.GlobalPosition = GlobalPosition;
        node.AddToActivityRoot(RoomLayerEnum.YSortLayer);
    }
    
    protected override void PhysicsProcessOver(float delta)
    {
        //移动
        var lastSlideCollision = GetLastSlideCollision();
        //撞到墙
        if (lastSlideCollision != null)
        {
            //创建粒子特效
            var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_weapon_BulletSmoke_tscn);
            var smoke = packedScene.Instantiate<GpuParticles2D>();
            smoke.GlobalPosition = lastSlideCollision.GetPosition();
            smoke.GlobalRotation = lastSlideCollision.GetNormal().Angle();
            smoke.AddToActivityRoot(RoomLayerEnum.YSortLayer);
            Destroy();
            return;
        }
        //距离太大, 自动销毁
        CurrFlyDistance += BulletData.FlySpeed * delta;
        if (CurrFlyDistance >= BulletData.MaxDistance)
        {
            PlayDisappearEffect();
            Destroy();
        }
    }
    
    private void OnArea2dEntered(Area2D other)
    {
        var role = other.AsActivityObject<Role>();
        if (role != null)
        {
            var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_weapon_BulletDisappear_tscn);
            var node = packedScene.Instantiate<Node2D>();
            node.GlobalPosition = GlobalPosition;
            node.AddToActivityRoot(RoomLayerEnum.YSortLayer);

            //计算子弹造成的伤害
            var damage = Utils.Random.RandomRangeInt(BulletData.MinHarm, BulletData.MaxHarm);
            if (BulletData.TriggerRole != null)
            {
                damage = BulletData.TriggerRole.RoleState.CallCalcDamageEvent(damage);
            }

            //击退
            if (role is not Player) //目标不是玩家才会触发击退
            {
                var attr = BulletData.Weapon.GetUseAttribute(BulletData.TriggerRole);
                var repel = Utils.Random.RandomConfigRange(attr.Bullet.RepelRnage);
                if (repel != 0)
                {
                    role.MoveController.AddForce(Vector2.FromAngle(BasisVelocity.Angle()) * repel);
                }
            }
            
            //造成伤害
            role.CallDeferred(nameof(Role.Hurt), damage, Rotation);
            Destroy();
        }
    }

    protected override void OnDestroy()
    {
        StopAllCoroutine();
    }

    private void TestBoom()
    {
        //击中爆炸，测试用
        if (BulletData.TriggerRole == null || !BulletData.TriggerRole.IsAi)
        {
            var explode = ObjectManager.GetExplode(ResourcePath.prefab_bullet_explode_Explode0001_tscn);
            explode.Position = Position;
            explode.RotationDegrees = Utils.Random.RandomRangeInt(0, 360);
            explode.AddToActivityRootDeferred(RoomLayerEnum.YSortLayer);
            explode.Init(BulletData.TriggerRole.AffiliationArea, AttackLayer, 25, BulletData.MinHarm, BulletData.MaxHarm, 50, 150);
            explode.RunPlay();
        }
    }
}