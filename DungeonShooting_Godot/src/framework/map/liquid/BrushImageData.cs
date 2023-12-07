using System;
using Godot;
using System.Collections.Generic;
using Config;

/// <summary>
/// 液体笔刷数据
/// </summary>
public class BrushImageData
{
    /// <summary>
    /// 笔刷宽度
    /// </summary>
    public int Width;
    /// <summary>
    /// 笔刷高度
    /// </summary>
    public int Height;
    /// <summary>
    /// 笔刷所有有效像素点
    /// </summary>
    public BrushPixelData[] Pixels;

    //有效像素范围
    public int PixelMinX = int.MaxValue;
    public int PixelMinY = int.MaxValue;
    public int PixelMaxX;
    public int PixelMaxY;
    
    /// <summary>
    /// 有效像素宽度
    /// </summary>
    public int PixelWidth;
    /// <summary>
    /// 有效像素高度
    /// </summary>
    public int PixelHeight;
    /// <summary>
    /// 笔刷材质
    /// </summary>
    public ExcelConfig.LiquidMaterial Material;

    private static readonly Dictionary<string, Image> _imageData = new Dictionary<string, Image>();

    public BrushImageData(ExcelConfig.LiquidMaterial material)
    {
        Material = material;
        var image = GetImageData(material.BrushTexture);
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
                        Material = material
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

    private static Image GetImageData(string path)
    {
        if (!_imageData.TryGetValue(path, out var image))
        {
            var texture = ResourceManager.LoadTexture2D(path);
            image = texture.GetImage();
        }

        return image;
    }

    /// <summary>
    /// 清除笔刷缓存数据
    /// </summary>
    public static void ClearBrushData()
    {
        foreach (var keyValuePair in _imageData)
        {
            keyValuePair.Value.Dispose();
        }
        _imageData.Clear();
    }
}