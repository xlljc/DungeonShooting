
using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 地形配置数据, 数据都为 int 数组, 下标0和1分别代表x和y, 单位: 像素
/// </summary>
public class TileSetTerrainInfo : IClone<TileSetTerrainInfo>
{
    //---------------------- 地板 ----------------------
    
    [JsonInclude] public int[] _f;
    
    //---------------------- 侧方墙壁 --------------------------
    
    [JsonInclude] public int[] _vl;
    [JsonInclude] public int[] _vc;
    [JsonInclude] public int[] _vr;
    [JsonInclude] public int[] _vs;
    
    //---------------------- 顶部墙壁47格 ----------------------
    
    //第一列
    [JsonInclude] public int[] _000_010_010;
    [JsonInclude] public int[] _010_010_010;
    [JsonInclude] public int[] _010_010_000;

    [JsonInclude] public int[] _000_010_000;

    //第二列
    [JsonInclude] public int[] _000_011_010;
    [JsonInclude] public int[] _010_011_010;
    [JsonInclude] public int[] _010_011_000;

    [JsonInclude] public int[] _000_011_000;

    //第三列
    [JsonInclude] public int[] _000_111_010;
    [JsonInclude] public int[] _010_111_010;
    [JsonInclude] public int[] _010_111_000;

    [JsonInclude] public int[] _000_111_000;

    //第四列
    [JsonInclude] public int[] _000_110_010;
    [JsonInclude] public int[] _010_110_010;
    [JsonInclude] public int[] _010_110_000;

    [JsonInclude] public int[] _000_110_000;

    //第五列
    [JsonInclude] public int[] _110_111_010;
    [JsonInclude] public int[] _010_011_011;
    [JsonInclude] public int[] _011_011_010;

    [JsonInclude] public int[] _010_111_110;

    //第六列
    [JsonInclude] public int[] _000_111_011;
    [JsonInclude] public int[] _011_111_111;
    [JsonInclude] public int[] _111_111_011;

    [JsonInclude] public int[] _011_111_000;

    //第七列
    [JsonInclude] public int[] _000_111_110;
    [JsonInclude] public int[] _110_111_111;
    [JsonInclude] public int[] _111_111_110;

    [JsonInclude] public int[] _110_111_000;

    //第八列
    [JsonInclude] public int[] _011_111_010;
    [JsonInclude] public int[] _010_110_110;
    [JsonInclude] public int[] _110_110_010;

    [JsonInclude] public int[] _010_111_011;

    //第九列
    [JsonInclude] public int[] _000_011_011;
    [JsonInclude] public int[] _011_011_011;
    [JsonInclude] public int[] _011_111_011;

    [JsonInclude] public int[] _011_011_000;

    //第十列
    [JsonInclude] public int[] _010_111_111;
    [JsonInclude] public int[] _110_111_011;
    [JsonInclude] public int[] _111_111_111;

    [JsonInclude] public int[] _111_111_000;

    //第十一列
    [JsonInclude] public int[] _000_111_111;
    [JsonInclude] public int[] _011_111_110;

    [JsonInclude] public int[] _111_111_010;

    //第十二列
    [JsonInclude] public int[] _000_110_110;
    [JsonInclude] public int[] _110_111_110;
    [JsonInclude] public int[] _110_110_110;
    [JsonInclude] public int[] _110_110_000;

    public Vector2I GetPosition(int[] ints)
    {
        return new Vector2I(ints[0], ints[1]);
    }

    public TileSetTerrainInfo Clone()
    {
        var terrainInfo = new TileSetTerrainInfo();
        if (_f != null) terrainInfo._f = new[] { _f[0], _f[1] };

        // 地板
        terrainInfo._f = (_f != null) ? new[] { _f[0], _f[1] } : null;

        // 侧方墙壁
        terrainInfo._vl = (_vl != null) ? new[] { _vl[0], _vl[1] } : null;
        terrainInfo._vc = (_vc != null) ? new[] { _vc[0], _vc[1] } : null;
        terrainInfo._vr = (_vr != null) ? new[] { _vr[0], _vr[1] } : null;
        terrainInfo._vs = (_vs != null) ? new[] { _vs[0], _vs[1] } : null;

        // 顶部墙壁47格
        // 第一列
        terrainInfo._000_010_010 = (_000_010_010 != null) ? new[] { _000_010_010[0], _000_010_010[1] } : null;
        terrainInfo._010_010_010 = (_010_010_010 != null) ? new[] { _010_010_010[0], _010_010_010[1] } : null;
        terrainInfo._010_010_000 = (_010_010_000 != null) ? new[] { _010_010_000[0], _010_010_000[1] } : null;
        terrainInfo._000_010_000 = (_000_010_000 != null) ? new[] { _000_010_000[0], _000_010_000[1] } : null;

        // 第二列
        terrainInfo._000_011_010 = (_000_011_010 != null) ? new[] { _000_011_010[0], _000_011_010[1] } : null;
        terrainInfo._010_011_010 = (_010_011_010 != null) ? new[] { _010_011_010[0], _010_011_010[1] } : null;
        terrainInfo._010_011_000 = (_010_011_000 != null) ? new[] { _010_011_000[0], _010_011_000[1] } : null;
        terrainInfo._000_011_000 = (_000_011_000 != null) ? new[] { _000_011_000[0], _000_011_000[1] } : null;
        
        // 第三列
        terrainInfo._000_111_010 = (_000_111_010 != null) ? new[] { _000_111_010[0], _000_111_010[1] } : null;
        terrainInfo._010_111_010 = (_010_111_010 != null) ? new[] { _010_111_010[0], _010_111_010[1] } : null;
        terrainInfo._010_111_000 = (_010_111_000 != null) ? new[] { _010_111_000[0], _010_111_000[1] } : null;
        terrainInfo._000_111_000 = (_000_111_000 != null) ? new[] { _000_111_000[0], _000_111_000[1] } : null;
        
        // 第四列
        terrainInfo._000_110_010 = (_000_110_010 != null) ? new[] { _000_110_010[0], _000_110_010[1] } : null;
        terrainInfo._010_110_010 = (_010_110_010 != null) ? new[] { _010_110_010[0], _010_110_010[1] } : null;
        terrainInfo._010_110_000 = (_010_110_000 != null) ? new[] { _010_110_000[0], _010_110_000[1] } : null;
        terrainInfo._000_110_000 = (_000_110_000 != null) ? new[] { _000_110_000[0], _000_110_000[1] } : null;
        
        // 第五列
        terrainInfo._110_111_010 = (_110_111_010 != null) ? new[] { _110_111_010[0], _110_111_010[1] } : null;
        terrainInfo._010_011_011 = (_010_011_011 != null) ? new[] { _010_011_011[0], _010_011_011[1] } : null;
        terrainInfo._011_011_010 = (_011_011_010 != null) ? new[] { _011_011_010[0], _011_011_010[1] } : null;
        terrainInfo._010_111_110 = (_010_111_110 != null) ? new[] { _010_111_110[0], _010_111_110[1] } : null;
        
        // 第六列
        terrainInfo._000_111_011 = (_000_111_011 != null) ? new[] { _000_111_011[0], _000_111_011[1] } : null;
        terrainInfo._011_111_111 = (_011_111_111 != null) ? new[] { _011_111_111[0], _011_111_111[1] } : null;
        terrainInfo._111_111_011 = (_111_111_011 != null) ? new[] { _111_111_011[0], _111_111_011[1] } : null;
        terrainInfo._011_111_000 = (_011_111_000 != null) ? new[] { _011_111_000[0], _011_111_000[1] } : null;
        
        // 第七列
        terrainInfo._000_111_110 = (_000_111_110 != null) ? new[] { _000_111_110[0], _000_111_110[1] } : null;
        terrainInfo._110_111_111 = (_110_111_111 != null) ? new[] { _110_111_111[0], _110_111_111[1] } : null;
        terrainInfo._111_111_110 = (_111_111_110 != null) ? new[] { _111_111_110[0], _111_111_110[1] } : null;
        terrainInfo._110_111_000 = (_110_111_000 != null) ? new[] { _110_111_000[0], _110_111_000[1] } : null;
        
        // 第八列
        terrainInfo._011_111_010 = (_011_111_010 != null) ? new[] { _011_111_010[0], _011_111_010[1] } : null;
        terrainInfo._010_110_110 = (_010_110_110 != null) ? new[] { _010_110_110[0], _010_110_110[1] } : null;
        terrainInfo._110_110_010 = (_110_110_010 != null) ? new[] { _110_110_010[0], _110_110_010[1] } : null;
        terrainInfo._010_111_011 = (_010_111_011 != null) ? new[] { _010_111_011[0], _010_111_011[1] } : null;
        
        // 第九列
        terrainInfo._000_011_011 = (_000_011_011 != null) ? new[] { _000_011_011[0], _000_011_011[1] } : null;
        terrainInfo._011_011_011 = (_011_011_011 != null) ? new[] { _011_011_011[0], _011_011_011[1] } : null;
        terrainInfo._011_111_011 = (_011_111_011 != null) ? new[] { _011_111_011[0], _011_111_011[1] } : null;
        terrainInfo._011_011_000 = (_011_011_000 != null) ? new[] { _011_011_000[0], _011_011_000[1] } : null;
        
        // 第十列
        terrainInfo._010_111_111 = (_010_111_111 != null) ? new[] { _010_111_111[0], _010_111_111[1] } : null;
        terrainInfo._110_111_011 = (_110_111_011 != null) ? new[] { _110_111_011[0], _110_111_011[1] } : null;
        terrainInfo._111_111_111 = (_111_111_111 != null) ? new[] { _111_111_111[0], _111_111_111[1] } : null;
        terrainInfo._111_111_000 = (_111_111_000 != null) ? new[] { _111_111_000[0], _111_111_000[1] } : null;
        
        // 第十一列
        terrainInfo._000_111_111 = (_000_111_111 != null) ? new[] { _000_111_111[0], _000_111_111[1] } : null;
        terrainInfo._011_111_110 = (_011_111_110 != null) ? new[] { _011_111_110[0], _011_111_110[1] } : null;
        terrainInfo._111_111_010 = (_111_111_010 != null) ? new[] { _111_111_010[0], _111_111_010[1] } : null;
        
        // 第十二列
        terrainInfo._000_110_110 = (_000_110_110 != null) ? new[] { _000_110_110[0], _000_110_110[1] } : null;
        terrainInfo._110_111_110 = (_110_111_110 != null) ? new[] { _110_111_110[0], _110_111_110[1] } : null;
        terrainInfo._110_110_110 = (_110_110_110 != null) ? new[] { _110_110_110[0], _110_110_110[1] } : null;
        terrainInfo._110_110_000 = (_110_110_000 != null) ? new[] { _110_110_000[0], _110_110_000[1] } : null;
        
        return terrainInfo;
    }
}