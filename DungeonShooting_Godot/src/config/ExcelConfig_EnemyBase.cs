using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class EnemyBase
    {
        /// <summary>
        /// 表Id
        /// </summary>
        [JsonInclude]
        public string Id;

        /// <summary>
        /// 绑定的ActivityBase表数据
        /// </summary>
        public ActivityBase Activity;

        /// <summary>
        /// 备注
        /// </summary>
        [JsonInclude]
        public string Remark;

        /// <summary>
        /// 血量
        /// </summary>
        [JsonInclude]
        public int Hp;

        /// <summary>
        /// 移动速度
        /// </summary>
        [JsonInclude]
        public float MoveSpeed;

        /// <summary>
        /// 移动加速度
        /// </summary>
        [JsonInclude]
        public float Acceleration;

        /// <summary>
        /// 移动摩擦力, 仅用于人物基础移动
        /// </summary>
        [JsonInclude]
        public float Friction;

        /// <summary>
        /// 是否可以拾起武器
        /// </summary>
        [JsonInclude]
        public bool CanPickUpWeapon;

        /// <summary>
        /// 视野半径, 单位像素, 发现玩家后改视野范围可以穿墙
        /// </summary>
        [JsonInclude]
        public float ViewRange;

        /// <summary>
        /// 发现玩家后跟随玩家的视野半径
        /// </summary>
        [JsonInclude]
        public float TailAfterViewRange;

        /// <summary>
        /// 背后的视野半径, 单位像素
        /// </summary>
        [JsonInclude]
        public float BackViewRange;

        /// <summary>
        /// 掉落金币数量区间, 如果为负数或者0则不会掉落金币 <br/>
        /// 格式为[value]或者[min,max]
        /// </summary>
        [JsonInclude]
        public int[] Gold;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public EnemyBase Clone()
        {
            var inst = new EnemyBase();
            inst.Id = Id;
            inst.Activity = Activity;
            inst.Remark = Remark;
            inst.Hp = Hp;
            inst.MoveSpeed = MoveSpeed;
            inst.Acceleration = Acceleration;
            inst.Friction = Friction;
            inst.CanPickUpWeapon = CanPickUpWeapon;
            inst.ViewRange = ViewRange;
            inst.TailAfterViewRange = TailAfterViewRange;
            inst.BackViewRange = BackViewRange;
            inst.Gold = Gold;
            return inst;
        }
    }
    private class Ref_EnemyBase : EnemyBase
    {
        [JsonInclude]
        public string __Activity;

    }
}