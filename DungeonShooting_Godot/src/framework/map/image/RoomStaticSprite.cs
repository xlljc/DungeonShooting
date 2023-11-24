
using System.Collections.Generic;
using Godot;

/// <summary>
/// RoomInfo 中用于存放静态Sprite的功能类
/// </summary>
public partial class RoomStaticSprite : Node2D, IDestroy
{
    
    public bool IsDestroyed { get; private set; }

    private readonly List<List<FreezeSprite>> _list = new List<List<FreezeSprite>>();
    private readonly InfiniteGrid<List<FreezeSprite>> _grid = new InfiniteGrid<List<FreezeSprite>>();
    private readonly RoomInfo _roomInfo;
    //网格划分的格子大小
    private readonly Vector2I _step;
    //当前残骸数量
    private int _count = 0;
    //最大残骸数量
    private int _maxCount = 1000;
    //每个格子中最大残骸数量
    private int _stepMaxCount = 30;
    private int _num = 0;

    public RoomStaticSprite(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        _step = new Vector2I(30, 30);
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;

        _grid.ForEach((x, y, data) =>
        {
            foreach (var spriteData in data)
            {
                spriteData.Destroy();
            }
            data.Clear();
        });
        _grid.Clear();
        QueueFree();
    }

    /// <summary>
    /// 添加静态精灵
    /// </summary>
    public void AddFreezeSprite(FreezeSprite freezeSprite)
    {
        var pos = ToGridPosition(freezeSprite.Position);
        var list = _grid.Get(pos);
        if (list == null)
        {
            list = new List<FreezeSprite>();
            _grid.Set(pos, list);
            _list.Add(list);
        }
        list.Add(freezeSprite);
        _count++;
        
        if (list.Count > _stepMaxCount) //检测单个step中残骸是否超出最大数量
        {
            var sprite = list[0];
            list.RemoveAt(0);
            sprite.Destroy();
            _count--;
        }
        else if (_count > _maxCount) //检测所有残骸是否超出最大数量
        {
            if (_num <= 0 || _list[0].Count == 0)
            {
                _num = 5;
                _list.Sort((l1, l2) => l2.Count - l1.Count);
            }
            else
            {
                _num--;
            }

            var tempList = _list[0];
            if (tempList.Count > 0)
            {
                var sprite = tempList[0];
                tempList.RemoveAt(0);
                sprite.Destroy();
                _count--;
            }
        }
    }

    /// <summary>
    /// 移除静态精灵
    /// </summary>
    public void RemoveFreezeSprite(FreezeSprite freezeSprite)
    {
        var pos = ToGridPosition(freezeSprite.Position);
        var list = _grid.Get(pos);
        if (list != null)
        {
            if (list.Remove(freezeSprite))
            {
                _count--;
            }
        }
    }

    public List<FreezeSprite> CollisionCircle(Vector2 position, float radius, bool unfreeze = false)
    {
        var len = radius * radius;
        var result = new List<FreezeSprite>();
        var startPosition = ToGridPosition(new Vector2(position.X - radius, position.Y - radius));
        var endPosition = ToGridPosition(new Vector2(position.X + radius, position.Y + radius));
        for (var x = startPosition.X; x <= endPosition.X; x++)
        {
            for (var y = startPosition.Y; y <= endPosition.Y; y++)
            {
                var data = _grid.Get(x, y);
                if (data != null)
                {
                    for (var i = 0; i < data.Count; i++)
                    {
                        var freezeSprite = data[i];
                        if (position.DistanceSquaredTo(freezeSprite.Position) < len)
                        {
                            result.Add(freezeSprite);
                            if (unfreeze)
                            {
                                freezeSprite.HandlerUnfreezeSprite();
                                data.RemoveAt(i--);
                                _count--;
                            }
                        }
                    }
                }
            }
        }

        return result;
    }

    private Vector2I ToGridPosition(Vector2 position)
    {
        var x = Mathf.FloorToInt(position.X / _step.X);
        var y = Mathf.FloorToInt(position.Y / _step.Y);
        return new Vector2I(x * _step.X, y * _step.Y);
    }
}