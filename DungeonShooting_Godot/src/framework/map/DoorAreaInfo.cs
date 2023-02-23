
using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 用于描述门生成区域
/// </summary>
public class DoorAreaInfo
{
    /// <summary>
    /// 门方向
    /// </summary>
    [JsonInclude]
    public DoorDirection Direction;
    /// <summary>
    /// 起始位置, 相对 tilemap 的横/纵轴原点, 单位: 像素
    /// </summary>
    [JsonInclude]
    public float Start = 0;
    /// <summary>
    /// 结束位置, 相对 tilemap 的横/纵轴原点, 单位: 像素
    /// </summary>
    [JsonInclude]
    public float End = 16;
    
    /// <summary>
    /// 起始点坐标, 该坐标位世界坐标, 单位: 像素, 不参与序列化与反序列化
    /// </summary>
    public Vector2 StartPosition;
    /// <summary>
    /// 结束点坐标, 该坐标位世界坐标, 单位: 像素, 不参与序列化与反序列化
    /// </summary>
    public Vector2 EndPosition;

    /// <summary>
    /// 自动计算 startPosition 和 endPosition
    /// </summary>
    public void CalcPosition(Vector2 rootPosition, Vector2 rootSize)
    {
        switch (Direction)
        {
            case DoorDirection.E:
                StartPosition = new Vector2(rootPosition.X, rootPosition.Y + Start);
                EndPosition = new Vector2(rootPosition.X, rootPosition.Y + End);
                break;
            case DoorDirection.W:
                StartPosition = new Vector2(rootPosition.X + rootSize.X, rootPosition.Y + Start);
                EndPosition = new Vector2(rootPosition.X + rootSize.X, rootPosition.Y + End);
                break;
            case DoorDirection.S:
                StartPosition = new Vector2(rootPosition.X + Start, rootPosition.Y + rootSize.Y);
                EndPosition = new Vector2(rootPosition.X + End, rootPosition.Y + rootSize.Y);
                break;
            case DoorDirection.N:
                StartPosition = new Vector2(rootPosition.X + Start, rootPosition.Y);
                EndPosition = new Vector2(rootPosition.X + End, rootPosition.Y);
                break;
        }
    }
}