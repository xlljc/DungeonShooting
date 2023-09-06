using Godot;

public partial class TestOptimizeSprite : Node2D
{
    [Export()] public Texture2D[] ImageList;

    [Export()] public Label Label;
    
    public override void _Ready()
    {
        ImageCanvas.Init(GetTree().CurrentScene);
        ImageCanvas.MaxHandlerTime = 4;
        
        var scale = 2;
        var imageCanvas = new ImageCanvas(1920 / scale, 1080 / scale);
        imageCanvas.Scale = new Vector2(scale, scale);

        var successCount = 0;
        
        for (int i = 0; i < 50000; i++)
        {
            var texture = Utils.Random.RandomChoose(ImageList);
            var x = Utils.Random.RandomRangeInt(0, imageCanvas.Width);
            var y = Utils.Random.RandomRangeInt(0, imageCanvas.Height);
            var centerX = Utils.Random.RandomRangeInt(0, texture.GetWidth());
            var centerY = Utils.Random.RandomRangeInt(0, texture.GetHeight());
            var angle = Utils.Random.RandomRangeInt(0, 360);
            imageCanvas.DrawImageInCanvas(texture, null, x, y,
                angle, centerX, centerY, Utils.Random.RandomBoolean(),
                () =>
                {
                    successCount++;
                    Label.Text = $"当前绘制数量: {successCount}";
                }
            );
        }
        
        // var delta = 360f / (15 * 8);
        // var angle = 0f;
        // for (int i = 0; i < 15; i++)
        // {
        //     for (int j = 0; j < 8; j++)
        //     {
        //         //var texture = Utils.RandomChoose(ImageList);
        //         var texture = ImageList[1];
        //         var centerX = texture.GetWidth() / 2;
        //         var centerY = texture.GetHeight() / 2;
        //         //var angle = Utils.RandomRangeInt(0, 360);
        //         //GD.Print($"x: {i}, y: {j}, angle: " + angle);
        //         imageCanvas.DrawImageInCanvas(texture,
        //             10 + i * 10, 10 + j * 10,
        //             angle, centerX, centerY, false
        //         );
        //         angle += delta;
        //     }
        // }

        //var texture = ImageList[0];
        //imageCanvas.DrawImageInCanvas(texture, imageCanvas.Width / 2, imageCanvas.Height / 2, 0, 0, 0, true);
        //imageCanvas.DrawImageInCanvas(texture, imageCanvas.Width / 2, imageCanvas.Height / 2, 0, texture.GetWidth() / 2, texture.GetHeight() / 2, true);



        AddChild(imageCanvas);
    }
}
