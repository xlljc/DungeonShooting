using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class LiquidMaterial
    {
        /// <summary>
        /// 表Id
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 备注
        /// </summary>
        [JsonInclude]
        public string Remark;

        /// <summary>
        /// 笔刷贴图
        /// </summary>
        [JsonInclude]
        public string BurshTexture;

        /// <summary>
        /// 补帧间距倍率(0-1)
        /// </summary>
        [JsonInclude]
        public float Ffm;

        /// <summary>
        /// 开始消退时间,单位秒 <br/>
        /// 小于0则永远不会消退
        /// </summary>
        [JsonInclude]
        public float Duration;

        /// <summary>
        /// 消退速度, 也就是 Alpha 值每秒变化的速度
        /// </summary>
        [JsonInclude]
        public float WriteOffSpeed;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public LiquidMaterial Clone()
        {
            var inst = new LiquidMaterial();
            inst.Id = Id;
            inst.Remark = Remark;
            inst.BurshTexture = BurshTexture;
            inst.Ffm = Ffm;
            inst.Duration = Duration;
            inst.WriteOffSpeed = WriteOffSpeed;
            return inst;
        }
    }
}