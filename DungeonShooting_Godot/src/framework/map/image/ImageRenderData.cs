

using System;
using Godot;

public class ImageRenderData
{
    /// <summary>
    /// 指定的画布
    /// </summary>
    public ImageCanvas ImageCanvas;
    /// <summary>
    /// 需要绘制的原图
    /// </summary>
    public Image SrcImage;
    /// <summary>
    /// 渲染材质
    /// </summary>
    public Material Material;
    /// <summary>
    /// x坐标
    /// </summary>
    public int X;
    /// <summary>
    /// y坐标
    /// </summary>
    public int Y;
    /// <summary>
    /// 旋转角度, 弧度制
    /// </summary>
    public float Rotation;
    /// <summary>
    /// 中心点x
    /// </summary>
    public int CenterX;
    /// <summary>
    /// 中心点y
    /// </summary>
    public int CenterY;
    /// <summary>
    /// 是否翻转y轴
    /// </summary>
    public bool FlipY;
    /// <summary>
    /// 旋转角度是否大于180度
    /// </summary>
    public bool RotationGreaterThanPi = false;
    /// <summary>
    /// 绘制完成的回调函数
    /// </summary>
    public Action OnDrawingComplete;
    
    //----------------------------------------------------------------------
    /// <summary>
    /// 将 SrcImage 渲染到 viewport 上的 sprite 对象
    /// </summary>
    public ImageRenderSprite RenderSprite;
    /// <summary>
    /// 在 viewport 上的宽度
    /// </summary>
    public int RenderWidth;
    /// <summary>
    /// 在 viewport 上的高度
    /// </summary>
    public int RenderHeight;
    /// <summary>
    /// 渲染在 viewport 上的x坐标
    /// </summary>
    public int RenderX;
    /// <summary>
    /// 渲染在 viewport 上的y坐标
    /// </summary>
    public int RenderY;
    /// <summary>
    /// 渲染在 viewport 上中心点x轴偏移量
    /// </summary>
    public int RenderOffsetX;
    /// <summary>
    /// 渲染在 viewport 上中心点y轴偏移量
    /// </summary>
    public int RenderOffsetY;
    public ImageCanvas.AreaPlaceholder AreaPlaceholder;
}