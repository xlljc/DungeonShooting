
using System;
using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 用于描述门生成区域
/// </summary>
[Serializable]
public class DoorAreaInfo
{
    /// <summary>
    /// 门方向
    /// </summary>
    [JsonInclude]
    public DoorDirection Direction;
    /// <summary>
    /// 起始位置, 相对 tilemap 自身的横/纵轴原点
    /// </summary>
    [JsonInclude]
    public float Start = 0;
    /// <summary>
    /// 结束位置, 相对 tilemap 自身的横/纵轴原点
    /// </summary>
    [JsonInclude]
    public float End = 1;
    
    public Vector2 StartPosition;
    public Vector2 EndPosition;
}