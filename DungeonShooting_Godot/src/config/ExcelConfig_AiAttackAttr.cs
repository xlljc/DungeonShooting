using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class AiAttackAttr
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 备注
        /// </summary>
        [JsonInclude]
        public string Remark;

        /// <summary>
        /// 开火时是否站立不动
        /// </summary>
        [JsonInclude]
        public bool FiringStand;

        /// <summary>
        /// 是否显示射击辅助线
        /// </summary>
        [JsonInclude]
        public bool ShowSubline;

        /// <summary>
        /// Ai属性 <br/>
        /// 目标锁定时间, 也就是瞄准目标多久才会开火, (单位: 秒)
        /// </summary>
        [JsonInclude]
        public float LockingTime;

        /// <summary>
        /// Ai属性 <br/>
        /// Ai使用该武器发射的子弹速度缩放比
        /// </summary>
        [JsonInclude]
        public float BulletSpeedScale;

        /// <summary>
        /// Ai属性 <br/>
        /// Ai使用该武器消耗弹药的概率, (0 - 1)
        /// </summary>
        [JsonInclude]
        public float AmmoConsumptionProbability;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public AiAttackAttr Clone()
        {
            var inst = new AiAttackAttr();
            inst.Id = Id;
            inst.Remark = Remark;
            inst.FiringStand = FiringStand;
            inst.ShowSubline = ShowSubline;
            inst.LockingTime = LockingTime;
            inst.BulletSpeedScale = BulletSpeedScale;
            inst.AmmoConsumptionProbability = AmmoConsumptionProbability;
            return inst;
        }
    }
}