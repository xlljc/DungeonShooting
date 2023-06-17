
using System;
using System.Collections.Generic;
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

        _canvas.Fill(Colors.Gray);
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
        var item = new EnqueueItem();
        item.Canvas = this;
        item.Image = texture.GetImage();
        item.X = x;
        item.Y = y;
        item.Angle = angle;
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
    }

    private void Redraw()
    {
        _texture.Update(_canvas);
    }

    private void HandlerDrawImageInCanvas(EnqueueItem item)
    {
        var image = item.Image;
        var newAngle = Mathf.RoundToInt(Utils.ConvertAngle(item.Angle));

        if (newAngle == 0) //原图, 直接画上去
        {
            if (item.FlipY)
            {
                image.FlipY();
            }

            _canvas.BlendRect(image, new Rect2I(0, 0, image.GetWidth(), image.GetHeight()),
                new Vector2I(item.X - item.CenterX, item.Y - item.CenterY));
        }
        else //其他角度
        {
            if (newAngle <= 180)
            {
                if (item.FlipY)
                {
                    image.FlipY();
                }

                DrawRotateImage(image, item.X, item.Y, newAngle, item.CenterX, item.CenterY);
            }
            else
            {
                image.FlipX();
                if (!item.FlipY)
                {
                    image.FlipY();
                }

                DrawRotateImage(
                    image, item.X, item.Y,
                    newAngle - 180,
                    image.GetWidth() - item.CenterX - 1, image.GetHeight() - item.CenterY - 1
                );
            }
        }

        image.Dispose();
        item.Image = null;
    }

    private void DrawRotateImage(Image origin, int x, int y, int angle, int centerX, int centerY)
    {
        var rotation = Mathf.DegToRad(angle);
        var width = origin.GetWidth();
        var height = origin.GetHeight();

        var cosAngle = Mathf.Cos(rotation);
        var sinAngle = Mathf.Sin(rotation);
        if (cosAngle == 0)
        {
            cosAngle = 1e-6f;
        }

        if (sinAngle == 0)
        {
            sinAngle = 1e-6f;
        }

        var newWidth = Mathf.RoundToInt(width * Mathf.Abs(cosAngle) + height * sinAngle);
        var newHeight = Mathf.RoundToInt(width * sinAngle + height * Mathf.Abs(cosAngle));

        var offsetX =
            Mathf.RoundToInt((centerX / sinAngle +
                              (0.5f * newWidth * sinAngle - 0.5f * newHeight * cosAngle + 0.5f * height) /
                              cosAngle -
                              (-0.5f * newWidth * cosAngle - 0.5f * newHeight * sinAngle + 0.5f * width) /
                              sinAngle - centerY / cosAngle) /
                             (cosAngle / sinAngle + sinAngle / cosAngle));


        var offsetY =
            Mathf.RoundToInt((centerX / cosAngle -
                                 (-0.5f * newWidth * cosAngle - 0.5f * newHeight * sinAngle + 0.5f * width) /
                                 cosAngle -
                                 (0.5f * newWidth * sinAngle - 0.5f * newHeight * cosAngle + 0.5f * height) /
                                 sinAngle + centerY / sinAngle) /
                             (sinAngle / cosAngle + cosAngle / sinAngle));

        var num1 = -0.5f * newWidth * cosAngle - 0.5f * newHeight * sinAngle + 0.5f * width;
        var num2 = 0.5f * newWidth * sinAngle - 0.5f * newHeight * cosAngle + 0.5f * height;

        var cw = _canvas.GetWidth();
        var ch = _canvas.GetHeight();
        for (var x2 = 0; x2 < newWidth; x2++)
        {
            for (var y2 = 0; y2 < newHeight; y2++)
            {
                var x1 = Mathf.RoundToInt(x2 * cosAngle + y2 * sinAngle + num1);
                var y1 = Mathf.RoundToInt(-x2 * sinAngle + y2 * cosAngle + num2);

                if (x1 < 0 || x1 >= width || y1 < 0 || y1 >= height)
                {
                    //在图片外
                    continue;
                }

                var cx = x2 + x - offsetX;
                if (cx < 0 || cx >= cw)
                {
                    continue;
                }

                var cy = y2 + y - offsetY;
                if (cy < 0 || cy >= ch)
                {
                    continue;
                }

                _canvas.SetPixel(cx, cy, _canvas.GetPixel(cx, cy).Blend(origin.GetPixel(x1, y1)));
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------
    private class EnqueueItem
    {
        public ImageCanvas Canvas;
        public Image Image;
        public int X;
        public int Y;
        public float Angle;
        public int CenterX;
        public int CenterY;
        public bool FlipY;
    }

    private static readonly Queue<EnqueueItem> _enqueueItems = new Queue<EnqueueItem>();
    private static readonly HashSet<ImageCanvas> _redrawCanvas = new HashSet<ImageCanvas>();

    /// <summary>
    /// 根据重绘队列更新画布
    /// </summary>
    public static void UpdateImageCanvas(float delta)
    {
        if (_enqueueItems.Count > 0)
        {
            _redrawCanvas.Clear();
            var startTime = DateTime.Now;

            var c = _enqueueItems.Count;
            //执行绘制操作
            //绘制的总时间不能超过2毫秒, 如果超过了, 那就下一帧再画其它的吧
            do
            {
                var item = _enqueueItems.Dequeue();
                if (!item.Canvas.IsDestroyed)
                {
                    item.Canvas.HandlerDrawImageInCanvas(item);
                    _redrawCanvas.Add(item.Canvas);
                }
            } while (_enqueueItems.Count > 0 && (DateTime.Now - startTime).TotalMilliseconds < 2);

            if (_enqueueItems.Count > 0)
            {
                GD.Print($"ImageCanvas: 还剩下{_enqueueItems.Count}个, 已经处理了{c - _enqueueItems.Count}个, 重绘画布{_redrawCanvas.Count}张");
            }
            else
            {
                GD.Print($"ImageCanvas: 全部画完, 已经处理了{c - _enqueueItems.Count}个, 重绘画布{_redrawCanvas.Count}张");
            }

            //重绘画布
            foreach (var drawCanvas in _redrawCanvas)
            {
                drawCanvas.Redraw();
            }

            _redrawCanvas.Clear();
        }
    }
}