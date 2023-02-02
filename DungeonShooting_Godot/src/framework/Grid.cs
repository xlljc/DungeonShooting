
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 网格数据结构, 通过 x 和 y 存储数据
/// </summary>
public class Grid<T>
{
    /// <summary>
    /// 遍历网格数据回调
    /// </summary>
    public delegate void EachGridCallback(int x, int y, T data);
    
    private readonly Dictionary<int, Dictionary<int, T>> _map = new Dictionary<int, Dictionary<int, T>>();

    /// <summary>
    /// 返回指定xy位置是否存在数据
    /// </summary>
    public bool Contains(int x, int y)
    {
        if (_map.TryGetValue(x, out var value))
        {
            return value.ContainsKey(y);
        }

        return false;
    }

    /// <summary>
    /// 设置指定xy位置的数据
    /// </summary>
    public void Set(int x, int y, T data)
    {
        if (_map.TryGetValue(x, out var value))
        {
            value[y] = data;
        }
        else
        {
            value = new Dictionary<int, T>();
            value.Add(y, data);
            _map.Add(x, value);
        }
    }

    /// <summary>
    /// 获取指定xy位置的数据
    /// </summary>
    public T Get(int x, int y)
    {
        if (_map.TryGetValue(x, out var value))
        {
            return value[y];
        }

        return default;
    }
    
    /// <summary>
    /// 往一个区域中填充指定的数据
    /// </summary>
    /// <param name="pos">起点位置</param>
    /// <param name="size">区域大小</param>
    /// <param name="data">数据</param>
    public void AddRect(Vector2 pos, Vector2 size, T data)
    {
        var x = (int)pos.x;
        var y = (int)pos.y;
        for (var i = 0; i < size.x; i++)
        {
            for (var j = 0; j < size.y; j++)
            {
                if (_map.TryGetValue(x + i, out var value))
                {
                    value[y + j] = data;
                }
                else
                {
                    value = new Dictionary<int, T>();
                    value.Add(y + j, data);
                    _map.Add(x + i, value);
                }
            }
        }
    }

    /// <summary>
    /// 移除指定区域数据
    /// </summary>
    /// <param name="pos">起点位置</param>
    /// <param name="size">区域大小</param>
    public void RemoveRect(Vector2 pos, Vector2 size)
    {
        var x = (int)pos.x;
        var y = (int)pos.y;
        for (var i = 0; i < size.x; i++)
        {
            for (var j = 0; j < size.y; j++)
            {
                if (_map.TryGetValue(x + i, out var value))
                {
                    value.Remove(y + j);
                    if (value.Count == 0)
                    {
                        _map.Remove(x + i);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 检测矩形区域内是否与其他数据有碰撞
    /// </summary>
    /// <param name="pos">起点位置</param>
    /// <param name="size">区域大小</param>
    public bool RectCollision(Vector2 pos, Vector2 size)
    {
        var x = (int)pos.x;
        var y = (int)pos.y;
        var w = (int)size.x;
        var h = (int)size.y;
        //先判断四个角
        if (Contains(x, y) || Contains(x + w - 1, y) || Contains(x, y + h - 1) || Contains(x + w - 1, y + h - 1))
        {
            return true;
        }

        //逐个点判断
        for (int i = 1; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (Contains(x + i, y + j))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 遍历网格数据
    /// </summary>
    public void ForEach(EachGridCallback cb)
    {
        foreach (var pair1 in _map)
        {
            foreach (var pair2 in pair1.Value)
            {
                cb(pair1.Key, pair2.Key, pair2.Value);
            }
        }
    }
}
