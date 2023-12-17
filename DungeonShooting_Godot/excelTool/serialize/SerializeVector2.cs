
using System.Text.Json.Serialization;

/// <summary>
/// 可序列化的 Vector2 对象
/// </summary>
public class SerializeVector2
{
    public SerializeVector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public SerializeVector2(SerializeVector2 v)
    {
        X = v.X;
        Y = v.Y;
    }

    public SerializeVector2()
    {
        
    }

    [JsonInclude]
    public float X { get; private set; }
    [JsonInclude]
    public float Y  { get; private set; }
}