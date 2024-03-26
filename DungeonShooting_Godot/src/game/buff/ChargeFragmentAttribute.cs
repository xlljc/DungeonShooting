using System;

/// <summary>
/// 充能片段
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ChargeFragmentAttribute : FragmentAttribute
{
    public ChargeFragmentAttribute(string name, string description) : base(name, description)
    {
    }
}