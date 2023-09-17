
using Godot;

/// <summary>
/// 特效管理器
/// </summary>
public static partial class SpecialEffectManager
{

    /// <summary>
    /// 基础特效播放类, 用于播放序列帧动画特效, 播完就回收
    /// </summary>
    private partial class SpecialEffect : AnimatedSprite2D
    {
        //记录循环次数
        public int LoopCount;

        private int currLoopCount = 0;
        public override void _Ready()
        {
            AnimationLooped += OnAnimationLooped;
        }

        //动画结束
        private void OnAnimationLooped()
        {
            currLoopCount++;
            if (currLoopCount >= LoopCount)
            {
                Over();
            }
        }

        private void Over()
        {
            currLoopCount = 0;
            QueueFree();
        }
    }

    /// <summary>
    /// 在场景指定位置播放一个特效, 特效必须是 SpriteFrames 类型
    /// </summary>
    /// <param name="root">挂载的根节点</param>
    /// <param name="path">特效SpriteFrames资源路径</param>
    /// <param name="animName">动画名称</param>
    /// <param name="pos">坐标</param>
    /// <param name="rotation">旋转角度, 弧度制</param>
    /// <param name="scale">缩放</param>
    /// <param name="offset">图像偏移</param>
    /// <param name="zIndex">层级</param>
    /// <param name="speed">播放速度</param>
    /// <param name="loopCount">循环次数, 到达该次数特效停止播放</param>
    public static void Play(Node root, string path, string animName, Vector2 pos, float rotation, Vector2 scale, Vector2 offset, int zIndex = 0, float speed = 1, int loopCount = 1)
    {
        var spriteFrames = ResourceManager.Load<SpriteFrames>(path);
        var specialEffect = new SpecialEffect();
        specialEffect.Position = pos;
        specialEffect.Rotation = rotation;
        specialEffect.Scale = scale;
        specialEffect.ZIndex = zIndex;
        specialEffect.Offset = offset;
        specialEffect.SpeedScale = speed;
        specialEffect.LoopCount = loopCount;
        specialEffect.SpriteFrames = spriteFrames;
        specialEffect.Play(animName);
        root.AddChild(specialEffect);
    }
}