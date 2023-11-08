using Godot;

public partial class Effect1 : AutoDestroyParticles
{
    private GpuParticles2D _particles2D;
    private bool _init;

    public override void PlayEffect()
    {
        if (!_init)
        {
            _particles2D = GetNode<GpuParticles2D>("GPUParticles2D");
            _init = true;
        }

        _particles2D.Amount = Utils.Random.RandomRangeInt(2, 6);
        _particles2D.Emitting = true;
        base.PlayEffect();
    }
}
