using System;

[AttributeUsage(AttributeTargets.Class)]
public class EffectFragmentAttribute : Attribute
{
    /// <summary>
    /// 效果名称
    /// </summary>
    public string EffectName { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
    
    public EffectFragmentAttribute(string effectName, string description)
    {
        EffectName = effectName;
        Description = description;
    }
}