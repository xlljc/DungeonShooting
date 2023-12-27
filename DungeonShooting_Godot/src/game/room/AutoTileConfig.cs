
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间图块配置信息
/// </summary>
public class AutoTileConfig
{
    public TileCellData Floor = new TileCellData(0, new Vector2I(0, 4));
    public TileCellData TopMask;
    public TileCellData Wall_Bottom;
    public TileCellData Wall_Left;
    public TileCellData Wall_Right;
    public TileCellData Wall_Top;
    public TileCellData Wall_Out_LB;
    public TileCellData Wall_Out_LT;
    public TileCellData Wall_Out_RB;
    public TileCellData Wall_Out_RT;
    public TileCellData Wall_IN_LT;
    public TileCellData Wall_IN_LB;
    public TileCellData Wall_IN_RT;
    public TileCellData Wall_IN_RB;
    
    public TileCellData Wall_Vertical_Left = new TileCellData(0, new Vector2I(1, 4));
    public TileCellData Wall_Vertical_Center = new TileCellData(0, new Vector2I(2, 4));
    public TileCellData Wall_Vertical_Right = new TileCellData(0, new Vector2I(3, 4));
    public TileCellData Wall_Vertical_Single = new TileCellData(0, new Vector2I(4, 4));
    
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
    //第九列
    public TileCellData Auto_000_011_011;
    public TileCellData Auto_011_011_011;
    public TileCellData Auto_011_111_011;
    public TileCellData Auto_011_011_000;
    //第十列
    public TileCellData Auto_010_111_111;
    public TileCellData Auto_110_111_011;
    public TileCellData Auto_111_111_111;
    public TileCellData Auto_111_111_000;
    //第十一列
    public TileCellData Auto_000_111_111;
    public TileCellData Auto_011_111_110;
    public TileCellData Auto_111_111_010;
    //第十二列
    public TileCellData Auto_000_110_110;
    public TileCellData Auto_110_111_110;
    public TileCellData Auto_110_110_110;
    public TileCellData Auto_110_110_000;
    
    //------------------------------------------------------------------------- 

    public AutoTileConfig(int sourceId, TileSetAtlasSource atlasSource)
    {
        var tilesCount = atlasSource.GetTilesCount();
        for (var i = 0; i < tilesCount; i++)
        {
            var pos = atlasSource.GetTileId(i);
            var tileData = atlasSource.GetTileData(pos, 0);
            if (tileData != null && tileData.Terrain != -1 && tileData.TerrainSet != -1) //判断是否使用掩码
            {
                HandlerTileData(tileData.GetTerrainPeeringValue(), sourceId, pos);
            }
        }

        TopMask = Auto_111_111_111;
        Wall_Bottom = Auto_000_111_111;
        Wall_Left = Auto_110_110_110;
        Wall_Right = Auto_011_011_011;
        Wall_Top = Auto_111_111_000;

        Wall_Out_LB = Auto_011_011_000;
        Wall_Out_LT = Auto_000_011_011;
        Wall_Out_RB = Auto_110_110_000;
        Wall_Out_RT = Auto_000_110_110;

        Wall_IN_LT = Auto_111_111_110;
        Wall_IN_LB = Auto_110_111_111;
        Wall_IN_RT = Auto_111_111_011;
        Wall_IN_RB = Auto_011_111_111;
    }

    public int GetLayer2(Vector2I atlasCoords)
    {
        return atlasCoords == Floor.AutoTileCoords ? GameConfig.FloorMapLayer : GameConfig.TopMapLayer;
    }

    private void HandlerTileData(uint peeringValue, int sourceId, Vector2I pos)
    {
        var temp = new TileCellData(sourceId, pos, peeringValue);
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
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                 TerrainPeering.Bottom:
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
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right | TerrainPeering.Bottom:
                Auto_110_111_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom |
                 TerrainPeering.RightBottom:
                Auto_010_011_011 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right |
                 TerrainPeering.Bottom:
                Auto_011_011_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                 TerrainPeering.LeftBottom | TerrainPeering.Bottom:
                Auto_010_111_110 = temp;
                break;
            case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom |
                 TerrainPeering.RightBottom:
                Auto_000_111_011 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_011_111_111 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left |
                 TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_111_111_011 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right:
                Auto_011_111_000 = temp;
                break;
            case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom |
                 TerrainPeering.Bottom:
                Auto_000_111_110 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_110_111_111 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left |
                 TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom:
                Auto_111_111_110 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right:
                Auto_110_111_000 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right | TerrainPeering.Bottom:
                Auto_011_111_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom |
                 TerrainPeering.Bottom:
                Auto_010_110_110 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Bottom:
                Auto_110_110_010 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                 TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_010_111_011 = temp;
                break;
            case TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_000_011_011 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right |
                 TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_011_011_011 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_011_111_011 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right:
                Auto_011_011_000 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                 TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_010_111_111 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_110_111_011 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left |
                 TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom |
                 TerrainPeering.RightBottom:
                Auto_111_111_111 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left |
                 TerrainPeering.Center | TerrainPeering.Right:
                Auto_111_111_000 = temp;
                break;
            case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom |
                 TerrainPeering.Bottom | TerrainPeering.RightBottom:
                Auto_000_111_111 = temp;
                break;
            case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom:
                Auto_011_111_110 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left |
                 TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom:
                Auto_111_111_010 = temp;
                break;
            case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom | TerrainPeering.Bottom:
                Auto_000_110_110 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom:
                Auto_110_111_110 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                 TerrainPeering.LeftBottom | TerrainPeering.Bottom:
                Auto_110_110_110 = temp;
                break;
            case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center:
                Auto_110_110_000 = temp;
                break;

            default:
                Debug.LogError("未知PeeringValue: " + peeringValue);
                break;
        }
    }
}