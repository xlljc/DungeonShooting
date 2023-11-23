
using Godot;

/// <summary>
/// 拖影精灵
/// </summary>
public partial class SmearingSprite : Sprite2D, IPoolItem
{
    public bool IsRecycled { get; set; }
    public string Logotype { get; set; }
    public bool IsDestroyed { get; set; }

    private double _timeOut = -1;
    private double _totalTime = 0;

    /// <summary>
    /// 从 ActivityObject 的 AnimatedSprite 中复制动画帧
    /// </summary>
    public void FromActivityObject(ActivityObject activityObject)
    {
        var currentTexture = activityObject.GetCurrentTexture();
        Texture = currentTexture;
        Offset = activityObject.AnimatedSprite.Offset;
        GlobalPosition = activityObject.AnimatedSprite.GlobalPosition;
        GlobalScale = activityObject.AnimatedSprite.GlobalScale;
        GlobalRotation = activityObject.AnimatedSprite.GlobalRotation;
    }

    /// <summary>
    /// 设置显示的时间, 过期会自动回收
    /// </summary>
    public void SetShowTimeout(float time)
    {
        _totalTime = time;
        _timeOut = time;
    }

    public override void _Process(double delta)
    {
        if (_timeOut > 0)
        {
            _timeOut -= delta;
            Modulate = new Color(1, 1, 1, (float)(_timeOut / _totalTime));
            if (_timeOut <= 0)
            {
                ObjectPool.Reclaim(this);
            }
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
        GetParent().RemoveChild(this);
    }

    public void OnLeavePool()
    {
        Modulate = Colors.White;
    }
}