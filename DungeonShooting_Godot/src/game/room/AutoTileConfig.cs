
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间图块配置信息
/// </summary>
public class AutoTileConfig
{
    public TileCellData IN_LT = new TileCellData(0, new Vector2I(3, 3));
    public TileCellData IN_LB = new TileCellData(0, new Vector2I(11, 2));
    public TileCellData IN_RT = new TileCellData(0, new Vector2I(1, 3));
    public TileCellData IN_RB = new TileCellData(0, new Vector2I(13, 2));
    public TileCellData R = new TileCellData(0, new Vector2I(1, 3));
    public TileCellData L = new TileCellData(0, new Vector2I(3, 3));
    public TileCellData T = new TileCellData(0, new Vector2I(2, 7));
    public TileCellData B = new TileCellData(0, new Vector2I(2, 2));
    
    public TileCellData OUT_LT = new TileCellData(0, new Vector2I(1, 2));
    public TileCellData OUT_LB = new TileCellData(0, new Vector2I(1, 7));
    public TileCellData OUT_RT = new TileCellData(0, new Vector2I(3, 2));
    public TileCellData OUT_RB = new TileCellData(0, new Vector2I(3, 7));
    
    public TileCellData WALL_BLOCK = new TileCellData(0, new Vector2I(2, 3));
    
    private List<Vector2I> _middleLayerAtlasCoords = new List<Vector2I>()
    {
        new Vector2I(1, 6),
        new Vector2I(2, 6),
        new Vector2I(3, 6),
        new Vector2I(1, 7),
        new Vector2I(2, 7),
        new Vector2I(3, 7),
    };
    
    private List<Vector2I> _topLayerAtlasCoords = new List<Vector2I>()
    {
        new Vector2I(1, 4),
        new Vector2I(1, 3),
        new Vector2I(1, 2),
        new Vector2I(2, 2),
        new Vector2I(3, 2),
        new Vector2I(3, 3),
        new Vector2I(3, 4),
        new Vector2I(11, 2),
        new Vector2I(13, 2),
    };

    public int GetLayer(Vector2I atlasCoords)
    {
        var layer = GameConfig.FloorMapLayer;
        if (_middleLayerAtlasCoords.Contains(atlasCoords))
        {
            layer = GameConfig.MiddleMapLayer;
        }
        else if (_topLayerAtlasCoords.Contains(atlasCoords))
        {
            layer = GameConfig.TopMapLayer;
        }

        return layer;
    }
    
    //-----------------------------------------------------------

    public TileCellData Floor = new TileCellData(0, new Vector2I(0, 4));
    public TileCellData TopMask = new TileCellData(0, new Vector2I(9, 2));
    public TileCellData WallLeft = new TileCellData(0, new Vector2I(1, 4));
    public TileCellData WallCenter = new TileCellData(0, new Vector2I(2, 4));
    public TileCellData WallRight = new TileCellData(0, new Vector2I(3, 4));
    public TileCellData WallSingle = new TileCellData(0, new Vector2I(4, 4));
    
    //----------------------------- 所有自动图块数据 -----------------------------
    //----------------------------- 命名规则: Auto_ + LT + T + RT + _ + L + C + R + _ + LB + B + RB
    //第一列
    public TileCellData Auto_000_010_010;
    public TileCellData Auto_010_010_010;
    public TileCellData Auto_010_010_000;
    public TileCellData Auto_000_010_000;
    //第二列
    public TileCellData Auto_000_011_010;
    public TileCellData Auto_010_011_010;
    public TileCellData Auto_010_011_000;
    public TileCellData Auto_000_011_000;
    //第三列
    public TileCellData Auto_000_111_010;
    public TileCellData Auto_010_111_010;
    public TileCellData Auto_010_111_000;
    public TileCellData Auto_000_111_000;
    //第四列
    public TileCellData Auto_000_110_010;
    public TileCellData Auto_010_110_010;
    public TileCellData Auto_010_110_000;
    public TileCellData Auto_000_110_000;
    //第五列
    public TileCellData Auto_110_111_010;
    public TileCellData Auto_010_011_011;
    public TileCellData Auto_011_011_010;
    public TileCellData Auto_010_111_110;
    //第六列
    public TileCellData Auto_000_111_011;
    public TileCellData Auto_011_111_111;
    public TileCellData Auto_111_111_011;
    public TileCellData Auto_011_111_000;
    //第七列
    public TileCellData Auto_000_111_110;
    public TileCellData Auto_110_111_111;
    public TileCellData Auto_111_111_110;
    public TileCellData Auto_110_111_000;
    //第八列
    public TileCellData Auto_011_111_010;
    public TileCellData Auto_010_110_110;
    public TileCellData Auto_110_110_010;
    public TileCellData Auto_010_111_011;
    //------------------------------------------------------------------------- 

    public AutoTileConfig(int sourceId, TileSetAtlasSource atlasSource)
    {
        var tilesCount = atlasSource.GetTilesCount();
        for (var i = 0; i < tilesCount; i++)
        {
            var pos = atlasSource.GetTileId(i);
            var tileData = atlasSource.GetTileData(pos, 0);
            if (tileData.Terrain != -1 && tileData.TerrainSet != -1) //判断是否使用掩码
            {
                HandlerTileData(tileData.GetTerrainPeeringValue(), sourceId, pos);
            }
        }
    }
    
    public int GetLayer2(Vector2I atlasCoords)
    {
        return atlasCoords == Floor.AutoTileCoords ? GameConfig.FloorMapLayer : GameConfig.TopMapLayer;
    }

    private void HandlerTileData(uint peeringValue, int sourceId, Vector2I pos)
    {
        var temp = new TileCellData(sourceId, pos);
        switch (peeringValue)
        {
            case TerrainPeering.Center | TerrainPeering.Bottom:
                Auto_000_010_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Bottom:
                Auto_010_010_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Center:
                Auto_010_010_000 = temp;
                break;
            case TerrainPeering.Center:
                Auto_000_010_000 = temp;
                break;
            
            case TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom:
                Auto_000_011_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom:
                Auto_010_011_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right:
                Auto_010_011_000 = temp;
                break;
            case TerrainPeering.Center | TerrainPeering.Right:
                Auto_000_011_000 = temp;
                break;
            
            case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom:
                Auto_000_111_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom:
                Auto_010_111_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right:
                Auto_010_111_000 = temp;
                break;
            case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right:
                Auto_000_111_000 = temp;
                break;
            
            case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom:
                Auto_000_110_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom:
                Auto_010_110_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center:
                Auto_010_110_000 = temp;
                break;
            case TerrainPeering.Left | TerrainPeering.Center:
                Auto_000_110_000 = temp;
                break;
            
            default:
                Debug.LogError("未知PeeringValue: " + peeringValue);
                break;
        }
    }
}