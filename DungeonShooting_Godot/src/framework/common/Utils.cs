using System.Linq;
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
        GD.Print("随机种子为: ", Random.Seed);
    }

    /// <summary>
    /// 根据四个点计算出矩形
    /// </summary>
    public static Rect2 CalcRect(float start1, float end1, float start2, float end2)
    {
        return new Rect2(
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
    public static void DrawNavigationPolygon(CanvasItem canvasItem, NavigationPolygonData[] polygonData, int width = 1)
    {
        for (var i = 0; i < polygonData.Length; i++)
        {
            var item = polygonData[i];
            if (item.Points.Count >= 2)
            {
                var array = item.ConvertPointsToVector2Array().ToList();
                array.Add(array[0]);
                if (item.Type == NavigationPolygonType.In)
                {
                    canvasItem.DrawPolyline(array.ToArray(), Colors.Yellow, width);
                }
                else
                {
                    canvasItem.DrawPolyline(array.ToArray(), Colors.Red, width);
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
}