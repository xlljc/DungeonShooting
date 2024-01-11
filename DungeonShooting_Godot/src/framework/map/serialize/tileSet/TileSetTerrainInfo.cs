
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 地形配置数据, 数据都为 int 数组, 下标0和1分别代表x和y, 单位: 像素
/// </summary>
public class TileSetTerrainInfo : IClone<TileSetTerrainInfo>
{
    public const byte TopLayerType = 1;
    public const byte MiddleLayerType = 2;
    public const byte FloorLayerType = 3;
    public const byte Terrain2x2Type = 4;

    //type = 3
    /// <summary>
    /// 地板 (1块) type = 3
    /// </summary>
    [JsonInclude] public Dictionary<uint, int[]> F;

    /// <summary>
    /// 侧方墙壁 (4块) type = 2
    /// </summary>
    [JsonInclude] public Dictionary<uint, int[]> M;
    
    /// <summary>
    /// 顶部墙壁47格 (47块) type = 1
    /// </summary>
    [JsonInclude] public Dictionary<uint, int[]> T;

    public void InitData()
    {
        T = new Dictionary<uint, int[]>();
        M = new Dictionary<uint, int[]>();
        F = new Dictionary<uint, int[]>();
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
    /// 返回这个TileSet地形是否可以正常使用了
    /// </summary>
    /// <returns></returns>
    public bool CanUse()
    {
        return T != null && T.Count == 47 && M != null && M.Count == 4 && F != null && F.Count == 1;
    }

    /// <summary>
    /// 将存储的坐标数据转换成 Vector2I 对象, 返回的 Vector2I 单位: 格
    /// </summary>
    public Vector2I GetPosition(int[] ints)
    {
        return new Vector2I(ints[0] / GameConfig.TileCellSize, ints[1] / GameConfig.TileCellSize);
    }
    
    /// <summary>
    /// 将地形掩码中的坐标转换为索引。
    /// </summary>
    /// <param name="bitCoords">地形坐标</param>
    /// <param name="type">地形类型</param>
    public int TerrainCoordsToIndex(Vector2I bitCoords, byte type)
    {
        if (type == 1)
        {
            return bitCoords.Y * GameConfig.TerrainBitSize1.X + bitCoords.X;
        }
        else if (type == 2)
        {
            return bitCoords.Y * GameConfig.TerrainBitSize2.X + bitCoords.X;
        }
        else if (type == 3)
        {
            return bitCoords.Y * GameConfig.TerrainBitSize3.X + bitCoords.X;
        }

        return -1;
    }

    /// <summary>
    /// 将地掩码值转换为索引值
    /// </summary>
    /// <param name="bit">地形掩码值</param>
    /// <param name="type">地形类型</param>
    public int TerrainBitToIndex(uint bit, byte type)
    {
        if (type == TopLayerType) //顶部墙壁
        {
            switch (bit)
            {
                case TerrainPeering.Center | TerrainPeering.Bottom: return 0;
                case TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom: return 1;
                case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom: return 2;
                case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom: return 3;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom: return 4; 
                case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 5;
                case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom: return 6;
                case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom: return 7;
                case TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 8;
                case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 9;
                case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 10;
                case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom | TerrainPeering.Bottom: return 11;
                case TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Bottom: return 12;
                case TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom: return 13;
                case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom: return 14;
                case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom: return 15;
                case TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 16;
                case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 17;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 18;
                case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom | TerrainPeering.Bottom: return 19;
                case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 20;
                case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom: return 21;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom: return 23;
                case TerrainPeering.Top | TerrainPeering.Center: return 24;
                case TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right: return 25;
                case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right: return 26;
                case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center: return 27;
                case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom: return 28;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 29;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom: return 30;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom: return 31;
                case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 32;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 33;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 34;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom | TerrainPeering.Bottom: return 35; 
                case TerrainPeering.Center: return 36;
                case TerrainPeering.Center | TerrainPeering.Right: return 37;
                case TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right: return 38;
                case TerrainPeering.Left | TerrainPeering.Center: return 39;
                case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom: return 40;   
                case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right: return 41;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right: return 42;
                case TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom: return 43;  
                case TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right: return 44;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right: return 45;    
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom: return 46;
                case TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center: return 47;
            }
        }
        else if (type == MiddleLayerType)
        {
            if (bit < 4)
            {
                return (int)bit;
            }
        }
        else if (type == FloorLayerType)
        {
            if (bit == 0)
            {
                return 0;
            }
        }

        return -1;
    }

    /// <summary>
    /// 根据给定的索引和类型计算地形掩码值。
    /// </summary>
    /// <param name="index">用于确定地形掩码的索引。</param>
    /// <param name="type">要计算地形位值的图层类型。</param>
    /// <returns></returns>
    public uint IndexToTerrainBit(int index, byte type)
    {
        if (type == TopLayerType) //顶部墙壁
        {
            switch (index)
            {
                case 0: return TerrainPeering.Center | TerrainPeering.Bottom;
                case 1: return TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom;
                case 2: return TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom;
                case 3: return TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom;
                case 4: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom; 
                case 5: return TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 6: return TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom;
                case 7: return TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom;
                case 8: return TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 9: return TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 10: return TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 11: return TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom | TerrainPeering.Bottom;
                case 12: return TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Bottom;
                case 13: return TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom;
                case 14: return TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom;
                case 15: return TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom;
                case 16: return TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 17: return TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 18: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 19: return TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom | TerrainPeering.Bottom;
                case 20: return TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom;                case 21: return TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom;
                case 23: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom;
                case 24: return TerrainPeering.Top | TerrainPeering.Center;
                case 25: return TerrainPeering.Top | TerrainPeering.Center | TerrainPeering.Right;
                case 26: return TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right;
                case 27: return TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center;
                case 28: return TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom;
                case 29: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 30: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom;
                case 31: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Bottom;
                case 32: return TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 33: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 34: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom;
                case 35: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.LeftBottom | TerrainPeering.Bottom;   
                case 36: return TerrainPeering.Center;
                case 37: return TerrainPeering.Center | TerrainPeering.Right;
                case 38: return TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right;
                case 39: return TerrainPeering.Left | TerrainPeering.Center;
                case 40: return TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.LeftBottom | TerrainPeering.Bottom;     
                case 41: return TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right;
                case 42: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right;
                case 43: return TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom | TerrainPeering.RightBottom;    
                case 44: return TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Center | TerrainPeering.Right;
                case 45: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right;      
                case 46: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.RightTop | TerrainPeering.Left | TerrainPeering.Center | TerrainPeering.Right | TerrainPeering.Bottom;
                case 47: return TerrainPeering.LeftTop | TerrainPeering.Top | TerrainPeering.Left | TerrainPeering.Center;
            }
        }
        else if (type == MiddleLayerType)
        {
            if (index >= 0 && index < 4)
            {
                return (uint)index;
            }
        }

        return TerrainPeering.None;
    }
    
    /// <summary>
    /// 将地形掩码存入 TileSetTerrainInfo 中
    /// </summary>
    public void SetTerrainCell(int index, byte type, int[] cellData)
    {
        if (type == 1) //顶部墙壁
        {
            var terrainBit = IndexToTerrainBit(index, type);
            if (terrainBit != TerrainPeering.None)
            {
                T[terrainBit] = cellData;
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
    /// 移除地形掩码
    /// </summary>
    public void RemoveTerrainCell(int index, byte type)
    {
        if (type == 1) //顶部墙壁
        {
            var terrainBit = IndexToTerrainBit(index, type);
            if (terrainBit != TerrainPeering.None)
            {
                T.Remove(terrainBit);
            }
        }
        else if (type == 2) //侧方墙壁
        {
            switch (index)
            {
                case 0: M.Remove(0); break;
                case 1: M.Remove(1); break;
                case 2: M.Remove(2); break;
                case 3: M.Remove(3); break;
            }
        }
        else if (type == 3) //地板
        {
            F.Remove(0);
        }
    }
    
    /// <summary>
    /// 获取指定索引的地形掩码存储的数据
    /// </summary>
    public int[] GetTerrainCell(int index, byte type)
    {
        if (type == 1) //顶部墙壁
        {
            var terrainBit = IndexToTerrainBit(index, type);
            if (terrainBit != TerrainPeering.None)
            {
                if (T.TryGetValue(terrainBit, out var cellData))
                {
                    return cellData;
                }
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