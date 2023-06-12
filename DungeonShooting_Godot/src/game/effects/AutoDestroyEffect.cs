
using Godot;

public partial class AutoDestroyEffect : GpuParticles2D
{
    /// <summary>
    /// 延时销毁时间
    /// </summary>
    [Export]
    public float DelayTime = 1f;
    
    public override async void _Ready()
    {
        Emitting = true;
        var sceneTreeTimer = GetTree().CreateTimer(DelayTime);
        await ToSignal(sceneTreeTimer, Timer.SignalName.Timeout);
        QueueFree();
    }
}