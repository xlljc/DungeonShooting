using Godot;
using System;
using System.Collections.Generic;

public partial class TestDrawSprite : Node2D
{
    [Export]
    public Label FpsLabel;

    [Export]
    public PathFollow2D PathFollow2D;

    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        FpsLabel.Text = "FPS: " + 1 / delta;
        PathFollow2D.Progress += 200 * (float)delta;
    }

    //使用sprite2d绘制精灵
    // public override void _Ready()
    // {
    //     for (int i = 0; i < 20000; i++)
    //     {
    //         var image = Image.Create(50, 50, false, Image.Format.Rgba8);
    //         image.Fill(new Color(Utils.Random.RandomRangeFloat(0, 1), Utils.Random.RandomRangeFloat(0, 1), Utils.Random.RandomRangeFloat(0, 1), Utils.Random.RandomRangeFloat(0, 1)));
    //         var imageTexture = ImageTexture.CreateFromImage(image);
    //         var sprite2D = new Sprite2D();
    //         sprite2D.Texture = imageTexture;
    //         sprite2D.Centered = false;
    //         sprite2D.Scale = new Vector2(Utils.Random.RandomRangeFloat(0.2f, 2f), Utils.Random.RandomRangeFloat(0.2f, 2f));
    //         sprite2D.Rotation = Utils.Random.RandomRangeFloat(0, Mathf.Pi);
    //         sprite2D.Position = new Vector2(Utils.Random.RandomRangeInt(0, 1600), Utils.Random.RandomRangeInt(0, 900));
    //         AddChild(sprite2D);
    //     }
    // }
    
    //尝试使用DrawTexture绘制texture，结果发现性比sprite2d还差
    // private class DrawTextureData
    // {
    //     public Texture2D Texture2D;
    //     public Vector2 Position;
    //     public float Rotation;
    //     public Vector2 Scale;
    // }
    // private List<DrawTextureData> _texture2Ds = new List<DrawTextureData>();
    
    // public override void _Ready()
    // {
    //     for (int i = 0; i < 10000; i++)
    //     {
    //         var image = Image.Create(100, 100, false, Image.Format.Rgba8);
    //         image.Fill(new Color(Utils.Random.RandomRangeFloat(0, 1), Utils.Random.RandomRangeFloat(0, 1), Utils.Random.RandomRangeFloat(0, 1), Utils.Random.RandomRangeFloat(0, 1)));
    //         var imageTexture = ImageTexture.CreateFromImage(image);
    //         _texture2Ds.Add(new DrawTextureData()
    //         {
    //             Texture2D = imageTexture,
    //             Position = new Vector2(Utils.Random.RandomRangeInt(0, 1600), Utils.Random.RandomRangeInt(0, 900)),
    //             Rotation = Utils.Random.RandomRangeFloat(0, Mathf.Pi),
    //             Scale = new Vector2(Utils.Random.RandomRangeFloat(0.2f, 2f), Utils.Random.RandomRangeFloat(0.2f, 2f))
    //         });
    //     }
    // }
    //
    // public override void _Process(double delta)
    // {
    //     QueueRedraw();
    // }
    //
    // public override void _Draw()
    // {
    //     foreach (var texture2D in _texture2Ds)
    //     {
    //         DrawSetTransform(texture2D.Position, texture2D.Rotation, texture2D.Scale);
    //         DrawTexture(texture2D.Texture2D, Vector2.Zero);
    //     }
    // }
}
