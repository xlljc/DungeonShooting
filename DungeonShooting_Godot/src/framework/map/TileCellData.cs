
using Godot;

/// <summary>
/// 地图cell属性信息
/// </summary>
public class TileCellData
{
    public TileCellData(int sourceId, Vector2I autoTileCoords)
    {
        SourceId = sourceId;
        AutoTileCoords = autoTileCoords;
    }

    /// <summary>
    /// 在TileSet中的图块id, 也就是sourceId
    /// </summary>
    public int SourceId;
    
    /// <summary>
    /// 如果是图块集, 该属性就表示在图块集的位置
    /// </summary>
    public Vector2I AutoTileCoords;
}