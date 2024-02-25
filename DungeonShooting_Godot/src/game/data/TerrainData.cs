

public class TerrainData
{
    /// <summary>
    /// TileSetTerrainInfo 的 index
    /// </summary>
    public int TerrainIndex;
    /// <summary>
    /// Terrain 数据
    /// </summary>
    public TileSetTerrainInfo TerrainInfo;
    /// <summary>
    /// 对应在 Godot.TileSet 中的 TerrainSet Index
    /// </summary>
    public int TerrainSetIndex;

    public TerrainData(int terrainIndex, TileSetTerrainInfo terrainInfo, int terrainSetIndex)
    {
        TerrainIndex = terrainIndex;
        TerrainInfo = terrainInfo;
        TerrainSetIndex = terrainSetIndex;
    }
}