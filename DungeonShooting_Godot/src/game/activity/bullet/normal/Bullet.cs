using System.Collections;
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
    
    /// <summary>
    /// 当前反弹次数
    /// </summary>
    public int CurrentBounce { get; protected set; } = 0;
    
    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;
    
    public override void OnInit()
    {
        BounceLockRotation = false;
        CollisionArea.AreaEntered += OnArea2dEntered;
    }
    
    public virtual void InitData(BulletData data, uint attackLayer)
    {
        BulletData = data;
        AttackLayer = attackLayer;
        Rotation = data.Rotation;

        float altitude;
        var triggerRole = data.TriggerRole;
        if (triggerRole != null)
        {
            altitude = -triggerRole.MountPoint.Position.Y;
            if (triggerRole.AffiliationArea != null) //设置所属区域
            {
                triggerRole.AffiliationArea.InsertItem(this);
            }
        }
        else
        {
            altitude = 8;
        }
        
        Position = data.Position + new Vector2(0, altitude);
        Altitude = altitude;
        if (data.VerticalSpeed != 0)
        {
            VerticalSpeed = data.VerticalSpeed;
        }
        EnableVerticalMotion = data.BulletBase.UseGravity;

        //BasisVelocity = new Vector2(data.FlySpeed, 0).Rotated(Rotation);
        MoveController.AddForce(new Vector2(data.FlySpeed, 0).Rotated(Rotation));
        
        //如果子弹会对玩家造成伤害, 则显示红色描边
        if (Player.Current.CollisionWithMask(attackLayer))
        {
            ShowBorderFlashes();
        }
        PutDown(RoomLayerEnum.YSortLayer);
        //播放子弹移动动画
        PlaySpriteAnimation(AnimatorNames.Move);
        //强制更新下坠逻辑处理
        UpdateFall((float)GetProcessDeltaTime());
        
        //过期销毁
        if (data.LifeTime > 0)
        {
            this.CallDelay(data.LifeTime, OnLimeOver);
        }
    }
    

    public override void OnMoveCollision(KinematicCollision2D collision)
    {
        CurrentBounce++;
        if (CurrentBounce > BulletData.BounceCount) //反弹次数超过限制
        {
            //创建粒子特效
            var packedScene = ResourceManager.Load<PackedScene>(ResourcePath.prefab_effect_weapon_BulletSmoke_tscn);
            var smoke = packedScene.Instantiate<GpuParticles2D>();
            var rotated = AnimatedSprite.Position.Rotated(Rotation);
            smoke.GlobalPosition = collision.GetPosition() + new Vector2(0, rotated.Y);
            smoke.GlobalRotation = collision.GetNormal().Angle();
            smoke.AddToActivityRoot(RoomLayerEnum.YSortLayer);
            Destroy();
        }
    }

    /// <summary>
    /// 碰到目标
    /// </summary>
    public virtual void OnCollisionTarget(ActivityObject o)
    {
        if (o is Role role)
        {
            PlayDisappearEffect();

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
                    //role.MoveController.AddForce(Vector2.FromAngle(BasisVelocity.Angle()) * repel);
                    role.MoveController.AddForce(Vector2.FromAngle(Velocity.Angle()) * repel);
                }
            }
            
            //造成伤害
            role.CallDeferred(nameof(Role.Hurt), damage, Rotation);
            Destroy();
        }
    }

    /// <summary>
    /// 到达最大运行距离
    /// </summary>
    public virtual void OnMaxDistance()
    {
        PlayDisappearEffect();
        Destroy();
    }
    
    /// <summary>
    /// 子弹生命周期结束
    /// </summary>
    public virtual void OnLimeOver()
    {
        PlayDisappearEffect();
        Destroy();
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
        node.GlobalPosition = AnimatedSprite.GlobalPosition;
        node.AddToActivityRoot(RoomLayerEnum.YSortLayer);
    }
    
    protected override void Process(float delta)
    {
        //距离太大, 自动销毁
        CurrFlyDistance += BulletData.FlySpeed * delta;
        if (CurrFlyDistance >= BulletData.MaxDistance)
        {
            OnMaxDistance();
        }
    }
    
    private void OnArea2dEntered(Area2D other)
    {
        if (IsDestroyed)
        {
            return;
        }
        var activityObject = other.AsActivityObject();
        OnCollisionTarget(activityObject);
    }
}