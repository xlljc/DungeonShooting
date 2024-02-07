
using System;
using System.Collections;
using Godot;
using Godot.Collections;

/// <summary>
/// 子弹类
/// </summary>
[Tool]
public partial class Bullet : ActivityObject, IBullet
{
    public event Action OnReclaimEvent;
    public event Action OnLeavePoolEvent;
    public bool IsRecycled { get; set; }
    public string Logotype { get; set; }
    
    /// <summary>
    /// 子弹伤害碰撞区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D CollisionArea { get; set; }
    
    /// <summary>
    /// 子弹伤害碰撞检测形状
    /// </summary>
    [Export, ExportFillNode]
    public CollisionShape2D CollisionShape2D { get; set; }
    
    /// <summary>
    /// 子节点包含的例子特效, 在创建完成后自动播放
    /// </summary>
    [Export]
    public Array<GpuParticles2D> Particles2D { get; set; }

    /// <summary>
    /// 攻击的层级
    /// </summary>
    public uint AttackLayer
    {
        get => CollisionArea.CollisionMask;
        set => CollisionArea.CollisionMask = value;
    }

    /// <summary>
    /// 子弹使用的数据
    /// </summary>
    public BulletData BulletData { get; private set; }
    
    /// <summary>
    /// 当前反弹次数
    /// </summary>
    public int CurrentBounce { get; protected set; } = 0;

    /// <summary>
    /// 当前穿透次数
    /// </summary>
    public int CurrentPenetration { get; protected set; } = 0;
    
    /// <summary>
    /// 是否是敌人使用的子弹
    /// </summary>
    public bool IsEnemyBullet { get; private set; } = false;

    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    private bool _init = false;

    public override void OnInit()
    {
        base.OnInit();
        OutlineColor = new Color(2.5f, 0, 0);
        SetBlendColor(new Color(2.0f, 2.0f, 2.0f));
    }

    public virtual void InitData(BulletData data, uint attackLayer)
    {
        if (!_init)
        {
            CollisionArea.AreaEntered += OnArea2dEntered;
            CollisionArea.BodyEntered += OnBodyEntered;
            _init = true;
        }
        
        CurrentBounce = 0;
        CurrentPenetration = 0;
        CurrFlyDistance = 0;
        
        BulletData = data;
        AttackLayer = attackLayer;
        Rotation = data.Rotation;
        
        var triggerRole = data.TriggerRole;
        if (data.TriggerRole != null && data.TriggerRole.AffiliationArea != null) //设置所属区域
        {
            if (triggerRole.AffiliationArea != null) 
            {
                triggerRole.AffiliationArea.InsertItem(this);
            }
        }
        
        Position = data.Position + new Vector2(0, data.Altitude);
        Altitude = data.Altitude;
        if (data.VerticalSpeed != 0)
        {
            VerticalSpeed = data.VerticalSpeed;
        }
        else
        {
            VerticalSpeed = 0;
        }

        //BasisVelocity = new Vector2(data.FlySpeed, 0).Rotated(Rotation);
        MoveController.AddForce(new Vector2(data.FlySpeed, 0).Rotated(Rotation));
        
        //如果子弹会对玩家造成伤害, 则显示红色描边
        if (Player.Current != null && Player.Current.CollisionWithMask(attackLayer))
        {
            if (!IsEnemyBullet)
            {
                RefreshBulletColor(true);
            }
        }
        else if (IsEnemyBullet)
        {
            RefreshBulletColor(false);
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
        
        if (Particles2D != null)
        {
            foreach (var particles2D in Particles2D)
            {
                particles2D.Restart();
            }
        }
    }

    /// <summary>
    /// 刷新子弹的颜色
    /// </summary>
    /// <param name="isEnemyBullet">是否是敌人使用的子弹</param>
    public virtual void RefreshBulletColor(bool isEnemyBullet)
    {
        IsEnemyBullet = isEnemyBullet;
        if (isEnemyBullet)
        {
            ShowOutline = true;
            SetBlendSchedule(1);
        }
        else
        {
            ShowOutline = false;
            SetBlendSchedule(0);
        }
    }
    
    public override void OnMoveCollision(KinematicCollision2D collision)
    {
        CurrentBounce++;
        if (CurrentBounce > BulletData.BounceCount) //反弹次数超过限制
        {
            //创建粒子特效
            OnPlayCollisionEffect(collision);
            LogicalFinish();
        }
    }

    /// <summary>
    /// 碰到目标
    /// </summary>
    public virtual void OnCollisionTarget(IHurt hurt)
    {
        OnPlayDisappearEffect();
        if (BulletData.Repel != 0)
        {
            var o = hurt.GetActivityObject();
            if (o != null && o is not Player) //目标不是玩家才会触发击退
            {
                o.AddRepelForce(Velocity.Normalized() * BulletData.Repel);
            }
        }

        //造成伤害
        hurt.Hurt(BulletData.TriggerRole.IsDestroyed ? null : BulletData.TriggerRole, BulletData.Harm, Rotation);

        //穿透次数
        CurrentPenetration++;
        if (CurrentPenetration > BulletData.Penetration)
        {
            CallDeferred(nameof(LogicalFinish));
        }
    }

    /// <summary>
    /// 到达最大运行距离
    /// </summary>
    public virtual void OnMaxDistance()
    {
        OnPlayDisappearEffect();
        LogicalFinish();
    }
    
    /// <summary>
    /// 子弹生命周期结束
    /// </summary>
    public virtual void OnLimeOver()
    {
        OnPlayDisappearEffect();
        LogicalFinish();
    }
    
    protected override void OnFallToGround()
    {
        //落地销毁
        OnPlayDisappearEffect();
        LogicalFinish();
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
    public virtual void OnPlayDisappearEffect()
    {
        PlayDisappearEffect(ResourcePath.prefab_effect_bullet_BulletDisappear0001_tscn);
    }

    /// <summary>
    /// 播放撞墙特效
    /// </summary>
    public virtual void OnPlayCollisionEffect(KinematicCollision2D collision)
    {
        PlayCollisionEffect(collision, ResourcePath.prefab_effect_bullet_BulletSmoke0001_tscn);
    }

    /// <summary>
    /// 播放子弹消失特效
    /// </summary>
    public void PlayDisappearEffect(string path)
    {
        var effect = ObjectManager.GetPoolItem<IEffect>(path);
        var node = (Node2D)effect;
        node.GlobalPosition = AnimatedSprite.GlobalPosition;
        node.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        effect.PlayEffect();
    }
    
    
    /// <summary>
    /// 播放子弹撞墙消失特效
    /// </summary>
    public void PlayCollisionEffect(KinematicCollision2D collision, string path)
    {
        var effect = ObjectManager.GetPoolItem<IEffect>(path);
        var smoke = (Node2D)effect;
        var rotated = AnimatedSprite.Position.Rotated(Rotation);
        smoke.GlobalPosition = collision.GetPosition() + new Vector2(0, rotated.Y);
        smoke.GlobalRotation = collision.GetNormal().Angle();
        smoke.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        effect.PlayEffect();
    }
    
    protected override void Process(float delta)
    {
        if (ActivityMaterial.DynamicCollision)
        {
            //子弹高度大于 16 关闭碰撞检测
            CollisionShape2D.Disabled = Altitude >= 16;
        }
        //距离太大, 自动销毁
        if (MoveController.Enable)
        {
            CurrFlyDistance += BulletData.FlySpeed * delta;
            if (CurrFlyDistance >= BulletData.MaxDistance)
            {
                OnMaxDistance();
            }
        }
    }

    protected virtual void OnBodyEntered(Node2D body)
    {
        if (IsDestroyed)
        {
            return;
        }

        if (body is IHurt hurt)
        {
            OnCollisionTarget(hurt);
        }
    }
    
    protected virtual void OnArea2dEntered(Area2D other)
    {
        if (IsDestroyed)
        {
            return;
        }
        
        if (other is IHurt hurt)
        {
            OnCollisionTarget(hurt);
        }
    }

    public virtual void LogicalFinish()
    {
        ObjectPool.Reclaim(this);
    }
    
    public virtual void OnReclaim()
    {
        Visible = false;
        if (Particles2D != null)
        {
            foreach (var particles2D in Particles2D)
            {
                particles2D.Emitting = false;
            }
        }
        if (OnReclaimEvent != null)
        {
            OnReclaimEvent();
        }
        if (AffiliationArea != null)
        {
            AffiliationArea.RemoveItem(this);
        }
        GetParent().CallDeferred(Node.MethodName.RemoveChild, this);
    }

    public virtual void OnLeavePool()
    {
        Visible = true;
        MoveController.ClearForce();
        StopAllCoroutine();
        if (OnLeavePoolEvent != null)
        {
            OnLeavePoolEvent();
        }
    }

    /// <summary>
    /// 设置是否启用移动逻辑
    /// </summary>
    public void SetEnableMovement(bool v)
    {
        MoveController.Enable = v;
        CollisionArea.Monitoring = v;
        CollisionArea.Monitorable = v;
        Collision.Disabled = !v;
    }
}