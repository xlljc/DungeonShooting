using Godot;

/// <summary>
/// 拖尾效果
/// </summary>
public partial class Trail : Line2D, IPoolItem
{
    /// <summary>
    /// 拖尾效果固定更新帧率
    /// </summary>
    public const int TrailUpdateFrame = 20;
    
    /// <summary>
    /// 拖尾最大点数
    /// </summary>
    public int MaxLength { get; set; } = 20;
    /// <summary>
    /// 拖尾绑定的物体
    /// </summary>
    public Node2D Target { get; private set; }

    public bool IsDestroyed { get; private set; }
    
    public bool IsRecycled { get; set; }
    public string Logotype { get; set; }

    private double _time = 0;
    private IPoolItem _targetPoolItem;

    /// <summary>
    /// 设置拖尾跟随的物体
    /// </summary>
    public void SetTarget(Node2D target)
    {
        Target = target;
        if (target is IPoolItem poolItem)
        {
            _targetPoolItem = poolItem;
        }
        else
        {
            _targetPoolItem = null;
        }

        if (target != null)
        {
            ClearPoints();
        }

        _time = 1f / TrailUpdateFrame;
    }

    public void SetColor(Color color)
    {
        Gradient.SetColor(0, color);
        color.A = 0;
        Gradient.SetColor(1, color);
    }

    public override void _Process(double delta)
    {
        if (_targetPoolItem != null && _targetPoolItem.IsRecycled) //目标物体被回收
        {
            SetTarget(null);
        }
        
        _time += delta;
        var v = 1f / TrailUpdateFrame;
        if (_time >= v) //执行更新点
        {
            _time %= v;

            var pointCount = GetPointCount();
            if (Target != null) //没有被回收
            {
                AddPoint(Target.GlobalPosition, 0);
                if (pointCount > MaxLength)
                {
                    RemovePoint(pointCount);
                }
            }
            else //被回收了, 
            {
                if (pointCount > 0)
                {
                    RemovePoint(pointCount - 1);
                }

                if (pointCount <= 1) //没有点了, 执行回收
                {
                    ObjectPool.Reclaim(this);
                }
            }
        }
        else if (Target != null && GetPointCount() >= 2) //没有被回收, 更新第一个点
        {
            SetPointPosition(0, Target.GlobalPosition);
        }
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
    public void OnReclaim()
    {
        GetParent().CallDeferred(Node.MethodName.RemoveChild, this);
        Target = null;
        _targetPoolItem = null;
    }

    public void OnLeavePool()
    {
        ClearPoints();
        _time = 0;
        ZIndex = 0;
    }
}