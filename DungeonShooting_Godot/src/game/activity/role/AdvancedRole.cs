using Godot;

/// <summary>
/// 高级角色，可以拾起和使用武器
/// </summary>
public abstract partial class AdvancedRole : Role
{
    /// <summary>
    /// 角色携带的武器背包
    /// </summary>
    public Package<Weapon, AdvancedRole> WeaponPack { get; private set; }

    /// <summary>
    /// 武器挂载点
    /// </summary>
    [Export, ExportFillNode]
    public MountRotation MountPoint { get; set; }
    /// <summary>
    /// 背后武器的挂载点
    /// </summary>
    [Export, ExportFillNode]
    public Marker2D BackMountPoint { get; set; }
    
    /// <summary>
    /// 近战碰撞检测区域
    /// </summary>
    [Export, ExportFillNode]
    public Area2D MeleeAttackArea { get; set; }
    
    /// <summary>
    /// 近战碰撞检测区域的碰撞器
    /// </summary>
    [Export, ExportFillNode]
    public CollisionPolygon2D MeleeAttackCollision { get; set; }

    /// <summary>
    /// 近战攻击时挥动武器的角度
    /// </summary>
    [Export]
    public float MeleeAttackAngle { get; set; } = 120;

    /// <summary>
    /// 武器挂载点是否始终指向目标
    /// </summary>
    public bool MountLookTarget { get; set; } = true;

    /// <summary>
    /// 是否处于近战攻击中
    /// </summary>
    public bool IsMeleeAttack { get; private set; }
    
    //近战计时器
    private float _meleeAttackTimer = 0;

    /// <summary>
    /// 当拾起某个武器时调用
    /// </summary>
    protected virtual void OnPickUpWeapon(Weapon weapon)
    {
    }
    
    /// <summary>
    /// 当扔掉某个武器时调用
    /// </summary>
    protected virtual void OnThrowWeapon(Weapon weapon)
    {
    }

    /// <summary>
    /// 当切换到某个武器时调用
    /// </summary>
    protected virtual void OnExchangeWeapon(Weapon weapon)
    {
    }

    public override void OnInit()
    {
        base.OnInit();
        WeaponPack = AddComponent<Package<Weapon, AdvancedRole>>();
        WeaponPack.SetCapacity(4);
        
        MountPoint.Master = this;
        
        MeleeAttackCollision.Disabled = true;
        //切换武器回调
        WeaponPack.ChangeActiveItemEvent += OnChangeActiveItem;
        //近战区域进入物体
        MeleeAttackArea.BodyEntered += OnMeleeAttackBodyEntered;
    }

    protected override void Process(float delta)
    {
        if (IsDie)
        {
            return;
        }

        if (_meleeAttackTimer > 0)
        {
            _meleeAttackTimer -= delta;
        }
        
        base.Process(delta);
    }
    
    /// <summary>
    /// 当武器放到后背时调用, 用于设置武器位置和角度
    /// </summary>
    /// <param name="weapon">武器实例</param>
    /// <param name="index">放入武器背包的位置</param>
    public virtual void OnPutBackMount(Weapon weapon, int index)
    {
        if (index < 8)
        {
            if (index % 2 == 0)
            {
                weapon.Position = new Vector2(-4, 3);
                weapon.RotationDegrees = 90 - (index / 2f) * 20;
                weapon.Scale = new Vector2(-1, 1);
            }
            else
            {
                weapon.Position = new Vector2(4, 3);
                weapon.RotationDegrees = 270 + (index - 1) / 2f * 20;
                weapon.Scale = new Vector2(1, 1);
            }
        }
        else
        {
            weapon.Visible = false;
        }
    }
    
    protected override void OnAffiliationChange(AffiliationArea prevArea)
    {
        //身上的武器的所属区域也得跟着变
        WeaponPack.ForEach((weapon, i) =>
        {
            if (AffiliationArea != null)
            {
                AffiliationArea.InsertItem(weapon);
            }
            else if (weapon.AffiliationArea != null)
            {
                weapon.AffiliationArea.RemoveItem(weapon);
            }
        });
    }
    
    public override void LookTargetPosition(Vector2 pos)
    {
        LookPosition = pos;
        if (MountLookTarget)
        {
            //脸的朝向
            var gPos = Position;
            if (pos.X > gPos.X && Face == FaceDirection.Left)
            {
                Face = FaceDirection.Right;
            }
            else if (pos.X < gPos.X && Face == FaceDirection.Right)
            {
                Face = FaceDirection.Left;
            }
            //枪口跟随目标
            MountPoint.SetLookAt(pos);
        }
    }

    /// <summary>
    /// 返回所有武器是否弹药都打光了
    /// </summary>
    public bool IsAllWeaponTotalAmmoEmpty()
    {
        foreach (var weapon in WeaponPack.ItemSlot)
        {
            if (weapon != null && !weapon.IsTotalAmmoEmpty())
            {
                return false;
            }
        }

        return true;
    }
    
    //-------------------------------------------------------------------------------------
    
    /// <summary>
    /// 拾起一个武器, 返回是否成功拾起, 如果不想立刻切换到该武器, exchange 请传 false
    /// </summary>
    /// <param name="weapon">武器对象</param>
    /// <param name="exchange">是否立即切换到该武器, 默认 true </param>
    public bool PickUpWeapon(Weapon weapon, bool exchange = true)
    {
        if (WeaponPack.PickupItem(weapon, exchange) != -1)
        {
            //从可互动队列中移除
            InteractiveItemList.Remove(weapon);
            OnPickUpWeapon(weapon);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public void ExchangeNextWeapon()
    {
        var weapon = WeaponPack.ActiveItem;
        WeaponPack.ExchangeNext();
        if (WeaponPack.ActiveItem != weapon)
        {
            OnExchangeWeapon(WeaponPack.ActiveItem);
        }
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public void ExchangePrevWeapon()
    {
        var weapon = WeaponPack.ActiveItem;
        WeaponPack.ExchangePrev();
        if (WeaponPack.ActiveItem != weapon)
        {
            OnExchangeWeapon(WeaponPack.ActiveItem);
        }
    }

    /// <summary>
    /// 扔掉当前使用的武器, 切换到上一个武器
    /// </summary>
    public void ThrowWeapon()
    {
        ThrowWeapon(WeaponPack.ActiveIndex);
    }

    /// <summary>
    /// 扔掉指定位置的武器
    /// </summary>
    /// <param name="index">武器在武器背包中的位置</param>
    public void ThrowWeapon(int index)
    {
        var weapon = WeaponPack.GetItem(index);
        if (weapon == null)
        {
            return;
        }

        var temp = weapon.AnimatedSprite.Position;
        if (Face == FaceDirection.Left)
        {
            temp.Y = -temp.Y;
        }
        //var pos = GlobalPosition + temp.Rotated(weapon.GlobalRotation);
        WeaponPack.RemoveItem(index);
        //播放抛出效果
        weapon.ThrowWeapon(this, GlobalPosition);
    }
    
    /// <summary>
    /// 切换到下一个武器
    /// </summary>
    public void ExchangeNextActiveProp()
    {
        var prop = ActivePropsPack.ActiveItem;
        ActivePropsPack.ExchangeNext();
        if (prop != ActivePropsPack.ActiveItem)
        {
            OnExchangeActiveProp(ActivePropsPack.ActiveItem);
        }
    }

    /// <summary>
    /// 切换到上一个武器
    /// </summary>
    public void ExchangePrevActiveProp()
    {
        var prop = ActivePropsPack.ActiveItem;
        ActivePropsPack.ExchangePrev();
        if (prop != ActivePropsPack.ActiveItem)
        {
            OnExchangeActiveProp(ActivePropsPack.ActiveItem);
        }
    }

    //-------------------------------------------------------------------------------------
    

    /// <summary>
    /// 触发换弹
    /// </summary>
    public virtual void Reload()
    {
        if (WeaponPack.ActiveItem != null)
        {
            WeaponPack.ActiveItem.Reload();
        }
    }
    
    public override void Attack()
    {
        if (!IsMeleeAttack && WeaponPack.ActiveItem != null)
        {
            WeaponPack.ActiveItem.Trigger(this);
        }
    }

    /// <summary>
    /// 触发近战攻击
    /// </summary>
    public virtual void MeleeAttack()
    {
        if (IsMeleeAttack || _meleeAttackTimer > 0)
        {
            return;
        }

        if (WeaponPack.ActiveItem != null && WeaponPack.ActiveItem.Attribute.CanMeleeAttack)
        {
            IsMeleeAttack = true;
            _meleeAttackTimer = RoleState.MeleeAttackTime;
            MountLookTarget = false;
            
            //WeaponPack.ActiveItem.TriggerMeleeAttack(this);
            //播放近战动画
            this.PlayAnimation_MeleeAttack(() =>
            {
                MountLookTarget = true;
                IsMeleeAttack = false;
            });
        }
    }

    /// <summary>
    /// 切换当前使用的武器的回调
    /// </summary>
    private void OnChangeActiveItem(Weapon weapon)
    {
        //这里处理近战区域
        if (weapon != null)
        {
            MeleeAttackCollision.Polygon = Utils.CreateSectorPolygon(
                Utils.ConvertAngle(-MeleeAttackAngle / 2f),
                (weapon.GetLocalFirePosition() - weapon.GripPoint.Position).Length() * 1.2f,
                MeleeAttackAngle,
                6
            );
            MeleeAttackArea.CollisionMask = AttackLayer | PhysicsLayer.Bullet;
        }
    }

    /// <summary>
    /// 近战区域碰到敌人
    /// </summary>
    private void OnMeleeAttackBodyEntered(Node2D body)
    {
        var activeWeapon = WeaponPack.ActiveItem;
        if (activeWeapon == null)
        {
            return;
        }
        var activityObject = body.AsActivityObject();
        if (activityObject != null)
        {
            if (activityObject is AdvancedRole role) //攻击角色
            {
                var damage = Utils.Random.RandomConfigRange(activeWeapon.Attribute.MeleeAttackHarmRange);
                damage = RoleState.CalcDamage(damage);
                
                //击退
                if (role is not Player) //目标不是玩家才会触发击退
                {
                    var attr = IsAi ? activeWeapon.AiUseAttribute : activeWeapon.PlayerUseAttribute;
                    var repel = Utils.Random.RandomConfigRange(attr.MeleeAttackRepelRange);
                    var position = role.GlobalPosition - MountPoint.GlobalPosition;
                    var v2 = position.Normalized() * repel;
                    role.MoveController.AddForce(v2);
                }
                
                role.CallDeferred(nameof(Hurt), this, damage, (role.GetCenterPosition() - GlobalPosition).Angle());
            }
            else if (activityObject is Bullet bullet) //攻击子弹
            {
                var attackLayer = bullet.AttackLayer;
                if (CollisionWithMask(attackLayer)) //是攻击玩家的子弹
                {
                    bullet.PlayDisappearEffect();
                    bullet.Destroy();
                }
            }
        }
    }

    public override float GetFirePointAltitude()
    {
        return -MountPoint.Position.Y;
    }

    public override float GetAttackRotation()
    {
        return MountPoint.RealRotation;
    }
    
    public override Vector2 GetMountPosition()
    {
        return MountPoint.GlobalPosition;
    }

    public override Node2D GetMountNode()
    {
        return MountPoint;
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        WeaponPack.Destroy();
    }
}