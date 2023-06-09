using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class Test
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 引用的武器数据
        /// </summary>
        public Weapon Weapon;
        [JsonInclude]
        public string _Weapon;

        /// <summary>
        /// 引用的武器数据(数组)
        /// </summary>
        public Weapon[] Weapons;
        [JsonInclude]
        public string[] _Weapons;

        /// <summary>
        /// 引用的武器数据(字典)
        /// </summary>
        public Dictionary<string, Weapon> WeaponMap;
        [JsonInclude]
        public Dictionary<string, string> _WeaponMap;

        /// <summary>
        /// 引用ActivityObject
        /// </summary>
        public ActivityObject ActivityObject;
        [JsonInclude]
        public string _ActivityObject;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public Test Clone()
        {
            var inst = new Test();
            inst.Id = Id;
            inst.Weapon = Weapon;
            inst.Weapons = Weapons;
            inst.WeaponMap = WeaponMap;
            inst.ActivityObject = ActivityObject;
            return inst;
        }
    }
}