namespace Config;

public class Weapon 
{
    /// <summary>
    /// 物体唯一id <br/>
    /// 不需要添加类型前缀
    /// </summary>
    public string Id;

    /// <summary>
    /// 武器 Prefab, 必须继承场景 "res://prefab/weapon/Weapon.tscn"
    /// </summary>
    public string Prefab;

    /// <summary>
    /// 重量
    /// </summary>
    public float Weight;

    /// <summary>
    /// 武器显示的名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 武器的图标
    /// </summary>
    public string Icon;

    /// <summary>
    /// 武器类型: <br/>
    /// 1.副武器 <br/>
    /// 2.主武器 <br/>
    /// 3.重型武器
    /// </summary>
    public byte WeightType;

    /// <summary>
    /// 是否连续发射, 如果为false, 则每次发射都需要扣动扳机
    /// </summary>
    public bool ContinuousShoot;

    /// <summary>
    /// 弹夹容量
    /// </summary>
    public int AmmoCapacity;

    /// <summary>
    /// 弹药容量上限
    /// </summary>
    public int MaxAmmoCapacity;

    /// <summary>
    /// 起始备用弹药数量
    /// </summary>
    public int StandbyAmmoCapacity;

    /// <summary>
    /// 装弹时间, 单位: 秒
    /// </summary>
    public float ReloadTime;

    /// <summary>
    /// 每粒子弹是否是单独装填, 如果是, 那么每上一发子弹的时间就是 ReloadTime, 可以做霰弹武器装填效果
    /// </summary>
    public bool AloneReload;

    /// <summary>
    /// 单独装填时每次装填子弹数量, 必须要将 'AloneReload' 属性设置为 true
    /// </summary>
    public int AloneReloadCount;

    /// <summary>
    /// 单独装填的子弹时可以立即射击, 必须要将 'AloneReload' 属性设置为 true
    /// </summary>
    public bool AloneReloadCanShoot;

    /// <summary>
    /// 是否为松发开火, 也就是松开扳机才开火, 若要启用该属性, 必须将 'ContinuousShoot' 设置为 false
    /// </summary>
    public bool LooseShoot;

    /// <summary>
    /// 最少需要蓄力多久才能开火, 必须将 'LooseShoot' 设置为 true
    /// </summary>
    public float MinChargeTime;

    /// <summary>
    /// 连续发射最小次数, 仅当 ContinuousShoot 为 false 时生效
    /// </summary>
    public int MinContinuousCount;

    /// <summary>
    /// 连续发射最大次数, 仅当 ContinuousShoot 为 false 时生效
    /// </summary>
    public int MaxContinuousCount;

    /// <summary>
    /// 按下一次扳机后需要多长时间才能再次感应按下
    /// </summary>
    public float TriggerInterval;

    /// <summary>
    /// 初始射速, 初始每分钟能开火次数
    /// </summary>
    public float StartFiringSpeed;

    /// <summary>
    /// 最终射速, 最终每分钟能开火次数, 仅当 ContinuousShoot 为 true 时生效
    /// </summary>
    public float FinalFiringSpeed;

    /// <summary>
    /// 按下扳机并开火后射速增加速率
    /// </summary>
    public float FiringSpeedAddSpeed;

    /// <summary>
    /// 松开扳机后射速消散速率
    /// </summary>
    public float FiringSpeedBackSpeed;

    /// <summary>
    /// 单次开火发射子弹最小数量
    /// </summary>
    public int MinFireBulletCount;

    /// <summary>
    /// 单次开火发射子弹最大数量
    /// </summary>
    public int MaxFireBulletCount;

    /// <summary>
    /// 开火前延时
    /// </summary>
    public float DelayedTime;

    /// <summary>
    /// 初始散射半径
    /// </summary>
    public float StartScatteringRange;

    /// <summary>
    /// 最终散射半径
    /// </summary>
    public float FinalScatteringRange;

    /// <summary>
    /// 每次发射后散射增加值
    /// </summary>
    public float ScatteringRangeAddValue;

    /// <summary>
    /// 松开扳机后散射销退速率
    /// </summary>
    public float ScatteringRangeBackSpeed;

    /// <summary>
    /// 松开扳机多久后开始销退散射值 (单位: 秒)
    /// </summary>
    public float ScatteringRangeBackTime;

    /// <summary>
    /// 子弹飞行最大距离
    /// </summary>
    public float MaxDistance;

    /// <summary>
    /// 子弹飞行最小距离
    /// </summary>
    public float MinDistance;

    /// <summary>
    /// 最大后坐力 (仅用于开火后武器身抖动)
    /// </summary>
    public float MaxBacklash;

    /// <summary>
    /// 最小后坐力 (仅用于开火后武器身抖动)
    /// </summary>
    public float MinBacklash;

    /// <summary>
    /// 后坐力偏移回归回归速度
    /// </summary>
    public float BacklashRegressionSpeed;

    /// <summary>
    /// 开火后武器口上抬角度
    /// </summary>
    public float UpliftAngle;

    /// <summary>
    /// 武器默认上抬角度
    /// </summary>
    public float DefaultAngle;

    /// <summary>
    /// 开火后武器口角度恢复速度倍数
    /// </summary>
    public float UpliftAngleRestore;

    /// <summary>
    /// 默认射出的子弹id
    /// </summary>
    public string BulletId;

    /// <summary>
    /// 投抛状态下物体碰撞器大小
    /// </summary>
    public Godot.Vector2 ThrowCollisionSize;

}