
using System.Collections.Generic;
using Godot;

/// <summary>
/// RoomInfo 中用于存放静态Sprite的功能类
/// </summary>
public partial class RoomStaticSprite : Node2D, IDestroy
{
    
    public bool IsDestroyed { get; private set; }
    
    private readonly Grid<List<FreezeSprite>> _grid = new Grid<List<FreezeSprite>>();
    private readonly RoomInfo _roomInfo;
    private readonly Vector2I _step;

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
        }
        list.Add(freezeSprite);
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
            list.Remove(freezeSprite);
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