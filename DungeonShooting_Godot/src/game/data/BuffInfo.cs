
using System;
using System.Collections.Generic;

/// <summary>
/// buff 属性数据
/// </summary>
public class BuffInfo
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
    /// buff 可传参数
    /// </summary>
    public List<int> Params;
    
    /// <summary>
    /// buff 类
    /// </summary>
    public Type Type;
    
    public BuffInfo(string name, string description, Type type)
    {
        Name = name;
        Description = description;
        Type = type;
        Params = new List<int>();
    }
    
    public BuffInfo(string name, string description, List<int> paramsList, Type type)
    {
        Name = name;
        Description = description;
        Params = paramsList;
        Type = type;
    }
}