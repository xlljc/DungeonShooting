
using System;
using Godot;

/// <summary>
/// 导航点交错异常
/// </summary>
public class NavigationPointInterleavingException : Exception
{
    /// <summary>
    /// 交错点
    /// </summary>
    public Vector2 Point { get; }
    
    public NavigationPointInterleavingException(Vector2 point, string message): base(message)
    {
        Point = point;
    }
}