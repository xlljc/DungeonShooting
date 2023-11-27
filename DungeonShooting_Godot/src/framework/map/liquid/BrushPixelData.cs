using Godot;

/// <summary>
/// 用于记录笔刷上的像素点
/// </summary>
public class BrushPixelData
{
    /// <summary>
    /// x 坐标
    /// </summary>
    public int X;
    /// <summary>
    /// y 坐标
    /// </summary>
    public int Y;
    /// <summary>
    /// 像素颜色
    /// </summary>
    public Color Color;
    /// <summary>
    /// 材质类型
    /// </summary>
    public byte Type;
    /// <summary>
    /// 开始消退时间
    /// </summary>
    public float Duration;
    /// <summary>
    /// 消退速度, 也就是 Alpha 值每秒变化的速度
    /// </summary>
    public float WriteOffSpeed;
}