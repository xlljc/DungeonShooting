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
        /// 激光子弹：2
        /// </summary>
        [JsonInclude]
        public int Type;

        /// <summary>
        /// 绑定子弹预制体，根据Type填不同的参数 <br/>
        /// Type为1，填ActivityBase表Id <br/>
        /// Type为2，填场景路径
        /// </summary>
        [JsonInclude]
        public string Prefab;

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
            return inst;
        }
    }
}