using Godot;
using System;

public partial class TestMask2 : SubViewportContainer
{
    [Export]
    public Sprite2D Canvas;

    [Export]
    public Texture2D Brush1;
    [Export]
    public Texture2D Brush2;
    
    private Grid<byte> _grid = new Grid<byte>();

    private Image _brushImage1;
    private Image _brushImage2;
    private Image _image;
    private ImageTexture _texture;

    public override void _Ready()
    {
        _brushImage1 = Brush1.GetImage();
        _brushImage2 = Brush2.GetImage();
        _image = Image.Create(480, 270, false, Image.Format.Rgba8);
        _texture = ImageTexture.CreateFromImage(_image);
        Canvas.Texture = _texture;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("fire"))
        {
            var time = DateTime.Now;
            RunTest(_brushImage1);
            Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds);
        }
        else if (Input.IsActionPressed("roll"))
        {
            var time = DateTime.Now;
            RunTest(_brushImage2);
            Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds);
        }

        if (Input.IsActionJustPressed("meleeAttack"))
        {
            var time = DateTime.Now;
            var mousePosition = GetGlobalMousePosition();
            for (int i = 0; i < 10; i++)
            {
                var pixel = _image.GetPixel((int)mousePosition.X / 4 + i, (int)mousePosition.Y / 4);
            }
            Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds);
        }
        
        if (Input.IsActionJustPressed("exchangeWeapon"))
        {
            _image.Fill(new Color(1, 1, 1, 0));
            _texture.Update(_image);
        }
        
        
    }

    private void RunTest(Image brushImage)
    {
        //_image.BlitRect();
        _image.BlendRect(brushImage,
            new Rect2I(Vector2I.Zero, brushImage.GetWidth(), brushImage.GetHeight()),
            (GetGlobalMousePosition() / 4 - brushImage.GetSize() / 2).AsVector2I()
        );
        _texture.Update(_image);
    }
}
