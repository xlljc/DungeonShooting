
using System.Text.Json.Serialization;
using Godot;

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

    public SerializeVector2(Vector2 v)
    {
        X = v.X;
        Y = v.Y;
    }

    public SerializeVector2(Vector2I v)
    {
        X = v.X;
        Y = v.Y;
    }

    public SerializeVector2()
    {
        
    }

    
    [JsonInclude]
    public float X;
    [JsonInclude]
    public float Y;
}