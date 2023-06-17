using Godot;

public partial class TestOptimizeSprite : Node2D
{
    [Export()] public Texture2D[] ImageList;

    public override void _Ready()
    {
        ImageCanvas.MaxHandlerTime = 16;
        var imageCanvas = new ImageCanvas(1920, 1080);

        for (int i = 0; i < 10000; i++)
        {
            var texture = Utils.RandomChoose(ImageList);
            var centerX = Utils.RandomRangeInt(0, texture.GetWidth() - 1);
            var centerY = Utils.RandomRangeInt(0, texture.GetHeight() - 1);
            var angle = Utils.RandomRangeInt(0, 360);
            imageCanvas.DrawImageInCanvas(texture,
                Utils.RandomRangeInt(0, imageCanvas.Width), Utils.RandomRangeInt(0, imageCanvas.Height),
                angle, centerX, centerY, Utils.RandomBoolean()
            );
        }

        AddChild(imageCanvas);
    }


    public override void _Process(double delta)
    {
        ImageCanvas.UpdateImageCanvas((float)delta);
    }
}
