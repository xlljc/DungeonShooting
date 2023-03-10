
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

    /// <summary>
    /// 转为 Vector2
    /// </summary>
    public Vector2 AsVector2()
    {
        return new Vector2(X, Y);
    }

    /// <summary>
    /// 转为 Vector2I
    /// </summary>
    public Vector2I AsVector2I()
    {
        return new Vector2I((int)X, (int)Y);
    }
}