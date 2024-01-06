
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// 图块集数据
/// </summary>
public class TileSetInfo : IClone<TileSetInfo>
{
    /// <summary>
    /// 图块集名称
    /// </summary>
    [JsonInclude]
    public string Name;

    /// <summary>
    /// 资源集合
    /// </summary>
    [JsonInclude]
    public List<TileSetSourceInfo> Sources;

    /// <summary>
    /// 初始化默认数据
    /// </summary>
    public void InitData()
    {
        Sources = new List<TileSetSourceInfo>();
    }
    
    public TileSetInfo Clone()
    {
        var tileSetInfo = new TileSetInfo();
        tileSetInfo.Name = Name;
        if (Sources != null)
        {
            tileSetInfo.Sources = new List<TileSetSourceInfo>();
            foreach (var source in Sources)
            {
                tileSetInfo.Sources.Add(source.Clone());
            }
        }
        return tileSetInfo;
    }

    public void Dispose()
    {
        if (Sources != null)
        {
            foreach (var tileSetSourceInfo in Sources)
            {
                tileSetSourceInfo.Dispose();
            }

            Sources = null;
        }
    }
}