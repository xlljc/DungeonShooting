
using System.Text.Json;
using System.Text.Json.Serialization;

public class TileSetSplit
{
    /// <summary>
    /// 错误类型
    /// </summary>
    [JsonInclude]
    public int ErrorType;

    /// <summary>
    /// 路径
    /// </summary>
    [JsonInclude]
    public string Path;

    /// <summary>
    /// 备注
    /// </summary>
    [JsonInclude]
    public string Remark;

    /// <summary>
    /// 图块集路径
    /// </summary>
    [JsonIgnore]
    public string TileSetPath => Path + "/TileSet.json";

    /// <summary>
    /// 图块集信息
    /// </summary>
    [JsonIgnore]
    public TileSetInfo TileSetInfo
    {
        get
        {
            if (_tileSetInfo == null && TileSetPath != null)
            {
                ReloadTileSetInfo();
            }

            return _tileSetInfo;
        }
    }

    private TileSetInfo _tileSetInfo;

    /// <summary>
    /// 设置图块集信息
    /// </summary>
    public void SetTileSetInfo(TileSetInfo info)
    {
        _tileSetInfo = info;
    }

    /// <summary>
    /// 重新加载图块集信息
    /// </summary>
    public void ReloadTileSetInfo()
    {
        var asText = ResourceManager.LoadText("res://" + TileSetPath);
        _tileSetInfo = JsonSerializer.Deserialize<TileSetInfo>(asText);
    }
}