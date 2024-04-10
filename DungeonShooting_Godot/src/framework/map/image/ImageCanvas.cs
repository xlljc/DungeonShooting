
using System;
using Godot;

/// <summary>
/// 静态图像画布类, 用于处理游戏中大量静态物体的解决方案<br/>
/// 将物体纹理绘直接绘制到画布上, 这样大大减少GPU开销, 从而提高帧率<br/>
/// 图像旋转遵循完美像素
/// </summary>
public partial class ImageCanvas : Sprite2D, IDestroy
{
    /// <summary>
    /// 画布大小
    /// </summary>
    public int Width { get; }
    
    /// <summary>
    /// 画布高度
    /// </summary>
    public int Height { get; }

    public bool IsDestroyed { get; private set; }

    private Image _canvas;
    private ImageTexture _texture;

    public ImageCanvas(int width, int height)
    {
        Width = width;
        Height = height;

        _canvas = Image.Create(width, height, false, Image.Format.Rgba8);
        _texture = ImageTexture.CreateFromImage(_canvas);
    }

    public override void _Ready()
    {
        Centered = false;
        Texture = _texture;
        ZIndex = -1;
    }

    /// <summary>
    /// 将指定纹理添加到预渲染队列中, 完成后会调用 onDrawingComplete 回调函数
    /// </summary>
    /// <param name="texture">需要渲染的纹理</param>
    /// <param name="modulate">混色</param>
    /// <param name="material">渲染材质, 不需要则传null</param>
    /// <param name="x">离画布左上角x坐标</param>
    /// <param name="y">离画布左上角y坐标</param>
    /// <param name="angle">旋转角度, 角度制</param>
    /// <param name="centerX">旋转中心点x</param>
    /// <param name="centerY">旋转中心点y</param>
    /// <param name="flipY">是否翻转y轴</param>
    /// <param name="onDrawingComplete">绘制完成的回调函数</param>
    public void DrawImageInCanvas(Texture2D texture, Color modulate, Material material, float x, float y, float angle, int centerX, int centerY, bool flipY, Action onDrawingComplete = null)
    {
        DrawImageInCanvas(texture,modulate, material, x, y, angle, centerX, centerY, flipY, true, onDrawingComplete);
    }
    private void DrawImageInCanvas(Texture2D texture, Color modulate, Material material, float x, float y, float angle, int centerX, int centerY, bool flipY, bool enableQueueCutting, Action onDrawingComplete)
    {
        var item = new ImageRenderData();
        item.OnDrawingComplete = onDrawingComplete;
        item.EnableQueueCutting = enableQueueCutting;
        item.ImageCanvas = this;
        item.SrcImage = texture.GetImage();
        item.Modulate = modulate;
        item.Material = material;
        var width = item.SrcImage.GetWidth();
        var height = item.SrcImage.GetHeight();
        if (width > 128)
        {
            Debug.LogError("警告: 图像宽度大于 128, 旋转后像素点可能绘制到画布外导致像素丢失!");
        }
        if (height > 128)
        {
            Debug.LogError("警告: 图像高度大于 128, 旋转后像素点可能绘制到画布外导致像素丢失!");
        }
        item.X = Mathf.RoundToInt(x);
        item.Y = Mathf.RoundToInt(y);
        item.Rotation = Mathf.DegToRad(Mathf.RoundToInt(Utils.ConvertAngle(angle)));
        item.CenterX = centerX;
        item.CenterY = centerY;
        if (item.Rotation > Mathf.Pi)
        {
            item.CenterX = width - item.CenterX;
            item.CenterY = height - item.CenterY;
            item.Rotation -= Mathf.Pi;
            item.RotationGreaterThanPi = true;
        }
        item.FlipY = flipY;
        
        var cosAngle = Mathf.Cos(item.Rotation);
        var sinAngle = Mathf.Sin(item.Rotation);
        if (cosAngle == 0)
        {
            cosAngle = 1e-6f;
        }

        if (sinAngle == 0)
        {
            sinAngle = 1e-6f;
        }

        //旋转后的图片宽高
        item.RenderWidth = Mathf.CeilToInt(width * Mathf.Abs(cosAngle) + height * sinAngle) + 2;
        item.RenderHeight = Mathf.CeilToInt(width * sinAngle + height * Mathf.Abs(cosAngle)) + 2;

        if (item.RenderWidth >= RenderViewportSize.X)
        {
            Debug.LogError($"图像旋转后的宽度大于{RenderViewportSize.X}, 不支持绘制到 ImageCanvas 下!");
            item.SrcImage.Dispose();
            return;
        }
        
        //旋转后的图像中心点偏移
        item.RenderOffsetX =
            (int)((item.CenterX / sinAngle +
                   (0.5f * item.RenderWidth * sinAngle - 0.5f * item.RenderHeight * cosAngle +
                    0.5f * height) /
                   cosAngle -
                   (-0.5f * item.RenderWidth * cosAngle - 0.5f * item.RenderHeight * sinAngle +
                    0.5f * width) /
                   sinAngle - item.CenterY / cosAngle) /
                  (cosAngle / sinAngle + sinAngle / cosAngle)) + 1;
        item.RenderOffsetY =
            (int)((item.CenterX / cosAngle -
                      (-0.5f * item.RenderWidth * cosAngle - 0.5f * item.RenderHeight * sinAngle + 0.5f * width) /
                      cosAngle -
                      (0.5f * item.RenderWidth * sinAngle - 0.5f * item.RenderHeight * cosAngle + 0.5f * height) /
                      sinAngle + item.CenterY / sinAngle) /
                  (sinAngle / cosAngle + cosAngle / sinAngle)) + 1;

        _queueItems.Add(item);
    }
    
    /// <summary>
    /// 将指定 ActivityObject 添加到预渲染队列中, 完成后会调用 onDrawingComplete 回调函数
    /// </summary>
    /// <param name="activityObject">物体实例</param>
    /// <param name="x">离画布左上角x坐标</param>
    /// <param name="y">离画布左上角y坐标</param>
    /// <param name="onDrawingComplete">绘制完成的回调</param>
    public void DrawActivityObjectInCanvas(ActivityObject activityObject, float x, float y, Action onDrawingComplete = null)
    {
        //是否翻转y轴
        var flipY = activityObject.Scale.Y < 0;

        var animatedSprite = activityObject.AnimatedSprite;
        var animatedSpritePosition = animatedSprite.Position;
        var ax = x + animatedSpritePosition.X;
        var ay = y + animatedSpritePosition.X;

        //先绘制阴影
        var shadowSprite = activityObject.ShadowSprite;
        var shadowSpriteTexture = activityObject.ShadowSprite.Texture;
        if (shadowSpriteTexture != null)
        {
            var spriteOffset = shadowSprite.Offset;
            var centerX = (int)-spriteOffset.X;
            var centerY = (int)-spriteOffset.Y;
            var angle = Utils.ConvertAngle(shadowSprite.GlobalRotationDegrees);
            if (shadowSprite.Centered)
            {
                centerX += shadowSpriteTexture.GetWidth() / 2;
                centerY += shadowSpriteTexture.GetHeight() / 2;
            }
            
            DrawImageInCanvas(
                shadowSprite.Texture, shadowSprite.Modulate, shadowSprite.Material,
                ax + activityObject.ShadowOffset.X, ay + activityObject.ShadowOffset.Y,
                angle,
                centerX, centerY, flipY,
                true, null
            );
        }
        
        //再绘制纹理
        var texture = activityObject.GetCurrentTexture();
        if (texture != null)
        {
            var spriteOffset = animatedSprite.Offset;
            var centerX = (int)-spriteOffset.X;
            var centerY = (int)-spriteOffset.Y;
            if (animatedSprite.Centered)
            {
                centerX += texture.GetWidth() / 2;
                centerY += texture.GetHeight() / 2;
            }
            //为了保证阴影在此之前渲染, 所以必须关闭插队渲染
            DrawImageInCanvas(
                texture, animatedSprite.Modulate, animatedSprite.Material,
                ax, ay,
                animatedSprite.GlobalRotationDegrees,
                centerX, centerY, flipY,
                false, onDrawingComplete
            );
        }
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
        _canvas.Dispose();
        _texture.Dispose();
    }

    /// <summary>
    /// 使用透明色替换掉整个画布
    /// </summary>
    public void Clear()
    {
        Clear(new Color(0, 0, 0, 0));
    }

    /// <summary>
    /// 使用指定颜色替换掉整个画布
    /// </summary>
    public void Clear(Color color)
    {
        _canvas.Fill(color);
        Redraw();
    }
    
    private void Redraw()
    {
        _texture.Update(_canvas);
    }
}