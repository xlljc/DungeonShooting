
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

/// <summary>
/// 描述导航多边形数据
/// </summary>
public class NavigationPolygonData
{
    /// <summary>
    /// 导航轮廓类型
    /// </summary>
    public NavigationPolygonType Type;
    /// <summary>
    /// 多边形的顶点
    /// </summary>
    public List<Vector2> Points = new List<Vector2>();
}