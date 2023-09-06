
using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 用于描述门生成区域
/// </summary>
public class DoorAreaInfo : IClone<DoorAreaInfo>
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
    public int Start = 0;
    /// <summary>
    /// 结束位置, 相对 tilemap 的横/纵轴原点, 单位: 像素
    /// </summary>
    [JsonInclude]
    public int End = 16;
    
    /// <summary>
    /// 起始点坐标, 该坐标为模板场景的世界坐标, 单位: 像素, 不参与序列化与反序列化
    /// </summary>
    public Vector2I StartPosition;
    /// <summary>
    /// 结束点坐标, 该坐标为模板场景的世界坐标, 单位: 像素, 不参与序列化与反序列化
    /// </summary>
    public Vector2I EndPosition;

    public DoorAreaInfo()
    {
    }

    public DoorAreaInfo(DoorDirection direction, int start, int end)
    {
        Direction = direction;
        Start = start;
        End = end;
    }
    
    /// <summary>
    /// 自动计算 startPosition 和 endPosition
    /// </summary>
    public void CalcPosition(Vector2I rootPosition, Vector2I rootSize)
    {
        switch (Direction)
        {
            case DoorDirection.E:
                StartPosition = new Vector2I(rootPosition.X, rootPosition.Y + Start);
                EndPosition = new Vector2I(rootPosition.X, rootPosition.Y + End);
                break;
            case DoorDirection.W:
                StartPosition = new Vector2I(rootPosition.X + rootSize.X, rootPosition.Y + Start);
                EndPosition = new Vector2I(rootPosition.X + rootSize.X, rootPosition.Y + End);
                break;
            case DoorDirection.S:
                StartPosition = new Vector2I(rootPosition.X + Start, rootPosition.Y + rootSize.Y);
                EndPosition = new Vector2I(rootPosition.X + End, rootPosition.Y + rootSize.Y);
                break;
            case DoorDirection.N:
                StartPosition = new Vector2I(rootPosition.X + Start, rootPosition.Y);
                EndPosition = new Vector2I(rootPosition.X + End, rootPosition.Y);
                break;
        }
    }

    public DoorAreaInfo Clone()
    {
        var data = new DoorAreaInfo();
        data.Start = Start;
        data.End = End;
        data.Direction = Direction;
        return data;
    }
    
    
}