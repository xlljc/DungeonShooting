
using System;
using System.Collections;
using Godot;

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

    /// <summary>
    /// 当前穿透次数
    /// </summary>
    public int CurrentPenetration { get; protected set; } = 0;
    
    //当前子弹已经飞行的距离
    private float CurrFlyDistance = 0;

    private bool _init = false;
    
    public virtual void InitData(BulletData data, uint attackLayer)
    {
        if (!_init)
        {
            CollisionArea.AreaEntered += OnArea2dEntered;
            _init = true;
        }

        CurrentBounce = 0;
        CurrentPenetration = 0;
        CurrFlyDistance = 0;
        
        BounceLockRotation = false;
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
            var effect = ObjectManager.GetPoolItem<IEffect>(ResourcePath.prefab_effect_weapon_BulletSmoke_tscn);
            var smoke = (Node2D)effect;
            var rotated = AnimatedSprite.Position.Rotated(Rotation);
            smoke.GlobalPosition = collision.GetPosition() + new Vector2(0, rotated.Y);
            smoke.GlobalRotation = collision.GetNormal().Angle();
            smoke.AddToActivityRoot(RoomLayerEnum.YSortLayer);
            effect.PlayEffect();
            DoReclaim();
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

            //击退
            if (role is not Player) //目标不是玩家才会触发击退
            {
                if (BulletData.Repel != 0)
                {
                    role.MoveController.AddForce(Velocity.Normalized() * BulletData.Repel);
                }
            }
            
            //造成伤害
            role.CallDeferred(nameof(AdvancedRole.Hurt), BulletData.Harm, Rotation);

            //穿透次数
            CurrentPenetration++;
            if (CurrentPenetration > BulletData.Penetration)
            {
                DoReclaim();
            }
        }
    }

    /// <summary>
    /// 到达最大运行距离
    /// </summary>
    public virtual void OnMaxDistance()
    {
        PlayDisappearEffect();
        DoReclaim();
    }
    
    /// <summary>
    /// 子弹生命周期结束
    /// </summary>
    public virtual void OnLimeOver()
    {
        PlayDisappearEffect();
        DoReclaim();
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
        var effect = ObjectManager.GetPoolItem<IEffect>(ResourcePath.prefab_effect_weapon_BulletDisappear_tscn);
        var node = (Node2D)effect;
        node.GlobalPosition = AnimatedSprite.GlobalPosition;
        node.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        effect.PlayEffect();
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
    
    public virtual void DoReclaim()
    {
        ObjectPool.Reclaim(this);
    }
    
    public virtual void OnReclaim()
    {
        if (OnReclaimEvent != null)
        {
            OnReclaimEvent();
        }
        if (AffiliationArea != null)
        {
            AffiliationArea.RemoveItem(this);
        }
        ShowOutline = false;
        GetParent().CallDeferred(Node.MethodName.RemoveChild, this);
    }

    public virtual void OnLeavePool()
    {
        MoveController.ClearForce();
        StopAllCoroutine();
        if (OnLeavePoolEvent != null)
        {
            OnLeavePoolEvent();
        }
    }
}