
using System.Text.Json.Serialization;
using Godot;

/// <summary>
/// 地形配置数据
/// </summary>
public class TileSetTerrainInfo
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
}