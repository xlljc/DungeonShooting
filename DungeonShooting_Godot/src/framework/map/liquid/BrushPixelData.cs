using Config;
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
    /// 材质液体材质
    /// </summary>
    public ExcelConfig.LiquidMaterial Material;
}