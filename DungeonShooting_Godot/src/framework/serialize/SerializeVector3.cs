using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 可序列化的 Vector3 对象
/// </summary>
public class SerializeVector3
{
    public SerializeVector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public SerializeVector3(Vector3 v)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
    }

    public SerializeVector3(Vector3I v)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
    }
    
    public SerializeVector3(SerializeVector3 v)
    {
        X = v.X;
        Y = v.Y;
    }
    
    public SerializeVector3()
    {
    }
    
    [JsonInclude]
    public float X { get; private set; }
    [JsonInclude]
    public float Y  { get; private set; }
    [JsonInclude]
    public float Z  { get; private set; }
    
    /// <summary>
    /// 转为 Vector3
    /// </summary>
    public Vector3 AsVector3()
    {
        return new Vector3(X, Y, Z);
    }

    /// <summary>
    /// 转为 Vector3I
    /// </summary>
    public Vector3I AsVector3I()
    {
        return new Vector3I((int)X, (int)Y, (int)Z);
    }
}