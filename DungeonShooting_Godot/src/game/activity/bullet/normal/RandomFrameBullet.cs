
using Godot;

/// <summary>
/// 随机选择帧的子弹
/// </summary>
[Tool]
public partial class RandomFrameBullet : Bullet
{
    public override void OnInit()
    {
        base.OnInit();
        var animation = AnimatedSprite.Animation;
        var frameCount = AnimatedSprite.SpriteFrames.GetFrameCount(animation);

        AnimatedSprite.Frame = Utils.Random.RandomRangeInt(0, frameCount - 1);
    }
}