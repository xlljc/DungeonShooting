using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Config;

public static partial class ExcelConfig
{
    public class ActivityMaterial
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
        /// 摩擦力
        /// </summary>
        [JsonInclude]
        public float Friction;

        /// <summary>
        /// 旋转摩擦力
        /// </summary>
        [JsonInclude]
        public float RotationFriction;

        /// <summary>
        /// 落地之后是否回弹
        /// </summary>
        [JsonInclude]
        public bool Bounce;

        /// <summary>
        /// 物体水平回弹强度
        /// </summary>
        [JsonInclude]
        public float BounceStrength;

        /// <summary>
        /// 物体下坠回弹的强度
        /// </summary>
        [JsonInclude]
        public float FallBounceStrength;

        /// <summary>
        /// 物体下坠回弹后的运动速度衰比例
        /// </summary>
        [JsonInclude]
        public float FallBounceSpeed;

        /// <summary>
        /// 物体下坠回弹后的旋转速度衰减比例
        /// </summary>
        [JsonInclude]
        public float FallBounceRotation;

        /// <summary>
        /// 返回浅拷贝出的新对象
        /// </summary>
        public ActivityMaterial Clone()
        {
            var inst = new ActivityMaterial();
            inst.Id = Id;
            inst.Remark = Remark;
            inst.Friction = Friction;
            inst.RotationFriction = RotationFriction;
            inst.Bounce = Bounce;
            inst.BounceStrength = BounceStrength;
            inst.FallBounceStrength = FallBounceStrength;
            inst.FallBounceSpeed = FallBounceSpeed;
            inst.FallBounceRotation = FallBounceRotation;
            return inst;
        }
    }
}