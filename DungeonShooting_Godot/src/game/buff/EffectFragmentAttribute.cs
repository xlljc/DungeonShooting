using System;

/// <summary>
/// 主动效果片段
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class EffectFragmentAttribute : FragmentAttribute
{
    public EffectFragmentAttribute(string name, string description) : base(name, description)
    {
    }
}