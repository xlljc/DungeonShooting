
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 液体画布
/// </summary>
public partial class LiquidCanvas : Sprite2D, IDestroy
{
    /// <summary>
    /// 程序每帧最多等待执行时间, 超过这个时间的像素点将交到下一帧执行, 单位: 毫秒
    /// </summary>
    public static float MaxWaitTime { get; set; } = 4f;

    /// <summary>
    /// 画布缩放
    /// </summary>
    public static int CanvasScale { get; } = 4;
    
    public bool IsDestroyed { get; private set; }
    
    private Image _image;
    private ImageTexture _texture;
    
    //画布上的像素点
    private LiquidPixel[,] _imagePixels;
    private List<LiquidPixel> _cacheImagePixels = new List<LiquidPixel>();
    //画布已经运行的时间
    private float _runTime = 0;
    private int _executeIndex = -1;
    //用于记录补间操作下有变动的像素点
    private List<LiquidPixel> _tempList = new List<LiquidPixel>();
    //记录是否有像素点发生变动
    private bool _changeFlag = false;
    //所属房间
    private RoomInfo _roomInfo;

    public LiquidCanvas(RoomInfo roomInfo, int width, int height)
    {
        _roomInfo = roomInfo;
        Centered = false;
        Material = ResourceManager.Load<Material>(ResourcePath.resource_material_Sawtooth_tres);
        
        _image = Image.Create(width, height, false, Image.Format.Rgba8);
        _texture = ImageTexture.CreateFromImage(_image);
        Texture = _texture;
        _imagePixels = new LiquidPixel[width, height];
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }
        
        IsDestroyed = true;
        QueueFree();
        _texture.Dispose();
        _image.Dispose();
    }

    public override void _Process(double delta)
    {
        //这里待优化, 应该每次绘制都将像素点放入 _tempList 中, 然后帧结束再统一提交

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
                    if ((DateTime.UtcNow - startTime).TotalMilliseconds > MaxWaitTime) //超过最大执行时间
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
                        if ((DateTime.UtcNow - startTime).TotalMilliseconds > MaxWaitTime) //超过最大执行时间
                        {
                            break;
                        }
                    }
                }
            }
        }

        if (_changeFlag)
        {
            _texture.Update(_image);
            _changeFlag = false;
        }

        _runTime += (float)delta;
    }

    /// <summary>
    /// 将画布外的坐标转为画布内的坐标
    /// </summary>
    public Vector2I ToLiquidCanvasPosition(Vector2 position)
    {
        return (_roomInfo.ToCanvasPosition(position) / CanvasScale).AsVector2I();
    }
    
    /// <summary>
    /// 根据画笔数据在画布上绘制液体, 转换坐标请调用 ToLiquidCanvasPosition() 函数
    /// </summary>
    /// <param name="brush">画笔数据</param>
    /// <param name="prevPosition">上一帧坐标, 相对于画布坐标, 改参数用于两点距离较大时执行补间操作, 如果传 null, 则不会进行补间</param>
    /// <param name="position">绘制坐标, 相对于画布坐标</param>
    /// <param name="rotation">旋转角度, 弧度制</param>
    public void DrawBrush(BrushImageData brush, Vector2I? prevPosition, Vector2I position, float rotation)
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
                    _changeFlag = true;
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
                _changeFlag = true;
                var temp = SetPixelData(x, y, brushPixel);
                _image.SetPixel(x, y, temp.Color);
            }
        }
    }

    /// <summary>
    /// 返回是否碰到任何有效像素点
    /// </summary>
    public bool Collision(int x, int y)
    {
        if (x >= 0 && x < _imagePixels.GetLength(0) && y >= 0 && y < _imagePixels.GetLength(1))
        {
            var result = _imagePixels[x, y];
            if (result != null && result.IsRun)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 返回碰撞到的有效像素点数据
    /// </summary>
    public LiquidPixel GetPixelData(int x, int y)
    {
        if (x >= 0 && x < _imagePixels.GetLength(0) && y >= 0 && y < _imagePixels.GetLength(1))
        {
            var result = _imagePixels[x, y];
            if (result != null && result.IsRun)
            {
                return result;
            }
        }

        return null;
    }
    
    /// <summary>
    /// 更新像素点数据逻辑
    /// </summary>
    private bool UpdateImagePixel(LiquidPixel imagePixel)
    {
        if (imagePixel.Color.A > 0)
        {
            if (imagePixel.Timer > 0) //继续等待消散
            {
                imagePixel.Timer -= _runTime - imagePixel.TempTime;
                imagePixel.TempTime = _runTime;
            }
            else
            {
                imagePixel.Color.A -= imagePixel.WriteOffSpeed * (_runTime - imagePixel.TempTime);
                
                if (imagePixel.Color.A <= 0) //完全透明了
                {
                    _changeFlag = true;
                    _image.SetPixel(imagePixel.X, imagePixel.Y, new Color(0, 0, 0, 0));
                    imagePixel.IsRun = false;
                    return true;
                }
                else
                {
                    _changeFlag = true;
                    _image.SetPixel(imagePixel.X, imagePixel.Y, imagePixel.Color);
                    imagePixel.TempTime = _runTime;
                }
            }
        }

        return false;
    }
    
    //记录像素点数据
    private LiquidPixel SetPixelData(int x, int y, BrushPixelData pixelData)
    {
        var temp = _imagePixels[x, y];
        if (temp == null)
        {
            temp = new LiquidPixel()
            {
                X = x,
                Y = y,
                Color = pixelData.Color,
                Type = pixelData.Type,
                Timer = pixelData.Duration,
                WriteOffSpeed = pixelData.WriteOffSpeed,
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
            
            temp.WriteOffSpeed = pixelData.WriteOffSpeed;
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
        var sv = Mathf.Sin(rotation);
        var cv = Mathf.Cos(rotation);
        var newX = Mathf.RoundToInt(x * cv - y * sv);
        var newY = Mathf.RoundToInt(x * sv + y * cv);
        newX += centerX;
        newY += centerY;
        return new Vector2I(newX, newY);
    }
}