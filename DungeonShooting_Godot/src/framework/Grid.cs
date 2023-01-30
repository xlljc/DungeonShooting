
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 网格数据结构
/// </summary>
public class Grid<T>
{
    private readonly Dictionary<int, Dictionary<int, T>> _map = new Dictionary<int, Dictionary<int, T>>();

    public bool Contains(int x, int y)
    {
        if (_map.TryGetValue(x, out var value))
        {
            return value.ContainsKey(y);
        }

        return false;
    }

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

    public T Get(int x, int y)
    {
        if (_map.TryGetValue(x, out var value))
        {
            return value[y];
        }
        return default;
    }

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

    public bool RectCollision(Vector2 pos, Vector2 size)
    {
        var x = (int)pos.x;
        var y = (int)pos.y;
        var w = (int)size.x;
        var h = (int)size.y;
        //先判断四个角
        if (Contains(x, y) || Contains(x + w, y) || Contains(x, y + h) || Contains(x + w, y + h))
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

    public void ForEach(Action<int, int, T> cb)
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
