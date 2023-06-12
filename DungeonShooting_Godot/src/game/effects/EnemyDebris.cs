
using System.Collections;
using Godot;

[Tool, GlobalClass]
public partial class EnemyDebris : ActivityObject
{

    private GpuParticles2D _gpuParticles2D;
    
    public override void OnInit()
    {
        var frameCount = AnimatedSprite.SpriteFrames.GetFrameCount(AnimatorNames.Default);
        AnimatedSprite.Frame = Utils.RandomRangeInt(0, frameCount - 1);

        Throw(
            Utils.RandomRangeInt(0, 16),
            Utils.RandomRangeInt(10, 60),
            new Vector2(Utils.RandomRangeInt(-25, 25), Utils.RandomRangeInt(-25, 25)),
            Utils.RandomRangeInt(-360, 360)
        );

        StartCoroutine(EmitParticles());
    }

    public IEnumerator EmitParticles()
    {
        var gpuParticles2D = GetNode<GpuParticles2D>("GPUParticles2D");
        gpuParticles2D.Emitting = true;
        yield return new WaitForSeconds(Utils.RandomRangeFloat(1f, 2.5f));
        gpuParticles2D.Emitting = false;
        yield return new WaitForSeconds(1);
        gpuParticles2D.QueueFree();
        EnableBehavior = false;
        Collision.QueueFree();
    }
}