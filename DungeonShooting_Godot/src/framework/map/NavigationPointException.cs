
using System;
using Godot;

/// <summary>
/// 导航点交错异常
/// </summary>
public class NavigationPointException : Exception
{
    /// <summary>
    /// 交错点
    /// </summary>
    public Vector2I Point { get; }
    
    public NavigationPointException(Vector2I point, string message): base(message)
    {
        Point = point;
    }
}