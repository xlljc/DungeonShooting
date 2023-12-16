using Godot;

/// <summary>
/// 常用函数工具类
/// </summary>
public static class Utils
{
    /// <summary>
    /// 默认随机数对象
    /// </summary>
    public static SeedRandom Random { get; }
    
    static Utils()
    {
        Random = new SeedRandom();
        Debug.Log("随机种子为: ", Random.Seed);
    }

    /// <summary>
    /// 根据两个点计算出矩形
    /// </summary>
    public static Rect2 CalcRect(float start1, float end1, float start2, float end2)
    {
        return new Rect2(
            Mathf.Min(start1, start2), Mathf.Min(end1, end2),
            Mathf.Abs(start1 - start2), Mathf.Abs(end1 - end2)
        );
    }
    
    /// <summary>
    /// 根据两个点计算出矩形
    /// </summary>
    public static Rect2I CalcRect(int start1, int end1, int start2, int end2)
    {
        return new Rect2I(
            Mathf.Min(start1, start2), Mathf.Min(end1, end2),
            Mathf.Abs(start1 - start2), Mathf.Abs(end1 - end2)
        );
    }
    
    /// <summary>
    /// 返回碰撞层 mask 是否会检测 layer 
    /// </summary>
    public static bool CollisionMaskWithLayer(uint mask, uint layer)
    {
        return (mask & layer) != 0;
    }

    /// <summary>
    /// 使用定的 canvasItem 绘制导航区域, 注意, 该函数只能在 draw 函数中调用
    /// </summary>
    public static void DrawNavigationPolygon(CanvasItem canvasItem, NavigationPolygonData[] polygonData, float width = 1)
    {
        for (var i = 0; i < polygonData.Length; i++)
        {
            var item = polygonData[i];
            var points = item.GetPoints();
            if (points.Length>= 2)
            {
                var array = new Vector2[points.Length + 1];
                for (var j = 0; j < points.Length; j++)
                {
                    array[j] = points[j];
                }

                array[array.Length - 1] = points[0];
                if (item.Type == NavigationPolygonType.In)
                {
                    canvasItem.DrawPolyline(array, Colors.Orange, width);
                }
                else
                {
                    canvasItem.DrawPolyline(array, Colors.Orange, width);
                }
            }
        }
    }
    
    /// <summary>
    /// 将一个任意角度转为0到360度
    /// </summary>
    public static float ConvertAngle(float angle)
    {
        angle %= 360; // 取余

        if (angle < 0) // 如果角度为负数，转为正数
        {
            angle += 360;
        }

        return angle;
    }
    
    /// <summary>
    /// 根据步长吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static float Adsorption(this float value, float step)
    {
        var f = Mathf.Round(value / step);
        return f * step;
    }
    
    /// <summary>
    /// 根据步长吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static int Adsorption(this float value, int step)
    {
        var f = Mathf.RoundToInt(value / step);
        return f * step;
    }
    
    /// <summary>
    /// 根据步长吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static Vector2 Adsorption(this Vector2 value, Vector2 step)
    {
        var x = Mathf.Round(value.X / step.X);
        var y = Mathf.Round(value.Y / step.Y);
        return new Vector2(x * step.X, y * step.Y);
    }
    
    /// <summary>
    /// 根据步长吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static Vector2I Adsorption(this Vector2 value, Vector2I step)
    {
        var x = Mathf.RoundToInt(value.X / step.X);
        var y = Mathf.RoundToInt(value.Y / step.Y);
        return new Vector2I(x * step.X, y * step.Y);
    }
    
    /// <summary>
    /// 根据步长按照 Floor() 函数吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static float FloorAdsorption(this float value, float step)
    {
        var f = Mathf.Floor(value / step);
        return f * step;
    }
    
    /// <summary>
    /// 根据步长按照 Floor() 函数吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static int FloorAdsorption(this float value, int step)
    {
        var f = Mathf.FloorToInt(value / step);
        return f * step;
    }

    /// <summary>
    /// 根据步长按照 Floor() 函数吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static Vector2 FloorAdsorption(this Vector2 value, Vector2 step)
    {
        var x = Mathf.Floor(value.X / step.X);
        var y = Mathf.Floor(value.Y / step.Y);
        return new Vector2(x * step.X, y * step.Y);
    }
    
    /// <summary>
    /// 根据步长按照 Floor() 函数吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static Vector2I FloorAdsorption(this Vector2 value, Vector2I step)
    {
        var x = Mathf.FloorToInt(value.X / step.X);
        var y = Mathf.FloorToInt(value.Y / step.Y);
        return new Vector2I(x * step.X, y * step.Y);
    }

    /// <summary>
    /// 字符串首字母小写
    /// </summary>
    public static string FirstToLower(this string str)
    {
        return str.Substring(0, 1).ToLower() + str.Substring(1);
    }
    
    /// <summary>
    /// 字符串首字母大写
    /// </summary>
    public static string FirstToUpper(this string str)
    {
        return str.Substring(0, 1).ToUpper() + str.Substring(1);
    }

    /// <summary>
    /// 将 Vector2 类型转为 Vector2I 类型
    /// </summary>
    public static Vector2I AsVector2I(this Vector2 vector2)
    {
        return new Vector2I((int)vector2.X, (int)vector2.Y);
    }

    /// <summary>
    /// 返回指定坐标是否在UI节范围点内
    /// </summary>
    public static bool IsPositionOver(this Control control, Vector2 position)
    {
        var globalPosition = control.GlobalPosition;
        var size = control.Size * control.Scale;
        return position.X >= globalPosition.X && position.X <= (globalPosition.X + size.X) &&
               position.Y >= globalPosition.Y && position.Y <= (globalPosition.Y + size.Y);
    }

    /// <summary>
    /// 判断点是否在区域内
    /// </summary>
    public static bool IsPositionInRect(Vector2 pos, Rect2 rect2)
    {
        return pos.X >= rect2.Position.X && pos.X <= rect2.Position.X + rect2.Size.X &&
               pos.Y >= rect2.Position.Y && pos.Y <= rect2.Position.Y + rect2.Size.Y;
    }

    /// <summary>
    /// 返回区域起始值, 用于获取配置表范围配置数据
    /// </summary>
    public static int GetConfigRangeStart(int[] range)
    {
        return range[0];
    }
    
    /// <summary>
    /// 返回区域结束值, 用于获取配置表范围配置数据
    /// </summary>
    public static int GetConfigRangeEnd(int[] range)
    {
        if (range.Length > 1)
        {
            return range[1];
        }

        return range[0];
    }
    
    /// <summary>
    /// 返回区域起始值, 用于获取配置表范围配置数据
    /// </summary>
    public static float GetConfigRangeStart(float[] range)
    {
        return range[0];
    }
    
    /// <summary>
    /// 返回区域结束值, 用于获取配置表范围配置数据
    /// </summary>
    public static float GetConfigRangeEnd(float[] range)
    {
        if (range.Length > 1)
        {
            return range[1];
        }

        return range[0];
    }

    /// <summary>
    /// 创建扇形多边形区域数据, 返回坐标点
    /// </summary>
    /// <param name="centerAngle">中心角度, 角度制</param>
    /// <param name="radius">扇形半径</param>
    /// <param name="range">扇形开口角度, 角度制</param>
    /// <param name="edgesCount">扇形弧度边的数量</param>
    /// <param name="offset">整体偏移坐标, 默认0</param>
    public static Vector2[] CreateSectorPolygon(float centerAngle, float radius, float range, uint edgesCount, Vector2? offset = null)
    {
        var point = new Vector2[edgesCount + 2];
        var edgesAngle = range / edgesCount;
        var startAngle = centerAngle - range * 0.5f;
        var temp = new Vector2(radius, 0);

        for (var i = 0; i <= edgesCount; i++)
        {
            if (offset == null)
            {
                point[i] = temp.Rotated(Mathf.DegToRad(startAngle + edgesAngle * i));
            }
            else
            {
                point[i] = temp.Rotated(Mathf.DegToRad(startAngle + edgesAngle * i)) + offset.Value;
            }
        }

        if (offset == null)
        {
            point[point.Length - 1] = Vector2.Zero;
        }
        else
        {
            point[point.Length - 1] = offset.Value;
        }
        
        return point;
    }

    /// <summary>
    /// 将 point 位置限制在 anchor 的周围, 最大距离为 distance, 并返回新的位置
    /// </summary>
    public static Vector2 ConstrainDistance(Vector2 point, Vector2 anchor, float distance)
    {
        return (point - anchor).Normalized() * distance + anchor;
    }
    
    /// <summary>
    /// 返回一个点是否在 Polygon 内部
    /// </summary>
    /// <param name="polygon">多边形顶点</param>
    /// <param name="point">目标点</param>
    public static bool IsPointInPolygon(Vector2[] polygon, Vector2 point)
    {
        var isInside = false;
        for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
        {
            if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y) &&
                point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) +
                polygon[i].X)
            {
                isInside = !isInside;
            }
        }

        return isInside;
    }

    /// <summary>
    /// 根据法线翻转向量
    /// </summary>
    public static Vector2 ReflectByNormal(Vector2 vector, Vector2 normal)
    {
        return vector.Reflect(normal.Rotated(Mathf.Pi * 0.5f));
    }
    
    /// <summary>
    /// 根据法线翻转角度, 弧度制
    /// </summary>
    public static float ReflectByNormal(float rotation, Vector2 normal)
    {
        return ReflectByNormal(Vector2.FromAngle(rotation), normal).Angle();
    }
}