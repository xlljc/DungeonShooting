using Godot;
using System;
using System.Collections.Generic;

public partial class TestMask2 : SubViewportContainer
{
    public class ImageData
    {
        public int Width;
        public int Height;
        public PixelData[] Pixels;

        //有效像素范围
        public int PixelMinX = int.MaxValue;
        public int PixelMinY = int .MaxValue;
        public int PixelMaxX;
        public int PixelMaxY;

        public int PixelWidth;
        public int PixelHeight;

        //补帧间距倍率
        public float Ffm;
        
        public ImageData(Image image, byte type, float ffm, float duration, float writeOffSpeed)
        {
            Ffm = ffm;
            var list = new List<PixelData>();
            var width = image.GetWidth();
            var height = image.GetHeight();
            var flag = false;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        flag = true;
                        list.Add(new PixelData()
                        {
                            X = x,
                            Y = y,
                            Color = pixel,
                            Type = type,
                            Duration = duration,
                            WriteOffSpeed = writeOffSpeed
                        });
                        if (x < PixelMinX)
                        {
                            PixelMinX = x;
                        }
                        else if (x > PixelMaxX)
                        {
                            PixelMaxX = x;
                        }
                        if (y < PixelMinY)
                        {
                            PixelMinY = y;
                        }
                        else if (y > PixelMaxY)
                        {
                            PixelMaxY = y;
                        }
                    }
                }
            }

            if (!flag)
            {
                throw new Exception("不能使用完全透明的图片作为笔刷!");
            }
            
            Pixels = list.ToArray();
            Width = width;
            Height = height;

            PixelWidth = PixelMaxX - PixelMinX;
            PixelHeight = PixelMaxY - PixelMinY;
        }
    }
    
    public class PixelData
    {
        public int X;
        public int Y;
        public Color Color;
        public byte Type;
        public float Duration;
        public float WriteOffSpeed;
    }

    public class ImagePixel
    {
        public int X;
        public int Y;
        public Color Color;
        public byte Type;
        public float Timer;
        public float Speed;
        public bool IsRun;
        public float TempTime;
        public bool TempFlag;
    }
    
    [Export]
    public Sprite2D Canvas;

    [Export]
    public Texture2D Brush1;
    [Export]
    public Texture2D Brush2;

    private ImageData _brushData1;
    private ImageData _brushData2;
    private Image _image;
    private ImageTexture _texture;

    private ImagePixel[,] _imagePixels;
    private List<ImagePixel> _cacheImagePixels = new List<ImagePixel>();
    private float _runTime = 0;
    private int _executeIndex = -1;
    private Vector2I? _prevPosition = null;
    private List<ImagePixel> _tempList = new List<ImagePixel>();

    //程序每帧最多等待执行时间, 超过这个时间的像素点将交到下一帧执行, 单位: 毫秒
    private float _maxWaitTime = 4f;
    
    public override void _Ready()
    {
        Engine.MaxFps = (int)DisplayServer.ScreenGetRefreshRate();
        //Engine.MaxFps = 5;
        _brushData1 = new ImageData(Brush1.GetImage(), 1, 0.5f, 5, 0.1f);
        _brushData2 = new ImageData(Brush2.GetImage(), 2, 0.8f, 5, 0.1f);
        _image = Image.Create(480, 270, false, Image.Format.Rgba8);
        _texture = ImageTexture.CreateFromImage(_image);
        Canvas.Texture = _texture;
        _imagePixels = new ImagePixel[480, 270];
        Debug.Log("width: : " + _brushData2.PixelWidth + ", height: " + _brushData2.PixelHeight);
    }

    public override void _Process(double delta)
    {
        //更新消除逻辑
        if (_cacheImagePixels.Count > 0)
        {
            var startIndex = _executeIndex;
            if (_executeIndex < 0 || _executeIndex >= _cacheImagePixels.Count)
            {
                _executeIndex = _cacheImagePixels.Count - 1;
            }

            var startTime = DateTime.UtcNow;
            var isOver = false;
            var index = 0;
            for (; _executeIndex >= 0; _executeIndex--)
            {
                index++;
                var imagePixel = _cacheImagePixels[_executeIndex];
                if (UpdateImagePixel(imagePixel)) //移除
                {
                    _cacheImagePixels.RemoveAt(_executeIndex);
                    if (_executeIndex < startIndex)
                    {
                        startIndex--;
                    }
                }

                if (index > 200)
                {
                    index = 0;
                    if ((DateTime.UtcNow - startTime).TotalMilliseconds > _maxWaitTime) //超过最大执行时间
                    {
                        isOver = true;
                        break;
                    }
                }
            }

            if (!isOver && startIndex >= 0 && _executeIndex < 0)
            {
                _executeIndex = _cacheImagePixels.Count - 1;
                for (; _executeIndex >= startIndex; _executeIndex--)
                {
                    index++;
                    var imagePixel = _cacheImagePixels[_executeIndex];
                    if (UpdateImagePixel(imagePixel)) //移除
                    {
                        _cacheImagePixels.RemoveAt(_executeIndex);
                    }
                    
                    if (index > 200)
                    {
                        index = 0;
                        if ((DateTime.UtcNow - startTime).TotalMilliseconds > _maxWaitTime) //超过最大执行时间
                        {
                            break;
                        }
                    }
                }
            }
        }
       
        var pos = (GetGlobalMousePosition() / 4).AsVector2I();
        if (Input.IsMouseButtonPressed(MouseButton.Left)) //绘制画笔1
        {
            var time = DateTime.UtcNow;
            if (_prevPosition != null)
            {
                DrawBrush(_brushData1, _prevPosition, pos, new Vector2(pos.X - _prevPosition.Value.X, pos.Y - _prevPosition.Value.Y).Angle());
            }
            else
            {
                DrawBrush(_brushData1, _prevPosition, pos, 0);
            }
            _prevPosition = pos;
            Debug.Log("用时: " + (DateTime.UtcNow - time).TotalMilliseconds);
        }
        else if (Input.IsMouseButtonPressed(MouseButton.Right))  //绘制画笔2
        {
            var time = DateTime.UtcNow;
            if (_prevPosition != null)
            {
                DrawBrush(_brushData2, _prevPosition, pos, new Vector2(pos.X - _prevPosition.Value.X, pos.Y - _prevPosition.Value.Y).Angle());
            }
            else
            {
                DrawBrush(_brushData2, _prevPosition, pos, 0);
            }
            _prevPosition = pos;
            Debug.Log("用时: " + (DateTime.UtcNow - time).TotalMilliseconds);
        }
        else
        {
            _prevPosition = null;
        }

        //碰撞检测
        if (Input.IsKeyPressed(Key.Space))
        {
            var mousePosition = GetGlobalMousePosition();
            var pixel = _image.GetPixel((int)mousePosition.X / 4, (int)mousePosition.Y / 4);
            Debug.Log("是否碰撞: " + (pixel.A > 0));
        }
        
        _texture.Update(_image);
        _runTime += (float)delta;
    }

    private bool UpdateImagePixel(ImagePixel imagePixel)
    {
        if (imagePixel.Color.A > 0)
        {
            if (imagePixel.Timer > 0)
            {
                imagePixel.Timer -= _runTime - imagePixel.TempTime;
                imagePixel.TempTime = _runTime;
            }
            else
            {
                imagePixel.Color.A -= imagePixel.Speed * (_runTime - imagePixel.TempTime);
                _image.SetPixel(imagePixel.X, imagePixel.Y, imagePixel.Color);
                if (imagePixel.Color.A <= 0)
                {
                    imagePixel.IsRun = false;
                    return true;
                }
                else
                {
                    imagePixel.TempTime = _runTime;
                }
            }
        }

        return false;
    }

    private void DrawBrush(ImageData brush, Vector2I? prevPosition, Vector2I position, float rotation)
    {
        var center = new Vector2I(brush.Width, brush.Height) / 2;
        var pos = position - center;
        var canvasWidth = _texture.GetWidth();
        var canvasHeight = _texture.GetHeight();
        //存在上一次记录的点
        if (prevPosition != null)
        {
            var offset = new Vector2(position.X - prevPosition.Value.X, position.Y - prevPosition.Value.Y);
            var maxL = Mathf.Lerp(
                brush.PixelHeight,
                brush.PixelWidth,
                Mathf.Abs(Mathf.Sin(offset.Angle() - rotation + Mathf.Pi * 0.5f))
            ) * brush.Ffm;
            var len = offset.Length();
            if (len > maxL) //距离太大了, 需要补间
            {
                Debug.Log($"距离太大了, 启用补间: len: {len}, maxL: {maxL}");
                var count = Mathf.CeilToInt(len / maxL);
                var step = new Vector2(offset.X / count, offset.Y / count);
                var prevPos = prevPosition.Value - center;
                
                for (var i = 1; i <= count; i++)
                {
                    foreach (var brushPixel in brush.Pixels)
                    {
                        var brushPos = RotatePixels(brushPixel.X, brushPixel.Y, center.X, center.Y, rotation);
                        var x = (int)(prevPos.X + step.X * i + brushPos.X);
                        var y = (int)(prevPos.Y + step.Y * i + brushPos.Y);
                        if (x >= 0 && x < canvasWidth && y >= 0 && y < canvasHeight)
                        {
                            var temp = SetPixelData(x, y, brushPixel);
                            if (!temp.TempFlag)
                            {
                                temp.TempFlag = true;
                                _tempList.Add(temp);
                            }
                        }
                    }
                }
                
                foreach (var brushPixel in brush.Pixels)
                {
                    var brushPos = RotatePixels(brushPixel.X, brushPixel.Y, center.X, center.Y, rotation);
                    var x = pos.X + brushPos.X;
                    var y = pos.Y + brushPos.Y;
                    if (x >= 0 && x < canvasWidth && y >= 0 && y < canvasHeight)
                    {
                        var temp = SetPixelData(x, y, brushPixel);
                        if (!temp.TempFlag)
                        {
                            temp.TempFlag = true;
                            _tempList.Add(temp);
                        }
                    }
                }

                foreach (var imagePixel in _tempList)
                {
                    _image.SetPixel(imagePixel.X, imagePixel.Y, imagePixel.Color);
                    imagePixel.TempFlag = false;
                }

                _tempList.Clear();
                return;
            }
        }

        foreach (var brushPixel in brush.Pixels)
        {
            var brushPos = RotatePixels(brushPixel.X, brushPixel.Y, center.X, center.Y, rotation);
            var x = pos.X + brushPos.X;
            var y = pos.Y + brushPos.Y;
            if (x >= 0 && x < canvasWidth && y >= 0 && y < canvasHeight)
            {
                var temp = SetPixelData(x, y, brushPixel);
                _image.SetPixel(x, y, temp.Color);
            }
        }
    }

    private ImagePixel SetPixelData(int x, int y, PixelData pixelData)
    {
        var temp = _imagePixels[x, y];
        if (temp == null)
        {
            temp = new ImagePixel()
            {
                X = x,
                Y = y,
                Color = pixelData.Color,
                Type = pixelData.Type,
                Timer = pixelData.Duration,
                Speed = pixelData.WriteOffSpeed,
            };
            _imagePixels[x, y] = temp;
                    
            _cacheImagePixels.Add(temp);
            temp.IsRun = true;
            temp.TempTime = _runTime;
        }
        else
        {
            if (temp.Type != pixelData.Type)
            {
                temp.Color = pixelData.Color;
                temp.Type = pixelData.Type;
            }
            else
            {
                var tempColor = pixelData.Color;
                temp.Color = new Color(tempColor.R, tempColor.G, tempColor.B, Mathf.Max(temp.Color.A, tempColor.A));
            }
            
            temp.Speed = pixelData.WriteOffSpeed;
            temp.Timer = pixelData.Duration;
            if (!temp.IsRun)
            {
                _cacheImagePixels.Add(temp);
                temp.IsRun = true;
                temp.TempTime = _runTime;
            }
        }

        return temp;
    }

    /// <summary>
    /// 根据 rotation 旋转像素点坐标, 并返回旋转后的坐标, rotation 为弧度制角度, 旋转中心点为 centerX, centerY
    /// </summary>
    private Vector2I RotatePixels(int x, int y, int centerX, int centerY, float rotation)
    {
        if (rotation == 0)
        {
            return new Vector2I(x, y);
        }
        
        x -= centerX;
        y -= centerY;
        var newX = Mathf.RoundToInt(x * Mathf.Cos(rotation) - y * Mathf.Sin(rotation));
        var newY = Mathf.RoundToInt(x * Mathf.Sin(rotation) + y * Mathf.Cos(rotation));
        newX += centerX;
        newY += centerY;
        return new Vector2I(newX, newY);
    }
}
