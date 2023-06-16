using Godot;

public partial class TestOptimizeSprite : Node2D
{
    [Export()] public Texture2D Texture2D;

    public override void _Ready()
    {
        var canvas = Image.Create(150, 150, true, Image.Format.Rgba8);

        for (int x = 0; x < 150; x++)
        {
            for (int y = 0; y < 150; y++)
            {
                canvas.SetPixel(x, y, Colors.Gray);
            }
        }

        var image = Texture2D.GetImage();
        RotateImage(image, canvas, 50, 50, 120, image.GetWidth(), image.GetHeight());

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

    public void RotateImage(Image origin, Image canvas, int x, int y, float angle, int centerX, int centerY)
    {
        angle = Mathf.DegToRad(angle);
        var width = origin.GetWidth();
        var height = origin.GetHeight();

        var cosAngle = Mathf.Cos(angle);
        var sinAngle = Mathf.Sin(angle);

        var newWidth = width * Mathf.Abs(cosAngle) + height * sinAngle;
        var newHeight = width * sinAngle + height * Mathf.Abs(cosAngle);

        var num1 = -0.5 * newWidth * cosAngle - 0.5 * newHeight * sinAngle + 0.5 * width;
        var num2 = 0.5 * newWidth * sinAngle - 0.5 * newHeight * cosAngle + 0.5 * height;

        var offsetX =
            (int)((centerX / sinAngle +
                   (0.5 * newWidth * sinAngle - 0.5 * newHeight * cosAngle + 0.5 * height) /
                   cosAngle -
                   (-0.5 * newWidth * cosAngle - 0.5 * newHeight * sinAngle + 0.5 * width) /
                   sinAngle - centerY / cosAngle) /
                  (cosAngle / sinAngle + sinAngle / cosAngle));

        var offsetY =
            (int)((centerX / cosAngle -
                      (-0.5 * newWidth * cosAngle - 0.5 * newHeight * sinAngle + 0.5 * width) /
                      cosAngle -
                      (0.5 * newWidth * sinAngle - 0.5 * newHeight * cosAngle + 0.5 * height) /
                      sinAngle + centerY / sinAngle) /
                  (sinAngle / cosAngle + cosAngle / sinAngle));


        for (int x2 = 0; x2 < newWidth; x2++)
        {
            for (int y2 = 0; y2 < newHeight; y2++)
            {
                //如果(x1,y1)不在原图宽高所表示范围内，则（x2,y2）处的像素值设置为0或255
                var x1 = x2 * cosAngle + y2 * sinAngle + num1;
                var y1 = -x2 * sinAngle + y2 * cosAngle + num2;

                if (x1 < 0 || x1 >= width || y1 < 0 || y1 >= height)
                {
                    //image.SetPixel(x, y, new Color(0, 0, 0, 0));
                    //在图片外
                    continue;
                }

                //如果(x1,y1)在原图宽高所表示范围内，使用最近邻插值或双线性插值，求出（x2,y2）处的像素值
                canvas.SetPixel(x2 + x - offsetX, y2 + y - offsetY,
                    origin.GetPixel(Mathf.FloorToInt(x1), Mathf.FloorToInt(y1)));
            }
        }
    }
}
