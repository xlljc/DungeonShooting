using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class AmmoBase
    {
        /// <summary>
        /// 武器属性id
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public AmmoBase Clone()
        {
            var inst = new AmmoBase();
            inst.Id = Id;
            return inst;
        }
    }
}