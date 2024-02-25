
using Godot;

/// <summary>
/// 弓箭
/// </summary>
[Tool]
public partial class Arrow : Bullet, IMountItem
{
    [Export, ExportFillNode]
    public AnimatedSprite2D HalfSprite { get; set; }

    public override void InitData(BulletData data, uint attackLayer)
    {
        base.InitData(data, attackLayer);
        SetEnableMovement(true);
        EnableVerticalMotion = false;
        DefaultLayer = RoomLayerEnum.NormalLayer;
    }

    public override CheckInteractiveResult CheckInteractive(ActivityObject master)
    {
        if (master is Role role) //如果角色有弓箭武器, 则可以拾起地上的箭
        {
            var index = role.WeaponPack.FindIndex((weapon, index) =>
            {
                return weapon.ActivityBase.Id == Ids.Id_weapon0016;
            });
            if (index >= 0)
            {
                var weapon = role.WeaponPack.GetItem(index);
                weapon.SetResidueAmmo(weapon.ResidueAmmo + 1);
                ObjectPool.Reclaim(this);
            }
        }
        return base.CheckInteractive(master);
    }

    public override void OnPlayDisappearEffect()
    {
    }

    protected override void OnArea2dEntered(Area2D other)
    {
        if (Velocity.Length() >= BulletData.FlySpeed / 2f)
        {
            base.OnArea2dEntered(other);
        }
    }

    protected override void OnBodyEntered(Node2D body)
    {
        if (Velocity.Length() >= BulletData.FlySpeed / 2f)
        {
            base.OnBodyEntered(body);
        }
    }

    public override void OnCollisionTarget(IHurt hurt)
    {
        base.OnCollisionTarget(hurt);
        if (CurrentPenetration > BulletData.Penetration)
        {
            var activityObject = hurt.GetActivityObject();
            if (activityObject != null)
            {
                CallDeferred(nameof(OnBindTarget), activityObject);
            }
        }
    }

    public override void OnMoveCollision(KinematicCollision2D collision)
    {
        if (Velocity.Length() >= BulletData.FlySpeed / 2f)
        {
            base.OnMoveCollision(collision);
        }
    }

    protected override void OnThrowOver()
    {
        SetOriginCollisionLayerValue(PhysicsLayer.Prop, true);
        if (!Utils.CollisionMaskWithLayer(CollisionLayer, PhysicsLayer.Prop))
        {
            CollisionLayer |= PhysicsLayer.Prop;
        }
    }

    public override void LogicalFinish()
    {
        if (State == BulletStateEnum.CollisionTarget) //碰撞到目标, 直接冻结
        {
            SetEnableMovement(false);
            var slideCollision = GetLastSlideCollision();
            if (slideCollision != null)
            {
                Position -= slideCollision.GetTravel();
            }
        }
        else if (State == BulletStateEnum.MaxDistance) //到达最大动力距离, 则开始下坠
        {
            EnableVerticalMotion = true;
        }
        else if (State == BulletStateEnum.FallToGround) //落地, 啥也不干
        {
            
        }
        else
        {
            //Debug.Log("碰撞速度: " + Velocity.Length());
            base.LogicalFinish();
        }
    }

    //将弓箭挂载到目标物体上
    private void OnBindTarget(ActivityObject activityObject)
    {
        if (activityObject.IsDestroyed) //目标已经销毁
        {
            OnUnmount(activityObject);
        }
        else
        {
            Altitude = Mathf.Max(1, -activityObject.ToLocal(GlobalPosition).Y);
            activityObject.AddMountObject(this);
        }
    }

    public void OnMount(ActivityObject target)
    {
        Reparent(target);
        AnimatedSprite.Play(AnimatorNames.HalfEnd);
        HalfSprite.Visible = true;
        RefreshBulletColor(false);
        EnableVerticalMotion = false;
    }

    public void OnUnmount(ActivityObject target)
    {
        AnimatedSprite.Play(AnimatorNames.Default);
        HalfSprite.Visible = false;
        SetEnableMovement(true);
        EnableVerticalMotion = true;
        MoveController.ClearForce();
        MoveController.BasisVelocity = Vector2.Zero;
        ShadowOffset = new Vector2(0, 1);
        Throw(Mathf.Max(3, Altitude), Utils.Random.RandomRangeInt(50, 80), Vector2.Zero, Utils.Random.RandomRangeInt(-30, 30));
        InheritVelocity(target);
    }

    public override void OnLeavePool()
    {
        SetOriginCollisionLayerValue(PhysicsLayer.Prop, false);
        if (Utils.CollisionMaskWithLayer(CollisionLayer, PhysicsLayer.Prop))
        {
            CollisionLayer ^= PhysicsLayer.Prop;
        }
        base.OnLeavePool();
    }
}