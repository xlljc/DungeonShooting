
using System.Text.Json;
using System.Text.Json.Serialization;
using Godot;

public class TileSetSplit : IDestroy
{
    [JsonIgnore]
    public bool IsDestroyed { get; private set; }
    
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
    private TileSet _tileSet;

    /// <summary>
    /// 设置图块集信息
    /// </summary>
    public void SetTileSetInfo(TileSetInfo info)
    {
        if (_tileSetInfo != null)
        {
            _tileSetInfo.Destroy();
        }

        if (_tileSet != null)
        {
            _tileSet.Dispose();
            _tileSet = null;
        }
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

    /// <summary>
    /// 获取由该对象生成的Godot.TileSet对象
    /// </summary>
    public TileSet GetTileSet()
    {
        if (_tileSet != null)
        {
            return _tileSet;
        }

        if (TileSetInfo == null)
        {
            return null;
        }
        _tileSet = new TileSet();
        if (_tileSetInfo.Sources != null)
        {
            //TileSet网格大小
            _tileSet.TileSize = GameConfig.TileCellSizeVector2I;
            //物理层
            _tileSet.AddPhysicsLayer();
            _tileSet.SetPhysicsLayerCollisionLayer(0, PhysicsLayer.Wall);
            _tileSet.SetPhysicsLayerCollisionMask(0, PhysicsLayer.None);
            //地形层
            _tileSet.AddTerrainSet();
            _tileSet.SetTerrainSetMode(0, TileSet.TerrainMode.CornersAndSides);
            _tileSet.AddTerrain(0);
            
            //Source资源
            foreach (var tileSetSourceInfo in _tileSetInfo.Sources)
            {
                var tileSetAtlasSource = new TileSetAtlasSource();
                //纹理
                var image = tileSetSourceInfo.GetSourceImage();
                tileSetAtlasSource.Texture = ImageTexture.CreateFromImage(image);
                
                var size = image.GetSize() / GameConfig.TileCellSize;
                //创建cell
                for (var i = 0; i < size.X; i++)
                {
                    for (var j = 0; j < size.Y; j++)
                    {
                        tileSetAtlasSource.CreateTile(new Vector2I(i, j));
                    }
                }
                
                //初始化地形
                tileSetAtlasSource.InitTerrain(tileSetSourceInfo.Terrain);
                
                _tileSet.AddSource(tileSetAtlasSource);
            }
        }

        return _tileSet;
    }
    
    public void Destroy()
    {
        if (IsDestroyed) return;
        IsDestroyed = true;
        
        if (_tileSetInfo != null)
        {
            _tileSetInfo.Destroy();
        }
        
        if (_tileSet != null)
        {
            _tileSet.Dispose();
        }
    }
}