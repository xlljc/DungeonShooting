using Godot;

public partial class Effect1 : GpuParticles2D
{
    public override async void _Ready()
    {
        var c = GetNode<GpuParticles2D>("GPUParticles2D");
        c.Amount = Utils.RandomRangeInt(2, 6);
        c.Emitting = true;
        Emitting = true;

        var sceneTreeTimer = GetTree().CreateTimer(1f);
        await ToSignal(sceneTreeTimer, Timer.SignalName.Timeout);
        QueueFree();
    }
    
}
