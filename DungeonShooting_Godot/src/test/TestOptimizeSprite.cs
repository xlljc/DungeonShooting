using Godot;

public partial class TestOptimizeSprite : Node2D
{
    [Export()]
    public Texture2D Texture2D;
    
    public override void _Ready()
    {
        var image = Image.Create(150, 150, true, Image.Format.Rgba8);
        
        for (int x = 0; x < 150; x++)
        {
            for (int y = 0; y < 150; y++)
            {
                image.SetPixel(x, y, Colors.Gray);
            }
        }
        var imgData = Texture2D.GetImage();
        var width = imgData.GetWidth();
        var height = imgData.GetHeight();

        // image.SetPixel(50, 50, Colors.Red);
        // image.SetPixel(50 + width, 50 + height, Colors.Red);
        // image.SetPixel(50, 50 + height, Colors.Red);
        // image.SetPixel(50 + width, 50, Colors.Red);
        // for (int x = 0; x < width; x++)
        // {
        //     for (int y = 0; y < height; y++)
        //     {
        //         image.SetPixel(x + 50, y + 50, imgData.GetPixel(x, y));
        //     }
        // }

        var angle = Mathf.DegToRad(45);
        var w1 = width * Mathf.Abs(Mathf.Cos(angle)) + height * Mathf.Sin(angle);
        var h1 = width *  Mathf.Sin(angle) + height *  Mathf.Abs(Mathf.Cos(angle));
        // image.SetPixel(50, 100, Colors.Green);
        // image.SetPixel(50 + (int)w1, 100 + (int)h1, Colors.Green);
        // image.SetPixel(50, 100 + (int)h1, Colors.Green);
        // image.SetPixel(50 + (int)w1, 100, Colors.Green);

        var centerX = 0.5f;
        var centerY = 0.5f;
        
        for (int x2 = 0; x2 < w1; x2++)
        {
            for (int y2 = 0; y2 < h1; y2++)
            {
                var x1=x2*Mathf.Cos(angle)+y2*Mathf.Sin(angle)-0.5*w1*Mathf.Cos(angle)-0.5*h1*Mathf.Sin(angle)+0.5*width;
                var y1=-x2*Mathf.Sin(angle)+y2*Mathf.Cos(angle)+0.5*w1*Mathf.Sin(angle)-0.5*h1*Mathf.Cos(angle)+0.5*height;
                

                if (x1 < 0 || x1 >= width || y1 < 0 || y1 >= height)
                {
                    image.SetPixel(x2 + 50, y2 + 50, new Color(0,0,0,0));
                    //在图片外
                    continue;
                }
                var x = Mathf.RoundToInt(x1);
                var y = Mathf.RoundToInt(y1);
                image.SetPixel(x2 + 50, y2 + 50, imgData.GetPixel(x, y));
                //如果(x1,y1)不在原图宽高所表示范围内，则（x2,y2）处的像素值设置为0或255
                //如果(x1,y1)在原图宽高所表示范围内，使用最近邻插值或双线性插值，求出（x2,y2）处的像素值
        
            }
        }

        //RotateImage(imgData, image, 0);

        var imageTexture = ImageTexture.CreateFromImage(image);
        var sprite2D = new Sprite2D();
        sprite2D.Texture = imageTexture;
        sprite2D.Position = new Vector2(900, 500);
        sprite2D.Scale = new Vector2(5, 5);
        AddChild(sprite2D);
    }

    public void RotateImage(Image origin, Image canvas, float angle)
    {
        
    }
}
