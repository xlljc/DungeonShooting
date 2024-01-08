
using Godot;

/// <summary>
/// 地形掩码
/// </summary>
public static class TerrainPeering
{
    /// <summary>
    /// 无
    /// </summary>
    public const uint None = 0;
    /// <summary>
    /// 左上
    /// </summary>
    public const uint LeftTop = 0b1;
    /// <summary>
    /// 上
    /// </summary>
    public const uint Top = 0b10;
    /// <summary>
    /// 右上
    /// </summary>
    public const uint RightTop = 0b100;
    /// <summary>
    /// 左
    /// </summary>
    public const uint Left = 0b1000;
    /// <summary>
    /// 中心
    /// </summary>
    public const uint Center = 0b10000;
    /// <summary>
    /// 右
    /// </summary>
    public const uint Right = 0b100000;
    /// <summary>
    /// 左下
    /// </summary>
    public const uint LeftBottom = 0b1000000;
    /// <summary>
    /// 下
    /// </summary>
    public const uint Bottom = 0b10000000;
    /// <summary>
    /// 右下
    /// </summary>
    public const uint RightBottom = 0b100000000;

    /// <summary>
    /// 获取地形掩码值
    /// </summary>
    public static uint GetTerrainPeeringValue(this TileData tileData)
    {
        if (tileData.Terrain != -1 && tileData.TerrainSet != -1)
        {
            var value = Center;
            if (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.TopLeftCorner) >= 0)
                value |= LeftTop;
            if (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.TopSide) >= 0)
                value |= Top;
            if (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.TopRightCorner) >= 0)
                value |= RightTop;
            if (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.LeftSide) >= 0)
                value |= Left;
            if (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.RightSide) >= 0)
                value |= Right;
            if (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.BottomLeftCorner) >= 0)
                value |= LeftBottom;
            if (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.BottomSide) >= 0)
                value |= Bottom;
            if (tileData.GetTerrainPeeringBit(TileSet.CellNeighbor.BottomRightCorner) >= 0)
                value |= RightBottom;
            return value;
        }

        return None;
    }

    /// <summary>
    /// 将地形掩码数据设置到 TileSetAtlasSource 中
    /// </summary>
    public static void InitTerrain(this TileSetAtlasSource atlasSource, TileSetTerrainInfo terrainInfo, int terrainSet, int terrain)
    {
        foreach (var keyValuePair in terrainInfo.T)
        {
            var pos = terrainInfo.GetPosition(keyValuePair.Value);
            var tileData = atlasSource.GetTileData(pos, 0);
            tileData.TerrainSet = terrainSet;
            tileData.Terrain = terrain;
            
            var bit = keyValuePair.Key;
            if ((bit & LeftTop) != 0)
            {
                tileData.SetTerrainPeeringBit(TileSet.CellNeighbor.TopLeftCorner, 0);
            }
            if ((bit & Top) != 0)
            {
                tileData.SetTerrainPeeringBit(TileSet.CellNeighbor.TopSide, 0);
            }
            if ((bit & RightTop) != 0)
            {
                tileData.SetTerrainPeeringBit(TileSet.CellNeighbor.TopRightCorner, 0);
            }
            if ((bit & Left) != 0)
            {
                tileData.SetTerrainPeeringBit(TileSet.CellNeighbor.LeftSide, 0);
            }
            if ((bit & Right) != 0)
            {
                tileData.SetTerrainPeeringBit(TileSet.CellNeighbor.RightSide, 0);
            }
            if ((bit & LeftBottom) != 0)
            {
                tileData.SetTerrainPeeringBit(TileSet.CellNeighbor.BottomLeftCorner, 0);
            }
            if ((bit & Bottom) != 0)
            {
                tileData.SetTerrainPeeringBit(TileSet.CellNeighbor.BottomSide, 0);
            }
            if ((bit & RightBottom) != 0)
            {
                tileData.SetTerrainPeeringBit(TileSet.CellNeighbor.BottomRightCorner, 0);
            }
        }
    }
}