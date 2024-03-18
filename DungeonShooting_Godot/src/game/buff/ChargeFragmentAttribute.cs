using System;

[AttributeUsage(AttributeTargets.Class)]
public class ChargeFragmentAttribute : Attribute
{
    /// <summary>
    /// 充能属性名称
    /// </summary>
    public string ChargeName { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
    
    public ChargeFragmentAttribute(string chargeName, string description)
    {
        ChargeName = chargeName;
        Description = description;
    }
}