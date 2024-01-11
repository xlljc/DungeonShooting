
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Godot;
using static TerrainPeering;

/// <summary>
/// 地形配置数据, 数据都为 int 数组, 下标0和1分别代表x和y, 单位: 像素
/// </summary>
public class TileSetTerrainInfo : IClone<TileSetTerrainInfo>
{
    public const byte TerrainLayerType = 1;
    public const byte MiddleLayerType = 2;
    public const byte FloorLayerType = 3;

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
    /// 自动平铺地形 (47块/13块) type = 1
    /// </summary>
    [JsonInclude] public Dictionary<uint, int[]> T;

    /// <summary>
    /// 地形类型, 0: 3x3地形, 1: 2x2地形
    /// </summary>
    [JsonInclude]
    public byte TerrainType;

    public void InitData()
    {
        TerrainType = 0;
        F = new Dictionary<uint, int[]>();
        M = new Dictionary<uint, int[]>();
        T = new Dictionary<uint, int[]>();
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
        terrainInfo.TerrainType = TerrainType;
        return terrainInfo;
    }

    /// <summary>
    /// 返回这个TileSet地形是否可以正常使用了
    /// </summary>
    /// <returns></returns>
    public bool CanUse()
    {
        if (TerrainType == 0)
        {
            if (T == null || T.Count != 47)
            {
                return false;
            }
        }
        else
        {
            if (T == null || T.Count != 13)
            {
                return false;
            }
        }

        return M != null && M.Count == 4 && F != null && F.Count == 1;
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
        if (type == TerrainLayerType)
        {
            return bitCoords.Y * GameConfig.TerrainBit3x3.X + bitCoords.X;
        }
        else if (type == MiddleLayerType)
        {
            return bitCoords.Y * GameConfig.TerrainBitMiddle.X + bitCoords.X;
        }
        else if (type == FloorLayerType)
        {
            return bitCoords.Y * GameConfig.TerrainBitFloor.X + bitCoords.X;
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
        if (type == TerrainLayerType) //顶部墙壁
        {
            if (TerrainType == 0) //47格
            {
                switch (bit)
                {
                    case Center | Bottom: return 0;
                    case Center | Right | Bottom: return 1;
                    case Left | Center | Right | Bottom: return 2;
                    case Left | Center | Bottom: return 3;
                    case LeftTop | Top | Left | Center | Right | Bottom: return 4; 
                    case Left | Center | Right | Bottom | RightBottom: return 5;
                    case Left | Center | Right | LeftBottom | Bottom: return 6;
                    case Top | RightTop | Left | Center | Right | Bottom: return 7;
                    case Center | Right | Bottom | RightBottom: return 8;
                    case Top | Left | Center | Right | LeftBottom | Bottom | RightBottom: return 9;
                    case Left | Center | Right | LeftBottom | Bottom | RightBottom: return 10;
                    case Left | Center | LeftBottom | Bottom: return 11;
                    case Top | Center | Bottom: return 12;
                    case Top | Center | Right | Bottom: return 13;
                    case Top | Left | Center | Right | Bottom: return 14;
                    case Top | Left | Center | Bottom: return 15;
                    case Top | Center | Right | Bottom | RightBottom: return 16;
                    case Top | RightTop | Left | Center | Right | LeftBottom | Bottom | RightBottom: return 17;
                    case LeftTop | Top | Left | Center | Right | LeftBottom | Bottom | RightBottom: return 18;
                    case Top | Left | Center | LeftBottom | Bottom: return 19;
                    case Top | RightTop | Center | Right | Bottom | RightBottom: return 20;
                    case Top | RightTop | Left | Center | Right | LeftBottom | Bottom: return 21;
                    case LeftTop | Top | Left | Center | Right | LeftBottom | Bottom: return 23;
                    case Top | Center: return 24;
                    case Top | Center | Right: return 25;
                    case Top | Left | Center | Right: return 26;
                    case Top | Left | Center: return 27;
                    case Top | RightTop | Center | Right | Bottom: return 28;
                    case LeftTop | Top | RightTop | Left | Center | Right | Bottom | RightBottom: return 29;
                    case LeftTop | Top | RightTop | Left | Center | Right | LeftBottom | Bottom: return 30;
                    case LeftTop | Top | Left | Center | Bottom: return 31;
                    case Top | RightTop | Left | Center | Right | Bottom | RightBottom: return 32;
                    case LeftTop | Top | RightTop | Left | Center | Right | LeftBottom | Bottom | RightBottom: return 33;
                    case LeftTop | Top | Left | Center | Right | Bottom | RightBottom: return 34;
                    case LeftTop | Top | Left | Center | LeftBottom | Bottom: return 35; 
                    case Center: return 36;
                    case Center | Right: return 37;
                    case Left | Center | Right: return 38;
                    case Left | Center: return 39;
                    case Top | Left | Center | Right | LeftBottom | Bottom: return 40;   
                    case Top | RightTop | Left | Center | Right: return 41;
                    case LeftTop | Top | Left | Center | Right: return 42;
                    case Top | Left | Center | Right | Bottom | RightBottom: return 43;  
                    case Top | RightTop | Center | Right: return 44;
                    case LeftTop | Top | RightTop | Left | Center | Right: return 45;    
                    case LeftTop | Top | RightTop | Left | Center | Right | Bottom: return 46;
                    case LeftTop | Top | Left | Center: return 47;
                }
            }
            else if (TerrainType == 1) //13格
            {
                switch (bit)
                {
                    //第一排
                    case Center | RightBottom: return 0;
                    case Center | RightBottom | LeftBottom: return 1;
                    case Center | LeftBottom: return 2;
                    case Center | LeftTop | RightTop | LeftBottom: return 3;
                    case Center | LeftTop | RightTop | RightBottom: return 4;
                    //第二排
                    case Center | RightTop | RightBottom: return 5;
                    case Center | LeftTop | LeftBottom | RightTop | RightBottom: return 6;
                    case Center | LeftTop | LeftBottom: return 7;
                    case Center | LeftTop | LeftBottom | RightBottom: return 8;
                    case Center | RightTop | LeftBottom | RightBottom: return 9;
                    //第三排
                    case Center | RightTop: return 10;
                    case Center | LeftTop | RightBottom: return 11;
                    case Center | LeftTop: return 12;
                }
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
        if (type == TerrainLayerType) //顶部墙壁
        {
            if (TerrainType == 0) //47格
            {
                switch (index)
                {
                    case 0: return Center | Bottom;
                    case 1: return Center | Right | Bottom;
                    case 2: return Left | Center | Right | Bottom;
                    case 3: return Left | Center | Bottom;
                    case 4: return LeftTop | Top | Left | Center | Right | Bottom; 
                    case 5: return Left | Center | Right | Bottom | RightBottom;
                    case 6: return Left | Center | Right | LeftBottom | Bottom;
                    case 7: return Top | RightTop | Left | Center | Right | Bottom;
                    case 8: return Center | Right | Bottom | RightBottom;
                    case 9: return Top | Left | Center | Right | LeftBottom | Bottom | RightBottom;
                    case 10: return Left | Center | Right | LeftBottom | Bottom | RightBottom;
                    case 11: return Left | Center | LeftBottom | Bottom;
                    case 12: return Top | Center | Bottom;
                    case 13: return Top | Center | Right | Bottom;
                    case 14: return Top | Left | Center | Right | Bottom;
                    case 15: return Top | Left | Center | Bottom;
                    case 16: return Top | Center | Right | Bottom | RightBottom;
                    case 17: return Top | RightTop | Left | Center | Right | LeftBottom | Bottom | RightBottom;
                    case 18: return LeftTop | Top | Left | Center | Right | LeftBottom | Bottom | RightBottom;
                    case 19: return Top | Left | Center | LeftBottom | Bottom;
                    case 20: return Top | RightTop | Center | Right | Bottom | RightBottom;
                    case 21: return Top | RightTop | Left | Center | Right | LeftBottom | Bottom;
                    case 23: return LeftTop | Top | Left | Center | Right | LeftBottom | Bottom;
                    case 24: return Top | Center;
                    case 25: return Top | Center | Right;
                    case 26: return Top | Left | Center | Right;
                    case 27: return Top | Left | Center;
                    case 28: return Top | RightTop | Center | Right | Bottom;
                    case 29: return LeftTop | Top | RightTop | Left | Center | Right | Bottom | RightBottom;
                    case 30: return LeftTop | Top | RightTop | Left | Center | Right | LeftBottom | Bottom;
                    case 31: return LeftTop | Top | Left | Center | Bottom;
                    case 32: return Top | RightTop | Left | Center | Right | Bottom | RightBottom;
                    case 33: return LeftTop | Top | RightTop | Left | Center | Right | LeftBottom | Bottom | RightBottom;
                    case 34: return LeftTop | Top | Left | Center | Right | Bottom | RightBottom;
                    case 35: return LeftTop | Top | Left | Center | LeftBottom | Bottom;   
                    case 36: return Center;
                    case 37: return Center | Right;
                    case 38: return Left | Center | Right;
                    case 39: return Left | Center;
                    case 40: return Top | Left | Center | Right | LeftBottom | Bottom;     
                    case 41: return Top | RightTop | Left | Center | Right;
                    case 42: return LeftTop | Top | Left | Center | Right;
                    case 43: return Top | Left | Center | Right | Bottom | RightBottom;    
                    case 44: return Top | RightTop | Center | Right;
                    case 45: return LeftTop | Top | RightTop | Left | Center | Right;      
                    case 46: return LeftTop | Top | RightTop | Left | Center | Right | Bottom;
                    case 47: return LeftTop | Top | Left | Center;
                }
            }
            else if (TerrainType == 1) //13格
            {
                switch (index)
                {
                    //第一排
                    case 0: return Center | RightBottom;
                    case 1: return Center | RightBottom | LeftBottom;
                    case 2: return Center | LeftBottom;
                    case 3: return Center | LeftTop | RightTop | LeftBottom;
                    case 4: return Center | LeftTop | RightTop | RightBottom;
                    //第二排
                    case 5: return Center | RightTop | RightBottom;
                    case 6: return Center | LeftTop | LeftBottom | RightTop | RightBottom;
                    case 7: return Center | LeftTop | LeftBottom;
                    case 8: return Center | LeftTop | LeftBottom | RightBottom;
                    case 9: return Center | RightTop | LeftBottom | RightBottom;
                    //第三排
                    case 10: return Center | RightTop;
                    case 11: return Center | LeftTop | RightBottom;
                    case 12: return Center | LeftTop;
                }
            }
        }
        else if (type == MiddleLayerType)
        {
            if (index >= 0 && index < 4)
            {
                return (uint)index;
            }
        }

        return None;
    }
    
    /// <summary>
    /// 将地形掩码存入 TileSetTerrainInfo 中
    /// </summary>
    public void SetTerrainCell(int index, byte type, int[] cellData)
    {
        if (type == TerrainLayerType) //顶部墙壁
        {
            var terrainBit = IndexToTerrainBit(index, type);
            if (terrainBit != None)
            {
                T[terrainBit] = cellData;
            }
        }
        else if (type == MiddleLayerType) //侧方墙壁
        {
            switch (index)
            {
                case 0: M[0] = cellData; break;
                case 1: M[1] = cellData; break;
                case 2: M[2] = cellData; break;
                case 3: M[3] = cellData; break;
            }
        }
        else if (type == FloorLayerType) //地板
        {
            F[0] = cellData;
        }
    }

    /// <summary>
    /// 移除地形掩码
    /// </summary>
    public void RemoveTerrainCell(int index, byte type)
    {
        if (type == TerrainLayerType) //顶部墙壁
        {
            var terrainBit = IndexToTerrainBit(index, type);
            if (terrainBit != None)
            {
                T.Remove(terrainBit);
            }
        }
        else if (type == MiddleLayerType) //侧方墙壁
        {
            switch (index)
            {
                case 0: M.Remove(0); break;
                case 1: M.Remove(1); break;
                case 2: M.Remove(2); break;
                case 3: M.Remove(3); break;
            }
        }
        else if (type == FloorLayerType) //地板
        {
            F.Remove(0);
        }
    }
    
    /// <summary>
    /// 获取指定索引的地形掩码存储的数据
    /// </summary>
    public int[] GetTerrainCell(int index, byte type)
    {
        if (type == TerrainLayerType) //顶部墙壁
        {
            var terrainBit = IndexToTerrainBit(index, type);
            if (terrainBit != None)
            {
                if (T.TryGetValue(terrainBit, out var cellData))
                {
                    return cellData;
                }
            }
        }
        else if (type == MiddleLayerType) //侧方墙壁
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
        else if (type == FloorLayerType) //地板
        {
            if (F.TryGetValue(0, out var cellData)) return cellData;
        }

        return null;
    }
}