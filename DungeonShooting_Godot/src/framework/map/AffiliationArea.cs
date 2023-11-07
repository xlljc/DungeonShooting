
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间归属区域
/// </summary>
public partial class AffiliationArea : Area2D, IDestroy
{
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 当前实例所属房间
    /// </summary>
    public RoomInfo RoomInfo;
    
    /// <summary>
    /// 当前归属区域包含的所有物体对象, 物体进入另一个区域时才会从该集合中移除
    /// </summary>
    private readonly HashSet<ActivityObject> _includeItems = new HashSet<ActivityObject>();

    /// <summary>
    /// 已经进入 AffiliationArea 所在范围内的物体, 物体一旦离开该区域就会立刻从该集合中删除
    /// </summary>
    private readonly HashSet<ActivityObject> _enterItems = new HashSet<ActivityObject>();
    
    /// <summary>
    /// 玩家是否是第一次进入
    /// </summary>
    public bool IsFirstEnterFlag { get; private set; } = true;

    private bool _init = false;
    private Vector2 _initSize;
    private RectangleShape2D _shape;
    
    /// <summary>
    /// 根据矩形区域初始化归属区域
    /// </summary>
    public void Init(RoomInfo roomInfo, Rect2I rect2)
    {
        if (_init)
        {
            return;
        }

        _init = true;

        _initSize = rect2.Size;
        RoomInfo = roomInfo;

        var collisionShape = new CollisionShape2D();
        collisionShape.GlobalPosition = rect2.Position + rect2.Size / 2;
        var shape = new RectangleShape2D();
        _shape = shape;
        shape.Size = rect2.Size;
        collisionShape.Shape = shape;
        AddChild(collisionShape);
        _Init();
    }

    private void _Init()
    {
        Monitoring = true;
        Monitorable = false;
        CollisionLayer = PhysicsLayer.None;
        CollisionMask = PhysicsLayer.Prop | PhysicsLayer.Player | PhysicsLayer.Enemy | PhysicsLayer.Debris | PhysicsLayer.Throwing;

        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    /// <summary>
    /// 将物体添加到当前所属区域中
    /// </summary>
    public void InsertItem(ActivityObject activityObject)
    {
        if (activityObject.AffiliationArea == this)
        {
            return;
        }

        if (activityObject.AffiliationArea != null)
        {
            activityObject.AffiliationArea._includeItems.Remove(activityObject);
        }
        activityObject.AffiliationArea = this;
        _includeItems.Add(activityObject);

        //如果是玩家
        if (activityObject == Player.Current)
        {
            OnPlayerInsertRoom();
        }
    }

    /// <summary>
    /// 将物体从当前所属区域移除
    /// </summary>
    public void RemoveItem(ActivityObject activityObject)
    {
        if (activityObject.AffiliationArea == null)
        {
            return;
        }
        activityObject.AffiliationArea = null;
        _includeItems.Remove(activityObject);
    }

    /// <summary>
    /// 获取该区域中物体的总数
    /// </summary>
    public int GetIncludeItemsCount()
    {
        return _includeItems.Count;
    }

    /// <summary>
    /// 统计符合条件的数量
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public int FindIncludeItemsCount(Func<ActivityObject, bool> handler)
    {
        var count = 0;
        foreach (var activityObject in _includeItems)
        {
            if (!activityObject.IsDestroyed && handler(activityObject))
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 查询所有符合条件的对象并返回
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public ActivityObject[] FindIncludeItems(Func<ActivityObject, bool> handler)
    {
        var list = new List<ActivityObject>();
        foreach (var activityObject in _includeItems)
        {
            if (!activityObject.IsDestroyed && handler(activityObject))
            {
                list.Add(activityObject);
            }
        }
        return list.ToArray();
    }

    /// <summary>
    /// 检查是否有符合条件的对象
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public bool ExistIncludeItem(Func<ActivityObject, bool> handler)
    {
        foreach (var activityObject in _includeItems)
        {
            if (!activityObject.IsDestroyed && handler(activityObject))
            {
                return true;
            }
        }

        return false;
    }
    
    /// <summary>
    /// 获取进入该区域中物体的总数
    /// </summary>
    public int GetEnterItemsCount()
    {
        return _enterItems.Count;
    }
    
    /// <summary>
    /// 统计进入该区域且符合条件的数量
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public int FindEnterItemsCount(Func<ActivityObject, bool> handler)
    {
        var count = 0;
        foreach (var activityObject in _enterItems)
        {
            if (!activityObject.IsDestroyed && handler(activityObject))
            {
                count++;
            }
        }
        return count;
    }
    
    /// <summary>
    /// 查询所有进入该区域且符合条件的对象并返回
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public ActivityObject[] FindEnterItems(Func<ActivityObject, bool> handler)
    {
        var list = new List<ActivityObject>();
        foreach (var activityObject in _enterItems)
        {
            if (!activityObject.IsDestroyed && handler(activityObject))
            {
                list.Add(activityObject);
            }
        }
        return list.ToArray();
    }

    /// <summary>
    /// 检查是否有进入该区域且符合条件的对象
    /// </summary>
    /// <param name="handler">操作函数, 返回是否满足要求</param>
    public bool ExistEnterItem(Func<ActivityObject, bool> handler)
    {
        foreach (var activityObject in _enterItems)
        {
            if (!activityObject.IsDestroyed && handler(activityObject))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 返回物体是否进入了该区域
    /// </summary>
    public bool IsEnter(ActivityObject activityObject)
    {
        return _enterItems.Contains(activityObject);
    }
    
    private void OnBodyEntered(Node2D body)
    {
        if (body is ActivityObject activityObject)
        {
            _enterItems.Add(activityObject);
            //注意需要延时调用
            CallDeferred(nameof(InsertItem), activityObject);
        }
    }
    
    private void OnBodyExited(Node2D body)
    {
        if (body is ActivityObject activityObject)
        {
            _enterItems.Remove(activityObject);
        }
    }

    //玩家进入房间
    private void OnPlayerInsertRoom()
    {
        if (IsFirstEnterFlag)
        {
            EventManager.EmitEvent(EventEnum.OnPlayerFirstEnterRoom, RoomInfo);
        }
        EventManager.EmitEvent(EventEnum.OnPlayerEnterRoom, RoomInfo);
        IsFirstEnterFlag = false;
    }

    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
        _includeItems.Clear();
        _enterItems.Clear();
    }

    /// <summary>
    /// 在初始区域的基础上扩展区域, size为扩展大小
    /// </summary>
    public void ExtendedRegion(Vector2 size)
    {
        _shape.Size = _initSize + size;
    }

    /// <summary>
    /// 将区域还原到初始大小
    /// </summary>
    public void RestoreRegion()
    {
        _shape.Size = _initSize;
    }
}