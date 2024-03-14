using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class ActivityBase
    {
        /// <summary>
        /// 物体唯一id <br/>
        /// 需要添加类型前缀
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 物体名称
        /// </summary>
        [JsonInclude]
        public string Name;

        /// <summary>
        /// Test(测试对象): 2 <br/>
        /// Role(角色): 3 <br/>
        /// Enemy(敌人): 4 <br/>
        /// Weapon(武器): 5 <br/>
        /// Bullet(子弹): 6 <br/>
        /// Shell(弹壳): 7 <br/>
        /// Effect(特效): 8 <br/>
        /// Prop(道具): 9 <br/>
        /// Treasure(宝箱): 10 <br/>
        /// Other(其它类型): 99
        /// </summary>
        [JsonInclude]
        public ActivityType Type;

        /// <summary>
        /// 物体品质, 用于武器和道具 <br/>
        /// 通用物品: 1 <br/>
        /// 基础: 2 <br/>
        /// 普通: 3 <br/>
        /// 稀有: 4 <br/>
        /// 史诗: 5 <br/>
        /// 传说: 6 <br/>
        /// 独一无二: 7
        /// </summary>
        [JsonInclude]
        public ActivityQuality Quality;

        /// <summary>
        /// 商店售价
        /// </summary>
        [JsonInclude]
        public uint Price;

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
        /// 是否是静态物体
        /// </summary>
        [JsonInclude]
        public bool IsStatic;

        /// <summary>
        /// 物体使用交互材质 <br/>
        /// 如果不填，则默认使用id为0001的材质
        /// </summary>
        public ActivityMaterial Material;

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
        public ActivityBase Clone()
        {
            var inst = new ActivityBase();
            inst.Id = Id;
            inst.Name = Name;
            inst.Type = Type;
            inst.Quality = Quality;
            inst.Price = Price;
            inst.Intro = Intro;
            inst.Details = Details;
            inst.IsStatic = IsStatic;
            inst.Material = Material;
            inst.Prefab = Prefab;
            inst.Icon = Icon;
            inst.ShowInMapEditor = ShowInMapEditor;
            return inst;
        }
    }
    private class Ref_ActivityBase : ActivityBase
    {
        [JsonInclude]
        public string __Material;

    }
}