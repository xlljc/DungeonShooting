using Godot;

public partial class TestOptimizeSprite : Node2D
{
    [Export()] public Texture2D Texture2D;

    public override void _Ready()
    {
        var imageCanvas = new ImageCanvas(1920, 1080);
        imageCanvas.Scale = new Vector2(4, 4);
        //imageCanvas.DrawImageInCanvas(Texture2D, 10, 30, 0, 0, 0, false);
        //var time = DateTime.Now;
        //imageCanvas.DrawImageInCanvas(Texture2D, 50, 30, 30, 0, 0, true);
        // GD.Print("useTime: " + (DateTime.Now - time).TotalMilliseconds);
        // var time2 = DateTime.Now;
        // //imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 0, 0, 0, false);
        // //imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 90, Texture2D.GetWidth() / 2, Texture2D.GetHeight() / 2, false);
        // imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 270, 0, 0, false);
        // //imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 90, 0, 0, false);
        // //imageCanvas.DrawImageInCanvas(Texture2D, 100, 100, 145, (int)(Texture2D.GetWidth() * 0.2f), (int)(Texture2D.GetHeight() * 0.2f), false);
        // //imageCanvas.DrawImageInCanvas(Texture2D, 140, 30, 270, 0, 0, true);
        //
        // GD.Print("useTime: " + (DateTime.Now - time2).TotalMilliseconds);

        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                var centerX = Utils.RandomRangeInt(0, Texture2D.GetWidth() - 1);
                var centerY = Utils.RandomRangeInt(0, Texture2D.GetHeight() - 1);
                imageCanvas.DrawImageInCanvas(Texture2D, 30 + (i + 1) * 10, 30 + (j + 1) * 10, Utils.RandomRangeInt(0, 360), centerX, centerY, Utils.RandomBoolean());
            }
        }
        
        AddChild(imageCanvas);
    }


    public override void _Process(double delta)
    {
        ImageCanvas.UpdateImageCanvas((float)delta);
    }
}
