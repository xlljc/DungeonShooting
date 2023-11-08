using Godot;
using Godot.Collections;

/// <summary>
/// 到期自动销毁的帧动画
/// </summary>
public partial class AutoDestroySprite : AnimatedSprite2D, IEffect
{
    /// <summary>
    /// 延时销毁时间
    /// </summary>
    [Export]
    public float DelayTime { get; set; } = 1f;
    
    /// <summary>
    /// 子节点包含的例子特效, 在创建完成后自动播放
    /// </summary>
    [Export]
    public Array<GpuParticles2D> Particles2D { get; set; }
    
    public bool IsDestroyed { get; private set; }
    public bool IsRecycled { get; set; }
    public string Logotype { get; set; }

    private double _timer;
    private bool _isPlay;
    
    public virtual void PlayEffect()
    {
        if (Particles2D != null)
        {
            foreach (var gpuParticles2D in Particles2D)
            {
                gpuParticles2D.Emitting = true;
            }
        }
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
            if (Particles2D != null)
            {
                foreach (var gpuParticles2D in Particles2D)
                {
                    gpuParticles2D.Emitting = false;
                }
            }
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