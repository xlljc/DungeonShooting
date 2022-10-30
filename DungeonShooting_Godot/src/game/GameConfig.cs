
using Godot;

public static class GameConfig
{
    /// <summary>
    /// 散射计算的默认距离
    /// </summary>
    public static readonly float ScatteringDistance = 300;
    /// <summary>
    /// 重力加速度
    /// </summary>
    public static readonly float G = 250f;
    /// <summary>
    /// 像素缩放
    /// </summary>
    public static readonly int WindowScale = 4;
    /// <summary>
    /// 游戏视图大小
    /// </summary>
    public static readonly Vector2 ViewportSize = new Vector2(480, 270);
}