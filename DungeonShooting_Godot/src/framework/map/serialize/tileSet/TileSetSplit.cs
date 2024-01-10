﻿
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
        
        if (_tileSet != null)
        {
            _tileSet.Dispose();
            _tileSet = null;
        }
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
            _tileSet.SetPhysicsLayerCollisionMask(0, PhysicsLayer.None);
            Debug.Log("GetPhysicsLayersCount: " + _tileSet.GetPhysicsLayersCount());
            //地形层 0
            _tileSet.AddTerrainSet();
            _tileSet.SetTerrainSetMode(0, TileSet.TerrainMode.CornersAndSides);
            _tileSet.AddTerrain(0);
            
            //Source资源
            foreach (var tileSetSourceInfo in _tileSetInfo.Sources)
            {
                var tileSetAtlasSource = new TileSetAtlasSource();
                _tileSet.AddSource(tileSetAtlasSource);
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
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(0, 0), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(1, 0), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(2, 0), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(3, 0), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(4, 0), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(5, 0), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(6, 0), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(7, 0), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(8, 0), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(9, 0), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(10, 0), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(11, 0), true);
                
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(0, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(1, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(2, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(3, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(4, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(5, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(6, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(7, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(8, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(9, 1), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(11, 1), false);
                
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(0, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(1, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(2, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(3, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(4, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(5, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(6, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(7, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(8, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(9, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(10, 2), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(11, 2), false);
                
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(0, 3), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(1, 3), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(2, 3), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(3, 3), true);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(4, 3), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(5, 3), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(6, 3), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(7, 3), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(8, 3), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(9, 3), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(10, 3), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(11, 3), false);
                                
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(0, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(1, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(2, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(3, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(4, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(5, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(6, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(7, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(8, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(9, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(10, 4), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 1, new Vector2I(11, 4), false);
                
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 2, new Vector2I(0, 0), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 2, new Vector2I(1, 0), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 2, new Vector2I(2, 0), false);
                SetAtlasSourceCollision(terrainInfo, tileSetAtlasSource, 2, new Vector2I(3, 0), false);
            }
        }

        return _tileSet;
    }

    /// <summary>
    /// 设置地图块的 Y 排序原点。
    /// </summary>
    private static void SetAtlasSourceYSortOrigin(TileSetTerrainInfo terrainInfo, TileSetAtlasSource tileSetAtlasSource, byte type, Vector2I bitCoords, int ySortOrigin)
    {
        var index = terrainInfo.TerrainCoordsToIndex(bitCoords, type);
        var cellData = terrainInfo.GetTerrainCell(index, type);
        if (cellData != null)
        {
            var pos = terrainInfo.GetPosition(cellData);
            var tileData = tileSetAtlasSource.GetTileData(pos, 0);
            tileData.YSortOrigin = ySortOrigin;
        }
    }

    /// <summary>
    /// 设置Atlas源碰撞信息, isHalf: 是否半块
    /// </summary>
    private static void SetAtlasSourceCollision(TileSetTerrainInfo terrainInfo, TileSetAtlasSource tileSetAtlasSource, byte type, Vector2I bitCoords, bool isHalf)
    {
        var index = terrainInfo.TerrainCoordsToIndex(bitCoords, type);
        var cellData = terrainInfo.GetTerrainCell(index, type);
        if (cellData != null)
        {
            var pos = terrainInfo.GetPosition(cellData);
            var tileData = tileSetAtlasSource.GetTileData(pos, 0);
            tileData.AddCollisionPolygon(0);
            if (isHalf)
            {
                tileData.SetCollisionPolygonPoints(0, 0, new[]
                {
                    new Vector2(-8, 0),
                    new Vector2(8, 0),
                    new Vector2(8, 8),
                    new Vector2(-8, 8)
                });
            }
            else
            {
                tileData.SetCollisionPolygonPoints(0, 0, new []
                {
                    new Vector2(-8, -8),
                    new Vector2(8, -8),
                    new Vector2(8, 8),
                    new Vector2(-8, 8)
                });
            }
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