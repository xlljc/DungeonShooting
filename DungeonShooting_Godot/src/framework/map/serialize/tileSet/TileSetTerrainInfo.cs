
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 地形配置数据, 数据都为 int 数组, 下标0和1分别代表x和y, 单位: 像素
/// </summary>
public class TileSetTerrainInfo : IClone<TileSetTerrainInfo>
{
    //---------------------- 地板 ----------------------

    [JsonInclude] public Dictionary<uint, int[]> F;
    
    //---------------------- 侧方墙壁 --------------------------
    
    [JsonInclude] public Dictionary<uint, int[]> M;
    
    //---------------------- 顶部墙壁47格 ----------------------
    
    [JsonInclude] public Dictionary<uint, int[]> T;

    public void InitData()
    {
        T = new Dictionary<uint, int[]>();
        M = new Dictionary<uint, int[]>();
        F = new Dictionary<uint, int[]>();
    }

    /// <summary>
    /// 将存储的坐标数据转换成 Vector2I 对象, 返回的 Vector2I 单位: 格
    /// </summary>
    public Vector2I GetPosition(int[] ints)
    {
        return new Vector2I(ints[0] / GameConfig.TileCellSize, ints[1] / GameConfig.TileCellSize);
    }
    
    public uint TerrainCoordsToIndex(Vector2I coords)
    {
        return (uint)(coords.Y * GameConfig.TerrainBitSize1.X + coords.X);
    }

    public TileSetTerrainInfo Clone()
    {
        var terrainInfo = new TileSetTerrainInfo();
        terrainInfo.InitData();
        foreach (var pair in T)
        {
            terrainInfo.T.Add(pair.Key, new []{ pair.Value[0], pair.Value[1] });
        }
        foreach (var pair in M)
        {
            terrainInfo.M.Add(pair.Key, new []{ pair.Value[0], pair.Value[1] });
        }
        foreach (var pair in F)
        {
            terrainInfo.F.Add(pair.Key, new []{ pair.Value[0], pair.Value[1] });
        }
        return terrainInfo;
    }
    
    /// <summary>
    /// 将地形掩码存入 TileSetTerrainInfo 中
    /// </summary>
    public void SetTerrainBit(int index, byte type, int[] cellData)
    {
        if (type == 1) //顶部墙壁
        {
            switch (index)
            {
                case 0: T[TerrainPeering.Center| TerrainPeering.Bottom] = cellData; break;
                case 1: T[TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom] = cellData; break;
                case 2: T[TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom] = cellData; break;
                case 3: T[TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Bottom] = cellData; break;
                case 4: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom] = cellData; break;
                case 5: T[TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 6: T[TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom] = cellData; break;
                case 7: T[TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom] = cellData; break;
                case 8: T[TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 9: T[TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 10: T[TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 11: T[TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.LeftBottom| TerrainPeering.Bottom] = cellData; break;
                case 12: T[TerrainPeering.Top| TerrainPeering.Center| TerrainPeering.Bottom] = cellData; break;
                case 13: T[TerrainPeering.Top| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom] = cellData; break;
                case 14: T[TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom] = cellData; break;      
                case 15: T[TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Bottom] = cellData; break;
                case 16: T[TerrainPeering.Top| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 17: T[TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 18: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 19: T[TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.LeftBottom| TerrainPeering.Bottom] = cellData; break; 
                case 20: T[TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 21: T[TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom] = cellData; break;
                case 22: break;
                case 23: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom] = cellData; break;
                case 24: T[TerrainPeering.Top| TerrainPeering.Center] = cellData; break;
                case 25: T[TerrainPeering.Top| TerrainPeering.Center| TerrainPeering.Right] = cellData; break;
                case 26: T[TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right] = cellData; break;
                case 27: T[TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center] = cellData; break;
                case 28: T[TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom] = cellData; break;  
                case 29: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 30: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom] = cellData; break;
                case 31: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Bottom] = cellData; break;
                case 32: T[TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 33: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 34: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 35: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.LeftBottom| TerrainPeering.Bottom] = cellData; break;
                case 36: T[TerrainPeering.Center] = cellData; break;
                case 37: T[TerrainPeering.Center| TerrainPeering.Right] = cellData; break;
                case 38: T[TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right] = cellData; break;
                case 39: T[TerrainPeering.Left| TerrainPeering.Center] = cellData; break;
                case 40: T[TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.LeftBottom| TerrainPeering.Bottom] = cellData; break;
                case 41: T[TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right] = cellData; break;    
                case 42: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right] = cellData; break;     
                case 43: T[TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom| TerrainPeering.RightBottom] = cellData; break;
                case 44: T[TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Center| TerrainPeering.Right] = cellData; break;
                case 45: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right] = cellData; break;
                case 46: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.RightTop| TerrainPeering.Left| TerrainPeering.Center| TerrainPeering.Right| TerrainPeering.Bottom] = cellData; break;
                case 47: T[TerrainPeering.LeftTop| TerrainPeering.Top| TerrainPeering.Left| TerrainPeering.Center] = cellData; break;
            }
        }
        else if (type == 2) //侧方墙壁
        {
            switch (index)
            {
                case 0: M[0] = cellData; break;
                case 1: M[1] = cellData; break;
                case 2: M[2] = cellData; break;
                case 3: M[3] = cellData; break;
            }
        }
        else if (type == 3) //地板
        {
            F[0] = cellData;
        }
    }
    
    /// <summary>
    /// 获取指定索引的地形掩码存储的数据
    /// </summary>
    public int[] GetTerrainBit(int index, byte type)
    {
        if (type == 1) //顶部墙壁
        {
            switch (index)
            {
                case 0:
                {
                    if (T.TryGetValue(TerrainPeering.Center | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 1:
                {
                    if (T.TryGetValue(TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 2:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 3:
                {
                    if (T.TryGetValue(TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 4:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 5:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom |
                            TerrainPeering.RightBottom, out var cellData))
                        return cellData;
                }
                    break;
                case 6:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                            TerrainPeering.LeftBottom | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 7:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 8:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom |
                            TerrainPeering.RightBottom, out var cellData))
                        return cellData;
                }
                    break;
                case 9:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                            TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 10:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                            TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 11:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom |
                            TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 12:
                {
                    if (T.TryGetValue(TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Bottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 13:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 14:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                            TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 15:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 16:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom |
                            TerrainPeering.RightBottom, out var cellData))
                        return cellData;
                }
                    break;
                case 17:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom |
                            TerrainPeering.RightBottom, out var cellData))
                        return cellData;
                }
                    break;
                case 18:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom |
                            TerrainPeering.RightBottom, out var cellData))
                        return cellData;
                }
                    break;
                case 19:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.LeftBottom | TerrainPeering.Bottom, out var
                                cellData))
                        return cellData;
                }
                    break;
                case 20:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 21:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 22:
                    break;
                case 23:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 24:
                {
                    if (T.TryGetValue(TerrainPeering.Top | TerrainPeering.Center, out var cellData))
                        return cellData;
                }
                    break;
                case 25:
                {
                    if (T.TryGetValue(TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 26:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 27:
                {
                    if (T.TryGetValue(TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 28:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 29:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop |
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom |
                            TerrainPeering.RightBottom, out var cellData))
                        return cellData;
                }
                    break;
                case 30:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop |
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                            TerrainPeering.LeftBottom | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 31:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 32:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 33:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop |
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                            TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 34:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 35:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.LeftBottom | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 36:
                {
                    if (T.TryGetValue(TerrainPeering.Center, out var cellData))
                        return cellData;
                }
                    break;
                case 37:
                {
                    if (T.TryGetValue(TerrainPeering.Center | TerrainPeering.Right, out var cellData))
                        return cellData;
                }
                    break;
                case 38:
                {
                    if (T.TryGetValue(TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 39:
                {
                    if (T.TryGetValue(TerrainPeering.Left | TerrainPeering.Center, out var cellData))
                        return cellData;
                }
                    break;
                case 40:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                            TerrainPeering.LeftBottom | TerrainPeering.Bottom, out var cellData))
                        return cellData;
                }
                    break;
                case 41:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right, out var cellData))
                        return cellData;
                }
                    break;
                case 42:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center |
                            TerrainPeering.Right, out var cellData))
                        return cellData;
                }
                    break;
                case 43:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right |
                            TerrainPeering.Bottom | TerrainPeering.RightBottom, out var cellData))
                        return cellData;
                }
                    break;
                case 44:
                {
                    if (T.TryGetValue(
                            TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 45:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop |
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right, out var cellData))
                        return cellData;
                }
                    break;
                case 46:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop |
                            TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom,
                            out var cellData))
                        return cellData;
                }
                    break;
                case 47:
                {
                    if (T.TryGetValue(
                            TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center,
                            out var cellData))
                        return cellData;
                }
                    break;
            }
        }
        else if (type == 2) //侧方墙壁
        {
            switch (index)
            {
                case 0:
                {
                    if (M.TryGetValue(0, out var cellData)) return cellData;
                }
                    break;
                case 1:
                {
                    if (M.TryGetValue(1, out var cellData)) return cellData;
                }
                    break;
                case 2:
                {
                    if (M.TryGetValue(2, out var cellData)) return cellData;
                }
                    break;
                case 3:
                {
                    if (M.TryGetValue(3, out var cellData)) return cellData;
                }
                    break;
            }
        }
        else if (type == 3) //地板
        {
            if (F.TryGetValue(0, out var cellData)) return cellData;
        }

        return null;
    }
}