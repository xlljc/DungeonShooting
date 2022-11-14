
using Godot;

/// <summary>
/// 特效管理器
/// </summary>
public static class SpecialEffectManager
{
    /// <summary>
    /// 在场景指定位置播放一个特效
    /// </summary>
    /// <param name="path">特效SpriteFrames资源路径</param>
    /// <param name="pos">坐标</param>
    /// <param name="rotation">旋转角度</param>
    /// <param name="scale">缩放</param>
    /// <param name="zIndex">层级</param>
    /// <param name="speed">播放速度</param>
    /// <param name="loopCount">循环次数, 到达该次数特效停止播放</param>
    public static void Play(string path, Vector2 pos, float rotation, Vector2 scale, int zIndex, float speed = 1, int loopCount = 1)
    {
        
    }
}