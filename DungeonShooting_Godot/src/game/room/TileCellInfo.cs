
using Godot;

/// <summary>
/// 地图cell属性信息
/// </summary>
public class TileCellInfo
{
    public TileCellInfo(int id, Vector2? autotileCoord)
    {
        Id = id;
        AutotileCoord = autotileCoord;
    }

    /// <summary>
    /// 在TileSet中的图块id
    /// </summary>
    public int Id;
    
    /// <summary>
    /// 如果是图块集, 该属性就表示在图块集的位置
    /// </summary>
    public Vector2? AutotileCoord;
}