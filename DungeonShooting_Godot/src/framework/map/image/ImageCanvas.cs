
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

        //_canvas.Fill(Colors.Gray);
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

        _enqueueItems.Enqueue(item);
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

    private void HandlerDrawImageInCanvas(ImageRenderData item)
    {
        
    }
}