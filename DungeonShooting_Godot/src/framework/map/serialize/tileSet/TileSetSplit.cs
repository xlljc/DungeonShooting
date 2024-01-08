
using System.Collections.Generic;
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
            //物理层 0
            _tileSet.AddPhysicsLayer();
            _tileSet.SetPhysicsLayerCollisionLayer(0, PhysicsLayer.Wall);
            _tileSet.SetPhysicsLayerCollisionMask(0, PhysicsLayer.Wall);
            Debug.Log("GetPhysicsLayersCount: " + _tileSet.GetPhysicsLayersCount());
            //地形层 0
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
                
                var terrainInfo = tileSetSourceInfo.Terrain;
                //初始化地形
                tileSetAtlasSource.InitTerrain(terrainInfo, 0, 0);
                //ySort
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(0, 2), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(1, 2), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(2, 2), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(3, 2), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(0, 3), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(1, 3), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(2, 3), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(3, 3), 23);

                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(5, 3), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(6, 3), 23);

                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(8, 3), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(9, 3), 23);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 1, new Vector2I(11, 3), 23);
                
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 2, new Vector2I(0, 0), 7);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 2, new Vector2I(1, 0), 7);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 2, new Vector2I(2, 0), 7);
                SetAtlasSourceYSortOrigin(terrainInfo, tileSetAtlasSource, 2, new Vector2I(3, 0), 7);
                
                //碰撞器
                var index = terrainInfo.TerrainCoordsToIndex(new Vector2I(0, 0), 1);
                var cellData = terrainInfo.GetTerrainBit(index, 1);
                var pos = terrainInfo.GetPosition(cellData);
                var tileData = tileSetAtlasSource.GetTileData(pos, 0);
                tileData.AddCollisionPolygon(0);
                tileData.SetCollisionPolygonPoints(0, 0, new []
                {
                    new Vector2(0, 0),
                    new Vector2(8, 0),
                    new Vector2(8, 8),
                    new Vector2(0, 8),
                });

                _tileSet.AddSource(tileSetAtlasSource);
            }
        }

        return _tileSet;
    }

    private static void SetAtlasSourceYSortOrigin(TileSetTerrainInfo terrainInfo, TileSetAtlasSource tileSetAtlasSource, byte type, Vector2I coords, int ySortOrigin)
    {
        var index = terrainInfo.TerrainCoordsToIndex(coords, type);
        var cellData = terrainInfo.GetTerrainBit(index, type);
        if (cellData != null)
        {
            var pos = terrainInfo.GetPosition(cellData);
            var tileData = tileSetAtlasSource.GetTileData(pos, 0);
            tileData.YSortOrigin = ySortOrigin;
        }
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