using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class Weapon
    {
        /// <summary>
        /// 武器属性id
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 属性绑定武器的Id,如果是Ai使用的数据, 则填空字符串串
        /// </summary>
        [JsonInclude]
        public string WeaponId;

        /// <summary>
        /// 武器显示的名称
        /// </summary>
        [JsonInclude]
        public string Name;

        /// <summary>
        /// 武器的图标
        /// </summary>
        [JsonInclude]
        public string Icon;

        /// <summary>
        /// 重量
        /// </summary>
        [JsonInclude]
        public float Weight;

        /// <summary>
        /// 武器类型: <br/>
        /// 1.副武器 <br/>
        /// 2.主武器 <br/>
        /// 3.重型武器
        /// </summary>
        [JsonInclude]
        public byte WeightType;

        /// <summary>
        /// 是否连续发射, 如果为false, 则每次发射都需要扣动扳机
        /// </summary>
        [JsonInclude]
        public bool ContinuousShoot;

        /// <summary>
        /// 弹夹容量
        /// </summary>
        [JsonInclude]
        public int AmmoCapacity;

        /// <summary>
        /// 弹药容量上限
        /// </summary>
        [JsonInclude]
        public int MaxAmmoCapacity;

        /// <summary>
        /// 默认起始备用弹药数量
        /// </summary>
        [JsonInclude]
        public int StandbyAmmoCapacity;

        /// <summary>
        /// 装弹时间 (单位: 秒)
        /// </summary>
        [JsonInclude]
        public float ReloadTime;

        /// <summary>
        /// 每粒子弹是否是单独装填, 如果是, 那么每上一发子弹的时间就是 ReloadTime, 可以做霰弹枪装填效果
        /// </summary>
        [JsonInclude]
        public bool AloneReload;

        /// <summary>
        /// 单独装填时每次装填子弹数量, 必须要将 'AloneReload' 属性设置为 true
        /// </summary>
        [JsonInclude]
        public int AloneReloadCount;

        /// <summary>
        /// 单独装弹模式下,从触发装弹到开始装第一发子弹中间的间隔时间, 必须要将 'AloneReload' 属性设置为 true
        /// </summary>
        [JsonInclude]
        public float AloneReloadBeginIntervalTime;

        /// <summary>
        /// 单独装弹模式下,从装完最后一发子弹到可以射击中间的间隔时间, 必须要将 'AloneReload' 属性设置为 true
        /// </summary>
        [JsonInclude]
        public float AloneReloadFinishIntervalTime;

        /// <summary>
        /// 单独装填的子弹时可以立即射击, 必须要将 'AloneReload' 属性设置为 true
        /// </summary>
        [JsonInclude]
        public bool AloneReloadCanShoot;

        /// <summary>
        /// 是否为松发开火, 也就是松开扳机才开火, 若要启用该属性, 必须将 'ContinuousShoot' 设置为 false
        /// </summary>
        [JsonInclude]
        public bool LooseShoot;

        /// <summary>
        /// 最少需要蓄力多久才能开火, 必须将 'LooseShoot' 设置为 true
        /// </summary>
        [JsonInclude]
        public float MinChargeTime;

        /// <summary>
        /// 连续发射最小次数, 仅当 ContinuousShoot 为 false 时生效
        /// </summary>
        [JsonInclude]
        public int MinContinuousCount;

        /// <summary>
        /// 连续发射最大次数, 仅当 ContinuousShoot 为 false 时生效
        /// </summary>
        [JsonInclude]
        public int MaxContinuousCount;

        /// <summary>
        /// 按下一次扳机后需要多长时间才能再次感应按下
        /// </summary>
        [JsonInclude]
        public float TriggerInterval;

        /// <summary>
        /// 初始射速, 初始每分钟能开火次数
        /// </summary>
        [JsonInclude]
        public float StartFiringSpeed;

        /// <summary>
        /// 最终射速, 最终每分钟能开火次数, 仅当 ContinuousShoot 为 true 时生效
        /// </summary>
        [JsonInclude]
        public float FinalFiringSpeed;

        /// <summary>
        /// 按下扳机并开火后射速每秒增加量
        /// </summary>
        [JsonInclude]
        public float FiringSpeedAddSpeed;

        /// <summary>
        /// 松开扳机后射速消散速率
        /// </summary>
        [JsonInclude]
        public float FiringSpeedBackSpeed;

        /// <summary>
        /// 单次开火发射子弹最小数量
        /// </summary>
        [JsonInclude]
        public int MinFireBulletCount;

        /// <summary>
        /// 单次开火发射子弹最大数量
        /// </summary>
        [JsonInclude]
        public int MaxFireBulletCount;

        /// <summary>
        /// 从按下扳机到发射第一发子弹的延时时, 如果中途松开扳机, 那么延时时间会重新计算, 必须将 'LooseShoot' 设置为 false
        /// </summary>
        [JsonInclude]
        public float DelayedTime;

        /// <summary>
        /// 初始散射半径
        /// </summary>
        [JsonInclude]
        public float StartScatteringRange;

        /// <summary>
        /// 最终散射半径
        /// </summary>
        [JsonInclude]
        public float FinalScatteringRange;

        /// <summary>
        /// 每次发射后散射增加值
        /// </summary>
        [JsonInclude]
        public float ScatteringRangeAddValue;

        /// <summary>
        /// 散射值销退速率
        /// </summary>
        [JsonInclude]
        public float ScatteringRangeBackSpeed;

        /// <summary>
        /// 开始销退散射值的延时时间
        /// </summary>
        [JsonInclude]
        public float ScatteringRangeBackDelayTime;

        /// <summary>
        /// 最小后坐力 (仅用于开火后武器身抖动)
        /// </summary>
        [JsonInclude]
        public float MinBacklash;

        /// <summary>
        /// 最大后坐力 (仅用于开火后武器身抖动)
        /// </summary>
        [JsonInclude]
        public float MaxBacklash;

        /// <summary>
        /// 后坐力偏移回归回归速度
        /// </summary>
        [JsonInclude]
        public float BacklashRegressionSpeed;

        /// <summary>
        /// 开火后武器口上抬角度
        /// </summary>
        [JsonInclude]
        public float UpliftAngle;

        /// <summary>
        /// 武器默认上抬角度
        /// </summary>
        [JsonInclude]
        public float DefaultAngle;

        /// <summary>
        /// 开火后武器口角度恢复速度倍数
        /// </summary>
        [JsonInclude]
        public float UpliftAngleRestore;

        /// <summary>
        /// 默认射出的子弹id
        /// </summary>
        [JsonInclude]
        public string BulletId;

        /// <summary>
        /// 子弹最小偏移角度 <br/>
        /// 用于设置子弹偏移朝向, 该属性和射半径效果类似, 但与其不同的是, 散射半径是用来控制枪口朝向的, 而该属性是控制子弹朝向的, 可用于制作霰弹枪子弹效果
        /// </summary>
        [JsonInclude]
        public float BulletMinDeviationAngle;

        /// <summary>
        /// 子弹最大偏移角度
        /// </summary>
        [JsonInclude]
        public float BulletMaxDeviationAngle;

        /// <summary>
        /// 子弹最大初速度
        /// </summary>
        [JsonInclude]
        public float BulletMaxSpeed;

        /// <summary>
        /// 子弹最小初速度
        /// </summary>
        [JsonInclude]
        public float BulletMinSpeed;

        /// <summary>
        /// 子弹飞行最小距离
        /// </summary>
        [JsonInclude]
        public float BulletMinDistance;

        /// <summary>
        /// 子弹飞行最大距离
        /// </summary>
        [JsonInclude]
        public float BulletMaxDistance;

        /// <summary>
        /// 投抛状态下物体碰撞器大小
        /// </summary>
        [JsonInclude]
        public SerializeVector2 ThrowCollisionSize;

        /// <summary>
        /// 射击音效
        /// </summary>
        public Sound ShootSound;

        /// <summary>
        /// 开始换弹音效
        /// </summary>
        public Sound BeginReloadSound;

        /// <summary>
        /// 开始换弹音效延时时间
        /// </summary>
        [JsonInclude]
        public float BeginReloadSoundDelayTime;

        /// <summary>
        /// 换弹音效
        /// </summary>
        public Sound ReloadSound;

        /// <summary>
        /// 换弹音效延时时间
        /// </summary>
        [JsonInclude]
        public float ReloadSoundDelayTime;

        /// <summary>
        /// 换弹结束音效
        /// </summary>
        public Sound ReloadFinishSound;

        /// <summary>
        /// 换弹结束音效在换弹结束前多久开始 <br/>
        /// 注意: 如果'AloneReload'为true, 那么当前属性的值应该小于'AloneReloadFinishIntervalTime'
        /// </summary>
        [JsonInclude]
        public float ReloadFinishSoundAdvanceTime;

        /// <summary>
        /// 上膛音效
        /// </summary>
        public Sound EquipSound;

        /// <summary>
        /// 上膛音效延时时间
        /// </summary>
        [JsonInclude]
        public float EquipSoundDelayTime;

        /// <summary>
        /// 其他音效
        /// </summary>
        public Dictionary<string, Sound> OtherSoundMap;

        /// <summary>
        /// Ai属性 <br/>
        /// Ai 使用该武器时的武器数据, 设置该字段, 可让同一把武器在敌人和玩家手上有不同属性 <br/>
        /// 如果不填则Ai和玩家使用同一种属性
        /// </summary>
        public Weapon AiUseAttribute;

        /// <summary>
        /// Ai属性 <br/>
        /// 目标锁定时间, 也就是瞄准目标多久才会开火, (单位: 秒)
        /// </summary>
        [JsonInclude]
        public float AiTargetLockingTime;

        /// <summary>
        /// Ai属性 <br/>
        /// Ai使用该武器发射的子弹速度缩放比
        /// </summary>
        [JsonInclude]
        public float AiBulletSpeedScale;

        /// <summary>
        /// Ai属性 <br/>
        /// Ai使用该武器消耗弹药的概率, (0 - 1)
        /// </summary>
        [JsonInclude]
        public float AiAmmoConsumptionProbability;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public Weapon Clone()
        {
            var inst = new Weapon();
            inst.Id = Id;
            inst.WeaponId = WeaponId;
            inst.Name = Name;
            inst.Icon = Icon;
            inst.Weight = Weight;
            inst.WeightType = WeightType;
            inst.ContinuousShoot = ContinuousShoot;
            inst.AmmoCapacity = AmmoCapacity;
            inst.MaxAmmoCapacity = MaxAmmoCapacity;
            inst.StandbyAmmoCapacity = StandbyAmmoCapacity;
            inst.ReloadTime = ReloadTime;
            inst.AloneReload = AloneReload;
            inst.AloneReloadCount = AloneReloadCount;
            inst.AloneReloadBeginIntervalTime = AloneReloadBeginIntervalTime;
            inst.AloneReloadFinishIntervalTime = AloneReloadFinishIntervalTime;
            inst.AloneReloadCanShoot = AloneReloadCanShoot;
            inst.LooseShoot = LooseShoot;
            inst.MinChargeTime = MinChargeTime;
            inst.MinContinuousCount = MinContinuousCount;
            inst.MaxContinuousCount = MaxContinuousCount;
            inst.TriggerInterval = TriggerInterval;
            inst.StartFiringSpeed = StartFiringSpeed;
            inst.FinalFiringSpeed = FinalFiringSpeed;
            inst.FiringSpeedAddSpeed = FiringSpeedAddSpeed;
            inst.FiringSpeedBackSpeed = FiringSpeedBackSpeed;
            inst.MinFireBulletCount = MinFireBulletCount;
            inst.MaxFireBulletCount = MaxFireBulletCount;
            inst.DelayedTime = DelayedTime;
            inst.StartScatteringRange = StartScatteringRange;
            inst.FinalScatteringRange = FinalScatteringRange;
            inst.ScatteringRangeAddValue = ScatteringRangeAddValue;
            inst.ScatteringRangeBackSpeed = ScatteringRangeBackSpeed;
            inst.ScatteringRangeBackDelayTime = ScatteringRangeBackDelayTime;
            inst.MinBacklash = MinBacklash;
            inst.MaxBacklash = MaxBacklash;
            inst.BacklashRegressionSpeed = BacklashRegressionSpeed;
            inst.UpliftAngle = UpliftAngle;
            inst.DefaultAngle = DefaultAngle;
            inst.UpliftAngleRestore = UpliftAngleRestore;
            inst.BulletId = BulletId;
            inst.BulletMinDeviationAngle = BulletMinDeviationAngle;
            inst.BulletMaxDeviationAngle = BulletMaxDeviationAngle;
            inst.BulletMaxSpeed = BulletMaxSpeed;
            inst.BulletMinSpeed = BulletMinSpeed;
            inst.BulletMinDistance = BulletMinDistance;
            inst.BulletMaxDistance = BulletMaxDistance;
            inst.ThrowCollisionSize = ThrowCollisionSize;
            inst.ShootSound = ShootSound;
            inst.BeginReloadSound = BeginReloadSound;
            inst.BeginReloadSoundDelayTime = BeginReloadSoundDelayTime;
            inst.ReloadSound = ReloadSound;
            inst.ReloadSoundDelayTime = ReloadSoundDelayTime;
            inst.ReloadFinishSound = ReloadFinishSound;
            inst.ReloadFinishSoundAdvanceTime = ReloadFinishSoundAdvanceTime;
            inst.EquipSound = EquipSound;
            inst.EquipSoundDelayTime = EquipSoundDelayTime;
            inst.OtherSoundMap = OtherSoundMap;
            inst.AiUseAttribute = AiUseAttribute;
            inst.AiTargetLockingTime = AiTargetLockingTime;
            inst.AiBulletSpeedScale = AiBulletSpeedScale;
            inst.AiAmmoConsumptionProbability = AiAmmoConsumptionProbability;
            return inst;
        }
    }
    private class Ref_Weapon : Weapon
    {
        [JsonInclude]
        public string __ShootSound;

        [JsonInclude]
        public string __BeginReloadSound;

        [JsonInclude]
        public string __ReloadSound;

        [JsonInclude]
        public string __ReloadFinishSound;

        [JsonInclude]
        public string __EquipSound;

        [JsonInclude]
        public Dictionary<string, string> __OtherSoundMap;

        [JsonInclude]
        public string __AiUseAttribute;

    }
}