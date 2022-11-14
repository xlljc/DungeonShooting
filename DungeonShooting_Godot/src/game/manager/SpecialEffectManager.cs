
using System.Collections.Generic;
using Godot;

/// <summary>
/// 特效管理器
/// </summary>
public static class SpecialEffectManager
{

    private static Stack<SpecialEffect> _specialEffectStack = new Stack<SpecialEffect>();

    /// <summary>
    /// 基础特效播放类, 用于播放序列帧动画特效, 播完就回收
    /// </summary>
    private class SpecialEffect : AnimatedSprite
    {
        //记录循环次数
        public int LoopCount;

        private int currLoopCount = 0;
        public override void _Ready()
        {
            Connect("animation_finished", this, nameof(OnAnimationFinished));
        }

        //动画结束
        private void OnAnimationFinished()
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
            RecycleSpecialEffect(this);
        }
    }

    /// <summary>
    /// 在场景指定位置播放一个特效
    /// </summary>
    /// <param name="path">特效SpriteFrames资源路径</param>
    /// <param name="animName">动画名称</param>
    /// <param name="pos">坐标</param>
    /// <param name="rotation">旋转角度, 弧度制</param>
    /// <param name="scale">缩放</param>
    /// <param name="offset">图像偏移</param>
    /// <param name="zIndex">层级</param>
    /// <param name="speed">播放速度</param>
    /// <param name="loopCount">循环次数, 到达该次数特效停止播放</param>
    public static void Play(string path, string animName, Vector2 pos, float rotation, Vector2 scale, Vector2 offset, int zIndex, float speed = 1, int loopCount = 1)
    {
        var spriteFrames = ResourceManager.Load<SpriteFrames>(path);
        
        var specialEffect = GetSpecialEffect();
        specialEffect.GlobalPosition = pos;
        specialEffect.Rotation = rotation;
        specialEffect.Scale = scale;
        specialEffect.ZIndex = zIndex;
        specialEffect.Offset = offset;
        specialEffect.SpeedScale = speed;
        specialEffect.LoopCount = loopCount;
        specialEffect.Frames = spriteFrames;
        specialEffect.Play(animName);
        GameApplication.Instance.Room.GetRoot(true).AddChild(specialEffect);
    }

    private static SpecialEffect GetSpecialEffect()
    {
        if (_specialEffectStack.Count > 0)
        {
            return _specialEffectStack.Pop();
        }

        return new SpecialEffect();
    }
    
    /// <summary>
    /// 回收2D音频播放节点
    /// </summary>
    private static void RecycleSpecialEffect(SpecialEffect inst)
    {
        var parent = inst.GetParent();
        if (parent != null)
        {
            parent.RemoveChild(inst);
        }

        inst.Playing = false;
        inst.Frames = null;
        _specialEffectStack.Push(inst);
    }
}