using System;
using Godot;

public partial class TestOptimizeSprite : Node2D
{
    [Export()] public Texture2D Texture2D;

    public override void _Ready()
    {
        var imageCanvas = new ImageCanvas(200, 200);
        imageCanvas.Scale = new Vector2(4, 4);
        imageCanvas.DrawImageInCanvas(Texture2D, 10, 30, 0, 0, 0, false);
        var time = DateTime.Now;
        //imageCanvas.DrawImageInCanvas(Texture2D, 50, 30, 30, 0, 0, true);
        GD.Print("useTime: " + (DateTime.Now - time).TotalMilliseconds);
        var time2 = DateTime.Now;
        //imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 0, 0, 0, false);
        //imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 90, Texture2D.GetWidth() / 2, Texture2D.GetHeight() / 2, false);
        imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 300, 0, 0, false);
        //imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 145, (int)(Texture2D.GetWidth() * 0.2f), (int)(Texture2D.GetHeight() * 0.2f), false);
        //imageCanvas.DrawImageInCanvas(Texture2D, 140, 30, 270, 0, 0, true);

        GD.Print("useTime: " + (DateTime.Now - time2).TotalMilliseconds);
        AddChild(imageCanvas);
    }

    private void Test1()
    {
        var canvas = Image.Create(150, 150, false, Image.Format.Rgba8);

        for (int x = 0; x < 150; x++)
        {
            for (int y = 0; y < 150; y++)
            {
                canvas.SetPixel(x, y, Colors.Gray);
            }
        }

        var image = Texture2D.GetImage();
        image.FlipX();
        image.Rotate180();
        canvas.BlitRect(image, new Rect2I(0, 0, image.GetWidth(), image.GetHeight()), new Vector2I(10, 10));
        //RotateImage(image, canvas, 50, 50, 30, image.GetWidth(), image.GetHeight());

        //RotateImage(imgData, image, 0);

        for (int i = 0; i < canvas.GetWidth(); i++)
        {
            canvas.SetPixel(i, 50, Colors.Black);
            canvas.SetPixel(i, 50 + image.GetHeight() / 2, Colors.Green);
        }

        for (int i = 0; i < canvas.GetHeight(); i++)
        {
            canvas.SetPixel(50, i, Colors.Black);
            canvas.SetPixel(50 + image.GetWidth() / 2, i, Colors.Green);
        }

        var imageTexture = ImageTexture.CreateFromImage(canvas);
        var sprite2D = new Sprite2D();
        sprite2D.Texture = imageTexture;
        sprite2D.Position = new Vector2(900, 500);
        sprite2D.Scale = new Vector2(5, 5);
        AddChild(sprite2D);
    }
}
