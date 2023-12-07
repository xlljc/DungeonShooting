

using Config;
using Godot;

/// <summary>
/// 液体画布上的像素点
/// </summary>
public class LiquidPixel
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
    /// <summary>
    /// 开始销退像素点的计时器
    /// </summary>
    public float Timer;
    /// <summary>
    /// 记录是否正在画布上显示
    /// </summary>
    public bool IsRun;
    /// <summary>
    /// 是否执行更新逻辑
    /// </summary>
    public bool IsUpdate;
    /// <summary>
    /// 上一次操作 Alpha的 时间, 用该时间和画布的 RunTime 相减可以计算出 delta
    /// </summary>
    public float TempTime;
    /// <summary>
    /// 用于补间操作记录该像素点是否已经被绘制过, 以便于优化性能
    /// </summary>
    public bool TempFlag;
}