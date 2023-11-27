using System;
using Godot;
using System.Collections.Generic;

/// <summary>
/// 液体笔刷数据
/// </summary>
public class BrushImageData
{
    /// <summary>
    /// 
    /// </summary>
    public int Width;

    public int Height;
    public BrushPixelData[] Pixels;

    //有效像素范围
    public int PixelMinX = int.MaxValue;
    public int PixelMinY = int.MaxValue;
    public int PixelMaxX;
    public int PixelMaxY;

    public int PixelWidth;
    public int PixelHeight;

    //补帧间距倍率
    public float Ffm;

    public BrushImageData(Image image, byte type, float ffm, float duration, float writeOffSpeed)
    {
        Ffm = ffm;
        var list = new List<BrushPixelData>();
        var width = image.GetWidth();
        var height = image.GetHeight();
        var flag = false;
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel = image.GetPixel(x, y);
                if (pixel.A > 0)
                {
                    flag = true;
                    list.Add(new BrushPixelData()
                    {
                        X = x,
                        Y = y,
                        Color = pixel,
                        Type = type,
                        Duration = duration,
                        WriteOffSpeed = writeOffSpeed
                    });
                    if (x < PixelMinX)
                    {
                        PixelMinX = x;
                    }
                    else if (x > PixelMaxX)
                    {
                        PixelMaxX = x;
                    }

                    if (y < PixelMinY)
                    {
                        PixelMinY = y;
                    }
                    else if (y > PixelMaxY)
                    {
                        PixelMaxY = y;
                    }
                }
            }
        }

        if (!flag)
        {
            throw new Exception("不能使用完全透明的图片作为笔刷!");
        }

        Pixels = list.ToArray();
        Width = width;
        Height = height;

        PixelWidth = PixelMaxX - PixelMinX;
        PixelHeight = PixelMaxY - PixelMinY;
    }
}