using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;

/// <summary>
/// buff 属性注册类, 调用 Init() 函数初始化数据
/// </summary>
public class PropFragmentRegister
{
    /// <summary>
    /// 所有 buff 属性信息
    /// </summary>
    public static Dictionary<string, PropFragmentInfo> BuffFragmentInfos { get; private set; }
    
    /// <summary>
    /// 所有主动道具使用条件数据
    /// </summary>
    public static Dictionary<string, PropFragmentInfo> ConditionFragmentInfos { get; private set; }
    
    /// <summary>
    /// 所有主动道具效果数据
    /// </summary>
    public static Dictionary<string, PropFragmentInfo> EffectFragmentInfos { get; private set; }
    
    /// <summary>
    /// 所有主动道具充能条件数据
    /// </summary>
    public static Dictionary<string, PropFragmentInfo> ChargeFragmentInfos { get; private set; }

    /// <summary>
    /// 初始化 buff
    /// </summary>
    public static void Init()
    {
        BuffFragmentInfos = new Dictionary<string, PropFragmentInfo>();
        ConditionFragmentInfos = new Dictionary<string, PropFragmentInfo>();
        EffectFragmentInfos = new Dictionary<string, PropFragmentInfo>();
        ChargeFragmentInfos = new Dictionary<string, PropFragmentInfo>();

        var types = typeof(PropFragmentRegister).Assembly.GetTypes();
        //包含[BuffAttribute]特性
        var buffs = types.Where(type =>
            type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(BuffFragment)));
        foreach (var type in buffs)
        {
            var attribute = (BuffFragmentAttribute)type.GetCustomAttribute(typeof(BuffFragmentAttribute), false);
            if (attribute != null)
            {
                if (BuffFragmentInfos.ContainsKey(attribute.BuffName))
                {
                    GD.PrintErr($"Buff '{attribute.BuffName}' 重名!");
                    continue;
                }

                var buffInfo = new PropFragmentInfo(attribute.BuffName, attribute.Description, type);
                BuffFragmentInfos.Add(attribute.BuffName, buffInfo);
            }
        }
        //包含[ConditionAttribute]特性
        var conditions = types.Where(type =>
            type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(ConditionFragment)));
        foreach (var type in conditions)
        {
            var attribute = (ConditionFragmentAttribute)type.GetCustomAttribute(typeof(ConditionFragmentAttribute), false);
            if (attribute != null)
            {
                if (ConditionFragmentInfos.ContainsKey(attribute.ConditionName))
                {
                    GD.PrintErr($"Condition '{attribute.ConditionName}' 重名!");
                    continue;
                }

                var conditionInfo = new PropFragmentInfo(attribute.ConditionName, attribute.Description, type);
                ConditionFragmentInfos.Add(attribute.ConditionName, conditionInfo);
            }
        }
        //包含[EffectAttribute]特性
        var effects = types.Where(type =>
            type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(EffectFragment)));
        foreach (var type in effects)
        {
            var attribute = (EffectFragmentAttribute)type.GetCustomAttribute(typeof(EffectFragmentAttribute), false);
            if (attribute != null)
            {
                if (EffectFragmentInfos.ContainsKey(attribute.EffectName))
                {
                    GD.PrintErr($"Effect '{attribute.EffectName}' 重名!");
                    continue;
                }

                var effectInfo = new PropFragmentInfo(attribute.EffectName, attribute.Description, type);
                EffectFragmentInfos.Add(attribute.EffectName, effectInfo);
            }
        }
        //包含[ChargeAttribute]特性
        var charges = types.Where(type =>
            type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(ChargeFragment)));
        foreach (var type in charges)
        {
            var attribute = (ChargeFragmentAttribute)type.GetCustomAttribute(typeof(ChargeFragmentAttribute), false);
            if (attribute != null)
            {
                if (ChargeFragmentInfos.ContainsKey(attribute.ChargeName))
                {
                    GD.PrintErr($"Charge '{attribute.ChargeName}' 重名!");
                    continue;
                }

                var chargeInfo = new PropFragmentInfo(attribute.ChargeName, attribute.Description, type);
                ChargeFragmentInfos.Add(attribute.ChargeName, chargeInfo);
            }
        }
    }
}