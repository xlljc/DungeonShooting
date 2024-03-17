using System;

[AttributeUsage(AttributeTargets.Class)]
public class BuffAttribute : Attribute
{
    /// <summary>
    /// Buff属性名称
    /// </summary>
    public string BuffName { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
    
    public BuffAttribute(string buffName, string description)
    {
        BuffName = buffName;
        Description = description;
    }
}