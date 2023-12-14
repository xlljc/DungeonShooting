
using Godot;

/// <summary>
/// 地图cell属性信息
/// </summary>
public class TileCellData
{
    public TileCellData(int id, Vector2I autoTileCoord)
    {
        Id = id;
        AutoTileCoord = autoTileCoord;
    }

    /// <summary>
    /// 在TileSet中的图块id, 也就是sourceId
    /// </summary>
    public int Id;
    
    /// <summary>
    /// 如果是图块集, 该属性就表示在图块集的位置
    /// </summary>
    public Vector2I AutoTileCoord;
}