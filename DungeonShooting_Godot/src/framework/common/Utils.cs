using System.Collections.Generic;
using Godot;

/// <summary>
/// 常用函数工具类
/// </summary>
public static class Utils
{

    /// <summary>
    /// 返回随机 boolean 值
    /// </summary>
    public static bool RandBoolean()
    {
        return GD.Randf() >= 0.5f;
    }

    /// <summary>
    /// 返回一个区间内的随机小数
    /// </summary>
    public static float RandRange(float min, float max)
    {
        if (min == max) return min;
        if (min > max)
            return GD.Randf() * (min - max) + max;
        return GD.Randf() * (max - min) + min;
    }

    /// <summary>
    /// 返回一个区间内的随机整数
    /// </summary>
    public static int RandRangeInt(int min, int max)
    {
        if (min == max) return min;
        if (min > max)
            return Mathf.FloorToInt(GD.Randf() * (min - max + 1) + max);
        return Mathf.FloorToInt(GD.Randf() * (max - min + 1) + min);
    }

    /// <summary>
    /// 随机返回其中一个参数
    /// </summary>
    public static T RandChoose<T>(params T[] list)
    {
        if (list.Length == 0)
        {
            return default;
        }

        return list[RandRangeInt(0, list.Length - 1)];
    }

    /// <summary>
    /// 随机返回集合中的一个元素
    /// </summary>
    public static T RandChoose<T>(List<T> list)
    {
        if (list.Count == 0)
        {
            return default;
        }

        return list[RandRangeInt(0, list.Count - 1)];
    }

    public static Rect2 CalcRect(float start1, float end1, float start2, float end2)
    {
        return new Rect2(
            Mathf.Min(start1, start2), Mathf.Min(end1, end2),
            Mathf.Abs(start1 - start2), Mathf.Abs(end1 - end2)
        );
    }
}