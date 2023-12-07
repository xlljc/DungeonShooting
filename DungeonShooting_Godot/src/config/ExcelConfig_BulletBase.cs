using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class BulletBase
    {
        /// <summary>
        /// 子弹id
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 子弹名称
        /// </summary>
        [JsonInclude]
        public string Name;

        /// <summary>
        /// 子弹类型： <br/>
        /// 实体子弹：1 <br/>
        /// 激光子弹：2 <br/>
        /// 纯伤害：3
        /// </summary>
        [JsonInclude]
        public int Type;

        /// <summary>
        /// 绑定子弹预制体，根据Type填不同的参数 <br/>
        /// Type为1，填ActivityBase表Id <br/>
        /// Type为2，填场景路径 <br/>
        /// Type为3，不填
        /// </summary>
        [JsonInclude]
        public string Prefab;

        /// <summary>
        /// 造成的伤害区间 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public int[] HarmRange;

        /// <summary>
        /// 造成伤害后击退值区间 <br/>
        /// 如果发射子弹,则按每发子弹算击退 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public float[] RepelRange;

        /// <summary>
        /// 子弹偏移角度区间 <br/>
        /// 用于设置子弹偏移朝向, 该属性和射半径效果类似, 但与其不同的是, 散射半径是用来控制枪口朝向的, 而该属性是控制子弹朝向的, 可用于制作霰弹枪子弹效果 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public float[] DeviationAngleRange;

        /// <summary>
        /// 子弹初速度区间 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public float[] SpeedRange;

        /// <summary>
        /// 子弹最大存在时间，单位：秒 <br/>
        /// 如果值小于等于0时子弹无限期存在 <br/>
        /// 只有Type为1时才需要填写 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public float[] LifeTimeRange;

        /// <summary>
        /// 子弹最大飞行距离区间 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public float[] DistanceRange;

        /// <summary>
        /// 初始纵轴速度区间 <br/>
        /// 只有Type为1时有效 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public float[] VerticalSpeed;

        /// <summary>
        /// 反弹次数区间 <br/>
        /// 只有Type为1或2时有效 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public int[] BounceCount;

        /// <summary>
        /// 子弹穿透次数区间 <br/>
        /// 只有Type为1时有效 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public int[] Penetration;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public BulletBase Clone()
        {
            var inst = new BulletBase();
            inst.Id = Id;
            inst.Name = Name;
            inst.Type = Type;
            inst.Prefab = Prefab;
            inst.HarmRange = HarmRange;
            inst.RepelRange = RepelRange;
            inst.DeviationAngleRange = DeviationAngleRange;
            inst.SpeedRange = SpeedRange;
            inst.LifeTimeRange = LifeTimeRange;
            inst.DistanceRange = DistanceRange;
            inst.VerticalSpeed = VerticalSpeed;
            inst.BounceCount = BounceCount;
            inst.Penetration = Penetration;
            return inst;
        }
    }
}