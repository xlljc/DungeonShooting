using Godot;

public partial class TestOptimizeSprite : Node2D
{
    [Export()] public Texture2D[] ImageList;

    [Export()] public SubViewport SubViewport;

    [Export()] public Camera2D ViewCamera;

    public override void _Ready()
    {
        ImageCanvas.Init(SubViewport);
        ImageCanvas.MaxHandlerTime = 4;
        
        var scale = 10;
        var imageCanvas = new ImageCanvas(1920 / scale, 1080 / scale);
        imageCanvas.Scale = new Vector2(scale, scale);
        var delta = 360f / (15 * 8);
        var angle = 0f;
        
        
        
        // for (int i = 0; i < 15; i++)
        // {
        //     for (int j = 0; j < 8; j++)
        //     {
        //         //var texture = Utils.RandomChoose(ImageList);
        //         var texture = ImageList[6];
        //         var centerX = 0;
        //         var centerY = 0;
        //         //var angle = Utils.RandomRangeInt(0, 360);
        //         GD.Print($"x: {i}, y: {j}, angle: " + angle);
        //         imageCanvas.DrawImageInCanvas(texture,
        //             //Utils.RandomRangeInt(0, imageCanvas.Width), Utils.RandomRangeInt(0, imageCanvas.Height),
        //             10 + i * 10, 10 + j * 10,
        //             angle, centerX, centerY, false
        //         );
        //         angle += delta;
        //     }
        // }

        var texture = ImageList[0];
        imageCanvas.DrawImageInCanvas(texture, imageCanvas.Width / 2, imageCanvas.Height / 2, 45, 0, 0, false);
        //imageCanvas.DrawImageInCanvas(texture, imageCanvas.Width / 2, imageCanvas.Height / 2, 45, texture.GetWidth() - 1, texture.GetHeight() - 1, false);



        AddChild(imageCanvas);
    }
}
