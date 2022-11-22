
using System.Collections.Generic;
using Godot;

public enum NavigationPolygonType
{
    /// <summary>
    /// 外轮廓
    /// </summary>
    Out,
    /// <summary>
    /// 内轮廓
    /// </summary>
    In,
}

public class NavigationPolygonData
{
    public NavigationPolygonType Type;
    public List<Vector2> Points = new List<Vector2>();
}