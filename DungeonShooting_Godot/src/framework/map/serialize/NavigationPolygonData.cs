
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
    [JsonInclude]
    public NavigationPolygonType Type;

    /// <summary>
    /// 多边形的顶点, 两个为一组, 单位: 像素, 需要获取转为 Vector2[] 的值请调用 GetPoints() 函数
    /// </summary>
    [JsonInclude]
    public List<float> Points;

    private Vector2[] _pointVector2Array;
    
    public NavigationPolygonData()
    {
    }

    public NavigationPolygonData(NavigationPolygonType type)
    {
        Type = type;
    }

    /// <summary>
    /// 读取所有的坐标点, 单位: 像素
    /// </summary>
    public Vector2[] GetPoints()
    {
        if (_pointVector2Array == null)
        {
            if (Points == null)
            {
                return null;
            }

            _pointVector2Array = new Vector2[Points.Count / 2];
            for (var i = 0; i < Points.Count; i += 2)
            {
                _pointVector2Array[i / 2] = new Vector2(Points[i], Points[i + 1]);
            }
        }

        return _pointVector2Array;
    }

    /// <summary>
    /// 设置所有的坐标点
    /// </summary>
    public void SetPoints(Vector2[] array)
    {
        _pointVector2Array = array;
        Points = new List<float>();
        foreach (var pos in array)
        {
            Points.Add(pos.X);
            Points.Add(pos.Y);
        }
    }
}