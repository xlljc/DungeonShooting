
using System.Text.Json.Serialization;

/// <summary>
/// 图集资源数据
/// </summary>
public class TileSetSourceInfo : IClone<TileSetSourceInfo>
{
    /// <summary>
    /// 资源名称
    /// </summary>
    [JsonInclude]
    public string Name;
    
    public TileSetSourceInfo Clone()
    {
        var tileSetSourceInfo = new TileSetSourceInfo();
        tileSetSourceInfo.Name = Name;
        return tileSetSourceInfo;
    }
}