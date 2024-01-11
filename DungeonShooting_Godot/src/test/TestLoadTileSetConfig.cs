using Godot;
using System;
using System.Linq;
using System.Text.Json;
using Godot.Collections;

public partial class TestLoadTileSetConfig : Node2D
{
    private System.Collections.Generic.Dictionary<string, TileSetSplit> _tileSetConfig;
    private TileMap _tileMap;
    
    public override void _Ready()
    {
        InitTileSetConfig();
        _tileMap = GetNode<TileMap>("TileMap");

        var tileSetSplit = _tileSetConfig["TileSet2"];
        var tileSet = tileSetSplit.GetTileSet();
        ResourceSaver.Save(tileSet, "test_tileset.tres");
        
        _tileMap.TileSet = tileSet;
        
        _tileMap.SetCell(0, new Vector2I(5, 5), 1, new Vector2I(0, 0));
        _tileMap.SetCellsTerrainConnect(0, new Array<Vector2I>()
        {
            new Vector2I(10, 10),
            new Vector2I(10, 11),
            new Vector2I(10, 12),
            new Vector2I(11, 10),
            new Vector2I(11, 11),
            new Vector2I(11, 12),
            new Vector2I(12, 10),
            new Vector2I(12, 11),
            new Vector2I(13, 10),
            new Vector2I(13, 11),
        }, 1, 0, false);
    }
    
    //初始化TileSet配置
    private void InitTileSetConfig()
    {
        //加载房间配置信息
        var asText = ResourceManager.LoadText("res://" + GameConfig.RoomTileSetDir + GameConfig.TileSetConfigFile);
        _tileSetConfig = JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, TileSetSplit>>(asText);
		
        //加载所有数据
        foreach (var tileSetSplit in _tileSetConfig)
        {
            tileSetSplit.Value.ReloadTileSetInfo();
        }
    }
    
}
