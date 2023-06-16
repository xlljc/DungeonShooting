using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 常用函数工具类
/// </summary>
public static class Utils
{

    private static readonly Random _random;
    
    static Utils()
    {
        var dateTime = DateTime.Now;
        var num = dateTime.Year * 100000 + dateTime.Month * 100000 + dateTime.Day * 100000 + dateTime.Hour * 10000 + dateTime.Minute * 100 + dateTime.Second;
        //_random = new Random(204313957);
        _random = new Random(num);
        GD.Print("随机种子为: ", num);
    }

    /// <summary>
    /// 返回一个随机的double值
    /// </summary>
    public static double RandomDouble()
    {
        return _random.NextDouble();
    }
    
    /// <summary>
    /// 返回随机 boolean 值
    /// </summary>
    public static bool RandomBoolean()
    {
        return _random.NextSingle() >= 0.5f;
    }

    /// <summary>
    /// 返回一个区间内的随机小数
    /// </summary>
    public static float RandomRangeFloat(float min, float max)
    {
        if (min == max) return min;
        if (min > max)
            return _random.NextSingle() * (min - max) + max;
        return _random.NextSingle() * (max - min) + min;
    }

    /// <summary>
    /// 返回一个区间内的随机整数
    /// </summary>
    public static int RandomRangeInt(int min, int max)
    {
        if (min == max) return min;
        if (min > max)
            return Mathf.FloorToInt(_random.NextSingle() * (min - max + 1) + max);
        return Mathf.FloorToInt(_random.NextSingle() * (max - min + 1) + min);
    }

    /// <summary>
    /// 随机返回其中一个参数
    /// </summary>
    public static T RandomChoose<T>(params T[] list)
    {
        if (list.Length == 0)
        {
            return default;
        }

        return list[RandomRangeInt(0, list.Length - 1)];
    }

    /// <summary>
    /// 随机返回集合中的一个元素
    /// </summary>
    public static T RandomChoose<T>(List<T> list)
    {
        if (list.Count == 0)
        {
            return default;
        }

        return list[RandomRangeInt(0, list.Count - 1)];
    }

    /// <summary>
    /// 随机返回集合中的一个元素, 并将其从集合中移除
    /// </summary>
    public static T RandomChooseAndRemove<T>(List<T> list)
    {
        if (list.Count == 0)
        {
            return default;
        }

        var index = RandomRangeInt(0, list.Count - 1);
        var result = list[index];
        list.RemoveAt(index);
        return result;
    }

    /// <summary>
    /// 从权重列表中随机抽取下标值
    /// </summary>
    public static int RandomWeight(List<int> weightList)
    {
        // 计算总权重
        var totalWeight = 0;
        foreach (var weight in weightList)
        {
            totalWeight += weight;
        }
        
        var randomNumber = _random.Next(totalWeight);
        var currentWeight = 0;
        for (var i = 0; i < weightList.Count; i++)
        {
            var value = weightList[i];
            currentWeight += value;
            if (randomNumber < currentWeight)
            {
                return i;
            }
        }

        return RandomRangeInt(0, weightList.Count - 1);
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
}