

using System.Text.Json.Serialization;

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

}