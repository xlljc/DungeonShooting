using Godot;

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
    
    public override async void _Ready()
    {
        var sceneTreeTimer = GetTree().CreateTimer(DelayTime);
        await ToSignal(sceneTreeTimer, Timer.SignalName.Timeout);
        QueueFree();
    }
}