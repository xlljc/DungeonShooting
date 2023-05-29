using System;
using Godot;

/// <summary>
/// 武器上的属性
/// </summary>
public class WeaponAttribute
{
    /// <summary>
    /// 武器显示的名称
    /// </summary>
    public string Name = "Gun1";
    /// <summary>
    /// 武器 Prefab, 必须继承场景 "res://prefab/weapon/Weapon.tscn"
    /// </summary>
    public string WeaponPrefab = ResourcePath.prefab_weapon_Weapon_tscn;
    /// <summary>
    /// 武器类型
    /// </summary>
    public WeaponWeightType WeightType = WeaponWeightType.MainWeapon;
    /// <summary>
    /// 武器的图标
    /// </summary>
    public string Icon = ResourcePath.resource_sprite_gun_gun1_png;
    /// <summary>
    /// 动画序列帧
    /// </summary>
    public string SpriteFrames;
    /// <summary>
    /// 是否连续发射, 如果为false, 则每次发射都需要扣动扳机
    /// </summary>
    public bool ContinuousShoot = true;
    /// <summary>
    /// 弹夹容量
    /// </summary>
    public int AmmoCapacity = 30;
    /// <summary>
    /// 弹药容量上限
    /// </summary>
    public int MaxAmmoCapacity = 120;
    /// <summary>
    /// 起始备用弹药数量
    /// </summary>
    public int StandbyAmmoCapacity = 90;
    /// <summary>
    /// 装弹时间, 单位: 秒
    /// </summary>
    public float ReloadTime = 1.5f;
    /// <summary>
    /// 每粒子弹是否是单独装填, 如果是, 那么每上一发子弹的时间就是 ReloadTime, 可以做霰弹武器装填效果
    /// </summary>
    public bool AloneReload = false;
    /// <summary>
    /// 单独装填时每次装填子弹数量, 必须要将 'AloneReload' 属性设置为 true
    /// </summary>
    public int AloneReloadCount = 1;
    /// <summary>
    /// 单独装填的子弹时可以立即射击, 必须要将 'AloneReload' 属性设置为 true
    /// </summary>
    public bool AloneReloadCanShoot = false;
    /// <summary>
    /// 是否为松发开火, 也就是松开扳机才开火, 若要启用该属性, 必须将 'ContinuousShoot' 设置为 false
    /// </summary>
    public bool LooseShoot = false;
    /// <summary>
    /// 最少需要蓄力多久才能开火, 必须将 'LooseShoot' 设置为 true
    /// </summary>
    public float MinChargeTime = 0f;
    /// <summary>
    /// 连续发射最小次数, 仅当 ContinuousShoot 为 false 时生效
    /// </summary>
    public int MinContinuousCount = 1;
    /// <summary>
    /// 连续发射最大次数, 仅当 ContinuousShoot 为 false 时生效
    /// </summary>
    public int MaxContinuousCount = 1;
    /// <summary>
    /// 按下一次扳机后需要多长时间才能再次感应按下
    /// </summary>
    public float TriggerInterval = 0;
    /// <summary>
    /// 初始射速, 初始每分钟能开火次数
    /// </summary>
    public float StartFiringSpeed = 300;
    /// <summary>
    /// 最终射速, 最终每分钟能开火次数, 仅当 ContinuousShoot 为 true 时生效
    /// </summary>
    public float FinalFiringSpeed = 300;
    /// <summary>
    /// 按下扳机并开火后射速增加速率
    /// </summary>
    public float FiringSpeedAddSpeed = 2;
    /// <summary>
    /// 松开扳机后射速消散速率
    /// </summary>
    public float FiringSpeedBackSpeed = 10;
    /// <summary>
    /// 单次开火发射子弹最小数量
    /// </summary>
    public int MinFireBulletCount = 1;
    /// <summary>
    /// 单次开火发射子弹最大数量
    /// </summary>
    public int MaxFireBulletCount = 1;
    /// <summary>
    /// 开火前延时
    /// </summary>
    public float DelayedTime = 0f;
    /// <summary>
    /// 初始散射半径
    /// </summary>
    public float StartScatteringRange = 0;
    /// <summary>
    /// 最终散射半径
    /// </summary>
    public float FinalScatteringRange = 20;
    /// <summary>
    /// 每次发射后散射增加值
    /// </summary>
    public float ScatteringRangeAddValue = 2;
    /// <summary>
    /// 松开扳机后散射销退速率
    /// </summary>
    public float ScatteringRangeBackSpeed = 10;
    /// <summary>
    /// 松开扳机多久后开始销退散射值
    /// </summary>
    public float ScatteringRangeBackTime = 0f;
    /// <summary>
    /// 子弹飞行最大距离
    /// </summary>
    public float MaxDistance = 600;
    /// <summary>
    /// 子弹飞行最小距离
    /// </summary>
    public float MinDistance = 800;
    /// <summary>
    /// 开火位置
    /// </summary>
    public Vector2 FirePosition = new Vector2(11, 0);
    /// <summary>
    /// 精灵位置
    /// </summary>
    public Vector2 SpritePosition = new Vector2(4, -3);
    /// <summary>
    /// 弹壳投抛起始位置
    /// </summary>
    public Vector2 ShellPosition = new Vector2(5, -2.5f);
    /// <summary>
    /// 重量
    /// </summary>
    public float Weight = 11;
    /// <summary>
    /// 最大后坐力 (仅用于开火后武器身抖动)
    /// </summary>
    public float MaxBacklash = 4;
    /// <summary>
    /// 最小后坐力 (仅用于开火后武器身抖动)
    /// </summary>
    public float MinBacklash = 2;
    /// <summary>
    /// 后坐力偏移回归回归速度
    /// </summary>
    public float BacklashRegressionSpeed = 35f;
    /// <summary>
    /// 开火后武器口上抬角度
    /// </summary>
    public float UpliftAngle = 30;
    /// <summary>
    /// 武器默认上抬角度
    /// </summary>
    public float DefaultAngle = 0;
    /// <summary>
    /// 开火后武器口角度恢复速度倍数
    /// </summary>
    public float UpliftAngleRestore = 1f;
    /// <summary>
    /// 默认射出的子弹
    /// </summary>
    public string BulletId = ActivityIdPrefix.Bullet + "0001";
    /// <summary>
    /// 武器精灵投抛时的旋转中心坐标
    /// </summary>
    public Vector2 ThrowSpritePosition = new Vector2(0, 0);
    /// <summary>
    /// 投抛状态下物体碰撞器大小
    /// </summary>
    public Vector2 ThrowCollisionSize = new Vector2(20, 15);

    /// <summary>
    /// 克隆一份新的属性配置
    /// </summary>
    /// <returns></returns>
    public WeaponAttribute Clone()
    {
        var attr = _Clone();
        if (AiUseAttribute != null)
        {
            attr.AiUseAttribute = AiUseAttribute._Clone();
        }
        return attr;
    }

    private WeaponAttribute _Clone()
    {
        var attr = new WeaponAttribute();
        attr.Name = Name;
        attr.WeaponPrefab = WeaponPrefab;
        attr.WeightType = WeightType;
        attr.Icon = Icon;
        attr.SpriteFrames = SpriteFrames;
        attr.ContinuousShoot = ContinuousShoot;
        attr.AmmoCapacity = AmmoCapacity;
        attr.MaxAmmoCapacity = MaxAmmoCapacity;
        attr.StandbyAmmoCapacity = StandbyAmmoCapacity;
        attr.ReloadTime = ReloadTime;
        attr.AloneReload = AloneReload;
        attr.AloneReloadCount = AloneReloadCount;
        attr.AloneReloadCanShoot = AloneReloadCanShoot;
        attr.LooseShoot = LooseShoot;
        attr.MinChargeTime = MinChargeTime;
        attr.MinContinuousCount = MinContinuousCount;
        attr.MaxContinuousCount = MaxContinuousCount;
        attr.TriggerInterval = TriggerInterval;
        attr.StartFiringSpeed = StartFiringSpeed;
        attr.FinalFiringSpeed = FinalFiringSpeed;
        attr.FiringSpeedAddSpeed = FiringSpeedAddSpeed;
        attr.FiringSpeedBackSpeed = FiringSpeedBackSpeed;
        attr.MinFireBulletCount = MinFireBulletCount;
        attr.MaxFireBulletCount = MaxFireBulletCount;
        attr.DelayedTime = DelayedTime;
        attr.StartScatteringRange = StartScatteringRange;
        attr.FinalScatteringRange = FinalScatteringRange;
        attr.ScatteringRangeAddValue = ScatteringRangeAddValue;
        attr.ScatteringRangeBackSpeed = ScatteringRangeBackSpeed;
        attr.ScatteringRangeBackTime = ScatteringRangeBackTime;
        attr.MaxDistance = MaxDistance;
        attr.MinDistance = MinDistance;
        attr.FirePosition = FirePosition;
        attr.ShellPosition = ShellPosition;
        attr.SpritePosition = SpritePosition;
        attr.Weight = Weight;
        attr.MaxBacklash = MaxBacklash;
        attr.MinBacklash = MinBacklash;
        attr.BacklashRegressionSpeed = BacklashRegressionSpeed;
        attr.UpliftAngle = UpliftAngle;
        attr.DefaultAngle = DefaultAngle;
        attr.UpliftAngleRestore = UpliftAngleRestore;
        attr.AiTargetLockingTime = AiTargetLockingTime;
        attr.BulletId = BulletId;
        attr.ThrowSpritePosition = ThrowSpritePosition;
        attr.ThrowCollisionSize = ThrowCollisionSize;
        return attr;
    }

    //------------------------------ Ai相关 -----------------------------

    /// <summary>
    /// 用于Ai, 目标锁定时间, 也就是瞄准目标多久才会开火
    /// </summary>
    public float AiTargetLockingTime = 0;
    
    /// <summary>
    /// 用于Ai, Ai使用该武器发射的子弹速度缩放比
    /// </summary>
    public float AiBulletSpeedScale = 0.7f;

    /// <summary>
    /// 用于Ai, Ai使用该武器消耗弹药的概率, (0 - 1)
    /// </summary>
    public float AiAmmoConsumptionProbability = 0f;
    
    /// <summary>
    /// Ai 使用该武器时的武器数据, 设置该字段, 可让同一把武器在敌人和玩家手上有不同属性
    /// </summary>
    public WeaponAttribute AiUseAttribute;
}