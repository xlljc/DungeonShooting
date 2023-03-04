
using System.Collections.Generic;
using System.Text.Json.Serialization;
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
    [JsonInclude] public NavigationPolygonType Type;

    /// <summary>
    /// 多边形的顶点
    /// </summary>
    [JsonInclude] public List<SerializeVector2> Points = new List<SerializeVector2>();

    public NavigationPolygonData()
    {
    }

    public NavigationPolygonData(NavigationPolygonType type, List<SerializeVector2> points)
    {
        Type = type;
        Points = points;
    }
    
    /// <summary>
    /// 将 Points 字段转为 Vector2[] 类型数据并返回
    /// </summary>
    public Vector2[] ConvertPointsToVector2Array()
    {
        if (Points == null)
        {
            return null;
        }

        var array = new Vector2[Points.Count];
        for (var i = 0; i < Points.Count; i++)
        {
            array[i] = Points[i].AsVector2();
        }

        return array;
    }

    /// <summary>
    /// 将 Points 字段转为 Vector2I[] 类型数据并返回
    /// </summary>
    public Vector2I[] ConvertPointsToVector2IArray()
    {
        if (Points == null)
        {
            return null;
        }

        var array = new Vector2I[Points.Count];
        for (var i = 0; i < Points.Count; i++)
        {
            array[i] = Points[i].AsVector2I();
        }

        return array;
    }

}