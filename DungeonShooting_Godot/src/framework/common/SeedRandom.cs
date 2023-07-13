
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 随机数类
/// </summary>
public class SeedRandom
{
    /// <summary>
    /// 种子值
    /// </summary>
    public int Seed { get; }
    
    private Random _random;
    private static int _staticSeed = 0;
    
    public SeedRandom(int seed)
    {
        Seed = seed;
        _random = new Random(seed);
    }

    public SeedRandom()
    {
        var dateTime = DateTime.Now;
        var num = dateTime.Year * 100000 + dateTime.Month * 100000 + dateTime.Day * 100000 + dateTime.Hour * 10000 + dateTime.Minute * 100 + dateTime.Second;
        num += _staticSeed;
        _staticSeed += 11111;
        Seed = num;
        _random = new Random(num);
    }
    
    /// <summary>
    /// 返回一个随机的double值
    /// </summary>
    public double RandomDouble()
    {
        return _random.NextDouble();
    }
    
    /// <summary>
    /// 返回随机 boolean 值
    /// </summary>
    public bool RandomBoolean()
    {
        return _random.NextSingle() >= 0.5f;
    }    
    /// <summary>
    /// 指定概率会返回 true, probability 范围 0 - 1
    /// </summary>
    public bool RandomBoolean(float probability)
    {
        return _random.NextSingle() <= probability;
    }

    /// <summary>
    /// 返回一个区间内的随机小数
    /// </summary>
    public float RandomRangeFloat(float min, float max)
    {
        if (min == max) return min;
        if (min > max)
            return _random.NextSingle() * (min - max) + max;
        return _random.NextSingle() * (max - min) + min;
    }

    /// <summary>
    /// 返回一个区间内的随机整数
    /// </summary>
    public int RandomRangeInt(int min, int max)
    {
        if (min == max) return min;
        if (min > max)
            return Mathf.FloorToInt(_random.NextSingle() * (min - max + 1) + max);
        return Mathf.FloorToInt(_random.NextSingle() * (max - min + 1) + min);
    }

    /// <summary>
    /// 随机返回其中一个参数
    /// </summary>
    public T RandomChoose<T>(params T[] list)
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
    public T RandomChoose<T>(List<T> list)
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
    public T RandomChooseAndRemove<T>(List<T> list)
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
    public int RandomWeight(List<int> weightList)
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
}