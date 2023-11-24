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
        
        public ImageData(Image image)
        {
            var list = new List<PixelData>();
            var width = image.GetWidth();
            var height = image.GetHeight();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        list.Add(new PixelData()
                        {
                            X = x,
                            Y = y,
                            Color = pixel
                        });
                    }
                }
            }

            Pixels = list.ToArray();
            Width = width;
            Height = height;
        }
    }
    
    public class PixelData
    {
        public int X;
        public int Y;
        public Color Color;
    }

    public class ImagePixel
    {
        public int X;
        public int Y;
        public Color Color;
        public byte Type;
        public float Speed;
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
    
    public override void _Ready()
    {
        _brushData1 = new ImageData(Brush1.GetImage());
        _brushData2 = new ImageData(Brush2.GetImage());
        _image = Image.Create(480, 270, false, Image.Format.Rgba8);
        _texture = ImageTexture.CreateFromImage(_image);
        Canvas.Texture = _texture;
        _imagePixels = new ImagePixel[480, 270];
    }

    public override void _Process(double delta)
    {
        var newDelta = (float)delta;
        var pos = (GetGlobalMousePosition() / 4).AsVector2I();
        if (Input.IsActionPressed("fire"))
        {
            var time = DateTime.Now;
            RunTest(_brushData1, pos, 4f);
            Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds);
        }
        else if (Input.IsActionPressed("roll"))
        {
            var time = DateTime.Now;
            RunTest(_brushData2, pos, 4f);
            Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds);
        }

        if (Input.IsActionJustPressed("meleeAttack"))
        {
            var mousePosition = GetGlobalMousePosition();
            var time = DateTime.Now;
            var pixel = _image.GetPixel((int)mousePosition.X / 4, (int)mousePosition.Y / 4);
            Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds + ", 是否碰撞: " + (pixel.A > 0));
        }
        
        if (Input.IsActionJustPressed("exchangeWeapon"))
        {
            _image.Fill(new Color(1, 1, 1, 0));
            _texture.Update(_image);
        }

        if (Input.IsActionPressed("move_left"))
        {
            var time = DateTime.Now;
            foreach (var imagePixel in _cacheImagePixels)
            {
                if (imagePixel.Color.A > 0)
                {
                    imagePixel.Color.A -= imagePixel.Speed * newDelta;
                    _image.SetPixel(imagePixel.X, imagePixel.Y, imagePixel.Color);
                }
            }
            Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds + ", 数量: " + _cacheImagePixels.Count);
        }
        _texture.Update(_image);
    }

    private void RunTest(ImageData brush, Vector2I position, float time)
    {
        var pos = position - new Vector2I(brush.Width, brush.Height) / 2;
        var canvasWidth = _texture.GetWidth();
        var canvasHeight = _texture.GetHeight();
        foreach (var brushPixel in brush.Pixels)
        {
            var x = pos.X + brushPixel.X;
            var y = pos.Y + brushPixel.Y;
            if (x >= 0 && x < canvasWidth && y >= 0 && y < canvasHeight)
            {
                _image.SetPixel(x, y, brushPixel.Color);
                var temp = _imagePixels[x, y];
                if (temp == null)
                {
                    temp = new ImagePixel()
                    {
                        X = x,
                        Y = y,
                        Color = brushPixel.Color,
                        Type = 0,
                        Speed = brushPixel.Color.A / time,
                    };
                    _imagePixels[x, y] = temp;
                    
                    _cacheImagePixels.Add(temp);
                }
                else
                {
                    temp.Color = brushPixel.Color;
                    temp.Speed = brushPixel.Color.A / time;
                }
            }
        }
    }
}
