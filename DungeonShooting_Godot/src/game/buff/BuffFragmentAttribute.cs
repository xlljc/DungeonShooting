using System;

[AttributeUsage(AttributeTargets.Class)]
public class BuffFragmentAttribute : Attribute
{
    /// <summary>
    /// Buff属性名称
    /// </summary>
    public string BuffName { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
    
    public BuffFragmentAttribute(string buffName, string description)
    {
        BuffName = buffName;
        Description = description;
    }
}