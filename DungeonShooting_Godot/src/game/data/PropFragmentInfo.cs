
using System;
using System.Collections.Generic;

/// <summary>
/// 道具逻辑片段数据
/// </summary>
public class PropFragmentInfo
{
    public class PropFragmentArgInfo
    {
        /// <summary>
        /// 参数索引
        /// </summary>
        public int ArgIndex;
        
        /// <summary>
        /// 参数描述
        /// </summary>
        public string Description;
        
        public PropFragmentArgInfo(int argIndex, string description)
        {
            ArgIndex = argIndex;
            Description = description;
        }
    }
    
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
    
    /// <summary>
    /// buff 参数信息
    /// </summary>
    public List<PropFragmentArgInfo> ArgInfos = new List<PropFragmentArgInfo>();
    
    public PropFragmentInfo(FragmentAttribute attribute, Type type)
    {
        Name = attribute.Name;
        Description = attribute.Description;
        Type = type;

        if (attribute.Arg1 != null)
        {
            ArgInfos.Add(new PropFragmentArgInfo(1, attribute.Arg1));
        }
        if (attribute.Arg2 != null)
        {
            ArgInfos.Add(new PropFragmentArgInfo(2, attribute.Arg2));
        }
        if (attribute.Arg3 != null)
        {
            ArgInfos.Add(new PropFragmentArgInfo(3, attribute.Arg3));
        }
        if (attribute.Arg4 != null)
        {
            ArgInfos.Add(new PropFragmentArgInfo(4, attribute.Arg4));
        }
        if (attribute.Arg5 != null)
        {
            ArgInfos.Add(new PropFragmentArgInfo(5, attribute.Arg5));
        }
        if (attribute.Arg6 != null)
        {
            ArgInfos.Add(new PropFragmentArgInfo(6, attribute.Arg6));
        }
        if (attribute.Arg7 != null)
        {
            ArgInfos.Add(new PropFragmentArgInfo(7, attribute.Arg7));
        }
        if (attribute.Arg8 != null)
        {
            ArgInfos.Add(new PropFragmentArgInfo(8, attribute.Arg8));
        }
    }
}