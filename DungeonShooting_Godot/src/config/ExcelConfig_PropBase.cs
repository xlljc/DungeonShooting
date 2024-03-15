using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class PropBase
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
        /// 是否是主动道具, 默认false
        /// </summary>
        [JsonInclude]
        public bool IsActivity;

        /// <summary>
        /// 被动Buff效果 <br/>
        /// 也就是当前buff道具所有挂载的被动属性集合, 具体属性名称请参阅buff属性表 <br/>
        /// key为buff属性名称 <br/>
        /// value为buff初始化参数
        /// </summary>
        [JsonInclude]
        public Dictionary<string, float[]> Buff;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public PropBase Clone()
        {
            var inst = new PropBase();
            inst.Id = Id;
            inst.Remark = Remark;
            inst.Activity = Activity;
            inst.IsActivity = IsActivity;
            inst.Buff = Buff;
            return inst;
        }
    }
    private class Ref_PropBase : PropBase
    {
        [JsonInclude]
        public string __Activity;

    }
}