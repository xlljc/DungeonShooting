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
    
    [Export]
    public Sprite2D Canvas;

    [Export]
    public Texture2D Brush1;
    [Export]
    public Texture2D Brush2;
    
    private Grid<byte> _grid = new Grid<byte>();

    private ImageData _brushData1;
    private ImageData _brushData2;
    private Image _image;
    private ImageTexture _texture;

    public override void _Ready()
    {
        _brushData1 = new ImageData(Brush1.GetImage());
        _brushData2 = new ImageData(Brush2.GetImage());
        _image = Image.Create(480, 270, false, Image.Format.Rgba8);
        _texture = ImageTexture.CreateFromImage(_image);
        Canvas.Texture = _texture;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("fire"))
        {
            var time = DateTime.Now;
            RunTest(_brushData1);
            Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds);
        }
        else if (Input.IsActionPressed("roll"))
        {
            var time = DateTime.Now;
            RunTest(_brushData2);
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
    }

    private void RunTest(ImageData brush)
    {
        var pos = (GetGlobalMousePosition() / 4 - new Vector2I(brush.Width, brush.Height) / 2).AsVector2I();
        var canvasWidth = _texture.GetWidth();
        var canvasHeight = _texture.GetHeight();
        foreach (var brushPixel in brush.Pixels)
        {
            var x = pos.X + brushPixel.X;
            var y = pos.Y + brushPixel.Y;
            if (x >= 0 && x < canvasWidth && y >= 0 && y < canvasHeight)
            {
                _image.SetPixel(x, y, brushPixel.Color);
            }
        }
        _texture.Update(_image);
    }
}
