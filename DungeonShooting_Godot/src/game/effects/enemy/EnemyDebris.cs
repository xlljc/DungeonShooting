
using System.Collections;
using Godot;

/// <summary>
/// 敌人死亡碎片
/// </summary>
[Tool]
public partial class EnemyDebris : ActivityObject
{

    private GpuParticles2D _gpuParticles2D;
    private bool _playOver = false;
    
    public override void OnInit()
    {
        var frameCount = AnimatedSprite.SpriteFrames.GetFrameCount(AnimatorNames.Default);
        AnimatedSprite.Frame = Utils.Random.RandomRangeInt(0, frameCount - 1);

        Throw(
            Utils.Random.RandomRangeInt(0, 16),
            Utils.Random.RandomRangeInt(10, 60),
            new Vector2(Utils.Random.RandomRangeInt(-25, 25), Utils.Random.RandomRangeInt(-25, 25)),
            Utils.Random.RandomRangeInt(-360, 360)
        );

        StartCoroutine(EmitParticles());
    }

    protected override void Process(float delta)
    {
        if (_playOver && !IsThrowing && Altitude <= 0 && MoveController.IsMotionless())
        {
            MoveController.SetAllVelocity(Vector2.Zero);
            Freeze();
        }
    }

    public IEnumerator EmitParticles()
    {
        var gpuParticles2D = GetNode<GpuParticles2D>("GPUParticles2D");
        gpuParticles2D.Emitting = true;
        yield return new WaitForSeconds(Utils.Random.RandomRangeFloat(1f, 2.5f));
        gpuParticles2D.Emitting = false;
        yield return new WaitForSeconds(1);
        _playOver = true;
    }
}