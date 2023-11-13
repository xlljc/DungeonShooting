
using Config;
using Godot;

public static class FireManager
{
    
    /// <summary>
    /// 投抛弹壳的默认实现方式, shellId为弹壳id
    /// </summary>
    public static ActivityObject ThrowShell(Weapon weapon, ExcelConfig.ActivityBase shell, float speedScale = 1)
    {
        var startPos = weapon.ShellPoint.GlobalPosition;
        float startHeight;
        var master = weapon.Master;
        if (master != null)
        {
            var shellPosition = master.MountPoint.Position + weapon.ShellPoint.Position;
            startHeight = -shellPosition.Y;
            startPos.Y += startHeight;
        }
        else
        {
            startHeight = weapon.Altitude;
        }

        var direction = weapon.GlobalRotationDegrees + Utils.Random.RandomRangeInt(-30, 30) + 180;
        var verticalSpeed = Utils.Random.RandomRangeInt((int)(60 * speedScale), (int)(120 * speedScale));
        var velocity = new Vector2(Utils.Random.RandomRangeInt((int)(20 * speedScale), (int)(60 * speedScale)), 0).Rotated(direction * Mathf.Pi / 180);
        var rotate = Utils.Random.RandomRangeInt((int)(-720 * speedScale), (int)(720 * speedScale));
        var shellInstance = ActivityObject.Create(shell);
        shellInstance.Rotation = (master != null ? master.MountPoint.RealRotation : weapon.Rotation);
        shellInstance.Throw(startPos, startHeight, verticalSpeed, velocity, rotate);
        shellInstance.InheritVelocity(master != null ? master : weapon);
        if (master == null)
        {
            weapon.AffiliationArea.InsertItem(shellInstance);
        }
        else
        {
            master.AffiliationArea.InsertItem(shellInstance);
        }
        
        return shellInstance;
    }

    /// <summary>
    /// 通过武器发射子弹
    /// </summary>
    public static IBullet ShootBullet(Weapon weapon, float fireRotation, ExcelConfig.BulletBase bullet)
    {
        if (bullet.Type == 1) //实体子弹
        {
            return ShootSolidBullet(weapon, fireRotation, bullet);
        }
        else if (bullet.Type == 2) //激光子弹
        {
            return ShootLaser(weapon, fireRotation, bullet);
        }
        else
        {
            Debug.LogError("暂未支持的子弹类型: " + bullet.Type);
        }

        return null;
    }

    /// <summary>
    /// 通 Role 对象直接发射子弹
    /// </summary>
    public static IBullet ShootBullet(Role trigger, float fireRotation, ExcelConfig.BulletBase bullet)
    {
        if (bullet.Type == 1) //实体子弹
        {
            return ShootSolidBullet(trigger, fireRotation, bullet);
        }

        return null;
    }

    /// <summary>
    /// 发射子弹的默认实现方式
    /// </summary>
    private static Bullet ShootSolidBullet(Weapon weapon, float fireRotation, ExcelConfig.BulletBase bullet)
    {
        var data = new BulletData()
        {
            Weapon = weapon,
            BulletBase = bullet,
            TriggerRole = weapon.TriggerRole,
            Harm = Utils.Random.RandomConfigRange(bullet.HarmRange),
            Repel = Utils.Random.RandomConfigRange(bullet.RepelRange),
            MaxDistance = Utils.Random.RandomConfigRange(bullet.DistanceRange),
            FlySpeed = Utils.Random.RandomConfigRange(bullet.SpeedRange),
            VerticalSpeed = Utils.Random.RandomConfigRange(bullet.VerticalSpeed),
            BounceCount = Utils.Random.RandomConfigRange(bullet.BounceCount),
            Penetration = Utils.Random.RandomConfigRange(bullet.Penetration),
            Position = weapon.FirePoint.GlobalPosition,
        };
        
        var deviationAngle = Utils.Random.RandomConfigRange(bullet.DeviationAngleRange);
        if (weapon.TriggerRole != null)
        {
            data.Altitude = weapon.TriggerRole.GetFirePointAltitude();
            var roleState = weapon.TriggerRole.RoleState;
            data.Harm = roleState.CalcDamage(data.Harm);
            data.Repel = roleState.CalcBulletRepel(data.Repel);
            data.FlySpeed = roleState.CalcBulletSpeed(data.FlySpeed);
            data.MaxDistance = roleState.CalcBulletDistance(data.MaxDistance);
            data.BounceCount = roleState.CalcBulletBounceCount(data.BounceCount);
            data.Penetration = roleState.CalcBulletPenetration(data.Penetration);
            deviationAngle = roleState.CalcBulletDeviationAngle(deviationAngle);
            
            if (weapon.TriggerRole.IsAi) //只有玩家使用该武器才能获得正常速度的子弹
            {
                data.FlySpeed *= weapon.AiUseAttribute.AiAttackAttr.BulletSpeedScale;
            }
        }
        else
        {
            data.Altitude = 1;
        }

        data.Rotation = fireRotation + Mathf.DegToRad(deviationAngle);
        //创建子弹
        var bulletInstance = ObjectManager.GetBullet(bullet.Prefab);
        bulletInstance.InitData(data, weapon.GetAttackLayer());
        return bulletInstance;
    }

    /// <summary>
    /// 发射射线的默认实现方式
    /// </summary>
    private static Laser ShootLaser(Weapon weapon, float fireRotation, ExcelConfig.BulletBase bullet)
    {
        var data = new BulletData()
        {
            Weapon = weapon,
            BulletBase = bullet,
            TriggerRole = weapon.TriggerRole,
            Harm = Utils.Random.RandomConfigRange(bullet.HarmRange),
            Repel = Utils.Random.RandomConfigRange(bullet.RepelRange),
            MaxDistance = Utils.Random.RandomConfigRange(bullet.DistanceRange),
            BounceCount = Utils.Random.RandomConfigRange(bullet.BounceCount),
            LifeTime = Utils.Random.RandomConfigRange(bullet.LifeTimeRange),
            Position = weapon.FirePoint.GlobalPosition,
        };

        var deviationAngle = Utils.Random.RandomConfigRange(bullet.DeviationAngleRange);
        if (weapon.TriggerRole != null)
        {
            data.Altitude = weapon.TriggerRole.GetFirePointAltitude();
            var roleState = weapon.TriggerRole.RoleState;
            data.Harm = roleState.CalcDamage(data.Harm);
            data.Repel = roleState.CalcBulletRepel(data.Repel);
            data.BounceCount = roleState.CalcBulletBounceCount(data.BounceCount);
            deviationAngle = roleState.CalcBulletDeviationAngle(deviationAngle);
        }
        else
        {
            data.Altitude = 1;
        }

        data.Rotation = fireRotation + Mathf.DegToRad(deviationAngle);
        //创建激光
        var laser = ObjectManager.GetLaser(bullet.Prefab);
        laser.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        laser.InitData(data, weapon.GetAttackLayer(), 3);
        return laser;
    }
    
    //-----------------------------------------------------------------------------------
    
    /// <summary>
    /// 发射子弹的默认实现方式
    /// </summary>
    private static Bullet ShootSolidBullet(Role role, float fireRotation, ExcelConfig.BulletBase bullet)
    {
        var data = new BulletData()
        {
            Weapon = null,
            BulletBase = bullet,
            TriggerRole = role,
            Harm = Utils.Random.RandomConfigRange(bullet.HarmRange),
            Repel = Utils.Random.RandomConfigRange(bullet.RepelRange),
            MaxDistance = Utils.Random.RandomConfigRange(bullet.DistanceRange),
            FlySpeed = Utils.Random.RandomConfigRange(bullet.SpeedRange),
            VerticalSpeed = Utils.Random.RandomConfigRange(bullet.VerticalSpeed),
            BounceCount = Utils.Random.RandomConfigRange(bullet.BounceCount),
            Penetration = Utils.Random.RandomConfigRange(bullet.Penetration),
        };

        if (role is AdvancedRole advancedRole)
        {
            data.Position = advancedRole.MountPoint.GlobalPosition;
        }
        else if (role is Enemy enemy)
        {
            data.Position = enemy.FirePoint.GlobalPosition;
        }
        else
        {
            data.Position = role.AnimatedSprite.GlobalPosition;
        }
        
        var deviationAngle = Utils.Random.RandomConfigRange(bullet.DeviationAngleRange);
        data.Altitude = role.GetFirePointAltitude();
        var roleState = role.RoleState;
        data.Harm = roleState.CalcDamage(data.Harm);
        data.Repel = roleState.CalcBulletRepel(data.Repel);
        data.FlySpeed = roleState.CalcBulletSpeed(data.FlySpeed);
        data.MaxDistance = roleState.CalcBulletDistance(data.MaxDistance);
        data.BounceCount = roleState.CalcBulletBounceCount(data.BounceCount);
        data.Penetration = roleState.CalcBulletPenetration(data.Penetration);
        deviationAngle = roleState.CalcBulletDeviationAngle(deviationAngle);
        
        // if (role.IsAi) //只有玩家使用该武器才能获得正常速度的子弹
        // {
        //     data.FlySpeed *= weapon.AiUseAttribute.AiAttackAttr.BulletSpeedScale;
        // }

        data.Rotation = fireRotation + Mathf.DegToRad(deviationAngle);
        //创建子弹
        var bulletInstance = ObjectManager.GetBullet(bullet.Prefab);
        bulletInstance.InitData(data, role.AttackLayer);
        return bulletInstance;
    }
    
}