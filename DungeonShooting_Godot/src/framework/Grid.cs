
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 网格数据结构
/// </summary>
public class Grid<T>
{
    private Dictionary<int, Dictionary<int, T>> _map = new Dictionary<int, Dictionary<int, T>>();

    public bool Contains(int x, int y)
    {
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

    public void AddRect(Vector2 pos, Vector2 size)
    {
        
    }

    public bool TestRect()
    {
        return true;
    }
}
