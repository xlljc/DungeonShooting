
using System;

[AttributeUsage(AttributeTargets.Class)]
public class ConditionFragmentAttribute : Attribute
{
    /// <summary>
    /// 条件名称
    /// </summary>
    public string ConditionName { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
    
    public ConditionFragmentAttribute(string conditionName, string description)
    {
        ConditionName = conditionName;
        Description = description;
    }
}