
using Godot;

/// <summary>
/// 到期自动销毁的粒子特效
/// </summary>
public partial class AutoDestroyParticles : GpuParticles2D, IEffect
{
    /// <summary>
    /// 延时销毁时间
    /// </summary>
    [Export]
    public float DelayTime { get; set; } = 1f;
    
    public bool IsDestroyed { get; private set; }
    public bool IsRecycled { get; set; }
    public string Logotype { get; set; }

    private double _timer;
    private bool _isPlay;
    
    public virtual void PlayEffect()
    {
        Emitting = true;
        _timer = 0;
        _isPlay = true;
    }

    public override void _Process(double delta)
    {
        if (!_isPlay)
        {
            return;
        }
        _timer += delta;
        if (_timer >= DelayTime)
        {
            Emitting = false;
            _isPlay = false;
            ObjectPool.Reclaim(this);
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
        
    }
}