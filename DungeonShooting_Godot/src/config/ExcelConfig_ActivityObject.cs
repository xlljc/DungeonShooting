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
        public string Id;

        /// <summary>
        /// Test(测试对象): 2 <br/>
        /// Role(角色): 3 <br/>
        /// Enemy(敌人): 4 <br/>
        /// Weapon(武器): 5 <br/>
        /// Bullet(子弹): 6 <br/>
        /// Shell(弹壳): 7 <br/>
        /// Effect(特效): 8 <br/>
        /// Prop(道具): 9 <br/>
        /// Other(其它类型): 99
        /// </summary>
        [JsonInclude]
        public int Type;

        /// <summary>
        /// 物体预制场景路径, 场景根节点必须是ActivityObject子类
        /// </summary>
        [JsonInclude]
        public string Prefab;

        /// <summary>
        /// 物体名称
        /// </summary>
        [JsonInclude]
        public string ItemName;

        /// <summary>
        /// 物体描述
        /// </summary>
        [JsonInclude]
        public string ItemDescription;

        /// <summary>
        /// 物体备注
        /// </summary>
        [JsonInclude]
        public string Remark;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public ActivityObject Clone()
        {
            var inst = new ActivityObject();
            inst.Id = Id;
            inst.Type = Type;
            inst.Prefab = Prefab;
            inst.ItemName = ItemName;
            inst.ItemDescription = ItemDescription;
            inst.Remark = Remark;
            return inst;
        }
    }
}