
using System;

/// <summary>
/// 道具逻辑片段数据
/// </summary>
public class PropFragmentInfo
{
    /// <summary>
    /// buff 名称
    /// </summary>
    public string Name;
    
    /// <summary>
    /// buff 描述
    /// </summary>
    public string Description;
    
    /// <summary>
    /// buff 类
    /// </summary>
    public Type Type;
    
    public PropFragmentInfo(string name, string description, Type type)
    {
        Name = name;
        Description = description;
        Type = type;
    }
}