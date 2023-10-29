using System.Collections;
using Godot;

/// <summary>
/// 子弹类
/// </summary>
[Tool]
public partial class Bullet : ActivityObject, IAmmo
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

    /// <summary>
    /// 发射该子弹的武器
    /// </summary>
    public Weapon Weapon { get; private set; }

    /// <summary>
    /// 最小伤害
    /// </summary>
    public int MinHarm { get; set; } = 4;
    
    /// <summary>
    /// 最大伤害
    /// </summary>
    public int MaxHarm { get; set; } = 4;
    
    /// <summary>
    /// 发射该子弹的角色
    /// </summary>
    public Role TriggerRole { get; private set; }

    // 最大飞行距离
    private float MaxDistance;

    // 子弹飞行速度
    private float FlySpeed;

    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    public void Init(Weapon weapon, uint targetLayer)
    {
        TriggerRole = weapon.TriggerRole;
        Weapon = weapon;
        AttackLayer = targetLayer;
    }
    
    /// <summary>
    /// 初始化子弹属性
    /// </summary>
    /// <param name="weapon">射出该子弹的武器</param>
    /// <param name="speed">速度</param>
    /// <param name="maxDistance">最大飞行距离</param>
    /// <param name="position">位置</param>
    /// <param name="rotation">角度</param>
    /// <param name="targetLayer">攻击目标层级</param>
    public void Init(Weapon weapon, float speed, float maxDistance, Vector2 position, float rotation, uint targetLayer)
    {
        Init(weapon, targetLayer);
        CollisionArea.AreaEntered += OnArea2dEntered;
        
        if (TriggerRole == null || !TriggerRole.IsAi) //只有玩家使用该武器才能获得正常速度的子弹
        {
            FlySpeed = speed;
        }
        else
        {
            FlySpeed = speed * weapon.AiUseAttribute.AiAttackAttr.BulletSpeedScale;
        }
        MaxDistance = maxDistance;
        Position = position;
        Rotation = rotation;
        ShadowOffset = new Vector2(0, 5);
        BasisVelocity = new Vector2(FlySpeed, 0).Rotated(Rotation);
        
        //如果子弹会对玩家造成伤害, 则显示红色描边
        if (Player.Current.CollisionWithMask(targetLayer))
        {
            ShowOutline = true;
            OutlineColor = new Color(1, 0, 0);
            StartCoroutine(BorderFlashes());
        }
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
        CurrFlyDistance += FlySpeed * delta;
        if (CurrFlyDistance >= MaxDistance)
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
            var damage = Utils.Random.RandomRangeInt(MinHarm, MaxHarm);
            if (TriggerRole != null)
            {
                damage = TriggerRole.RoleState.CallCalcDamageEvent(damage);
            }

            //击退
            if (role is not Player) //目标不是玩家才会触发击退
            {
                var attr = Weapon.GetUseAttribute(TriggerRole);
                var repel = Utils.Random.RandomConfigRange(attr.RepelRnage);
                role.MoveController.AddForce(Vector2.FromAngle(BasisVelocity.Angle()) * repel, repel * 2);
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
}