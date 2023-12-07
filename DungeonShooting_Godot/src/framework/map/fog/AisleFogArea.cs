
using Godot;

/// <summary>
/// 过道迷雾区域碰撞器, 用于检测玩家是否进入过道
/// </summary>
public partial class AisleFogArea : Area2D, IDestroy
{
    public bool IsDestroyed { get; private set; }
    
    /// <summary>
    /// 所属连接的门 (起始门)
    /// </summary>
    public RoomDoorInfo RoomDoorInfo { get; private set; }
    
    private bool _init = false;
    private RectangleShape2D _shape;
    
    /// <summary>
    /// 根据矩形区域初始化归属区域
    /// </summary>
    public void Init(RoomDoorInfo doorInfo, Rect2I rect2)
    {
        if (_init)
        {
            return;
        }

        _init = true;
        
        RoomDoorInfo = doorInfo;
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
        CollisionMask = PhysicsLayer.Player;

        BodyEntered += OnBodyEntered;
        //BodyExited += OnBodyExited;
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
    }
    
    private void OnBodyEntered(Node2D body)
    {
        if (body == Player.Current)
        {
            //注意需要延时调用
            CallDeferred(nameof(InsertPlayer));
        }
    }

    private void InsertPlayer()
    {
        //Debug.Log("玩家进入过道");
        if (!RoomDoorInfo.AisleFogMask.IsExplored)
        {
            EventManager.EmitEvent(EventEnum.OnPlayerFirstEnterAisle, RoomDoorInfo);
        }
        EventManager.EmitEvent(EventEnum.OnPlayerEnterAisle, RoomDoorInfo);
        FogMaskHandler.RefreshAisleFog(RoomDoorInfo);
    }
}