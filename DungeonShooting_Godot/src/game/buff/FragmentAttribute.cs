using System;

/// <summary>
/// 属性逻辑基类
/// </summary>
public abstract class FragmentAttribute : Attribute
{
    /// <summary>
    /// 属性名称
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 参数1 描述
    /// </summary>
    public string Arg1 { get; set; }

    /// <summary>
    /// 参数2 描述
    /// </summary>
    public string Arg2 { get; set; }

    /// <summary>
    /// 参数3 描述
    /// </summary>
    public string Arg3 { get; set; }

    /// <summary>
    /// 参数4 描述
    /// </summary>
    public string Arg4 { get; set; }

    /// <summary>
    /// 参数5 描述
    /// </summary>
    public string Arg5 { get; set; }

    /// <summary>
    /// 参数6 描述
    /// </summary>
    public string Arg6 { get; set; }

    /// <summary>
    /// 参数7 描述
    /// </summary>
    public string Arg7 { get; set; }

    /// <summary>
    /// 参数8 描述
    /// </summary>
    public string Arg8 { get; set; }

    public FragmentAttribute(string name, string description)
    {
        Name = name;
        Description = description;
    }
}