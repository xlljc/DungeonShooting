using System.Text.Json.Serialization;

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
    
}