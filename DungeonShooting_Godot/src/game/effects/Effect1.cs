using Godot;

public partial class Effect1 : AutoDestroyEffect
{
    public override void _Ready()
    {
        var c = GetNode<GpuParticles2D>("GPUParticles2D");
        c.Amount = Utils.Random.RandomRangeInt(2, 6);
        c.Emitting = true;

        base._Ready();
    }
    
}
