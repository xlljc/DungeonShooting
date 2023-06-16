
using Godot;

public partial class ImageCanvas : Sprite2D
{
    public int Width { get; }
    public int Height { get; }

    private Image _canvas;
    private ImageTexture _texture;

    public ImageCanvas(int width, int height)
    {
        Width = width;
        Height = height;
        
        _canvas = Image.Create(width, height, false, Image.Format.Rgba8);
    }

    public override void _Ready()
    {
        var imageTexture = ImageTexture.CreateFromImage(_canvas);
        Texture = imageTexture;
    }
    
    public void DrawImageInCanvas(Texture2D texture, int x, int y, float angle, int centerX, int centerY, bool flipY)
    {
        var image = texture.GetImage();
        var newAngle = Mathf.RoundToInt(Utils.ConvertAngle(angle));

        if (newAngle == 0) //原图, 直接画上去
        {
            //_canvas.BlitRectMask();
        }
        else if (newAngle == 90) //旋转90度
        {
            
        }
        else if (newAngle == 180) //旋转180度
        {
            
        }
        else if (newAngle == 270) //旋转270度
        {
            
        }
        else
        {
            
        }
    }
    
    private void DrawRotateImage(Image origin, Image canvas, int x, int y, int angle, int centerX, int centerY)
    {
        var rotation = Mathf.DegToRad(angle);
        var width = origin.GetWidth();
        var height = origin.GetHeight();

        var cosAngle = Mathf.Cos(rotation);
        var sinAngle = Mathf.Sin(rotation);

        var newWidth = width * Mathf.Abs(cosAngle) + height * sinAngle;
        var newHeight = width * sinAngle + height * Mathf.Abs(cosAngle);

        var num1 = -0.5f * newWidth * cosAngle - 0.5f * newHeight * sinAngle + 0.5f * width;
        var num2 = 0.5f * newWidth * sinAngle - 0.5f * newHeight * cosAngle + 0.5f * height;

        var offsetX =
            (int)((centerX / sinAngle +
                   (0.5f * newWidth * sinAngle - 0.5f * newHeight * cosAngle + 0.5f * height) /
                   cosAngle -
                   (-0.5f * newWidth * cosAngle - 0.5f * newHeight * sinAngle + 0.5f * width) /
                   sinAngle - centerY / cosAngle) /
                  (cosAngle / sinAngle + sinAngle / cosAngle));

        var offsetY =
            (int)((centerX / cosAngle -
                      (-0.5f * newWidth * cosAngle - 0.5f * newHeight * sinAngle + 0.5f * width) /
                      cosAngle -
                      (0.5f * newWidth * sinAngle - 0.5f * newHeight * cosAngle + 0.5f * height) /
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