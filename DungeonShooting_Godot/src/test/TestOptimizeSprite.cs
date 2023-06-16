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
        

        // image.SetPixel(50, 100, Colors.Green);
        // image.SetPixel(50 + (int)w1, 100 + (int)h1, Colors.Green);
        // image.SetPixel(50, 100 + (int)h1, Colors.Green);
        // image.SetPixel(50 + (int)w1, 100, Colors.Green);

        // var centerX = 0.5f;
        // var centerY = 0.5f;

        var angle = Mathf.DegToRad(44);
        var newWidth = width * Mathf.Abs(Mathf.Cos(angle)) + height * Mathf.Sin(angle);
        var newHeight = width * Mathf.Sin(angle) + height * Mathf.Abs(Mathf.Cos(angle));

        var ox = width * 1;
        var oy = height * 0.5;


        var offsetX =
            (ox / Mathf.Sin(angle) +
             (0.5 * newWidth * Mathf.Sin(angle) - 0.5 * newHeight * Mathf.Cos(angle) + 0.5 * height) /
             Mathf.Cos(angle) -
             (-0.5 * newWidth * Mathf.Cos(angle) - 0.5 * newHeight * Mathf.Sin(angle) + 0.5 * width) /
             Mathf.Sin(angle) - oy / Mathf.Cos(angle)) /
            (Mathf.Cos(angle) / Mathf.Sin(angle) + Mathf.Sin(angle) / Mathf.Cos(angle));
        
        var offsetY =
            (ox / Mathf.Cos(angle) -
                (-0.5 * newWidth * Mathf.Cos(angle) - 0.5 * newHeight * Mathf.Sin(angle) + 0.5 * width) /
                Mathf.Cos(angle) -
                (0.5 * newWidth * Mathf.Sin(angle) - 0.5 * newHeight * Mathf.Cos(angle) + 0.5 * height) /
                Mathf.Sin(angle) + oy / Mathf.Sin(angle)) /
            (Mathf.Sin(angle) / Mathf.Cos(angle) + Mathf.Cos(angle) / Mathf.Sin(angle));
        
        
        for (int x2 = 0; x2 < newWidth; x2++)
        {
            for (int y2 = 0; y2 < newHeight; y2++)
            {
                //如果(x1,y1)不在原图宽高所表示范围内，则（x2,y2）处的像素值设置为0或255
                var x1 = x2 * Mathf.Cos(angle) + y2 * Mathf.Sin(angle) - 0.5 * newWidth * Mathf.Cos(angle) -
                    0.5 * newHeight * Mathf.Sin(angle) + 0.5 * width;
                var y1 = -x2 * Mathf.Sin(angle) + y2 * Mathf.Cos(angle) + 0.5 * newWidth * Mathf.Sin(angle) -
                    0.5 * newHeight * Mathf.Cos(angle) + 0.5 * height;

                var x = x2 + 50 - (int)offsetX;
                var y = y2 + 50 - (int)offsetY;
                
                if (x1 < 0 || x1 >= width || y1 < 0 || y1 >= height)
                {
                    //image.SetPixel(x, y, new Color(0, 0, 0, 0));
                    //在图片外
                    continue;
                }
                
                //如果(x1,y1)在原图宽高所表示范围内，使用最近邻插值或双线性插值，求出（x2,y2）处的像素值
                image.SetPixel(x, y, imgData.GetPixel(Mathf.FloorToInt(x1), Mathf.FloorToInt(y1)));
            }
        }

        //RotateImage(imgData, image, 0);

        for (int i = 0; i < image.GetWidth(); i++)
        {
            image.SetPixel(i, 50, Colors.Black);
            image.SetPixel(i, 50 + imgData.GetHeight() / 2, Colors.Green);
        }

        for (int i = 0; i < image.GetHeight(); i++)
        {
            image.SetPixel(50, i, Colors.Black);
            image.SetPixel(50 + imgData.GetWidth() / 2, i, Colors.Green);
        }
        
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
