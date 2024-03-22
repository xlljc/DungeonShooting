
using System;

/// <summary>
/// 条件片段
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ConditionFragmentAttribute : FragmentAttribute
{
    public ConditionFragmentAttribute(string name, string description) : base(name, description)
    {
    }
}