using Godot;
using System;
using System.Collections.Generic;

public partial class TestGridData : Node2D
{

    public class TestGrid<T>
    {
        public TestGrid(int width)
        {
            Width = width;
        }

        public int Width { get; }
        private Dictionary<int, T> _dictionary = new Dictionary<int, T>();

        public void Set(int x, int y, T data)
        {
            if (x <= Width)
            {
                _dictionary[y * Width + x] = data;
            }
        }

        public T Get(int x, int y)
        {
            if (x <= Width && _dictionary.TryGetValue(y * Width + x, out var value))
            {
                return value;
            }

            return default;
        }

        public void ForEach(Action<int, int, T> callback)
        {
            foreach (var keyValuePair in _dictionary)
            {
                var index = keyValuePair.Key;
                callback(index % Width, index / Width, keyValuePair.Value);
            }
        }
    }

    public override void _Ready()
    {
        var time = DateTime.Now;
        var testGrid = new TestGrid<int>(100000);
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                testGrid.Set(i, j, i + j);
            }
        }
        Debug.Log("TestGrid设置值用时： " + (DateTime.Now - time).Milliseconds);

        time = DateTime.Now;
        var testGrid2 = new Grid<int>();
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                testGrid2.Set(i, j, i + j);
            }
        }
        Debug.Log("Grid设置值用时： " + (DateTime.Now - time).Milliseconds);
        
        time = DateTime.Now;
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                testGrid.Get(i, j);
            }
        }
        Debug.Log("TestGrid取值用时： " + (DateTime.Now - time).Milliseconds);

        time = DateTime.Now;
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                testGrid2.Get(i, j);
            }
        }
        Debug.Log("Grid取值用时： " + (DateTime.Now - time).Milliseconds);
        
        time = DateTime.Now;
        testGrid.ForEach((i, i1, arg3) =>
        {
            
        });
        Debug.Log("TestGrid遍历用时： " + (DateTime.Now - time).Milliseconds);
        
        time = DateTime.Now;
        testGrid2.ForEach((i, i1, arg3) =>
        {
            
        });
        Debug.Log("Grid遍历用时： " + (DateTime.Now - time).Milliseconds);
    }
}
