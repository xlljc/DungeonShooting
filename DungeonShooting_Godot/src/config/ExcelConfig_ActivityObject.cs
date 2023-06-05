using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class ActivityObject
    {
        /// <summary>
        /// 物体唯一id <br/>
        /// 需要添加类型前缀
        /// </summary>
        [JsonInclude]
        public string Id { get; private set; }

        /// <summary>
        /// Test(测试对象): 2 <br/>
        /// Role(角色): 3 <br/>
        /// Enemy(敌人): 4 <br/>
        /// Weapon(武器): 5 <br/>
        /// Bullet(子弹): 6 <br/>
        /// Shell(弹壳): 7 <br/>
        /// Effect(特效): 8 <br/>
        /// Other(其它类型): 9
        /// </summary>
        [JsonInclude]
        public int Type { get; private set; }

        /// <summary>
        /// 物体预制场景路径, 场景根节点必须是ActivityObject子类
        /// </summary>
        [JsonInclude]
        public string Prefab { get; private set; }

    }
}