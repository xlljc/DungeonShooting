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
        /// 物体名称
        /// </summary>
        [JsonInclude]
        public string Name;

        /// <summary>
        /// 物体简介 <br/>
        /// 一句对物体简短的介绍, 比如拾起物体时弹出的描述
        /// </summary>
        [JsonInclude]
        public string Intro;

        /// <summary>
        /// 物体详情 <br/>
        /// 在图鉴中的描述
        /// </summary>
        [JsonInclude]
        public string Details;

        /// <summary>
        /// 物体预制场景路径, 场景根节点必须是ActivityObject子类
        /// </summary>
        [JsonInclude]
        public string Prefab;

        /// <summary>
        /// 物体图标 <br/>
        /// 如果不需要在图鉴或者地图编辑器中显示该物体, 则可以不用设置
        /// </summary>
        [JsonInclude]
        public string Icon;

        /// <summary>
        /// 是否在地图编辑器中显示该物体
        /// </summary>
        [JsonInclude]
        public bool ShowInMapEditor;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public ActivityObject Clone()
        {
            var inst = new ActivityObject();
            inst.Id = Id;
            inst.Type = Type;
            inst.Name = Name;
            inst.Intro = Intro;
            inst.Details = Details;
            inst.Prefab = Prefab;
            inst.Icon = Icon;
            inst.ShowInMapEditor = ShowInMapEditor;
            return inst;
        }
    }
}