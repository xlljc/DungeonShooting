using System;
using System.Collections;
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
    public static float Adsorption(float value, float step)
    {
        var f = Mathf.Round(value / step);
        return f * step;
    }
    
    /// <summary>
    /// 根据步长吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static int Adsorption(float value, int step)
    {
        var f = Mathf.RoundToInt(value / step);
        return f * step;
    }
    
    /// <summary>
    /// 根据步长吸附值
    /// </summary>
    /// <param name="value">原数值</param>
    /// <param name="step">吸附步长</param>
    public static Vector2 Adsorption(Vector2 value, Vector2 step)
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
    public static Vector2I Adsorption(Vector2 value, Vector2I step)
    {
        var x = Mathf.RoundToInt(value.X / step.X);
        var y = Mathf.RoundToInt(value.Y / step.Y);
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
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelay(this ICoroutine coroutine, float delayTime, Action cb)
    {
        coroutine.StartCoroutine(_CallDelay(delayTime, cb));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelay<T1>(this ICoroutine coroutine, float delayTime, Action<T1> cb, T1 arg1)
    {
        coroutine.StartCoroutine(_CallDelay(delayTime, cb, arg1));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelay<T1, T2>(this ICoroutine coroutine, float delayTime, Action<T1, T2> cb, T1 arg1, T2 arg2)
    {
        coroutine.StartCoroutine(_CallDelay(delayTime, cb, arg1, arg2));
    }
    
    /// <summary>
    /// 延时指定时间调用一个回调函数
    /// </summary>
    public static void CallDelay<T1, T2, T3>(this ICoroutine coroutine, float delayTime, Action<T1, T2, T3> cb, T1 arg1, T2 arg2, T3 arg3)
    {
        coroutine.StartCoroutine(_CallDelay(delayTime, cb, arg1, arg2, arg3));
    }

    private static IEnumerator _CallDelay(float delayTime, Action cb)
    {
        yield return new WaitForSeconds(delayTime);
        cb();
    }
    
    private static IEnumerator _CallDelay<T1>(float delayTime, Action<T1> cb, T1 arg1)
    {
        yield return new WaitForSeconds(delayTime);
        cb(arg1);
    }
    
    private static IEnumerator _CallDelay<T1, T2>(float delayTime, Action<T1, T2> cb, T1 arg1, T2 arg2)
    {
        yield return new WaitForSeconds(delayTime);
        cb(arg1, arg2);
    }
    
    private static IEnumerator _CallDelay<T1, T2, T3>(float delayTime, Action<T1, T2, T3> cb, T1 arg1, T2 arg2, T3 arg3)
    {
        yield return new WaitForSeconds(delayTime);
        cb(arg1,arg2, arg3);
    }
}