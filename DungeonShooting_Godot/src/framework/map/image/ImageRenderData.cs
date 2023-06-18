

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
    public int X;
    public int Y;
    public float Rotation;
    public int CenterX;
    public int CenterY;
    public bool FlipY;
    
    //----------------------------------------
    public ImageRenderSprite RenderSprite;
    public int RenderWidth;
    public int RenderHeight;
    
    public int RenderOffsetX;
    public int RenderOffsetY;
}