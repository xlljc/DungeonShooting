

using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 可序列化的 Color 对象
/// </summary>
public class SerializeColor
{
    public SerializeColor(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public SerializeColor(Color color)
    {
        R = color.R;
        G = color.G;
        B = color.B;
        A = color.A;
    }

    public SerializeColor()
    {
    }
    
    [JsonInclude]
    public float R { get; private set; }
    [JsonInclude]
    public float G  { get; private set; }
    [JsonInclude]
    public float B  { get; private set; }
    [JsonInclude]
    public float A  { get; private set; }
    
    /// <summary>
    /// 转为 Color
    /// </summary>
    public Color AsColor()
    {
        return new Color(R, G, B, A);
    }

}