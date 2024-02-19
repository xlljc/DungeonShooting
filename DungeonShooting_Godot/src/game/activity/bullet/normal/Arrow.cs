
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
        Debug.Log("IsFallOver: ", IsFallOver, ", ", CollisionLayer);
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

    public override void OnMoveCollision(KinematicCollision2D collision)
    {
        CurrentBounce++;
        if (CurrentBounce > BulletData.BounceCount) //反弹次数超过限制
        {
            //创建粒子特效
            OnPlayCollisionEffect(collision);
            ObjectPool.Reclaim(this);
        }
    }

    public override void OnCollisionTarget(IHurt hurt)
    {
        base.OnCollisionTarget(hurt);
        var activityObject = hurt.GetActivityObject();
        if (activityObject != null)
        {
            CallDeferred(nameof(OnBindTarget), activityObject);
        }
    }

    public override void LogicalFinish()
    {
        SetEnableMovement(false);
        var slideCollision = GetLastSlideCollision();
        if (slideCollision != null)
        {
            Position -= slideCollision.GetTravel();
        }
    }

    //将弓箭挂载到目标物体上
    private void OnBindTarget(ActivityObject activityObject)
    {
        if (activityObject.IsDestroyed)
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
    }

    public void OnUnmount(ActivityObject target)
    {
        SetOriginCollisionLayerValue(PhysicsLayer.Prop, true);
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