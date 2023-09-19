using Godot;
using Godot.Collections;

/// <summary>
/// 到期自动销毁的帧动画
/// </summary>
public partial class AutoDestroySprite : AnimatedSprite2D
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
    
    public override async void _Ready()
    {
        var sceneTreeTimer = GetTree().CreateTimer(DelayTime);
        if (Particles2D != null)
        {
            foreach (var gpuParticles2D in Particles2D)
            {
                gpuParticles2D.Emitting = true;
            }
        }
        await ToSignal(sceneTreeTimer, Timer.SignalName.Timeout);
        QueueFree();
    }
}