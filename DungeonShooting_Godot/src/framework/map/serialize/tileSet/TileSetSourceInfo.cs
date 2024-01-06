
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Godot;

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

    /// <summary>
    /// 资源路径
    /// </summary>
    [JsonInclude]
    public string SourcePath;

    /// <summary>
    /// 地形配置数据
    /// </summary>
    [JsonInclude]
    public TileSetTerrainInfo Terrain;

    /// <summary>
    /// 组合数据
    /// </summary>
    [JsonInclude]
    public List<TileCombinationInfo> Combination;
    
    [JsonIgnore]
    private Image _sourceImage;
    [JsonIgnore]
    private bool _overWriteImage;

    /// <summary>
    /// 初始化默认数据
    /// </summary>
    public void InitData()
    {
        Terrain = new TileSetTerrainInfo();
        Combination = new List<TileCombinationInfo>();
    }

    /// <summary>
    /// 是否重写过Image
    /// </summary>
    public bool IsOverWriteImage()
    {
        return _overWriteImage;
    }
    
    /// <summary>
    /// 获取资源图像数据
    /// </summary>
    public Image GetSourceImage()
    {
        if (_sourceImage == null && string.IsNullOrEmpty(SourcePath))
        {
            return null;
        }
        return _sourceImage ??= Image.LoadFromFile(SourcePath);
    }
    
    /// <summary>
    /// 设置图像资源
    /// </summary>
    public void SetSourceImage(Image image)
    {
        _overWriteImage = true;
        _sourceImage = image;
    }

    public TileSetSourceInfo Clone()
    {
        var tileSetSourceInfo = new TileSetSourceInfo();
        tileSetSourceInfo.Name = Name;
        tileSetSourceInfo.Terrain = Terrain;
        tileSetSourceInfo.Combination = Combination;
        tileSetSourceInfo.SourcePath = SourcePath;
        tileSetSourceInfo._sourceImage = _sourceImage;
        return tileSetSourceInfo;
    }
}