
using Godot;

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
        
        var w = _canvas.GetWidth();
        var h = _canvas.GetHeight();
        for (int i = 0; i < w; i++)
        {
            _canvas.SetPixel(i, h / 2, Colors.Green);
        }
        
        for (int j = 0; j < h; j++)
        {
            _canvas.SetPixel(w / 2, j, Colors.Green);
        }
    }

    public override void _Ready()
    {
        Centered = false;
        Texture = _texture;
    }

    /// <summary>
    /// 添加到预渲染队列中
    /// </summary>
    /// <param name="texture">需要渲染的纹理</param>
    /// <param name="x">离画布左上角x坐标</param>
    /// <param name="y">离画布左上角y坐标</param>
    /// <param name="angle">旋转角度, 角度制</param>
    /// <param name="centerX">旋转中心点x</param>
    /// <param name="centerY">旋转中心点y</param>
    /// <param name="flipY">是否翻转y轴</param>
    public void DrawImageInCanvas(Texture2D texture, int x, int y, float angle, int centerX, int centerY, bool flipY)
    {
        var item = new ImageRenderData();
        item.ImageCanvas = this;
        item.SrcImage = texture.GetImage();
        item.X = x;
        item.Y = y;
        item.Rotation = Mathf.DegToRad(Mathf.RoundToInt(Utils.ConvertAngle(angle)));
        item.CenterX = centerX;
        item.CenterY = centerY;
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

        var width = item.SrcImage.GetWidth();
        var height = item.SrcImage.GetHeight();
        if (width > 128)
        {
            GD.PrintErr("警告: 图像宽度大于 128, 旋转后像素点可能绘制到画布外导致像素丢失!");
        }
        if (height > 128)
        {
            GD.PrintErr("警告: 图像高度大于 128, 旋转后像素点可能绘制到画布外导致像素丢失!");
        }
        //旋转后的图片宽高
        item.RenderWidth = Mathf.CeilToInt(width * Mathf.Abs(cosAngle) + height * sinAngle) + 2;
        item.RenderHeight = Mathf.CeilToInt(width * sinAngle + height * Mathf.Abs(cosAngle)) + 2;

        if (item.RenderWidth >= RenderViewportSize.X)
        {
            GD.PrintErr($"图像旋转后的宽度大于{RenderViewportSize.X}, 不支持绘制到 ImageCanvas 下!");
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

        _queueItems.Enqueue(item);
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

    private void Redraw()
    {
        _texture.Update(_canvas);
    }
}