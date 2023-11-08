
using System;
using Config;
using Godot;

public partial class Weapon
{
    //-------------------------------- 子弹相关 -----------------------------

    /// <summary>
    /// 投抛弹壳的默认实现方式, shellId为弹壳id
    /// </summary>
    protected ActivityObject ThrowShell(ExcelConfig.ActivityBase shell, float speedScale = 1)
    {
        var startPos = ShellPoint.GlobalPosition;
        float startHeight;
        if (Master != null)
        {
            var shellPosition = (Master != null ? Master.MountPoint.Position : Position) + ShellPoint.Position;
            startHeight = -shellPosition.Y;
            startPos.Y += startHeight;
        }
        else
        {
            startHeight = Altitude;
        }

        var direction = GlobalRotationDegrees + Utils.Random.RandomRangeInt(-30, 30) + 180;
        var verticalSpeed = Utils.Random.RandomRangeInt((int)(60 * speedScale), (int)(120 * speedScale));
        var velocity = new Vector2(Utils.Random.RandomRangeInt((int)(20 * speedScale), (int)(60 * speedScale)), 0).Rotated(direction * Mathf.Pi / 180);
        var rotate = Utils.Random.RandomRangeInt((int)(-720 * speedScale), (int)(720 * speedScale));
        var shellInstance = Create(shell);
        shellInstance.Rotation = (Master != null ? Master.MountPoint.RealRotation : Rotation);
        shellInstance.Throw(startPos, startHeight, verticalSpeed, velocity, rotate);
        shellInstance.InheritVelocity(Master != null ? Master : this);
        if (Master == null)
        {
            AffiliationArea.InsertItem(shellInstance);
        }
        else
        {
            Master.AffiliationArea.InsertItem(shellInstance);
        }
        
        return shellInstance;
    }

    /// <summary>
    /// 发射子弹
    /// </summary>
    protected IBullet ShootBullet(float fireRotation, ExcelConfig.BulletBase bullet)
    {
        if (bullet.Type == 1) //实体子弹
        {
            return ShootSolidBullet(fireRotation, bullet);
        }
        else if (bullet.Type == 2) //激光子弹
        {
            return ShootLaser(fireRotation, bullet);
        }

        return null;
    }

    /// <summary>
    /// 发射子弹的默认实现方式
    /// </summary>
    private Bullet ShootSolidBullet(float fireRotation, ExcelConfig.BulletBase bullet)
    {
        var data = new BulletData()
        {
            Weapon = this,
            BulletBase = bullet,
            TriggerRole = TriggerRole,
            Harm = Utils.Random.RandomConfigRange(bullet.HarmRange),
            Repel = Utils.Random.RandomConfigRange(bullet.RepelRange),
            MaxDistance = Utils.Random.RandomConfigRange(bullet.DistanceRange),
            FlySpeed = Utils.Random.RandomConfigRange(bullet.SpeedRange),
            VerticalSpeed = Utils.Random.RandomConfigRange(bullet.VerticalSpeed),
            BounceCount = Utils.Random.RandomConfigRange(bullet.BounceCount),
            Penetration = Utils.Random.RandomConfigRange(bullet.Penetration),
            Position = FirePoint.GlobalPosition,
        };
        
        var deviationAngle = Utils.Random.RandomConfigRange(bullet.DeviationAngleRange);
        if (TriggerRole != null)
        {
            var roleState = TriggerRole.RoleState;
            data.Harm = roleState.CalcDamage(data.Harm);
            data.Repel = roleState.CalcBulletRepel(this, data.Repel);
            data.FlySpeed = roleState.CalcBulletSpeed(this, data.FlySpeed);
            data.MaxDistance = roleState.CalcBulletDistance(this, data.MaxDistance);
            data.BounceCount = roleState.CalcBulletBounceCount(this, data.BounceCount);
            deviationAngle = roleState.CalcBulletDeviationAngle(this, deviationAngle);
            
            if (TriggerRole.IsAi) //只有玩家使用该武器才能获得正常速度的子弹
            {
                data.FlySpeed *= AiUseAttribute.AiAttackAttr.BulletSpeedScale;
            }
        }

        data.Rotation = fireRotation + Mathf.DegToRad(deviationAngle);
        //创建子弹
        var bulletInstance = ObjectManager.GetBullet(bullet.Prefab);
        bulletInstance.InitData(data, GetAttackLayer());
        return bulletInstance;
    }

    /// <summary>
    /// 发射射线的默认实现方式
    /// </summary>
    private Laser ShootLaser(float fireRotation, ExcelConfig.BulletBase bullet)
    {
        var data = new BulletData()
        {
            Weapon = this,
            BulletBase = bullet,
            TriggerRole = TriggerRole,
            Harm = Utils.Random.RandomConfigRange(bullet.HarmRange),
            Repel = Utils.Random.RandomConfigRange(bullet.RepelRange),
            MaxDistance = Utils.Random.RandomConfigRange(bullet.DistanceRange),
            BounceCount = Utils.Random.RandomConfigRange(bullet.BounceCount),
            LifeTime = Utils.Random.RandomConfigRange(bullet.LifeTimeRange),
            Position = FirePoint.GlobalPosition,
        };

        var deviationAngle = Utils.Random.RandomConfigRange(bullet.DeviationAngleRange);
        if (TriggerRole != null)
        {
            var roleState = TriggerRole.RoleState;
            data.Harm = roleState.CalcDamage(data.Harm);
            data.Repel = roleState.CalcBulletRepel(this, data.Repel);
            data.BounceCount = roleState.CalcBulletBounceCount(this, data.BounceCount);
            deviationAngle = roleState.CalcBulletDeviationAngle(this, deviationAngle);
        }

        data.Rotation = fireRotation + Mathf.DegToRad(deviationAngle);
        //创建激光
        var laser = ObjectManager.GetLaser(bullet.Prefab);
        laser.AddToActivityRoot(RoomLayerEnum.YSortLayer);
        laser.InitData(data, GetAttackLayer(), 3);
        return laser;
    }
}