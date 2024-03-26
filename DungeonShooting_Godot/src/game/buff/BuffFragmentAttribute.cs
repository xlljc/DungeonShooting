using System;

/// <summary>
/// buff片段
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class BuffFragmentAttribute : FragmentAttribute
{
    public BuffFragmentAttribute(string name, string description) : base(name, description)
    {
    }
}