using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class ActivePropBase
    {
        /// <summary>
        /// Buff Id
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 备注
        /// </summary>
        [JsonInclude]
        public string Remark;

        /// <summary>
        /// 属性绑定Buff实体的Id，这个id时ActivityBase表Id
        /// </summary>
        public ActivityBase Activity;

        /// <summary>
        /// 被动Buff效果 <br/>
        /// 也就是当前buff道具所有挂载的被动属性集合, 具体属性名称请参阅buff属性表 <br/>
        /// key为buff属性名称 <br/>
        /// value为buff初始化参数
        /// </summary>
        [JsonInclude]
        public Dictionary<string, System.Text.Json.JsonElement[]> Buff;

        /// <summary>
        /// 道具使用条件 <br/>
        /// 参数配置方式与buff字段相同 <br/>
        /// 性名称请参阅condition属性表
        /// </summary>
        [JsonInclude]
        public Dictionary<string, System.Text.Json.JsonElement[]> Condition;

        /// <summary>
        /// 道具使用效果 <br/>
        /// 参数配置方式与buff字段相同 <br/>
        /// 性名称请参阅effect属性表
        /// </summary>
        [JsonInclude]
        public Dictionary<string, System.Text.Json.JsonElement[]> Effect;

        /// <summary>
        /// 道具充能效果 <br/>
        /// 参数配置方式与buff字段相同 <br/>
        /// 性名称请参阅charge属性表 <br/>
        /// 注意: 仅当'IsConsumables'为false是生效
        /// </summary>
        [JsonInclude]
        public Dictionary<string, System.Text.Json.JsonElement[]> Charge;

        /// <summary>
        /// 使用道具的效果持续时间 <br/>
        /// 单位: 秒
        /// </summary>
        [JsonInclude]
        public float Duration;

        /// <summary>
        /// 使用一次后的冷却时间 <br/>
        /// 单位: 秒
        /// </summary>
        [JsonInclude]
        public float CooldownTime;

        /// <summary>
        /// 是否是消耗品, 如果为true, 则每次使用都会消耗叠加数量
        /// </summary>
        [JsonInclude]
        public bool IsConsumables;

        /// <summary>
        /// 最大叠加次数, 仅当'IsConsumables'为true时有效
        /// </summary>
        [JsonInclude]
        public uint MaxCount;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public ActivePropBase Clone()
        {
            var inst = new ActivePropBase();
            inst.Id = Id;
            inst.Remark = Remark;
            inst.Activity = Activity;
            inst.Buff = Buff;
            inst.Condition = Condition;
            inst.Effect = Effect;
            inst.Charge = Charge;
            inst.Duration = Duration;
            inst.CooldownTime = CooldownTime;
            inst.IsConsumables = IsConsumables;
            inst.MaxCount = MaxCount;
            return inst;
        }
    }
    private class Ref_ActivePropBase : ActivePropBase
    {
        [JsonInclude]
        public string __Activity;

    }
}