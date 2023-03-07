
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 归属区域
/// </summary>
public partial class AffiliationArea : Area2D
{
    /// <summary>
    /// 当前实例所属房间
    /// </summary>
    public RoomInfo RoomInfo;
    
    /// <summary>
    /// 当前归属区域包含的所有物体对象
    /// </summary>
    private readonly HashSet<ActivityObject> _includeItems = new HashSet<ActivityObject>();

    private bool _init = false;
    //玩家是否是第一次进入
    private bool _isFirstEnterFlag = true;
    
    /// <summary>
    /// 根据矩形区域初始化归属区域
    /// </summary>
    public void Init(RoomInfo roomInfo, Rect2 rect2)
    {
        if (_init)
        {
            return;
        }

        _init = true;

        RoomInfo = roomInfo;
        var collisionShape = new CollisionShape2D();
        collisionShape.GlobalPosition = rect2.Position + rect2.Size / 2;
        var shape = new RectangleShape2D();
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
        CollisionMask = PhysicsLayer.Props | PhysicsLayer.Player | PhysicsLayer.Enemy;

        BodyEntered += OnBodyEntered;
    }

    /// <summary>
    /// 将物体添加到当前所属区域中
    /// </summary>
    public void InsertItem(ActivityObject activityObject)
    {
        if (activityObject.Affiliation == this)
        {
            return;
        }

        if (activityObject.Affiliation != null)
        {
            activityObject.Affiliation.RemoveItem(activityObject);
        }
        activityObject.Affiliation = this;
        _includeItems.Add(activityObject);

        //如果是玩家
        if (activityObject == Player.Current)
        {
            OnPlayerEnterRoom();
        }
    }

    /// <summary>
    /// 将物体从当前所属区域移除
    /// </summary>
    public void RemoveItem(ActivityObject activityObject)
    {
        activityObject.Affiliation = null;
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
            if (handler(activityObject))
            {
                count++;
            }
        }
        return count;
    }
    
    private void OnBodyEntered(Node2D body)
    {
        if (body is ActivityObject activityObject)
        {
            InsertItem(activityObject);
        }
    }

    //玩家进入房间
    private void OnPlayerEnterRoom()
    {
        if (_isFirstEnterFlag)
        {
            EventManager.EmitEvent(EventEnum.OnPlayerFirstEnterRoom, RoomInfo);
            _isFirstEnterFlag = false;
        }
        EventManager.EmitEvent(EventEnum.OnPlayerEnterRoom, RoomInfo);
    }
}