using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public class Weapon
{
    /// <summary>
    /// 物体唯一id <br/>
    /// 不需要添加类型前缀
    /// </summary>
    [JsonInclude]
    public string Id { get; private set; }

    /// <summary>
    /// 武器 Prefab, 必须继承场景 "res://prefab/weapon/Weapon.tscn"
    /// </summary>
    [JsonInclude]
    public string Prefab { get; private set; }

    /// <summary>
    /// 重量
    /// </summary>
    [JsonInclude]
    public float Weight { get; private set; }

    /// <summary>
    /// 武器显示的名称
    /// </summary>
    [JsonInclude]
    public string Name { get; private set; }

    /// <summary>
    /// 武器的图标
    /// </summary>
    [JsonInclude]
    public string Icon { get; private set; }

    /// <summary>
    /// 武器类型: <br/>
    /// 1.副武器 <br/>
    /// 2.主武器 <br/>
    /// 3.重型武器
    /// </summary>
    [JsonInclude]
    public byte WeightType { get; private set; }

    /// <summary>
    /// 是否连续发射, 如果为false, 则每次发射都需要扣动扳机
    /// </summary>
    [JsonInclude]
    public bool ContinuousShoot { get; private set; }

    /// <summary>
    /// 弹夹容量
    /// </summary>
    [JsonInclude]
    public int AmmoCapacity { get; private set; }

    /// <summary>
    /// 弹药容量上限
    /// </summary>
    [JsonInclude]
    public int MaxAmmoCapacity { get; private set; }

    /// <summary>
    /// 起始备用弹药数量
    /// </summary>
    [JsonInclude]
    public int StandbyAmmoCapacity { get; private set; }

    /// <summary>
    /// 装弹时间, 单位: 秒
    /// </summary>
    [JsonInclude]
    public float ReloadTime { get; private set; }

    /// <summary>
    /// 每粒子弹是否是单独装填, 如果是, 那么每上一发子弹的时间就是 ReloadTime, 可以做霰弹武器装填效果
    /// </summary>
    [JsonInclude]
    public bool AloneReload { get; private set; }

    /// <summary>
    /// 单独装填时每次装填子弹数量, 必须要将 'AloneReload' 属性设置为 true
    /// </summary>
    [JsonInclude]
    public int AloneReloadCount { get; private set; }

    /// <summary>
    /// 单独装填的子弹时可以立即射击, 必须要将 'AloneReload' 属性设置为 true
    /// </summary>
    [JsonInclude]
    public bool AloneReloadCanShoot { get; private set; }

    /// <summary>
    /// 是否为松发开火, 也就是松开扳机才开火, 若要启用该属性, 必须将 'ContinuousShoot' 设置为 false
    /// </summary>
    [JsonInclude]
    public bool LooseShoot { get; private set; }

    /// <summary>
    /// 最少需要蓄力多久才能开火, 必须将 'LooseShoot' 设置为 true
    /// </summary>
    [JsonInclude]
    public float MinChargeTime { get; private set; }

    /// <summary>
    /// 连续发射最小次数, 仅当 ContinuousShoot 为 false 时生效
    /// </summary>
    [JsonInclude]
    public int MinContinuousCount { get; private set; }

    /// <summary>
    /// 连续发射最大次数, 仅当 ContinuousShoot 为 false 时生效
    /// </summary>
    [JsonInclude]
    public int MaxContinuousCount { get; private set; }

    /// <summary>
    /// 按下一次扳机后需要多长时间才能再次感应按下
    /// </summary>
    [JsonInclude]
    public float TriggerInterval { get; private set; }

    /// <summary>
    /// 初始射速, 初始每分钟能开火次数
    /// </summary>
    [JsonInclude]
    public float StartFiringSpeed { get; private set; }

    /// <summary>
    /// 最终射速, 最终每分钟能开火次数, 仅当 ContinuousShoot 为 true 时生效
    /// </summary>
    [JsonInclude]
    public float FinalFiringSpeed { get; private set; }

    /// <summary>
    /// 按下扳机并开火后射速增加速率
    /// </summary>
    [JsonInclude]
    public float FiringSpeedAddSpeed { get; private set; }

    /// <summary>
    /// 松开扳机后射速消散速率
    /// </summary>
    [JsonInclude]
    public float FiringSpeedBackSpeed { get; private set; }

    /// <summary>
    /// 单次开火发射子弹最小数量
    /// </summary>
    [JsonInclude]
    public int MinFireBulletCount { get; private set; }

    /// <summary>
    /// 单次开火发射子弹最大数量
    /// </summary>
    [JsonInclude]
    public int MaxFireBulletCount { get; private set; }

    /// <summary>
    /// 开火前延时
    /// </summary>
    [JsonInclude]
    public float DelayedTime { get; private set; }

    /// <summary>
    /// 初始散射半径
    /// </summary>
    [JsonInclude]
    public float StartScatteringRange { get; private set; }

    /// <summary>
    /// 最终散射半径
    /// </summary>
    [JsonInclude]
    public float FinalScatteringRange { get; private set; }

    /// <summary>
    /// 每次发射后散射增加值
    /// </summary>
    [JsonInclude]
    public float ScatteringRangeAddValue { get; private set; }

    /// <summary>
    /// 松开扳机后散射销退速率
    /// </summary>
    [JsonInclude]
    public float ScatteringRangeBackSpeed { get; private set; }

    /// <summary>
    /// 松开扳机多久后开始销退散射值 (单位: 秒)
    /// </summary>
    [JsonInclude]
    public float ScatteringRangeBackTime { get; private set; }

    /// <summary>
    /// 子弹飞行最大距离
    /// </summary>
    [JsonInclude]
    public float MaxDistance { get; private set; }

    /// <summary>
    /// 子弹飞行最小距离
    /// </summary>
    [JsonInclude]
    public float MinDistance { get; private set; }

    /// <summary>
    /// 最大后坐力 (仅用于开火后武器身抖动)
    /// </summary>
    [JsonInclude]
    public float MaxBacklash { get; private set; }

    /// <summary>
    /// 最小后坐力 (仅用于开火后武器身抖动)
    /// </summary>
    [JsonInclude]
    public float MinBacklash { get; private set; }

    /// <summary>
    /// 后坐力偏移回归回归速度
    /// </summary>
    [JsonInclude]
    public float BacklashRegressionSpeed { get; private set; }

    /// <summary>
    /// 开火后武器口上抬角度
    /// </summary>
    [JsonInclude]
    public float UpliftAngle { get; private set; }

    /// <summary>
    /// 武器默认上抬角度
    /// </summary>
    [JsonInclude]
    public float DefaultAngle { get; private set; }

    /// <summary>
    /// 开火后武器口角度恢复速度倍数
    /// </summary>
    [JsonInclude]
    public float UpliftAngleRestore { get; private set; }

    /// <summary>
    /// 默认射出的子弹id
    /// </summary>
    [JsonInclude]
    public string BulletId { get; private set; }

    /// <summary>
    /// 投抛状态下物体碰撞器大小
    /// </summary>
    [JsonInclude]
    public SerializeVector2 ThrowCollisionSize { get; private set; }

}